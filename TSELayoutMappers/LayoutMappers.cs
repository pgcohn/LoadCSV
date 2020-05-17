using System;
using System.Collections.Generic;

using TSEdb;

namespace TSELayoutMappers
{
    public enum TSETypes
    {
        tseString,
        tseDate,
        tseInt,
        tseLong,
        tseDouble
    }

    public class Maps
    {
        public IEnumerable<TSEMapper> CandidatoMap = new TSEMapper[]
        {
            new TSEMapper("CandNomeCompleto",   TSETypes.tseString, 10 ),
            new TSEMapper("CandSeqCand",        TSETypes.tseLong,   11 ),
            new TSEMapper("CandDtNasc",         TSETypes.tseDate,   26 ),
            new TSEMapper("CandCPF",            TSETypes.tseLong,   13 ),
            new TSEMapper("CandTitEleitoral",   TSETypes.tseLong,   27 ),
            new TSEMapper("CandSexo",           TSETypes.tseString, 30 ),
            new TSEMapper("CandCorRaca",        TSETypes.tseInt,    35 ),
            new TSEMapper("CandNacionalidade",  TSETypes.tseString, 38 ),
            new TSEMapper("CandUFNasc",         TSETypes.tseString, 39 ),
            new TSEMapper("CandNomeMunNasc",    TSETypes.tseString, 41 )
        };

        public IEnumerable<TSEMapper> CandidaturaMap = new TSEMapper[]
        {
            new TSEMapper("AnoEl",              TSETypes.tseInt,    2 ),
            new TSEMapper("Turno",              TSETypes.tseInt,    3 ),
            new TSEMapper("SeqCand",            TSETypes.tseLong,   11 ),
            new TSEMapper("CnddtrNumUrna",      TSETypes.tseInt,    12 ),
            new TSEMapper("CnddtrNomeUrna",     TSETypes.tseString, 14 ),
            new TSEMapper("CnddtrIdade",        TSETypes.tseInt,    28 ),
            new TSEMapper("CnddtrEmail",        TSETypes.tseString, 45 ),

            //new TSEMapper("TotBensValor",       TSETypes.tseDouble, 10 ),
            new TSEMapper("CnddtrDespesaMax",   TSETypes.tseDouble, 42 ),
            new TSEMapper("CnddtrCodsitTot",    TSETypes.tseString, 43 ),
            //new TSEMapper("TotVotos",           TSETypes.tseInt, 10 ),

            new TSEMapper("UfCod",              TSETypes.tseString, 5 ),
            //new TSEMapper("TipoUE",             TSETypes.tseInt, 10 ),
            new TSEMapper("UECod",              TSETypes.tseInt,    6 ),
            new TSEMapper("MunECod",            TSETypes.tseString, 7 ),

            new TSEMapper("CodCargo",           TSETypes.tseInt,    8 ),
            new TSEMapper("SitCandidaturaCod",  TSETypes.tseInt,    15 ),
            new TSEMapper("PartidoSigla",       TSETypes.tseString, 18 ),
            new TSEMapper("SeqCol",             TSETypes.tseLong,   20 ),

            new TSEMapper("OcupCod",            TSETypes.tseInt,    24 ),
            new TSEMapper("GrauInstrucaoCod",   TSETypes.tseInt,    31 ),
            new TSEMapper("EdoCivilCod",        TSETypes.tseInt,    33 )
        };

        public IEnumerable<TSEMapper> BemCandidatoMap = new TSEMapper[]
        {
            new TSEMapper( "BemAnoEl",          TSETypes.tseInt,    2 ),
            new TSEMapper( "BemSeqCand",        TSETypes.tseLong,   5 ),
            new TSEMapper( "BemCandUF",         TSETypes.tseString, 4 ),
            new TSEMapper( "BemCandTipoBemCod", TSETypes.tseString, 7 ),
            new TSEMapper( "BemCand",           TSETypes.tseString, 8 ),
            new TSEMapper( "BemCandValor",      TSETypes.tseDouble, 9 )
        };
    }

    public class TSEMapper
    {
        public String FieldName { get; set; }
        public TSETypes FieldType { get; set; }
        public Int32 FieldPos { get; set; }

        public TSEMapper(String _fName, TSETypes _fType, Int32 _fPos)
        {
            FieldName = _fName;
            FieldType = _fType;
            FieldPos = _fPos;
        }
    }
}
