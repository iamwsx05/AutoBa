using Common.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace AutoBa
{
    public partial class frmAutoBa : frmBaseMdi
    {
        public frmAutoBa()
        {
            InitializeComponent();
        }

        #region 变量
        string timePoint = " 01:17:00";
        List<string> timePointList = new List<string> { "01:17" , "18:17" };
        static bool isExecing { get; set; }
        EntityDGExtra exVo = null;
        List<EntityPatUpload> dataSource = null;
        #endregion

        #region 方法

        #region Init
        private void Init()
        {
            DateTime dteStart;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(-1);
            dteStart = dateTime.AddDays(-6);

            string text = dateTime.ToString("yyyy-MM-dd");
            string strStart = dteStart.ToString("yyyy-MM-dd");
            this.dteStart.Text = strStart;
            this.dteEnd.Text = text;
            dateTime = DateTime.Now;
            dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(1.0);
            this.lblExecTime.Text = dateTime.ToString("yyyy-MM-dd") + this.timePoint;
            this.lblInfo.Visible = false;
            this.lblInfo.BringToFront();
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            workingArea = Screen.PrimaryScreen.WorkingArea;
            this.lblInfo.Location = new Point((workingArea.Width - this.lblInfo.Width) / 2, (workingArea.Height - this.lblInfo.Height) / 2);


            exVo = new EntityDGExtra();
            exVo.YYBH = ctlUploadSbPublic.strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
            exVo.FWSJGDM = ctlUploadSbPublic.strReadXML("DGCSZYYB", "FWSJGDM", "AnyOne");
            exVo.JBR = ctlUploadSbPublic.strReadXML("DGCSZYYB", "JBR", "AnyOne"); ;// 操作员工号
            string strPwd = ctlUploadSbPublic.strReadXML("DGCSZYYB", "PASSWORDZY", "AnyOne");
            //this.Query();
            this.QueryFail();
        }
        #endregion

        #region Exec
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        private void Exec(string dteStart, string dteEnd)
        {
            List<EntityParm> dicParm = new List<EntityParm>();
            string beginDate = string.Empty;
            string endDate = string.Empty;
            beginDate = dteStart;
            endDate = dteEnd;

            string msg = string.Empty;
            string msg2 = string.Empty;
            int failCount = 0;
            int successCount = 0;

            if (beginDate != string.Empty && endDate != string.Empty)
            {
                dicParm.Add(Function.GetParm("queryDate", beginDate + "|" + endDate));
            }

            dicParm.Add(Function.GetParm("chkStat", "1"));

            if (dicParm.Count > 0)
            {
                this.lblInfo.Visible = true;

                #region 病案首页
                UploadBiz biz = new UploadBiz();
                dataSource = biz.GetPatList(dicParm);
                MthFirstPageUpload();
                foreach (EntityPatUpload item in dataSource)
                {
                    if (item.fpVo != null && item.Issucess == -1)
                    {
                        failCount++;
                        msg += item.FailMsg + "\n";
                    }
                    else if (item.fpVo != null && item.Issucess == 1)
                        successCount++;
                }
                msg = "病案首页上传-->" + Environment.NewLine + "上传成功：" + successCount.ToString() + "   上传失败：" + failCount.ToString() + "\n\n" + msg;
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
                        msg += item.FailMsg + "\n";
                    }
                    else if (item.xjVo != null && item.Issucess == 1)
                        successCount++;
                }
                msg = "出院小结上传-->" + Environment.NewLine + "上传成功：" + successCount.ToString() + "   上传失败：" + failCount.ToString() + "\n\n" + msg; ;
                Log.Output(msg);
                #endregion

                dataSource = biz.GetPatList(dicParm);
                this.gcData.DataSource = dataSource;
            }

            this.Query();
            this.QueryFail();
            this.lblInfo.Visible = false;
        }
        #endregion

        #region Query
        private void Query()
        {
            List<EntityParm> dicParm = new List<EntityParm>();
            string beginDate = string.Empty;
            string endDate = string.Empty;
            beginDate = dteStart.Text.Trim();
            endDate = dteEnd.Text.Trim();

            if (beginDate != string.Empty && endDate != string.Empty)
            {
                if (Function.Datetime(beginDate + " 00:00:00") > Function.Datetime(endDate + " 00:00:00"))
                {
                    DialogBox.Msg("开始时间不能大于结束时间。");
                    return;
                }
                dicParm.Add(Function.GetParm("queryDate", beginDate + "|" + endDate));
            }

            if (this.txtCardNo.Text.Trim() != string.Empty)
            {
                dicParm.Add(Function.GetParm("cardNo", this.txtCardNo.Text.Trim()));
            }
            if (this.txtJZJLH.Text.Trim() != string.Empty)
            {
                dicParm.Add(Function.GetParm("JZJLH", this.txtJZJLH.Text.Trim()));
            }
            if (this.chkSZ.Checked == true)
            {
                dicParm.Add(Function.GetParm("chkStat", this.chkSZ.CheckState.ToString()));
            }
            try
            {
                uiHelper.BeginLoading(this);
                if (dicParm.Count > 0)
                {
                    UploadBiz biz = new UploadBiz();
                    dataSource = biz.GetPatList(dicParm);
                    this.gcData.DataSource = dataSource;
                }
                else
                {
                    DialogBox.Msg("请输入查询条件。");
                }
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion


        #region QueryBa
        private void QueryBa()
        {
            List<EntityParm> dicParm = new List<EntityParm>();
            string beginDate = string.Empty;
            string endDate = string.Empty;
            beginDate = dteStart.Text.Trim();
            endDate = dteEnd.Text.Trim();

            if (beginDate != string.Empty && endDate != string.Empty)
            {
                if (Function.Datetime(beginDate + " 00:00:00") > Function.Datetime(endDate + " 00:00:00"))
                {
                    DialogBox.Msg("开始时间不能大于结束时间。");
                    return;
                }
                dicParm.Add(Function.GetParm("queryDate", beginDate + "|" + endDate));
            }

            if (this.txtCardNo.Text.Trim() != string.Empty)
            {
                dicParm.Add(Function.GetParm("cardNo", this.txtCardNo.Text.Trim()));
            }
            if (this.txtJZJLH.Text.Trim() != string.Empty)
            {
                dicParm.Add(Function.GetParm("JZJLH", this.txtJZJLH.Text.Trim()));
            }
            if (this.chkSZ.Checked == true)
            {
                dicParm.Add(Function.GetParm("chkStat", this.chkSZ.CheckState.ToString()));
            }
            try
            {
                uiHelper.BeginLoading(this);
                if (dicParm.Count > 0)
                {
                    UploadBiz biz = new UploadBiz();
                    dataSource = biz.GetPatList(dicParm);
                    this.gcData.DataSource = dataSource;
                }
                else
                {
                    DialogBox.Msg("请输入查询条件。");
                }
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region QueryFail
        /// <summary>
        /// 获取上传失败信息
        /// </summary>
        internal void   QueryFail()
        {
            this.gcFailData.DataSource = new UploadBiz().GetFailPatList();
        }
        #endregion

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

        #endregion

        #region 事件

        #region timer_Tick
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            if (timePointList.Contains(DateTime.Now.ToString("HH:mm")))
            {
                try
                {
                    if (isExecing)
                        return;

                    isExecing = true;
                    this.Exec(dateTime.AddDays(-30).ToString("yyyy-MM-dd"), dateTime.ToString("yyyy-MM-dd"));
                }
                catch (Exception ex)
                {
                    ExceptionLog.OutPutException(ex);
                }
                finally
                {
                    isExecing = false;
                    this.lblExecTime.Text = dateTime.AddDays(1).ToString("yyyy-MM-dd") + this.timePoint;
                }
            }
            else
            {
                dateTime = Convert.ToDateTime(this.lblExecTime.Text);
                TimeSpan timeSpan = dateTime.Subtract(DateTime.Now);
                this.lblCountDown.Text = timeSpan.Hours + "时" + timeSpan.Minutes + "分" + timeSpan.Seconds + "秒";
            }
        }
        #endregion

        #region Form1_Load
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Init();
        }
        #endregion

        #region Form1_SizeChanged
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.notifyIcon.Visible = true;
            }
        }
        #endregion

        #region notifyIcon_MouseDoubleClick
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.notifyIcon.Visible = false;
            }
        }
        #endregion

        #region Form1_FormClosing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
            }
            else
            {
                if (MessageBox.Show("确定退出任务？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region btnQuery_Click
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
            this.Query();
        }
        #endregion

        #region btnUpload_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            #region 病案首页
            this.tabControl1.SelectedIndex = 1;
            this.lblInfo.Visible = true;
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

            this.lblInfo.Visible = false;

            this.Query();
            this.QueryFail();
        }
        #endregion

        #region GetLstRowObject
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

        #region  gvData_RowCellStyle
        private void gvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == this.gvData.FocusedColumn && e.RowHandle == this.gvData.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.FromArgb(251, 165, 8);
                e.Appearance.BackColor2 = Color.White;
            }

            int hand = e.RowHandle;
            if (hand < 0) return;
            EntityPatUpload vo = this.gvData.GetRow(hand) as EntityPatUpload;
            if (vo.SZ == "已上传")
                e.Appearance.ForeColor = Color.FromArgb(0, 0, 156);

            this.gvData.Invalidate();
        }
        #endregion

        #endregion

        private void btnQueryBa_Click(object sender, EventArgs e)
        {
            List<EntityQueryBa> dataBa = new List<EntityQueryBa>();
            List<EntityQueryBa> dataIcare = new List<EntityQueryBa>();
            string parm = txtJzjlh2.Text;
            string beginDate = dteStart.Text;
            string endDate = dteEnd.Text;
            if (beginDate != string.Empty && endDate != string.Empty)
            {
                if (Function.Datetime(beginDate + " 00:00:00") > Function.Datetime(endDate + " 00:00:00"))
                {
                    DialogBox.Msg("开始时间不能大于结束时间。");
                    return;
                }
            }
            new UploadBiz().GetQuerypat(beginDate,endDate,parm, out dataIcare, out dataBa);

            this.gcBa.DataSource = dataBa;
            this.gcIcare.DataSource = dataIcare;
        }
    }
}
