using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace AutoBa
{
    [DataContract, Serializable]
    [Entity(TableName = "t_upload")]
    public class EntityPatUpload : BaseDataContract
    {
        [DataMember]
        [Entity(FieldName = "JZJLH", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 19)]
        public string JZJLH { get; set; }
        [DataMember]
        [Entity(FieldName = "REGISTERID", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 20)]
        public string REGISTERID { get; set; }
        [DataMember]
        public string InDeptName { get; set; }
        [DataMember]
        public string OutDeptName { get; set; }
        [DataMember]
        public string JBR { get; set; }
        [DataMember]
        public string JBRXM { get; set; }
        [DataMember]
        public string SZ { get; set; }
        [DataMember]
        public string RYSJ { get; set; }
        [DataMember]
        public string CYSJ { get; set; }
        [DataMember]
        public int XH { get; set; }
        [DataMember]
        public int Issucess { get; set; }
        [DataMember]
        public string FailMsg { get; set; }
        [DataMember]
        public EntityFirstPage fpVo { get; set; }
        [DataMember]
        [Entity(FieldName = "OUTDEPTCODE", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 18)]
        public string OUTDEPTCODE { get; set; }
        [DataMember]
        [Entity(FieldName = "BIRTH", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 17)]
        public string BIRTH { get; set; }
        [DataMember]
        [Entity(FieldName = "FTIMES", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 16)]
        public string FTIMES { get; set; }
        [DataMember]
        [Entity(FieldName = "FPRN", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 15)]
        public string FPRN { get; set; }
        [DataMember]
        [Entity(FieldName = "SERNO", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 1)]
        public decimal SERNO { get; set; }
        [DataMember]
        [Entity(FieldName = "UPLOADTYPE", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 2)]
        public decimal UPLOADTYPE { get; set; }
        [DataMember]
        [Entity(FieldName = "PATNAME", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 3)]
        public string PATNAME { get; set; }
        [DataMember]
        [Entity(FieldName = "PATSEX", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 4)]
        public string PATSEX { get; set; }
        [DataMember]
        [Entity(FieldName = "IDCARD", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 5)]
        public string IDCARD { get; set; }
        [DataMember]
        [Entity(FieldName = "INPATIENTID", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 6)]
        public string INPATIENTID { get; set; }
        [DataMember]
        public EntityCyxj xjVo { get; set; }
        [DataMember]
        [Entity(FieldName = "INDEPTCODE", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 7)]
        public string INDEPTCODE { get; set; }
        [DataMember]
        [Entity(FieldName = "RECORDDDATE", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 9)]
        public DateTime RECORDDDATE { get; set; }
        [DataMember]
        [Entity(FieldName = "STATUS", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 10)]
        public decimal STATUS { get; set; }
        [DataMember]
        [Entity(FieldName = "INPATIENTDATE", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 11)]
        public DateTime? INPATIENTDATE { get; set; }
        [DataMember]
        [Entity(FieldName = "OUTHOSPITALDATE", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 12)]
        public DateTime? OUTHOSPITALDATE { get; set; }
        [DataMember]
        [Entity(FieldName = "UPLOADDATE", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 13)]
        public DateTime UPLOADDATE { get; set; }
        [DataMember]
        [Entity(FieldName = "FID", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 14)]
        public string FID { get; set; }
        [DataMember]
        [Entity(FieldName = "OPERCODE", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 8)]
        public string OPERCODE { get; set; }

        [DataMember]
        [Entity(FieldName = "first", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 19)]
        public int first { get; set; }
        [DataMember]
        [Entity(FieldName = "xj", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 20)]
        public int xj { get; set; }
        [DataMember]
        [Entity(FieldName = "firstMsg", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 21)]
        public string firstMsg { get; set; }
        [DataMember]
        [Entity(FieldName = "xjMsg", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 22)]
        public string xjMsg { get; set; }
        [DataMember]
        [Entity(FieldName = "firstSource", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 23)]
        public int firstSource { get; set; }

        public string firstSourceStr {
            get
            {
                if (firstSource == 1)
                    return "病案";
                else if (firstSource == 2)
                {
                    return "ICARE";
                }
                else
                    return "无首页信息";
             }
        }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        public class EnumCols
        {
            public string SERNO = "SERNO";
            public string JZJLH = "JZJLH";
            public string OUTDEPTCODE = "OUTDEPTCODE";
            public string BIRTH = "BIRTH";
            public string FTIMES = "FTIMES";
            public string FPRN = "FPRN";
            public string FID = "FID";
            public string UPLOADDATE = "UPLOADDATE";
            public string OUTHOSPITALDATE = "OUTHOSPITALDATE";
            public string REGISTERID = "REGISTERID";
            public string INPATIENTDATE = "INPATIENTDATE";
            public string RECORDDDATE = "RECORDDDATE";
            public string OPERCODE = "OPERCODE";
            public string DEPTCODE = "DEPTCODE";
            public string INPATIENTID = "INPATIENTID";
            public string IDCARD = "IDCARD";
            public string PATSEX = "PATSEX";
            public string PATNAME = "PATNAME";
            public string UPLOADTYPE = "UPLOADTYPE";
            public string STATUS = "STATUS";
            public string first = "first";
            public string xj = "xj";
            public string firstMsg = "firstMsg";
            public string xjMsg = "xjMsg";
            public string firstSource = "firstSource";
        }
    }

    public class clsInHospitalMainCharge
    {
        public double m_dblMoney { get; set; }
        public string m_strRegisterID { get; set; }
        public string m_strTypeName { get; set; }

    }

    public class EntityQueryBa
    {
        public string inpatientId { get; set; }
        public string fprn { get; set; }
        public string jzjlh { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string IDcard { get; set; }
        public string inTimes { get; set; }
        public string rysj { get; set; }
        public string cysj { get; set; }
    }
}
