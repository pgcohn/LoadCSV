using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MySql.Data.Entity;

namespace TSEdb
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class TSEdbContext : DbContext
    {
        public TSEdbContext()
            : base()
        {

        }

        public DbSet<CandidaturaNCO> CandidaturasNCO { get; set; }
        public DbSet<CandidatoNCO> CandidatosNCO { get; set; }
        public DbSet<BemCandidatoNCO> BensCandidatoNCO { get; set; }

        public DbSet<CandidaturaNE> CandidaturasNE { get; set; }
        public DbSet<CandidatoNE> CandidatosNE { get; set; }
        public DbSet<BemCandidatoNE> BensCandidatoNE { get; set; }

        public DbSet<CandidaturaS> CandidaturasS { get; set; }
        public DbSet<CandidatoS> CandidatosS { get; set; }
        public DbSet<BemCandidatoS> BensCandidatoS { get; set; }

        public DbSet<CandidaturaBAESRJ> CandidaturasBAESRJ { get; set; }
        public DbSet<CandidatoBAESRJ> CandidatosBAESRJ { get; set; }
        public DbSet<BemCandidatoBAESRJ> BensCandidatoBAESRJ { get; set; }

        public DbSet<CandidaturaMG> CandidaturasMG { get; set; }
        public DbSet<CandidatoMG> CandidatosMG { get; set; }
        public DbSet<BemCandidatoMG> BensCandidatoMG { get; set; }

        public DbSet<CandidaturaSP> CandidaturasSP { get; set; }
        public DbSet<CandidatoSP> CandidatosSP { get; set; }
        public DbSet<BemCandidatoSP> BensCandidatoSP { get; set; }

        /*
        public DbSet<Coligacao> Coligacoes { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Ocupacao> Ocupacoes { get; set; }
        public DbSet<GrauInstrucao> GrausInstrucao { get; set; }
        public DbSet<EdoCivil> EdosCivis { get; set; }
        public DbSet<CargoEletivo> CargosEletivos { get; set; }
        public DbSet<UF> UFs { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<PerfilEleitorado> PerfisEleitorado { get; set; }
        public DbSet<ClassLGBTBrasilPartido> ClassLGBTBrasilPartidos { get; set; }
        public DbSet<ClassLGBTBrasilColigacao> ClassLGBTBrasilColigacoes { get; set; }
        public DbSet<Cadeira> Cadeiras { get; set; }
        public DbSet<VotoCandMunZona> VotosCandMunZona { get; set; }
        public DbSet<SitCandidatura> SitsCandidatura { get; set; }
        public DbSet<TipoBem> TiposBem { get; set; }
        */
    }

    public class Candidato
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 CandIdCand { get; set; }
        [Index]
        public Int64 CandSeqCand { get; set; }
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

    [Table("CandidatosNCO")]
    public class CandidatoNCO : Candidato { }
    [Table("CandidatosNE")]
    public class CandidatoNE : Candidato { }
    [Table("CandidatosS")]
    public class CandidatoS : Candidato { }
    [Table("CandidatosBAESRJ")]
    public class CandidatoBAESRJ : Candidato { }
    [Table("CandidatosMG")]
    public class CandidatoMG : Candidato { }
    [Table("CandidatosSP")]
    public class CandidatoSP : Candidato { }

    [Table("Candidaturas")]
    public class Candidatura
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 CnddtrIdCnddtr { get; set; }
        [Index, Column(Order = 0)]
        public Int32 AnoEl { get; set; }
        [Index, Column(Order = 1)]
        public Int32 Turno { get; set; }
        [Index, Column(Order = 2)]
        public Int64 SeqCand { get; set; }
        public Int32 CnddtrNumUrna { get; set; }
        public string CnddtrNomeUrna { get; set; }
        public Int32 CnddtrIdade { get; set; }
        public string CnddtrEmail { get; set; }

        public Double TotBensValor { get; set; }
        public Double CnddtrDespesaMax { get; set; }
        public string CnddtrCodsitTot { get; set; }
        public Int32 TotVotos { get; set; }

        public string UfCod { get; set; }

        public Int32 TipoUE { get; set; }
        public Int32 UECod { get; set; }
        public String MunECod { get; set; }

        public Int32 CodCargo { get; set; }

        public Int32 SitCandidaturaCod { get; set; }

        public String PartidoSigla { get; set; }

        public Int64 SeqCol { get; set; }

        public Int32 OcupCod { get; set; }

        public Int32 GrauInstrucaoCod { get; set; }

        public Int32 EdoCivilCod { get; set; }
    }

    [Table("CandidaturasNCO")]
    public class CandidaturaNCO : Candidatura { }
    [Table("CandidaturasNE")]
    public class CandidaturaNE : Candidatura { }
    [Table("CandidaturasS")]
    public class CandidaturaS : Candidatura { }
    [Table("CandidaturasBAESRJ")]
    public class CandidaturaBAESRJ : Candidatura { }
    [Table("CandidaturasMG")]
    public class CandidaturaMG : Candidatura { }
    [Table("CandidaturasSP")]
    public class CandidaturaSP : Candidatura { }

    public class BemCandidato
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 BemCandID { get; set; }
        public Int32 BemAnoEl { get; set; }
        public Int64 BemSeqCand { get; set; }
        public String BemCandUF { get; set; }
        public String BemCandTipoBemCod { get; set; }
        public String BemCand { get; set; }
        public Double BemCandValor { get; set; }
    }

    [Table("BensCandidatoNCO")]
    public class BemCandidatoNCO : BemCandidato { }
    [Table("BensCandidatoNE")]
    public class BemCandidatoNE : BemCandidato { }
    [Table("BensCandidatoS")]
    public class BemCandidatoS : BemCandidato { }
    [Table("BensCandidatoBAESRJ")]
    public class BemCandidatoBAESRJ : BemCandidato { }
    [Table("BensCandidatoMG")]
    public class BemCandidatoMG : BemCandidato { }
    [Table("BensCandidatoSP")]
    public class BemCandidatoSP : BemCandidato { }

}
