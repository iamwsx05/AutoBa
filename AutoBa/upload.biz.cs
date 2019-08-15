using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace AutoBa
{
    public class UploadBiz
    {
        #region 病案首页 & 出院小结
        /// <summary>
        /// 病案首页 & 出院小结
        /// </summary>
        /// <param name="dicParm"></param>
        /// <returns></returns>
        public List<EntityPatUpload> GetPatList(List<EntityParm> dicParm)
        {
            string SqlBa = string.Empty;
            string SqlReg = string.Empty;
            string SqlJs = string.Empty;
            int n = 0;
            List<EntityPatUpload> data = new List<EntityPatUpload>();
            SqlHelper svcBa = null;
            SqlHelper svc = null;
            try
            {
                #region Sql 首页信息
                svcBa = new SqlHelper(EnumBiz.baDB);
                svc = new SqlHelper(EnumBiz.onlineDB);
                SqlBa = @"select 
                                        a.ftimes as FTIMES,
                                        a.fid,
                                        a.fzyid,
                                        a.fcydate,
                                        '' as JZJLH,
                                        '' as FWSJGDM,
                                        '' as FBGLX,
                                        a.fidcard,
                                        a.FFBBHNEW,a.FFBNEW,
                                        a.FASCARD1,
                                        a.FPRN,
                                        a.FNAME,a.FSEXBH,
                                        a.FSEX,a.FBIRTHDAY,
                                        a.FAGE,a.fcountrybh,
                                        a.fcountry,a.fnationalitybh,
                                        a.fnationality,a.FCSTZ,
                                        a.FRYTZ,a.FBIRTHPLACE,
                                        a.FNATIVE,a.FIDCard,
                                        a.FJOB,a.FSTATUSBH,
                                        a.FSTATUS,a.FCURRADDR,
                                        a.FCURRTELE,a.FCURRPOST,
                                        a.FHKADDR,a.FHKPOST,
                                        a.FDWNAME,a.FDWADDR,
                                        a.FDWTELE,a.FDWPOST,
                                        a.FLXNAME,a.FRELATE,
                                        a.FLXADDR,a.FLXTELE,
                                        a.FRYTJBH,a.FRYTJ,
                                        a.FRYDATE,a.FRYTIME,
                                        a.FRYTYKH,a.FRYDEPT,
                                        a.FRYBS,a.FZKTYKH,
                                        a.FZKDEPT,a.FZKTIME,
                                        a.FCYDATE,a.FCYTIME,
                                        a.FCYTYKH,a.FCYDEPT,
                                        a.FCYBS,a.FDAYS,
                                        a.FMZZDBH,a.FMZZD,
                                        a.FMZDOCTBH,a.FMZDOCT,
                                        a.FJBFXBH,a.FJBFX,
                                        a.FYCLJBH,a.FYCLJ,
                                        a.FQJTIMES,a.FQJSUCTIMES,
                                        a.FPHZD,a.FPHZDNUM,
                                        a.FPHZDBH,a.FIFGMYWBH,
                                        a.FIFGMYW,a.FGMYW,
                                        a.FBODYBH,a.FBODY,
                                        a.FBLOODBH,a.FBLOOD,
                                        a.FRHBH,a.FRH,
                                        a.FKZRBH,a.FKZR,
                                        a.FZRDOCTBH,a.FZRDOCTOR,
                                        a.FZZDOCTBH,a.FZZDOCT,
                                        a.FZYDOCTBH,a.FZYDOCT,
                                        a.FNURSEBH,a.FNURSE,
                                        a.FJXDOCTBH,a.FJXDOCT,
                                        a.FSXDOCTBH,a.FSXDOCT,
                                        a.FBMYBH,
                                        a.FBMY,a.FQUALITYBH,
                                        a.FQUALITY,a.FZKDOCTBH,
                                        a.FZKDOCT,a.FZKNURSEBH,
                                        a.FZKNURSE,a.FZKRQ,
                                        a.FLYFSBH,a.FLYFS,a.FYZOUTHOSTITAL,
                                        a.FSQOUTHOSTITAL,a.FISAGAINRYBH,
                                        a.FISAGAINRY,a.FISAGAINRYMD,
                                        a.FRYQHMDAYS,a.FRYQHMHOURS,
                                        a.FRYQHMMINS,a.FRYQHMCOUNTS,
                                        a.FRYHMDAYS,a.FRYHMHOURS,
                                        a.FRYHMMINS,a.FRYHMCOUNTS,a.FSUM1,
                                        a.FZFJE,a.FZHFWLYLF,a.FZHFWLCZF,a.FZHFWLHLF,
                                        a.FZHFWLQTF,a.FZDLBLF,a.FZDLSSSF,
                                        a.FZDLYXF,a.FZDLLCF,a.FZLLFFSSF,a.FZLLFWLZWLF,
                                        a.FZLLFSSF,a.FZLLFMZF,
                                        a.FZLLFSSZLF,a.FKFLKFF,a.FZYLZF,
                                        a.FXYF,a.FXYLGJF,a.FZCHYF,
                                        a.FZCYF,a.FXYLXF,a.FXYLBQBF,
                                        a.FXYLQDBF,a.FXYLYXYZF,a.FXYLXBYZF,
                                        a.FHCLCJF,a.FHCLZLF,a.FHCLSSF,
                                        a.FQTF,a.FZYF,a.FZKDATE,
                                        a.FJOBBH,a.FZHFWLYLF01,a.FZHFWLYLF02,
                                        a.FZYLZDF,a.FZYLZLF,a.FZYLZLF01,a.FZYLZLF02,
                                        a.FZYLZLF03,a.FZYLZLF04,a.FZYLZLF05,a.FZYLZLF06,a.FZYLQTF,
                                        a.FZCLJGZJF,a.FZYLQTF01,a.FZYLQTF02
                                        from tPatientVisit a where a.fzyid is not null ";
                #endregion

                #region SqlReg  查找住院记录

                SqlReg = @"select t1.registerid_chr,
                                t1.patientid_chr as MZH,
                                d.lastname_vchr as xm,
                                d.birth_dat as birth,
                                d.sex_chr as sex,
                                d.idcard_chr,
                                d.homeaddress_vchr as YJDZ,
                                t1.inpatientid_chr as ipno,
                                t1.inpatientcount_int as rycs,
                                t1.deptid_chr as rydeptid,
                                t11.deptname_vchr as ryks,
                                c.outdeptid_chr as cydeptid,
                                c1.deptname_vchr as cyks,
                                to_char(t1.inpatient_dat, 'yyyymmdd') as RYRQ1,
                                to_char(c.outhospital_dat, 'yyyymmdd') as CYRQ1,
                                t1.inpatient_dat as RYSJ,
                                c.modify_dat as CYSJ,
                                rehis.emrinpatientdate,
                                ee.lastname_vchr as jbr,
                                dd.serno,
                                dd.status,
                                dd.uploaddate
                                from t_opr_bih_register t1
                                left join t_bse_deptdesc t11
                                on t1.deptid_chr = t11.deptid_chr
                                left join t_opr_bih_leave c
                                on t1.registerid_chr = c.registerid_chr
                                left join t_bse_deptdesc c1
                                on c.outdeptid_chr = c1.deptid_chr
                                left join t_opr_bih_registerdetail d
                                on t1.registerid_chr = d.registerid_chr
                                left join t_bse_hisemr_relation rehis
                                on t1.registerid_chr = rehis.registerid_chr
                                left join t_upload dd
                                on t1.registerid_chr = dd.registerid
                                and dd.uploadtype = 1
                                left join t_bse_employee ee
                                on dd.opercode = ee.empno_chr
                                where c.status_int = 1 ";
                #endregion

                #region 结算记录
                SqlJs = @"select a.registerid_chr, a.jzjlh, a.invoiceno_vchr, b.inpatientid_chr
                                  from t_ins_chargezy_csyb a
                                  left join t_opr_bih_register b
                                    on a.registerid_chr = b.registerid_chr
                                    left join t_upload c
                                        on a.registerid_chr = c.registerid and c.uploadtype = 1
                                 where (a.createtime between
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))  ";
                #endregion

                #region 条件
                string strSubJs = string.Empty;
                List<IDataParameter> lstParm = new List<IDataParameter>();
                // 默认参数
                foreach (EntityParm po in dicParm)
                {
                    string keyValue = po.value;
                    switch (po.key)
                    {
                        case "queryDate":
                            IDataParameter parm1 = svc.CreateParm();
                            parm1.Value = keyValue.Split('|')[0] + " 00:00:00";
                            lstParm.Add(parm1);
                            IDataParameter parm2 = svc.CreateParm();
                            parm2.Value = keyValue.Split('|')[1] + " 23:59:59";
                            lstParm.Add(parm2);
                            break;
                        case "cardNo":
                            strSubJs += " and b.inpatientid_chr = " + keyValue + "";
                            break;
                        case "JZJLH":
                            strSubJs += " and a.jzjlh = '" + keyValue + "'";
                            break;
                        case "chkStat":
                            strSubJs += " and c.status is null ";
                            break;
                        default:
                            break;
                    }
                }

                #endregion

                #region 赋值

                if (!string.IsNullOrEmpty(strSubJs))
                    SqlJs += strSubJs;

                DataTable dtJs = svc.GetDataTable(SqlJs, lstParm.ToArray());

                #region
                if (dtJs != null && dtJs.Rows.Count > 0)
                {
                    string ipnoStr = string.Empty;
                    string registeridStr = string.Empty;
                    List<string> lstReg = new List<string>();
                    List<string> lstIpno = new List<string>();
                    DataTable dtBa = null;
                    DataTable dtReg = null;
                    foreach (DataRow drJs in dtJs.Rows)
                    {
                        string registerid = drJs["registerid_chr"].ToString();
                        string ipno = drJs["inpatientid_chr"].ToString();
                        if (lstReg.Contains(registerid))
                            continue;
                        lstReg.Add(registerid);
                        registeridStr += "'" + registerid + "',";

                        if (lstIpno.Contains(ipno))
                            continue;
                        ipnoStr += "'" + ipno + "',";
                        lstIpno.Add(ipno);
                    }

                    if (!string.IsNullOrEmpty(ipnoStr))
                    {
                        ipnoStr = ipnoStr.TrimEnd(',');
                        registeridStr = registeridStr.TrimEnd(',');
                        SqlBa += " and (a.fprn in (" + ipnoStr + ")" + " or a.fzyid in (" + ipnoStr + ")" + ")";
                        dtBa = svcBa.GetDataTable(SqlBa);

                        SqlReg += "and t1.registerid_chr in (" + registeridStr + ")";
                        dtReg = svc.GetDataTable(SqlReg);
                    }

                    foreach (DataRow drReg in dtReg.Rows)
                    {
                        string MZH = drReg["MZH"].ToString();
                        string emrinpatientdate = Function.Datetime(drReg["emrinpatientdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        string ipno = drReg["ipno"].ToString();
                        string registerid = drReg["registerid_chr"].ToString();
                        int rycs = Function.Int(drReg["rycs"].ToString());
                        string cydate = Function.Datetime(drReg["cysj"]).ToString("yyyy-MM-dd");
                        string cydate1 = Function.Datetime(drReg["cysj"]).AddDays(-1).ToString("yyyy-MM-dd");
                        string cydate2 = Function.Datetime(drReg["cysj"]).AddDays(1).ToString("yyyy-MM-dd");
                        string rydate = Function.Datetime(drReg["rysj"]).ToString("yyyy-MM-dd");
                        string rydate1 = Function.Datetime(drReg["rysj"]).AddDays(-1).ToString("yyyy-MM-dd");
                        string rydate2 = Function.Datetime(drReg["rysj"]).AddDays(1).ToString("yyyy-MM-dd");
                        string jzjlh = string.Empty;
                        string FPHM = string.Empty;
                        
                        DataRow[] drrFPHM = dtJs.Select("registerid_chr = '" + registerid + "'");
                        if (drrFPHM.Length > 0)
                        {
                            jzjlh = drrFPHM[0]["jzjlh"].ToString();
                            foreach (DataRow drrF in drrFPHM)
                            {
                                FPHM += drrF["invoiceno_vchr"].ToString() + ",";
                            }
                            if (!string.IsNullOrEmpty(FPHM))
                            {
                                FPHM = FPHM.TrimEnd(',');
                            }
                        }

                        DataRow[] drr = dtBa.Select("fprn =  '" + ipno + "' or fzyid = '" + ipno + "'");
                        if (drr.Length > 0)
                        {
                            foreach (DataRow drrBa in drr)
                            {
                                string fcydate = Function.Datetime(drrBa["fcydate"]).ToString("yyyy-MM-dd");
                                string frydate = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                int ftimes = Function.Int(drrBa["FTIMES"].ToString());
                                if (cydate == fcydate || cydate1 == fcydate || cydate2 == fcydate || rydate == frydate || rydate1 == frydate || rydate2==frydate)
                                {
                                    #region 上传信息 病案首页
                                    EntityPatUpload upVo = new EntityPatUpload();
                                    upVo.fpVo = new EntityFirstPage();

                                    upVo.fpVo.JZJLH = jzjlh;
                                    upVo.fpVo.FWSJGDM = drrBa["FWSJGDM"].ToString();
                                    upVo.fpVo.FFBBHNEW = drrBa["FFBBHNEW"].ToString();
                                    upVo.fpVo.FFBNEW = drrBa["FFBNEW"].ToString();
                                    if (drrBa["FASCARD1"] != DBNull.Value)
                                        upVo.fpVo.FASCARD1 = drrBa["FASCARD1"].ToString();
                                    else
                                        upVo.fpVo.FASCARD1 = "1";
                                    upVo.fpVo.FTIMES = Function.Int(drrBa["FTIMES"].ToString());
                                    upVo.fpVo.FPRN = drrBa["FPRN"].ToString();
                                    upVo.fpVo.FNAME = drrBa["FNAME"].ToString();
                                    upVo.fpVo.FSEXBH = drrBa["FSEXBH"].ToString();
                                    upVo.fpVo.FSEX = drrBa["FSEX"].ToString();
                                    upVo.fpVo.FBIRTHDAY = Function.Datetime(drrBa["FBIRTHDAY"]).ToString("yyyyMMdd");
                                    upVo.fpVo.FAGE = drrBa["FAGE"].ToString();
                                    upVo.fpVo.fcountrybh = drrBa["fcountrybh"].ToString();
                                    if (upVo.fpVo.fcountrybh == "")
                                        upVo.fpVo.fcountrybh = "-";
                                    upVo.fpVo.fcountry = drrBa["fcountry"].ToString();
                                    if (upVo.fpVo.fcountry == "")
                                        upVo.fpVo.fcountry = "-";
                                    upVo.fpVo.fnationalitybh = drrBa["fnationalitybh"].ToString();
                                    if (upVo.fpVo.fnationalitybh == "")
                                        upVo.fpVo.fnationalitybh = "-";
                                    upVo.fpVo.fnationality = drrBa["fnationality"].ToString();
                                    upVo.fpVo.FCSTZ = drrBa["FCSTZ"].ToString();
                                    upVo.fpVo.FRYTZ = drrBa["FRYTZ"].ToString();
                                    upVo.fpVo.FBIRTHPLACE = drrBa["FBIRTHPLACE"].ToString();
                                    upVo.fpVo.FNATIVE = drrBa["FNATIVE"].ToString();
                                    upVo.fpVo.FIDCard = drrBa["FIDCard"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FIDCard))
                                        upVo.fpVo.FIDCard = "无";
                                    upVo.fpVo.FJOB = drrBa["FJOB"].ToString();
                                    upVo.fpVo.FSTATUS = drrBa["FSTATUS"].ToString().Trim();
                                    if (upVo.fpVo.FSTATUS == "已婚")
                                        upVo.fpVo.FSTATUSBH = "2";
                                    else if (upVo.fpVo.FSTATUS == "未婚")
                                        upVo.fpVo.FSTATUSBH = "1";
                                    else if (upVo.fpVo.FSTATUS == "丧偶")
                                        upVo.fpVo.FSTATUSBH = "3";
                                    else if (upVo.fpVo.FSTATUS == "离婚")
                                        upVo.fpVo.FSTATUSBH = "4";
                                    else
                                        upVo.fpVo.FSTATUSBH = "9";
                                    upVo.fpVo.FCURRADDR = drrBa["FCURRADDR"].ToString();
                                    upVo.fpVo.FCURRTELE = drrBa["FCURRTELE"].ToString();
                                    upVo.fpVo.FCURRPOST = drrBa["FCURRPOST"].ToString();
                                    upVo.fpVo.FHKADDR = drrBa["FHKADDR"].ToString();
                                    upVo.fpVo.FHKPOST = drrBa["FHKPOST"].ToString();
                                    upVo.fpVo.FDWNAME = drrBa["FDWNAME"].ToString();
                                    upVo.fpVo.FDWADDR = drrBa["FDWADDR"].ToString();
                                    upVo.fpVo.FDWTELE = drrBa["FDWTELE"].ToString();
                                    upVo.fpVo.FDWPOST = drrBa["FDWPOST"].ToString();
                                    upVo.fpVo.FLXNAME = drrBa["FLXNAME"].ToString();
                                    upVo.fpVo.FRELATE = drrBa["FRELATE"].ToString();
                                    if (upVo.fpVo.FRELATE.Length > 10)
                                        upVo.fpVo.FRELATE = upVo.fpVo.FRELATE.Substring(0, 10);
                                    upVo.fpVo.FLXADDR = drrBa["FLXADDR"].ToString();
                                    upVo.fpVo.FLXTELE = drrBa["FLXTELE"].ToString();
                                    upVo.fpVo.FRYTJBH = drrBa["FRYTJBH"].ToString();
                                    if (upVo.fpVo.FRYTJBH == "")
                                        upVo.fpVo.FRYTJBH = "-";
                                    upVo.fpVo.FRYTJ = drrBa["FRYTJ"].ToString();
                                    if (upVo.fpVo.FRYTJ == "")
                                        upVo.fpVo.FRYTJ = "-";
                                    upVo.fpVo.FRYDATE = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                    upVo.fpVo.FRYTIME = drrBa["FRYTIME"].ToString();
                                    if (upVo.fpVo.FRYTIME.Trim().Length < 4)
                                        upVo.fpVo.FRYTIME = Function.Datetime(drrBa["FRYTIME"].ToString() + ":00:00").ToString("HH:mm:ss");
                                    upVo.fpVo.FRYTYKH = drrBa["FRYTYKH"].ToString();
                                    upVo.fpVo.FRYDEPT = drrBa["FRYDEPT"].ToString();
                                    upVo.fpVo.FRYBS = drrBa["FRYBS"].ToString().Trim();
                                    if (upVo.fpVo.FRYBS == "")
                                        upVo.fpVo.FRYBS = upVo.fpVo.FRYDEPT;
                                    upVo.fpVo.FZKTYKH = drrBa["FZKTYKH"].ToString();
                                    upVo.fpVo.FZKDEPT = drrBa["FZKDEPT"].ToString();
                                    upVo.fpVo.FZKTIME = drrBa["FZKTIME"].ToString();
                                    if (upVo.fpVo.FZKTIME.Length < 4)
                                        upVo.fpVo.FZKTIME = Function.Datetime(drrBa["FZKTIME"].ToString() + ":00:00").ToString("HH:MM:ss");
                                    upVo.fpVo.FCYDATE = Function.Datetime(drrBa["FCYDATE"]).ToString("yyyy-MM-dd");

                                    upVo.fpVo.FCYTIME = drrBa["FCYTIME"].ToString();
                                    if (upVo.fpVo.FCYTIME.Length < 4)
                                        upVo.fpVo.FCYTIME = Function.Datetime(drrBa["FCYTIME"].ToString() + ":00:00").ToString("HH:MM:ss");
                                    upVo.fpVo.FCYTYKH = drrBa["FCYTYKH"].ToString();
                                    upVo.fpVo.FCYDEPT = drrBa["FCYDEPT"].ToString();
                                    upVo.fpVo.FCYBS = drrBa["FCYBS"].ToString().Trim();
                                    if (upVo.fpVo.FCYBS == "")
                                        upVo.fpVo.FCYBS = upVo.fpVo.FCYDEPT;
                                    upVo.fpVo.FDAYS = drrBa["FDAYS"].ToString();
                                    upVo.fpVo.FMZZDBH = drrBa["FMZZDBH"].ToString();
                                    upVo.fpVo.FMZZD = drrBa["FMZZD"].ToString();
                                    upVo.fpVo.FMZDOCTBH = drrBa["FMZDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FMZDOCTBH))
                                        upVo.fpVo.FMZDOCTBH = "无";
                                    upVo.fpVo.FMZDOCT = drrBa["FMZDOCT"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FMZDOCT))
                                        upVo.fpVo.FMZDOCT = "无";
                                    upVo.fpVo.FJBFXBH = drrBa["FJBFXBH"].ToString();
                                    upVo.fpVo.FJBFX = drrBa["FJBFX"].ToString();
                                    upVo.fpVo.FYCLJBH = drrBa["FYCLJBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FYCLJBH))
                                        upVo.fpVo.FYCLJBH = "2";
                                    upVo.fpVo.FYCLJ = drrBa["FYCLJ"].ToString();
                                    if (!string.IsNullOrEmpty(upVo.fpVo.FYCLJBH))
                                        upVo.fpVo.FYCLJ = "是";
                                    else
                                        upVo.fpVo.FYCLJ = "否";
                                    upVo.fpVo.FQJTIMES = drrBa["FQJTIMES"].ToString();
                                    upVo.fpVo.FQJSUCTIMES = drrBa["FQJSUCTIMES"].ToString();
                                    if (!string.IsNullOrEmpty(upVo.fpVo.FQJTIMES) && string.IsNullOrEmpty(upVo.fpVo.FQJSUCTIMES))
                                    {
                                        upVo.fpVo.FQJSUCTIMES = upVo.fpVo.FQJTIMES;
                                    }
                                    upVo.fpVo.FPHZD = drrBa["FPHZD"].ToString();
                                    if (upVo.fpVo.FPHZD.Length > 100)
                                        upVo.fpVo.FPHZD = upVo.fpVo.FPHZD.Substring(0, 100);

                                    if (drrBa["FPHZDNUM"].ToString().Trim() != "")
                                        upVo.fpVo.FPHZDNUM = drrBa["FPHZDNUM"].ToString();
                                    else
                                        upVo.fpVo.FPHZDNUM = "-";

                                    if (drrBa["FPHZDBH"].ToString().Trim() != "")
                                        upVo.fpVo.FPHZDBH = drrBa["FPHZDBH"].ToString();
                                    else
                                        upVo.fpVo.FPHZDBH = "0";

                                    upVo.fpVo.FIFGMYWBH = drrBa["FIFGMYWBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FIFGMYWBH))
                                        upVo.fpVo.FIFGMYWBH = "1";
                                    if (drrBa["FIFGMYW"].ToString() != "")
                                        upVo.fpVo.FIFGMYW = drrBa["FIFGMYW"].ToString();
                                    else
                                        upVo.fpVo.FIFGMYW = "-";
                                    if (drrBa["FGMYW"].ToString() != "")
                                        upVo.fpVo.FGMYW = drrBa["FGMYW"].ToString();
                                    else
                                        upVo.fpVo.FGMYW = "-";
                                    if (drrBa["FBODYBH"].ToString().Trim() != "")
                                        upVo.fpVo.FBODYBH = drrBa["FBODYBH"].ToString();
                                    else
                                        upVo.fpVo.FBODYBH = "2";
                                    if (drrBa["FBODY"].ToString().Trim() != "")
                                        upVo.fpVo.FBODY = drrBa["FBODY"].ToString();
                                    else
                                        upVo.fpVo.FBODY = "否";
                                    upVo.fpVo.FBLOODBH = drrBa["FBLOODBH"].ToString();
                                    upVo.fpVo.FBLOOD = drrBa["FBLOOD"].ToString();
                                    upVo.fpVo.FRHBH = drrBa["FRHBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FRHBH))
                                        upVo.fpVo.FRHBH = "4";
                                    upVo.fpVo.FRH = drrBa["FRH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FRH))
                                        upVo.fpVo.FRH = "未查";
                                    upVo.fpVo.FKZRBH = drrBa["FKZRBH"].ToString();
                                    upVo.fpVo.FKZR = drrBa["FKZR"].ToString();
                                    upVo.fpVo.FZRDOCTBH = drrBa["FZRDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FZRDOCTBH))
                                        upVo.fpVo.FZRDOCTBH = "-";
                                    upVo.fpVo.FZRDOCTOR = drrBa["FZRDOCTOR"].ToString();
                                    upVo.fpVo.FZZDOCTBH = drrBa["FZZDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FZZDOCTBH))
                                        upVo.fpVo.FZZDOCTBH = "-";
                                    upVo.fpVo.FZZDOCT = drrBa["FZZDOCT"].ToString();
                                    upVo.fpVo.FZYDOCTBH = drrBa["FZYDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FZYDOCTBH))
                                        upVo.fpVo.FZYDOCTBH = "-";
                                    upVo.fpVo.FZYDOCT = drrBa["FZYDOCT"].ToString();
                                    upVo.fpVo.FNURSEBH = drrBa["FNURSEBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FNURSEBH))
                                        upVo.fpVo.FNURSEBH = "-";
                                    upVo.fpVo.FNURSE = drrBa["FNURSE"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FNURSE))
                                        upVo.fpVo.FNURSE = "-";
                                    upVo.fpVo.FJXDOCTBH = drrBa["FJXDOCTBH"].ToString();
                                    upVo.fpVo.FJXDOCT = drrBa["FJXDOCT"].ToString();
                                    upVo.fpVo.FSXDOCTBH = drrBa["FSXDOCTBH"].ToString();
                                    upVo.fpVo.FSXDOCT = drrBa["FSXDOCT"].ToString();
                                    upVo.fpVo.FBMYBH = drrBa["FBMYBH"].ToString();
                                    upVo.fpVo.FBMY = drrBa["FBMY"].ToString();
                                    upVo.fpVo.FQUALITYBH = drrBa["FQUALITYBH"].ToString();
                                    upVo.fpVo.FQUALITY = drrBa["FQUALITY"].ToString();
                                    upVo.fpVo.FZKDOCTBH = drrBa["FZKDOCTBH"].ToString();
                                    if (upVo.fpVo.FZKDOCTBH == "")
                                        upVo.fpVo.FZKDOCTBH = "-";
                                    upVo.fpVo.FZKDOCT = drrBa["FZKDOCT"].ToString();
                                    upVo.fpVo.FZKNURSEBH = drrBa["FZKNURSEBH"].ToString().Trim();
                                    if (upVo.fpVo.FZKNURSEBH == "")
                                        upVo.fpVo.FZKNURSEBH = "-";
                                    upVo.fpVo.FZKNURSE = drrBa["FZKNURSE"].ToString();
                                    if (upVo.fpVo.FZKNURSE == "")
                                        upVo.fpVo.FZKNURSE = "-";
                                    upVo.fpVo.FZKRQ = Function.Datetime(drrBa["FZKRQ"]).ToString("yyyyMMdd");

                                    upVo.fpVo.FLYFSBH = drrBa["FLYFSBH"].ToString().Trim();
                                    if (upVo.fpVo.FLYFSBH != "1" || upVo.fpVo.FLYFSBH != "2" ||
                                        upVo.fpVo.FLYFSBH != "3" || upVo.fpVo.FLYFSBH != "4" || upVo.fpVo.FLYFSBH != "5")
                                        upVo.fpVo.FLYFSBH = "9";

                                    upVo.fpVo.FLYFS = drrBa["FLYFS"].ToString();
                                    if (upVo.fpVo.FLYFS.Length >= 26)
                                        upVo.fpVo.FLYFS = upVo.fpVo.FLYFS.Substring(0, 50);

                                    upVo.fpVo.FYZOUTHOSTITAL = drrBa["FYZOUTHOSTITAL"].ToString();
                                    upVo.fpVo.FSQOUTHOSTITAL = drrBa["FSQOUTHOSTITAL"].ToString();
                                    upVo.fpVo.FISAGAINRYBH = drrBa["FISAGAINRYBH"].ToString();
                                    if (upVo.fpVo.FISAGAINRYBH == "")
                                        upVo.fpVo.FISAGAINRYBH = "-";
                                    upVo.fpVo.FISAGAINRY = drrBa["FISAGAINRY"].ToString();
                                    if (upVo.fpVo.FISAGAINRY == "")
                                        upVo.fpVo.FISAGAINRY = "-";
                                    upVo.fpVo.FISAGAINRYMD = drrBa["FISAGAINRYMD"].ToString();
                                    if (upVo.fpVo.FISAGAINRYMD == "")
                                        upVo.fpVo.FISAGAINRYMD = "-";
                                    upVo.fpVo.FRYQHMDAYS = drrBa["FRYQHMDAYS"].ToString();
                                    upVo.fpVo.FRYQHMHOURS = drrBa["FRYQHMHOURS"].ToString();
                                    upVo.fpVo.FRYQHMMINS = drrBa["FRYQHMMINS"].ToString();
                                    upVo.fpVo.FRYQHMCOUNTS = drrBa["FRYQHMCOUNTS"].ToString();
                                    upVo.fpVo.FRYHMDAYS = drrBa["FRYHMDAYS"].ToString();
                                    upVo.fpVo.FRYHMHOURS = drrBa["FRYHMHOURS"].ToString();
                                    upVo.fpVo.FRYHMMINS = drrBa["FRYHMMINS"].ToString();
                                    upVo.fpVo.FRYHMCOUNTS = drrBa["FRYHMCOUNTS"].ToString();
                                    upVo.fpVo.FSUM1 = Function.Dec(drrBa["FSUM1"].ToString());
                                    upVo.fpVo.FZFJE = Function.Dec(drrBa["FZFJE"].ToString());
                                    upVo.fpVo.FZHFWLYLF = Function.Dec(drrBa["FZHFWLYLF"].ToString());
                                    upVo.fpVo.FZHFWLCZF = Function.Dec(drrBa["FZHFWLCZF"].ToString());
                                    upVo.fpVo.FZHFWLHLF = Function.Dec(drrBa["FZHFWLHLF"].ToString());
                                    upVo.fpVo.FZHFWLQTF = Function.Dec(drrBa["FZHFWLQTF"].ToString());
                                    upVo.fpVo.FZDLBLF = Function.Dec(drrBa["FZDLBLF"].ToString());
                                    upVo.fpVo.FZDLSSSF = Function.Dec(drrBa["FZDLSSSF"].ToString());
                                    upVo.fpVo.FZDLYXF = Function.Dec(drrBa["FZDLYXF"].ToString());
                                    upVo.fpVo.FZDLLCF = Function.Dec(drrBa["FZDLLCF"].ToString());
                                    upVo.fpVo.FZLLFFSSF = Function.Dec(drrBa["FZLLFFSSF"].ToString());
                                    upVo.fpVo.FZLLFWLZWLF = Function.Dec(drrBa["FZLLFWLZWLF"].ToString());
                                    upVo.fpVo.FZLLFSSF = Function.Dec(drrBa["FZLLFSSF"].ToString());
                                    upVo.fpVo.FZLLFMZF = Function.Dec(drrBa["FZLLFMZF"].ToString());
                                    upVo.fpVo.FZLLFSSZLF = Function.Dec(drrBa["FZLLFSSZLF"].ToString());
                                    upVo.fpVo.FKFLKFF = Function.Dec(drrBa["FKFLKFF"].ToString());
                                    upVo.fpVo.FZYLZF = Function.Dec(drrBa["FZYLZF"].ToString());
                                    upVo.fpVo.FXYF = Function.Dec(drrBa["FXYF"].ToString());
                                    upVo.fpVo.FXYLGJF = Function.Dec(drrBa["FXYLGJF"].ToString());
                                    upVo.fpVo.FZCHYF = Function.Dec(drrBa["FZCHYF"].ToString());
                                    upVo.fpVo.FZCYF = Function.Dec(drrBa["FZCYF"].ToString());
                                    upVo.fpVo.FXYLXF = Function.Dec(drrBa["FXYLXF"].ToString());
                                    upVo.fpVo.FXYLBQBF = Function.Dec(drrBa["FXYLBQBF"].ToString());
                                    upVo.fpVo.FXYLQDBF = Function.Dec(drrBa["FXYLQDBF"].ToString());
                                    upVo.fpVo.FXYLYXYZF = Function.Dec(drrBa["FXYLYXYZF"].ToString());
                                    upVo.fpVo.FXYLXBYZF = Function.Dec(drrBa["FXYLXBYZF"].ToString());
                                    upVo.fpVo.FHCLCJF = Function.Dec(drrBa["FHCLCJF"].ToString());
                                    upVo.fpVo.FHCLZLF = Function.Dec(drrBa["FHCLZLF"].ToString());
                                    upVo.fpVo.FHCLSSF = Function.Dec(drrBa["FHCLSSF"].ToString());
                                    upVo.fpVo.FQTF = Function.Dec(drrBa["FQTF"]);
                                    upVo.fpVo.FBGLX = drrBa["FBGLX"].ToString();

                                    if (drrBa["fidcard"].ToString() != "")
                                        upVo.fpVo.GMSFHM = drrBa["fidcard"].ToString();
                                    else
                                        upVo.fpVo.GMSFHM = drReg["idcard_chr"].ToString();

                                    upVo.fpVo.FZYF = Function.Dec(drrBa["FZYF"].ToString());
                                    if (drrBa["FZKDATE"] != DBNull.Value)
                                        upVo.fpVo.FZKDATE = Function.Datetime(drrBa["FZKDATE"]).ToString("yyyy-MM-dd");
                                    else
                                        upVo.fpVo.FZKDATE = "";

                                    upVo.fpVo.FZKTIME = Function.Datetime(upVo.fpVo.FZKDATE + " " + upVo.fpVo.FZKTIME).ToString("yyyyMMddHHmmss");
                                    upVo.fpVo.FJOBBH = drrBa["FJOBBH"].ToString();
                                    upVo.fpVo.FZHFWLYLF01 = Function.Dec(drrBa["FZHFWLYLF01"]);
                                    upVo.fpVo.FZHFWLYLF02 = Function.Dec(drrBa["FZHFWLYLF02"]);
                                    upVo.fpVo.FZYLZDF = Function.Dec(drrBa["FZYLZDF"]);
                                    upVo.fpVo.FZYLZLF = Function.Dec(drrBa["FZYLZLF"]);
                                    upVo.fpVo.FZYLZLF01 = Function.Dec(drrBa["FZYLZLF01"]);
                                    upVo.fpVo.FZYLZLF02 = Function.Dec(drrBa["FZYLZLF02"]);
                                    upVo.fpVo.FZYLZLF03 = Function.Dec(drrBa["FZYLZLF03"]);
                                    upVo.fpVo.FZYLZLF04 = Function.Dec(drrBa["FZYLZLF04"]);
                                    upVo.fpVo.FZYLZLF05 = Function.Dec(drrBa["FZYLZLF05"]);
                                    upVo.fpVo.FZYLZLF06 = Function.Dec(drrBa["FZYLZLF06"]);
                                    upVo.fpVo.FZYLQTF = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZCLJGZJF = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZYLQTF01 = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZYLQTF02 = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZYID = drrBa["FZYID"].ToString();

                                    upVo.fpVo.ZYH = ipno;
                                    upVo.fpVo.FPHM = FPHM;

                                    #endregion

                                    #region 上传信息 出院小结
                                    DataTable dtXj = GetPatCyxjList2(ipno, emrinpatientdate);

                                    if (dtXj != null && dtXj.Rows.Count > 0)
                                    {
                                        #region 上传信息 出院小结
                                        DataRow drXj = dtXj.Rows[0];

                                        upVo.xjVo = new EntityCyxj();
                                        upVo.xjVo.JZJLH = jzjlh;
                                        upVo.xjVo.MZH = MZH;
                                        upVo.xjVo.ZYH = ipno;
                                        upVo.xjVo.MZZD = drrBa["FMZZD"].ToString();
                                        if (upVo.xjVo.MZZD.Length > 100)
                                            upVo.xjVo.MZZD = upVo.xjVo.MZZD.Substring(0, 100);
                                        if (string.IsNullOrEmpty(upVo.xjVo.MZZD))
                                            upVo.xjVo.MZZD = "-";
                                        upVo.xjVo.RYZD = drXj["inhospitaldiagnose"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.RYZD))
                                            upVo.xjVo.RYZD = drrBa["FMZZD"].ToString();
                                        upVo.xjVo.CYZD = drXj["outhospitaldiagnose"].ToString().Trim();
                                        if (drXj["outhospitaldiagnose"] == DBNull.Value)
                                            upVo.xjVo.CYZD = "-";
                                        upVo.xjVo.XM = drrBa["FNAME"].ToString(); ;
                                        upVo.xjVo.XB = drrBa["FSEX"].ToString();
                                        if (upVo.xjVo.XB == "男")
                                            upVo.xjVo.XB = "1";
                                        else if (upVo.xjVo.XB == "女")
                                            upVo.xjVo.XB = "2";
                                        else upVo.xjVo.XB = "9";
                                        upVo.xjVo.NL = drrBa["FAGE"].ToString();

                                        if (drrBa["fidcard"].ToString() != "")
                                            upVo.xjVo.GMSFHM = drrBa["fidcard"].ToString();
                                        else
                                            upVo.xjVo.GMSFHM = drReg["idcard_chr"].ToString();

                                        upVo.xjVo.RYRQ = drReg["RYRQ1"].ToString();
                                        upVo.xjVo.CYRQ = drReg["CYRQ1"].ToString();
                                        upVo.xjVo.RYSJ = drReg["RYSJ"].ToString();
                                        upVo.xjVo.CYSJ = drReg["CYSJ"].ToString();
                                        upVo.xjVo.ZYTS = drrBa["FDAYS"].ToString();
                                        upVo.xjVo.ZY = drrBa["fjob"].ToString();
                                        upVo.xjVo.JG = drrBa["FNATIVE"].ToString();
                                        if (string.IsNullOrEmpty(upVo.xjVo.JG))
                                            upVo.xjVo.JG = "无";
                                        upVo.xjVo.YJDZ = drReg["YJDZ"].ToString();
                                        if (string.IsNullOrEmpty(upVo.xjVo.YJDZ))
                                            upVo.xjVo.YJDZ = "-";
                                        upVo.xjVo.CYYZ = drXj["outhospitaladvice_right"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.CYYZ))
                                            upVo.xjVo.CYYZ = "-";
                                        upVo.xjVo.RYQK = drXj["inhospitaldiagnose_right"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.RYQK))
                                            upVo.xjVo.RYQK = "-";
                                        upVo.xjVo.YSQM = drXj["doctorname"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.YSQM))
                                            upVo.xjVo.YSQM = "-";
                                        upVo.xjVo.RYHCLGC = "-";
                                        upVo.xjVo.CYSQK = drXj["outhospitalcase_right"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.CYSQK))
                                            upVo.xjVo.CYSQK = "-";
                                        upVo.xjVo.ZLJG = drXj["inhospitalby"].ToString().Trim();
                                        if (upVo.xjVo.ZLJG.Length > 1000)
                                            upVo.xjVo.ZLJG = upVo.xjVo.ZLJG.Substring(0, 1000);
                                        if (string.IsNullOrEmpty(upVo.xjVo.ZLJG))
                                            upVo.xjVo.ZLJG = "-";

                                        if (ftimes > 0)
                                            upVo.xjVo.FTIMES = ftimes.ToString();
                                        else
                                            upVo.xjVo.FTIMES = rycs.ToString();

                                        upVo.xjVo.FSUM1 = Function.Dec(drrBa["FSUM1"].ToString());
                                        upVo.xjVo.FPHM = FPHM;
                                        #endregion
                                    }
                                    #endregion

                                    #region  显示列表
                                    upVo.XH = ++n;
                                    upVo.UPLOADTYPE = 1;
                                    upVo.PATNAME = upVo.fpVo.FNAME;
                                    upVo.PATSEX = upVo.fpVo.FSEX;
                                    upVo.IDCARD = upVo.fpVo.FIDCard;
                                    upVo.INPATIENTID = upVo.fpVo.FZYID;
                                    upVo.INDEPTCODE = drReg["rydeptid"].ToString();
                                    upVo.INPATIENTDATE = Function.Datetime(Function.Datetime(drReg["rysj"]).ToString("yyyy-MM-dd"));
                                    upVo.OUTHOSPITALDATE = Function.Datetime(Function.Datetime(drReg["cysj"]).ToString("yyyy-MM-dd"));
                                    upVo.RYSJ = Function.Datetime(drReg["rysj"]).ToString("yyyy-MM-dd HH:mm");
                                    upVo.CYSJ = Function.Datetime(drReg["cysj"]).ToString("yyyy-MM-dd HH:mm");
                                    upVo.FPRN = upVo.fpVo.FPRN;
                                    upVo.FTIMES = drReg["rycs"].ToString();
                                    upVo.BIRTH = Function.Datetime(drReg["birth"]).ToString("yyyy-mm-dd");
                                    upVo.InDeptName = drReg["ryks"].ToString();
                                    upVo.OutDeptName = drReg["cyks"].ToString();
                                    upVo.OUTDEPTCODE = drReg["cydeptid"].ToString();
                                    upVo.JZJLH = jzjlh;
                                    upVo.REGISTERID = drReg["registerid_chr"].ToString();
                                    upVo.STATUS = Function.Int(drReg["status"]);
                                    upVo.SERNO = Function.Dec(drReg["serno"]);
                                    if (drReg["status"].ToString() == "1")
                                        upVo.SZ = "已上传";
                                    else
                                        upVo.SZ = "未上传";

                                    if (drReg["jbr"] != DBNull.Value)
                                        upVo.JBRXM = drReg["jbr"].ToString();
                                    if (drReg["uploaddate"] != DBNull.Value)
                                        upVo.UPLOADDATE = drReg["uploaddate"].ToString();

                                    #endregion

                                    data.Add(upVo);
                                }
                            }
                        }
                    }
                }
                #endregion

                #endregion
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetPatFirstList--" + e);
            }
            finally
            {
                svc = null;
            }
            return data;
        }
        #endregion

        #region 病案首页 & 出院小结 代结算
        /// <summary>
        /// 病案首页 & 出院小结 返回结算
        /// </summary>
        /// <param name="dicParm"></param>
        /// <returns></returns>
        public List<EntityPatUpload> GetPatList2(List<EntityParm> dicParm)
        {
            string SqlBa = string.Empty;
            string Sql2 = string.Empty;
            string SlqXj1 = string.Empty;
            string SlqXj2 = string.Empty;
            string SqlJs = string.Empty;
            int n = 0;
            string jzjlhStr = string.Empty;
            // DataRow[] drr = null;
            List<EntityPatUpload> data = new List<EntityPatUpload>();
            SqlHelper svcBa = null;
            SqlHelper svc = null;
            try
            {
                #region Sql 首页信息
                svcBa = new SqlHelper(EnumBiz.baDB);
                svc = new SqlHelper(EnumBiz.onlineDB);
                SqlBa = @"select 
                                        a.ftimes as FTIMES,
                                        a.fid,
                                        a.fzyid,
                                        a.fcydate,
                                        '' as JZJLH,
                                        '' as FWSJGDM,
                                        '' as FBGLX,
                                        a.fidcard,
                                        a.FFBBHNEW,a.FFBNEW,
                                        a.FASCARD1,
                                        a.FPRN,
                                        a.FNAME,a.FSEXBH,
                                        a.FSEX,a.FBIRTHDAY,
                                        a.FAGE,a.fcountrybh,
                                        a.fcountry,a.fnationalitybh,
                                        a.fnationality,a.FCSTZ,
                                        a.FRYTZ,a.FBIRTHPLACE,
                                        a.FNATIVE,a.FIDCard,
                                        a.FJOB,a.FSTATUSBH,
                                        a.FSTATUS,a.FCURRADDR,
                                        a.FCURRTELE,a.FCURRPOST,
                                        a.FHKADDR,a.FHKPOST,
                                        a.FDWNAME,a.FDWADDR,
                                        a.FDWTELE,a.FDWPOST,
                                        a.FLXNAME,a.FRELATE,
                                        a.FLXADDR,a.FLXTELE,
                                        a.FRYTJBH,a.FRYTJ,
                                        a.FRYDATE,a.FRYTIME,
                                        a.FRYTYKH,a.FRYDEPT,
                                        a.FRYBS,a.FZKTYKH,
                                        a.FZKDEPT,a.FZKTIME,
                                        a.FCYDATE,a.FCYTIME,
                                        a.FCYTYKH,a.FCYDEPT,
                                        a.FCYBS,a.FDAYS,
                                        a.FMZZDBH,a.FMZZD,
                                        a.FMZDOCTBH,a.FMZDOCT,
                                        a.FJBFXBH,a.FJBFX,
                                        a.FYCLJBH,a.FYCLJ,
                                        a.FQJTIMES,a.FQJSUCTIMES,
                                        a.FPHZD,a.FPHZDNUM,
                                        a.FPHZDBH,a.FIFGMYWBH,
                                        a.FIFGMYW,a.FGMYW,
                                        a.FBODYBH,a.FBODY,
                                        a.FBLOODBH,a.FBLOOD,
                                        a.FRHBH,a.FRH,
                                        a.FKZRBH,a.FKZR,
                                        a.FZRDOCTBH,a.FZRDOCTOR,
                                        a.FZZDOCTBH,a.FZZDOCT,
                                        a.FZYDOCTBH,a.FZYDOCT,
                                        a.FNURSEBH,a.FNURSE,
                                        a.FJXDOCTBH,a.FJXDOCT,
                                        a.FSXDOCTBH,a.FSXDOCT,
                                        a.FBMYBH,
                                        a.FBMY,a.FQUALITYBH,
                                        a.FQUALITY,a.FZKDOCTBH,
                                        a.FZKDOCT,a.FZKNURSEBH,
                                        a.FZKNURSE,a.FZKRQ,
                                        a.FLYFSBH,a.FLYFS,a.FYZOUTHOSTITAL,
                                        a.FSQOUTHOSTITAL,a.FISAGAINRYBH,
                                        a.FISAGAINRY,a.FISAGAINRYMD,
                                        a.FRYQHMDAYS,a.FRYQHMHOURS,
                                        a.FRYQHMMINS,a.FRYQHMCOUNTS,
                                        a.FRYHMDAYS,a.FRYHMHOURS,
                                        a.FRYHMMINS,a.FRYHMCOUNTS,a.FSUM1,
                                        a.FZFJE,a.FZHFWLYLF,a.FZHFWLCZF,a.FZHFWLHLF,
                                        a.FZHFWLQTF,a.FZDLBLF,a.FZDLSSSF,
                                        a.FZDLYXF,a.FZDLLCF,a.FZLLFFSSF,a.FZLLFWLZWLF,
                                        a.FZLLFSSF,a.FZLLFMZF,
                                        a.FZLLFSSZLF,a.FKFLKFF,a.FZYLZF,
                                        a.FXYF,a.FXYLGJF,a.FZCHYF,
                                        a.FZCYF,a.FXYLXF,a.FXYLBQBF,
                                        a.FXYLQDBF,a.FXYLYXYZF,a.FXYLXBYZF,
                                        a.FHCLCJF,a.FHCLZLF,a.FHCLSSF,
                                        a.FQTF,a.FZYF,a.FZKDATE,
                                        a.FJOBBH,a.FZHFWLYLF01,a.FZHFWLYLF02,
                                        a.FZYLZDF,a.FZYLZLF,a.FZYLZLF01,a.FZYLZLF02,
                                        a.FZYLZLF03,a.FZYLZLF04,a.FZYLZLF05,a.FZYLZLF06,a.FZYLQTF,
                                        a.FZCLJGZJF,a.FZYLQTF01,a.FZYLQTF02
                                        from tPatientVisit a where a.fzyid is not null ";
                #endregion

                #region  查找住院记录

                Sql2 = @"select t1.registerid_chr,
                                t1.patientid_chr as MZH,
                                d.lastname_vchr as xm,
                                d.birth_dat as birth,
                                d.sex_chr as sex,
                                d.idcard_chr,
                                d.homeaddress_vchr as YJDZ,
                                t1.inpatientid_chr as ipno,
                                t1.inpatientcount_int as rycs,
                                t1.deptid_chr as rydeptid,
                                t11.deptname_vchr as ryks,
                                c.outdeptid_chr as cydeptid,
                                c1.deptname_vchr as cyks,
                                to_char(t1.inpatient_dat, 'yyyymmdd') as RYRQ1,
                                to_char(c.outhospital_dat, 'yyyymmdd') as CYRQ1,
                                t1.inpatient_dat as RYSJ,
                                c.modify_dat as CYSJ,
                                rehis.emrinpatientdate,
                                ee.lastname_vchr as jbr,
                                dd.serno,
                                dd.status,
                                dd.uploaddate
                                from t_opr_bih_register t1
                                left join t_bse_deptdesc t11
                                on t1.deptid_chr = t11.deptid_chr
                                left join t_opr_bih_leave c
                                on t1.registerid_chr = c.registerid_chr
                                left join t_bse_deptdesc c1
                                on c.outdeptid_chr = c1.deptid_chr
                                left join t_opr_bih_registerdetail d
                                on t1.registerid_chr = d.registerid_chr
                                left join t_bse_hisemr_relation rehis
                                on t1.registerid_chr = rehis.registerid_chr
                                left join t_upload dd
                                on t1.registerid_chr = dd.registerid
                                and dd.uploadtype = 1
                                left join t_bse_employee ee
                                on dd.opercode = ee.empno_chr
                                where c.status_int = 1  ";
                #endregion

                #region 结算记录
                SqlJs = @"select a.* from BaTemp a 
                                left join t_upload c
                                on a.jzjlh = c.jzjlh and c.uploadtype = 1
                                where a.name is not null ";
                #endregion

                #region 查找发票号
                string sqlFp = @"select a.status_int as status, a.status_int, a.invoiceno_vchr as invono, d.registerid_chr
                          from t_opr_bih_invoice2 a,
                               t_opr_bih_chargedefinv b,
                               t_opr_bih_charge c,
                               t_opr_bih_register d
                         where a.invoiceno_vchr = b.invoiceno_vchr 
                           and b.chargeno_chr = c.chargeno_chr 
                           and c.registerid_chr  = d.registerid_chr
                           and c.status_int = 1   ";
                #endregion

                #region 条件
                string strSubJs = string.Empty;
                List<IDataParameter> lstParm = new List<IDataParameter>();
                // 默认参数
                foreach (EntityParm po in dicParm)
                {
                    string keyValue = po.value;
                    switch (po.key)
                    {
                        case "queryDate":
                            IDataParameter parm1 = svc.CreateParm();
                            parm1.Value = keyValue.Split('|')[0] + " 00:00:00";
                            lstParm.Add(parm1);
                            IDataParameter parm2 = svc.CreateParm();
                            parm2.Value = keyValue.Split('|')[1] + " 23:59:59";
                            lstParm.Add(parm2);
                            break;
                        case "cardNo":
                            strSubJs += " and a.zyh = " + keyValue + "";
                            break;
                        case "JZJLH":
                            strSubJs += " and a.jzjlh = '" + keyValue + "'";
                            break;
                        case "chkStat":
                            strSubJs += " and c.status is null ";
                            break;
                        default:
                            break;
                    }
                }

                #endregion

                #region 赋值

                if (!string.IsNullOrEmpty(strSubJs))
                    SqlJs += strSubJs;

                DataTable dtJs = svc.GetDataTable(SqlJs);

                #region
                if (dtJs != null && dtJs.Rows.Count > 0)
                {
                    string ipnoStr = string.Empty;
                    List<string> lstIpno = new List<string>();
                    DataTable dtBa = null;
                    DataTable dt2 = null;
                    DataTable dtFp = null;
                    foreach (DataRow drJs in dtJs.Rows)
                    {
                        string ipno = drJs["zyh"].ToString();

                        if (lstIpno.Contains(ipno) || string.IsNullOrEmpty(ipno))
                            continue;
                        ipnoStr += "'" + ipno + "',";
                        lstIpno.Add(ipno);
                    }

                    if (!string.IsNullOrEmpty(ipnoStr))
                    {
                        ipnoStr = ipnoStr.TrimEnd(',');
                        SqlBa += " and (a.fprn in (" + ipnoStr + ")" + " or a.fzyid in (" + ipnoStr + ")" + ")";
                        dtBa = svcBa.GetDataTable(SqlBa);

                        Sql2 += "and t1.INPATIENTID_CHR in (" + ipnoStr + ")";
                        dt2 = svc.GetDataTable(Sql2);
                        sqlFp += " and d.INPATIENTID_CHR in (" + ipnoStr + ")";
                        dtFp = svc.GetDataTable(sqlFp);
                    }

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        string jzjlh = string.Empty;
                        string MZH = dr2["MZH"].ToString();
                        string ipno = dr2["ipno"].ToString();
                        string registerid = dr2["registerid_chr"].ToString();
                        int rycs = Function.Int(dr2["rycs"].ToString());
                        string cydate = Function.Datetime(dr2["cysj"]).ToString("yyyy-MM-dd");
                        string cydate1 = Function.Datetime(dr2["cysj"]).AddDays(-1).ToString("yyyy-MM-dd");
                        string cydate2 = Function.Datetime(dr2["cysj"]).AddDays(1).ToString("yyyy-MM-dd");
                        string rydate = Function.Datetime(dr2["rysj"]).ToString("yyyy-MM-dd");
                        string emrinpatientdate = Function.Datetime(dr2["emrinpatientdate"]).ToString("yyyy-MM-dd HH:mm:ss");

                        //出入院日期等于临时表
                        DataRow[] drrTemp = dtJs.Select("zyh = '" + ipno + "' and ( ryrq = '" + rydate + "' or cyrq = '" + cydate + "')");
                        if (drrTemp == null || drrTemp.Length <= 0)
                        {
                            continue;
                        }
                        else
                        {
                            jzjlh = drrTemp[0]["jzjlh"].ToString();
                        }

                        string FPHM = string.Empty;
                        DataRow[] drrFPHM = dtFp.Select("registerid_chr = '" + registerid + "'");
                        if (drrFPHM.Length > 0)
                        {
                            foreach (DataRow drrF in drrFPHM)
                            {
                                FPHM += drrF["invono"].ToString() + ",";
                            }
                            if (!string.IsNullOrEmpty(FPHM))
                            {
                                FPHM = FPHM.TrimEnd(',');
                            }
                        }

                        DataRow[] drr = dtBa.Select("fprn =  '" + ipno + "' or fzyid = '" + ipno + "'");
                        if (drr.Length > 0)
                        {
                            foreach (DataRow drrBa in drr)
                            {
                                string fcydate = Function.Datetime(drrBa["fcydate"]).ToString("yyyy-MM-dd");
                                string frydate = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                int ftimes = Function.Int(drrBa["FTIMES"].ToString());
                                if (cydate == fcydate || cydate1 == fcydate || cydate2 == fcydate || rydate == frydate)
                                {
                                    #region 上传信息 病案首页
                                    EntityPatUpload upVo = new EntityPatUpload();
                                    upVo.fpVo = new EntityFirstPage();

                                    upVo.fpVo.JZJLH = jzjlh;

                                    upVo.fpVo.FWSJGDM = drrBa["FWSJGDM"].ToString();
                                    upVo.fpVo.FFBBHNEW = drrBa["FFBBHNEW"].ToString();
                                    upVo.fpVo.FFBNEW = drrBa["FFBNEW"].ToString();
                                    if (drrBa["FASCARD1"] != DBNull.Value)
                                        upVo.fpVo.FASCARD1 = drrBa["FASCARD1"].ToString();
                                    else
                                        upVo.fpVo.FASCARD1 = "1";
                                    upVo.fpVo.FTIMES = Function.Int(drrBa["FTIMES"].ToString());
                                    upVo.fpVo.FPRN = drrBa["FPRN"].ToString();
                                    upVo.fpVo.FNAME = drrBa["FNAME"].ToString();
                                    upVo.fpVo.FSEXBH = drrBa["FSEXBH"].ToString();
                                    upVo.fpVo.FSEX = drrBa["FSEX"].ToString();
                                    upVo.fpVo.FBIRTHDAY = Function.Datetime(drrBa["FBIRTHDAY"]).ToString("yyyyMMdd");
                                    upVo.fpVo.FAGE = drrBa["FAGE"].ToString();
                                    upVo.fpVo.fcountrybh = drrBa["fcountrybh"].ToString();
                                    if (upVo.fpVo.fcountrybh == "")
                                        upVo.fpVo.fcountrybh = "-";
                                    upVo.fpVo.fcountry = drrBa["fcountry"].ToString();
                                    if (upVo.fpVo.fcountry == "")
                                        upVo.fpVo.fcountry = "-";
                                    upVo.fpVo.fnationalitybh = drrBa["fnationalitybh"].ToString();
                                    if (upVo.fpVo.fnationalitybh == "")
                                        upVo.fpVo.fnationalitybh = "-";
                                    upVo.fpVo.fnationality = drrBa["fnationality"].ToString();
                                    upVo.fpVo.FCSTZ = drrBa["FCSTZ"].ToString();
                                    upVo.fpVo.FRYTZ = drrBa["FRYTZ"].ToString();
                                    upVo.fpVo.FBIRTHPLACE = drrBa["FBIRTHPLACE"].ToString();
                                    upVo.fpVo.FNATIVE = drrBa["FNATIVE"].ToString();
                                    upVo.fpVo.FIDCard = drrBa["FIDCard"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FIDCard))
                                        upVo.fpVo.FIDCard = "无";
                                    upVo.fpVo.FJOB = drrBa["FJOB"].ToString();
                                    upVo.fpVo.FSTATUS = drrBa["FSTATUS"].ToString().Trim();
                                    if (upVo.fpVo.FSTATUS == "已婚")
                                        upVo.fpVo.FSTATUSBH = "2";
                                    else if (upVo.fpVo.FSTATUS == "未婚")
                                        upVo.fpVo.FSTATUSBH = "1";
                                    else if (upVo.fpVo.FSTATUS == "丧偶")
                                        upVo.fpVo.FSTATUSBH = "3";
                                    else if (upVo.fpVo.FSTATUS == "离婚")
                                        upVo.fpVo.FSTATUSBH = "4";
                                    else
                                        upVo.fpVo.FSTATUSBH = "9";
                                    upVo.fpVo.FCURRADDR = drrBa["FCURRADDR"].ToString();
                                    upVo.fpVo.FCURRTELE = drrBa["FCURRTELE"].ToString();
                                    upVo.fpVo.FCURRPOST = drrBa["FCURRPOST"].ToString();
                                    upVo.fpVo.FHKADDR = drrBa["FHKADDR"].ToString();
                                    upVo.fpVo.FHKPOST = drrBa["FHKPOST"].ToString();
                                    upVo.fpVo.FDWNAME = drrBa["FDWNAME"].ToString();
                                    upVo.fpVo.FDWADDR = drrBa["FDWADDR"].ToString();
                                    upVo.fpVo.FDWTELE = drrBa["FDWTELE"].ToString();
                                    upVo.fpVo.FDWPOST = drrBa["FDWPOST"].ToString();
                                    upVo.fpVo.FLXNAME = drrBa["FLXNAME"].ToString();
                                    upVo.fpVo.FRELATE = drrBa["FRELATE"].ToString();
                                    if (upVo.fpVo.FRELATE.Length > 10)
                                        upVo.fpVo.FRELATE = upVo.fpVo.FRELATE.Substring(0, 10);
                                    upVo.fpVo.FLXADDR = drrBa["FLXADDR"].ToString();
                                    upVo.fpVo.FLXTELE = drrBa["FLXTELE"].ToString();
                                    upVo.fpVo.FRYTJBH = drrBa["FRYTJBH"].ToString();
                                    if (upVo.fpVo.FRYTJBH == "")
                                        upVo.fpVo.FRYTJBH = "-";
                                    upVo.fpVo.FRYTJ = drrBa["FRYTJ"].ToString();
                                    if (upVo.fpVo.FRYTJ == "")
                                        upVo.fpVo.FRYTJ = "-";
                                    upVo.fpVo.FRYDATE = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                    upVo.fpVo.FRYTIME = drrBa["FRYTIME"].ToString();
                                    if (upVo.fpVo.FRYTIME.Trim().Length < 4)
                                        upVo.fpVo.FRYTIME = Function.Datetime(drrBa["FRYTIME"].ToString() + ":00:00").ToString("HH:mm:ss");
                                    upVo.fpVo.FRYTYKH = drrBa["FRYTYKH"].ToString();
                                    upVo.fpVo.FRYDEPT = drrBa["FRYDEPT"].ToString();
                                    upVo.fpVo.FRYBS = drrBa["FRYBS"].ToString().Trim();
                                    if (upVo.fpVo.FRYBS == "")
                                        upVo.fpVo.FRYBS = upVo.fpVo.FRYDEPT;
                                    upVo.fpVo.FZKTYKH = drrBa["FZKTYKH"].ToString();
                                    upVo.fpVo.FZKDEPT = drrBa["FZKDEPT"].ToString();
                                    upVo.fpVo.FZKTIME = drrBa["FZKTIME"].ToString();
                                    if (upVo.fpVo.FZKTIME.Length < 4)
                                        upVo.fpVo.FZKTIME = Function.Datetime(drrBa["FZKTIME"].ToString() + ":00:00").ToString("HH:MM:ss");
                                    upVo.fpVo.FCYDATE = Function.Datetime(drrBa["FCYDATE"]).ToString("yyyy-MM-dd");

                                    upVo.fpVo.FCYTIME = drrBa["FCYTIME"].ToString();
                                    if (upVo.fpVo.FCYTIME.Length < 4)
                                        upVo.fpVo.FCYTIME = Function.Datetime(drrBa["FCYTIME"].ToString() + ":00:00").ToString("HH:MM:ss");
                                    upVo.fpVo.FCYTYKH = drrBa["FCYTYKH"].ToString();
                                    upVo.fpVo.FCYDEPT = drrBa["FCYDEPT"].ToString();
                                    upVo.fpVo.FCYBS = drrBa["FCYBS"].ToString().Trim();
                                    if (upVo.fpVo.FCYBS == "")
                                        upVo.fpVo.FCYBS = upVo.fpVo.FCYDEPT;
                                    upVo.fpVo.FDAYS = drrBa["FDAYS"].ToString();
                                    upVo.fpVo.FMZZDBH = drrBa["FMZZDBH"].ToString();
                                    upVo.fpVo.FMZZD = drrBa["FMZZD"].ToString();
                                    upVo.fpVo.FMZDOCTBH = drrBa["FMZDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FMZDOCTBH))
                                        upVo.fpVo.FMZDOCTBH = "无";
                                    upVo.fpVo.FMZDOCT = drrBa["FMZDOCT"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FMZDOCT))
                                        upVo.fpVo.FMZDOCT = "无";
                                    upVo.fpVo.FJBFXBH = drrBa["FJBFXBH"].ToString();
                                    upVo.fpVo.FJBFX = drrBa["FJBFX"].ToString();
                                    upVo.fpVo.FYCLJBH = drrBa["FYCLJBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FYCLJBH))
                                        upVo.fpVo.FYCLJBH = "2";
                                    upVo.fpVo.FYCLJ = drrBa["FYCLJ"].ToString();
                                    if (!string.IsNullOrEmpty(upVo.fpVo.FYCLJBH))
                                        upVo.fpVo.FYCLJ = "是";
                                    else
                                        upVo.fpVo.FYCLJ = "否";
                                    upVo.fpVo.FQJTIMES = drrBa["FQJTIMES"].ToString();
                                    upVo.fpVo.FQJSUCTIMES = drrBa["FQJSUCTIMES"].ToString();
                                    if (!string.IsNullOrEmpty(upVo.fpVo.FQJTIMES) && string.IsNullOrEmpty(upVo.fpVo.FQJSUCTIMES))
                                    {
                                        upVo.fpVo.FQJSUCTIMES = upVo.fpVo.FQJTIMES;
                                    }
                                    upVo.fpVo.FPHZD = drrBa["FPHZD"].ToString();
                                    if (upVo.fpVo.FPHZD.Length > 100)
                                        upVo.fpVo.FPHZD = upVo.fpVo.FPHZD.Substring(0, 100);

                                    if (drrBa["FPHZDNUM"].ToString().Trim() != "")
                                        upVo.fpVo.FPHZDNUM = drrBa["FPHZDNUM"].ToString();
                                    else
                                        upVo.fpVo.FPHZDNUM = "-";

                                    if (drrBa["FPHZDBH"].ToString().Trim() != "")
                                        upVo.fpVo.FPHZDBH = drrBa["FPHZDBH"].ToString();
                                    else
                                        upVo.fpVo.FPHZDBH = "0";

                                    upVo.fpVo.FIFGMYWBH = drrBa["FIFGMYWBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FIFGMYWBH))
                                        upVo.fpVo.FIFGMYWBH = "1";
                                    if (drrBa["FIFGMYW"].ToString() != "")
                                        upVo.fpVo.FIFGMYW = drrBa["FIFGMYW"].ToString();
                                    else
                                        upVo.fpVo.FIFGMYW = "-";
                                    if (drrBa["FGMYW"].ToString() != "")
                                        upVo.fpVo.FGMYW = drrBa["FGMYW"].ToString();
                                    else
                                        upVo.fpVo.FGMYW = "-";
                                    if (drrBa["FBODYBH"].ToString().Trim() != "")
                                        upVo.fpVo.FBODYBH = drrBa["FBODYBH"].ToString();
                                    else
                                        upVo.fpVo.FBODYBH = "2";
                                    if (drrBa["FBODY"].ToString().Trim() != "")
                                        upVo.fpVo.FBODY = drrBa["FBODY"].ToString();
                                    else
                                        upVo.fpVo.FBODY = "否";
                                    upVo.fpVo.FBLOODBH = drrBa["FBLOODBH"].ToString();
                                    upVo.fpVo.FBLOOD = drrBa["FBLOOD"].ToString();
                                    upVo.fpVo.FRHBH = drrBa["FRHBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FRHBH))
                                        upVo.fpVo.FRHBH = "4";
                                    upVo.fpVo.FRH = drrBa["FRH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FRH))
                                        upVo.fpVo.FRH = "未查";
                                    upVo.fpVo.FKZRBH = drrBa["FKZRBH"].ToString();
                                    upVo.fpVo.FKZR = drrBa["FKZR"].ToString();
                                    upVo.fpVo.FZRDOCTBH = drrBa["FZRDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FZRDOCTBH))
                                        upVo.fpVo.FZRDOCTBH = "-";
                                    upVo.fpVo.FZRDOCTOR = drrBa["FZRDOCTOR"].ToString();
                                    upVo.fpVo.FZZDOCTBH = drrBa["FZZDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FZZDOCTBH))
                                        upVo.fpVo.FZZDOCTBH = "-";
                                    upVo.fpVo.FZZDOCT = drrBa["FZZDOCT"].ToString();
                                    upVo.fpVo.FZYDOCTBH = drrBa["FZYDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FZYDOCTBH))
                                        upVo.fpVo.FZYDOCTBH = "-";
                                    upVo.fpVo.FZYDOCT = drrBa["FZYDOCT"].ToString();
                                    upVo.fpVo.FNURSEBH = drrBa["FNURSEBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FNURSEBH))
                                        upVo.fpVo.FNURSEBH = "-";
                                    upVo.fpVo.FNURSE = drrBa["FNURSE"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FNURSE))
                                        upVo.fpVo.FNURSE = "-";
                                    upVo.fpVo.FJXDOCTBH = drrBa["FJXDOCTBH"].ToString();
                                    upVo.fpVo.FJXDOCT = drrBa["FJXDOCT"].ToString();
                                    upVo.fpVo.FSXDOCTBH = drrBa["FSXDOCTBH"].ToString();
                                    upVo.fpVo.FSXDOCT = drrBa["FSXDOCT"].ToString();
                                    upVo.fpVo.FBMYBH = drrBa["FBMYBH"].ToString();
                                    upVo.fpVo.FBMY = drrBa["FBMY"].ToString();
                                    upVo.fpVo.FQUALITYBH = drrBa["FQUALITYBH"].ToString();
                                    upVo.fpVo.FQUALITY = drrBa["FQUALITY"].ToString();
                                    upVo.fpVo.FZKDOCTBH = drrBa["FZKDOCTBH"].ToString();
                                    if (upVo.fpVo.FZKDOCTBH == "")
                                        upVo.fpVo.FZKDOCTBH = "-";
                                    upVo.fpVo.FZKDOCT = drrBa["FZKDOCT"].ToString();
                                    upVo.fpVo.FZKNURSEBH = drrBa["FZKNURSEBH"].ToString().Trim();
                                    if (upVo.fpVo.FZKNURSEBH == "")
                                        upVo.fpVo.FZKNURSEBH = "-";
                                    upVo.fpVo.FZKNURSE = drrBa["FZKNURSE"].ToString();
                                    if (upVo.fpVo.FZKNURSE == "")
                                        upVo.fpVo.FZKNURSE = "-";
                                    upVo.fpVo.FZKRQ = Function.Datetime(drrBa["FZKRQ"]).ToString("yyyyMMdd");

                                    upVo.fpVo.FLYFSBH = drrBa["FLYFSBH"].ToString().Trim();
                                    if (upVo.fpVo.FLYFSBH != "1" || upVo.fpVo.FLYFSBH != "2" ||
                                        upVo.fpVo.FLYFSBH != "3" || upVo.fpVo.FLYFSBH != "4" || upVo.fpVo.FLYFSBH != "5")
                                        upVo.fpVo.FLYFSBH = "9";

                                    upVo.fpVo.FLYFS = drrBa["FLYFS"].ToString();
                                    if (upVo.fpVo.FLYFS.Length >= 26)
                                        upVo.fpVo.FLYFS = upVo.fpVo.FLYFS.Substring(0, 50);

                                    upVo.fpVo.FYZOUTHOSTITAL = drrBa["FYZOUTHOSTITAL"].ToString();
                                    upVo.fpVo.FSQOUTHOSTITAL = drrBa["FSQOUTHOSTITAL"].ToString();
                                    upVo.fpVo.FISAGAINRYBH = drrBa["FISAGAINRYBH"].ToString();
                                    if (upVo.fpVo.FISAGAINRYBH == "")
                                        upVo.fpVo.FISAGAINRYBH = "-";
                                    upVo.fpVo.FISAGAINRY = drrBa["FISAGAINRY"].ToString();
                                    if (upVo.fpVo.FISAGAINRY == "")
                                        upVo.fpVo.FISAGAINRY = "-";
                                    upVo.fpVo.FISAGAINRYMD = drrBa["FISAGAINRYMD"].ToString();
                                    if (upVo.fpVo.FISAGAINRYMD == "")
                                        upVo.fpVo.FISAGAINRYMD = "-";
                                    upVo.fpVo.FRYQHMDAYS = drrBa["FRYQHMDAYS"].ToString();
                                    upVo.fpVo.FRYQHMHOURS = drrBa["FRYQHMHOURS"].ToString();
                                    upVo.fpVo.FRYQHMMINS = drrBa["FRYQHMMINS"].ToString();
                                    upVo.fpVo.FRYQHMCOUNTS = drrBa["FRYQHMCOUNTS"].ToString();
                                    upVo.fpVo.FRYHMDAYS = drrBa["FRYHMDAYS"].ToString();
                                    upVo.fpVo.FRYHMHOURS = drrBa["FRYHMHOURS"].ToString();
                                    upVo.fpVo.FRYHMMINS = drrBa["FRYHMMINS"].ToString();
                                    upVo.fpVo.FRYHMCOUNTS = drrBa["FRYHMCOUNTS"].ToString();
                                    upVo.fpVo.FSUM1 = Function.Dec(drrBa["FSUM1"].ToString());
                                    upVo.fpVo.FZFJE = Function.Dec(drrBa["FZFJE"].ToString());
                                    upVo.fpVo.FZHFWLYLF = Function.Dec(drrBa["FZHFWLYLF"].ToString());
                                    upVo.fpVo.FZHFWLCZF = Function.Dec(drrBa["FZHFWLCZF"].ToString());
                                    upVo.fpVo.FZHFWLHLF = Function.Dec(drrBa["FZHFWLHLF"].ToString());
                                    upVo.fpVo.FZHFWLQTF = Function.Dec(drrBa["FZHFWLQTF"].ToString());
                                    upVo.fpVo.FZDLBLF = Function.Dec(drrBa["FZDLBLF"].ToString());
                                    upVo.fpVo.FZDLSSSF = Function.Dec(drrBa["FZDLSSSF"].ToString());
                                    upVo.fpVo.FZDLYXF = Function.Dec(drrBa["FZDLYXF"].ToString());
                                    upVo.fpVo.FZDLLCF = Function.Dec(drrBa["FZDLLCF"].ToString());
                                    upVo.fpVo.FZLLFFSSF = Function.Dec(drrBa["FZLLFFSSF"].ToString());
                                    upVo.fpVo.FZLLFWLZWLF = Function.Dec(drrBa["FZLLFWLZWLF"].ToString());
                                    upVo.fpVo.FZLLFSSF = Function.Dec(drrBa["FZLLFSSF"].ToString());
                                    upVo.fpVo.FZLLFMZF = Function.Dec(drrBa["FZLLFMZF"].ToString());
                                    upVo.fpVo.FZLLFSSZLF = Function.Dec(drrBa["FZLLFSSZLF"].ToString());
                                    upVo.fpVo.FKFLKFF = Function.Dec(drrBa["FKFLKFF"].ToString());
                                    upVo.fpVo.FZYLZF = Function.Dec(drrBa["FZYLZF"].ToString());
                                    upVo.fpVo.FXYF = Function.Dec(drrBa["FXYF"].ToString());
                                    upVo.fpVo.FXYLGJF = Function.Dec(drrBa["FXYLGJF"].ToString());
                                    upVo.fpVo.FZCHYF = Function.Dec(drrBa["FZCHYF"].ToString());
                                    upVo.fpVo.FZCYF = Function.Dec(drrBa["FZCYF"].ToString());
                                    upVo.fpVo.FXYLXF = Function.Dec(drrBa["FXYLXF"].ToString());
                                    upVo.fpVo.FXYLBQBF = Function.Dec(drrBa["FXYLBQBF"].ToString());
                                    upVo.fpVo.FXYLQDBF = Function.Dec(drrBa["FXYLQDBF"].ToString());
                                    upVo.fpVo.FXYLYXYZF = Function.Dec(drrBa["FXYLYXYZF"].ToString());
                                    upVo.fpVo.FXYLXBYZF = Function.Dec(drrBa["FXYLXBYZF"].ToString());
                                    upVo.fpVo.FHCLCJF = Function.Dec(drrBa["FHCLCJF"].ToString());
                                    upVo.fpVo.FHCLZLF = Function.Dec(drrBa["FHCLZLF"].ToString());
                                    upVo.fpVo.FHCLSSF = Function.Dec(drrBa["FHCLSSF"].ToString());
                                    upVo.fpVo.FQTF = Function.Dec(drrBa["FQTF"]);
                                    upVo.fpVo.FBGLX = drrBa["FBGLX"].ToString();

                                    if (drrBa["fidcard"].ToString() != "")
                                        upVo.fpVo.GMSFHM = drrBa["fidcard"].ToString();
                                    else
                                        upVo.fpVo.GMSFHM = dr2["idcard_chr"].ToString();

                                    upVo.fpVo.FZYF = Function.Dec(drrBa["FZYF"].ToString());
                                    if (drrBa["FZKDATE"] != DBNull.Value)
                                        upVo.fpVo.FZKDATE = Function.Datetime(drrBa["FZKDATE"]).ToString("yyyy-MM-dd");
                                    else
                                        upVo.fpVo.FZKDATE = "";

                                    upVo.fpVo.FZKTIME = Function.Datetime(upVo.fpVo.FZKDATE + " " + upVo.fpVo.FZKTIME).ToString("yyyyMMddHHmmss");
                                    upVo.fpVo.FJOBBH = drrBa["FJOBBH"].ToString();
                                    upVo.fpVo.FZHFWLYLF01 = Function.Dec(drrBa["FZHFWLYLF01"]);
                                    upVo.fpVo.FZHFWLYLF02 = Function.Dec(drrBa["FZHFWLYLF02"]);
                                    upVo.fpVo.FZYLZDF = Function.Dec(drrBa["FZYLZDF"]);
                                    upVo.fpVo.FZYLZLF = Function.Dec(drrBa["FZYLZLF"]);
                                    upVo.fpVo.FZYLZLF01 = Function.Dec(drrBa["FZYLZLF01"]);
                                    upVo.fpVo.FZYLZLF02 = Function.Dec(drrBa["FZYLZLF02"]);
                                    upVo.fpVo.FZYLZLF03 = Function.Dec(drrBa["FZYLZLF03"]);
                                    upVo.fpVo.FZYLZLF04 = Function.Dec(drrBa["FZYLZLF04"]);
                                    upVo.fpVo.FZYLZLF05 = Function.Dec(drrBa["FZYLZLF05"]);
                                    upVo.fpVo.FZYLZLF06 = Function.Dec(drrBa["FZYLZLF06"]);
                                    upVo.fpVo.FZYLQTF = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZCLJGZJF = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZYLQTF01 = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZYLQTF02 = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZYID = drrBa["FZYID"].ToString();

                                    upVo.fpVo.ZYH = ipno;
                                    upVo.fpVo.FPHM = FPHM;

                                    #endregion

                                    #region 出院小结
                                    DataTable dtXj = GetPatCyxjList2(ipno, emrinpatientdate);

                                    if (dtXj != null && dtXj.Rows.Count > 0)
                                    {
                                        #region 上传信息 出院小结
                                        DataRow drXj = dtXj.Rows[0];

                                        upVo.xjVo = new EntityCyxj();
                                        upVo.xjVo.JZJLH = jzjlh;
                                        upVo.xjVo.MZH = MZH;
                                        upVo.xjVo.ZYH = ipno;
                                        upVo.xjVo.MZZD = drrBa["FMZZD"].ToString();
                                        if (upVo.xjVo.MZZD.Length > 100)
                                            upVo.xjVo.MZZD = upVo.xjVo.MZZD.Substring(0, 100);
                                        if (string.IsNullOrEmpty(upVo.xjVo.MZZD))
                                            upVo.xjVo.MZZD = "-";
                                        upVo.xjVo.RYZD = drXj["inhospitaldiagnose"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.RYZD))
                                            upVo.xjVo.RYZD = drrBa["FMZZD"].ToString();
                                        upVo.xjVo.CYZD = drXj["outhospitaldiagnose"].ToString().Trim();
                                        if (drXj["outhospitaldiagnose"] == DBNull.Value)
                                            upVo.xjVo.CYZD = "-";
                                        upVo.xjVo.XM = drrBa["FNAME"].ToString(); ;
                                        upVo.xjVo.XB = drrBa["FSEX"].ToString();
                                        if (upVo.xjVo.XB == "男")
                                            upVo.xjVo.XB = "1";
                                        else if (upVo.xjVo.XB == "女")
                                            upVo.xjVo.XB = "2";
                                        else upVo.xjVo.XB = "9";
                                        upVo.xjVo.NL = drrBa["FAGE"].ToString();

                                        if (drrBa["fidcard"].ToString() != "")
                                            upVo.xjVo.GMSFHM = drrBa["fidcard"].ToString();
                                        else
                                            upVo.xjVo.GMSFHM = dr2["idcard_chr"].ToString();

                                        upVo.xjVo.RYRQ = dr2["RYRQ1"].ToString();
                                        upVo.xjVo.CYRQ = dr2["CYRQ1"].ToString();
                                        upVo.xjVo.RYSJ = dr2["RYSJ"].ToString();
                                        upVo.xjVo.CYSJ = dr2["CYSJ"].ToString();
                                        upVo.xjVo.ZYTS = drrBa["FDAYS"].ToString();
                                        upVo.xjVo.ZY = drrBa["fjob"].ToString();
                                        upVo.xjVo.JG = drrBa["FNATIVE"].ToString();
                                        if (string.IsNullOrEmpty(upVo.xjVo.JG))
                                            upVo.xjVo.JG = "无";
                                        upVo.xjVo.YJDZ = dr2["YJDZ"].ToString();
                                        if (string.IsNullOrEmpty(upVo.xjVo.YJDZ))
                                            upVo.xjVo.YJDZ = "-";
                                        upVo.xjVo.CYYZ = drXj["outhospitaladvice_right"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.CYYZ))
                                            upVo.xjVo.CYYZ = "-";
                                        upVo.xjVo.RYQK = drXj["inhospitaldiagnose_right"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.RYQK))
                                            upVo.xjVo.RYQK = "-";
                                        upVo.xjVo.YSQM = drXj["doctorname"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.YSQM))
                                            upVo.xjVo.YSQM = "-";
                                        upVo.xjVo.RYHCLGC = "-";
                                        upVo.xjVo.CYSQK = drXj["outhospitalcase_right"].ToString().Trim();
                                        if (string.IsNullOrEmpty(upVo.xjVo.CYSQK))
                                            upVo.xjVo.CYSQK = "-";
                                        upVo.xjVo.ZLJG = drXj["inhospitalby"].ToString().Trim();
                                        if (upVo.xjVo.ZLJG.Length > 1000)
                                            upVo.xjVo.ZLJG = upVo.xjVo.ZLJG.Substring(0, 1000);
                                        if (string.IsNullOrEmpty(upVo.xjVo.ZLJG))
                                            upVo.xjVo.ZLJG = "-";

                                        if (ftimes > 0)
                                            upVo.xjVo.FTIMES = ftimes.ToString();
                                        else
                                            upVo.xjVo.FTIMES = rycs.ToString();

                                        upVo.xjVo.FSUM1 = Function.Dec(drrBa["FSUM1"].ToString());
                                        upVo.xjVo.FPHM = FPHM;
                                        #endregion
                                    }
                                    #endregion

                                    #region  显示列表
                                    upVo.XH = ++n;
                                    upVo.UPLOADTYPE = 1;
                                    upVo.PATNAME = upVo.fpVo.FNAME;
                                    upVo.PATSEX = upVo.fpVo.FSEX;
                                    upVo.IDCARD = upVo.fpVo.FIDCard;
                                    upVo.INPATIENTID = upVo.fpVo.FZYID;
                                    upVo.INDEPTCODE = dr2["rydeptid"].ToString();
                                    upVo.INPATIENTDATE = Function.Datetime(Function.Datetime(dr2["rysj"]).ToString("yyyy-MM-dd"));
                                    upVo.OUTHOSPITALDATE = Function.Datetime(Function.Datetime(dr2["cysj"]).ToString("yyyy-MM-dd"));
                                    upVo.RYSJ = Function.Datetime(dr2["rysj"]).ToString("yyyy-MM-dd HH:mm");
                                    upVo.CYSJ = Function.Datetime(dr2["cysj"]).ToString("yyyy-MM-dd HH:mm");
                                    upVo.FPRN = upVo.fpVo.FPRN;
                                    upVo.FTIMES = dr2["rycs"].ToString();
                                    upVo.BIRTH = Function.Datetime(dr2["birth"]).ToString("yyyy-mm-dd");
                                    upVo.InDeptName = dr2["ryks"].ToString();
                                    upVo.OutDeptName = dr2["cyks"].ToString();
                                    upVo.OUTDEPTCODE = dr2["cydeptid"].ToString();
                                    upVo.JZJLH = jzjlh;
                                    upVo.REGISTERID = dr2["registerid_chr"].ToString();
                                    upVo.STATUS = Function.Int(dr2["status"]);
                                    upVo.SERNO = Function.Dec(dr2["serno"]);
                                    if (dr2["status"].ToString() == "1")
                                        upVo.SZ = "已上传";
                                    else
                                        upVo.SZ = "未上传";

                                    if (dr2["jbr"] != DBNull.Value)
                                        upVo.JBRXM = dr2["jbr"].ToString();
                                    if (dr2["uploaddate"] != DBNull.Value)
                                        upVo.UPLOADDATE = dr2["uploaddate"].ToString();

                                    #endregion

                                    data.Add(upVo);
                                }
                            }
                        }
                    }
                }
                #endregion
                #endregion
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetPatFirstList--" + e);
            }
            finally
            {
                svc = null;
            }

            return data;
        }
        #endregion

        #region 出院小结
        /// <summary>
        /// 出院小结
        /// </summary>
        /// <param name="dicParm"></param>
        /// <returns></returns>
        public DataTable GetPatCyxjList2(string ipno, string emrinpatientdate)
        {
            SqlHelper svc = null;
            IDataParameter[] parm = null;
            string opendate = string.Empty;
            DataTable dtResult = null;

            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);

                #region 出院小结

                string Slq1 = @"select createdate,opendate 
                          from outhospitalrecord 
                          where inpatientid = ?
                           and inpatientdate= to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                           and status=0 ";

                string Slq2 = @"select a.inpatientid,
                               a.inpatientdate,
                               a.opendate,
                               a.createdate,
                               a.createuserid,
                               a.ifconfirm,
                               a.confirmreason,
                               a.confirmreasonxml,
                               a.firstprintdate,
                               a.deactiveddate,
                               a.deactivedoperatorid,
                               a.status,
                               a.heartid,
                               a.heartidxml,
                               a.xrayid,
                               a.xrayidxml,
                               a.inhospitalcase,
                               a.inhospitalcasexml,
                               a.inhospitaldiagnose,
                               a.inhospitaldiagnosexml,
                               a.outhospitaldiagnose,
                               a.outhospitaldiagnosexml,
                               a.inhospitalby,
                               a.inhospitalbyxml,
                               a.outhospitalcase,
                               a.outhospitalcasexml,
                               a.outhospitaladvice,
                               a.outhospitaladvicexml,
                               b.modifydate,
                               b.modifyuserid,
                               b.outhospitaldate,
                               b.heartid_right,
                               b.xrayid_right,
                               b.inhospitaldiagnose_right,
                               b.outhospitaldiagnose_right,
                               b.inhospitalcase_right,
                               b.inhospitalby_right,
                               b.outhospitalcase_right,
                               b.outhospitaladvice_right,
                               b.maindoctorid,
                               b.doctorid,
                               b.maindoctorname,
                               b.doctorname
                          from outhospitalrecord a, outhospitalrecordcontent b
                         where a.inpatientid = ?
                           and a.inpatientdate = to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                           and a.opendate = to_date(?,'yyyy-mm-dd hh24:mi:ss')
                           and a.status = 0
                           and b.inpatientid = a.inpatientid
                           and b.inpatientdate = a.inpatientdate
                           and b.opendate = a.opendate
                           and b.modifydate = (select max(modifydate)
                                                 from outhospitalrecordcontent
                                                where inpatientid = a.inpatientid
                                                  and inpatientdate = a.inpatientdate
                                                  and opendate = a.opendate) ";
                #endregion

                if (!string.IsNullOrEmpty(ipno) && !string.IsNullOrEmpty(emrinpatientdate))
                {
                    parm = svc.CreateParm(2);
                    parm[0].Value = ipno;
                    parm[1].Value = emrinpatientdate;

                    DataTable dt1 = svc.GetDataTable(Slq1, parm);

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        DataRow dr = dt1.Rows[0];
                        opendate = Function.Datetime(dr["opendate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (!string.IsNullOrEmpty(opendate))
                    {
                        parm = svc.CreateParm(3);
                        parm[0].Value = ipno;
                        parm[1].Value = emrinpatientdate;
                        parm[2].Value = opendate;

                        dtResult = svc.GetDataTable(Slq2, parm);
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetPatCyxjList--" + e);
            }
            finally
            {
                svc = null;
            }
            return dtResult;
        }
        #endregion

        #region 获取患者首页其他信息
        /// <summary>
        /// 获取患者首页其他信息
        /// </summary>
        /// <param name="lstUpVo"></param>
        /// <returns></returns>
        public List<EntityPatUpload> GetPatFirstInfo(List<EntityPatUpload> lstUpVo)
        {
            string SqlZd = string.Empty;
            string SqlZk = string.Empty;
            string SqlFop = string.Empty;
            string SqlFy = string.Empty;
            string SqlZl = string.Empty;
            string SqlHl = string.Empty;
            string SqlZdfj = string.Empty;
            string SqlCh = string.Empty;

            DataTable DtZd = null;
            DataTable DtZk = null;
            DataTable DtFop = null;
            DataTable DtFy = null;
            DataTable DtZl = null;
            DataTable DtHl = null;
            DataTable DtZdfj = null;
            DataTable DtCh = null;

            SqlHelper svcBa = new SqlHelper(EnumBiz.baDB);

            try
            {
                for (int i = 0; i < lstUpVo.Count; i++)
                {
                    #region 诊断信息
                    SqlZd = @"select b.fid, b.FPRN,b.FTIMES,b.FZDLX,b.FICDVersion,b.FICDM,b.FJBNAME,b.FRYBQBH,b.FRYBQ 
                           from  tDiagnose  b where b.fprn = ? and b.ftimes = ? ";
                    #endregion

                    #region 转科信息
                    SqlZk = @"select * from tSwitchKs a where a.fprn = ? and a.ftimes = ? ";
                    #endregion

                    #region 手术信息
                    SqlFop = @"select FPRN,FTIMES,FNAME,FOPTIMES,FOPCODE,FOP,FOPDATE,FQIEKOUBH,FQIEKOU,FYUHEBH,
                            FYUHE,FDOCBH,FDOCNAME,FMAZUIBH,FMAZUI,FIFFSOP,FOPDOCT1BH,FOPDOCT1,FOPDOCT2BH,
                            FOPDOCT2,FMZDOCTBH,FMZDOCT,FZQSSBH,FZQSS,FSSJBBH,FSSJB,FOPKSNAME,FOPTYKH
                            from tOperation a where a.fprn = ? and a.ftimes = ? ";
                    #endregion

                    #region 妇婴卡信息
                    SqlFy = @"select distinct c.ftimes, c.FPRN,c.FTIMES,c.FBABYNUM,c.FNAME,c.FBABYSEXBH,c.FBABYSEX,c.FTZ,c.FRESULTBH,c.FRESULT,
                            c.FZGBH,c.FZG,c.FBABYSUC,c.FHXBH,c.FHX from  tBabyCard c where c.fprn = ? and c.ftimes = ?";
                    #endregion

                    #region 肿瘤卡信息
                    SqlZl = @"select FPRN,FTIMES,FFLFSBH,FFLFS,FFLCXBH,FFLCX,FFLZZBH,FFLZZ,FYJY,FYCS,FYTS,FYRQ1,
                            FYRQ2,FQJY,FQCS,FQTS,FQRQ1,FQRQ2,FZNAME,FZJY,FZCS,FZTS,FZRQ1,FZRQ2,FHLFSBH,
                            FHLFS,FHLFFBH,FHLFF
                            from tKnubCard d where d.fprn = ? and d.ftimes = ?";
                    #endregion

                    #region 化疗记录
                    SqlHl = @"select FPRN,FTIMES,FHLRQ1,FHLDRUG ,FHLPROC,FHLLXBH,FHLLX  from tKnubHl e where e.fprn = ? and e.ftimes = ?";
                    #endregion

                    #region 病人诊断码附加编码
                    SqlZdfj = @"select FPRN,FTIMES,FZDLX,FICDM,FFJICDM,FFJJBNAME,FFRYBQBH,FFRYBQ,FPX 
                                from TDiagnoseAdd f where f.fprn = ? and f.ftimes = ?";
                    #endregion

                    #region 中医院病人附加信息
                    SqlCh = @"select FPRN,FTIMES,FZLLBBH,FZLLB,FZZZYBH,FZZZY,FRYCYBH,FRYCY,FMZZYZDBH,
                            FMZZYZD,FSSLCLJBH,FSSLCLJ,FSYJGZJBH,FSYJGZJ,FSYZYSBBH,FSYZYSB,
                            FSYZYJSBH,FSYZYJS,FBZSHBH,FBZSH
                             from tChAdd g where g.fprn = ? and g.ftimes = ?";
                    #endregion

                    #region 条件
                    IDataParameter[] parmZd = null;
                    parmZd = svcBa.CreateParm(2);
                    parmZd[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmZd[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmZk = null;
                    parmZk = svcBa.CreateParm(2);
                    parmZk[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmZk[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmFop = null;
                    parmFop = svcBa.CreateParm(2);
                    parmFop[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmFop[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmFy = null;
                    parmFy = svcBa.CreateParm(2);
                    parmFy[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmFy[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmZl = null;
                    parmZl = svcBa.CreateParm(2);
                    parmZl[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmZl[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmHl = null;
                    parmHl = svcBa.CreateParm(2);
                    parmHl[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmHl[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmZdfj = null;
                    parmZdfj = svcBa.CreateParm(2);
                    parmZdfj[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmZdfj[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmCh = null;
                    parmCh = svcBa.CreateParm(2);
                    parmCh[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmCh[1].Value = lstUpVo[i].fpVo.FTIMES;

                    DtZd = svcBa.GetDataTable(SqlZd, parmZd);
                    DtZk = svcBa.GetDataTable(SqlZk, parmZk);
                    DtFop = svcBa.GetDataTable(SqlFop, parmFop);
                    DtFy = svcBa.GetDataTable(SqlFy, parmFy);
                    DtZl = svcBa.GetDataTable(SqlZl, parmZl);
                    DtHl = svcBa.GetDataTable(SqlHl, parmHl);
                    DtZdfj = svcBa.GetDataTable(SqlZdfj, parmZdfj);
                    DtCh = svcBa.GetDataTable(SqlCh, parmCh);

                    #endregion

                    #region 赋值
                    #region//转科信息
                    if (DtZk != null && DtZk.Rows.Count > 0)
                    {
                        EntityBrzkqk zkVo = null;
                        lstUpVo[i].fpVo.lstZkVo = new List<EntityBrzkqk>();

                        foreach (DataRow dr in DtZk.Rows)
                        {
                            zkVo = new EntityBrzkqk();

                            zkVo.FZKTYKH = dr["FZKTYKH"].ToString();
                            zkVo.FZKDEPT = dr["FZKDEPT"].ToString();
                            zkVo.FZKDATE = Function.Datetime(dr["FZKDATE"]).ToString("yyyy-MM-dd");
                            zkVo.FZKTIME = Function.Datetime(dr["FZKTIME"].ToString()).ToString("HH:mm:ss");
                            zkVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstZkVo.Add(zkVo);
                        }
                    }
                    #endregion

                    #region //诊断信息
                    if (DtZd != null && DtZd.Rows.Count > 0)
                    {
                        EntityBrzdxx zdVo = null;
                        lstUpVo[i].fpVo.lstZdVo = new List<EntityBrzdxx>();

                        foreach (DataRow dr in DtZd.Rows)
                        {
                            zdVo = new EntityBrzdxx();

                            zdVo.FZDLX = dr["FZDLX"].ToString();
                            zdVo.FICDVersion = dr["FICDVersion"].ToString();
                            zdVo.FICDM = dr["FICDM"].ToString();
                            if (dr["FJBNAME"].ToString().Length > 10)
                                zdVo.FJBNAME = dr["FJBNAME"].ToString().Substring(0, 10);
                            else
                                zdVo.FJBNAME = dr["FJBNAME"].ToString();
                            zdVo.FRYBQBH = dr["FRYBQBH"].ToString();
                            if (zdVo.FRYBQBH == "")
                                zdVo.FRYBQBH = "无";
                            zdVo.FRYBQ = dr["FRYBQ"].ToString();
                            if (zdVo.FRYBQ == "")
                                zdVo.FRYBQ = "无";
                            zdVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstZdVo.Add(zdVo);
                        }
                    }
                    #endregion

                    #region//手术信息
                    if (DtFop != null && DtFop.Rows.Count > 0)
                    {
                        EntityBrssxx fopVo = null;
                        lstUpVo[i].fpVo.lstSsVo = new List<EntityBrssxx>();

                        foreach (DataRow dr in DtFop.Rows)
                        {
                            fopVo = new EntityBrssxx();
                            fopVo.FNAME = dr["FNAME"].ToString();
                            if (fopVo.FNAME == "")
                                continue;
                            fopVo.FOPTIMES = dr["FOPTIMES"].ToString();
                            if (fopVo.FOPTIMES == "0")
                                fopVo.FOPTIMES = "1";
                            fopVo.FOPCODE = dr["FOPCODE"].ToString();
                            fopVo.FOP = dr["FOP"].ToString();
                            fopVo.FOPDATE = Function.Datetime(dr["FOPDATE"]).ToString("yyyyMMdd");
                            fopVo.FQIEKOUBH = dr["FQIEKOUBH"].ToString() == "" ? "无" : dr["FQIEKOUBH"].ToString();
                            fopVo.FQIEKOU = dr["FQIEKOU"].ToString() == "" ? "无" : dr["FQIEKOU"].ToString();
                            fopVo.FYUHEBH = dr["FYUHEBH"].ToString() == "" ? "无" : dr["FYUHEBH"].ToString();
                            if (fopVo.FYUHEBH == "")
                                fopVo.FYUHEBH = "-";
                            fopVo.FYUHE = dr["FYUHE"].ToString();
                            if (fopVo.FYUHE == "")
                                fopVo.FYUHE = "-";
                            fopVo.FDOCBH = dr["FDOCBH"].ToString();
                            if (fopVo.FDOCBH == "")
                                fopVo.FDOCBH = "-";
                            fopVo.FDOCNAME = dr["FDOCNAME"].ToString() == "" ? "无" : dr["FDOCNAME"].ToString();
                            fopVo.FMAZUIBH = dr["FMAZUIBH"].ToString();
                            if (fopVo.FMAZUIBH == "")
                                fopVo.FMAZUIBH = "无";
                            if (fopVo.FMZDOCTBH == "")
                                fopVo.FMZDOCTBH = "无";
                            fopVo.FMAZUI = dr["FMAZUI"].ToString() == "" ? "无" : dr["FMAZUI"].ToString();
                            fopVo.FIFFSOP = dr["FIFFSOP"].ToString();
                            if (fopVo.FIFFSOP == "False")
                                fopVo.FIFFSOP = "0";
                            else if (fopVo.FIFFSOP == "True")
                                fopVo.FIFFSOP = "1";
                            fopVo.FOPDOCT1BH = dr["FOPDOCT1BH"].ToString();
                            if (fopVo.FOPDOCT1BH == "")
                                fopVo.FOPDOCT1BH = "无";
                            fopVo.FOPDOCT1 = dr["FOPDOCT1"].ToString();
                            if (fopVo.FOPDOCT1 == "")
                                fopVo.FOPDOCT1 = "-";
                            fopVo.FOPDOCT2BH = dr["FOPDOCT2BH"].ToString();
                            if (fopVo.FOPDOCT2BH == "")
                                fopVo.FOPDOCT2BH = "无";
                            fopVo.FOPDOCT2 = dr["FOPDOCT2"].ToString();
                            if (fopVo.FOPDOCT2 == "")
                                fopVo.FOPDOCT2 = "无";
                            fopVo.FMZDOCTBH = dr["FMZDOCTBH"].ToString();
                            if (fopVo.FMZDOCTBH == "")
                                fopVo.FMZDOCTBH = "无";
                            fopVo.FMZDOCT = dr["FMZDOCT"].ToString();
                            if (fopVo.FMZDOCT == "")
                                fopVo.FMZDOCT = "无";
                            fopVo.FZQSSBH = dr["FZQSSBH"].ToString();
                            if (fopVo.FZQSSBH == "")
                                fopVo.FZQSSBH = "无";
                            fopVo.FZQSS = dr["FZQSS"].ToString();
                            fopVo.FSSJBBH = dr["FSSJBBH"].ToString();
                            if (fopVo.FSSJBBH == "")
                                fopVo.FSSJBBH = "无";
                            fopVo.FSSJB = dr["FSSJB"].ToString();
                            fopVo.FOPKSNAME = dr["FOPKSNAME"].ToString();
                            if (fopVo.FOPKSNAME == "")
                                fopVo.FOPKSNAME = "无";
                            fopVo.FOPTYKH = dr["FOPTYKH"].ToString();
                            if (fopVo.FOPTYKH == "")
                                fopVo.FOPTYKH = "无";

                            fopVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstSsVo.Add(fopVo);
                        }
                    }
                    #endregion

                    #region //妇婴卡信息
                    if (DtFy != null && DtFy.Rows.Count > 0)
                    {
                        EntityFyksj fyVo = null;
                        lstUpVo[i].fpVo.lstFyVo = new List<EntityFyksj>();

                        foreach (DataRow dr in DtFy.Rows)
                        {
                            fyVo = new EntityFyksj();

                            fyVo.FBABYNUM = dr["FBABYNUM"].ToString() == "" ? "-" : dr["FBABYNUM"].ToString();
                            fyVo.FNAME = dr["FNAME"].ToString() == "" ? "-" : dr["FNAME"].ToString();
                            fyVo.FBABYSEXBH = dr["FBABYSEXBH"].ToString() == "" ? "-" : dr["FBABYSEXBH"].ToString();
                            fyVo.FBABYSEX = dr["FBABYSEX"].ToString() == "" ? "-" : dr["FBABYSEX"].ToString();
                            fyVo.FTZ = dr["FTZ"].ToString() == "" ? "-" : dr["FTZ"].ToString();
                            fyVo.FRESULTBH = dr["FRESULTBH"].ToString() == "" ? "-" : dr["FRESULTBH"].ToString();
                            fyVo.FRESULT = dr["FRESULT"].ToString() == "" ? "-" : dr["FRESULT"].ToString();
                            fyVo.FZGBH = dr["FZGBH"].ToString() == "" ? "-" : dr["FZGBH"].ToString();
                            fyVo.FZG = dr["FZG"].ToString() == "" ? "-" : dr["FZG"].ToString();
                            fyVo.FBABYSUC = dr["FBABYSUC"].ToString() == "" ? "0" : dr["FBABYSUC"].ToString();
                            fyVo.FHXBH = dr["FHXBH"].ToString() == "" ? "-" : dr["FHXBH"].ToString();
                            fyVo.FHX = dr["FHX"].ToString() == "" ? "-" : dr["FHX"].ToString();
                            fyVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstFyVo.Add(fyVo);
                        }
                    }
                    #endregion

                    #region //肿瘤卡
                    if (DtZl != null && DtZl.Rows.Count > 0)
                    {
                        EntityZlksj zlVo = null;
                        lstUpVo[i].fpVo.lstZlVo = new List<EntityZlksj>();

                        foreach (DataRow dr in DtZl.Rows)
                        {
                            zlVo = new EntityZlksj();

                            zlVo.FFLFSBH = dr["FFLFSBH"].ToString();
                            zlVo.FFLFS = dr["FFLFS"].ToString();
                            zlVo.FFLCXBH = dr["FFLCXBH"].ToString();
                            zlVo.FFLCX = dr["FFLCX"].ToString();
                            zlVo.FFLZZBH = dr["FFLZZBH"].ToString();
                            zlVo.FFLZZ = dr["FFLZZ"].ToString();
                            zlVo.FYJY = dr["FYJY"].ToString();
                            zlVo.FYCS = dr["FYCS"].ToString();
                            zlVo.FYTS = dr["FYTS"].ToString();
                            zlVo.FYRQ1 = Function.Datetime(dr["FYRQ1"]).ToString("yyyyMMdd");
                            zlVo.FYRQ2 = Function.Datetime(dr["FYRQ2"]).ToString("yyyyMMdd");
                            zlVo.FQJY = dr["FQJY"].ToString();
                            zlVo.FQCS = dr["FQCS"].ToString();
                            zlVo.FQTS = dr["FQTS"].ToString();
                            zlVo.FQRQ1 = Function.Datetime(dr["FQRQ1"]).ToString("yyyyMMdd");
                            zlVo.FQRQ2 = Function.Datetime(dr["FQRQ2"]).ToString("yyyyMMdd");
                            zlVo.FZNAME = dr["FZNAME"].ToString();
                            zlVo.FZJY = dr["FZJY"].ToString();
                            zlVo.FZCS = dr["FZCS"].ToString();
                            zlVo.FZTS = dr["FZTS"].ToString();
                            zlVo.FZRQ1 = Function.Datetime(dr["FZRQ1"]).ToString("yyyyMMdd");
                            zlVo.FZRQ2 = Function.Datetime(dr["FZRQ2"]).ToString("yyyyMMdd");
                            zlVo.FHLFSBH = dr["FHLFSBH"].ToString();
                            zlVo.FHLFS = dr["FHLFS"].ToString();
                            zlVo.FHLFFBH = dr["FHLFFBH"].ToString();
                            zlVo.FHLFF = dr["FHLFF"].ToString();
                            zlVo.FPRN = dr["FPRN"].ToString();

                            if (string.IsNullOrEmpty(zlVo.FFLFSBH) || string.IsNullOrEmpty(zlVo.FHLFSBH))
                                continue;

                            lstUpVo[i].fpVo.lstZlVo.Add(zlVo);
                        }
                    }
                    #endregion

                    #region//肿瘤化疗记录
                    if (DtHl != null && DtHl.Rows.Count > 0)
                    {
                        EntityZlhljlsj hlVo = null;
                        lstUpVo[i].fpVo.lstHlVo = new List<EntityZlhljlsj>();

                        foreach (DataRow dr in DtHl.Rows)
                        {
                            hlVo = new EntityZlhljlsj();

                            hlVo.FHLRQ1 = Function.Datetime(dr["FHLRQ1"]).ToString("yyyyMMdd");
                            hlVo.FHLDRUG = dr["FHLDRUG"].ToString();
                            hlVo.FHLPROC = dr["FHLPROC"].ToString();
                            hlVo.FHLLXBH = dr["FHLLXBH"].ToString();
                            hlVo.FHLLX = dr["FHLLX"].ToString();
                            hlVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstHlVo.Add(hlVo);
                        }
                    }
                    #endregion

                    #region//肿瘤化疗记录
                    if (DtZdfj != null && DtZdfj.Rows.Count > 0)
                    {
                        EntityBrzdfjm zdfjVo = null;
                        lstUpVo[i].fpVo.lstZdfjVo = new List<EntityBrzdfjm>();

                        foreach (DataRow dr in DtZdfj.Rows)
                        {
                            zdfjVo = new EntityBrzdfjm();

                            zdfjVo.FZDLX = dr["FZDLX"].ToString();
                            zdfjVo.FICDM = dr["FICDM"].ToString();
                            zdfjVo.FFJICDM = dr["FFJICDM"].ToString();
                            zdfjVo.FFJJBNAME = dr["FFJJBNAME"].ToString();
                            zdfjVo.FFRYBQBH = dr["FFRYBQBH"].ToString();
                            zdfjVo.FFRYBQ = dr["FFRYBQ"].ToString();
                            zdfjVo.FPX = dr["FPX"].ToString();
                            lstUpVo[i].fpVo.lstZdfjVo.Add(zdfjVo);
                        }
                    }
                    #endregion

                    #region //中医院病人附加信息
                    if (DtCh != null && DtCh.Rows.Count > 0)
                    {
                        EntityZyybrfjxx zyVo = null;
                        lstUpVo[i].fpVo.lstZyVo = new List<EntityZyybrfjxx>();

                        foreach (DataRow dr in DtCh.Rows)
                        {
                            zyVo = new EntityZyybrfjxx();

                            zyVo.FPRN = dr["FFLFSBH"].ToString();
                            zyVo.FZLLBBH = dr["FFLFS"].ToString();
                            zyVo.FZLLB = dr["FFLCXBH"].ToString();
                            zyVo.FZZZYBH = dr["FFLCX"].ToString();
                            zyVo.FZZZY = dr["FFLZZBH"].ToString();
                            zyVo.FRYCYBH = dr["FFLZZ"].ToString();
                            zyVo.FRYCY = dr["FYJY"].ToString();
                            zyVo.FMZZYZDBH = dr["FYCS"].ToString();
                            zyVo.FMZZYZD = dr["FYTS"].ToString();
                            zyVo.FSSLCLJBH = dr["FYRQ1"].ToString();
                            zyVo.FSSLCLJ = dr["FYRQ2"].ToString();
                            zyVo.FSYJGZJBH = dr["FQJY"].ToString();
                            zyVo.FSYJGZJ = dr["FQCS"].ToString();
                            zyVo.FSYZYSBBH = dr["FQTS"].ToString();
                            zyVo.FSYZYSB = dr["FQRQ1"].ToString();
                            zyVo.FSYZYJSBH = dr["FQRQ2"].ToString();
                            zyVo.FSYZYJS = dr["FZNAME"].ToString();
                            zyVo.FBZSHBH = dr["FZJY"].ToString();
                            zyVo.FBZSH = dr["FZCS"].ToString();

                            lstUpVo[i].fpVo.lstZyVo.Add(zyVo);
                        }
                    }
                    #endregion

                    #endregion

                }
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetPatFirstInfo--" + e);
            }
            finally
            {
                svcBa = null;
            }
            return lstUpVo;
        }
        #endregion

        #region 保存首页上传信息
        /// <summary>
        /// 保存首页上传信息
        /// </summary>
        /// <param name="lstVo"></param>
        /// <returns></returns>
        public int SavePatFirstPage(List<EntityPatUpload> lstVo)
        {
            int affectRows = 0;
            decimal serNo = 0;
            string Sql = string.Empty;
            List<EntityPatUpload> lstVo1 = new List<EntityPatUpload>();
            SqlHelper svc = null;
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);

                if (lstVo.Count > 0)  // new
                {
                    foreach (EntityPatUpload item in lstVo)
                    {
                        if (item.Issucess == -1)
                            continue;
                        if (item.STATUS <= 0)
                        {
                            if (item.Issucess == 1) //上传成功
                            {
                                if (CheckSequence(svc, "t_upload") > 0)
                                    serNo = Function.Dec(GetNextID(svc, "t_upload").ToString());
                                item.SERNO = serNo;
                                item.STATUS = 1;
                                item.UPLOADDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                item.RECORDDDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                item.OPERCODE = item.JBR;
                                lstVo1.Add(item);
                            }
                        }
                        else if (item.STATUS == 1) //已上传过
                        {
                            item.UPLOADDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            #region Sql
                            svc = new SqlHelper(EnumBiz.onlineDB);
                            Sql = @"update t_upload set UPLOADDATE = ? where serno = ?";

                            IDataParameter[] parm = svc.CreateParm(2);
                            parm[0].Value = item.UPLOADDATE;
                            parm[1].Value = item.SERNO;

                            #endregion
                            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));

                        }
                    }
                    if (lstVo1.Count > 0)
                    {
                        lstParm.Add(svc.GetInsertParm(lstVo1.ToArray()));
                    }
                    if (lstParm.Count > 0)
                        affectRows = svc.Commit(lstParm);

                }
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("SavePatFirstPage--" + e);
                affectRows = -1;
            }
            finally
            {
                svc = null;
            }
            return affectRows;
        }

        #endregion

        #region 获取下一个ID
        /// <summary>
        /// 获取下一个ID
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns>获取下一个ID</returns>
        public int GetNextID(SqlHelper svc, string tabName)
        {
            int intMinID = 0;
            string Sql = string.Empty;
            tabName = tabName.ToLower();
            try
            {
                if (this.CheckSequence(svc, tabName) >= 0)
                {
                    Sql = @"update sysSequenceid  set curid = curid + 1 where tabname = ?";
                    IDataParameter[] parm = svc.CreateParm(1);
                    parm[0].Value = tabName;
                    if (svc.ExecSql(Sql, parm) > 0)
                    {
                        Sql = @"select curid from sysSequenceid  where tabname = ?";
                        parm = svc.CreateParm(1);
                        parm[0].Value = tabName;

                        DataTable dt = svc.GetDataTable(Sql, parm);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            intMinID = (dt.Rows[0]["curid"] == System.DBNull.Value) ? 1 : Function.Int(dt.Rows[0]["curid"]);
                        }
                        else
                        {
                            intMinID = 1;
                        }
                    }
                    else
                    {
                        intMinID = -1;
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException(e);
                intMinID = 1;
            }
            return intMinID;
        }
        #endregion

        #region 检查
        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        private int CheckSequence(SqlHelper svc, string tabName)
        {
            string Sql = @"select 1 from sysSequenceid  t where t.tabname = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = tabName;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0)
            {
                Sql = @"insert into sysSequenceid  (tabname,colname, curid) values (?, ?,?)";
                parm = svc.CreateParm(3);
                parm[0].Value = tabName;
                parm[1].Value = "serno";
                parm[2].Value = 0;
                parm[2].DbType = DbType.Int32;
                return svc.ExecSql(Sql, parm);
            }
            return 1;
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
