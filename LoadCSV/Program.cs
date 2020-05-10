using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        static void Main(string[] args)
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            culture.NumberFormat.NumberGroupSeparator = ",";
            Thread.CurrentThread.CurrentCulture = culture;

            Maps maps = new Maps();
            Int32 totCands = 0;
            Int32 totBens = 0;

            //foreach (String uf in new String[] { "AC" })
            foreach (String uf in ufs)
            {
                totCands += LoadTSE.LoadCSV<Candidato>(2016, uf, "consulta_cand", maps.CandidatoMap);
                totBens += LoadTSE.LoadCSV<BensCandidato>(2016, uf, "bem_candidato", maps.BensCandidatoMap);
            }

            Console.WriteLine("Total de Candidatos: {0}", totCands.ToString("#,#"));
            Console.WriteLine("Total de Bens: {0}", totBens.ToString("#,#"));

            Console.ReadLine();
        }
    }
}
