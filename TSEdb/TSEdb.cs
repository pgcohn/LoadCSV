using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSEdb
{
    public class Candidato
    {
        public string CandNomeCompleto { get; set; }
        public DateTime CandDtNasc { get; set; }
        public Int64 CandCPF { get; set; }
        public Int64 CandTitEleitoral { get; set; }
        public string CandSexo { get; set; }
        public Int32 CandCorRaca { get; set; }
        public string CandNacionalidade { get; set; }
        public string CandUFNasc { get; set; }
        public string CandNomeMunNasc { get; set; }
    }

    public class BensCandidato
    {
        public Int32 AnoEl { get; set; }
        public Int64 SeqCand { get; set; }
        public String BemCandUF { get; set; }
        public String BemCandTipoBemCod { get; set; }
        public String BemCand { get; set; }
        public Double BemCandValor { get; set; }
    }
}
