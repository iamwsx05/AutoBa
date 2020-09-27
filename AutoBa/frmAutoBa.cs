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
        string timePoint = " 05:17:00";
        List<string> timePointList = new List<string> { "05:17" };
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
            this.Query();
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
            if(this.chkShowYb.Checked == true)
            {
                dicParm.Add(Function.GetParm("chkShowYb", this.chkShowYb.CheckState.ToString()));
            }
            if(this.chkFromIcare.Checked == true)
            {
                dicParm.Add(Function.GetParm("chkFromIcare", this.chkFromIcare.CheckState.ToString()));
            }
            try
            {
                uiHelper.BeginLoading(this);
                if (dicParm.Count > 0)
                {
                    UploadBiz biz = new UploadBiz();
                    dataSource = biz.GetPatList(dicParm);
                    this.gcData.DataSource = dataSource;
                    this.gcData.RefreshDataSource();
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
        internal void  QueryFail()
        {
            this.gcFailData.DataSource = new UploadBiz().GetFailPatList();
            this.gcFailData.RefreshDataSource();
        }
        #endregion

        #region Query
        private void Query2()
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
            string jzjhl = @"('230494920',
'230208227',
'230331251',
'230068879',
'230434399',
'230774105',
'230805413',
'229319298',
'230268245',
'229009108',
'230281992',
'230121317',
'230538150',
'230199176',
'230478422',
'230479082',
'230009160',
'229633633',
'229891347',
'229821757',
'230539225',
'230760178',
'229823702',
'230325247',
'230533085',
'230718683',
'230557180',
'230541666',
'230701600',
'230001573',
'230731466',
'225302911',
'230954255',
'230680212',
'230437560',
'230326192',
'230469496',
'230536611',
'230797741',
'230268202',
'230447988',
'230299219',
'230570603',
'230901619',
'231010275',
'230845842',
'230682942',
'230392817',
'231019215',
'230943115',
'231055434',
'230881160',
'230179263',
'230078424',
'229440525',
'229239963',
'230707501',
'230877214',
'231104945',
'230962880',
'230937789',
'230950843',
'230726507',
'230543117',
'230796157',
'230896279',
'231140993',
'226777558',
'230760056',
'230804468',
'230713169',
'230659932',
'231041135',
'230902878',
'231054394',
'231036806',
'230723700',
'230838666',
'230622699',
'231043916',
'230473686',
'230921172',
'230635262',
'230876431',
'231016807',
'230668701',
'230532086',
'231081443',
'230576755',
'230266266',
'230759263',
'230043552',
'229805377',
'230404532',
'229693848',
'230499977',
'230546100',
'230548181',
'230032842',
'229650912',
'230891502',
'230323076',
'231133188',
'231070096',
'230747492',
'230563981',
'231200895',
'230872309',
'231086793',
'230699280',
'231169924',
'231040567',
'231040698',
'231025523',
'230083463',
'230792534',
'230820993',
'230697729',
'231034436',
'230999328',
'229961946',
'231056261',
'231303981',
'230643149',
'231113037',
'230871767',
'230657108',
'228539307',
'231207957',
'231068731',
'231202035',
'231200373',
'231102580',
'231308446',
'229618930',
'231054528',
'230179755',
'231285646',
'230411774',
'230946420',
'231377051',
'230522313',
'230631652',
'230913277',
'230900340',
'231363270',
'231158710',
'230894467',
'231017962',
'231175205',
'231215435',
'230893953',
'230791052',
'230591034',
'230791616',
'230611105',
'231422212',
'230965928',
'231033226',
'231465749',
'231329770',
'231124070',
'229549168',
'230950675',
'229900395',
'231325301',
'231180983',
'231121973',
'231027859',
'230985145',
'230591833',
'231192240',
'231500705',
'224897955',
'230964158',
'230912616',
'230844886',
'231466437',
'231379020',
'231150826',
'230675712',
'231501566',
'231346954',
'230884778',
'230898611',
'231357617',
'229597538',
'231020656',
'230527106',
'231353876',
'231163715',
'230975775',
'231020226',
'231204727',
'231340537',
'231009660',
'231217421',
'231170739',
'231170836',
'231345499',
'231387878',
'230755867',
'231201395',
'230878189',
'231036158',
'231324243',
'231228726',
'230762616',
'230654372',
'231348334',
'230175343',
'230889902',
'231621879',
'231138339',
'230474157',
'230920764',
'231524771',
'231543665',
'231530762',
'231175425',
'231546732',
'231444479',
'231623024',
'231449314',
'229709161',
'231400270',
'231580607',
'231650880',
'230766603',
'231344499',
'231147390',
'230688891',
'231594667',
'231705569',
'231573113',
'231691477',
'231433720',
'231474011',
'231697263',
'230850916',
'231272996',
'231630627',
'230806473',
'230520744',
'231336214',
'231062115',
'231574954',
'231072929',
'231261962',
'231089846',
'231418498',
'231302118',
'231574017',
'231059119',
'231392760',
'231179738',
'230945341',
'231720576',
'230250670',
'231294139',
'231661300',
'230185001',
'231592733',
'231413805',
'231484037',
'230085197',
'231364455',
'231545445',
'228707813',
'231426298',
'231219765',
'231140701',
'231818308',
'230943454',
'231334900',
'230814008',
'231849130',
'231435944',
'231465452',
'231175282',
'231485551',
'231704282',
'231811563',
'231354810')
";

         
                dicParm.Add(Function.GetParm("JZJLH1", jzjhl));
            
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
                    this.gcData.RefreshDataSource();
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
                    this.Exec(dateTime.AddDays(-15).ToString("yyyy-MM-dd"), dateTime.ToString("yyyy-MM-dd"));
                    Init();
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

        #region btnQueryBa_Click
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
        #endregion

        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Query2();
        }
    }
}
