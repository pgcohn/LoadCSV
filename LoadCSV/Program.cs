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
                try
                {
                    contextDB.Database.CreateIfNotExists();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.ReadKey();
                }
            }

            String path = @"C:\Users\Paulo\Documents\Pessoal\Projeto QMR\Eleições {0}\{2}_{0}\{2}_{0}_{1}.txt";

            DateTime inicio = DateTime.Now;
            Console.WriteLine("Inicio Programa: {0}", inicio.ToString());

            LoadTSE.LoadCands<CandidatoS, CandidaturaS, BemCandidatoS>(sul, maps, path);
            LoadTSE.LoadCands<CandidatoMG, CandidaturaMG, BemCandidatoMG>(mg, maps, path);
            LoadTSE.LoadCands<CandidatoNE, CandidaturaNE, BemCandidatoNE>(nordeste, maps, path);
            LoadTSE.LoadCands<CandidatoSP, CandidaturaSP, BemCandidatoSP>(sp, maps, path);
            LoadTSE.LoadCands<CandidatoNCO, CandidaturaNCO, BemCandidatoNCO>(nortecentrooeste, maps, path);
            LoadTSE.LoadCands<CandidatoBAESRJ, CandidaturaBAESRJ, BemCandidatoBAESRJ>(baesrj, maps, path);

            TimeSpan tempo = DateTime.Now - inicio;
            Console.WriteLine("Tempo total: {0}", tempo.ToString());

            Console.ReadLine();
        }
    }
}
