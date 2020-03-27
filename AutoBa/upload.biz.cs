using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
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
            bool isExisitBa = false;
            bool isUploadparm = false;
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

                SqlReg = @"select distinct t1.registerid_chr,
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
                                rehis.emrinpatientid,
                                rehis.emrinpatientdate,
                                ee.lastname_vchr as jbr,
                                --dd.serno,
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
                                left join t_bse_employee ee
                                on dd.opercode = ee.empno_chr
                                where c.status_int = 1 ";
                #endregion

                #region 结算记录
                SqlJs = @"select distinct a.registerid_chr, a.jzjlh, a.invoiceno_vchr, b.inpatientid_chr,c.status,c.firstSource
                                  from t_ins_chargezy_csyb a
                                  left join t_opr_bih_register b
                                    on a.registerid_chr = b.registerid_chr
                                    left join t_upload c
                                        on a.registerid_chr = c.registerid 
                                 where (a.createtime between
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))  ";

                //SqlJs = @"select a.registerid_chr, a.jzjlh, a.invoiceno_vchr, b.inpatientid_chr,c.status,c.firstSource
                //                  from t_ins_chargezy_csyb a
                //                  left join t_opr_bih_register b
                //                    on a.registerid_chr = b.registerid_chr
                //                    left join t_upload c
                //                        on a.registerid_chr = c.registerid 
                //                 where  ";
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
                        case "JZJLH1":
                            strSubJs += "  a.jzjlh in " + keyValue;
                            break;
                        case "chkStat":
                            //strSubJs += " (and c.status is null or c.status = 0) ";
                            isUploadparm = true;
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
                        int uploadStatus = Function.Int(drJs["status"]);
                        int firstSource = Function.Int(drJs["firstSource"]);
                        //未上传，来源icaren也属于未上传
                        if(isUploadparm)
                        {
                            if (uploadStatus == 1 && firstSource == 1)
                                continue;
                        }

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
                        isExisitBa = false;
                        string MZH = drReg["MZH"].ToString();
                        string emrinpatientid = drReg["emrinpatientid"].ToString();
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

                        #region 查找发票号
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
                        #endregion

                        EntityPatUpload upVo = new EntityPatUpload();
                        upVo.fpVo = new EntityFirstPage();

                        DataRow[] drr = dtBa.Select("fprn =  '" + ipno + "' or fzyid = '" + ipno + "'");
                        if (drr.Length > 0)
                        {
                            foreach (DataRow drrBa in drr)
                            {
                                string fcydate = Function.Datetime(drrBa["fcydate"]).ToString("yyyy-MM-dd");
                                string frydate = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                int ftimes = Function.Int(drrBa["FTIMES"].ToString());

                                if (cydate == fcydate || cydate1 == fcydate || cydate2 == fcydate || rydate == frydate || rydate1 == frydate || rydate2 == frydate)
                                {
                                    isExisitBa = true;

                                    #region 首页信息  来源病案
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
                                    if (string.IsNullOrEmpty(upVo.fpVo.FJOB))
                                        upVo.fpVo.FJOB = "其他";
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
                                    if (string.IsNullOrEmpty(upVo.fpVo.FJOBBH))
                                        upVo.fpVo.FJOBBH = "90";
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
                                    upVo.firstSource = 1;//来源病案
                                    #endregion
                                }
                            }
                        }

                        if (!isExisitBa)
                        {
                            EntityPatUpload vo = GetPatBaFromIcare(ipno, registerid, emrinpatientdate);
                            if (vo != null)
                            {
                                vo.fpVo.FPHM = FPHM;
                                upVo.fpVo = vo.fpVo;
                                upVo.fpVo.JZJLH = jzjlh;
                                upVo.fpVo.FWSJGDM = "12441900457226325L";
                                upVo.firstSource = 2;
                            }
                        }

                        #region  显示列表
                        upVo.XH = ++n;
                        upVo.UPLOADTYPE = 1;
                        upVo.PATNAME = drReg["xm"].ToString();
                        upVo.PATSEX = drReg["sex"].ToString();
                        upVo.IDCARD = drReg["idcard_chr"].ToString();
                        upVo.INPATIENTID = drReg["ipno"].ToString();
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
                        //upVo.SERNO = Function.Dec(drReg["serno"]);
                        if (drReg["status"].ToString() == "1")
                            upVo.SZ = "已上传";
                        else
                            upVo.SZ = "未上传";

                        if (drReg["jbr"] != DBNull.Value)
                            upVo.JBRXM = drReg["jbr"].ToString();
                        if (drReg["uploaddate"] != DBNull.Value)
                            upVo.UPLOADDATE = Function.Datetime(drReg["uploaddate"]);
                        #endregion

                        #region 上传信息 出院小结
                        if (emrinpatientid != ipno)
                            Log.Output("D:\\log.txt", emrinpatientid + " --" + ipno);
                        DataTable dtXj = GetPatCyxjList2(emrinpatientid, emrinpatientdate);

                        if (dtXj != null && dtXj.Rows.Count > 0)
                        {
                            #region 上传信息 出院小结
                            DataRow drXj = dtXj.Rows[0];
                            upVo.xjVo = new EntityCyxj();
                            upVo.xjVo.JZJLH = jzjlh;
                            upVo.xjVo.MZH = MZH;
                            upVo.xjVo.ZYH = ipno;
                            upVo.xjVo.MZZD = upVo.fpVo.FMZZD;//drrBa["FMZZD"].ToString();
                            if (string.IsNullOrEmpty(upVo.xjVo.MZZD))
                                upVo.xjVo.MZZD = "-";
                            if (upVo.xjVo.MZZD.Length > 100)
                                upVo.xjVo.MZZD = upVo.xjVo.MZZD.Substring(0, 100);
                            upVo.xjVo.RYZD = drXj["inhospitaldiagnose"].ToString().Trim();
                            if (string.IsNullOrEmpty(upVo.xjVo.RYZD))
                                upVo.xjVo.RYZD = upVo.fpVo.FMZZD;
                            upVo.xjVo.CYZD = drXj["outhospitaldiagnose"].ToString().Trim();
                            if (drXj["outhospitaldiagnose"] == DBNull.Value)
                                upVo.xjVo.CYZD = "-";
                            upVo.xjVo.XM = upVo.fpVo.FNAME;
                            upVo.xjVo.XB = upVo.fpVo.FSEX;
                            if (upVo.xjVo.XB == "男")
                                upVo.xjVo.XB = "1";
                            else if (upVo.xjVo.XB == "女")
                                upVo.xjVo.XB = "2";
                            else upVo.xjVo.XB = "9";
                            upVo.xjVo.NL = upVo.fpVo.FAGE;
                            if (!string.IsNullOrEmpty(upVo.fpVo.FIDCard))
                                upVo.xjVo.GMSFHM = upVo.fpVo.FIDCard;
                            else
                                upVo.xjVo.GMSFHM = drReg["idcard_chr"].ToString();
                            upVo.xjVo.RYRQ = drReg["RYRQ1"].ToString();
                            upVo.xjVo.CYRQ = drReg["CYRQ1"].ToString();
                            upVo.xjVo.RYSJ = drReg["RYSJ"].ToString();
                            upVo.xjVo.CYSJ = drReg["CYSJ"].ToString();
                            upVo.xjVo.ZYTS = upVo.fpVo.FDAYS;
                            upVo.xjVo.ZY = upVo.fpVo.FJOB;
                            upVo.xjVo.JG = upVo.fpVo.FNATIVE;
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

                            if (upVo.fpVo.FTIMES > 0)
                                upVo.xjVo.FTIMES = upVo.fpVo.FTIMES.ToString();
                            else
                                upVo.xjVo.FTIMES = rycs.ToString();
                            upVo.xjVo.FSUM1 = Function.Dec(upVo.fpVo.FSUM1);
                            upVo.xjVo.FPHM = FPHM;
                            #endregion
                        }
                        #endregion

                        data.Add(upVo);
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
                                on a.jzjlh = c.jzjlh 
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
                            //strSubJs += " and a.jzjlh in " + keyValue + "";
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
                                    if (string.IsNullOrEmpty(upVo.fpVo.FJOB))
                                        upVo.fpVo.FJOB = "其他";
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
                                    if (string.IsNullOrEmpty(upVo.fpVo.FJOBBH))
                                        upVo.fpVo.FJOBBH = "90";
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
                                    //upVo.SERNO = Function.Dec(dr2["serno"]);
                                    if (dr2["status"].ToString() == "1")
                                        upVo.SZ = "已上传";
                                    else
                                        upVo.SZ = "未上传";

                                    if (dr2["jbr"] != DBNull.Value)
                                        upVo.JBRXM = dr2["jbr"].ToString();
                                    if (dr2["uploaddate"] != DBNull.Value)
                                        upVo.UPLOADDATE = Function.Datetime(dr2["uploaddate"]);

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

        #region 查询对应
        /// <summary>
        /// 查询对应
        /// </summary>
        /// <param name="parmStr"></param>
        /// <returns></returns>
        public void GetQuerypat(string dteBegin ,string dteEnd,string parmStr,out List<EntityQueryBa> dataIcare,out List<EntityQueryBa> dataBa)
        {
            string SqlBa = string.Empty;
            string SqlReg = string.Empty;
            string SqlJs = string.Empty;
            IDataParameter [] parm = null;
            SqlHelper svcBa = null;
            SqlHelper svc = null;
            dataIcare = new List<EntityQueryBa>();
            dataBa = new List<EntityQueryBa>();
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
                                rehis.emrinpatientid,
                                rehis.emrinpatientdate,
                                ee.lastname_vchr as jbr,
                                --dd.serno,
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
                                left join t_bse_employee ee
                                on dd.opercode = ee.empno_chr
                                where c.status_int = 1 ";
                #endregion

                #region 结算记录
                SqlJs = @"select a.registerid_chr, a.jzjlh, a.invoiceno_vchr, b.inpatientid_chr
                                  from t_ins_chargezy_csyb a
                                  left join t_opr_bih_register b
                                    on a.registerid_chr = b.registerid_chr
                                    inner join t_upload c
                                        on a.registerid_chr = c.registerid 
                                 where (a.createtime between
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))  ";


                #endregion

                #region 条件
                string strSubJs = string.Empty;
                strSubJs = "and (a.jzjlh = '" + parmStr + "' or b.inpatientid_chr = '" + parmStr + "')";
                #endregion

                #region 赋值

                if (!string.IsNullOrEmpty(strSubJs))
                    SqlJs += strSubJs;

                parm = svc.CreateParm(2);
                parm[0].Value = dteBegin + " 00:00:00";
                parm[1].Value = dteEnd + " 23:59:59";
                DataTable dtJs = svc.GetDataTable(SqlJs,parm);

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
                        string emrinpatientid = drReg["emrinpatientid"].ToString();
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

                        #region 查找发票号
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
                        #endregion

                        #region ba
                        DataRow[] drr = dtBa.Select("fprn =  '" + ipno + "' or fzyid = '" + ipno + "'");
                        if (drr.Length > 0)
                        {
                            foreach (DataRow drrBa in drr)
                            {
                                string fcydate = Function.Datetime(drrBa["fcydate"]).ToString("yyyy-MM-dd");
                                string frydate = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                int ftimes = Function.Int(drrBa["FTIMES"].ToString());
                                EntityQueryBa vo = new EntityQueryBa();
                                vo.inpatientId = drrBa["fzyid"].ToString();
                                vo.fprn = drrBa["FPRN"].ToString();
                                vo.name = drrBa["FNAME"].ToString();
                                vo.sex = drrBa["FSEX"].ToString();
                                vo.IDcard = drrBa["FASCARD1"].ToString();
                                vo.inTimes = drrBa["FTIMES"].ToString();
                                vo.rysj = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                vo.cysj = Function.Datetime(drrBa["FCYDATE"]).ToString("yyyy-MM-dd"); 
                                dataBa.Add(vo);
                            }
                        }
                        #endregion

                        #region  显示列表
                        EntityQueryBa voR = new EntityQueryBa();
                        voR.inpatientId = drReg["ipno"].ToString();
                        voR.jzjlh = jzjlh;
                        //voR.fprn = drReg["FPRN"].ToString();
                        voR.name = drReg["xm"].ToString();
                        voR.sex = drReg["sex"].ToString();
                        voR.IDcard = drReg["idcard_chr"].ToString();
                        voR.inTimes = drReg["rycs"].ToString();
                        voR.rysj = Function.Datetime(drReg["rysj"]).ToString("yyyy-MM-dd");
                        voR.cysj = Function.Datetime(drReg["cysj"]).ToString("yyyy-MM-dd");
                        dataIcare.Add(voR);
                        #endregion

                    }
                }
                #endregion

                #endregion
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetQuerypat--" + e);
            }
            finally
            {
                svc = null;
            }
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

                string Sql1 = @"select createdate,opendate 
                          from outhospitalrecord 
                          where inpatientid = ?
                           and inpatientdate= to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                           and status=0
                            union 
                            select createdate,opendate 
                                                      from t_emr_outhospitalin24hours 
                                                      where inpatientid = ?
                                                       and inpatientdate = to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                                       and status=0 ";
                //普通出院记录
                string Sql2 = @"select a.inpatientid,
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

                ///24小时出院记录
                string Sql3 = @"select 
                               t.inpatientid,
                               t.inpatientdate,
                               t.opendate,
                               t.createdate,
                               t.createuserid,
                               t.deactiveddate,
                               t.deactivedoperatorid,
                               t.status,
                               t.representor,
                               t.maindescription,
                               t.maindescriptionxml,
                               t.inhospitalinstance  as inhospitaldiagnose_right,
                               t.inhospitalinstancexml,
                               t.inhospitaldiagnose1,
                               t.inhospitaldiagnose1xml,
                               t.inhospitaldiagnose2 as inhospitaldiagnose,
                               t.inhospitaldiagnose2xml,
                               t.diagnosecoruse as inhospitalby,
                               t.diagnosecorusexml,
                               t.outhospitalinstance as outhospitalcase_right,
                               t.outhospitalinstancexml,
                               t.outhospitaldiagnose1 as outhospitaldiagnose,
                               t.outhospitaldiagnose1xml,
                               t.outhospitaldiagnose2,
                               t.outhospitaldiagnose2xml,
                               t.outhospitaladvice1 as outhospitaladvice_right,
                               t.outhospitaladvice1xml,
                               t.outhospitaladvice2,
                               t.outhospitaladvice2xml,
                               t.doctorsign,
                               t.recorddate,
                               t.modifydate,
                               t.modifyuserid,
                               t.firstprintdate,
                               t.outhospitaldate,
                         f_getempnamebyno_1stofall(t.doctorsign)  as doctorname
                              from t_emr_outhospitalin24hours t 
                             where t.inpatientid = ?
                               and t.inpatientdate =  to_date(?,'yyyy-mm-dd hh24:mi:ss')
                               and t.opendate = to_date(?,'yyyy-mm-dd hh24:mi:ss')
                               and t.status = 0";

                #endregion



                if (!string.IsNullOrEmpty(ipno) && !string.IsNullOrEmpty(emrinpatientdate))
                {
                    parm = svc.CreateParm(4);
                    parm[0].Value = ipno;
                    parm[1].Value = emrinpatientdate;
                    parm[2].Value = ipno;
                    parm[3].Value = emrinpatientdate;

                    DataTable dt1 = svc.GetDataTable(Sql1, parm);

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

                        dtResult = svc.GetDataTable(Sql2, parm);

                        if (dtResult == null || dtResult.Rows.Count <= 0)
                        {
                            parm = svc.CreateParm(3);
                            parm[0].Value = ipno;
                            parm[1].Value = emrinpatientdate;
                            parm[2].Value = opendate;

                            dtResult = svc.GetDataTable(Sql3, parm);
                        }
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

        #region 病案首页 icare
        /// <summary>
        /// 病案首页 icare
        /// </summary>
        /// <param name="dicParm"></param>
        /// <returns></returns>
        public EntityPatUpload GetPatBaFromIcare(string ipno, string registerid, string emrinpatientdate)
        {
            SqlHelper svc = null;
            IDataParameter[] parm = null;
            string opendate = string.Empty;
            DataTable dtResult = null;
            DataTable dtPatinfo = null;
            DataTable dtbTransfer = null;
            DataTable dtbDs = null;
            DataTable dtbOutDiag = null;
            DataTable dtbOP = null;
            DataTable dtbTumor = null;
            DataTable dtbInfant = null;
            DataTable dtbZlksjj = null;
            DataTable dtbInInfo = null;
            EntityPatUpload upVo = null;

            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);

                #region 首页

                string Sql1 = @"select opendate
                                      from inhospitalmainrecord
                                     where inpatientid = ?
                                       and inpatientdate  = to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                       and status = 1 ";

                string Sql2 = @"select a.inpatientid,
                                           a.inpatientdate,
                                           a.opendate,
                                           a.lastmodifydate,
                                           a.lastmodifyuserid,
                                           a.deactiveddate,
                                           a.deactivedoperatorid,
                                           a.status,
                                           a.diagnosis,
                                           a.inhospitaldiagnosis,
                                           a.doctor,
                                           a.confirmdiagnosisdate,
                                           a.condictionwhenin,
                                           a.maindiagnosis,
                                           a.mainconditionseq,
                                           a.icd_10ofmain,
                                           a.infectiondiagnosis,
                                           a.infectioncondictionseq,
                                           a.icd_10ofinfection,
                                           a.pathologydiagnosis,
                                           a.scachesource,
                                           a.sensitive,
                                           a.hbsag,
                                           a.hcv_ab,
                                           a.hiv_ab,
                                           a.accordwithouthospital,
                                           a.accordinwithout,
                                           a.accordbeforeoperationwithafter,
                                           a.accordclinicwithpathology,
                                           a.accordradiatewithpathology,
                                           a.salvetimes,
                                           a.salvesuccess,
                                           a.directordt,
                                           a.subdirectordt,
                                           a.dt,
                                           a.inhospitaldt,
                                           a.attendinforadvancesstudydt,
                                           a.graduatestudentintern,
                                           a.intern,
                                           a.coder,
                                           a.quality,
                                           a.qcdt,
                                           a.qcnurse,
                                           a.qctime,
                                           a.rtmodeseq,
                                           a.rtruleseq,
                                           a.rtco,
                                           a.rtaccelerator,
                                           a.rtx_ray,
                                           a.rtlacuna,
                                           a.originaldiseaseseq,
                                           a.originaldiseasegy,
                                           a.originaldiseasetimes,
                                           a.originaldiseasedays,
                                           a.originaldiseasebegindate,
                                           a.originaldiseaseenddate,
                                           a.lymphseq,
                                           a.lymphgy,
                                           a.lymphtimes,
                                           a.lymphdays,
                                           a.lymphbegindate,
                                           a.lymphenddate,
                                           a.metastasisgy,
                                           a.metastasistimes,
                                           a.metastasisdays,
                                           a.metastasisbegindate,
                                           a.metastasisenddate,
                                           a.chemotherapymodeseq,
                                           a.chemotherapywholebody,
                                           a.chemotherapylocal,
                                           a.chemotherapyintubate,
                                           a.chemotherapythorax,
                                           a.chemotherapyabdomen,
                                           a.chemotherapyspinal,
                                           a.chemotherapyothertry,
                                           a.chemotherapyother,
                                           a.totalamt,
                                           a.bedamt,
                                           a.nurseamt,
                                           a.wmamt,
                                           a.cmfinishedamt,
                                           a.cmsemifinishedamt,
                                           a.radiationamt,
                                           a.assayamt,
                                           a.o2amt,
                                           a.bloodamt,
                                           a.treatmentamt,
                                           a.operationamt,
                                           a.deliverychildamt,
                                           a.checkamt,
                                           a.anaethesiaamt,
                                           a.babyamt,
                                           a.accompanyamt,
                                           a.otheramt1,
                                           a.otheramt2,
                                           a.otheramt3,
                                           a.corpsecheck,
                                           a.firstcase,
                                           a.follow,
                                           a.follow_week,
                                           a.follow_month,
                                           a.follow_year,
                                           a.modelcase,
                                           a.bloodtype,
                                           a.bloodrh,
                                           a.bloodtransactoin,
                                           a.rbc,
                                           a.plt,
                                           a.plasm,
                                           a.wholeblood,
                                           a.otherblood,
                                           a.consultation,
                                           a.longdistanctconsultation,
                                           a.toplevel,
                                           a.nurseleveli,
                                           a.nurselevelii,
                                           a.nurseleveliii,
                                           a.icu,
                                           a.specialnurse,
                                           a.insurancenum,
                                           a.modeofpayment,
                                           a.patienthistoryno,
                                           a.outpatientdate,
                                           a.birthplace,
                                           a.operation,
                                           a.baby,
                                           a.chemotherapy,path,
                                    newbabyweight,newbabyinhostpitalweight,sszyj_jbbm,blzd_blh,blzd_jbbm,discharged_int,discharged_varh,readmitted31_int,readmitted31_varh,inrnssday,
                                    inrnsshour,inrnssmin,outrnssday,outrnsshour,outrnssmin,inhospitalway,
                                    medicalamt_new,treatmentamt_new,compositeeotheramt_new,pdamt_new,ldamt_new,idamt_new,cdamt_new,noopamt_new,opbytreatmentamt_new,physicalamt_new,
                                    rehabilitationamt_new,cmtamt_new,aaamt_new,albuminamt_new,globulinamt_new,cfamt_new,cytokinesamt_new,onetimebysupmt_new,onetimebytmamt_new,onttimebyopamt_new,
                                    tumor,t,n,m,installments,metastasiscount,f_getempnamebyid(a.doctor)  doctorname,
										                                    f_getempnamebyid(a.directordt) directordtname,
										                                    f_getempnamebyid(a.subdirectordt) subdirectordtname,
										                                    f_getempnamebyid(a.dt) dtname,
										                                    f_getempnamebyid(a.inhospitaldt) inhospitaldtname,
										                                    f_getempnamebyid(a.attendinforadvancesstudydt) attendinforadvancesstudydtname,
										                                    f_getempnamebyid(a.graduatestudentintern) graduatestudentinternname,
										                                    f_getempnamebyid(a.intern) internname,
										                                    f_getempnamebyid(a.coder) codername,
										                                    f_getempnamebyid(a.qcdt) qcdtname,
										                                    f_getempnamebyid(a.qcnurse) qcnursename
										                                    from inhospitalmainrecord_content a 
                                                                             where inpatientid = ? and 
                                                                             inpatientdate = to_date(?, 'yyyy-mm-dd hh24:mi:ss') and 
                                                                             opendate= to_date(?, 'yyyy-mm-dd hh24:mi:ss') and status =1 ";
                #endregion

                #region 字典
                DataTable dtDic = GetGDCaseDICT();
                #endregion

                if (!string.IsNullOrEmpty(ipno) && !string.IsNullOrEmpty(emrinpatientdate))
                {
                    parm = svc.CreateParm(2);
                    parm[0].Value = ipno;
                    parm[1].Value = emrinpatientdate;

                    DataTable dt1 = svc.GetDataTable(Sql1, parm);

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

                        dtResult = svc.GetDataTable(Sql2, parm);
                        dtPatinfo = GetPatinfo(registerid);
                        dtbInInfo = GetPatientInInfo(registerid);
                        dtbTransfer = GetTransferInfo(registerid);
                        dtbDs = GetPatientDiagnosisInfo(registerid);
                        dtbOutDiag = GetDiagnosis(registerid, "3");
                        dtbOP = GetOperationInfo(registerid);
                        dtbTumor = GetChemotherapyMedicine(registerid);
                        dtbInfant = LaborInfo(registerid);
                        dtbZlksjj = GetChemotherapyInfo(registerid);

                        if (dtResult != null && dtResult.Rows.Count > 0)
                        {
                            DataRow dr = dtResult.Rows[0];
                            DataRow drPatient = dtPatinfo.Rows[0];
                            DataRow drInInfo = dtbInInfo.Rows[0];
                            DataRow drDS = dtbDs.Rows[0];

                            upVo = new EntityPatUpload();
                            upVo.fpVo = new EntityFirstPage();
                            upVo.fpVo.FBGLX = "";
                            #region 付款方式
                            if (dr["MODEOFPAYMENT"].ToString() == "城镇职工基本医疗保险")
                            {
                                upVo.fpVo.FFBBHNEW = "1";
                                upVo.fpVo.FFBNEW = "城镇职工基本医疗保险";
                            }
                            else if (dr["MODEOFPAYMENT"].ToString() == "城镇居民基本医疗保险")
                            {
                                upVo.fpVo.FFBBHNEW = "2";
                                upVo.fpVo.FFBNEW = "城镇居民基本医疗保险";
                            }
                            else if (dr["MODEOFPAYMENT"].ToString() == "新型农村合作医疗")
                            {
                                upVo.fpVo.FFBBHNEW = "3";
                                upVo.fpVo.FFBNEW = "新型农村合作医疗";
                            }
                            else if (dr["MODEOFPAYMENT"].ToString() == "贫困救助")
                            {
                                upVo.fpVo.FFBBHNEW = "4";
                                upVo.fpVo.FFBNEW = "贫困救助";
                            }
                            else if (dr["MODEOFPAYMENT"].ToString() == "商业医疗保险")
                            {
                                upVo.fpVo.FFBBHNEW = "5";
                                upVo.fpVo.FFBNEW = "商业医疗保险";
                            }
                            else if (dr["MODEOFPAYMENT"].ToString() == "全公费")
                            {
                                upVo.fpVo.FFBBHNEW = "6";
                                upVo.fpVo.FFBNEW = "全公费";
                            }
                            else if (dr["MODEOFPAYMENT"].ToString() == "全自费")
                            {
                                upVo.fpVo.FFBBHNEW = "7";
                                upVo.fpVo.FFBNEW = "全自费";
                            }
                            else if (dr["MODEOFPAYMENT"].ToString() == "其他社会保险")
                            {
                                upVo.fpVo.FFBBHNEW = "8";
                                upVo.fpVo.FFBNEW = "其他社会保险";
                            }
                            else
                            {
                                upVo.fpVo.FFBBHNEW = "9";
                                upVo.fpVo.FFBNEW = "其他";
                            }
                            #endregion

                            #region 健康卡号
                            if (dr["INSURANCENUM"] != DBNull.Value)
                                upVo.fpVo.FASCARD1 = dr["INSURANCENUM"].ToString();
                            else
                                upVo.fpVo.FASCARD1 = "-";
                            #endregion

                            #region 住院次数
                            upVo.fpVo.FTIMES = Function.Int(drPatient["ftimes"]);
                            #endregion

                            #region 病案号
                            upVo.fpVo.FPRN = drPatient["fprn"].ToString();
                            #endregion

                            #region 病人姓名
                            upVo.fpVo.FNAME = drPatient["FNAME"].ToString();
                            #endregion

                            #region 性别
                            string strSex = drPatient["fsex"].ToString();
                            upVo.fpVo.FSEX = strSex.Trim();
                            if (strSex == "男")
                            {
                                upVo.fpVo.FSEXBH = "1";
                            }
                            else if (strSex.Trim() == "女")
                            {
                                upVo.fpVo.FSEXBH = "2";
                            }
                            #endregion

                            #region 出生日期
                            upVo.fpVo.FBIRTHDAY = Function.Datetime(drPatient["fbirthday"]).ToString("yyyyMMdd");
                            #endregion

                            #region 年龄 
                            upVo.fpVo.FAGE = CalcAge(Function.Datetime(drPatient["fbirthday"]), Function.Datetime(drPatient["frydate"]));
                            #endregion

                            #region 国籍
                            string fcountry = drPatient["fcountry"].ToString();
                            if (!string.IsNullOrEmpty(fcountry))
                            {
                                DataRow[] drD = dtDic.Select("FCODE='GBCOUNTRY' and fmc='" + fcountry + "'");
                                if (drD != null && drD.Length > 0)
                                {
                                    upVo.fpVo.fcountry = drD[0]["fmc"].ToString();
                                    upVo.fpVo.fcountrybh = drD[0]["fbh"].ToString();
                                }
                            }
                            if (string.IsNullOrEmpty(upVo.fpVo.fcountrybh))
                                upVo.fpVo.fcountrybh = "-";
                            if (string.IsNullOrEmpty(upVo.fpVo.fcountry))
                                upVo.fpVo.fcountry = "-";
                            #endregion

                            #region 民族
                            string fnationality = drPatient["fnationality"].ToString();
                            if (!string.IsNullOrEmpty(fcountry))
                            {
                                DataRow[] drD = dtDic.Select("FCODE='GBNATIONALITY' and fmc='" + fnationality + "'");
                                if (drD != null && drD.Length > 0)
                                {
                                    upVo.fpVo.fnationalitybh = drD[0]["fbh"].ToString();
                                    upVo.fpVo.fnationality = drD[0]["fmc"].ToString();
                                }
                            }
                            if (string.IsNullOrEmpty(upVo.fpVo.fnationalitybh))
                                upVo.fpVo.fnationalitybh = "-";
                            if (string.IsNullOrEmpty(upVo.fpVo.fnationality))
                                upVo.fpVo.fnationality = "-";
                            #endregion

                            #region 新生儿出生体重
                            upVo.fpVo.FCSTZ = dr["newbabyweight"].ToString();
                            #endregion

                            #region 新生入院生体重
                            upVo.fpVo.FRYTZ = dr["NEWBABYINHOSTPITALWEIGHT"].ToString();
                            #endregion

                            #region 出生地
                            upVo.fpVo.FBIRTHPLACE = drPatient["fbirthplace"].ToString();
                            #endregion

                            #region 籍贯
                            upVo.fpVo.FNATIVE = drPatient["FNATIVE"].ToString();
                            #endregion

                            #region 身份证号
                            upVo.fpVo.FIDCard = drPatient["fidcard"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FIDCard))
                                upVo.fpVo.FIDCard = "-";
                            #endregion

                            #region 职业
                            upVo.fpVo.FJOB = drPatient["fjob"].ToString().Trim();
                            if (string.IsNullOrEmpty(upVo.fpVo.FJOB))
                                upVo.fpVo.FJOB = "其他";
                            #endregion

                                #region 婚姻状况
                            upVo.fpVo.FSTATUS = drPatient["fstatus"].ToString();
                            if (drPatient["fstatus"].ToString() == "未婚")
                            {
                                upVo.fpVo.FSTATUSBH = "1";
                            }
                            else if (drPatient["fstatus"].ToString() == "已婚")
                            {
                                upVo.fpVo.FSTATUSBH = "2";
                            }
                            else if (drPatient["fstatus"].ToString() == "离婚")
                            {
                                upVo.fpVo.FSTATUSBH = "4";
                            }
                            else if (drPatient["fstatus"].ToString() == "丧偶")
                            {
                                upVo.fpVo.FSTATUSBH = "3";
                            }
                            else
                                upVo.fpVo.FSTATUSBH = "9";
                            #endregion

                            #region 地址联系方式等
                            upVo.fpVo.FCURRADDR = drPatient["FCURRADDR"].ToString();
                            upVo.fpVo.FCURRTELE = drPatient["FCURRTELE"].ToString();
                            upVo.fpVo.FCURRPOST = drPatient["FCURRPOST"].ToString();
                            upVo.fpVo.FHKADDR = drPatient["fhkaddr"].ToString();
                            upVo.fpVo.FHKPOST = drPatient["fhkpost"].ToString();
                            upVo.fpVo.FDWNAME = drPatient["fdwname"].ToString();
                            upVo.fpVo.FDWADDR = drPatient["fdwaddr"].ToString();
                            upVo.fpVo.FDWTELE = drPatient["fdwtele"].ToString();
                            upVo.fpVo.FDWPOST = drPatient["fdwpost"].ToString();
                            upVo.fpVo.FLXNAME = drPatient["flxname"].ToString();
                            upVo.fpVo.FRELATE = drPatient["frelate"].ToString();
                            if (upVo.fpVo.FRELATE.Length > 10)
                                upVo.fpVo.FRELATE = upVo.fpVo.FRELATE.Substring(0, 10);
                            upVo.fpVo.FLXADDR = drPatient["FLXADDR"].ToString();
                            upVo.fpVo.FLXTELE = drPatient["flxtele"].ToString();
                            #endregion

                            #region 入院途径
                            if (dr["inhospitalway"].ToString() == "1")//急诊
                            {
                                upVo.fpVo.FRYTJ = "急诊";
                                upVo.fpVo.FRYTJBH = "1";
                            }
                            else if (dr["inhospitalway"].ToString() == "2")//门诊
                            {
                                upVo.fpVo.FRYTJ = "门诊";
                                upVo.fpVo.FRYTJBH = "2";
                            }
                            else if (dr["inhospitalway"].ToString() == "3")//其他医疗机构转入
                            {
                                upVo.fpVo.FRYTJ = "其他医疗机构转入";
                                upVo.fpVo.FRYTJBH = "3";
                            }
                            else
                            {
                                upVo.fpVo.FRYTJ = "其他";
                                upVo.fpVo.FRYTJBH = "9";
                            }
                            #endregion

                            #region 出入院科室
                            DataView dvT = new DataView(dtbTransfer);
                            dvT.RowFilter = "TYPE_INT = 5";
                            string strInDeptName = string.Empty;
                            if (dvT.Count > 0)
                            {
                                strInDeptName = dvT[0]["deptname_vchr"].ToString();
                                upVo.fpVo.FRYDEPT = dvT[0]["deptname_vchr"].ToString();
                                upVo.fpVo.FRYTYKH = dvT[0]["ba_deptnum"].ToString();
                            }
                            if (string.IsNullOrEmpty(upVo.fpVo.FRYTYKH))
                                upVo.fpVo.FRYTYKH = "-";
                            if (string.IsNullOrEmpty(upVo.fpVo.FRYDEPT))
                                upVo.fpVo.FRYDEPT = "-";
                            dvT.RowFilter = "TYPE_INT = 7 or TYPE_INT = 6";
                            if (dvT.Count > 0)
                            {
                                upVo.fpVo.FCYDEPT = dvT[0]["deptname_vchr"].ToString();
                                upVo.fpVo.FCYTYKH = dvT[0]["ba_deptnum"].ToString();
                            }
                            if (string.IsNullOrEmpty(upVo.fpVo.FCYTYKH))
                                upVo.fpVo.FCYTYKH = "-";
                            if (string.IsNullOrEmpty(upVo.fpVo.FCYDEPT))
                                upVo.fpVo.FCYDEPT = "-";

                            upVo.fpVo.FRYBS = upVo.fpVo.FRYDEPT;
                            upVo.fpVo.FCYBS = upVo.fpVo.FCYDEPT;
                            #endregion

                            #region 首次转科
                            dvT.RowFilter = "TYPE_INT = 3";
                            dvT.Sort = "modify_dat asc";
                            List<ListViewItem> lstItems = new List<ListViewItem>();
                            if (dvT.Count > 0)
                            {
                                for (int iL = 0; iL < dvT.Count; iL++)
                                {
                                    ListViewItem lvi = null;
                                    if (iL > 0)
                                    {
                                        if (dvT[iL]["ba_deptnum"].ToString() == dvT[iL - 1]["ba_deptnum"].ToString())//只是转区，未转科
                                        {
                                            continue;
                                        }
                                        lvi = new ListViewItem(dvT[iL - 1]["deptname_vchr"].ToString());
                                    }
                                    else//首次转科的源科室为入院科室
                                    {
                                        lvi = new ListViewItem(strInDeptName);
                                    }
                                    lvi.SubItems.Add(Convert.ToDateTime(dvT[iL]["modify_dat"]).ToString("yyyy-MM-dd HH:mm"));
                                    lvi.SubItems.Add(dvT[iL]["deptname_vchr"].ToString());
                                    lvi.SubItems.Add(dvT[iL]["ba_deptnum"].ToString());
                                    lstItems.Add(lvi);
                                }
                                ListViewItem vo = lstItems[0];
                                upVo.fpVo.FZKTYKH = vo.SubItems[3].ToString();
                                upVo.fpVo.FZKTYKH = upVo.fpVo.FZKTYKH.Replace("ListViewSubItem: {", "").Replace("}", "");
                                upVo.fpVo.FZKDEPT = vo.SubItems[2].ToString();
                                upVo.fpVo.FZKDEPT = upVo.fpVo.FZKDEPT.Replace("ListViewSubItem: {", "").Replace("}", "");
                                upVo.fpVo.FZKTIME = vo.SubItems[1].ToString();
                                upVo.fpVo.FZKTIME = Function.Datetime(upVo.fpVo.FZKTIME.Replace("ListViewSubItem: {", "").Replace("}", "")).ToString("yyyyMMddHHmmss");
                                #region 首次转科日期
                                upVo.fpVo.FZKDATE = vo.SubItems[1].ToString();
                                upVo.fpVo.FZKDATE = Function.Datetime(upVo.fpVo.FZKDATE.Replace("ListViewSubItem: {", "").Replace("}", "")).ToString("yyyy-MM-dd");
                                #endregion
                            }

                            if (string.IsNullOrEmpty(upVo.fpVo.FZKTYKH))
                                upVo.fpVo.FZKTYKH = "-";
                            if (string.IsNullOrEmpty(upVo.fpVo.FZKDEPT))
                                upVo.fpVo.FZKDEPT = "-";
                            if (string.IsNullOrEmpty(upVo.fpVo.FZKTIME))
                                upVo.fpVo.FZKTIME = "";
                            if (string.IsNullOrEmpty(upVo.fpVo.FZKDATE))
                                upVo.fpVo.FZKDATE = "";

                            #endregion

                            #region 入院日期
                            upVo.fpVo.FRYDATE = Function.Datetime(drPatient["frydate"]).ToString("yyyy-MM-dd");
                            upVo.fpVo.FRYTIME = Function.Datetime(drPatient["frydate"]).ToString("HH:mm:ss");
                            #endregion

                            #region 出院时间
                            upVo.fpVo.FCYDATE = Function.Datetime(drPatient["fcydate"]).ToString("yyyy-MM-dd");
                            upVo.fpVo.FCYTIME = Function.Datetime(drPatient["fcydate"]).ToString("HH:MM:ss");
                            #endregion

                            #region 住院天数
                            TimeSpan ts = Function.Datetime(upVo.fpVo.FCYDATE) - Function.Datetime(upVo.fpVo.FRYDATE);
                            upVo.fpVo.FDAYS = ts.Days.ToString();
                            if (upVo.fpVo.FDAYS == "0")
                                upVo.fpVo.FDAYS = "1";
                            //Log.Output("D:\\log.txt",upVo.JZJLH + "-->" + ts.TotalDays + "--" + ts.Days);
                            #endregion

                            #region 门诊诊断
                            upVo.fpVo.FMZZDBH = drInInfo["mzicd10"].ToString();
                            upVo.fpVo.FMZZD = drInInfo["diagnosis"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FMZZDBH))
                                upVo.fpVo.FMZZDBH = "-";
                            if (string.IsNullOrEmpty(upVo.fpVo.FMZZD))
                                upVo.fpVo.FMZZD = "-";

                            if(upVo.fpVo.FMZZD.Length > 50)
                            {
                                upVo.fpVo.FMZZD = upVo.fpVo.FMZZD.Substring(0,50);
                            }
                            #endregion

                            #region 门诊医生
                            upVo.fpVo.FMZDOCTBH = dr["doctor"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FMZDOCTBH))
                                upVo.fpVo.FMZDOCTBH = "-";
                            upVo.fpVo.FMZDOCT = GetEmpByID(upVo.fpVo.FMZDOCTBH);
                            if (string.IsNullOrEmpty(upVo.fpVo.FMZDOCT))
                                upVo.fpVo.FMZDOCT = "-";
                            #endregion

                            #region 疾病分型
                            if (dr["CONDICTIONWHENIN"].ToString() == "0")
                            {
                                upVo.fpVo.FJBFXBH = "1";
                                upVo.fpVo.FJBFX = "一般";
                            }
                            else if (dr["CONDICTIONWHENIN"].ToString() == "1")
                            {
                                upVo.fpVo.FJBFXBH = "2";
                                upVo.fpVo.FJBFX = "急";
                            }
                            else if (dr["CONDICTIONWHENIN"].ToString() == "2")
                            {
                                upVo.fpVo.FJBFXBH = "3";
                                upVo.fpVo.FJBFX = "疑难";
                            }
                            else if (dr["CONDICTIONWHENIN"].ToString() == "3")
                            {
                                upVo.fpVo.FJBFXBH = "4";
                                upVo.fpVo.FJBFX = "危重";
                            }
                            #endregion

                            #region 临床路径
                            if (dr["PATH"].ToString() == "1")
                            {
                                upVo.fpVo.FYCLJBH = "1";
                                upVo.fpVo.FYCLJ = "是";
                            }
                            else
                            {
                                upVo.fpVo.FYCLJBH = "2";
                                upVo.fpVo.FYCLJ = "否";
                            }
                            #endregion
;
                            #region 抢救 次数
                            int intNum = 0;
                            int.TryParse(drDS["SALVETIMES"].ToString(), out intNum);
                            upVo.fpVo.FQJTIMES = intNum.ToString();
                            int.TryParse(drDS["SALVESUCCESS"].ToString(), out intNum);
                            upVo.fpVo.FQJSUCTIMES = intNum.ToString();
                            #endregion

                            #region 病理诊断 病理疾病
                            upVo.fpVo.FPHZD = drDS["PATHOLOGYDIAGNOSIS"].ToString();
                            if (upVo.fpVo.FPHZD.Length > 100)
                                upVo.fpVo.FPHZD = upVo.fpVo.FPHZD.Substring(0, 100);
                            upVo.fpVo.FPHZDNUM = drDS["blzd_blh"].ToString();
                            upVo.fpVo.FPHZDBH = drDS["blzd_jbbm"].ToString();
                            #endregion

                            #region 过敏
                            if (string.IsNullOrEmpty(upVo.fpVo.FIFGMYWBH))
                            {
                                upVo.fpVo.FIFGMYWBH = "1";
                                upVo.fpVo.FIFGMYW = "否";
                                upVo.fpVo.FGMYW = "无";
                            }
                            else
                            {
                                upVo.fpVo.FIFGMYW = "是";
                                upVo.fpVo.FIFGMYWBH = "2";
                            }

                            #endregion

                            #region  尸检
                            int FBODYBH = Function.Int(dr["CORPSECHECK"]);
                            if (FBODYBH == 1)
                            {
                                upVo.fpVo.FBODYBH = FBODYBH.ToString();
                                upVo.fpVo.FBODY = "是";
                            }
                            else
                            {
                                upVo.fpVo.FBODYBH = "2";
                                upVo.fpVo.FBODY = "否";
                            }
                            #endregion

                            #region 血型
                            int FBLOODBH = Function.Int(dr["BLOODTYPE"]);
                            upVo.fpVo.FBLOODBH = FBLOODBH.ToString();
                            if (FBLOODBH == 1)
                                upVo.fpVo.FBLOOD = "A";
                            else if (FBLOODBH == 2)
                                upVo.fpVo.FBLOOD = "B";
                            else if (FBLOODBH == 3)
                                upVo.fpVo.FBLOOD = "O";
                            else if (FBLOODBH == 4)
                                upVo.fpVo.FBLOOD = "AB";
                            else if (FBLOODBH == 5)
                                upVo.fpVo.FBLOOD = "不详";
                            else if (FBLOODBH == 6)
                                upVo.fpVo.FBLOOD = "未查";
                            else
                            {
                                upVo.fpVo.FBLOODBH = "5";
                                upVo.fpVo.FBLOOD = "不详";
                            }

                            #endregion

                            #region RH
                            int BLOODRH = Function.Int(dr["BLOODRH"]);
                            upVo.fpVo.FRHBH = BLOODRH.ToString();
                            if (BLOODRH == 1)
                                upVo.fpVo.FRH = "阴";
                            else if (BLOODRH == 2)
                                upVo.fpVo.FRH = "阳";
                            else if (BLOODRH == 3)
                                upVo.fpVo.FRH = "不详";
                            else
                                upVo.fpVo.FRH = "未查";
                            #endregion

                            #region 主任 医生 护士 
                            upVo.fpVo.FKZRBH = drDS["DIRECTORDT"].ToString();
                            upVo.fpVo.FKZR = GetEmpByID(drDS["DIRECTORDT"].ToString());

                            upVo.fpVo.FZRDOCTBH = drDS["SUBDIRECTORDT"].ToString();
                            upVo.fpVo.FZRDOCTOR = GetEmpByID(drDS["SUBDIRECTORDT"].ToString());

                            upVo.fpVo.FZZDOCTBH = drDS["DT"].ToString();
                            upVo.fpVo.FZZDOCT = GetEmpByID(drDS["DT"].ToString());

                            upVo.fpVo.FZYDOCTBH = drDS["INHOSPITALDT"].ToString();
                            upVo.fpVo.FZYDOCT = GetEmpByID(drDS["INHOSPITALDT"].ToString());

                            upVo.fpVo.FNURSEBH = drDS["GRADUATESTUDENTINTERN"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FNURSEBH))
                                upVo.fpVo.FNURSEBH = "-";

                            upVo.fpVo.FNURSE = GetEmpByID(drDS["GRADUATESTUDENTINTERN"].ToString());
                            if (string.IsNullOrEmpty(upVo.fpVo.FNURSE))
                                upVo.fpVo.FNURSE = "-";
                            upVo.fpVo.FJXDOCTBH = drDS["ATTENDINFORADVANCESSTUDYDT"].ToString();
                            upVo.fpVo.FJXDOCT = GetEmpByID(drDS["ATTENDINFORADVANCESSTUDYDT"].ToString());

                            upVo.fpVo.FSXDOCTBH = drDS["INTERN"].ToString();
                            upVo.fpVo.FSXDOCT = GetEmpByID(drDS["INTERN"].ToString());

                            upVo.fpVo.FBMYBH = drDS["QCDT"].ToString();
                            upVo.fpVo.FBMY = GetEmpByID(drDS["QCDT"].ToString());
                            #endregion

                            #region 病案质量
                            upVo.fpVo.FQUALITYBH = (Function.Int(dr["QUALITY"]) + 1).ToString();
                            if (upVo.fpVo.FQUALITYBH == "1")
                                upVo.fpVo.FQUALITY = "甲";
                            else if (upVo.fpVo.FQUALITYBH == "2")
                                upVo.fpVo.FQUALITY = "乙";
                            else
                            {
                                upVo.fpVo.FQUALITYBH = "3";
                                upVo.fpVo.FQUALITY = "丙";
                            }
                                
                            #endregion

                            #region 质控
                            upVo.fpVo.FZKDOCTBH = drDS["QCDT"].ToString();
                            upVo.fpVo.FZKDOCT = GetEmpByID(drDS["QCDT"].ToString());

                            upVo.fpVo.FZKNURSEBH = drDS["QCNURSE"].ToString();
                            upVo.fpVo.FZKNURSE = GetEmpByID(drDS["QCNURSE"].ToString());

                            upVo.fpVo.FZKRQ = Function.Datetime(drDS["QCTIME"]).ToString("yyyyMMdd");
                            #endregion

                            #region 离院方式
                            int FLYFSBH = Function.Int(drDS["discharged_int"]);
                            if (FLYFSBH == 1)
                            {
                                upVo.fpVo.FLYFSBH = FLYFSBH.ToString();
                                upVo.fpVo.FLYFS = "医嘱离院";
                            }
                            else if (FLYFSBH == 2)
                            {
                                upVo.fpVo.FLYFSBH = FLYFSBH.ToString();
                                upVo.fpVo.FLYFS = "医嘱转院";
                            }
                            else if (FLYFSBH == 4)
                            {
                                upVo.fpVo.FLYFSBH = FLYFSBH.ToString();
                                upVo.fpVo.FLYFS = "非医嘱转院";
                            }
                            else if (FLYFSBH == 5)
                            {
                                upVo.fpVo.FLYFSBH = FLYFSBH.ToString();
                                upVo.fpVo.FLYFS = "死亡";
                            }
                            else
                            {
                                upVo.fpVo.FLYFSBH = "9";
                                upVo.fpVo.FLYFS = "其他";
                            }
                            #endregion

                            #region 再住院
                            upVo.fpVo.FYZOUTHOSTITAL = drDS["discharged_varh"].ToString();
                            upVo.fpVo.FSQOUTHOSTITAL = drDS["discharged_varh"].ToString();

                            if (drDS["readmitted31_int"] != DBNull.Value)
                            {
                                int FISAGAINRYBH = Function.Int(drDS["readmitted31_int"]);
                                if (FISAGAINRYBH == 2)
                                {
                                    upVo.fpVo.FISAGAINRYBH = "2";
                                    upVo.fpVo.FISAGAINRY = "有";
                                    upVo.fpVo.FISAGAINRYMD = drDS["readmitted31_varh"].ToString();
                                }
                                else
                                {
                                    upVo.fpVo.FISAGAINRYBH = "1";
                                    upVo.fpVo.FISAGAINRY = "无";
                                    upVo.fpVo.FISAGAINRYMD = "-";
                                }
                            }
                            else
                            {
                                upVo.fpVo.FISAGAINRYBH = "1";
                                upVo.fpVo.FISAGAINRY = "无";
                                upVo.fpVo.FISAGAINRYMD = "-";
                            }
                            #endregion

                            #region 颅脑损伤昏迷时间

                            decimal decNum = 0;
                            int.TryParse(drDS["inrnssday"].ToString().Trim(), out intNum);
                            upVo.fpVo.FRYQHMDAYS = intNum.ToString();
                            decimal.TryParse(drDS["inrnsshour"].ToString().Trim(), out decNum);
                            upVo.fpVo.FRYQHMHOURS = decNum.ToString();
                            int.TryParse(drDS["inrnssmin"].ToString().Trim(), out intNum);
                            upVo.fpVo.FRYQHMMINS = intNum.ToString();
                            upVo.fpVo.FRYQHMCOUNTS = (Function.Int(drDS["inrnssday"]) * 60 * 60).ToString();

                            decimal.TryParse(drDS["outrnssday"].ToString().Trim(), out decNum);
                            upVo.fpVo.FRYHMDAYS = decNum.ToString();
                            decimal.TryParse(drDS["outrnsshour"].ToString().Trim(), out decNum);
                            upVo.fpVo.FRYHMHOURS = decNum.ToString();
                            decimal.TryParse(drDS["outrnssmin"].ToString().Trim(), out decNum);
                            upVo.fpVo.FRYHMMINS = decNum.ToString();
                            upVo.fpVo.FRYHMCOUNTS = (Function.Int(drDS["outrnssday"]) * 60 * 60).ToString();
                            #endregion

                            #region 费用
                            clsInHospitalMainCharge[] objChargeArr = null;
                            GetCHRCATE(registerid, out objChargeArr);

                            if (objChargeArr == null || objChargeArr.Length <= 0)
                            {
                                upVo.fpVo.FSUM1 = 0;
                            }
                            else
                            {
                                decimal sumMoney = 0;
                                double fssxmamt = 0;
                                double lcwlzlf = 0;
                                double mzamt = 0;
                                double ssamt = 0;
                                double sszlamt = 0;
                                double xyamt = 0;
                                double kjyamt = 0;
                                for (int iC = 0; iC < objChargeArr.Length; iC++)
                                {
                                    sumMoney += Function.Dec(objChargeArr[iC].m_dblMoney);

                                    double p_dblMoney = objChargeArr[iC].m_dblMoney;
                                    string p_strChargeName = objChargeArr[iC].m_strTypeName;

                                    switch (p_strChargeName)
                                    {
                                        case "临床诊断项目费"://
                                            upVo.fpVo.FZDLLCF = Function.Dec(p_dblMoney);
                                            break;
                                        case "手术治疗费"://
                                            sszlamt = p_dblMoney;
                                            break;
                                        case "麻醉费"://
                                            upVo.fpVo.FZLLFMZF = Function.Dec(p_dblMoney);
                                            mzamt = p_dblMoney;
                                            break;
                                        case "手术费"://
                                            upVo.fpVo.FZLLFSSZLF = Function.Dec(p_dblMoney);
                                            ssamt = p_dblMoney;
                                            break;
                                        case "其他费":
                                            upVo.fpVo.FQTF = Function.Dec(p_dblMoney);
                                            break;
                                        case "护理费"://
                                            upVo.fpVo.FZHFWLHLF = Function.Dec(p_dblMoney);
                                            break;
                                        case "血费":
                                            upVo.fpVo.FXYLXF = Function.Dec(p_dblMoney);
                                            break;
                                        case "抗菌药物费用"://
                                            upVo.fpVo.FXYLGJF = Function.Dec(p_dblMoney);
                                            kjyamt = p_dblMoney;
                                            break;
                                        case "西药费"://
                                            upVo.fpVo.FXYF = Function.Dec(p_dblMoney);
                                            xyamt = p_dblMoney;
                                            break;
                                        case "中草药费":
                                            upVo.fpVo.FZCYF = Function.Dec(p_dblMoney);
                                            break;
                                        case "中成药费"://
                                            upVo.fpVo.FZCHYF = Function.Dec(p_dblMoney);
                                            break;
                                        case "一般医疗服务费"://
                                            upVo.fpVo.FZHFWLYLF = Function.Dec(p_dblMoney);
                                            break;
                                        case "一般治疗操作费"://
                                            upVo.fpVo.FZHFWLCZF = Function.Dec(p_dblMoney);
                                            break;
                                        case "其他费用":
                                            upVo.fpVo.FZHFWLQTF = Function.Dec(p_dblMoney);
                                            break;
                                        case "病理诊断费":
                                            upVo.fpVo.FZDLBLF = Function.Dec(p_dblMoney);
                                            break;
                                        case "实验室诊断费"://
                                            upVo.fpVo.FZDLSSSF = Function.Dec(p_dblMoney);
                                            break;
                                        case "影像学诊断费"://
                                            upVo.fpVo.FZDLYXF = Function.Dec(p_dblMoney);
                                            break;
                                        case "非手术治疗项目费"://
                                            fssxmamt = p_dblMoney;
                                            break;
                                        case "临床物理治疗费"://
                                            lcwlzlf = p_dblMoney;
                                            break;
                                        case "康复费":
                                            upVo.fpVo.FKFLKFF = Function.Dec(p_dblMoney);
                                            break;
                                        case "中医治疗费":
                                            upVo.fpVo.FZYLZF = Function.Dec(p_dblMoney);
                                            break;
                                        case "白蛋白类制品费":
                                            upVo.fpVo.FXYLBQBF = Function.Dec(p_dblMoney);
                                            break;
                                        case "球蛋白类制品费":
                                            upVo.fpVo.FXYLQDBF = Function.Dec(p_dblMoney);
                                            break;
                                        case "凝血因子类制品费":
                                            upVo.fpVo.FXYLYXYZF = Function.Dec(p_dblMoney);
                                            break;
                                        case "细胞因子类制品费":
                                            upVo.fpVo.FXYLXBYZF = Function.Dec(p_dblMoney);
                                            break;
                                        case "检查用一次性医用材料"://
                                            upVo.fpVo.FHCLCJF = Function.Dec(p_dblMoney);
                                            break;
                                        case "治疗用一次性医用材料费"://
                                            upVo.fpVo.FHCLZLF = Function.Dec(p_dblMoney);
                                            break;
                                        case "手术用一次性医用材料费"://
                                            upVo.fpVo.FHCLSSF = Function.Dec(p_dblMoney);
                                            break;
                                        default:
                                            break;
                                    }

                                    upVo.fpVo.FZLLFFSSF = Function.Dec(fssxmamt + lcwlzlf);
                                    upVo.fpVo.FZLLFWLZWLF = Function.Dec(lcwlzlf);
                                    upVo.fpVo.FZLLFSSF = Function.Dec(mzamt + ssamt + sszlamt);
                                }
                                upVo.fpVo.FSUM1 = sumMoney;
                            }

                            upVo.fpVo.FZFJE = GetSelfPay(registerid);
                            upVo.fpVo.GMSFHM = drPatient["fidcard"].ToString();
                            upVo.fpVo.FZYF = upVo.fpVo.FZCHYF + upVo.fpVo.FZCYF;
                            #endregion

                            #region 职业编号
                            string FJOB = drPatient["fjob"].ToString();
                            if (!string.IsNullOrEmpty(FJOB))
                            {
                                DataRow[] drD = dtDic.Select("FCODE='GBVOCATIONNEW' and fmc='" + FJOB + "'");
                                if (drD != null && drD.Length > 0)
                                {
                                    upVo.fpVo.FJOBBH = drD[0]["FBH"].ToString();
                                }
                            }

                            if (string.IsNullOrEmpty(upVo.fpVo.FJOBBH))
                                upVo.fpVo.FJOBBH = "90";
                            #endregion

                            #region 中医类
                            upVo.fpVo.FZHFWLYLF01 = 0;
                            upVo.fpVo.FZHFWLYLF02 = 0;
                            upVo.fpVo.FZYLZDF = 0;
                            upVo.fpVo.FZYLZLF = 0;
                            upVo.fpVo.FZYLZLF01 = 0;
                            upVo.fpVo.FZYLZLF02 = 0;
                            upVo.fpVo.FZYLZLF03 = 0;
                            upVo.fpVo.FZYLZLF04 = 0;
                            upVo.fpVo.FZYLZLF05 = 0;
                            upVo.fpVo.FZYLZLF06 = 0;
                            upVo.fpVo.FZYLQTF = 0;
                            upVo.fpVo.FZCLJGZJF = 0;
                            upVo.fpVo.FZYLQTF01 = 0;
                            upVo.fpVo.FZYLQTF02 = 0;
                            upVo.fpVo.FZYID = dr["inpatientid"].ToString();
                            #endregion

                            #region 住院号
                            upVo.fpVo.ZYH = ipno;
                            #endregion

                            #region 转科情况（数据集）
                            upVo.fpVo.lstZkVo = GetBrzkqk(lstItems, upVo.fpVo.FPRN);
                            #endregion

                            #region 数据集(病人诊断信息)
                            upVo.fpVo.lstZdVo = GetBrzdxx(dtbDs, dtbOutDiag, upVo.fpVo.FPRN);
                            #endregion

                            #region 数据集（肿瘤化疗记录）
                            upVo.fpVo.lstHlVo = GetZlhljlsj(dtbTumor, upVo.fpVo.FPRN);
                            #endregion

                            #region 数据集(病人手术信息)
                            upVo.fpVo.lstSsVo = GetBrssxx(dtbOP, upVo.fpVo.FPRN, upVo.fpVo.FNAME);
                            #endregion

                            #region 数据集（妇婴卡）
                            upVo.fpVo.lstFyVo = GetFyksj(dtbInfant, upVo.fpVo.FPRN, upVo.fpVo.FNAME);
                            #endregion

                            #region 数据集（肿瘤卡）
                            upVo.fpVo.lstZlVo = GetZlksj(dtbZlksjj, upVo.fpVo.FPRN);
                            #endregion

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetPatBaFromIcare-->" + ex.ToString());
            }
            finally
            {
                svc = null;
            }
            return upVo;
        }
        #endregion

        #region 病案首页 转科信息
        public List<EntityBrzkqk> GetBrzkqk(List<ListViewItem> lstItems, string fprn)
        {
            List<EntityBrzkqk> data = new List<EntityBrzkqk>();

            if (lstItems == null)
            {
                return null;
            }

            for (int iL = 1; iL < lstItems.Count; iL++)
            {
                EntityBrzkqk vo = new EntityBrzkqk();
                ListViewItem item = lstItems[0];
                vo.FPRN = fprn;
                vo.FZKTYKH = item.SubItems[3].ToString().Replace("{ListViewSubItem: {", "").Replace("}}", "");
                vo.FZKDEPT = item.SubItems[2].ToString().Replace("{ListViewSubItem: {", "").Replace("}}", "");
                vo.FZKDATE = Function.Datetime(item.SubItems[1].ToString().Replace("{ListViewSubItem: {", "").Replace("}}", "")).ToString("yyyy-MM-dd");
                vo.FZKTIME = Function.Datetime(item.SubItems[1].ToString().Replace("{ListViewSubItem: {", "").Replace("}}", "")).ToString("HH:mm:ss");
            }

            return data;
        }
        #endregion

        #region 病案首页 诊断信息
        public List<EntityBrzdxx> GetBrzdxx(DataTable dt, DataTable dtbOutDiag, string fprn)
        {
            if (dt == null)
                return null;

            List<EntityBrzdxx> data = new List<EntityBrzdxx>();
            DataRow drDS = dt.Rows[0];
            string outMainDiag = drDS["maindiagnosis"].ToString();
            string outMainDiagICD = drDS["icd_10ofmain"].ToString();
            int FRYBQBH = Function.Int(drDS["mainconditionseq"]);

            EntityBrzdxx voM = new EntityBrzdxx();
            voM.FJBNAME = outMainDiag;
            if (string.IsNullOrEmpty(voM.FJBNAME))
                voM.FJBNAME = "-";
            if (!string.IsNullOrEmpty(outMainDiagICD))
                voM.FICDM = outMainDiagICD;
            else
                voM.FICDM = "-";
            voM.FRYBQBH = (FRYBQBH + 1).ToString();
            if (voM.FRYBQBH == "1")
                voM.FRYBQ = "有";
            else if (voM.FRYBQBH == "2")
                voM.FRYBQ = "临床未确定";
            else if (voM.FRYBQBH == "3")
                voM.FRYBQ = "情况不明";
            else
            {
                voM.FRYBQBH = "4";
                voM.FRYBQ = "无";
            }

            voM.FZDLX = "1";
            voM.FICDVersion = "10";
            voM.FPRN = fprn;
            data.Add(voM);

            string PoisoningReson = drDS["SCACHESOURCE"].ToString().Trim();
            string PoisoningResonICD = drDS["SCACHESOURCEICD"].ToString();
            if(!string.IsNullOrEmpty(PoisoningReson))
            {
                EntityBrzdxx voP = new EntityBrzdxx();
                voP.FJBNAME = PoisoningReson;
                if (string.IsNullOrEmpty(voP.FJBNAME))
                    voP.FJBNAME = "-";
                if (!string.IsNullOrEmpty(PoisoningResonICD))
                    voP.FICDM = PoisoningResonICD;
                else
                    voP.FICDM = "-";
                voP.FRYBQBH = "-";
                voP.FRYBQ = "-";
                voP.FZDLX = "s";
                voP.FICDVersion = "10";
                voP.FPRN = fprn;
                data.Add(voP);
            }
           
            if (dtbOutDiag != null && dtbOutDiag.Rows.Count > 0)
            {
                foreach (DataRow dr in dtbOutDiag.Rows)
                {
                    EntityBrzdxx vo = new EntityBrzdxx();
                    vo.FPRN = fprn;
                    vo.FZDLX = "2";
                    vo.FICDM = dr["code"].ToString();
                    if (string.IsNullOrEmpty(vo.FICDM))
                        vo.FICDM = "-";
                    vo.FJBNAME = dr["name"].ToString().Trim();
                    vo.FRYBQ = dr["outinfo"].ToString();

                    if (vo.FRYBQ == "有")
                        vo.FRYBQBH = "1";
                    else if (vo.FRYBQ == "临床未确定")
                        vo.FRYBQBH = "2";
                    else if (vo.FRYBQ == "情况不明")
                        vo.FRYBQBH = "3";
                    else if (vo.FRYBQ == "无")
                        vo.FRYBQBH = "4";
                    else
                    {
                        vo.FRYBQBH = "4";
                        vo.FRYBQ = "无";
                    }
                    if(string.IsNullOrEmpty(vo.FJBNAME) )
                    {
                        continue;
                    }
                    vo.FICDVersion = "10";

                    data.Add(vo);
                }
            }

            return data;
        }
        #endregion

        #region 病案首页 肿瘤化疗记录
        public List<EntityZlhljlsj> GetZlhljlsj(DataTable dtbTumor, string fprn)
        {
            if (dtbTumor == null)
                return null;

            List<EntityZlhljlsj> data = new List<EntityZlhljlsj>();

            if (dtbTumor != null && dtbTumor.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtbTumor.Rows)
                {
                    EntityZlhljlsj vo = new EntityZlhljlsj();
                    vo.FHLRQ1 = Function.Datetime(drTemp["curedate"]).ToString("yyyyMMdd");

                    vo.FHLDRUG = drTemp["medicine"].ToString();
                    vo.FHLPROC = drTemp["treatment"].ToString();

                    if (string.IsNullOrEmpty(vo.FHLPROC))
                        vo.FHLPROC = "-";

                    if (drTemp["result"].ToString() == "CR")
                    {
                        vo.FHLLXBH = "1";
                    }
                    else if (drTemp["result"].ToString() == "PR")
                    {
                        vo.FHLLXBH = "2";
                    }
                    else if (drTemp["result"].ToString() == "MR")
                    {
                        vo.FHLLXBH = "3";
                    }
                    else if (drTemp["result"].ToString() == "S")
                    {
                        vo.FHLLXBH = "4";
                    }
                    else if (drTemp["result"].ToString() == "P")
                    {
                        vo.FHLLXBH = "5";
                    }
                    else if (drTemp["result"].ToString() == "NA")
                    {
                        vo.FHLLXBH = "6";
                    }

                    vo.FHLLX = drTemp["result"].ToString();
                    data.Add(vo);
                }
            }

            return data;
        }
        #endregion

        #region 病案首页 肿瘤卡
        public List<EntityZlksj> GetZlksj(DataTable dtbTumor, string fprn)
        {
            if (dtbTumor == null)
                return null;

            List<EntityZlksj> data = new List<EntityZlksj>();

            if (dtbTumor != null && dtbTumor.Rows.Count > 0)
            {
                int intTemp = 0;
                foreach (DataRow drInfo in dtbTumor.Rows)
                {
                    EntityZlksj vo = new EntityZlksj();
                    vo.FPRN = fprn;
                    intTemp = Function.Int(drInfo["RTMODESEQ"]);
                    if (intTemp == 0)
                    {
                        vo.FFLFSBH = "1";
                        vo.FFLFS = "根治性";
                    }
                    else if (intTemp == 1)
                    {
                        vo.FFLFSBH = "2";
                        vo.FFLFS = "姑息性";
                    }
                    else if (intTemp == 2)
                    {
                        vo.FFLFSBH = "3";
                        vo.FFLFS = "辅助性";
                    }



                    intTemp = Function.Int(drInfo["RTRULESEQ"]);
                    if (intTemp == 0)
                    {
                        vo.FFLCXBH = "1";
                        vo.FFLFS = "连续";
                    }
                    else if (intTemp == 1)
                    {
                        vo.FFLCXBH = "2";
                        vo.FFLFS = "间断";
                    }
                    else if (intTemp == 2)
                    {
                        vo.FFLCXBH = "3";
                        vo.FFLFS = "分段";
                    }

                    intTemp = Function.Int(drInfo["RTRULESEQ"]);
                    if (intTemp == 0)
                    {
                        vo.FFLCXBH = "1";
                        vo.FFLFS = "连续";
                    }
                    else if (intTemp == 1)
                    {
                        vo.FFLCXBH = "2";
                        vo.FFLFS = "间断";
                    }
                    else if (intTemp == 2)
                    {
                        vo.FFLCXBH = "3";
                        vo.FFLFS = "分段";
                    }

                    if (drInfo["RTCO"].ToString() == "1")
                    {
                        vo.FFLZZBH = "1";
                        vo.FFLZZ = "钴";
                    }
                    else if (drInfo["RTACCELERATOR"].ToString() == "1")
                    {
                        vo.FFLZZBH = "2";
                        vo.FFLZZ = "直加";
                    }
                    else if (drInfo["RTX_RAY"].ToString() == "1")
                    {
                        vo.FFLZZBH = "3";
                        vo.FFLZZ = "X线";
                    }
                    else if (drInfo["RTLACUNA"].ToString() == "1")
                    {
                        vo.FFLZZBH = "4";
                        vo.FFLZZ = "后装";
                    }

                    vo.FYJY = drInfo["ORIGINALDISEASEGY"].ToString();
                    vo.FYCS = drInfo["ORIGINALDISEASETIMES"].ToString();
                    vo.FYTS = drInfo["ORIGINALDISEASEDAYS"].ToString();
                    vo.FYRQ1 = Function.Datetime(drInfo["ORIGINALDISEASEBEGINDATE"]).ToString("yyyyMMdd");
                    vo.FYRQ2 = Function.Datetime(drInfo["ORIGINALDISEASEENDDATE"]).ToString("yyyyMMdd");

                    vo.FQJY = drInfo["LYMPHGY"].ToString();
                    vo.FQCS = drInfo["LYMPHTIMES"].ToString();
                    vo.FQTS = drInfo["LYMPHDAYS"].ToString();
                    vo.FQRQ1 = Function.Datetime(drInfo["LYMPHBEGINDATE"]).ToString("yyyyMMdd");
                    vo.FQRQ2 = Function.Datetime(drInfo["LYMPHENDDATE"]).ToString("yyyyMMdd");

                    vo.FZNAME = "-";
                    vo.FZJY = drInfo["METASTASISGY"].ToString();
                    vo.FZCS = drInfo["METASTASISTIMES"].ToString();
                    vo.FZTS = drInfo["METASTASISDAYS"].ToString();
                    vo.FZRQ1 = Function.Datetime(drInfo["METASTASISBEGINDATE"]).ToString("yyyyMMdd");
                    vo.FZRQ2 = Function.Datetime(drInfo["METASTASISENDDATE"]).ToString("yyyyMMdd");

                    if (drInfo["CHEMOTHERAPYWHOLEBODY"].ToString() == "1")
                    {
                        vo.FHLFSBH = "1";
                        vo.FHLFS = "根治性";
                    }

                    else if (drInfo["CHEMOTHERAPYINTUBATE"].ToString() == "1")
                    {
                        vo.FHLFSBH = "2";
                        vo.FHLFS = "姑息性";
                    }
                    else if (drInfo["CHEMOTHERAPYTHORAX"].ToString() == "1")
                    {
                        vo.FHLFSBH = "3";
                        vo.FHLFS = "新辅助性";
                    }
                    else if (drInfo["CHEMOTHERAPYABDOMEN"].ToString() == "1")
                    {
                        vo.FHLFSBH = "4";
                        vo.FHLFS = "辅助性";
                    }
                    else if (drInfo["CHEMOTHERAPYSPINAL"].ToString() == "1")
                    {
                        vo.FHLFSBH = "5";
                        vo.FHLFS = "新药试用";
                    }
                    else if (drInfo["CHEMOTHERAPYOTHER"].ToString() == "1")
                    {
                        vo.FHLFSBH = "6";
                        vo.FHLFS = "其他";
                    }


                    if (string.IsNullOrEmpty(vo.FHLFSBH) && string.IsNullOrEmpty(vo.FFLFSBH))
                    {
                        continue;
                    }

                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 病案首页 手术信息
        public List<EntityBrssxx> GetBrssxx(DataTable dtbOP, string fprn, string name)
        {
            if (dtbOP == null)
                return null;
            List<EntityBrssxx> data = new List<EntityBrssxx>();

            try
            {
                if (dtbOP != null && dtbOP.Rows.Count > 0)
                {
                    int intOpTimes = 0;
                    foreach (DataRow drTemp in dtbOP.Rows)
                    {
                        EntityBrssxx vo = new EntityBrssxx();
                        vo.FPRN = fprn;
                        vo.FNAME = name;
                        vo.FOPTIMES = (++intOpTimes).ToString();
                        vo.FOPCODE = drTemp["opcode"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPCODE))
                            vo.FOPCODE = "-";
                        vo.FOP = drTemp["opname"].ToString();
                        vo.FOPDATE = Function.Datetime(drTemp["opdate"]).ToString("yyyyMMdd");
                        #region 切口愈合情况
                        string[] strCut = drTemp["cutlevel"].ToString().Split('/');
                        if (strCut != null && strCut.Length == 2)
                        {
                            vo.FQIEKOU = strCut[0];
                            vo.FYUHE = strCut[1];
                            if (strCut[0] == "Ⅰ")
                            {
                                vo.FQIEKOUBH = "1";
                            }
                            else if (strCut[0] == "Ⅱ")
                            {
                                vo.FQIEKOUBH = "2";
                            }
                            else if (strCut[0] == "Ⅲ" || strCut[0] == "III")
                            {
                                vo.FQIEKOUBH = "3";
                            }
                            else
                            {
                                vo.FQIEKOUBH = "1";
                            }

                            if (strCut[1] == "甲")
                            {
                                vo.FYUHEBH = "1";
                            }
                            else if (strCut[1] == "乙")
                            {
                                vo.FYUHEBH = "2";
                            }
                            else if (strCut[1] == "丙")
                            {
                                vo.FYUHEBH = "3";
                            }
                            else
                            {
                                vo.FYUHEBH = "4";
                            }
                        }


                        if (string.IsNullOrEmpty(vo.FQIEKOUBH))
                            vo.FQIEKOUBH = "-";
                        if (string.IsNullOrEmpty(vo.FQIEKOU))
                            vo.FQIEKOU = "-";

                        if (string.IsNullOrEmpty(vo.FYUHEBH))
                            vo.FYUHEBH = "-";
                        if (string.IsNullOrEmpty(vo.FYUHE))
                            vo.FYUHE = "-";
                        #endregion
                        vo.FDOCBH = drTemp["opdoctorno"].ToString();
                        vo.FDOCNAME = drTemp["opdoctor"].ToString();
                        vo.FMAZUIBH = drTemp["anacode"].ToString();
                        if (string.IsNullOrEmpty(vo.FMAZUIBH))
                            vo.FMAZUIBH = "-";
                        vo.FMAZUI = drTemp["Ananame"].ToString();
                        if (string.IsNullOrEmpty(vo.FMAZUI))
                            vo.FMAZUI = "-";
                        if (vo.FMAZUI.Length > 15)
                            vo.FMAZUI = vo.FMAZUI.Substring(0,15);
                        vo.FIFFSOP = "1";
                        vo.FOPDOCT1BH = drTemp["firstassistno"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPDOCT1BH))
                            vo.FOPDOCT1BH = "-";
                        vo.FOPDOCT1 = drTemp["firstassist"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPDOCT1))
                            vo.FOPDOCT1 = "-";
                        vo.FOPDOCT2BH = drTemp["secondassistno"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPDOCT2BH))
                            vo.FOPDOCT2BH = "-";
                        vo.FOPDOCT2 = drTemp["secondassist"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPDOCT2))
                            vo.FOPDOCT2 = "-";
                        vo.FMZDOCTBH = drTemp["anadoctorno"].ToString();
                        if (string.IsNullOrEmpty(vo.FMZDOCTBH))
                            vo.FMZDOCTBH = "-";
                        vo.FMZDOCT = drTemp["anadoctor"].ToString();
                        if (string.IsNullOrEmpty(vo.FMZDOCT))
                            vo.FMZDOCT = "-";
                        if (drTemp["operationelective"].ToString() == "是")
                        {
                            vo.FZQSSBH = "1";
                            vo.FZQSS = "是";
                        }
                        else
                        {
                            vo.FZQSSBH = "0";
                            vo.FZQSS = "否";
                        }

                        if (drTemp["operationlevel"].ToString() == "一级手术")
                        {
                            vo.FSSJBBH = "1";
                            vo.FSSJB = "一级";
                        }
                        else if (drTemp["operationlevel"].ToString() == "二级手术")
                        {
                            vo.FSSJBBH = "2";
                            vo.FSSJB = "二级";
                        }
                        else if (drTemp["operationlevel"].ToString() == "三级手术")
                        {
                            vo.FSSJBBH = "3";
                            vo.FSSJB = "三级";
                        }
                        else if (drTemp["operationlevel"].ToString() == "四级手术")
                        {
                            vo.FSSJBBH = "4";
                            vo.FSSJB = "四级";
                        }
                        else
                        {
                            vo.FSSJBBH = "1";
                            vo.FSSJB = "一级";
                        }

                        vo.FOPKSNAME = "-";
                        vo.FOPTYKH = "-";
                        data.Add(vo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return data;
        }
        #endregion

        #region 病案首页 妇婴卡
        public List<EntityFyksj> GetFyksj(DataTable dtbInfant, string fprn, string name)
        {
            if (dtbInfant == null)
                return null;

            List<EntityFyksj> data = new List<EntityFyksj>();
            if (dtbInfant != null && dtbInfant.Rows.Count > 0)
            {
                int iRow = 0;
                foreach (DataRow drTemp in dtbInfant.Rows)
                {
                    EntityFyksj vo = new EntityFyksj();
                    vo.FPRN = fprn;
                    vo.FNAME = name;
                    vo.FBABYNUM = (iRow + 1).ToString();
                    iRow++;
                    if (drTemp["sex"].ToString() == "男")
                    {
                        vo.FBABYSEXBH = "1";
                    }
                    else if (drTemp["sex"].ToString() == "女")
                    {
                        vo.FBABYSEXBH = "2";
                    }
                    vo.FBABYSEX = drTemp["sex"].ToString();
                    vo.FTZ = drTemp["infantweight"].ToString();
                    if (drTemp["LaborResult"].ToString() == "活产")
                    {
                        vo.FRESULTBH = "1";
                    }
                    else if (drTemp["LaborResult"].ToString() == "死产")
                    {
                        vo.FRESULTBH = "2";
                    }
                    else if (drTemp["LaborResult"].ToString() == "死胎")
                    {
                        vo.FRESULTBH = "3";
                    }
                    vo.FRESULT = drTemp["LaborResult"].ToString();
                    if (drTemp["InfantResult"].ToString() == "死亡")
                    {
                        vo.FZGBH = "1";
                    }
                    else if (drTemp["InfantResult"].ToString() == "转科")
                    {
                        vo.FZGBH = "2";
                    }
                    else if (drTemp["InfantResult"].ToString() == "出院")
                    {
                        vo.FZGBH = "3";
                    }
                    vo.FZG = drTemp["InfantResult"].ToString();
                    vo.FBABYSUC = Function.Int(drTemp["rescuesucctimes"]).ToString();
                    if (drTemp["InfantBreath"].ToString() == "自然")
                    {
                        vo.FHXBH = "1";
                    }
                    else if (drTemp["InfantBreath"].ToString() == "Ⅰ度窒息")
                    {
                        vo.FHXBH = "2";
                    }
                    else if (drTemp["InfantBreath"].ToString() == "Ⅱ度窒息")
                    {
                        vo.FHXBH = "3";
                    }
                    vo.FHX = drTemp["InfantBreath"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 获取出院其他诊断
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strType"></param>
        /// <returns></returns>
        public DataTable GetDiagnosis(string p_strRegisterID, string p_strType)
        {
            DataTable p_dtbResult = null;
            try
            {
                string strSQL = @"select b.icd10 code, 
                                        b.diagnosis name, 
                                        b.result outinfo
                                          from inhospitalmainrecord a
                                         inner join inhospitalmainrecord_diagnosis b
                                            on a.inpatientid = b.inpatientid
                                           and a.inpatientdate = b.inpatientdate
                                           and a.opendate = b.opendate
                                         inner join t_bse_hisemr_relation r
                                            on r.emrinpatientid = a.inpatientid
                                           and r.emrinpatientdate = a.inpatientdate
                                         where a.status = 1
                                           and b.status = 1
                                           and b.diagnosistype = ?
                                           and r.registerid_chr = ?
                                         order by b.seqid";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] objDPArr = null;
                objDPArr = svc.CreateParm(2);
                objDPArr[0].Value = p_strType;
                objDPArr[1].Value = p_strRegisterID;
                p_dtbResult = svc.GetDataTable(strSQL, objDPArr);

            }
            catch (Exception objEx)
            {

            }
            return p_dtbResult;
        }
        #endregion

        #region 获取病案基本内容
        public DataTable GetPatinfo(string registerid)
        {
            SqlHelper svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);

                #region 
                string sql = @"select distinct le.outhospital_dat              fcydate,
                                                    le.outdeptid_chr,
                                                    gre2.ba_deptname                fcydept,
                                                    gre2.ba_deptnum                 fcytykh,
                                                    trin.ba_deptname                frydept,
                                                    re.state_int                    fryinfo,
                                                    red.lastname_vchr               fname,
                                                    red.sex_chr                     fsex,
                                                    red.birth_dat                   fbirthday,
                                                    red.birthplace_vchr             fbirthplace,
                                                    red.idcard_chr                  fidcard,
                                                    red.nationality_vchr            fcountry,
                                                    red.race_vchr                   fnationality,
                                                    red.nativeplace_vchr            fnative,
                                                    red.homeaddress_vchr            fcurraddr,
                                                    red.homephone_vchr              fcurrtele,
                                                    red.contactpersonpc_chr         fcurrpost,
                                                    red.occupation_vchr             fjob,
                                                    red.married_chr                 fstatus,
                                                    red.employer_vchr               fdwname,
                                                    red.officeaddress_vchr          fdwaddr,
                                                    red.officephone_vchr            fdwtele,
                                                    red.officepc_vchr               fdwpost,
                                                    red.residenceplace_vchr         fhkaddr,
                                                    red.homepc_chr                  fhkpost,
                                                    red.contactpersonfirstname_vchr flxname,
                                                    red.patientrelation_vchr        frelate,
                                                    red.contactpersonaddress_vchr   flxaddr,
                                                    red.contactpersonphone_vchr     flxtele,
                                                    red.insuranceid_vchr,
                                                    re.patientid_chr,
                                                    re.inpatientid_chr              fprn,
                                                    re.inpatient_dat                frydate,
                                                    re.inpatientcount_int           ftimes,
                                                    re.registerid_chr,
                                                    re.casedoctor_chr,
                                                    re.paytypeid_chr,
                                                    trin.ba_deptnum                 frytykh,
                                                    pa.patientsources_vchr,
                                                    pay.ba_paytypeid_chr
                                      from t_opr_bih_leave le
                                     inner join t_opr_bih_register re
                                        on re.registerid_chr = le.registerid_chr
                                       and re.status_int != '-1'
                                     inner join t_opr_bih_registerdetail red
                                        on red.registerid_chr = le.registerid_chr
                                     inner join t_bse_patient pa
                                        on pa.patientid_chr = re.patientid_chr
                                       and pa.status_int = 1
                                      left outer join t_emr_group_relation gre2
                                        on le.outdeptid_chr = gre2.groupid_chr
                                      left outer join (select tr4.registerid_chr,
                                                              tr4.doctorid_chr,
                                                              gre4.ba_deptnum,
                                                              gre4.ba_deptname
                                                         from t_opr_bih_transfer tr4, t_emr_group_relation gre4
                                                        where tr4.type_int = 5
                                                          and gre4.groupid_chr = tr4.targetdeptid_chr) trin
                                        on trin.registerid_chr = le.registerid_chr
                                      left outer join t_emr_paytype_relation pay
                                        on pay.paytypeid_chr = re.paytypeid_chr
                                     where le.status_int = 1
                                       and le.registerid_chr = ?
                                     order by le.outhospital_dat desc ";
                #endregion


                if (!string.IsNullOrEmpty(registerid))
                {
                    parm = svc.CreateParm(1);
                    parm[0].Value = registerid;
                    dt = svc.GetDataTable(sql, parm);
                }
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetPatinfo--" + e);
            }
            finally
            {
                svc = null;
            }
            return dt;
        }
        #endregion

        #region 获取病案首页病人入院信息
        public DataTable GetPatientInInfo(string p_strRegisterID)
        {
            DataTable p_dtbResult = null;
            try
            {
                string strSQL = @"select a.diagnosisxml,
                                           a.mzicd10,
                                           b.diagnosis,
                                           b.condictionwhenin,
                                           b.confirmdiagnosisdate,
                                           b.doctor,
                                           b.insurancenum,
                                           b.inhospitalway,
                                           b.condictionwhenin,
                                           b.path,
                                           b.newbabyweight,
                                           b.newbabyinhostpitalweight,
                                           b.modeofpayment
                                      from inhospitalmainrecord a
                                     inner join inhospitalmainrecord_content b
                                        on a.inpatientid = b.inpatientid
                                       and a.inpatientdate = b.inpatientdate
                                       and a.opendate = b.opendate
                                     inner join t_bse_hisemr_relation r
                                        on r.emrinpatientid = a.inpatientid
                                       and r.emrinpatientdate = a.inpatientdate
                                     where a.status = 1
                                       and b.status = 1
                                       and r.registerid_chr = ?";


                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] objDPArr = null;
                objDPArr = svc.CreateParm(1);
                objDPArr[0].Value = p_strRegisterID;

                p_dtbResult = svc.GetDataTable(strSQL, objDPArr);
            }
            catch (Exception objEx)
            {
            }
            return p_dtbResult;
        }
        #endregion

        #region  获取病案字典
        public DataTable GetGDCaseDICT()
        {
            SqlHelper svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            try
            {
                svc = new SqlHelper(EnumBiz.baDB);
                #region 
                string sql = @"select t.fmc, t.fcode, t.fbh, t.fzjc from tstandardmx t where t.fzf = 0";
                #endregion
                dt = svc.GetDataTable(sql, parm);

            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetGDCaseDICT--" + e);
            }
            finally
            {
                svc = null;
            }
            return dt;
        }
        #endregion

        #region  获取简要住院周转记录
        public DataTable GetTransferInfo(string registerid)
        {
            SqlHelper svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                #region 
                string sql = @"select d.deptname_vchr, gre1.ba_deptnum, t.modify_dat,t.type_int
                          from t_opr_bih_transfer t, t_emr_group_relation gre1, t_bse_deptdesc d
                         where t.targetdeptid_chr = gre1.groupid_chr
                           and t.targetdeptid_chr = d.deptid_chr
                           and t.registerid_chr = ?
                         order by t.modify_dat ";
                parm = svc.CreateParm(1);
                parm[0].Value = registerid;
                #endregion
                dt = svc.GetDataTable(sql, parm);

            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetTransferInfo--" + e);
            }
            finally
            {
                svc = null;
            }
            return dt;
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

                    if (lstUpVo[i].firstSource == 2)
                    {
                        continue;
                    }
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

                    #region//诊断
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

        #region  获取病案首页病人诊断信息
        public DataTable GetPatientDiagnosisInfo(string registerid)
        {
            SqlHelper svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                #region 
                string sql = @"select a.maindiagnosisxml,
                                       a.icd_10ofmainxml,
                                       a.pathologydiagnosisxml,
                                       a.scachesourcexml,
                                       '' scachesourceicdxml,
                                       a.sensitivexml,
                                       a.hbsagxml,
                                       a.hcv_abxml,
                                       a.hiv_abxml,
                                       a.accordwithouthospitalxml,
                                       a.accordinwithoutxml,
                                       a.accordbfoprwithafxml,
                                       a.accordclinicwithpathologyxml,
                                       a.accordradiatewithpathologyxml,
                                       a.salvetimesxml,
                                       a.salvesuccessxml,
                                       '' subsidiarydiagnosisxml,
                                       '' subsidiarydiagnosis,
                                       '' icdofsubsidiarydiagnosis,
                                       '' subsidiarydiagnosisseq,
                                       b.maindiagnosis,
                                       b.mainconditionseq,
                                       b.icd_10ofmain,
                                       b.pathologydiagnosis,
                                       b.scachesource,
                                       b.sszyj_jbbm scachesourceicd,
                                       b.sensitive,
                                       b.hbsag,
                                       b.hcv_ab,
                                       b.hiv_ab,
                                       b.accordwithouthospital,
                                       b.accordinwithout,
                                       b.accordbeforeoperationwithafter,
                                       b.accordclinicwithpathology,
                                       b.accordradiatewithpathology,
                                       b.salvetimes,
                                       b.salvesuccess,
                                       b.quality,
                                       b.qctime,
                                       b.directordt,
                                       b.subdirectordt,
                                       b.dt,
                                       b.inhospitaldt,
                                       b.attendinforadvancesstudydt,
                                       b.graduatestudentintern,
                                       b.intern,
                                       b.coder,
                                       b.qcdt,
                                       b.qcnurse,
                                       b.blzd_blh,
                                       b.blzd_jbbm,
                                       b.discharged_int,
                                       b.discharged_varh,
                                       b.readmitted31_int,
                                       b.readmitted31_varh,
                                       b.inrnssday,
                                       b.inrnsshour,
                                       b.inrnssmin,
                                       b.outrnssday,
                                       b.outrnsshour,
                                       b.outrnssmin
                                  from inhospitalmainrecord a
                                 inner join inhospitalmainrecord_content b
                                    on a.inpatientid = b.inpatientid
                                   and a.inpatientdate = b.inpatientdate
                                   and a.opendate = b.opendate
                                 inner join t_bse_hisemr_relation r
                                    on r.emrinpatientid = a.inpatientid
                                   and r.emrinpatientdate = a.inpatientdate
                                 where a.status = 1
                                   and b.status = 1
                                   and r.registerid_chr = ? ";
                parm = svc.CreateParm(1);
                parm[0].Value = registerid;
                #endregion
                dt = svc.GetDataTable(sql, parm);

            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetPatientDiagnosisInfo--" + e);
            }
            finally
            {
                svc = null;
            }
            return dt;
        }
        #endregion

        #region 获取肿瘤专科病人化疗疗记录
        public DataTable GetChemotherapyMedicine(string p_strRegisterID)
        {
            DataTable p_dtbResult = null;
            try
            {
                string strSQL = @"select b.chemotherapydate curedate,
                                           b.medicinename medicine,
                                           b.period treatment,
                                           b.field_cr,
                                           b.field_pr,
                                           b.field_mr,
                                           b.field_s,
                                           b.field_p,
                                           b.field_na
                                      from inhospitalmainrecord a, ihmainrecord_chemotherapy b,
                                           t_bse_hisemr_relation     r
                                     where a.inpatientid = b.inpatientid
                                       and a.inpatientdate = b.inpatientdate
                                       and a.status = 1
                                       and b.status = 1
                                       and a.opendate = b.opendate
                                       and a.inpatientid = r.emrinpatientid
                                       and a.inpatientdate = r.emrinpatientdate
                                       and r.registerid_chr = ?
                                     order by seqid";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] objDPArr = null;
                objDPArr = svc.CreateParm(1);
                objDPArr[0].Value = p_strRegisterID;

                p_dtbResult = svc.GetDataTable(strSQL, objDPArr);

                if (p_dtbResult != null)
                {
                    p_dtbResult.Columns.Add("result");
                    if (p_dtbResult.Rows.Count > 0)
                    {
                        DataRow drTemp = null;
                        for (int iRow = 0; iRow < p_dtbResult.Rows.Count; iRow++)
                        {
                            drTemp = p_dtbResult.Rows[iRow];
                            if (drTemp["FIELD_CR"].ToString() == "1")
                            {
                                drTemp["result"] = "CR";
                            }
                            else if (drTemp["FIELD_PR"].ToString() == "1")
                            {
                                drTemp["result"] = "PR";
                            }
                            else if (drTemp["FIELD_MR"].ToString() == "1")
                            {
                                drTemp["result"] = "MR";
                            }
                            else if (drTemp["FIELD_S"].ToString() == "1")
                            {
                                drTemp["result"] = "S";
                            }
                            else if (drTemp["FIELD_P"].ToString() == "1")
                            {
                                drTemp["result"] = "P";
                            }
                            else if (drTemp["FIELD_NA"].ToString() == "1")
                            {
                                drTemp["result"] = "NA";
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
            }
            return p_dtbResult;
        }
        #endregion

        #region 获取肿瘤专科病人治疗记录
        /// <summary>
        /// 获取肿瘤专科病人治疗记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">肿瘤专科病人治疗记录</param>
        /// <returns></returns>
        public DataTable GetChemotherapyInfo(string p_strRegisterID)
        {
            DataTable p_dtbResult = null;
            try
            {
                string strSQL = @"select a.originaldiseasegyxml,
                                           a.originaldiseasetimesxml,
                                           a.originaldiseasedaysxml,
                                           a.lymphgyxml,
                                           a.lymphtimesxml,
                                           a.lymphdaysxml,
                                           a.metastasisgyxml,
                                           a.metastasistimesxml,
                                           a.metastasisdaysxml,
                                           b.rtmodeseq,
                                           b.rtruleseq,
                                           b.rtco,
                                           b.rtaccelerator,
                                           b.rtx_ray,
                                           b.rtlacuna,
                                           b.originaldiseaseseq,
                                           b.originaldiseasegy,
                                           b.originaldiseasetimes,
                                           b.originaldiseasedays,
                                           b.originaldiseasebegindate,
                                           b.originaldiseaseenddate,
                                           b.lymphseq,
                                           b.lymphgy,
                                           b.lymphtimes,
                                           b.lymphdays,
                                           b.lymphbegindate,
                                           b.lymphenddate,
                                           b.metastasisgy,
                                           b.metastasistimes,
                                           b.metastasisdays,
                                           b.metastasisbegindate,
                                           b.metastasisenddate,
                                           b.chemotherapymodeseq,
                                           b.chemotherapywholebody,
                                           b.chemotherapylocal,
                                           b.chemotherapyintubate,
                                           b.chemotherapythorax,
                                           b.chemotherapyabdomen,
                                           b.chemotherapyspinal,
                                           b.chemotherapyothertry,
                                           b.chemotherapyother
                                      from inhospitalmainrecord a
                                     inner join inhospitalmainrecord_content b
                                        on a.inpatientid = b.inpatientid
                                       and a.inpatientdate = b.inpatientdate
                                       and a.opendate = b.opendate
                                     inner join t_bse_hisemr_relation r
                                        on r.emrinpatientid = a.inpatientid
                                       and r.emrinpatientdate = a.inpatientdate
                                     where a.status = 1
                                       and b.status = 1
                                       and r.registerid_chr = ?";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] objDPArr = null;
                objDPArr = svc.CreateParm(1);
                objDPArr[0].Value = p_strRegisterID;

                p_dtbResult = svc.GetDataTable(strSQL, objDPArr);
            }
            catch (Exception objEx)
            {
            }
            return p_dtbResult;
        }
        #endregion

        #region 病人手术信息
        public DataTable GetOperationInfo(string p_strRegisterID)
        {
            DataTable dtbOP = null;
            try
            {
                string strSQL = @"select b.operationid opcode,
                                           b.operationdate opdate,
                                           b.operationname opname,
                                           b.operator,
                                           b.assistant1,
                                           b.assistant2,
                                           b.aanaesthesiamodeid,
                                           b.cutlevel,
                                           b.anaesthetist,
                                           b.operationaanaesthesiamodename ananame,
                                           b.operationanaesthetistname anadoctor,
                                           '' cutlevelid,
                                           b.operationlevel,
                                           b.operationelective,
                                           e1.lastname_vchr opdoctor,
                                           e1.empno_chr opdoctorno,
                                           e2.lastname_vchr firstassist,
                                           e2.empno_chr firstassistno,
                                           e3.lastname_vchr secondassist,
                                           e3.empno_chr secondassistno,
                                           e4.empno_chr anadoctorno
                                      from inhospitalmainrecord a
                                     inner join inhospitalmainrecord_operation b
                                        on a.inpatientid = b.inpatientid
                                       and a.inpatientdate = b.inpatientdate
                                     inner join t_bse_hisemr_relation r
                                        on r.emrinpatientid = a.inpatientid
                                       and r.emrinpatientdate = a.inpatientdate
                                      left outer join t_bse_employee e1
                                        on b.operator = e1.empid_chr
                                       and e1.status_int = 1
                                      left outer join t_bse_employee e2
                                        on b.assistant1 = e2.empid_chr
                                       and e2.status_int = 1
                                      left outer join t_bse_employee e3
                                        on b.assistant2 = e3.empid_chr
                                       and e3.status_int = 1
                                      left outer join t_bse_employee e4
                                        on b.anaesthetist = e4.empid_chr
                                       and e4.status_int = 1
                                     where a.status = 1
                                       and b.status = 1
                                       and r.registerid_chr = ?
                                     order by b.seqid ";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] objDPArr = null;
                objDPArr = svc.CreateParm(1);
                objDPArr[0].Value = p_strRegisterID;
                dtbOP = svc.GetDataTable(strSQL, objDPArr);

                if (dtbOP != null)
                {
                    dtbOP.Columns.Add("anacode");
                    if (dtbOP.Rows.Count > 0)
                    {
                        DataRow drTemp = null;
                        int intRowsCount = dtbOP.Rows.Count;
                        for (int iRow = 0; iRow < intRowsCount; iRow++)
                        {
                            drTemp = dtbOP.Rows[iRow];
                            if (drTemp["operationlevel"].ToString() == "一级手术")
                            {
                                drTemp["operationlevel"] = "一级";
                            }
                            else if (drTemp["operationlevel"].ToString() == "二级手术")
                            {
                                drTemp["operationlevel"] = "二级";
                            }
                            else if (drTemp["operationlevel"].ToString() == "三级手术")
                            {
                                drTemp["operationlevel"] = "三级";
                            }
                            else if (drTemp["operationlevel"].ToString() == "四级手术")
                            {
                                drTemp["operationlevel"] = "四级";
                            }
                            drTemp["operationelective"] = drTemp["operationelective"].ToString().Trim();
                            if (drTemp["operationelective"].ToString() != "是" && drTemp["operationelective"].ToString() != "否")
                            {
                                drTemp["operationelective"] = "否";
                            }
                        }
                    }
                }

            }
            catch (Exception objEx)
            {
            }
            return dtbOP;
        }
        #endregion

        #region 产科分娩婴儿记录
        public DataTable LaborInfo(string p_strRegisterID)
        {
            DataTable dtbLabor = null;
            try
            {
                string strSQL = @"select b.male,
                                           b.female,
                                           b.liveborn,
                                           b.dieborn,
                                           b.dienotborn,
                                           b.weight infantweight,
                                           b.die,
                                           b.changedepartment,
                                           b.outhospital,
                                           b.suffocate2,
                                           b.naturalcondiction,
                                           b.suffocate1,
                                           b.infectiontimes,
                                           b.infectionname name,
                                           b.icd10 code,
                                           b.salvetimes rescuetimes,
                                           b.salvesuccesstimes rescuesucctimes,
                                           b.seqid
                                      from inhospitalmainrecord      a,
                                           inhospitalmainrecord_baby b,
                                           t_bse_hisemr_relation     r
                                     where a.inpatientid = b.inpatientid
                                       and a.inpatientdate = b.inpatientdate
                                       and a.status = 1
                                       and b.status = 1
                                       and a.inpatientid = r.emrinpatientid
                                       and a.inpatientdate = r.emrinpatientdate
                                       and r.registerid_chr = ?
                                     order by b.seqid";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] objDPArr = null;
                objDPArr = svc.CreateParm(1);
                objDPArr[0].Value = p_strRegisterID;

                dtbLabor = svc.GetDataTable(strSQL, objDPArr);

                if (dtbLabor != null)
                {
                    dtbLabor.Columns.Add("sex");
                    dtbLabor.Columns.Add("LaborResult");
                    dtbLabor.Columns.Add("InfantResult");
                    dtbLabor.Columns.Add("InfantBreath");

                    if (dtbLabor.Rows.Count > 0)
                    {
                        DataRow drTemp = null;
                        int intRowsCount = dtbLabor.Rows.Count;
                        for (int iRow = 0; iRow < intRowsCount; iRow++)
                        {
                            drTemp = dtbLabor.Rows[iRow];
                            drTemp["seqid"] = Convert.ToInt32(drTemp["seqid"]) + 1;
                            if (drTemp["male"].ToString() == "1")
                            {
                                drTemp["sex"] = "男";
                            }
                            else if (drTemp["female"].ToString() == "1")
                            {
                                drTemp["sex"] = "女";
                            }
                            if (drTemp["liveborn"].ToString() == "1")
                            {
                                drTemp["LaborResult"] = "活产";
                            }
                            else if (drTemp["dieborn"].ToString() == "1")
                            {
                                drTemp["LaborResult"] = "死产";
                            }
                            else if (drTemp["dienotborn"].ToString() == "1")
                            {
                                drTemp["LaborResult"] = "死胎";
                            }
                            if (drTemp["die"].ToString() == "1")
                            {
                                drTemp["InfantResult"] = "死亡";
                            }
                            else if (drTemp["changedepartment"].ToString() == "1")
                            {
                                drTemp["InfantResult"] = "转科";
                            }
                            else if (drTemp["outhospital"].ToString() == "1")
                            {
                                drTemp["InfantResult"] = "出院";
                            }
                            if (drTemp["NATURALCONDICTION"].ToString() == "1")
                            {
                                drTemp["InfantBreath"] = "自然";
                            }
                            else if (drTemp["suffocate1"].ToString() == "1")
                            {
                                drTemp["InfantBreath"] = "Ⅰ度窒息";
                            }
                            else if (drTemp["suffocate2"].ToString() == "1")
                            {
                                drTemp["InfantBreath"] = "Ⅱ度窒息";
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
            }
            return dtbLabor;
        }
        #endregion

        #region 同步费用信息
        /// <summary>
        /// 同步费用信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long GetCHRCATE(string p_strRegisterID, out clsInHospitalMainCharge[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"select sum(k.tolfee_mny) tolfee_mny, k.groupname_chr
                                      from (select (round(b.amount_dec * b.unitprice_dec, 2) +
                                                   round(nvl(b.totaldiffcostmoney_dec, 0), 2)) as tolfee_mny,
                                                   c.itembihctype_chr,
                                                   d.typename_vchr groupname_chr
                                              from t_opr_bih_patientcharge b,
                                                   t_bse_chargeitem        c,
                                                   t_bse_chargeitemextype  d
                                             where b.chargeitemid_chr = c.itemid_chr
                                               and b.status_int = 1
                                               and b.pstatus_int > 0
                                               and c.itembihctype_chr = d.typeid_chr
                                               and d.flag_int = 5
                                               and b.registerid_chr = ?) k
                                     group by k.groupname_chr";

                SqlHelper objHRPServ = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] objDPArr = null;
                objDPArr = objHRPServ.CreateParm(1);
                objDPArr[0].Value = p_strRegisterID;

                DataTable dtbResult = null;
                dtbResult = objHRPServ.GetDataTable(strSQL, objDPArr);

                if (dtbResult.Rows.Count > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount <= 0)
                    {
                        return 1;
                    }

                    DataRow drCurrent = null;
                    p_objRecordArr = new clsInHospitalMainCharge[intRowsCount];
                    double dblTemp = 0D;
                    for (int i = 0; i < intRowsCount; i++)
                    {
                        p_objRecordArr[i] = new clsInHospitalMainCharge();
                        drCurrent = dtbResult.Rows[i];
                        p_objRecordArr[i].m_strRegisterID = p_strRegisterID;
                        if (double.TryParse(drCurrent["tolfee_mny"].ToString(), out dblTemp))
                        {
                            p_objRecordArr[i].m_dblMoney = dblTemp;
                        }
                        else
                        {
                            p_objRecordArr[i].m_dblMoney = 0.00D;
                        }
                        p_objRecordArr[i].m_strTypeName = drCurrent["groupname_chr"].ToString();
                    }

                    lngRes = 1;
                }
            }
            catch (Exception objEx)
            {
                lngRes = -1;
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 自付费用 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <returns></returns>
        public decimal GetSelfPay(string p_strRegisterID)
        {
            decimal m_strSelfamt = 0;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }
            try
            {
                string strSQL = @"select sum(t.sbsum_mny) sbsum_mny
                                  from t_opr_bih_charge t, t_Opr_Bih_Register t2
                                 where t.registerid_chr = t2.registerid_chr
                                   and t2.registerid_chr = ?";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] objDPArr = null;
                svc.CreateParm(1);
                objDPArr[0].Value = p_strRegisterID;
                DataTable dtbValue = new DataTable();
                dtbValue = svc.GetDataTable(strSQL, objDPArr);

                if (dtbValue.Rows.Count > 0 && dtbValue.Rows.Count > 0)
                {
                    m_strSelfamt = Function.Dec(dtbValue.Rows[0]["sbsum_mny"]);
                }
            }
            catch (Exception objEx)
            {

            }
            //返回
            return m_strSelfamt;
        }
        #endregion

        #region  根据ID查询员工
        public string GetEmpByID(string employeeid)
        {
            SqlHelper svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            string name = string.Empty;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                #region 
                string sql = @" select t.empno_chr,
                                       t.lastname_vchr,
                                       t.technicalrank_chr,
                                       t.pycode_chr,
                                       t.empid_chr,
                                       t.psw_chr,
                                       t.digitalsign_dta,
                                       t.technicallevel_chr
                                  from t_bse_employee t
                                 where t.status_int <> -1
                                   and t.empid_chr = ?
                                 order by t.isemployee_int desc, t.empid_chr desc";
                parm = svc.CreateParm(1);
                parm[0].Value = employeeid;
                #endregion
                dt = svc.GetDataTable(sql, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    name = dt.Rows[0]["lastname_vchr"].ToString();
                }
                else
                {
                    name = "";
                }

            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetEmpByID--" + e);
            }
            finally
            {
                svc = null;
            }
            return name;
        }
        #endregion

        #region 保存首页上传信息
        /// <summary>
        /// 保存首页上传信息
        /// </summary>
        /// <param name="lstVo"></param> 0 
        /// <returns></returns>
        public int SavePatFirstPage(List<EntityPatUpload> lstVo, int type = 0)
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
                        string sql = @"select * from t_upload where jzjlh = '" + item.JZJLH + "'";
                        DataTable dt = svc.GetDataTable(sql);
                        if (type == 0)
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                item.UPLOADDATE = DateTime.Now;
                                Sql = @"update t_upload set status = ?, UPLOADDATE = ? ,first = ?, firstMsg = ?,firstSource= ? where jzjlh = ?";
                                if (item.Issucess == 1)
                                {
                                    IDataParameter[] parm = svc.CreateParm(6);
                                    parm[0].Value = 1;
                                    parm[1].Value = item.UPLOADDATE;
                                    parm[2].Value = 0;
                                    parm[3].Value = "";
                                    parm[4].Value = item.firstSource;
                                    parm[5].Value = item.JZJLH;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                                else if (item.Issucess == -1)
                                {
                                    if (item.STATUS == 1&& item.first == 1)
                                        continue;
                                    IDataParameter[] parm = svc.CreateParm(6);
                                    parm[0].Value = "";
                                    parm[1].Value = item.UPLOADDATE;
                                    parm[2].Value = -1;
                                    parm[3].Value = item.FailMsg;
                                    parm[4].Value = item.firstSource;
                                    parm[5].Value = item.JZJLH;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                            }
                            else
                            {
                                if (CheckSequence(svc, "t_upload") > 0)
                                    serNo = Function.Dec(GetNextID(svc, "t_upload").ToString());
                                item.SERNO = serNo;
                                item.UPLOADDATE = DateTime.Now;
                                item.RECORDDDATE = DateTime.Now;
                                item.OPERCODE = item.JBR;
                                if (item.Issucess == -1)
                                {
                                    item.first = -1;
                                    item.firstMsg = item.FailMsg;
                                }
                                else
                                {
                                    item.STATUS = 1;
                                }

                                lstVo1.Add(item);
                            }
                        }
                        else if (type == 1)
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                item.UPLOADDATE = DateTime.Now;
                                Sql = @"update t_upload set UPLOADDATE = ? ,xj = ?, xjMsg = ? where jzjlh = ?";
                                if (item.Issucess == 1)
                                {
                                    IDataParameter[] parm = svc.CreateParm(4);
                                    parm[0].Value = item.UPLOADDATE;
                                    parm[1].Value = 0;
                                    parm[2].Value = "";
                                    parm[3].Value = item.JZJLH;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                                else if (item.Issucess == -1)
                                {
                                    if (item.STATUS == 1 && item.xj == 1)
                                        continue;
                                    IDataParameter[] parm = svc.CreateParm(4);
                                    parm[0].Value = item.UPLOADDATE;
                                    parm[1].Value = -1;
                                    parm[2].Value = item.FailMsg;
                                    parm[3].Value = item.JZJLH;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                            }
                            else
                            {
                                //if (CheckSequence(svc, "t_upload") > 0)
                                //    serNo = Function.Dec(GetNextID(svc, "t_upload").ToString());
                                //item.SERNO = serNo;
                                //item.STATUS = 1;
                                //item.UPLOADDATE = DateTime.Now;
                                //item.RECORDDDATE = DateTime.Now;
                                //item.OPERCODE = item.JBR;
                                //if (item.Issucess == -1)
                                //{
                                //    item.first = -1;
                                //    item.firstMsg = item.FailMsg;
                                //}

                                //lstVo1.Add(item);
                            }
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

        #region  获取上传失败信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dicParm"></param>
        /// <returns></returns>
        public List<EntityPatUpload> GetFailPatList()
        {
            List<EntityPatUpload> data = new List<EntityPatUpload>();
            SqlHelper svc = null;

            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);

                string Sql1 = @"select SERNO,
                                        JZJLH,
                                        OUTHOSPITALDATE,
                                        REGISTERID,
                                        INPATIENTDATE,
                                        RECORDDDATE,
                                        INPATIENTID,
                                        PATSEX,
                                        PATNAME,
                                        first,
                                        xj,
                                        firstMsg,
                                        xjMsg from t_upload where first = -1 or xj= -1 ";
                DataTable dt = svc.GetDataTable(Sql1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityPatUpload vo = new EntityPatUpload();
                        vo.JZJLH = dr["JZJLH"].ToString();
                        vo.INPATIENTID = dr["INPATIENTID"].ToString();
                        vo.PATNAME = dr["PATNAME"].ToString();
                        vo.PATSEX = dr["PATSEX"].ToString();
                        vo.RYSJ = dr["INPATIENTDATE"].ToString();
                        vo.CYSJ = dr["OUTHOSPITALDATE"].ToString();
                        vo.firstMsg = dr["firstMsg"].ToString();
                        vo.xjMsg = dr["xjMsg"].ToString();
                        data.Add(vo);
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }

            return data;
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

        #region 计算年龄
        /// <summary>
        /// 生日转换为年龄
        /// </summary>
        /// <param name="date"></param>
        /// <param name="yearOnly">只转换成周岁</param>
        /// <returns>不足一岁用月份表示，如6M</returns>
        public string CalcAge(DateTime? date, DateTime? inhospitalDate)
        {
            if (date == null)
                return string.Empty;

            DateTime beginDateTime = Function.Datetime(date);
            DateTime endDateTime = Function.Datetime(inhospitalDate);
            if (beginDateTime > endDateTime)
            {
                return "";
            }

            /*计算出生日期到当前日期总月数*/
            int months = endDateTime.Month - beginDateTime.Month + 12 * (endDateTime.Year - beginDateTime.Year);
            /*出生日期加总月数后，如果大于当前日期则减一个月*/
            int totalMonth = (beginDateTime.AddMonths(months) > endDateTime) ? months - 1 : months;
            if (totalMonth >= 12)
            {
                /*计算整年*/
                int fullYear = totalMonth / 12;
                int month = totalMonth % 12;
                if (month > 0)
                    return fullYear + "岁";
                else
                    return fullYear + "岁" + month + "月";
            }
            else
            {
                return totalMonth + "月";
            }

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
