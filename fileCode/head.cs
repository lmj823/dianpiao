using System;
using System.Collections.Generic;
using System.Text;

namespace ebankGateway
{
   public class head
    {
       private string reqsysId = "Z01";

        public string ReqsysId
        {
            get { return reqsysId; }
            set { reqsysId = value; }
        }
        private string rspsysId = "S16";

        public string RspsysId
        {
            get { return rspsysId; }
            set { rspsysId = value; }
        }
        private string serviceNo = "";

        public string ServiceNo
        {
            get { return serviceNo; }
            set { serviceNo = value; }
        }
        private string sceneNo = "";

        public string SceneNo
        {
            get { return sceneNo; }
            set { sceneNo = value; }
        }
        private string tradeCode = "";

        public string TradeCode
        {
            get { return tradeCode; }
            set { tradeCode = value; }
        }
        private string serviceVer = "1.0.0";

        public string ServiceVer
        {
            get { return serviceVer; }
            set { serviceVer = value; }
        }
        private string sceneVer = "01";

        public string SceneVer
        {
            get { return sceneVer; }
            set { sceneVer = value; }
        }
        private string globalSeq = "Z01" + DateTime.Now.ToString("yyyyMMdd")+ConfigApp.Xtgzh;

        public string GlobalSeq
        {
            get { return globalSeq; }
            set { globalSeq = value; }
        }
        private string globaTraceNo = "";

        public string GlobaTraceNo
        {
            get { return globaTraceNo; }
            set { globaTraceNo = value; }
        }
        private string channelCode = "NET";

        public string ChannelCode
        {
            get { return channelCode; }
            set { channelCode = value; }
        }
        private string orgNum = "";

        public string OrgNum
        {
            get { return orgNum; }
            set { orgNum = value; }
        }
        private string telrNum = "";

        public string TelrNum
        {
            get { return telrNum; }
            set { telrNum = value; }
        }
        private string zoneNo = "";

        public string ZoneNo
        {
            get { return zoneNo; }
            set { zoneNo = value; }
        }
        private string myBank = "001";

        public string MyBank
        {
            get { return myBank; }
            set { myBank = value; }
        }
        private string authOrgNum = "";

        public string AuthOrgNum
        {
            get { return authOrgNum; }
            set { authOrgNum = value; }
        }
        private string authTelrNum = "";

        public string AuthTelrNum
        {
            get { return authTelrNum; }
            set { authTelrNum = value; }
        }
        private string termiId = "";

        public string TermiId
        {
            get { return termiId; }
            set { termiId = value; }
        }
        private string reqDate = DateTime.Now.ToString("yyyyMMdd");

        public string ReqDate
        {
            get { return reqDate; }
            set { reqDate = value; }
        }
        private string reqtime = DateTime.Now.ToString("HHmmss");

        public string Reqtime
        {
            get { return reqtime; }
            set { reqtime = value; }
        }
        private string reqSerno = "";

        public string ReqSerno
        {
            get { return reqSerno; }
            set { reqSerno = value; }
        }
        private string rspSeq = "";

        public string RspSeq
        {
            get { return rspSeq; }
            set { rspSeq = value; }
        }
        private string rspDate = "";

        public string RspDate
        {
            get { return rspDate; }
            set { rspDate = value; }
        }
        private string rspTime = "";

        public string RspTime
        {
            get { return rspTime; }
            set { rspTime = value; }
        }
        private string errorTyp = "";

        public string ErrorTyp
        {
            get { return errorTyp; }
            set { errorTyp = value; }
        }
        private string errorCode = "";

        public string ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }
        private string errorMsg = "";

        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }
        private string mac = "";

        public string Mac
        {
            get { return mac; }
            set { mac = value; }
        }
    }
}
