using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using TSELayoutMappers;

namespace TSELoadCSV
{
    public class LoadTSE
    {
        static String[][] ReadCSV(Int32 ano, String uf, String arq)
        {
            List<String[]> campos = new List<String[]>();

            String arquivo = String.Format(@"C:\Users\Paulo\Documents\Pessoal\Projeto QMR\Eleições {0}\{2}_{0}\{2}_{0}_{1}.txt", ano, uf, arq);

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

        public static Int32 LoadCSV<Type>(Int32 ano, String uf, String arquivo, IEnumerable<TSEMapper> map)
        {
            String[][] campos = ReadCSV(2016, uf, arquivo);
            Console.WriteLine(String.Format("{0}: {1:#,#}", uf, campos.Length));

            foreach (String[] cmps in campos)
            {
                try
                {
                    String jsonString = JsonObject(cmps, map);
                    //Console.WriteLine(jsonString);
                    Type cand = JsonSerializer.Deserialize<Type>(jsonString);
                    //Console.WriteLine(String.Format("UF: {0}, Municipio: {1}, Nome: {2}", cand.UF, cand.Municipio, cand.Nome));
                }
                catch (Exception ex)
                {

                }
            }

            return campos.Length;
        }
    }
}
