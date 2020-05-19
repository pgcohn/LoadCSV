using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        
        public static void SumValores<CnddtrType, BensType>()
        {
            GC.Collect();

            TSEdbContext DB = new TSEdbContext();
            {
                IQueryable<Candidatura> cnddtrs = (IQueryable<Candidatura>)DB.Set(typeof(CnddtrType));
                IQueryable<BemCandidato> bens = (IQueryable<BemCandidato>)DB.Set(typeof(BensType));

                var valores = (from bem in bens
                        group bem by bem.BemSeqCand into bensCand
                        select new
                        {
                            Cand = bensCand.Key,
                            Bens = bensCand.Sum(b => b.BemCandValor)
                        }).ToList();

                int i = 0;
                foreach(var valor in valores)
                {
                    Candidatura cnddtr = cnddtrs.First(c=>c.SeqCand.Equals(valor.Cand));
                    cnddtr.TotBensValor = valor.Bens;
                    if (++i % 100 == 0)
                    {
                        try
                        {
                            Console.Write(".");
                            DB.SaveChanges();
                            DB.Dispose();
                            DB = new TSEdbContext();
                            cnddtrs = (IQueryable<Candidatura>)DB.Set(typeof(CnddtrType));
                            bens = (IQueryable<BemCandidato>)DB.Set(typeof(BensType));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Deu merda: {0}", ex.Message);
                            Console.ReadLine();
                        }
                    }
                }

                try
                {
                    Console.WriteLine();
                    DB.SaveChanges();
                    DB.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static Int32 LoadCSV<EntType>(String[][] campos, IEnumerable<TSEMapper> map, String path)
        {
            GC.Collect();

            DateTime inicio = DateTime.Now;

            TSEdbContext DB = new TSEdbContext();
            {
                int i = 0;
                foreach (String[] cmps in campos)
                {
                    try
                    {
                        String jsonString = JsonObject(cmps, map);
                        EntType entity = JsonSerializer.Deserialize<EntType>(jsonString);
                        DB.Set(entity.GetType()).Add(entity);
                        if (++i % 100 == 0)
                        {
                            //Console.Write(".");
                            try
                            {
                                DB.SaveChanges();
                                DB.Dispose();
                                DB = new TSEdbContext();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Deu merda: {0}", ex.Message);
                                Console.ReadLine();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deu merda: {0}", ex.Message);
                        Console.ReadLine();
                    }
                }

                try
                {
                    DB.SaveChanges();
                    DB.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deu merda: {0}", ex.Message);
                    Console.ReadLine();
                }

                DateTime fim = DateTime.Now;
                TimeSpan intervalo = fim - inicio;
                Console.Write("Tempo:  {0} ", intervalo.ToString());
            }

            return campos.Length;
        }

        public static void LoadCands<Cand, Cnddtr, Bens>(String[] regiao, Maps maps, String path)
        {
            int totBens = 0;
            int totCands = 0;

            foreach (String uf in regiao)
            {
                String[][] campos = LoadTSE.ReadCSV(2016, uf, "consulta_cand", path);
                Console.Write(String.Format(Environment.NewLine + "Cnds {0}: {1:#,#} ", uf, campos.Length));
                totCands += LoadTSE.LoadCSV<Cand>(campos, maps.CandidatoMap, path);
                LoadTSE.LoadCSV<Cnddtr>(campos, maps.CandidaturaMap, path);

                campos = LoadTSE.ReadCSV(2016, uf, "bem_candidato", path);
                Console.Write(String.Format(Environment.NewLine + "Bens {0}: {1:#,#} ", uf, campos.Length));
                totBens += LoadTSE.LoadCSV<Bens>(campos, maps.BemCandidatoMap, path);
            }

            Console.WriteLine(Environment.NewLine + "Total de Candidatos: {0}", totCands.ToString("#,#"));
            Console.WriteLine("Total de Bens: {0}" + Environment.NewLine, totBens.ToString("#,#"));

            LoadTSE.SumValores<Cnddtr, Bens>();
        }
    }
}
