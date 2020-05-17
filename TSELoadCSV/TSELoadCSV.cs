using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TSEdb;
using TSELayoutMappers;

namespace TSELoadCSV
{
    public class LoadTSE
    {
        public static String[][] ReadCSV(Int32 ano, String uf, String arq, String path)
        {
            List<String[]> campos = new List<String[]>();

            String arquivo = String.Format(path, ano, uf, arq);

            try
            {
                System.IO.StreamReader readerArq = new System.IO.StreamReader(arquivo, Encoding.Default);

                while (!readerArq.EndOfStream)
                {
                    String linha = readerArq.ReadLine();
                    campos.Add(linha.Split(';'));
                }

                readerArq.Close();
            }
            catch (Exception)
            {
                Console.WriteLine(String.Format("{0} deu pau", arquivo));
            }

            return campos.ToArray();
        }

        static public String unq(String value)
        {
            return value.Substring(1, value.Length - 2);
        }

        static String JsonObject(String[] campos, IEnumerable<TSEMapper> map)
        {
            String result = "{";
            Boolean firstField = true;

            foreach (TSEMapper m in map)
            {
                string fld = "\"" + m.FieldName + "\": ";

                switch (m.FieldType)
                {
                    case TSETypes.tseInt:
                        fld += Convert.ToInt32(unq(campos[m.FieldPos]));
                        break;

                    case TSETypes.tseLong:
                        fld += Convert.ToInt64(unq(campos[m.FieldPos]));
                        break;

                    case TSETypes.tseDouble:
                        fld += Convert.ToDouble(unq(campos[m.FieldPos]));
                        break;

                    case TSETypes.tseDate:
                        fld += "\"" + Convert.ToDateTime(unq(campos[m.FieldPos])).ToString("yyyy-MM-ddTHH:mm:ss") + "\"";
                        break;

                    case TSETypes.tseString:
                        fld += campos[m.FieldPos].Replace(@"\", @"\\").Replace('\x09', ' ');
                        break;
                }

                if (firstField)
                {
                    result += fld;
                    firstField = false;
                }
                else
                    result += "," + fld;
            }

            result += "}";

            return result;
        }

        public static Int32 LoadCSV<EntType>(String[][] campos, IEnumerable<TSEMapper> map, String path)
        {
            DateTime inicio = DateTime.Now;
            Console.WriteLine("Inicio: {0}", inicio.ToString());

            using (TSEdbContext DB = new TSEdbContext())
            {
                foreach (String[] cmps in campos)
                {
                    try
                    {
                        String jsonString = JsonObject(cmps, map);
                        EntType entity = JsonSerializer.Deserialize<EntType>(jsonString);
                        DB.Set(entity.GetType()).Add(entity);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deu merda: {0}", ex.Message);
                        Console.ReadLine();
                    }
                }

                DateTime meio = DateTime.Now;
                Console.Write("Meio:   {0} ", meio.ToString());
                TimeSpan intervalo = meio - inicio;
                Console.WriteLine("Tempo:  {0}", intervalo.ToString());

                try
                {
                    DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deu merda: {0}", ex.Message);
                    Console.ReadLine();
                }

                DateTime fim = DateTime.Now;
                Console.Write ("Fim:    {0} ", fim.ToString());
                intervalo = fim - inicio;
                Console.WriteLine("Tempo:  {0}", intervalo.ToString());
            }

            return campos.Length;
        }
    }
}
