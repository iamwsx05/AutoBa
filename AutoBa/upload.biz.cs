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
            bool isFromIcare = false;
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
                                dd.status,
                                dd.first,
                                dd.xj,
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
                //SqlJs = @"select distinct a.registerid_chr, a.jzjlh, a.invoiceno_vchr, b.inpatientid_chr,c.status,c.firstSource
                //                  from t_ins_chargezy_csyb a
                //                  left join t_opr_bih_register b
                //                    on a.registerid_chr = b.registerid_chr
                //                    left join t_upload c
                //                        on a.registerid_chr = c.registerid 
                //                 where(a.createtime between to_date(?, 'yyyy-mm-dd hh24:mi:ss') 
                //                    and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))  ";

                //SqlJs = @"select distinct a.registerid_chr,
                //                            e.jzjlh,
                //                            d.invoiceno_vchr,
                //                            b.inpatientid_chr,
                //                            c.status,
                //                            c.firstSource
                //              from  t_opr_bih_charge a
                //              left join t_opr_bih_register b
                //                on a.registerid_chr = b.registerid_chr
                //                left join t_opr_bih_chargedefinv d
                //                on a.chargeno_chr = d.chargeno_chr
                //                left join t_ins_chargezy_csyb e
                //                on a.registerid_chr = e.registerid_chr
                //              left join t_upload c
                //                on a.registerid_chr = c.registerid
                //            where a.class_int = 2
                //             and (a.operdate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                //                  to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";

                SqlJs = @"select distinct a.registerid_chr,
                                            e.jzjlh,
                                            d.invoiceno_vchr,
                                            b.inpatientid_chr,
                                            c.status,
                                            c.firstSource
                              from  t_opr_bih_charge a
                              left join t_opr_bih_register b
                                on a.registerid_chr = b.registerid_chr
                                left join t_opr_bih_chargedefinv d
                                on a.chargeno_chr = d.chargeno_chr
                                left join t_ins_chargezy_csyb e
                                on a.registerid_chr = e.registerid_chr
                              left join t_upload c
                                on a.registerid_chr = c.registerid
                            where a.class_int = 2
                             and (a.operdate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                  to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";
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
                            strSubJs += " and b.inpatientid_chr = '" + keyValue + "'";
                            break;
                        case "JZJLH":
                            strSubJs += " and e.jzjlh = '" + keyValue + "'";
                            break;
                        case "JZJLH1":
                            strSubJs += " and e.jzjlh in " + keyValue;
                            break;
                        case "chkStat":
                            isUploadparm = true;
                            break;
                        case "chkShowYb":
                            strSubJs += " and (e.jzjlh is not null or e.jzjlh <> '') ";
                            break;
                        case "isFromIcare":
                            isFromIcare = true;
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


                if (dtJs != null && dtJs.Rows.Count > 0)
                {
                    string ipnoStr = string.Empty;
                    string registeridStr = string.Empty;
                    List<string> lstReg = new List<string>();
                    List<string> lstIpno = new List<string>();
                    List<EntityFirstPage> lstFpJh = new List<EntityFirstPage>();
                    DataTable dtBa = null;
                    DataTable dtReg = null;
                    foreach (DataRow drJs in dtJs.Rows)
                    {
                        string registerid = drJs["registerid_chr"].ToString();
                        string ipno = drJs["inpatientid_chr"].ToString();
                        int uploadStatus = Function.Int(drJs["status"]);
                        int firstSource = Function.Int(drJs["firstSource"]);
                        //未上传，来源icare也属于未上传
                        if (isUploadparm)
                        {
                            if (uploadStatus == 1 && firstSource == 1)//uploadStatus 1 已上传 1 病案 
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

                        SqlReg += "and t1.registerid_chr in (" + registeridStr + ")" + Environment.NewLine + " order by emrinpatientdate";
                        dtReg = svc.GetDataTable(SqlReg);
                        lstFpJh = GetPatBaFromJH(registeridStr);
                    }
                    string emrIpnoStr = string.Empty;
                    string beginEmrDate = string.Empty;
                    string endEmrDate = string.Empty;
                    if (dtReg != null && dtReg.Rows.Count > 0)
                    {
                        beginEmrDate = Function.Datetime(dtReg.Rows[0]["emrinpatientdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        endEmrDate = Function.Datetime(dtReg.Rows[dtReg.Rows.Count - 1]["emrinpatientdate"]).ToString("yyyy-MM-dd HH:mm:ss");


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
                            TimeSpan ts = Function.Datetime(cydate) - Function.Datetime(rydate);
                            string FDAYS = ts.Days.ToString();
                            string uploadStatus = drReg["status"].ToString();
                            string jzjlh = string.Empty;
                            string FPHM = string.Empty;
                            EntityPatUpload upVo = null;

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

                            #region 首页信息
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
                                        upVo = new EntityPatUpload();
                                        upVo.fpVo = new EntityFirstPage();
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
                                        upVo.fpVo.FRYTJ = drrBa["FRYTJ"].ToString();
                                        upVo.fpVo.FRYTJBH = drrBa["FRYTJBH"].ToString().Trim();
                                        if (upVo.fpVo.FRYTJBH.Contains("门诊"))
                                        {
                                            upVo.fpVo.FRYTJBH = "2";
                                            upVo.fpVo.FRYTJ = "门诊";
                                        }
                                        if (upVo.fpVo.FRYTJBH.Contains("急诊"))
                                        {
                                            upVo.fpVo.FRYTJBH = "1";
                                            upVo.fpVo.FRYTJ = "急诊";
                                        }
                                        if (upVo.fpVo.FRYTJBH.Contains("其他医疗机构转入"))
                                        {
                                            upVo.fpVo.FRYTJBH = "3";
                                            upVo.fpVo.FRYTJ = "其他医疗机构转入";
                                        }

                                        if (upVo.fpVo.FRYTJBH.Contains("其他"))
                                        {
                                            upVo.fpVo.FRYTJBH = "9";
                                            upVo.fpVo.FRYTJ = "其他";
                                        }
                                        if (upVo.fpVo.FRYTJBH == "")
                                            upVo.fpVo.FRYTJBH = "9";
                                        if (upVo.fpVo.FRYTJ == "")
                                            upVo.fpVo.FRYTJ = "-";
                                        upVo.fpVo.FRYDATE = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                        upVo.fpVo.FRYTIME = drrBa["FRYTIME"].ToString();
                                        upVo.fpVo.FRYTIME = Function.Datetime(drrBa["FRYTIME"]).ToString("HH:mm:ss");
                                        upVo.fpVo.FRYTYKH = drrBa["FRYTYKH"].ToString();
                                        upVo.fpVo.FRYDEPT = drrBa["FRYDEPT"].ToString();
                                        upVo.fpVo.FRYBS = drrBa["FRYBS"].ToString().Trim();
                                        if (upVo.fpVo.FRYBS == "")
                                            upVo.fpVo.FRYBS = upVo.fpVo.FRYDEPT;
                                        upVo.fpVo.FZKTYKH = drrBa["FZKTYKH"].ToString();
                                        if (string.IsNullOrEmpty(upVo.fpVo.FZKTYKH))
                                            upVo.fpVo.FZKTYKH = "-";
                                        upVo.fpVo.FZKDEPT = drrBa["FZKDEPT"].ToString();
                                        if (string.IsNullOrEmpty(upVo.fpVo.FZKDEPT))
                                            upVo.fpVo.FZKDEPT = "-";
                                        upVo.fpVo.FZKTIME = drrBa["FZKTIME"].ToString();
                                        upVo.fpVo.FZKTIME = Function.Datetime(drrBa["FZKTIME"]).ToString("HH:mm:ss");
                                        upVo.fpVo.FCYDATE = Function.Datetime(drrBa["FCYDATE"]).ToString("yyyy-MM-dd");

                                        upVo.fpVo.FCYTIME = drrBa["FCYTIME"].ToString();
                                        upVo.fpVo.FCYTIME = Function.Datetime(drrBa["FCYTIME"]).ToString("HH:mm:ss");
                                        upVo.fpVo.FCYTYKH = drrBa["FCYTYKH"].ToString();
                                        upVo.fpVo.FCYDEPT = drrBa["FCYDEPT"].ToString();
                                        upVo.fpVo.FCYBS = drrBa["FCYBS"].ToString().Trim();
                                        if (upVo.fpVo.FCYBS == "")
                                            upVo.fpVo.FCYBS = upVo.fpVo.FCYDEPT;
                                        TimeSpan tsDay = Function.Datetime(upVo.fpVo.FCYDATE) - Function.Datetime(upVo.fpVo.FRYDATE);
                                        upVo.fpVo.FDAYS = tsDay.Days.ToString();
                                        if (upVo.fpVo.FDAYS == "0")
                                            upVo.fpVo.FDAYS = "1";
                                        upVo.fpVo.FMZZDBH = drrBa["FMZZDBH"].ToString();
                                        if (string.IsNullOrEmpty(upVo.fpVo.FMZZDBH))
                                            upVo.fpVo.FMZZDBH = "-";
                                        upVo.fpVo.FMZZD = drrBa["FMZZD"].ToString();
                                        if (string.IsNullOrEmpty(upVo.fpVo.FMZZD))
                                            upVo.fpVo.FMZZD = "-";
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
                                        if (string.IsNullOrEmpty(upVo.fpVo.FKZRBH))
                                            upVo.fpVo.FKZRBH = "-";
                                        upVo.fpVo.FKZR = drrBa["FKZR"].ToString();
                                        if (string.IsNullOrEmpty(upVo.fpVo.FKZR))
                                            upVo.fpVo.FKZR = "-";
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
                                EntityPatUpload vo = null;
                                EntityFirstPage firstVo = null;
                                if (lstFpJh != null)
                                {
                                    firstVo = lstFpJh.Find(r => r.registerId == registerid);
                                    if(firstVo!=null)
                                    {
                                        //首页信息不全
                                        if (string.IsNullOrEmpty(firstVo.FKZR))
                                            firstVo = null;
                                    }
                                    
                                    //勾选 首页来源icare
                                    if (isFromIcare)
                                        firstVo = null;
                                }
                                    
                                if (firstVo != null)
                                {
                                    upVo = new EntityPatUpload();
                                    upVo.fpVo = new EntityFirstPage();
                                    upVo.fpVo = firstVo;
                                    if( string.IsNullOrEmpty(upVo.fpVo.FIDCard))
                                        upVo.fpVo.FIDCard = drReg["idcard_chr"].ToString();
                                    if( string.IsNullOrEmpty(upVo.fpVo.GMSFHM))
                                        upVo.fpVo.GMSFHM = drReg["idcard_chr"].ToString();
                                    if (Function.Int(upVo.fpVo.FDAYS) < 0)
                                        upVo.fpVo.FDAYS = FDAYS;
                                    upVo.fpVo.ZYH = emrinpatientid;
                                    upVo.fpVo.FPHM = FPHM;
                                    upVo.fpVo.JZJLH = jzjlh;
                                    upVo.fpVo.FWSJGDM = "12441900457226325L";
                                    upVo.firstSource = 3;
                                }
                                else
                                {
                                    vo = GetPatBaFromIcare(ipno, registerid, emrinpatientdate);
                                    if (vo != null)
                                    {
                                        upVo = new EntityPatUpload();
                                        upVo.fpVo = new EntityFirstPage();
                                        upVo.fpVo = vo.fpVo;
                                        upVo.fpVo.ZYH = emrinpatientid;
                                        if (Function.Int(upVo.fpVo.FDAYS) < 0)
                                            upVo.fpVo.FDAYS = FDAYS;
                                        upVo.fpVo.FPHM = FPHM;
                                        upVo.fpVo.JZJLH = jzjlh;
                                        upVo.fpVo.FWSJGDM = "12441900457226325L";
                                        upVo.firstSource = 2;
                                    }
                                }
                            }

                            if (upVo == null)
                            {
                                upVo = new EntityPatUpload();
                                upVo.fpVo = new EntityFirstPage();
                                upVo.firstSource = -1;
                            }
                            #endregion
                            
                            #region  显示列表

                            upVo.XH = ++n;
                            upVo.MZH = MZH;
                            upVo.UPLOADTYPE = 1;
                            upVo.PATNAME = drReg["xm"].ToString();
                            upVo.PATSEX = drReg["sex"].ToString();
                            upVo.IDCARD = drReg["idcard_chr"].ToString();
                            upVo.INPATIENTID = drReg["ipno"].ToString();
                            upVo.emrinpatientId = drReg["emrinpatientid"].ToString();
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
                            if (drReg["first"].ToString() == "0")
                                upVo.firstSz = "已上传";
                            else
                                upVo.firstSz = "未上传";
                            if (drReg["xj"].ToString() == "0")
                                upVo.xjSz = "已上传";
                            else
                                upVo.xjSz = "未上传";

                            if (drReg["first"].ToString() == "0" && drReg["xj"].ToString() == "0")
                            {
                                upVo.SZ = "已上传";
                            }
                            else
                            {
                                upVo.SZ = "未上传";
                            }

                            if (drReg["jbr"] != DBNull.Value)
                                upVo.JBRXM = drReg["jbr"].ToString();
                            if (drReg["uploaddate"] != DBNull.Value)
                            {
                                upVo.UPLOADDATE = Function.Datetime(drReg["uploaddate"]);
                                upVo.uploadDateStr = Function.Datetime(drReg["uploaddate"]).ToString("yyyy-MM-dd HH:mm");
                            }
                            upVo.emrinpatientDate = emrinpatientdate;
                            upVo.RYRQ = drReg["RYRQ1"].ToString();
                            upVo.CYRQ = drReg["CYRQ1"].ToString();
                            upVo.RYSJ1 = drReg["RYSJ"].ToString();
                            upVo.CYSJ1 = drReg["CYSJ"].ToString();
                            upVo.idCard = drReg["idcard_chr"].ToString();
                            upVo.MZH = MZH;
                            upVo.YJDZ = drReg["YJDZ"].ToString();
                            #endregion

                            data.Add(upVo);
                            emrIpnoStr += "'" + emrinpatientid + "',";
                        }
                    }
                    if (!string.IsNullOrEmpty(emrIpnoStr))
                        emrIpnoStr = "(" + emrIpnoStr.TrimEnd(',') + ")";

                    #region 出院小结
                    if (data != null)
                    {
                        List<EntityCyxj> lstXjJH = new List<EntityCyxj>();
                        List<EntityCyxj> lstXj = new List<EntityCyxj>();

                        #region JH小结
                        lstXjJH = GetPatCyxjFromJH(registeridStr);

                        if (lstXjJH != null)
                        {
                            foreach (var upVo in data)
                            {
                                EntityCyxj xjVo = lstXjJH.Find(r => r.registerId == upVo.REGISTERID);
                                if (xjVo != null)
                                {
                                    upVo.xjVo = new EntityCyxj();
                                    upVo.xjVo = xjVo;
                                    upVo.xjVo.MZH = upVo.MZH;
                                    upVo.xjVo.JZJLH = upVo.JZJLH;
                                    if (upVo.fpVo.FTIMES > 0)
                                        upVo.xjVo.FTIMES = upVo.FTIMES.ToString(); 
                                    upVo.xjVo.FSUM1 = Function.Dec(upVo.fpVo.FSUM1);
                                    upVo.xjVo.FPHM = upVo.fpVo.FPHM;
                                    if (!string.IsNullOrEmpty(upVo.fpVo.FIDCard))
                                        upVo.xjVo.GMSFHM = upVo.fpVo.FIDCard;
                                    upVo.xjVo.ZY = upVo.fpVo.FJOB;
                                    upVo.xjSource = 2;
                                }
                            }
                        }
                        #endregion

                        #region icare小结
                        lstXj = GetPatCyxjList2(emrIpnoStr, beginEmrDate, endEmrDate);
                        if (lstXj != null)
                        {
                            foreach (var upVo in data)
                            {
                                if (upVo.xjSource == 2)
                                    continue;

                                upVo.xjVo = lstXj.Find(r => r.ZYH == upVo.emrinpatientId && r.emrinpatientDate == upVo.emrinpatientDate);
                                if (upVo.xjVo != null)
                                {
                                    upVo.xjSource = 1;
                                    upVo.xjVo.JZJLH = upVo.JZJLH;
                                    upVo.xjVo.ZYH = upVo.INPATIENTID;
                                    upVo.xjVo.MZH = upVo.MZH;
                                    upVo.xjVo.MZZD = upVo.fpVo.FMZZD;
                                    if (string.IsNullOrEmpty(upVo.xjVo.MZZD))
                                        upVo.xjVo.MZZD = "-";
                                    if (upVo.xjVo.MZZD.Length > 100)
                                        upVo.xjVo.MZZD = upVo.xjVo.MZZD.Substring(0, 100);

                                    if (string.IsNullOrEmpty(upVo.xjVo.RYZD))
                                        upVo.xjVo.RYZD = upVo.fpVo.FMZZD;
                                    if (string.IsNullOrEmpty(upVo.xjVo.RYZD))
                                        upVo.xjVo.RYZD = "-";
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
                                        upVo.xjVo.GMSFHM = upVo.idCard;
                                    upVo.xjVo.RYRQ = upVo.RYRQ;
                                    upVo.xjVo.CYRQ = upVo.CYRQ;
                                    upVo.xjVo.RYSJ = upVo.RYSJ1;
                                    upVo.xjVo.CYSJ = upVo.CYSJ1;
                                    upVo.xjVo.ZYTS = upVo.fpVo.FDAYS;
                                    upVo.xjVo.ZY = upVo.fpVo.FJOB;
                                    upVo.xjVo.JG = upVo.fpVo.FNATIVE;
                                    if (string.IsNullOrEmpty(upVo.xjVo.JG))
                                        upVo.xjVo.JG = "无";
                                    upVo.xjVo.YJDZ = upVo.YJDZ;
                                    if (string.IsNullOrEmpty(upVo.xjVo.YJDZ))
                                        upVo.xjVo.YJDZ = "-";
                                    upVo.xjVo.FTIMES = upVo.fpVo.FTIMES.ToString();
                                    upVo.xjVo.FSUM1 = upVo.fpVo.FSUM1;
                                    upVo.xjVo.FPHM = upVo.fpVo.FPHM;
                                }
                            }
                        }
                        #endregion
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
        public void GetQuerypat(string dteBegin, string dteEnd, string parmStr, out List<EntityQueryBa> dataIcare, out List<EntityQueryBa> dataBa)
        {
            string SqlBa = string.Empty;
            string SqlReg = string.Empty;
            string SqlJs = string.Empty;
            IDataParameter[] parm = null;
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
                DataTable dtJs = svc.GetDataTable(SqlJs, parm);

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
        public List<EntityCyxj> GetPatCyxjList2(string ipnoStr, string BeginEmrinpatientdate, string EndEmrinpatientdate)
        {
            SqlHelper svc = null;
            IDataParameter[] parm = null;
            string opendate = string.Empty;
            DataTable dtResult = null;
            List<EntityCyxj> dataXj = null;

            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);

                #region 出院小结
                ;
                #region
                //普通出院记录
                string Sql2 = @"select                                 
									a.inpatientid,
                                       a.inpatientdate,
                                       a.opendate,
                                       a.inhospitaldiagnose,
                                       a.outhospitaldiagnose,
                                       a.inhospitalby,
                                       b.inhospitaldiagnose_right,
                                       b.outhospitalcase_right,
                                       b.outhospitaladvice_right,
                                       b.doctorname
                                  from outhospitalrecord a,
                                       outhospitalrecordcontent b,
                                       (select inpatientid, inpatientdate, createdate, opendate
                                          from outhospitalrecord
                                         where inpatientid in {0}
                                           and inpatientdate between
                                               to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                               to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                           and status = 0
                                        union
                                        select inpatientid, inpatientdate, createdate, opendate
                                          from t_emr_outhospitalin24hours
                                         where inpatientid in {0}
                                           and inpatientdate between
                                               to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                               to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                           and status = 0) c
                                 where a.inpatientid = c.inpatientid
                                   and a.inpatientdate = c.inpatientdate
                                   and a.opendate = c.opendate
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
                               t.inhospitaldiagnose1 as inhospitaldiagnose,
							   t.outhospitaldiagnose1 as outhospitaldiagnose,
							   t.diagnosecoruse as inhospitalby,
							   t.inhospitalinstance  as inhospitaldiagnose_right,
                               t.outhospitalinstance as outhospitalcase_right,
                               t.outhospitaladvice1 as outhospitaladvice_right,
                             f_getempnamebyno_1stofall(t.doctorsign)  as doctorname
                             from t_emr_outhospitalin24hours t ,
                              (select inpatientid, inpatientdate, createdate, opendate
                                          from outhospitalrecord
                                         where inpatientid in {0}
                                           and inpatientdate between
                                               to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                               to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                           and status = 0
                                        union
                                        select inpatientid, inpatientdate, createdate, opendate
                                          from t_emr_outhospitalin24hours
                                         where inpatientid in {0}
                                           and inpatientdate between
                                               to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                               to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                           and status = 0) c
                             where t.inpatientid = c.inpatientid
                               and t.inpatientdate =   c.inpatientdate
                               and t.opendate = c.opendate
                               and t.status = 0";
                #endregion

                #endregion

                if (!string.IsNullOrEmpty(ipnoStr))
                {
                    Sql2 = string.Format(Sql2, ipnoStr);
                    Sql3 = string.Format(Sql3, ipnoStr);
                    string sql = Sql2 + Environment.NewLine + "union all " + Environment.NewLine + Sql3;
                    parm = svc.CreateParm(8);
                    parm[0].Value = BeginEmrinpatientdate;
                    parm[1].Value = EndEmrinpatientdate;
                    parm[2].Value = BeginEmrinpatientdate;
                    parm[3].Value = EndEmrinpatientdate;
                    parm[4].Value = BeginEmrinpatientdate;
                    parm[5].Value = EndEmrinpatientdate;
                    parm[6].Value = BeginEmrinpatientdate;
                    parm[7].Value = EndEmrinpatientdate;

                    dtResult = svc.GetDataTable(sql, parm);

                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        dataXj = new List<EntityCyxj>();

                        foreach (DataRow drXj in dtResult.Rows)
                        {
                            EntityCyxj vo = new EntityCyxj();
                            #region 上传信息 出院小结

                            vo.emrinpatientDate = Function.Datetime(drXj["inpatientdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                            vo.ZYH = drXj["inpatientid"].ToString();
                            vo.RYZD = drXj["inhospitaldiagnose"].ToString().Trim();
                            vo.CYZD = drXj["outhospitaldiagnose"].ToString().Trim();
                            if (drXj["outhospitaldiagnose"] == DBNull.Value)
                                vo.CYZD = "-";
                            vo.CYYZ = drXj["outhospitaladvice_right"].ToString().Trim();
                            if (string.IsNullOrEmpty(vo.CYYZ))
                                vo.CYYZ = "-";
                            vo.RYQK = drXj["inhospitaldiagnose_right"].ToString().Trim();
                            if (string.IsNullOrEmpty(vo.RYQK))
                                vo.RYQK = "-";
                            vo.YSQM = drXj["doctorname"].ToString().Trim();
                            if (string.IsNullOrEmpty(vo.YSQM))
                                vo.YSQM = "-";
                            vo.RYHCLGC = "-";
                            vo.CYSQK = drXj["outhospitalcase_right"].ToString().Trim();
                            if (string.IsNullOrEmpty(vo.CYSQK))
                                vo.CYSQK = "-";
                            vo.ZLJG = drXj["inhospitalby"].ToString().Trim();
                            if (vo.ZLJG.Length > 1000)
                                vo.ZLJG = vo.ZLJG.Substring(0, 1000);
                            if (string.IsNullOrEmpty(vo.ZLJG))
                                vo.ZLJG = "-";

                            dataXj.Add(vo);
                            #endregion
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
            return dataXj;
        }
        #endregion

        #region 首页 嘉禾
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipno"></param>
        /// <param name="registerid"></param>
        /// <param name="emrinpatientdate"></param>
        /// <returns></returns>
        public List<EntityFirstPage> GetPatBaFromJH(string registeridStr)
        {
            List<EntityFirstPage> data = null;
            EntityFirstPage firstPageVo = null;
            SqlHelper svc = null;
            try
            {
                svc = new SqlHelper(EnumBiz.interfaceDB);

                #region
                string sql = @" select  a.PATIENT_ID,
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
                                        from jhemr.jhemr_his_ba123 a where a.PATIENT_ID in ";
                #endregion

                if (string.IsNullOrEmpty(registeridStr))
                    return null;

                sql += "(" + registeridStr + ")";
                DataTable dt = svc.GetDataTable(sql);

                string sqlZk = @"select * from jhemr.jhemr_his_ba2 where Patient_Id in (" + registeridStr + ")";
                DataTable dtZk = svc.GetDataTable(sqlZk);

                sql = @"select * from jhemr.jhemr_his_ba3 where Patient_Id in (" + registeridStr + ")";
                DataTable dtZd = svc.GetDataTable(sql);

                sql = @"select * from jhemr.jhemr_his_ba7 where Patient_Id in (" + registeridStr + ")";
                DataTable dtbTumor = svc.GetDataTable(sql);

                sql = @"select * from jhemr.jhemr_his_ba4 where Patient_Id in (" + registeridStr + ")";
                DataTable dtFop = svc.GetDataTable(sql);

                sql = @"select * from jhemr.jhemr_his_ba5 where Patient_Id in (" + registeridStr + ")";
                DataTable dtFy = svc.GetDataTable(sql);

                sql = @"select * from jhemr.jhemr_his_ba6 where Patient_Id in (" + registeridStr + ")";
                DataTable dtZl = svc.GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    data = new List<EntityFirstPage>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        firstPageVo = new EntityFirstPage();
                        string registerId = dr["PATIENT_ID"].ToString();
                        firstPageVo.registerId = registerId;

                        #region 首页信息  
                        firstPageVo.FWSJGDM = dr["FWSJGDM"].ToString();
                        firstPageVo.FFBBHNEW = dr["FFBBHNEW"].ToString();
                        firstPageVo.FFBNEW = dr["FFBNEW"].ToString();
                        if (dr["FASCARD1"] != DBNull.Value)
                            firstPageVo.FASCARD1 = dr["FASCARD1"].ToString();
                        else
                            firstPageVo.FASCARD1 = "1";
                        firstPageVo.FTIMES = Function.Int(dr["FTIMES"].ToString());
                        firstPageVo.FPRN = dr["FPRN"].ToString();
                        firstPageVo.FNAME = dr["FNAME"].ToString();
                        firstPageVo.FSEXBH = dr["FSEXBH"].ToString();
                        firstPageVo.FSEX = dr["FSEX"].ToString();
                        firstPageVo.FBIRTHDAY = Function.Datetime(dr["FBIRTHDAY"]).ToString("yyyyMMdd");
                        firstPageVo.FAGE = dr["FAGE"].ToString();
                        firstPageVo.fcountrybh = dr["fcountrybh"].ToString();
                        if (firstPageVo.fcountrybh == "")
                            firstPageVo.fcountrybh = "-";
                        firstPageVo.fcountry = dr["fcountry"].ToString();
                        if (firstPageVo.fcountry == "")
                            firstPageVo.fcountry = "-";
                        firstPageVo.fnationalitybh = dr["fnationalitybh"].ToString();
                        if (firstPageVo.fnationalitybh == "")
                            firstPageVo.fnationalitybh = "-";
                        firstPageVo.fnationality = dr["fnationality"].ToString();
                        firstPageVo.FCSTZ = dr["FCSTZ"].ToString();
                        firstPageVo.FRYTZ = dr["FRYTZ"].ToString();
                        firstPageVo.FBIRTHPLACE = dr["FBIRTHPLACE"].ToString();
                        firstPageVo.FNATIVE = dr["FNATIVE"].ToString();
                        firstPageVo.FIDCard = dr["FIDCard"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FIDCard))
                            firstPageVo.FIDCard = "无";
                        firstPageVo.FJOB = dr["FJOB"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FJOB))
                            firstPageVo.FJOB = "其他";
                        firstPageVo.FSTATUS = dr["FSTATUS"].ToString().Trim();
                        if (firstPageVo.FSTATUS.Contains("已婚"))
                            firstPageVo.FSTATUSBH = "2";
                        else if (firstPageVo.FSTATUS == "未婚")
                            firstPageVo.FSTATUSBH = "1";
                        else if (firstPageVo.FSTATUS == "丧偶")
                            firstPageVo.FSTATUSBH = "3";
                        else if (firstPageVo.FSTATUS == "离婚")
                            firstPageVo.FSTATUSBH = "4";
                        else
                            firstPageVo.FSTATUSBH = "9";
                        firstPageVo.FCURRADDR = dr["FCURRADDR"].ToString();
                        firstPageVo.FCURRTELE = dr["FCURRTELE"].ToString();
                        firstPageVo.FCURRPOST = dr["FCURRPOST"].ToString();
                        firstPageVo.FHKADDR = dr["FHKADDR"].ToString();
                        firstPageVo.FHKPOST = dr["FHKPOST"].ToString();
                        firstPageVo.FDWNAME = dr["FDWNAME"].ToString();
                        firstPageVo.FDWADDR = dr["FDWADDR"].ToString();
                        firstPageVo.FDWTELE = dr["FDWTELE"].ToString();
                        firstPageVo.FDWPOST = dr["FDWPOST"].ToString();
                        firstPageVo.FLXNAME = dr["FLXNAME"].ToString();
                        firstPageVo.FRELATE = dr["FRELATE"].ToString();
                        if (firstPageVo.FRELATE.Length > 10)
                            firstPageVo.FRELATE = firstPageVo.FRELATE.Substring(0, 10);
                        firstPageVo.FLXADDR = dr["FLXADDR"].ToString();
                        firstPageVo.FLXTELE = dr["FLXTELE"].ToString();
                        firstPageVo.FRYTJ = dr["FRYTJ"].ToString();
                        if (firstPageVo.FRYTJ.Contains("急诊"))
                            firstPageVo.FRYTJBH = "1";
                        else if (firstPageVo.FRYTJ.Contains("门诊"))
                            firstPageVo.FRYTJBH = "2";
                        else if (firstPageVo.FRYTJ.Contains("其他医疗机构转入"))
                            firstPageVo.FRYTJBH = "3";
                        else
                        {
                            firstPageVo.FRYTJ = "其他";
                            firstPageVo.FRYTJBH = "9";
                        } 
                        firstPageVo.FRYDATE = Function.Datetime(dr["FRYDATE"]).ToString("yyyy-MM-dd");
                        firstPageVo.FRYTIME = dr["FRYTIME"].ToString();
                        firstPageVo.FRYTIME = Function.Datetime(dr["FRYTIME"]).ToString("HH:mm:ss");
                        firstPageVo.FRYTYKH = dr["FRYTYKH"].ToString();
                        firstPageVo.FRYDEPT = dr["FRYDEPT"].ToString();
                        firstPageVo.FRYBS = dr["FRYBS"].ToString().Trim();
                        if (firstPageVo.FRYBS == "")
                            firstPageVo.FRYBS = firstPageVo.FRYDEPT;
                        firstPageVo.FZKTYKH = dr["FZKTYKH"].ToString();
                        firstPageVo.FZKDEPT = dr["FZKDEPT"].ToString();
                        firstPageVo.FZKTIME = dr["FZKTIME"].ToString();
                        if (firstPageVo.FZKTIME.Length < 4)
                            firstPageVo.FZKTIME = Function.Datetime(dr["FZKTIME"]).ToString("HH:mm:ss");
                        firstPageVo.FCYDATE = Function.Datetime(dr["FCYDATE"]).ToString("yyyy-MM-dd");

                        firstPageVo.FCYTIME = dr["FCYTIME"].ToString();
                        if (firstPageVo.FCYTIME.Length < 4)
                            firstPageVo.FCYTIME = Function.Datetime(dr["FCYTIME"]).ToString("HH:mm:ss");
                        firstPageVo.FCYTYKH = dr["FCYTYKH"].ToString();
                        firstPageVo.FCYDEPT = dr["FCYDEPT"].ToString();
                        firstPageVo.FCYBS = dr["FCYBS"].ToString().Trim();
                        if (firstPageVo.FCYBS == "")
                            firstPageVo.FCYBS = firstPageVo.FCYDEPT;
                        TimeSpan ts = Function.Datetime(firstPageVo.FCYDATE) - Function.Datetime(firstPageVo.FRYDATE);
                        firstPageVo.FDAYS = ts.Days.ToString();
                        if (firstPageVo.FDAYS == "0")
                            firstPageVo.FDAYS = "1";

                        firstPageVo.FMZZD = dr["FMZZD"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FMZZD))
                            firstPageVo.FMZZD = "-";
                        firstPageVo.FMZZDBH = dr["FMZZDBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FMZZDBH))
                            firstPageVo.FMZZDBH = "-";

                            firstPageVo.FMZDOCTBH = dr["FMZDOCTBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FMZDOCTBH))
                            firstPageVo.FMZDOCTBH = "无";
                        firstPageVo.FMZDOCT = dr["FMZDOCT"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FMZDOCT))
                            firstPageVo.FMZDOCT = "无";
                        firstPageVo.FJBFXBH = dr["FJBFXBH"].ToString();
                        firstPageVo.FJBFX = dr["FJBFX"].ToString();
                        firstPageVo.FYCLJBH = dr["FYCLJBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FYCLJBH))
                            firstPageVo.FYCLJBH = "2";
                        firstPageVo.FYCLJ = dr["FYCLJ"].ToString();
                        if (!string.IsNullOrEmpty(firstPageVo.FYCLJBH))
                            firstPageVo.FYCLJ = "是";
                        else
                            firstPageVo.FYCLJ = "否";
                        firstPageVo.FQJTIMES = dr["FQJTIMES"].ToString();
                        firstPageVo.FQJSUCTIMES = dr["FQJSUCTIMES"].ToString();
                        if (!string.IsNullOrEmpty(firstPageVo.FQJTIMES) && string.IsNullOrEmpty(firstPageVo.FQJSUCTIMES))
                        {
                            firstPageVo.FQJSUCTIMES = firstPageVo.FQJTIMES;
                        }
                        firstPageVo.FPHZD = dr["FPHZD"].ToString();
                        if (firstPageVo.FPHZD.Length > 100)
                            firstPageVo.FPHZD = firstPageVo.FPHZD.Substring(0, 100);

                        if (dr["FPHZDNUM"].ToString().Trim() != "")
                            firstPageVo.FPHZDNUM = dr["FPHZDNUM"].ToString();
                        else
                            firstPageVo.FPHZDNUM = "-";

                        if (dr["FPHZDBH"].ToString().Trim() != "")
                            firstPageVo.FPHZDBH = dr["FPHZDBH"].ToString();
                        else
                            firstPageVo.FPHZDBH = "0";

                        firstPageVo.FIFGMYWBH = dr["FIFGMYWBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FIFGMYWBH))
                            firstPageVo.FIFGMYWBH = "1";
                        if (dr["FIFGMYW"].ToString() != "")
                            firstPageVo.FIFGMYW = dr["FIFGMYW"].ToString();
                        else
                            firstPageVo.FIFGMYW = "-";
                        if (dr["FGMYW"].ToString() != "")
                            firstPageVo.FGMYW = dr["FGMYW"].ToString();
                        else
                            firstPageVo.FGMYW = "-";
                        if (dr["FBODYBH"].ToString().Trim() != "")
                            firstPageVo.FBODYBH = dr["FBODYBH"].ToString();
                        else
                            firstPageVo.FBODYBH = "2";
                        if (dr["FBODY"].ToString().Trim() != "")
                            firstPageVo.FBODY = dr["FBODY"].ToString();
                        else
                            firstPageVo.FBODY = "否";
                        firstPageVo.FBLOODBH = dr["FBLOODBH"].ToString();
                        firstPageVo.FBLOOD = dr["FBLOOD"].ToString();
                        firstPageVo.FRHBH = dr["FRHBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FRHBH))
                            firstPageVo.FRHBH = "4";
                        firstPageVo.FRH = dr["FRH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FRH))
                            firstPageVo.FRH = "未查";
                        firstPageVo.FKZRBH = dr["FKZRBH"].ToString();
                        firstPageVo.FKZR = dr["FKZR"].ToString();
                        firstPageVo.FZRDOCTBH = dr["FZRDOCTBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FZRDOCTBH))
                            firstPageVo.FZRDOCTBH = "-";
                        firstPageVo.FZRDOCTOR = dr["FZRDOCTOR"].ToString();
                        firstPageVo.FZZDOCTBH = dr["FZZDOCTBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FZZDOCTBH))
                            firstPageVo.FZZDOCTBH = "-";
                        firstPageVo.FZZDOCT = dr["FZZDOCT"].ToString();
                        firstPageVo.FZYDOCTBH = dr["FZYDOCTBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FZYDOCTBH))
                            firstPageVo.FZYDOCTBH = "-";
                        firstPageVo.FZYDOCT = dr["FZYDOCT"].ToString();
                        firstPageVo.FNURSEBH = dr["FNURSEBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FNURSEBH))
                            firstPageVo.FNURSEBH = "-";
                        firstPageVo.FNURSE = dr["FNURSE"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FNURSE))
                            firstPageVo.FNURSE = "-";
                        firstPageVo.FJXDOCTBH = dr["FJXDOCTBH"].ToString();
                        firstPageVo.FJXDOCT = dr["FJXDOCT"].ToString();
                        firstPageVo.FSXDOCTBH = dr["FSXDOCTBH"].ToString();
                        firstPageVo.FSXDOCT = dr["FSXDOCT"].ToString();
                        firstPageVo.FBMYBH = dr["FBMYBH"].ToString();
                        firstPageVo.FBMY = dr["FBMY"].ToString();
                        firstPageVo.FQUALITYBH = dr["FQUALITYBH"].ToString();
                        firstPageVo.FQUALITY = dr["FQUALITY"].ToString();
                        firstPageVo.FZKDOCTBH = dr["FZKDOCTBH"].ToString();
                        if (firstPageVo.FZKDOCTBH == "")
                            firstPageVo.FZKDOCTBH = "-";
                        firstPageVo.FZKDOCT = dr["FZKDOCT"].ToString();
                        firstPageVo.FZKNURSEBH = dr["FZKNURSEBH"].ToString().Trim();
                        if (firstPageVo.FZKNURSEBH == "")
                            firstPageVo.FZKNURSEBH = "-";
                        firstPageVo.FZKNURSE = dr["FZKNURSE"].ToString();
                        if (firstPageVo.FZKNURSE == "")
                            firstPageVo.FZKNURSE = "-";
                        firstPageVo.FZKRQ = Function.Datetime(dr["FZKRQ"]).ToString("yyyyMMdd");

                        firstPageVo.FLYFSBH = dr["FLYFSBH"].ToString().Trim();
                        if (firstPageVo.FLYFSBH != "1" || firstPageVo.FLYFSBH != "2" ||
                            firstPageVo.FLYFSBH != "3" || firstPageVo.FLYFSBH != "4" || firstPageVo.FLYFSBH != "5")
                            firstPageVo.FLYFSBH = "9";

                        firstPageVo.FLYFS = dr["FLYFS"].ToString();
                        if (firstPageVo.FLYFS.Length >= 26)
                            firstPageVo.FLYFS = firstPageVo.FLYFS.Substring(0, 50);

                        firstPageVo.FYZOUTHOSTITAL = dr["FYZOUTHOSTITAL"].ToString();
                        firstPageVo.FSQOUTHOSTITAL = dr["FSQOUTHOSTITAL"].ToString();
                        firstPageVo.FISAGAINRYBH = dr["FISAGAINRYBH"].ToString();
                        if (firstPageVo.FISAGAINRYBH == "")
                            firstPageVo.FISAGAINRYBH = "-";
                        firstPageVo.FISAGAINRY = dr["FISAGAINRY"].ToString();
                        if (firstPageVo.FISAGAINRY == "")
                            firstPageVo.FISAGAINRY = "-";
                        firstPageVo.FISAGAINRYMD = dr["FISAGAINRYMD"].ToString();
                        if (firstPageVo.FISAGAINRYMD == "")
                            firstPageVo.FISAGAINRYMD = "-";
                        firstPageVo.FRYQHMDAYS = dr["FRYQHMDAYS"].ToString();
                        firstPageVo.FRYQHMHOURS = dr["FRYQHMHOURS"].ToString();
                        firstPageVo.FRYQHMMINS = dr["FRYQHMMINS"].ToString();
                        firstPageVo.FRYQHMCOUNTS = dr["FRYQHMCOUNTS"].ToString();
                        firstPageVo.FRYHMDAYS = dr["FRYHMDAYS"].ToString();
                        firstPageVo.FRYHMHOURS = dr["FRYHMHOURS"].ToString();
                        firstPageVo.FRYHMMINS = dr["FRYHMMINS"].ToString();
                        firstPageVo.FRYHMCOUNTS = dr["FRYHMCOUNTS"].ToString();
                        firstPageVo.FSUM1 = Function.Dec(dr["FSUM1"].ToString());
                        firstPageVo.FZFJE = Function.Dec(dr["FZFJE"].ToString());
                        firstPageVo.FZHFWLYLF = Function.Dec(dr["FZHFWLYLF"].ToString());
                        firstPageVo.FZHFWLCZF = Function.Dec(dr["FZHFWLCZF"].ToString());
                        firstPageVo.FZHFWLHLF = Function.Dec(dr["FZHFWLHLF"].ToString());
                        firstPageVo.FZHFWLQTF = Function.Dec(dr["FZHFWLQTF"].ToString());
                        firstPageVo.FZDLBLF = Function.Dec(dr["FZDLBLF"].ToString());
                        firstPageVo.FZDLSSSF = Function.Dec(dr["FZDLSSSF"].ToString());
                        firstPageVo.FZDLYXF = Function.Dec(dr["FZDLYXF"].ToString());
                        firstPageVo.FZDLLCF = Function.Dec(dr["FZDLLCF"].ToString());
                        firstPageVo.FZLLFFSSF = Function.Dec(dr["FZLLFFSSF"].ToString());
                        firstPageVo.FZLLFWLZWLF = Function.Dec(dr["FZLLFWLZWLF"].ToString());
                        firstPageVo.FZLLFSSF = Function.Dec(dr["FZLLFSSF"].ToString());
                        firstPageVo.FZLLFMZF = Function.Dec(dr["FZLLFMZF"].ToString());
                        firstPageVo.FZLLFSSZLF = Function.Dec(dr["FZLLFSSZLF"].ToString());
                        firstPageVo.FKFLKFF = Function.Dec(dr["FKFLKFF"].ToString());
                        firstPageVo.FZYLZF = Function.Dec(dr["FZYLZF"].ToString());
                        firstPageVo.FXYF = Function.Dec(dr["FXYF"].ToString());
                        firstPageVo.FXYLGJF = Function.Dec(dr["FXYLGJF"].ToString());
                        firstPageVo.FZCHYF = Function.Dec(dr["FZCHYF"].ToString());
                        firstPageVo.FZCYF = Function.Dec(dr["FZCYF"].ToString());
                        firstPageVo.FXYLXF = Function.Dec(dr["FXYLXF"].ToString());
                        firstPageVo.FXYLBQBF = Function.Dec(dr["FXYLBQBF"].ToString());
                        firstPageVo.FXYLQDBF = Function.Dec(dr["FXYLQDBF"].ToString());
                        firstPageVo.FXYLYXYZF = Function.Dec(dr["FXYLYXYZF"].ToString());
                        firstPageVo.FXYLXBYZF = Function.Dec(dr["FXYLXBYZF"].ToString());
                        firstPageVo.FHCLCJF = Function.Dec(dr["FHCLCJF"].ToString());
                        firstPageVo.FHCLZLF = Function.Dec(dr["FHCLZLF"].ToString());
                        firstPageVo.FHCLSSF = Function.Dec(dr["FHCLSSF"].ToString());
                        firstPageVo.FQTF = Function.Dec(dr["FQTF"]);
                        firstPageVo.FBGLX = dr["FBGLX"].ToString();

                        if (dr["fidcard"].ToString() != "")
                            firstPageVo.GMSFHM = dr["fidcard"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.GMSFHM))
                            firstPageVo.GMSFHM = dr["FASCARD1"].ToString(); ;
                        firstPageVo.FZYF = Function.Dec(dr["FZYF"].ToString());
                        if (dr["FZKDATE"] != DBNull.Value)
                            firstPageVo.FZKDATE = Function.Datetime(dr["FZKDATE"]).ToString("yyyy-MM-dd");
                        else
                            firstPageVo.FZKDATE = "";

                        firstPageVo.FZKTIME = Function.Datetime(firstPageVo.FZKDATE + " " + firstPageVo.FZKTIME).ToString("yyyyMMddHHmmss");
                        firstPageVo.FJOBBH = dr["FJOBBH"].ToString();
                        if (string.IsNullOrEmpty(firstPageVo.FJOBBH))
                            firstPageVo.FJOBBH = "90";
                        firstPageVo.FZHFWLYLF01 = Function.Dec(dr["FZHFWLYLF01"]);
                        firstPageVo.FZHFWLYLF02 = Function.Dec(dr["FZHFWLYLF02"]);
                        firstPageVo.FZYLZDF = Function.Dec(dr["FZYLZDF"]);
                        firstPageVo.FZYLZLF = Function.Dec(dr["FZYLZLF"]);
                        firstPageVo.FZYLZLF01 = Function.Dec(dr["FZYLZLF01"]);
                        firstPageVo.FZYLZLF02 = Function.Dec(dr["FZYLZLF02"]);
                        firstPageVo.FZYLZLF03 = Function.Dec(dr["FZYLZLF03"]);
                        firstPageVo.FZYLZLF04 = Function.Dec(dr["FZYLZLF04"]);
                        firstPageVo.FZYLZLF05 = Function.Dec(dr["FZYLZLF05"]);
                        firstPageVo.FZYLZLF06 = Function.Dec(dr["FZYLZLF06"]);
                        firstPageVo.FZYLQTF = Function.Dec(dr["FZYLQTF"]);
                        firstPageVo.FZCLJGZJF = Function.Dec(dr["FZYLQTF"]);
                        firstPageVo.FZYLQTF01 = Function.Dec(dr["FZYLQTF"]);
                        firstPageVo.FZYLQTF02 = Function.Dec(dr["FZYLQTF"]);
                        firstPageVo.FZYID = dr["FZYID"].ToString();
                        #endregion

                        #region 转科情况（数据集dtZk）
                        if(dtZk != null && dtZk.Rows.Count > 0)
                        {
                            DataRow[] drrZk = dtZk.Select("Patient_Id = '" + registerId + "'");
                            if (drrZk.Length > 0)
                            {
                                EntityBrzkqk zkVo = null;
                                firstPageVo.lstZkVo = new List<EntityBrzkqk>();

                                for (int i = 0; i < drrZk.Length; i++)
                                {
                                    DataRow drZk = drrZk[i];
                                    zkVo = new EntityBrzkqk();
                                    if (string.IsNullOrEmpty(zkVo.FZKTYKH))
                                        continue;
                                    zkVo.FZKTYKH = drZk["FZKTYKH"].ToString();
                                    zkVo.FZKDEPT = drZk["FZKDEPT"].ToString();
                                    zkVo.FZKDATE = Function.Datetime(drZk["FZKDATE"]).ToString("yyyy-MM-dd");
                                    zkVo.FZKTIME = Function.Datetime(drZk["FZKTIME"].ToString()).ToString("HH:mm:ss");
                                    zkVo.FPRN = drZk["FPRN"].ToString();
                                    firstPageVo.lstZkVo.Add(zkVo);
                                }
                            }
                        }
                        #endregion

                        #region 数据集(病人诊断信息)
                        if(dtZd != null && dtZd.Rows.Count > 0)
                        {
                            DataRow[] drrZd = dtZd.Select("Patient_Id = '" + registerId + "'");
                            if (drrZd.Length > 0)
                            {
                                EntityBrzdxx zdVo = null;
                                firstPageVo.lstZdVo = new List<EntityBrzdxx>();

                                for (int i = 0; i < drrZd.Length; i++)
                                {
                                    DataRow drZd = drrZd[i];
                                    zdVo = new EntityBrzdxx();

                                    zdVo.FZDLX = drZd["FZDLX"].ToString();
                                    zdVo.FICDVersion = drZd["FICDVersion"].ToString();
                                    zdVo.FICDM = drZd["FICDM"].ToString();
                                    if (drZd["FJBNAME"].ToString().Length > 10)
                                        zdVo.FJBNAME = drZd["FJBNAME"].ToString().Substring(0, 10);
                                    else
                                        zdVo.FJBNAME = drZd["FJBNAME"].ToString();
                                    zdVo.FRYBQBH = drZd["FRYBQBH"].ToString();
                                    if (zdVo.FRYBQBH == "")
                                        zdVo.FRYBQBH = "无";
                                    zdVo.FRYBQ = drZd["FRYBQ"].ToString();
                                    if (zdVo.FRYBQ == "")
                                        zdVo.FRYBQ = "无";
                                    zdVo.FPRN = drZd["FPRN"].ToString();
                                    firstPageVo.lstZdVo.Add(zdVo);
                                }
                            }
                        }
                        #endregion

                        #region 数据集（肿瘤化疗记录）
                        if(dtbTumor != null && dtbTumor.Rows.Count > 0)
                        {
                            DataRow[] drrTumor = dtbTumor.Select("Patient_Id = '" + registerId + "'");
                            if (drrTumor.Length > 0)
                            {
                                firstPageVo.lstHlVo = new List<EntityZlhljlsj>();

                                for (int i = 0; i < drrTumor.Length; i++)
                                {
                                    DataRow drTemp = drrTumor[i];
                                    EntityZlhljlsj vo = new EntityZlhljlsj();
                                    vo.FHLRQ1 = Function.Datetime(drTemp["FHLRQ1"]).ToString("yyyyMMdd");
                                    vo.FHLDRUG = drTemp["FHLDRUG"].ToString();
                                    vo.FHLPROC = drTemp["FHLPROC"].ToString();
                                    vo.FHLLXBH = drTemp["FHLLXBH"].ToString();
                                    vo.FHLLX = drTemp["FHLLX"].ToString();
                                    vo.FPRN = drTemp["FPRN"].ToString();

                                    firstPageVo.lstHlVo.Add(vo);
                                }
                            }
                        }
                        
                        #endregion

                        #region 数据集(病人手术信息)

                        if (dtFop != null && dtFop.Rows.Count > 0)
                        {
                            EntityBrssxx fopVo = null;
                            firstPageVo.lstSsVo = new List<EntityBrssxx>();
                            DataRow[] drrFop = dtFop.Select("Patient_Id = '" + registerId + "'");
                            if (drrFop.Length > 0)
                            {
                                for (int i = 0; i < drrFop.Length; i++)
                                {
                                    DataRow drFop = drrFop[i];
                                    fopVo = new EntityBrssxx();
                                    fopVo.FNAME = drFop["FNAME"].ToString();
                                    if (fopVo.FNAME == "")
                                        continue;
                                    fopVo.FOPTIMES = drFop["FOPTIMES"].ToString();
                                    if (fopVo.FOPTIMES == "0")
                                        fopVo.FOPTIMES = "1";
                                    fopVo.FOPCODE = drFop["FOPCODE"].ToString();
                                    fopVo.FOP = drFop["FOP"].ToString();
                                    fopVo.FOPDATE = Function.Datetime(drFop["FOPDATE"]).ToString("yyyyMMdd");
                                    fopVo.FQIEKOUBH = drFop["FQIEKOUBH"].ToString() == "" ? "无" : drFop["FQIEKOUBH"].ToString();
                                    fopVo.FQIEKOU = drFop["FQIEKOU"].ToString() == "" ? "无" : drFop["FQIEKOU"].ToString();
                                    fopVo.FYUHEBH = drFop["FYUHEBH"].ToString() == "" ? "无" : drFop["FYUHEBH"].ToString();
                                    if (fopVo.FYUHEBH == "")
                                        fopVo.FYUHEBH = "-";
                                    fopVo.FYUHE = drFop["FYUHE"].ToString();
                                    if (fopVo.FYUHE == "")
                                        fopVo.FYUHE = "-";
                                    fopVo.FDOCBH = drFop["FDOCBH"].ToString();
                                    if (fopVo.FDOCBH == "")
                                        fopVo.FDOCBH = "-";
                                    fopVo.FDOCNAME = drFop["FDOCNAME"].ToString() == "" ? "无" : drFop["FDOCNAME"].ToString();
                                    fopVo.FMAZUIBH = drFop["FMAZUIBH"].ToString();
                                    if (fopVo.FMAZUIBH == "")
                                        fopVo.FMAZUIBH = "无";
                                    if (fopVo.FMZDOCTBH == "")
                                        fopVo.FMZDOCTBH = "无";
                                    fopVo.FMAZUI = drFop["FMAZUI"].ToString() == "" ? "无" : drFop["FMAZUI"].ToString();
                                    fopVo.FIFFSOP = drFop["FIFFSOP"].ToString();
                                    if (fopVo.FIFFSOP == "False")
                                        fopVo.FIFFSOP = "0";
                                    else if (fopVo.FIFFSOP == "True")
                                        fopVo.FIFFSOP = "1";
                                    fopVo.FOPDOCT1BH = drFop["FOPDOCT1BH"].ToString();
                                    if (fopVo.FOPDOCT1BH == "")
                                        fopVo.FOPDOCT1BH = "无";
                                    fopVo.FOPDOCT1 = drFop["FOPDOCT1"].ToString();
                                    if (fopVo.FOPDOCT1 == "")
                                        fopVo.FOPDOCT1 = "-";
                                    fopVo.FOPDOCT2BH = drFop["FOPDOCT2BH"].ToString();
                                    if (fopVo.FOPDOCT2BH == "")
                                        fopVo.FOPDOCT2BH = "无";
                                    fopVo.FOPDOCT2 = drFop["FOPDOCT2"].ToString();
                                    if (fopVo.FOPDOCT2 == "")
                                        fopVo.FOPDOCT2 = "无";
                                    fopVo.FMZDOCTBH = drFop["FMZDOCTBH"].ToString();
                                    if (fopVo.FMZDOCTBH == "")
                                        fopVo.FMZDOCTBH = "无";
                                    fopVo.FMZDOCT = drFop["FMZDOCT"].ToString();
                                    if (fopVo.FMZDOCT == "")
                                        fopVo.FMZDOCT = "无";
                                    fopVo.FZQSSBH = drFop["FZQSSBH"].ToString();
                                    if (fopVo.FZQSSBH == "")
                                        fopVo.FZQSSBH = "无";
                                    fopVo.FZQSS = drFop["FZQSS"].ToString();
                                    fopVo.FSSJBBH = drFop["FSSJBBH"].ToString();
                                    if (fopVo.FSSJBBH == "")
                                        fopVo.FSSJBBH = "无";
                                    fopVo.FSSJB = drFop["FSSJB"].ToString();
                                    fopVo.FOPKSNAME = drFop["FOPKSNAME"].ToString();
                                    if (fopVo.FOPKSNAME == "")
                                        fopVo.FOPKSNAME = "无";
                                    fopVo.FOPTYKH = drFop["FOPTYKH"].ToString();
                                    if (fopVo.FOPTYKH == "")
                                        fopVo.FOPTYKH = "无";

                                    fopVo.FPRN = drFop["FPRN"].ToString();
                                    firstPageVo.lstSsVo.Add(fopVo);
                                }

                            }
                        }
                        #endregion

                        #region 数据集（妇婴卡）

                        if (dtFy != null && dtFy.Rows.Count > 0)
                        {
                            EntityFyksj fyVo = null;
                            firstPageVo.lstFyVo = new List<EntityFyksj>();
                            DataRow[] drrFy = dtFy.Select("Patient_Id = '" + registerId + "'");
                            if (drrFy.Length > 0)
                            {
                                for (int i = 0; i < drrFy.Length; i++)
                                {
                                    DataRow drFy = drrFy[i];
                                    fyVo = new EntityFyksj();
                                    fyVo.FBABYNUM = drFy["FBABYNUM"].ToString() == "" ? "-" : drFy["FBABYNUM"].ToString();
                                    fyVo.FNAME = drFy["FNAME"].ToString() == "" ? "-" : drFy["FNAME"].ToString();
                                    fyVo.FBABYSEXBH = drFy["FBABYSEXBH"].ToString() == "" ? "-" : drFy["FBABYSEXBH"].ToString();
                                    fyVo.FBABYSEX = drFy["FBABYSEX"].ToString() == "" ? "-" : drFy["FBABYSEX"].ToString();
                                    fyVo.FTZ = drFy["FTZ"].ToString() == "" ? "-" : drFy["FTZ"].ToString();
                                    fyVo.FRESULTBH = drFy["FRESULTBH"].ToString() == "" ? "-" : drFy["FRESULTBH"].ToString();
                                    fyVo.FRESULT = drFy["FRESULT"].ToString() == "" ? "-" : drFy["FRESULT"].ToString();
                                    fyVo.FZGBH = drFy["FZGBH"].ToString() == "" ? "-" : drFy["FZGBH"].ToString();
                                    fyVo.FZG = drFy["FZG"].ToString() == "" ? "-" : drFy["FZG"].ToString();
                                    fyVo.FBABYSUC = drFy["FBABYSUC"].ToString() == "" ? "0" : drFy["FBABYSUC"].ToString();
                                    fyVo.FHXBH = drFy["FHXBH"].ToString() == "" ? "-" : drFy["FHXBH"].ToString();
                                    fyVo.FHX = drFy["FHX"].ToString() == "" ? "-" : drFy["FHX"].ToString();
                                    fyVo.FPRN = drFy["FPRN"].ToString();
                                    firstPageVo.lstFyVo.Add(fyVo);
                                }
                            }
                        }
                        #endregion

                        #region 数据集（肿瘤卡）

                        if (dtZl != null && dtZl.Rows.Count > 0)
                        {
                            DataRow[] drrZl = dtZl.Select("Patient_Id = '" + registerId + "'");
                            if (drrZl.Length > 0)
                            {
                                EntityZlksj zlVo = null;
                                firstPageVo.lstZlVo = new List<EntityZlksj>();
                                for (int i = 0; i < drrZl.Length; i++)
                                {
                                    DataRow drZl = drrZl[i];
                                    zlVo = new EntityZlksj();
                                    zlVo.FFLFSBH = drZl["FFLFSBH"].ToString();
                                    zlVo.FFLFS = drZl["FFLFS"].ToString();
                                    zlVo.FFLCXBH = drZl["FFLCXBH"].ToString();
                                    zlVo.FFLCX = drZl["FFLCX"].ToString();
                                    zlVo.FFLZZBH = drZl["FFLZZBH"].ToString();
                                    zlVo.FFLZZ = drZl["FFLZZ"].ToString();
                                    zlVo.FYJY = drZl["FYJY"].ToString();
                                    zlVo.FYCS = drZl["FYCS"].ToString();
                                    zlVo.FYTS = drZl["FYTS"].ToString();
                                    zlVo.FYRQ1 = Function.Datetime(drZl["FYRQ1"]).ToString("yyyyMMdd");
                                    zlVo.FYRQ2 = Function.Datetime(drZl["FYRQ2"]).ToString("yyyyMMdd");
                                    zlVo.FQJY = drZl["FQJY"].ToString();
                                    zlVo.FQCS = drZl["FQCS"].ToString();
                                    zlVo.FQTS = drZl["FQTS"].ToString();
                                    zlVo.FQRQ1 = Function.Datetime(drZl["FQRQ1"]).ToString("yyyyMMdd");
                                    zlVo.FQRQ2 = Function.Datetime(drZl["FQRQ2"]).ToString("yyyyMMdd");
                                    zlVo.FZNAME = drZl["FZNAME"].ToString();
                                    zlVo.FZJY = drZl["FZJY"].ToString();
                                    zlVo.FZCS = drZl["FZCS"].ToString();
                                    zlVo.FZTS = drZl["FZTS"].ToString();
                                    zlVo.FZRQ1 = Function.Datetime(drZl["FZRQ1"]).ToString("yyyyMMdd");
                                    zlVo.FZRQ2 = Function.Datetime(drZl["FZRQ2"]).ToString("yyyyMMdd");
                                    zlVo.FHLFSBH = drZl["FHLFSBH"].ToString();
                                    zlVo.FHLFS = drZl["FHLFS"].ToString();
                                    zlVo.FHLFFBH = drZl["FHLFFBH"].ToString();
                                    zlVo.FHLFF = drZl["FHLFF"].ToString();
                                    zlVo.FPRN = drZl["FPRN"].ToString();
                                    if (string.IsNullOrEmpty(zlVo.FFLFSBH) || string.IsNullOrEmpty(zlVo.FHLFSBH))
                                        continue;

                                    firstPageVo.lstZlVo.Add(zlVo);
                                }
                            }
                        }
                        #endregion

                        data.Add(firstPageVo);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }


            return data;
        }
        #endregion

        #region 出院小结 嘉禾
        public List<EntityCyxj> GetPatCyxjFromJH(string registerIdStr)
        {
            List<EntityCyxj> data = null;
            EntityCyxj xjVo = null;
            SqlHelper svc = null;
            try
            {
                svc = new SqlHelper(EnumBiz.interfaceDB);
                string sql = "select * from jhemr.sp3_3022  a where a.patient_id in (" + registerIdStr + ")";
                DataTable dtXj = svc.GetDataTable(sql);

                #region 上传信息 出院小结

                if (dtXj != null && dtXj.Rows.Count > 0)
                {
                    data = new List<EntityCyxj>();
                    foreach (DataRow dr in dtXj.Rows)
                    {
                        xjVo = new EntityCyxj();
                        xjVo.registerId = dr["patient_id"].ToString();
                        xjVo.ZYH = dr["ZYH"].ToString();
                        xjVo.MZZD = dr["MZZD"].ToString();
                        if (string.IsNullOrEmpty(xjVo.MZZD))
                            xjVo.MZZD = "-";
                        if (xjVo.MZZD.Length > 100)
                            xjVo.MZZD = xjVo.MZZD.Substring(0, 100);
                        xjVo.RYZD = dr["RYZD"].ToString().Trim();
                        xjVo.CYZD = dr["CYZD"].ToString().Trim();
                        if (dr["CYZD"] == DBNull.Value)
                            xjVo.CYZD = "-";
                        xjVo.XM = dr["XM"].ToString();
                        xjVo.XB = dr["XB"].ToString();
                        if (xjVo.XB == "男")
                            xjVo.XB = "1";
                        else if (xjVo.XB == "女")
                            xjVo.XB = "2";
                        else xjVo.XB = "9";
                        xjVo.NL = dr["NL"].ToString();
                        xjVo.GMSFHM = dr["GMSFHM"].ToString();
                        xjVo.RYRQ = Function.Datetime(dr["RYRQ"]).ToString("yyyyMMdd");
                        xjVo.CYRQ = Function.Datetime(dr["CYRQ"]).ToString("yyyyMMdd");
                        xjVo.ZYTS = dr["ZYTS"].ToString();

                        xjVo.JG = dr["JG"].ToString();
                        if (string.IsNullOrEmpty(xjVo.JG))
                            xjVo.JG = "无";
                        xjVo.YJDZ = dr["YJDZ"].ToString();
                        if (string.IsNullOrEmpty(xjVo.YJDZ))
                            xjVo.YJDZ = "-";
                        xjVo.CYYZ = dr["CYYZ"].ToString().Trim();
                        if (string.IsNullOrEmpty(xjVo.CYYZ))
                            xjVo.CYYZ = "-";
                        xjVo.RYQK = dr["RYQK"].ToString().Trim();
                        if (string.IsNullOrEmpty(xjVo.RYQK))
                            xjVo.RYQK = "-";
                        if(xjVo.RYQK.Length > 1000)
                        {
                            xjVo.RYQK = xjVo.RYQK.Substring(0, 1000);
                        }
                        xjVo.YSQM = dr["YSQM"].ToString().Trim();
                        if (string.IsNullOrEmpty(xjVo.YSQM))
                            xjVo.YSQM = "-";
                        xjVo.RYHCLGC = dr["ZLJG"].ToString().Trim();
                        xjVo.CYSQK = dr["CYSQK"].ToString().Trim();
                        if (string.IsNullOrEmpty(xjVo.CYSQK))
                            xjVo.CYSQK = "-";
                        xjVo.ZLJG = dr["ZLJG"].ToString().Trim();
                        if (xjVo.ZLJG.Length > 1000)
                            xjVo.ZLJG = xjVo.ZLJG.Substring(0, 1000);
                        if (string.IsNullOrEmpty(xjVo.ZLJG))
                            xjVo.ZLJG = "-";

                        data.Add(xjVo);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }

            return data;
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
                                    tumor,t,n,m,installments,metastasiscount,
  a.directordt, --科主任
                    a.subdirectordt,--副主任
                    a.dt,--主治医生
                    a.inhospitaldt,--住院医师
                    a.attendinforadvancesstudydt,
                    a.graduatestudentintern,--责任护士
                    a.intern,
                    a.coder,--编码员
                    a.qcdt,--质控医师
                    a.qcnurse,--质控护士
f_getempnamebyid(a.doctor)  doctorname,
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
                        DateTime dteMax = Function.Datetime(dt1.Rows[0]["opendate"]);

                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            DateTime dteTmp = Function.Datetime(dr1["opendate"]);

                            if (dteTmp > dteMax)
                                dteMax = dteTmp;
                        }

                        opendate = dteMax.ToString("yyyy-MM-dd HH:mm:ss");
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

                            if (upVo.fpVo.FMZZD.Length > 50)
                            {
                                upVo.fpVo.FMZZD = upVo.fpVo.FMZZD.Substring(0, 50);
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
                            upVo.fpVo.FKZRBH = dr["directordt"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FKZRBH))
                                upVo.fpVo.FKZRBH = "-";
                            upVo.fpVo.FKZR = dr["directordtname"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FKZR))
                                upVo.fpVo.FKZR = "-";

                            upVo.fpVo.FZRDOCTBH = dr["subdirectordt"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FZRDOCTBH))
                                upVo.fpVo.FZRDOCTBH = "-";
                            upVo.fpVo.FZRDOCTOR = dr["subdirectordtname"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FZRDOCTOR))
                                upVo.fpVo.FZRDOCTOR = "-";

                            upVo.fpVo.FZZDOCTBH = dr["dt"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FZZDOCTBH))
                                upVo.fpVo.FZZDOCTBH = "-";
                            upVo.fpVo.FZZDOCT = dr["dtname"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FZZDOCT))
                                upVo.fpVo.FZZDOCT = "-";

                            upVo.fpVo.FZYDOCTBH = dr["inhospitaldt"].ToString();
                            upVo.fpVo.FZYDOCT = dr["inhospitaldtname"].ToString();

                            upVo.fpVo.FNURSEBH = dr["graduatestudentintern"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FNURSEBH))
                                upVo.fpVo.FNURSEBH = "-";
                            upVo.fpVo.FNURSE = dr["graduatestudentintern"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FNURSE))
                                upVo.fpVo.FNURSE = "-";

                            upVo.fpVo.FJXDOCTBH = dr["attendinforadvancesstudydt"].ToString();
                            upVo.fpVo.FJXDOCT = dr["attendinforadvancesstudydtname"].ToString();

                            upVo.fpVo.FSXDOCTBH = dr["intern"].ToString();
                            upVo.fpVo.FSXDOCT = dr["internname"].ToString();

                            upVo.fpVo.FBMYBH = dr["coder"].ToString();
                            upVo.fpVo.FBMY = dr["codername"].ToString();

                            upVo.fpVo.FZKDOCTBH = dr["qcdt"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FZKDOCTBH))
                                upVo.fpVo.FZKDOCTBH = "-";
                            upVo.fpVo.FZKDOCT = dr["qcdtname"].ToString();
                            if (string.IsNullOrEmpty(upVo.fpVo.FZKDOCT))
                                upVo.fpVo.FZKDOCT = "-";

                            upVo.fpVo.FZKNURSEBH = dr["qcnurse"].ToString();
                            upVo.fpVo.FZKNURSE = dr["qcnursename"].ToString();

                            upVo.fpVo.FZKRQ = Function.Datetime(dr["QCTIME"]).ToString("yyyyMMdd");

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
            if (voM.FJBNAME.Length > 50)
                voM.FJBNAME = voM.FJBNAME.Substring(0, 50);
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
            if (!string.IsNullOrEmpty(PoisoningReson))
            {
                EntityBrzdxx voP = new EntityBrzdxx();
                voP.FJBNAME = PoisoningReson;
                if (string.IsNullOrEmpty(voP.FJBNAME))
                    voP.FJBNAME = "-";

                if (voP.FJBNAME.Length > 50)
                    voP.FJBNAME = voP.FJBNAME.Substring(0, 50);
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
                    if (vo.FJBNAME.Length > 50)
                        vo.FJBNAME = vo.FJBNAME.Substring(0, 50);
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
                    if (string.IsNullOrEmpty(vo.FJBNAME))
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
                            vo.FMAZUI = vo.FMAZUI.Substring(0, 15);
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
                    else
                    {
                        vo.FZGBH = "3";
                    }

                    vo.FZG = drTemp["InfantResult"].ToString();
                    if (string.IsNullOrEmpty(vo.FZG))
                        vo.FZG = "出院";
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
                            if (string.IsNullOrEmpty(zkVo.FZKTYKH))
                                continue;
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
                            if (dr["FJBNAME"].ToString().Length > 100)
                                zdVo.FJBNAME = dr["FJBNAME"].ToString().Substring(0, 100);
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
                        int n = 0;
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
                            fopVo.FPXH = ++n;
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
                        Log.Output("中医院病人附加信息-->" + lstUpVo[i].fpVo.ZYH);
                        foreach (DataRow dr in DtCh.Rows)
                        {
                            zyVo = new EntityZyybrfjxx();
                            zyVo.FPRN = dr["FPRN"].ToString();
                            //zyVo.FPRN = dr["FFLFSBH"].ToString();
                            zyVo.FZLLBBH = dr["FZLLBBH"].ToString();
                            zyVo.FZLLB = dr["FZLLB"].ToString();
                            zyVo.FZZZYBH = dr["FZZZYBH"].ToString();
                            zyVo.FZZZY = dr["FZZZY"].ToString();
                            zyVo.FRYCYBH = dr["FRYCYBH"].ToString();
                            zyVo.FRYCY = dr["FRYCY"].ToString();
                            zyVo.FMZZYZDBH = dr["FMZZYZDBH"].ToString();
                            zyVo.FMZZYZD = dr["FMZZYZD"].ToString();
                            zyVo.FSSLCLJBH = dr["FSSLCLJBH"].ToString();
                            zyVo.FSSLCLJ = dr["FSSLCLJ"].ToString();
                            zyVo.FSYJGZJBH = dr["FSYJGZJBH"].ToString();
                            zyVo.FSYJGZJ = dr["FSYJGZJ"].ToString();
                            zyVo.FSYZYSBBH = dr["FSYZYSBBH"].ToString();
                            zyVo.FSYZYSB = dr["FSYZYSB"].ToString();
                            zyVo.FSYZYJSBH = dr["FSYZYJSBH"].ToString();
                            zyVo.FSYZYJS = dr["FSYZYJS"].ToString();
                            zyVo.FBZSHBH = dr["FBZSHBH"].ToString();
                            zyVo.FBZSH = dr["FBZSH"].ToString();

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
                        string sql = @"select * from t_upload where REGISTERID = '" + item.REGISTERID + "'";
                        DataTable dt = svc.GetDataTable(sql);
                        if (type == 0)
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                item.UPLOADDATE = DateTime.Now;
                                Sql = @"update t_upload set status = ?, UPLOADDATE = ? ,first = ?, firstMsg = ?,firstSource= ? where REGISTERID = ?";
                                if (item.Issucess == 1)
                                {
                                    IDataParameter[] parm = svc.CreateParm(6);
                                    parm[0].Value = 1;
                                    parm[1].Value = item.UPLOADDATE;
                                    parm[2].Value = 0;
                                    parm[3].Value = "";
                                    parm[4].Value = item.firstSource;
                                    parm[5].Value = item.REGISTERID;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                                else if (item.Issucess == -1)
                                {
                                    if (item.STATUS == 1 && item.first == 1)
                                        continue;
                                    IDataParameter[] parm = svc.CreateParm(6);
                                    parm[0].Value = "";
                                    parm[1].Value = item.UPLOADDATE;
                                    parm[2].Value = -1;
                                    parm[3].Value = item.FailMsg;
                                    parm[4].Value = item.firstSource;
                                    parm[5].Value = item.REGISTERID;
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
                                Sql = @"update t_upload set UPLOADDATE = ? ,xj = ?, xjMsg = ?,xjSource = ? where REGISTERID = ?";
                                if (item.Issucess == 1)
                                {
                                    IDataParameter[] parm = svc.CreateParm(5);
                                    parm[0].Value = item.UPLOADDATE;
                                    parm[1].Value = 0;
                                    parm[2].Value = "";
                                    parm[3].Value = item.xjSource;
                                    parm[4].Value = item.REGISTERID;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                                else if (item.Issucess == -1)
                                {
                                    if (item.STATUS == 1 && item.xj == 1)
                                        continue;
                                    IDataParameter[] parm = svc.CreateParm(5);
                                    parm[0].Value = item.UPLOADDATE;
                                    parm[1].Value = -1;
                                    parm[2].Value = item.FailMsg;
                                    parm[3].Value = item.xjSource;
                                    parm[4].Value = item.REGISTERID;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
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
                                        firstsource,
                                        first,
                                        xj,
                                        firstMsg,
                                        xjMsg from t_upload where (first = -1 or xj= -1) ";
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

                        vo.INPATIENTDATE = Function.Datetime(Function.Datetime(dr["INPATIENTDATE"]).ToString("yyyy-MM-dd"));
                        vo.OUTHOSPITALDATE = Function.Datetime(Function.Datetime(dr["OUTHOSPITALDATE"]).ToString("yyyy-MM-dd"));

                        vo.firstMsg = dr["firstMsg"].ToString();
                        vo.xjMsg = dr["xjMsg"].ToString();
                        vo.firstSource = Function.Int(dr["firstSource"]);
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
