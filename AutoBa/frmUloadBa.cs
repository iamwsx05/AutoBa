using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Common.Controls;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace AutoBa
{
    public partial class frmUloadBa : frmBaseMdi
    {
        public frmUloadBa()
        {
            InitializeComponent();
        }
        List<EntityPatUpload> dataSource = null;
        private void frmUloadBa_Load(object sender, EventArgs e)
        {

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Query();
        }

        private void Query()
        {
            List<EntityParm> dicParm = new List<EntityParm>();

            //string jzjlhStr = "('189576383', '199460220', '202730452', '204125302', '01574135', '203957487', '196880183', '203142067', '202560312')";
            string jzjlhStr = @"('189576383',
'199460220',
'199463489',
'199596986',
'200304225',
'200307830',
'200547006',
'202730452',
'200752087',
'204732796',
'203874779',
'203797551',
'204125302',
'203933618',
'205839905',
'206626911',
'208693559',
'207872778',
'207958317',
'207696994',
'208099307',
'202560312',
'208033616',
'204370663',
'207255678',
'203554022',
'205320816',
'203142067',
'197556771',
'198648310',
'198362305',
'198538347',
'201085521',
'209368426',
'207659226',
'208959995',
'197608985',
'205378093',
'204870057',
'206555334',
'209200110',
'200157170',
'201574135',
'202742235',
'203957487',
'197757240',
'209224579',
'208846076',
'206303937',
'196880183',
'199893779',
'204252210',
'206300414',
'198876721',
'201656303'
)";



            if (this.txtCardNo.Text.Trim() != string.Empty)
            {
                dicParm.Add(Function.GetParm("cardNo", this.txtCardNo.Text.Trim()));
            }
            //if (this.txtJZJLH.Text.Trim() != string.Empty)
            {
                //dicParm.Add(Function.GetParm("JZJLH", this.txtJZJLH.Text.Trim()));
                 dicParm.Add(Function.GetParm("JZJLH1", jzjlhStr));
            }
            if (this.chkSZ.Checked == true)
            {
                dicParm.Add(Function.GetParm("chkStat", this.chkSZ.CheckState.ToString()));
            }
            try
            {
                uiHelper.BeginLoading(this);

                UploadBiz biz = new UploadBiz();
                dataSource = biz.GetPatList(dicParm);
                
                this.gcData.DataSource = dataSource;

            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            #region 病案首页
            string msg = string.Empty;
            string msg2 = string.Empty;
            int failCount = 0;
            int successCount = 0;
            string jzjlh = string.Empty;
            List<EntityParm> dicParm = new List<EntityParm>();
            dataSource = GetLstRowObject();
            MthFirstPageUpload();
            foreach (EntityPatUpload item in dataSource)
            {
                if (item.fpVo != null && item.Issucess == -1)
                {
                    failCount++;
                    msg += item.FailMsg + Environment.NewLine;
                }
                else if (item.fpVo != null && item.Issucess == 1)
                    successCount++;
                jzjlh += "'" + item.JZJLH + "',";
            }
            msg = "病案首页-->" + Environment.NewLine + "上传成功：" + successCount.ToString() + "   上传失败：" + failCount.ToString() + "\n\n" + msg;
            Log.Output(msg);

            #endregion

            #region 出院小结上传
            successCount = 0;
            failCount = 0;
            MthCyxjUpload();
            msg = string.Empty;
            foreach (EntityPatUpload item in dataSource)
            {
                if (item.xjVo != null && item.Issucess == -1)
                {
                    failCount++;
                    msg += item.FailMsg + Environment.NewLine;
                }
                else if (item.xjVo != null && item.Issucess == 1)
                    successCount++;
            }
            msg = "出院小结上传-->" + Environment.NewLine + "上传成功：" + successCount.ToString() + "   上传失败：" + failCount.ToString() + "\n\n" + msg;
            Log.Output(msg);
            #endregion

            this.Query();
        }

        #region 首页数据上传
        /// <summary>
        /// 数据上传
        /// </summary>
        public void MthFirstPageUpload()
        {
            long lngRes = -1;
            List<EntityPatUpload> data = new List<EntityPatUpload>();

            try
            {
                string strUser = ctlUploadSbPublic.strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
                string strPwd = ctlUploadSbPublic.strReadXML("DGCSZYYB", "PASSWORDZY", "AnyOne");
                lngRes = ctlUploadSbPublic.lngUserLoin(strUser, strPwd, false);
                if (lngRes > 0)
                {
                    EntityDGExtra extraVo = new EntityDGExtra();
                    extraVo.YYBH = ctlUploadSbPublic.strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
                    extraVo.JBR = ctlUploadSbPublic.strReadXML("DGCSZYYB", "JBR", "AnyOne"); ;// 操作员工号
                    extraVo.FWSJGDM = ctlUploadSbPublic.strReadXML("DGCSZYYB", "FWSJGDM", "AnyOne");
                    System.Text.StringBuilder strValue = null;

                    UploadBiz biz = new UploadBiz();
                    dataSource = biz.GetPatFirstInfo(dataSource);

                    lngRes = ctlUploadSbPublic.lngFunSP3_3021(ref dataSource, extraVo, ref strValue);

                    if (biz.SavePatFirstPage(dataSource,0) >= 0)
                    {
                        lngRes = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException("MthFirstPageUpload-->" + ex);
            }
            finally
            {
            }
        }
        #endregion

        #region 出院小结数据上传
        /// <summary>
        /// 数据上传
        /// </summary>
        public void MthCyxjUpload()
        {
            try
            {
                long lngRes = 1;

                string strUser = ctlUploadSbPublic.strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
                string strPwd = ctlUploadSbPublic.strReadXML("DGCSZYYB", "PASSWORDZY", "AnyOne");
                lngRes = ctlUploadSbPublic.lngUserLoin(strUser, strPwd, false);
                if (lngRes > 0)
                {
                    EntityDGExtra extraVo = new EntityDGExtra();
                    extraVo.YYBH = ctlUploadSbPublic.strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
                    extraVo.JBR = ctlUploadSbPublic.strReadXML("DGCSZYYB", "JBR", "AnyOne");// 操作员工号
                    System.Text.StringBuilder strValue = null;
                    lngRes = ctlUploadSbPublic.lngFunSP3_3022(ref dataSource, extraVo, ref strValue);

                    UploadBiz biz = new UploadBiz();
                    if (biz.SavePatFirstPage(dataSource,1) >= 0)
                    {
                        lngRes = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException("MthCyxjUpload-->" + ex);
            }
            finally
            {
            }
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<EntityPatUpload> GetLstRowObject()
        {
            List<EntityPatUpload> data = new List<EntityPatUpload>();
            EntityPatUpload vo = null;
            string value = string.Empty;

            int[] rownumber = this.gvData.GetSelectedRows();//获取选中行号；
            for (int i = 0; i < rownumber.Length; i++)
            {
                vo = gvData.GetRow(rownumber[i]) as EntityPatUpload;
                data.Add(vo);
            }
            return data;
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            uiHelper.ExportToXls(gvData);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            
        }
    }
}