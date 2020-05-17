using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Threading;

using TSEdb;
using TSELayoutMappers;
using TSELoadCSV;

namespace LoadCSV
{
    class Program
    {
        static String[] ufs = {"AC", "AL", "AM", "AP", "BA", "BR", "CE", "DF", "ES", "GO", "MA", "MG", "MS", "MT",
                               "PA", "PB", "PE", "PI", "PR", "RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO" };

        static String[] norte = {"AM", "AP", "PA", "RR", "TO" };

        static String[] nordeste = {"AL", "CE", "MA", "PB", "PE", "PI", "RN", "SE" };

        static String[] nortecentrooeste = { "AM", "AP", "PA", "RR", "TO", "AC", "DF", "GO", "MS", "MT", "RO" };

        static String[] sul = {"PR", "RS", "SC" };

        static String[] baesrj = { "BA", "ES", "RJ" };
        static String[] mg = { "MG" };
        static String[] sp = { "SP" };

        static void Main(string[] args)
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            culture.NumberFormat.NumberGroupSeparator = ",";
            Thread.CurrentThread.CurrentCulture = culture;

            Maps maps = new Maps();

            using (TSEdbContext contextDB = new TSEdbContext())
            {
                contextDB.Database.CreateIfNotExists();
            }

            String path = @"C:\Users\Paulo\Documents\Pessoal\Projeto QMR\Eleições {0}\{2}_{0}\{2}_{0}_{1}.txt";

            Int32 totCands = 0;
            Int32 totBens = 0;

            DateTime inicio = DateTime.Now;

            foreach(String uf in baesrj)
            {
                totBens = 0;
                totCands = 0;

                String[][] campos = LoadTSE.ReadCSV(2016, uf, "consulta_cand", path);
                Console.WriteLine(String.Format("{0}: {1:#,#}", uf, campos.Length));
                totCands += LoadTSE.LoadCSV<CandidatoBAESRJ>(campos, maps.CandidatoMap, path);
                LoadTSE.LoadCSV<CandidaturaBAESRJ>(campos, maps.CandidaturaMap, path);

                campos = LoadTSE.ReadCSV(2016, uf, "bem_candidato", path);
                Console.WriteLine(String.Format("{0}: {1:#,#}", uf, campos.Length));
                totBens += LoadTSE.LoadCSV<BemCandidatoBAESRJ>(campos, maps.BemCandidatoMap, path);

                Console.WriteLine("Total de Candidatos: {0}", totCands.ToString("#,#"));
                Console.WriteLine("Total de Bens: {0}", totBens.ToString("#,#"));
            }

            foreach (String uf in sul)
            {
                totBens = 0;
                totCands = 0;

                String[][] campos = LoadTSE.ReadCSV(2016, uf, "consulta_cand", path);
                Console.WriteLine(String.Format("{0}: {1:#,#}", uf, campos.Length));
                totCands += LoadTSE.LoadCSV<CandidatoS>(campos, maps.CandidatoMap, path);
                LoadTSE.LoadCSV<CandidaturaS>(campos, maps.CandidaturaMap, path);

                campos = LoadTSE.ReadCSV(2016, uf, "bem_candidato", path);
                Console.WriteLine(String.Format("{0}: {1:#,#}", uf, campos.Length));
                totBens += LoadTSE.LoadCSV<BemCandidatoS>(campos, maps.BemCandidatoMap, path);

                Console.WriteLine("Total de Candidatos: {0}", totCands.ToString("#,#"));
                Console.WriteLine("Total de Bens: {0}", totBens.ToString("#,#"));
            }

            TimeSpan tempo = DateTime.Now - inicio;
            Console.WriteLine("Tempo total: {0}", tempo.ToString());

            Console.ReadLine();
        }
    }
}
