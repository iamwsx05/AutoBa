using Common.Controls;
using Common.Entity;
using DevExpress.XtraReports.UI;
using System;
using System.Xml;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoBa
{
    public class ctlUploadSbPublic : BaseController
    {

        #region 接口函数
        /**初始化，在调用DLL前，初始化调用环境变量。整个调用工程只需调用该函数一次即可。
         * LPTSTR svrIP：代理服务器的ＩＰ地址。
         * USHORT svrPort： 代理服务器的监听端口。
         * SndBufSize：socket发送缓存大小。
         * RecvBufSize：socket接收缓存大小。
         * Return:HRESULT 1表示成功；-11表示系统初始化失败。*/
        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "Initialize")]
        public static extern int Initialize(string svrIP, int svrPort, int SndBuffSize, int RecvBuffSize);

        /**创建实例
         * 创建一个功能调用实例。在进行一个新的功能调用前必须执行该操作，以取得调用的处理句柄。返回的句柄将成为其他功能调用的入口参数。
         * Return:HANDLE 大于0的LONG型值表示创建成功；-13表示创建失败。*/
        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "CreateInstace")]
        public static extern int CreateInstace();

        /**设置入参
         * 提供功能调用的参数组，比如功能号以及其他功能的调用参数。（功能号的paramName规定为“FN”）
         * HANDLE pDataHandle：功能调用的处理句柄，由接口函数CreateInstace()创建。
         * LPCTSTR paramName：参数名称。
         * LPCTSTR paramValue：参数值。
         * Return:HRESULT 成功返回1；失败返回-14，详细的错误信息可以通过调用GetSysMessage()取得
         */
        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "SetParam")]
        public static extern int SetParam(int pDataHandle, string paramName, string paramValue);

        /**数据集*/
        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "InsertDataSet")]
        public static extern int InsertDataSet(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "InsertRow")]
        public static extern int InsertRow(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "SetField")]
        public static extern int SetField(int pDataHandle, string fieldName, string fieldValue);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "EndRow")]
        public static extern int EndRow(int pDataHandle, int rowID);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "EndDataSet")]
        public static extern int EndDataSet(int pDataHandle, string name);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "Run")]
        public static extern int Run(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetParam")]
        public static extern int GetParam(int pDataHandle, string paramName, StringBuilder paramValue, int nMaxValueLenth);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "LocateDataSet")]
        public static extern int LocateDataSet(int pDataHandle, string Name);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetRowSize")]
        public static extern int GetRowSize(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "NextRow")]
        public static extern int NextRow(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetCurrentRow")]
        public static extern int GetCurrentRow(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetColSize")]
        public static extern int GetColSize(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetFieldValue")]
        public static extern int GetFieldValue(int pDataHandle, string name, StringBuilder value, int nMaxValueLenth);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "DestroyInstance")]
        public static extern int DestroyInstance(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetSysMessage")]
        public static extern int GetSysMessage(int pDataHandle, StringBuilder pMassage, int nMaxMessage);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "EncryptWithCipher")]
        public static extern int EncryptWithCipher(int pDataHandle, string PlainData, StringBuilder EncryptedData, int nMaxValueLenth);
        #endregion

        #region 病案首页上传[SP3_3021]
        /// <summary>
        /// 病案首页上传[SP3_3021]
        /// </summary>
        /// <param name="lstVo"></param>
        /// <param name="exVo"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static long lngFunSP3_3021(ref List<EntityPatUpload> lstVo, EntityDGExtra exVo, ref StringBuilder strValue)
        {
            long lngRes = -1;
            int intRet;
            int j = 0;
            int intH = 0 ;
            string jzjlh = string.Empty;
            if (lstVo.Count > 0)
            {
                foreach (EntityPatUpload item in lstVo)
                {
                    try
                    {
                        #region 入参
                        intH = CreateInstace();
                        if (intH > 0)
                        {
                            intRet = SetParam(intH, "FN", "SP3_3021");
                            intRet = SetParam(intH, "JBR", exVo.JBR);
                            intRet = SetParam(intH, "YYBH", exVo.YYBH);
                        }

                        item.JBR = exVo.JBR;
                        item.Issucess = -1; //-1 上传失败
                        if (item.firstSource == 0)
                        {
                            item.firstMsg = "无首页信息";
                            continue;
                        }
                        jzjlh = item.JZJLH;
                        string logStr = string.Empty;
                        logStr += "JZJLH:" + item.fpVo.JZJLH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FWSJGDM:" + exVo.FWSJGDM + Environment.NewLine;//
                        logStr += "FFBBHNEW:" + item.fpVo.FFBBHNEW.ToString().Trim() + Environment.NewLine;//
                        logStr += "FFBNEW:" + item.fpVo.FFBNEW.ToString().Trim() + Environment.NewLine;//
                        logStr += "FASCARD1:" + item.fpVo.FASCARD1.ToString().Trim() + Environment.NewLine;//
                        logStr += "FTIMES:" + item.fpVo.FTIMES.ToString().Trim() + Environment.NewLine;//
                        logStr += "FPRN:" + item.fpVo.FPRN.ToString().Trim() + Environment.NewLine;//
                        logStr += "FNAME:" + item.fpVo.FNAME.ToString().Trim() + Environment.NewLine;//
                        logStr += "FSEXBH:" + item.fpVo.FSEXBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "nFSEX:" + item.fpVo.FSEX.ToString().Trim() + Environment.NewLine;//
                        logStr += "FBIRTHDAY:" + item.fpVo.FBIRTHDAY.ToString() + Environment.NewLine;//
                        logStr += "FAGE:" + item.fpVo.FAGE.ToString() + Environment.NewLine;//
                        logStr += "fcountrybh:" + item.fpVo.fcountrybh.ToString() + Environment.NewLine;//
                        logStr += "fcountry:" + item.fpVo.fcountry.ToString() + Environment.NewLine;//
                        logStr += "fnationalitybh:" + item.fpVo.fnationalitybh.ToString() + Environment.NewLine;//
                        logStr += "fnationality:" + item.fpVo.fnationality.ToString() + Environment.NewLine;//
                        logStr += "FCSTZ:" + (Function.Dec(item.fpVo.FCSTZ) / 1000).ToString("0.00") + Environment.NewLine;//
                        logStr += "FRYTZ:" + (Function.Dec(item.fpVo.FRYTZ) / 1000).ToString("0.00") + Environment.NewLine;//
                        logStr += "FBIRTHPLACE:" + item.fpVo.FBIRTHPLACE.ToString().Trim() + Environment.NewLine;//
                        logStr += "FNATIVE:" + item.fpVo.FNATIVE.ToString().Trim() + Environment.NewLine;//
                        logStr += "FIDCard:" + item.fpVo.FIDCard.ToString() + Environment.NewLine;//
                        logStr += "FJOB:" + item.fpVo.FJOB.ToString().Trim() + Environment.NewLine;//
                        logStr += "FSTATUSBH:" + item.fpVo.FSTATUSBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FSTATUS:" + item.fpVo.FSTATUS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FCURRADDR:" + item.fpVo.FCURRADDR.ToString().Trim() + Environment.NewLine;//
                        logStr += "FCURRTELE:" + item.fpVo.FCURRTELE.ToString() + Environment.NewLine;//
                        logStr += "FCURRPOST:" + item.fpVo.FCURRPOST.ToString().Trim() + Environment.NewLine;//
                        logStr += "FHKADDR:" + item.fpVo.FHKADDR.ToString().Trim() + Environment.NewLine;//
                        logStr += "FHKPOST:" + item.fpVo.FHKPOST.ToString().Trim() + Environment.NewLine;//
                        logStr += "FDWNAME:" + item.fpVo.FDWNAME.ToString().Trim() + Environment.NewLine;//
                        logStr += "FDWADDR:" + item.fpVo.FDWADDR.ToString() + Environment.NewLine;//
                        logStr += "FDWTELE:" + item.fpVo.FDWTELE.ToString().Trim() + Environment.NewLine;//
                        logStr += "FDWPOST:" + item.fpVo.FDWPOST.ToString().Trim() + Environment.NewLine;//
                        logStr += "FLXNAME:" + item.fpVo.FLXNAME.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRELATE:" + item.fpVo.FRELATE.ToString().Trim() + Environment.NewLine;//
                        logStr += "FLXADDR:" + item.fpVo.FLXADDR.ToString() + Environment.NewLine;//
                        logStr += "FLXTELE:" + item.fpVo.FLXTELE.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYTJBH:" + item.fpVo.FRYTJBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYTJ:" + item.fpVo.FRYTJ.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYDATE:" + item.fpVo.FRYDATE.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYTIME:" + item.fpVo.FRYTIME.ToString() + Environment.NewLine;//
                        logStr += "FRYTYKH:" + item.fpVo.FRYTYKH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYDEPT:" + item.fpVo.FRYDEPT.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYBS:" + item.fpVo.FRYBS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZKTYKH:" + item.fpVo.FZKTYKH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZKDEPT:" + item.fpVo.FZKDEPT.ToString() + Environment.NewLine;//
                        logStr += "FZKTIME:" + item.fpVo.FZKTIME.ToString() + Environment.NewLine;//
                        logStr += "FCYDATE:" + item.fpVo.FCYDATE.ToString().Trim() + Environment.NewLine;//
                        logStr += "FCYTIME:" + item.fpVo.FCYTIME.ToString().Trim() + Environment.NewLine;//
                        logStr += "FCYTYKH:" + item.fpVo.FCYTYKH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FCYDEPT:" + item.fpVo.FCYDEPT.ToString() + Environment.NewLine;//
                        logStr += "FCYBS:" + item.fpVo.FCYBS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FDAYS:" + item.fpVo.FDAYS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FMZZDBH:" + item.fpVo.FMZZDBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FMZZD:" + item.fpVo.FMZZD.ToString().Trim() + Environment.NewLine;//
                        logStr += "FMZDOCTBH:" + item.fpVo.FMZDOCTBH + Environment.NewLine;//
                        logStr += "FMZDOCT:" + item.fpVo.FMZDOCT.ToString().Trim() + Environment.NewLine;//
                        logStr += "FJBFXBH:" + item.fpVo.FJBFXBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FJBFX:" + item.fpVo.FJBFX.ToString().Trim() + Environment.NewLine;//
                        logStr += "FYCLJBH:" + item.fpVo.FYCLJBH.ToString() + Environment.NewLine;//
                        logStr += "FYCLJ:" + item.fpVo.FYCLJ.ToString().Trim() + Environment.NewLine;//
                        logStr += "FQJTIMES:" + item.fpVo.FQJTIMES.ToString().Trim() + Environment.NewLine;//
                        logStr += "FQJSUCTIMES:" + item.fpVo.FQJSUCTIMES.ToString().Trim() + Environment.NewLine;//
                        logStr += "FPHZD:" + item.fpVo.FPHZD.ToString().Trim() + Environment.NewLine;//
                        logStr += "FPHZDNUM:" + item.fpVo.FPHZDNUM.ToString() + Environment.NewLine;//
                        logStr += "FPHZDBH:" + item.fpVo.FPHZDBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FIFGMYWBH:" + item.fpVo.FIFGMYWBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FIFGMYW:" + item.fpVo.FIFGMYW.ToString().Trim() + Environment.NewLine;//
                        logStr += "FGMYW:" + item.fpVo.FGMYW.ToString().Trim() + Environment.NewLine;//
                        logStr += "FBODYBH:" + item.fpVo.FBODYBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FBODY:" + item.fpVo.FBODY.ToString() + Environment.NewLine;//
                        logStr += "FBLOODBH:" + item.fpVo.FBLOODBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FBLOOD:" + item.fpVo.FBLOOD.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRHBH:" + item.fpVo.FRHBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRH:" + item.fpVo.FRH.ToString() + Environment.NewLine;//
                        logStr += "FKZRBH:" + item.fpVo.FKZRBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FKZR:" + item.fpVo.FKZR.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZRDOCTBH:" + item.fpVo.FZRDOCTBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZRDOCTOR:" + item.fpVo.FZRDOCTOR.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZZDOCTBH:" + item.fpVo.FZZDOCTBH.ToString() + Environment.NewLine;//
                        logStr += "FZZDOCT:" + item.fpVo.FZZDOCT.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZYDOCTBH:" + item.fpVo.FZYDOCTBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZYDOCT:" + item.fpVo.FZYDOCT.ToString().Trim() + Environment.NewLine;//
                        logStr += "FNURSEBH:" + item.fpVo.FNURSEBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FNURSE:" + item.fpVo.FNURSE.ToString() + Environment.NewLine;//
                        logStr += "FJXDOCTBH:" + item.fpVo.FJXDOCTBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FJXDOCT:" + item.fpVo.FJXDOCT.ToString().Trim() + Environment.NewLine;//
                        logStr += "FSXDOCTBH:" + item.fpVo.FSXDOCTBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FSXDOCT:" + item.fpVo.FSXDOCT.ToString() + Environment.NewLine;//
                        logStr += "FBMYBH:" + item.fpVo.FBMYBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FBMY:" + item.fpVo.FBMY.ToString().Trim() + Environment.NewLine;//
                        logStr += "FQUALITYBH:" + item.fpVo.FQUALITYBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FQUALITY:" + item.fpVo.FQUALITY.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZKDOCTBH:" + item.fpVo.FZKDOCTBH.ToString() + Environment.NewLine;//
                        logStr += "FZKDOCT:" + item.fpVo.FZKDOCT.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZKNURSEBH:" + item.fpVo.FZKNURSEBH.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZKNURSE:" + item.fpVo.FZKNURSE.ToString().Trim() + Environment.NewLine;//
                        logStr += "FZKRQ:" + item.fpVo.FZKRQ.ToString().Trim() + Environment.NewLine;//
                        logStr += "FLYFSBH:" + item.fpVo.FLYFSBH.ToString() + Environment.NewLine;//
                        logStr += "FLYFS:" + item.fpVo.FLYFS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FYZOUTHOSTITAL:" + item.fpVo.FYZOUTHOSTITAL.ToString().Trim() + Environment.NewLine;//
                        logStr += "nFSQOUTHOSTITAL:" + item.fpVo.FSQOUTHOSTITAL.ToString().Trim() + Environment.NewLine;//
                        logStr += "FISAGAINRYBH:" + item.fpVo.FISAGAINRYBH.ToString() + Environment.NewLine;//
                        logStr += "FISAGAINRY:" + item.fpVo.FISAGAINRY.ToString().Trim() + Environment.NewLine;//
                        logStr += "FISAGAINRYMD:" + item.fpVo.FISAGAINRYMD.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYQHMDAYS:" + item.fpVo.FRYQHMDAYS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYQHMHOURS:" + item.fpVo.FRYQHMHOURS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYQHMMINS:" + item.fpVo.FRYQHMMINS.ToString() + Environment.NewLine;//
                        logStr += "FRYQHMCOUNTS:" + item.fpVo.FRYQHMCOUNTS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYHMDAYS:" + item.fpVo.FRYHMDAYS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYHMHOURS:" + item.fpVo.FRYHMHOURS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYHMMINS:" + item.fpVo.FRYHMMINS.ToString().Trim() + Environment.NewLine;//
                        logStr += "FRYHMCOUNTS:" + item.fpVo.FRYHMCOUNTS.ToString() + Environment.NewLine;//
                        logStr += "FSUM1:" + item.fpVo.FSUM1.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZFJE:" + item.fpVo.FZFJE.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZHFWLYLF:" + item.fpVo.FZHFWLYLF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZHFWLCZF:" + item.fpVo.FZHFWLCZF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZHFWLHLF:" + item.fpVo.FZHFWLHLF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZHFWLQTF:" + item.fpVo.FZHFWLQTF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZDLBLF:" + item.fpVo.FZDLBLF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZDLSSSF:" + item.fpVo.FZDLSSSF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZDLYXF:" + item.fpVo.FZDLYXF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZDLLCF:" + item.fpVo.FZDLLCF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZLLFFSSF:" + item.fpVo.FZLLFFSSF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZLLFWLZWLF:" + item.fpVo.FZLLFWLZWLF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZLLFSSF:" + item.fpVo.FZLLFSSF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZLLFMZF:" + item.fpVo.FZLLFMZF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZLLFSSZLF:" + item.fpVo.FZLLFSSZLF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FKFLKFF:" + item.fpVo.FKFLKFF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLZF:" + item.fpVo.FZYLZF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FXYF:" + item.fpVo.FXYF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FXYLGJF:" + item.fpVo.FXYLGJF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZCHYF:" + item.fpVo.FZCHYF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZCYF:" + item.fpVo.FZCYF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FXYLXF:" + item.fpVo.FXYLXF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FXYLBQBF:" + item.fpVo.FXYLBQBF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FXYLQDBF:" + item.fpVo.FXYLQDBF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FXYLYXYZF:" + item.fpVo.FXYLYXYZF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FXYLXBYZF:" + item.fpVo.FXYLXBYZF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FHCLCJF:" + item.fpVo.FHCLCJF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FHCLZLF:" + item.fpVo.FHCLZLF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FHCLSSF:" + item.fpVo.FHCLSSF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FQTF:" + item.fpVo.FQTF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FBGLX:" + item.fpVo.FBGLX.ToString() + Environment.NewLine;//
                        logStr += "GMSFHM:" + item.fpVo.GMSFHM.ToString() + Environment.NewLine;//
                        logStr += "YYBH:" + exVo.YYBH.ToString() + Environment.NewLine;//
                        logStr += "FZYF:" + item.fpVo.FZYF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZKDATE:" + item.fpVo.FZKDATE.ToString() + Environment.NewLine;//
                        logStr += "FJOBBH:" + item.fpVo.FJOBBH.ToString() + Environment.NewLine;//
                        logStr += "FZHFWLYLF01:" + item.fpVo.FZHFWLYLF01.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZHFWLYLF02:" + item.fpVo.FZHFWLYLF02.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLZDF:" + item.fpVo.FZYLZDF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLZLF:" + item.fpVo.FZYLZLF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLZLF01:" + item.fpVo.FZYLZLF01.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLZLF02:" + item.fpVo.FZYLZLF02.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLZLF03:" + item.fpVo.FZYLZLF03.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLZLF04:" + item.fpVo.FZYLZLF04.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLZLF05:" + item.fpVo.FZYLZLF05.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLZLF06:" + item.fpVo.FZYLZLF06.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLQTF:" + item.fpVo.FZYLQTF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZCLJGZJF:" + item.fpVo.FZCLJGZJF.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLQTF01:" + item.fpVo.FZYLQTF01.ToString("0.00") + Environment.NewLine;//
                        logStr += "FZYLQTF02:" + item.fpVo.FZYLQTF02.ToString("0.00") + Environment.NewLine;//
                        logStr += "ZYH:" + item.fpVo.ZYH.ToString() + Environment.NewLine;//
                        logStr += "FPHM:" + item.fpVo.FPHM.ToString() + Environment.NewLine;//
                        Log.Output(logStr);

                        if (item.firstSource == 1 || item.firstSource == 2)
                        {

                        }
                        else
                        {
                            continue;
                        }
                        intRet = SetParam(intH, "JZJLH", item.fpVo.JZJLH.ToString().Trim());//
                        intRet = SetParam(intH, "FWSJGDM", exVo.FWSJGDM);
                        intRet = SetParam(intH, "FFBBHNEW", item.fpVo.FFBBHNEW.ToString().Trim());//
                        intRet = SetParam(intH, "FFBNEW", item.fpVo.FFBNEW.ToString().Trim());
                        intRet = SetParam(intH, "FASCARD1", item.fpVo.FASCARD1.ToString().Trim());
                        intRet = SetParam(intH, "FTIMES", item.fpVo.FTIMES.ToString().Trim());
                        intRet = SetParam(intH, "FPRN", item.fpVo.FPRN.ToString().Trim());
                        intRet = SetParam(intH, "FNAME", item.fpVo.FNAME.ToString().Trim());//
                        intRet = SetParam(intH, "FSEXBH", item.fpVo.FSEXBH.ToString().Trim());//
                        intRet = SetParam(intH, "FSEX", item.fpVo.FSEX.ToString().Trim());//
                        intRet = SetParam(intH, "FBIRTHDAY", item.fpVo.FBIRTHDAY.ToString());//
                        intRet = SetParam(intH, "FAGE", item.fpVo.FAGE.ToString());
                        intRet = SetParam(intH, "fcountrybh", item.fpVo.fcountrybh.ToString());
                        intRet = SetParam(intH, "fcountry", item.fpVo.fcountry.ToString());
                        intRet = SetParam(intH, "fnationalitybh", item.fpVo.fnationalitybh.ToString());
                        intRet = SetParam(intH, "fnationality", item.fpVo.fnationality.ToString());
                        intRet = SetParam(intH, "FCSTZ", (Function.Dec(item.fpVo.FCSTZ) / 1000).ToString("0.00"));
                        intRet = SetParam(intH, "FRYTZ", (Function.Dec(item.fpVo.FRYTZ) / 1000).ToString("0.00"));
                        intRet = SetParam(intH, "FBIRTHPLACE", item.fpVo.FBIRTHPLACE.ToString().Trim());
                        intRet = SetParam(intH, "FNATIVE", item.fpVo.FNATIVE.ToString().Trim());
                        intRet = SetParam(intH, "FIDCard", item.fpVo.FIDCard);
                        intRet = SetParam(intH, "FJOB", item.fpVo.FJOB.ToString().Trim());
                        intRet = SetParam(intH, "FSTATUSBH", item.fpVo.FSTATUSBH.ToString().Trim());
                        intRet = SetParam(intH, "FSTATUS", item.fpVo.FSTATUS.ToString().Trim());
                        intRet = SetParam(intH, "FCURRADDR", item.fpVo.FCURRADDR.ToString().Trim());
                        intRet = SetParam(intH, "FCURRTELE", item.fpVo.FCURRTELE);
                        intRet = SetParam(intH, "FCURRPOST", item.fpVo.FCURRPOST.ToString().Trim());
                        intRet = SetParam(intH, "FHKADDR", item.fpVo.FHKADDR.ToString().Trim());
                        intRet = SetParam(intH, "FHKPOST", item.fpVo.FHKPOST.ToString().Trim());
                        intRet = SetParam(intH, "FDWNAME", item.fpVo.FDWNAME.ToString().Trim());
                        intRet = SetParam(intH, "FDWADDR", item.fpVo.FDWADDR);
                        intRet = SetParam(intH, "FDWTELE", item.fpVo.FDWTELE.ToString().Trim());
                        intRet = SetParam(intH, "FDWPOST", item.fpVo.FDWPOST.ToString().Trim());
                        intRet = SetParam(intH, "FLXNAME", item.fpVo.FLXNAME.ToString().Trim());
                        intRet = SetParam(intH, "FRELATE", item.fpVo.FRELATE.ToString().Trim());
                        intRet = SetParam(intH, "FLXADDR", item.fpVo.FLXADDR);
                        intRet = SetParam(intH, "FLXTELE", item.fpVo.FLXTELE.ToString().Trim());
                        intRet = SetParam(intH, "FRYTJBH", item.fpVo.FRYTJBH.ToString().Trim());
                        intRet = SetParam(intH, "FRYTJ", item.fpVo.FRYTJ.ToString().Trim());
                        intRet = SetParam(intH, "FRYDATE", item.fpVo.FRYDATE.ToString().Trim());
                        intRet = SetParam(intH, "FRYTIME", item.fpVo.FRYTIME.ToString());
                        intRet = SetParam(intH, "FRYTYKH", item.fpVo.FRYTYKH.ToString().Trim());
                        intRet = SetParam(intH, "FRYDEPT", item.fpVo.FRYDEPT.ToString().Trim());
                        intRet = SetParam(intH, "FRYBS", item.fpVo.FRYBS.ToString().Trim());
                        intRet = SetParam(intH, "FZKTYKH", item.fpVo.FZKTYKH.ToString().Trim());
                        intRet = SetParam(intH, "FZKDEPT", item.fpVo.FZKDEPT);
                        intRet = SetParam(intH, "FZKTIME", item.fpVo.FZKTIME.ToString());
                        intRet = SetParam(intH, "FCYDATE", item.fpVo.FCYDATE.ToString().Trim());
                        intRet = SetParam(intH, "FCYTIME", item.fpVo.FCYTIME.ToString().Trim());
                        intRet = SetParam(intH, "FCYTYKH", item.fpVo.FCYTYKH.ToString().Trim());
                        intRet = SetParam(intH, "FCYDEPT", item.fpVo.FCYDEPT);
                        intRet = SetParam(intH, "FCYBS", item.fpVo.FCYBS.ToString().Trim());
                        intRet = SetParam(intH, "FDAYS", item.fpVo.FDAYS.ToString().Trim());
                        intRet = SetParam(intH, "FMZZDBH", item.fpVo.FMZZDBH.ToString().Trim());
                        intRet = SetParam(intH, "FMZZD", item.fpVo.FMZZD.ToString().Trim());
                        intRet = SetParam(intH, "FMZDOCTBH", item.fpVo.FMZDOCTBH);
                        intRet = SetParam(intH, "FMZDOCT", item.fpVo.FMZDOCT.ToString().Trim());
                        intRet = SetParam(intH, "FJBFXBH", item.fpVo.FJBFXBH.ToString().Trim());
                        intRet = SetParam(intH, "FJBFX", item.fpVo.FJBFX.ToString().Trim());
                        intRet = SetParam(intH, "FYCLJBH", item.fpVo.FYCLJBH);
                        intRet = SetParam(intH, "FYCLJ", item.fpVo.FYCLJ.ToString().Trim());
                        intRet = SetParam(intH, "FQJTIMES", item.fpVo.FQJTIMES.ToString().Trim());
                        intRet = SetParam(intH, "FQJSUCTIMES", item.fpVo.FQJSUCTIMES.ToString().Trim());
                        intRet = SetParam(intH, "FPHZD", item.fpVo.FPHZD.ToString().Trim());
                        intRet = SetParam(intH, "FPHZDNUM", item.fpVo.FPHZDNUM);
                        intRet = SetParam(intH, "FPHZDBH", item.fpVo.FPHZDBH.ToString().Trim());
                        intRet = SetParam(intH, "FIFGMYWBH", item.fpVo.FIFGMYWBH.ToString().Trim());
                        intRet = SetParam(intH, "FIFGMYW", item.fpVo.FIFGMYW.ToString().Trim());
                        intRet = SetParam(intH, "FGMYW", item.fpVo.FGMYW.ToString().Trim());
                        intRet = SetParam(intH, "FBODYBH", item.fpVo.FBODYBH.ToString().Trim());
                        intRet = SetParam(intH, "FBODY", item.fpVo.FBODY);
                        intRet = SetParam(intH, "FBLOODBH", item.fpVo.FBLOODBH.ToString().Trim());
                        intRet = SetParam(intH, "FBLOOD", item.fpVo.FBLOOD.ToString().Trim());
                        intRet = SetParam(intH, "FRHBH", item.fpVo.FRHBH.ToString().Trim());
                        intRet = SetParam(intH, "FRH", item.fpVo.FRH);
                        intRet = SetParam(intH, "FKZRBH", item.fpVo.FKZRBH.ToString().Trim());
                        intRet = SetParam(intH, "FKZR", item.fpVo.FKZR.ToString().Trim());
                        intRet = SetParam(intH, "FZRDOCTBH", item.fpVo.FZRDOCTBH.ToString().Trim());
                        intRet = SetParam(intH, "FZRDOCTOR", item.fpVo.FZRDOCTOR.ToString().Trim());
                        intRet = SetParam(intH, "FZZDOCTBH", item.fpVo.FZZDOCTBH);
                        intRet = SetParam(intH, "FZZDOCT", item.fpVo.FZZDOCT.ToString().Trim());
                        intRet = SetParam(intH, "FZYDOCTBH", item.fpVo.FZYDOCTBH.ToString().Trim());
                        intRet = SetParam(intH, "FZYDOCT", item.fpVo.FZYDOCT.ToString().Trim());
                        intRet = SetParam(intH, "FNURSEBH", item.fpVo.FNURSEBH.ToString().Trim());
                        intRet = SetParam(intH, "FNURSE", item.fpVo.FNURSE);
                        intRet = SetParam(intH, "FJXDOCTBH", item.fpVo.FJXDOCTBH.ToString().Trim());
                        intRet = SetParam(intH, "FJXDOCT", item.fpVo.FJXDOCT.ToString().Trim());
                        intRet = SetParam(intH, "FSXDOCTBH", item.fpVo.FSXDOCTBH.ToString().Trim());
                        intRet = SetParam(intH, "FSXDOCT", item.fpVo.FSXDOCT);
                        intRet = SetParam(intH, "FBMYBH", item.fpVo.FBMYBH.ToString().Trim());
                        intRet = SetParam(intH, "FBMY", item.fpVo.FBMY.ToString().Trim());
                        intRet = SetParam(intH, "FQUALITYBH", item.fpVo.FQUALITYBH.ToString().Trim());
                        intRet = SetParam(intH, "FQUALITY", item.fpVo.FQUALITY.ToString().Trim());
                        intRet = SetParam(intH, "FZKDOCTBH", item.fpVo.FZKDOCTBH);
                        intRet = SetParam(intH, "FZKDOCT", item.fpVo.FZKDOCT.ToString().Trim());
                        intRet = SetParam(intH, "FZKNURSEBH", item.fpVo.FZKNURSEBH.ToString().Trim());
                        intRet = SetParam(intH, "FZKNURSE", item.fpVo.FZKNURSE.ToString().Trim());
                        intRet = SetParam(intH, "FZKRQ", item.fpVo.FZKRQ.ToString().Trim());
                        intRet = SetParam(intH, "FLYFSBH", item.fpVo.FLYFSBH);
                        intRet = SetParam(intH, "FLYFS", item.fpVo.FLYFS.ToString().Trim());
                        intRet = SetParam(intH, "FYZOUTHOSTITAL", item.fpVo.FYZOUTHOSTITAL.ToString().Trim());
                        intRet = SetParam(intH, "FSQOUTHOSTITAL", item.fpVo.FSQOUTHOSTITAL.ToString().Trim());
                        intRet = SetParam(intH, "FISAGAINRYBH", item.fpVo.FISAGAINRYBH);
                        intRet = SetParam(intH, "FISAGAINRY", item.fpVo.FISAGAINRY.ToString().Trim());
                        intRet = SetParam(intH, "FISAGAINRYMD", item.fpVo.FISAGAINRYMD.ToString().Trim());
                        intRet = SetParam(intH, "FRYQHMDAYS", item.fpVo.FRYQHMDAYS.ToString().Trim());
                        intRet = SetParam(intH, "FRYQHMHOURS", item.fpVo.FRYQHMHOURS.ToString().Trim());
                        intRet = SetParam(intH, "FRYQHMMINS", item.fpVo.FRYQHMMINS);
                        intRet = SetParam(intH, "FRYQHMCOUNTS", item.fpVo.FRYQHMCOUNTS.ToString().Trim());
                        intRet = SetParam(intH, "FRYHMDAYS", item.fpVo.FRYHMDAYS.ToString().Trim());
                        intRet = SetParam(intH, "FRYHMHOURS", item.fpVo.FRYHMHOURS.ToString().Trim());
                        intRet = SetParam(intH, "FRYHMMINS", item.fpVo.FRYHMMINS.ToString().Trim());
                        intRet = SetParam(intH, "FRYHMCOUNTS", item.fpVo.FRYHMCOUNTS);
                        intRet = SetParam(intH, "FSUM1", item.fpVo.FSUM1.ToString("0.00"));
                        intRet = SetParam(intH, "FZFJE", item.fpVo.FZFJE.ToString("0.00"));
                        intRet = SetParam(intH, "FZHFWLYLF", item.fpVo.FZHFWLYLF.ToString("0.00"));
                        intRet = SetParam(intH, "FZHFWLCZF", item.fpVo.FZHFWLCZF.ToString("0.00"));
                        intRet = SetParam(intH, "FZHFWLHLF", item.fpVo.FZHFWLHLF.ToString("0.00"));
                        intRet = SetParam(intH, "FZHFWLQTF", item.fpVo.FZHFWLQTF.ToString("0.00"));
                        intRet = SetParam(intH, "FZDLBLF", item.fpVo.FZDLBLF.ToString("0.00"));
                        intRet = SetParam(intH, "FZDLSSSF", item.fpVo.FZDLSSSF.ToString("0.00"));
                        intRet = SetParam(intH, "FZDLYXF", item.fpVo.FZDLYXF.ToString("0.00"));
                        intRet = SetParam(intH, "FZDLLCF", item.fpVo.FZDLLCF.ToString("0.00"));
                        intRet = SetParam(intH, "FZLLFFSSF", item.fpVo.FZLLFFSSF.ToString("0.00"));
                        intRet = SetParam(intH, "FZLLFWLZWLF", item.fpVo.FZLLFWLZWLF.ToString("0.00"));
                        intRet = SetParam(intH, "FZLLFSSF", item.fpVo.FZLLFSSF.ToString("0.00"));
                        intRet = SetParam(intH, "FZLLFMZF", item.fpVo.FZLLFMZF.ToString("0.00"));
                        intRet = SetParam(intH, "FZLLFSSZLF", item.fpVo.FZLLFSSZLF.ToString("0.00"));
                        intRet = SetParam(intH, "FKFLKFF", item.fpVo.FKFLKFF.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLZF", item.fpVo.FZYLZF.ToString("0.00"));
                        intRet = SetParam(intH, "FXYF", item.fpVo.FXYF.ToString("0.00"));
                        intRet = SetParam(intH, "FXYLGJF", item.fpVo.FXYLGJF.ToString("0.00"));
                        intRet = SetParam(intH, "FZCHYF", item.fpVo.FZCHYF.ToString("0.00"));
                        intRet = SetParam(intH, "FZCYF", item.fpVo.FZCYF.ToString("0.00"));
                        intRet = SetParam(intH, "FXYLXF", item.fpVo.FXYLXF.ToString("0.00"));
                        intRet = SetParam(intH, "FXYLBQBF", item.fpVo.FXYLBQBF.ToString("0.00"));
                        intRet = SetParam(intH, "FXYLQDBF", item.fpVo.FXYLQDBF.ToString("0.00"));
                        intRet = SetParam(intH, "FXYLYXYZF", item.fpVo.FXYLYXYZF.ToString("0.00"));
                        intRet = SetParam(intH, "FXYLXBYZF", item.fpVo.FXYLXBYZF.ToString("0.00"));
                        intRet = SetParam(intH, "FHCLCJF", item.fpVo.FHCLCJF.ToString("0.00"));
                        intRet = SetParam(intH, "FHCLZLF", item.fpVo.FHCLZLF.ToString("0.00"));
                        intRet = SetParam(intH, "FHCLSSF", item.fpVo.FHCLSSF.ToString("0.00"));
                        intRet = SetParam(intH, "FQTF", item.fpVo.FQTF.ToString("0.00"));
                        if (item.STATUS != 1)
                            item.fpVo.FBGLX = "1";
                        else
                            item.fpVo.FBGLX = "2";
                        intRet = SetParam(intH, "FBGLX", item.fpVo.FBGLX.ToString());
                        intRet = SetParam(intH, "GMSFHM", item.fpVo.GMSFHM);
                        intRet = SetParam(intH, "YYBH", exVo.YYBH);
                        intRet = SetParam(intH, "FZYF", item.fpVo.FZYF.ToString("0.00"));
                        intRet = SetParam(intH, "FZKDATE", item.fpVo.FZKDATE.ToString());
                        intRet = SetParam(intH, "FJOBBH", item.fpVo.FJOBBH.ToString());
                        intRet = SetParam(intH, "FZHFWLYLF01", item.fpVo.FZHFWLYLF01.ToString("0.00"));
                        intRet = SetParam(intH, "FZHFWLYLF02", item.fpVo.FZHFWLYLF02.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLZDF", item.fpVo.FZYLZDF.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLZLF", item.fpVo.FZYLZLF.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLZLF01", item.fpVo.FZYLZLF01.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLZLF02", item.fpVo.FZYLZLF02.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLZLF03", item.fpVo.FZYLZLF03.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLZLF04", item.fpVo.FZYLZLF04.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLZLF05", item.fpVo.FZYLZLF05.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLZLF06", item.fpVo.FZYLZLF06.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLQTF", item.fpVo.FZYLQTF.ToString("0.00"));
                        intRet = SetParam(intH, "FZCLJGZJF", item.fpVo.FZCLJGZJF.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLQTF01", item.fpVo.FZYLQTF01.ToString("0.00"));
                        intRet = SetParam(intH, "FZYLQTF02", item.fpVo.FZYLQTF02.ToString("0.00"));

                        intRet = SetParam(intH, "ZYH", item.fpVo.ZYH);
                        intRet = SetParam(intH, "FPHM", item.fpVo.FPHM);

                        ++j;
                        #endregion

                        #region 数据集(病人转科情况)：BRZKQKSJJ
                        if (item.fpVo.lstZkVo != null && item.fpVo.lstZkVo.Count > 0)
                        {
                            intRet = InsertDataSet(intH);
                            for (int i = 0; i < item.fpVo.lstZkVo.Count; i++)
                            {
                                logStr = "病人转科情况：" + Environment.NewLine;
                                logStr += "FZKTYKH:" + item.fpVo.lstZkVo[i].FZKTYKH.ToString() + Environment.NewLine;//
                                logStr += "FZKDEPT:" + item.fpVo.lstZkVo[i].FZKDEPT.ToString() + Environment.NewLine;//
                                logStr += "FZKDATE:" + item.fpVo.lstZkVo[i].FZKDATE.ToString().Trim() + Environment.NewLine;//
                                logStr += "FZKTIME:" + item.fpVo.lstZkVo[i].FZKTIME.ToString().Trim() + Environment.NewLine;//
                                logStr += "FPRN:" + item.fpVo.lstZkVo[i].FPRN.ToString().Trim() + Environment.NewLine;//
                                Log.Output(logStr);

                                intRet = InsertRow(intH);
                                intRet = SetField(intH, "FZKTYKH", item.fpVo.lstZkVo[i].FZKTYKH);//
                                intRet = SetField(intH, "FZKDEPT", item.fpVo.lstZkVo[i].FZKDEPT);//
                                intRet = SetField(intH, "FZKDATE", item.fpVo.lstZkVo[i].FZKDATE);//
                                intRet = SetField(intH, "FZKTIME", item.fpVo.lstZkVo[i].FZKTIME);//
                                intRet = SetField(intH, "FPRN", item.fpVo.lstZkVo[i].FPRN);//
                                EndRow(intH, i);
                            }
                            intRet = EndDataSet(intH, "BRZKQKSJJ");
                        }

                        #endregion

                        #region 数据集(病人诊断信息):  BRZDXXSJJ
                        if (item.fpVo.lstZdVo != null && item.fpVo.lstZdVo.Count > 0)
                        {
                            intRet = InsertDataSet(intH);
                            for (int i = 0; i < item.fpVo.lstZdVo.Count; i++)
                            {
                                logStr = "病人诊断信息：" + Environment.NewLine;
                                logStr += "FZDLX:" + item.fpVo.lstZdVo[i].FZDLX.ToString() + Environment.NewLine;//
                                logStr += "FICDVersion:" + item.fpVo.lstZdVo[i].FICDVersion.ToString() + Environment.NewLine;//
                                logStr += "FICDM:" + item.fpVo.lstZdVo[i].FICDM.ToString().Trim() + Environment.NewLine;//
                                logStr += "FJBNAME:" + item.fpVo.lstZdVo[i].FJBNAME.ToString().Trim() + Environment.NewLine;//
                                logStr += "FRYBQBH:" + item.fpVo.lstZdVo[i].FRYBQBH.ToString().Trim() + Environment.NewLine;//
                                logStr += "FRYBQ:" + item.fpVo.lstZdVo[i].FRYBQ.ToString().Trim() + Environment.NewLine;//
                                logStr += "FPRN:" + item.fpVo.lstZdVo[i].FPRN.ToString().Trim() + Environment.NewLine;//
                                Log.Output(logStr);

                                intRet = InsertRow(intH);
                                intRet = SetField(intH, "FZDLX", item.fpVo.lstZdVo[i].FZDLX);//
                                intRet = SetField(intH, "FICDVersion", item.fpVo.lstZdVo[i].FICDVersion);//
                                intRet = SetField(intH, "FICDM", item.fpVo.lstZdVo[i].FICDM);//
                                intRet = SetField(intH, "FJBNAME", item.fpVo.lstZdVo[i].FJBNAME);//
                                intRet = SetField(intH, "FRYBQBH", item.fpVo.lstZdVo[i].FRYBQBH);
                                intRet = SetField(intH, "FRYBQ", item.fpVo.lstZdVo[i].FRYBQ);//
                                intRet = SetField(intH, "FPRN", item.fpVo.lstZdVo[i].FPRN);//
                                EndRow(intH, i);
                            }
                            intRet = EndDataSet(intH, "BRZDXXSJJ");
                        }
                        #endregion

                        #region 数据集(病人手术信息):BRSSXXSJJ
                        if (item.fpVo.lstSsVo != null && item.fpVo.lstSsVo.Count > 0)
                        {
                            intRet = InsertDataSet(intH);
                            for (int i = 0; i < item.fpVo.lstSsVo.Count; i++)
                            {
                                intRet = InsertRow(intH);
                                intRet = SetField(intH, "FNAME", item.fpVo.lstSsVo[i].FNAME);//
                                intRet = SetField(intH, "FOPTIMES", item.fpVo.lstSsVo[i].FOPTIMES);//
                                intRet = SetField(intH, "FOPCODE", item.fpVo.lstSsVo[i].FOPCODE);//
                                intRet = SetField(intH, "FOP", item.fpVo.lstSsVo[i].FOP);//
                                intRet = SetField(intH, "FOPDATE", item.fpVo.lstSsVo[i].FOPDATE);//
                                intRet = SetField(intH, "FQIEKOUBH", item.fpVo.lstSsVo[i].FQIEKOUBH);//
                                intRet = SetField(intH, "FQIEKOU", item.fpVo.lstSsVo[i].FQIEKOU);//
                                intRet = SetField(intH, "FYUHEBH", item.fpVo.lstSsVo[i].FYUHEBH);//
                                intRet = SetField(intH, "FYUHE", item.fpVo.lstSsVo[i].FYUHE);//
                                intRet = SetField(intH, "FDOCBH", item.fpVo.lstSsVo[i].FDOCBH);//
                                intRet = SetField(intH, "FDOCNAME", item.fpVo.lstSsVo[i].FDOCNAME);//
                                intRet = SetField(intH, "FMAZUIBH", item.fpVo.lstSsVo[i].FMAZUIBH);//
                                intRet = SetField(intH, "FMAZUI", item.fpVo.lstSsVo[i].FMAZUI);//
                                intRet = SetField(intH, "FIFFSOP", item.fpVo.lstSsVo[i].FIFFSOP);//
                                intRet = SetField(intH, "FOPDOCT1BH", item.fpVo.lstSsVo[i].FOPDOCT1BH);//
                                intRet = SetField(intH, "FOPDOCT1", item.fpVo.lstSsVo[i].FOPDOCT1);//
                                intRet = SetField(intH, "FOPDOCT2BH", item.fpVo.lstSsVo[i].FOPDOCT2BH);//
                                intRet = SetField(intH, "FOPDOCT2", item.fpVo.lstSsVo[i].FOPDOCT2);//
                                intRet = SetField(intH, "FMZDOCTBH", item.fpVo.lstSsVo[i].FMZDOCTBH);//
                                intRet = SetField(intH, "FMZDOCT", item.fpVo.lstSsVo[i].FMZDOCT);//
                                intRet = SetField(intH, "FZQSSBH", item.fpVo.lstSsVo[i].FZQSSBH);//
                                intRet = SetField(intH, "FZQSS", item.fpVo.lstSsVo[i].FZQSS);//
                                intRet = SetField(intH, "FSSJBBH", item.fpVo.lstSsVo[i].FSSJBBH);//
                                intRet = SetField(intH, "FSSJB", item.fpVo.lstSsVo[i].FSSJB);//
                                intRet = SetField(intH, "FOPKSNAME", item.fpVo.lstSsVo[i].FOPKSNAME);//
                                intRet = SetField(intH, "FOPTYKH", item.fpVo.lstSsVo[i].FOPTYKH);//
                                intRet = SetField(intH, "FPRN", item.fpVo.lstSsVo[i].FPRN);//
                                EndRow(intH, i);
                            }
                            intRet = EndDataSet(intH, "BRSSXXSJJ");
                        }

                        #endregion

                        #region 数据集（妇婴卡）
                        if (item.fpVo.lstFyVo != null && item.fpVo.lstFyVo.Count > 0)
                        {
                            intRet = InsertDataSet(intH);
                            for (int i = 0; i < item.fpVo.lstFyVo.Count; i++)
                            {
                                intRet = InsertRow(intH);
                                intRet = SetField(intH, "FBABYNUM", item.fpVo.lstFyVo[i].FBABYNUM);//
                                intRet = SetField(intH, "FNAME", item.fpVo.lstFyVo[i].FNAME);//
                                intRet = SetField(intH, "FBABYSEXBH", item.fpVo.lstFyVo[i].FBABYSEXBH);//
                                intRet = SetField(intH, "FBABYSEX", item.fpVo.lstFyVo[i].FBABYSEX);//
                                intRet = SetField(intH, "FTZ", (Function.Dec(item.fpVo.lstFyVo[i].FTZ) / 1000).ToString("0.00"));//
                                intRet = SetField(intH, "FRESULTBH", item.fpVo.lstFyVo[i].FRESULTBH);//
                                intRet = SetField(intH, "FRESULT", item.fpVo.lstFyVo[i].FRESULT);//
                                intRet = SetField(intH, "FZGBH", item.fpVo.lstFyVo[i].FZGBH);//
                                intRet = SetField(intH, "FZG", item.fpVo.lstFyVo[i].FZG);//
                                intRet = SetField(intH, "FBABYSUC", item.fpVo.lstFyVo[i].FBABYSUC);//
                                intRet = SetField(intH, "FHXBH", item.fpVo.lstFyVo[i].FHXBH);//
                                intRet = SetField(intH, "FHX", item.fpVo.lstFyVo[i].FHX);//
                                intRet = SetField(intH, "FPRN", item.fpVo.lstFyVo[i].FPRN);//
                                EndRow(intH, i);
                            }
                            intRet = EndDataSet(intH, "FYKSJJ");
                        }

                        #endregion

                        #region 数据集（肿瘤卡）：ZLKSJJ
                        if (item.fpVo.lstZlVo != null && item.fpVo.lstZlVo.Count > 0)
                        {
                            intRet = InsertDataSet(intH);
                            for (int i = 0; i < item.fpVo.lstZlVo.Count; i++)
                            {
                                string logStr1 = string.Empty;

                                intRet = InsertRow(intH);
                                intRet = SetField(intH, "FFLFSBH", item.fpVo.lstZlVo[i].FFLFSBH);//
                                intRet = SetField(intH, "FFLFS", item.fpVo.lstZlVo[i].FFLFS);//
                                intRet = SetField(intH, "FFLCXBH", item.fpVo.lstZlVo[i].FFLCXBH);//
                                intRet = SetField(intH, "FFLCX", item.fpVo.lstZlVo[i].FFLCX);//
                                intRet = SetField(intH, "FFLZZBH", item.fpVo.lstZlVo[i].FFLZZBH);//
                                intRet = SetField(intH, "FFLZZ", item.fpVo.lstZlVo[i].FFLZZ);//
                                intRet = SetField(intH, "FYJY", item.fpVo.lstZlVo[i].FYJY);//
                                intRet = SetField(intH, "FYCS", item.fpVo.lstZlVo[i].FYCS);//
                                intRet = SetField(intH, "FYTS", item.fpVo.lstZlVo[i].FYTS);//
                                intRet = SetField(intH, "FYRQ1", item.fpVo.lstZlVo[i].FYRQ1);//
                                intRet = SetField(intH, "FYRQ2", item.fpVo.lstZlVo[i].FYRQ2);//
                                intRet = SetField(intH, "FQJY", item.fpVo.lstZlVo[i].FQJY);//
                                intRet = SetField(intH, "FQCS", item.fpVo.lstZlVo[i].FQCS);//
                                intRet = SetField(intH, "FQTS", item.fpVo.lstZlVo[i].FQTS);//
                                intRet = SetField(intH, "FQRQ1", item.fpVo.lstZlVo[i].FQRQ1);//
                                intRet = SetField(intH, "FQRQ2", item.fpVo.lstZlVo[i].FQRQ2);//
                                intRet = SetField(intH, "FZNAME", item.fpVo.lstZlVo[i].FZNAME);//
                                intRet = SetField(intH, "FZJY", item.fpVo.lstZlVo[i].FZJY);//
                                intRet = SetField(intH, "FZCS", item.fpVo.lstZlVo[i].FZCS);//
                                intRet = SetField(intH, "FZTS", item.fpVo.lstZlVo[i].FZTS);//
                                intRet = SetField(intH, "FZRQ1", item.fpVo.lstZlVo[i].FZRQ1);//
                                intRet = SetField(intH, "FZRQ2", item.fpVo.lstZlVo[i].FZRQ2);//
                                intRet = SetField(intH, "FHLFSBH", item.fpVo.lstZlVo[i].FHLFSBH);//
                                intRet = SetField(intH, "FHLFS", item.fpVo.lstZlVo[i].FHLFS);//
                                intRet = SetField(intH, "FHLFFBH", item.fpVo.lstZlVo[i].FHLFFBH);//
                                intRet = SetField(intH, "FHLFF", item.fpVo.lstZlVo[i].FHLFF);//
                                intRet = SetField(intH, "FPRN", item.fpVo.lstZlVo[i].FPRN);//

                                logStr1 += "数据集（肿瘤卡）：" + Environment.NewLine;

                                logStr1 += "FFLFSBH" + item.fpVo.lstZlVo[i].FFLFSBH + Environment.NewLine;
                                logStr1 += "FFLFS" + item.fpVo.lstZlVo[i].FFLFS + Environment.NewLine;
                                logStr1 += "FFLCXBH" + item.fpVo.lstZlVo[i].FFLCXBH + Environment.NewLine;
                                logStr1 += "FFLCX" + item.fpVo.lstZlVo[i].FFLCX + Environment.NewLine;
                                logStr1 += "FFLZZBH" + item.fpVo.lstZlVo[i].FFLZZBH + Environment.NewLine;
                                logStr1 += "FFLZZ" + item.fpVo.lstZlVo[i].FFLZZ + Environment.NewLine;
                                logStr1 += "FYJY" + item.fpVo.lstZlVo[i].FYJY + Environment.NewLine;
                                logStr1 += "FYCS" + item.fpVo.lstZlVo[i].FYCS + Environment.NewLine;
                                logStr1 += "FYTS" + item.fpVo.lstZlVo[i].FYTS + Environment.NewLine;
                                logStr1 += "FYRQ1" + item.fpVo.lstZlVo[i].FYRQ1 + Environment.NewLine;
                                logStr1 += "FYRQ2" + item.fpVo.lstZlVo[i].FYRQ2 + Environment.NewLine;
                                logStr1 += "FQJY" + item.fpVo.lstZlVo[i].FQJY + Environment.NewLine;
                                logStr1 += "FQCS" + item.fpVo.lstZlVo[i].FQCS + Environment.NewLine;
                                logStr1 += "FQTS" + item.fpVo.lstZlVo[i].FQTS + Environment.NewLine;
                                logStr1 += "FQRQ1" + item.fpVo.lstZlVo[i].FQRQ1 + Environment.NewLine;
                                logStr1 += "FQRQ2" + item.fpVo.lstZlVo[i].FQRQ2 + Environment.NewLine;
                                logStr1 += "FZNAME" + item.fpVo.lstZlVo[i].FZNAME + Environment.NewLine;
                                logStr1 += "FZJY" + item.fpVo.lstZlVo[i].FZJY + Environment.NewLine;
                                logStr1 += "FZCS" + item.fpVo.lstZlVo[i].FZCS + Environment.NewLine;
                                logStr1 += "FZTS" + item.fpVo.lstZlVo[i].FZTS + Environment.NewLine;
                                logStr1 += "FZRQ1" + item.fpVo.lstZlVo[i].FZRQ1 + Environment.NewLine;
                                logStr1 += "FZRQ2" + item.fpVo.lstZlVo[i].FZRQ2 + Environment.NewLine;
                                logStr1 += "FHLFSBH" + item.fpVo.lstZlVo[i].FHLFSBH + Environment.NewLine;
                                logStr1 += "FHLFS" + item.fpVo.lstZlVo[i].FHLFS + Environment.NewLine;
                                logStr1 += "FHLFFBH" + item.fpVo.lstZlVo[i].FHLFFBH + Environment.NewLine;
                                logStr1 += "FHLFF" + item.fpVo.lstZlVo[i].FHLFF + Environment.NewLine;
                                logStr1 += "FPRN" + item.fpVo.lstZlVo[i].FPRN + Environment.NewLine;

                                Log.Output(logStr1);

                                EndRow(intH, i);
                            }
                            intRet = EndDataSet(intH, "ZLKSJJ");
                        }

                        #endregion

                        #region 数据集（肿瘤化疗记录）：ZLHLJLSJJ
                        if (item.fpVo.lstHlVo != null && item.fpVo.lstHlVo.Count > 0)
                        {
                            intRet = InsertDataSet(intH);
                            for (int i = 0; i < item.fpVo.lstHlVo.Count; i++)
                            {
                                string logStr2 = string.Empty;
                                intRet = InsertRow(intH);
                                intRet = SetField(intH, "FHLRQ1", item.fpVo.lstHlVo[i].FHLRQ1);//
                                intRet = SetField(intH, "FHLDRUG", item.fpVo.lstHlVo[i].FHLDRUG);//
                                intRet = SetField(intH, "FHLPROC", item.fpVo.lstHlVo[i].FHLPROC);//
                                intRet = SetField(intH, "FHLLXBH", item.fpVo.lstHlVo[i].FHLLXBH);//
                                intRet = SetField(intH, "FHLLX", item.fpVo.lstHlVo[i].FHLLX);//
                                intRet = SetField(intH, "FPRN", item.fpVo.lstHlVo[i].FPRN);//

                                logStr2 += "肿瘤化疗记录：" + Environment.NewLine;
                                logStr2 += "FHLRQ1" + item.fpVo.lstHlVo[i].FHLRQ1 + Environment.NewLine;
                                logStr2 += "FHLDRUG" + item.fpVo.lstHlVo[i].FHLDRUG + Environment.NewLine;
                                logStr2 += "FHLPROC" + item.fpVo.lstHlVo[i].FHLPROC + Environment.NewLine;
                                logStr2 += "FHLLXBH" + item.fpVo.lstHlVo[i].FHLLXBH + Environment.NewLine;
                                logStr2 += "FHLLX" + item.fpVo.lstHlVo[i].FHLLX + Environment.NewLine;
                                logStr2 += "FPRN" + item.fpVo.lstHlVo[i].FPRN + Environment.NewLine;
                                Log.Output(logStr);

                                EndRow(intH, j);
                            }
                            intRet = EndDataSet(intH, "ZLHLJLSJJ");
                        }


                        #endregion

                        #region 数据集（病人诊断码附加编码）：BRZDMFJBMSJJ
                        if (item.fpVo.lstZdfjVo != null && item.fpVo.lstZdfjVo.Count > 0)
                        {
                            intRet = InsertDataSet(intH);
                            for (int k = 0; k < item.fpVo.lstZdfjVo.Count; k++)
                            {
                                intRet = InsertRow(intH);
                                intRet = SetField(intH, "FZDLX", item.fpVo.lstZdfjVo[k].FZDLX);//
                                intRet = SetField(intH, "FICDM", item.fpVo.lstZdfjVo[k].FICDM);//
                                intRet = SetField(intH, "FFJICDM", item.fpVo.lstZdfjVo[k].FFJICDM);//
                                intRet = SetField(intH, "FFJJBNAME", item.fpVo.lstZdfjVo[k].FFJJBNAME);//
                                intRet = SetField(intH, "FFRYBQBH", item.fpVo.lstZdfjVo[k].FFRYBQBH);//
                                intRet = SetField(intH, "FFRYBQ", item.fpVo.lstZdfjVo[k].FFRYBQ);//
                                intRet = SetField(intH, "FPX", item.fpVo.lstZdfjVo[k].FPX);//
                                EndRow(intH, k);
                            }
                            intRet = EndDataSet(intH, "BRZDMFJBMSJJ");
                        }

                        #endregion

                        #region （新增）数据集（中医院病人附加信息）：ZYYBRFJXXSJJ
                        if (item.fpVo.lstZyVo != null && item.fpVo.lstZyVo.Count > 0)
                        {
                            intRet = InsertDataSet(intH);
                            for (int i = 0; i < item.fpVo.lstZyVo.Count; i++)
                            {
                                intRet = InsertRow(intH);
                                intRet = SetField(intH, "FPRN", item.fpVo.lstZyVo[i].FPRN);//
                                intRet = SetField(intH, "FZLLBBH", item.fpVo.lstZyVo[i].FZLLBBH);//
                                intRet = SetField(intH, "FZLLB", item.fpVo.lstZyVo[i].FZLLB);//
                                intRet = SetField(intH, "FZZZYBH", item.fpVo.lstZyVo[i].FZZZYBH);//
                                intRet = SetField(intH, "FZZZY", item.fpVo.lstZyVo[i].FZZZY);//
                                intRet = SetField(intH, "FRYCYBH", item.fpVo.lstZyVo[i].FRYCYBH);//
                                intRet = SetField(intH, "FRYCY", item.fpVo.lstZyVo[i].FRYCY);//
                                intRet = SetField(intH, "FMZZYZDBH", item.fpVo.lstZyVo[i].FMZZYZDBH);//
                                intRet = SetField(intH, "FMZZYZD", item.fpVo.lstZyVo[i].FMZZYZD);//
                                intRet = SetField(intH, "FSSLCLJBH", item.fpVo.lstZyVo[i].FSSLCLJBH);//
                                intRet = SetField(intH, "FSSLCLJ", item.fpVo.lstZyVo[i].FSSLCLJ);//
                                intRet = SetField(intH, "FSYJGZJBH", item.fpVo.lstZyVo[i].FSYJGZJBH);//
                                intRet = SetField(intH, "FSYJGZJ", item.fpVo.lstZyVo[i].FSYJGZJ);//
                                intRet = SetField(intH, "FSYZYSBBH", item.fpVo.lstZyVo[i].FSYZYSBBH);//
                                intRet = SetField(intH, "FSYZYSB", item.fpVo.lstZyVo[i].FSYZYSB);//
                                intRet = SetField(intH, "FSYZYJSBH", item.fpVo.lstZyVo[i].FSYZYJSBH);//
                                intRet = SetField(intH, "FSYZYJS", item.fpVo.lstZyVo[i].FSYZYJS);//
                                intRet = SetField(intH, "FBZSHBH", item.fpVo.lstZyVo[i].FBZSHBH);//
                                intRet = SetField(intH, "FBZSH", item.fpVo.lstZyVo[i].FBZSH);//
                                EndRow(intH, i);
                            }

                            intRet = EndDataSet(intH, "ZYYBRFJXXSJJ");
                        }

                        #endregion

                        #region 上传

                        intRet = Run(intH);
                        strValue = new StringBuilder(1024);
                        intRet = GetParam(intH, "FHZ", strValue, 1024);
                        if (strValue.ToString() == "1")
                        {
                            StringBuilder sbValue = new StringBuilder(1024);
                            intRet = GetParam(intH, "MSG", sbValue, 1024);
                            string strCGBZ = sbValue.ToString().Trim();//原因
                            if (strCGBZ == "执行成功！" || strCGBZ == "1")
                            {
                                lngRes = 1;
                                item.Issucess = 1;//1  上传成功
                                item.FailMsg = "";
                            }
                        }
                        else
                        {
                            intRet = GetParam(intH, "MSG", strValue, 1024);
                            ExceptionLog.OutPutException(item.JZJLH + "-" + item.INPATIENTID + ":" + strValue.ToString());
                            if (strValue.ToString().Contains("已存在对应就诊记录号"))
                                item.Issucess = 1;
                            else
                                item.Issucess = -1; //-1 上传失败
                            item.FailMsg = strValue.ToString();
                        }
                        DestroyInstance(intH);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.OutPutException("jzjlh-->" + ex);
                        //MessageBox.Show("jzjlh-->" + ex);
                        lngRes = -1;
                    }
                }  
            }
            return lngRes;
        }
        #endregion

        #region 出院小结上传
        public static long lngFunSP3_3022(ref List<EntityPatUpload> lstVo, EntityDGExtra exVo, ref StringBuilder strValue)
        {
            long lngRes = -1;
            int intRet;
            string logStr = string.Empty;
            for (int i = 0; i < lstVo.Count;i++ )
            {
                try
                {
                    int intH = CreateInstace();
                    if (intH > 0)
                    {
                        if (lstVo[i].xjVo == null)
                        {
                            lstVo[i].Issucess = -1; //-1 上传失败
                            lstVo[i].FailMsg = "就诊记录号:" + lstVo[i].JZJLH + " 住院号：" + lstVo[i].INPATIENTID + "小结为空！";
                            continue;
                        }
                        if (lstVo[i].firstSource == 0)
                        {
                            lstVo[i].Issucess = -1; //-1 上传失败
                            lstVo[i].FailMsg = "无首页信息";
                            continue;
                        }
                        lstVo[i].Issucess = -1;

                        #region log
                        logStr = string.Empty;
                        logStr += "出院小结数据上传：" + Environment.NewLine;
                        logStr += "FN:" + "SP3_3022" + Environment.NewLine;
                        logStr += "JBR:" + exVo.JBR + Environment.NewLine;
                        logStr += "YYBH:" + exVo.YYBH + Environment.NewLine;
                        logStr += "JZJLH:" + lstVo[i].xjVo.JZJLH + Environment.NewLine;
                        logStr += "MZH:" + lstVo[i].xjVo.MZH + Environment.NewLine;
                        logStr += "ZYH:" + lstVo[i].xjVo.ZYH + Environment.NewLine;
                        logStr += "MZZD:" + lstVo[i].xjVo.MZZD + Environment.NewLine;
                        logStr += "RYZD:" + lstVo[i].xjVo.RYZD + Environment.NewLine;
                        logStr += "CYZD:" + lstVo[i].xjVo.CYZD + Environment.NewLine;
                        logStr += "XM:" + lstVo[i].xjVo.XM + Environment.NewLine;
                        logStr += "XB:" + lstVo[i].xjVo.XB + Environment.NewLine;
                        logStr += "NL:" + lstVo[i].xjVo.NL + Environment.NewLine;
                        logStr += "ZY:" + lstVo[i].xjVo.ZY + Environment.NewLine;
                        logStr += "JG:" + lstVo[i].xjVo.JG + Environment.NewLine;
                        logStr += "RYRQ:" + lstVo[i].xjVo.RYRQ + Environment.NewLine;
                        logStr += "CYRQ:" + lstVo[i].xjVo.CYRQ + Environment.NewLine;
                        logStr += "ZYTS:" + lstVo[i].xjVo.ZYTS + Environment.NewLine;
                        logStr += "YJDZ:" + lstVo[i].xjVo.YJDZ + Environment.NewLine;
                        logStr += "ZLJG:" + lstVo[i].xjVo.ZLJG + Environment.NewLine;
                        logStr += "YSQM:" + lstVo[i].xjVo.YSQM + Environment.NewLine;
                        logStr += "RYQK:" + lstVo[i].xjVo.RYQK + Environment.NewLine;
                        logStr += "RYHCLGC:" + lstVo[i].xjVo.RYHCLGC + Environment.NewLine;
                        logStr += "CYSQK:" + lstVo[i].xjVo.CYSQK + Environment.NewLine;
                        logStr += "GMSFHM:" + lstVo[i].xjVo.GMSFHM + Environment.NewLine;
                        logStr += "FTIMES:" + lstVo[i].xjVo.FTIMES + Environment.NewLine;
                        logStr += "FSUM1:" + lstVo[i].xjVo.FSUM1.ToString("0.00") + Environment.NewLine;
                        logStr += "FPHM:" + lstVo[i].xjVo.FPHM + Environment.NewLine;


                        Log.Output(logStr);
                        #endregion

                        #region 入参
                        intRet = SetParam(intH, "FN", "SP3_3022");
                        intRet = SetParam(intH, "JBR", exVo.JBR);
                        intRet = SetParam(intH, "YYBH", exVo.YYBH);
                        intRet = SetParam(intH, "JZJLH", lstVo[i].xjVo.JZJLH);//
                        intRet = SetParam(intH, "MZH", lstVo[i].xjVo.MZH);//
                        intRet = SetParam(intH, "ZYH", lstVo[i].xjVo.ZYH);//
                        intRet = SetParam(intH, "MZZD", lstVo[i].xjVo.MZZD);
                        intRet = SetParam(intH, "RYZD", lstVo[i].xjVo.RYZD);
                        intRet = SetParam(intH, "CYZD", lstVo[i].xjVo.CYZD);
                        intRet = SetParam(intH, "XM", lstVo[i].xjVo.XM);
                        intRet = SetParam(intH, "XB", lstVo[i].xjVo.XB);
                        intRet = SetParam(intH, "NL", lstVo[i].xjVo.NL);
                        intRet = SetParam(intH, "ZY", lstVo[i].xjVo.ZY);
                        intRet = SetParam(intH, "JG", lstVo[i].xjVo.JG);
                        intRet = SetParam(intH, "RYRQ", lstVo[i].xjVo.RYRQ);
                        intRet = SetParam(intH, "CYRQ", lstVo[i].xjVo.CYRQ);
                        intRet = SetParam(intH, "ZYTS", lstVo[i].xjVo.ZYTS);
                        intRet = SetParam(intH, "YJDZ", lstVo[i].xjVo.YJDZ);
                        intRet = SetParam(intH, "ZLJG", lstVo[i].xjVo.ZLJG);
                        intRet = SetParam(intH, "CYYZ", lstVo[i].xjVo.CYYZ);
                        intRet = SetParam(intH, "YSQM", lstVo[i].xjVo.YSQM);
                        intRet = SetParam(intH, "RYQK", lstVo[i].xjVo.RYQK);
                        intRet = SetParam(intH, "RYHCLGC", lstVo[i].xjVo.RYHCLGC);
                        intRet = SetParam(intH, "CYSQK", lstVo[i].xjVo.CYSQK);
                        intRet = SetParam(intH, "GMSFHM", lstVo[i].xjVo.GMSFHM);

                        intRet = SetParam(intH, "FTIMES", lstVo[i].xjVo.FTIMES);
                        intRet = SetParam(intH, "FSUM1", lstVo[i].xjVo.FSUM1.ToString("0.00"));
                        intRet = SetParam(intH, "FPHM", lstVo[i].xjVo.FPHM);

                        intRet = Run(intH);
                        #endregion
                    }

                    #region 上传
                    strValue = new StringBuilder(1024);
                    intRet = GetParam(intH, "FHZ", strValue, 1024);
                    if (strValue.ToString() == "1")
                    {
                        StringBuilder sbValue = new StringBuilder(1024);
                        intRet = GetParam(intH, "MSG", sbValue, 1024);
                        string strCGBZ = sbValue.ToString().Trim();//原因
                        if (strCGBZ == "执行成功！" || strCGBZ == "1")
                        {
                            lngRes = 1;
                            lstVo[i].JBR = exVo.JBR;
                            lstVo[i].Issucess = 1;//1  上传成功
                            lstVo[i].FailMsg = "";
                        }
                    }
                    else
                    {
                        lngRes = -1;
                        intRet = GetParam(intH, "MSG", strValue, 1024);
                        ExceptionLog.OutPutException(lstVo[i].JZJLH + "-" + lstVo[i].INPATIENTID + ":" + strValue.ToString());
                        lstVo[i].Issucess = -1; //-1 上传失败
                        lstVo[i].FailMsg = strValue.ToString();
                    }
                    DestroyInstance(intH);
                    #endregion
                }
                catch (Exception ex)
                {
                    ExceptionLog.OutPutException(ex);
                }
            }
            return lngRes;
        }
        #endregion

        #region 门诊处方项目明细上传
        public static long lngFunSP3_2002(ref List<EntityMzcf> lstVo, EntityDGExtra exVo, ref StringBuilder strValue)
        {
            long lngRes = -1;
            int intRet;
            for (int i = 0; i < lstVo.Count; i++)
            {
                int intH = CreateInstace();
                if (intH > 0)
                {
                    intRet = SetParam(intH, "FN", "SP3_2002");
                    intRet = SetParam(intH, "JBR", exVo.JBR);
                    lstVo[i].JBR = exVo.JBR;
                    intRet = SetParam(intH, "YYBH", exVo.YYBH);
                    intRet = InsertDataSet(intH);
                }
                else
                    continue;
                
                for (int j = 0; j < lstVo[i].lstCfMsg.Count; j++)
                {
                    intRet = InsertRow(intH);
                    intRet = SetField(intH, "ZYH", lstVo[i].ZYH);//
                    intRet = SetField(intH, "CFH", lstVo[i].CFH);//
                    intRet = SetField(intH, "GMSFHM", lstVo[i].GMSFHM);//
                    intRet = SetField(intH, "JZLB", lstVo[i].JZLB);//
                    intRet = SetField(intH, "FYRQ", lstVo[i].FYRQ);//8位
                    intRet = SetField(intH, "XMXH", lstVo[i].lstCfMsg[j].XMXH);//
                    intRet = SetField(intH, "XMBH", lstVo[i].lstCfMsg[j].XMBH);//
                    intRet = SetField(intH, "XMMC", lstVo[i].lstCfMsg[j].XMMC);//
                    intRet = SetField(intH, "JG", lstVo[i].lstCfMsg[j].JG);
                    intRet = SetField(intH, "MCYL", lstVo[i].lstCfMsg[j].MCYL);//
                    intRet = SetField(intH, "JE", lstVo[i].lstCfMsg[j].JE);
                    intRet = SetField(intH, "ZFBL", lstVo[i].lstCfMsg[j].ZFBL);//0<= X <= 1 无比例时，默认传0
                    intRet = SetField(intH, "YSGH", lstVo[i].YSGH);
                    intRet = SetField(intH, "BZ", lstVo[i].lstCfMsg[j].BZ);
                    EndRow(intH, i + 1);

                    //Log.Output("\r\n ZYH:" + lstVo[i].ZYH + "\r\n CFH:" + lstVo[i].CFH + " \r\n GMSFHM:" +
                    //    lstVo[i].GMSFHM + "\r\n JZLB:" + lstVo[i].JZLB + "\r\n FYRQ:" + lstVo[i].FYRQ + "\r\n XMXH:" +
                    //    lstVo[i].lstCfMsg[j].XMXH + "\r\n XMBH:" + lstVo[i].lstCfMsg[j].XMBH + "\r\n XMMC:" + 
                    //    lstVo[i].lstCfMsg[j].XMMC + "\r\n JG:" +
                    //    lstVo[i].lstCfMsg[j].JG + "\r\n MCYL:" + lstVo[i].lstCfMsg[j].MCYL + "\r\n JE:" +
                    //    lstVo[i].lstCfMsg[j].JE + "\r\n ZFBL:" + lstVo[i].lstCfMsg[j].ZFBL + "\r\n YSGH:" +
                    //    lstVo[i].YSGH + "\r\n BZ:" + lstVo[i].lstCfMsg[j].BZ);
                }
                intRet = EndDataSet(intH, "MZCFXMDR");
                intRet = Run(intH);

                strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {
                    StringBuilder sbValue = new StringBuilder(1024);
                    intRet = GetParam(intH, "MSG", sbValue, 1024);
                    string strCGBZ = sbValue.ToString().Trim();//原因
                    if (strCGBZ == "执行成功！" || strCGBZ == "1")
                    {
                        intRet = GetParam(intH, "PH", strValue, 1024);//批号，	批号（PH）：由医保系统产生，作为本次传送的标识。
                        string strPH = strValue.ToString();
                        lstVo[i].PH = strPH;
                        intRet = GetParam(intH, "ZJE", strValue, 1024);//	总金额：本次传送的记帐处方项目汇总的医疗费用总额。每条传入的处方项目的“金额”字段合计出的总金额。
                        string strZJE = strValue.ToString();
                        lstVo[i].ZJE = strZJE;
                        lngRes = 1;
                        lstVo[i].IsSuccess = 1;
                    }
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    lstVo[i].IsSuccess = -1;
                    lstVo[i].failMsg = strValue.ToString();
                }
                DestroyInstance(intH);
            }
           
            return lngRes;
        }
        #endregion

        #region 门诊记帐处方项目删除
        public static long lngFunSP3_2003(List<EntityMzcf> lstVo, EntityDGExtra exVo, ref StringBuilder strValue)
        {
            long lngRes = -1;
            
            int intRet;
            for (int i = 0; i < lstVo.Count; i++)
            {
                int intH = CreateInstace();
                if (intH < 0)
                    continue;
                intRet = SetParam(intH, "FN", "SP3_2003");
                intRet = SetParam(intH, "YYBH", exVo.YYBH);
                intRet = SetParam(intH,"ZYH", lstVo[i].ZYH);
                intRet = SetParam(intH, "CFH", lstVo[i].CFH);
                intRet = SetParam(intH, "JBR", exVo.JBR);
                lstVo[i].JBR = exVo.JBR;
                intRet = Run(intH);
                strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {
                    StringBuilder sbValue = new StringBuilder(1024);
                    intRet = GetParam(intH, "MSG", sbValue, 1024);
                    string strCGBZ = sbValue.ToString().Trim();//原因
                    if (strCGBZ == "执行成功！" || strCGBZ == "1")
                    {
                        lstVo[i].STATUS = 2;
                        lngRes = 1;
                        lstVo[i].IsSuccess = 1;
                    }
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    lstVo[i].IsSuccess = -1;
                    lstVo[i].failMsg = strValue.ToString();
                }
                DestroyInstance(intH);
            }

            return lngRes;
        }
        #endregion

        #region
        #region HISYB.XML读写操作
        /// <summary>
        /// XML文件名
        /// </summary>
        public static string XMLFile = Application.StartupPath + @"\HISYB.xml";
        /// <summary>
        /// 读操作
        /// </summary>
        /// <param name="parentnode"></param>
        /// <param name="childnode"></param>
        /// <param name="key"></param>
        public static string strReadXML(string parentnode, string childnode, string key)
        {
            string strRet = "";
            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null)
                    {
                        strRet = xndC.Attributes["value"].Value.ToString().Trim();
                    }
                }
            }
            catch
            {
                strRet = "";
            }
            return strRet;
        }
        #endregion

        #region 医院登录
        /// <summary>
        /// 医院登录
        /// </summary>
        /// <param name="strUser">用户名</param>
        /// <param name="strPwd">口令</param>
        /// <param name="Blxml">判断是否读取xml用户名和密码</param>
        /// <returns></returns>
        public static long lngUserLoin(string strUser, string strPwd, bool Blxml)
        {
            try
            {
                //初始化
                lngInitialize();
                int intPtr = CreateInstace();
                string strHosCode = strUser;
                if (Blxml)
                {
                    strUser = strReadXML("DGCSMZYB", "USERNAMEMZ", "AnyOne");//need modify 需要传进来
                    strPwd = strReadXML("DGCSMZYB", "PASSWORDMZ", "AnyOne");
                    strHosCode = strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
                }
                int intRet;
                StringBuilder sbPwd = new StringBuilder(32);
                if (intPtr > 0)
                {
                    intRet = EncryptWithCipher(intPtr, strPwd, sbPwd, 32);
                    if (intRet < 0)
                    {
                        MessageBox.Show("明文密码加密失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        DestroyInstance(intPtr);
                        return -1;
                    }
                    string strNewPwd = sbPwd.ToString();
                    //医院登录
                    intRet = SetParam(intPtr, "FN", "1");
                    intRet = SetParam(intPtr, "YYBH", strHosCode);
                    intRet = SetParam(intPtr, "USERID", strUser);
                    intRet = SetParam(intPtr, "PWD", strNewPwd);
                    intRet = SetParam(intPtr, "JBRLX", "1");
                    intRet = SetParam(intPtr, "CLIENTTYPE", "HIS");
                    intRet = SetParam(intPtr, "JBR", "001");
                    intRet = Run(intPtr);
                    if (intRet < 0)
                    {
                        StringBuilder strRetValue1 = new StringBuilder(32);
                        intRet = GetSysMessage(intPtr, strRetValue1, 66);
                        MessageBox.Show(strRetValue1.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        DestroyInstance(intPtr);
                        return -1;
                    }
                    StringBuilder strRetValue = new StringBuilder(32);
                    StringBuilder strRetMessage = new StringBuilder(1024);
                    intRet = GetParam(intPtr, "FHZ", strRetValue, 32);
                    intRet = GetParam(intPtr, "MSG", strRetMessage, 1024);
                    if (strRetValue.ToString() == "EHIS9700")
                    {
                        ExceptionLog.OutPutException("返回值：EHIS9700 \r\n" + "社保系统登录故障，请稍后重新登录.");
                        MessageBox.Show("社保系统登录故障，请稍后重新登录！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        DestroyInstance(intPtr);
                        return -1;
                    }
                    else if (strRetValue.ToString() != "1")
                    {
                        ExceptionLog.OutPutException(strRetMessage.ToString());
                        MessageBox.Show(strRetMessage.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        DestroyInstance(intPtr);
                    }
                }
                else
                {
                    MessageBox.Show("创建实例失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DestroyInstance(intPtr);
                    return -1;
                }
            }
            catch (Exception ex )
            {
                ExceptionLog.OutPutException(ex);
            }
            
            return 1;
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static long lngInitialize()
        {
            int intPtr = -1;
            try
            {
                string svrIP = strReadXML("DGCSMZYB", "svrIPMZ", "AnyOne");
                string svrPort = strReadXML("DGCSMZYB", "svrPortMZ", "AnyOne");
                short shPort = 0;
                short.TryParse(svrPort, out shPort);
                string SndBufSize = strReadXML("DGCSMZYB", "SndBufSizeMZ", "AnyOne");
                string RecvBufSize = strReadXML("DGCSMZYB", "RecvBufSizeMZ", "AnyOne");
                int intSize = 0, intSize2 = 0;
                int.TryParse(SndBufSize, out intSize);
                int.TryParse(RecvBufSize, out intSize2);
                intPtr = Initialize(svrIP, shPort, intSize, intSize2);
                if (intPtr < 0)
                {
                    MessageBox.Show("初始化失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return -1;
                }
                
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            return intPtr;
        }
        #endregion

        #endregion
    }
}
