using System;
using System.Collections.Generic;

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
            new TSEMapper("CandDtNasc",         TSETypes.tseDate,   26 ),
            new TSEMapper("CandCPF",            TSETypes.tseLong,   13 ),
            new TSEMapper("CandTitEleitoral",   TSETypes.tseLong,   27 ),
            new TSEMapper("CandSexo",           TSETypes.tseString, 30 ),
            new TSEMapper("CandCorRaca",        TSETypes.tseInt,    35 ),
            new TSEMapper("CandNacionalidade",  TSETypes.tseString, 38 ),
            new TSEMapper("CandUFNasc",         TSETypes.tseString, 39 ),
            new TSEMapper("CandNomeMunNasc",    TSETypes.tseString, 41 )
        };

        public IEnumerable<TSEMapper> BensCandidatoMap = new TSEMapper[]
        {
            new TSEMapper( "AnoEl",             TSETypes.tseInt,    2 ),
            new TSEMapper( "SeqCand",           TSETypes.tseLong,   5 ),
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
