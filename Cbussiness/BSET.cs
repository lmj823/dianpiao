using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ebankGateway
{
    public class BSET
    {
        static string filepath = System.Windows.Forms.Application.StartupPath + "\\Files\\BS.xml";
        /// <summary>
        /// 设置报文头
        /// </summary>
        /// <param name="lsh"></param>
        /// <param name="tranCode"></param>
        /// <returns></returns>
        static private XmlHelper setHead(head s)
        {
            XmlHelper xh = new XmlHelper(filepath, true);
            xh.UpdateContent("/root/head/ReqsysId", s.ReqsysId);
            xh.UpdateContent("/root/head/RspsysId", s.RspsysId);
            xh.UpdateContent("/root/head/ServiceNo", s.ServiceNo);
            xh.UpdateContent("/root/head/SceneNo", s.SceneNo);
            xh.UpdateContent("/root/head/TradeCode", s.TradeCode);
            xh.UpdateContent("/root/head/ServiceVer", s.ServiceVer);
            xh.UpdateContent("/root/head/SceneVer", s.SceneVer);
            xh.UpdateContent("/root/head/GloSeqNo", s.GlobalSeq);
            xh.UpdateContent("/root/head/GlobaTraceNo", s.GlobalSeq+ConfigApp.Lsh);
            xh.UpdateContent("/root/head/ChannelCode", s.ChannelCode);
            xh.UpdateContent("/root/head/OrgNum", s.OrgNum);
            xh.UpdateContent("/root/head/TelrNum", s.TelrNum);
            xh.UpdateContent("/root/head/ZoneNo", s.ZoneNo);
            xh.UpdateContent("/root/head/MyBank", s.MyBank);
            xh.UpdateContent("/root/head/AuthOrgNum", s.AuthOrgNum);
            xh.UpdateContent("/root/head/AuthTelrNum", s.AuthTelrNum);
            xh.UpdateContent("/root/head/TermiId", s.TermiId);
            xh.UpdateContent("/root/head/ReqDate", s.ReqDate);
            xh.UpdateContent("/root/head/Reqtime", s.Reqtime);
            xh.UpdateContent("/root/head/ReqSerno", s.GlobalSeq.Substring(10).PadLeft(16,'0'));
            xh.UpdateContent("/root/head/RspSeq", s.RspSeq);
            xh.UpdateContent("/root/head/RspDate", s.RspDate);
            xh.UpdateContent("/root/head/RspTime", s.RspTime);
            xh.UpdateContent("/root/head/ErrorTyp", s.ErrorTyp);
            xh.UpdateContent("/root/head/ErrorCode", s.ErrorCode);
            xh.UpdateContent("/root/head/ErrorMsg", s.ErrorMsg);
            xh.UpdateContent("/root/head/Mac", s.Mac);
            return xh;
        }

        static public string BS30600004(BS30600004 b,string nodeNo,string trandType,ref XmlDocument xml)
        {
            head s = new head();
            if(trandType=="ET0102")
            {
            s.TradeCode = "WYPJ30600003";
            s.ServiceNo = "P0720001";
            s.SceneNo = "01";
            }
            else if (trandType == "ET0103")
            {
                s.TradeCode = "WYPJ30600004";
                s.ServiceNo = "P0720001";
                s.SceneNo = "02";
            }
            XmlHelper xhs = setHead(s);
            string send = "";
            xhs.setBody("applicantAcctNo", b.ApplicantAcctNo);
            xhs.setBody("billType", b.BillType);
            xhs.setBody("billClass", b.BillClass);
            xhs.setBody("acptDt", b.AcptDt);
            xhs.setBody("dueDt", b.DueDt);
            xhs.setBody("billMoney", b.BillMoney);
            xhs.setBody("remitter", b.Remitter);
            xhs.setBody("remitterAcctNo", b.RemitterAcctNo);
            xhs.setBody("remitterBankName", b.RemitterBankName);
            xhs.setBody("remitterBankNo", b.RemitterBankNo);
            xhs.setBody("payee", b.Payee);
            xhs.setBody("payeeAcctNo", b.PayeeAcctNo);
            xhs.setBody("payeeBankName", b.PayeeBankName);
            xhs.setBody("payeeBankNo", b.PayeeBankNo);
            xhs.setBody("acceptor", b.Acceptor);
            xhs.setBody("acceptorAcctNo", b.AcceptorAcctNo);
            xhs.setBody("acceptorBankName", b.AcceptorBankName);
            xhs.setBody("acceptorBankNo", b.AcceptorBankNo);
            xhs.setBody("forbidFlag", b.ForbidFlag);
            xhs.setBody("conferNo", b.ConferNo);
            xhs.setBody("invoiceNo", b.InvoiceNo);
            xhs.setBody("reserve1", "");
            if (trandType == "ET0103")
            {
                xhs.setBody("billId", b.BillId);
            }
            else
            {
                xhs.setBody("origin", b.Origin);
            }
            
            send = xhs.GetXmlDoc().OuterXml.Length.ToString().PadLeft(8,'0')+xhs.GetXmlDoc().OuterXml;
            XmlDocument recv = new XmlDocument();
            bool jg = tcp.SLSendData(send, nodeNo, trandType, ref xml);
            return jg.ToString();
        }

        static public string BS30600007(BS30600004 b, string nodeNo, string trandType, ref XmlDocument xml)
        {
            head s = new head();
            s.ServiceNo = "P0720001";
            s.SceneNo = "03";
            s.TradeCode = "WYPJ30600007";
            XmlHelper xhs = setHead(s);
            string send = "";
            xhs.setBody("applicantAcctNo", b.ApplicantAcctNo);
            xhs.setBody("billId", b.BillId);
            xhs.setBody("reserve1", b.Reserve1);

            send = xhs.GetXmlDoc().OuterXml.Length.ToString().PadLeft(8, '0') + xhs.GetXmlDoc().OuterXml;
            XmlDocument recv = new XmlDocument();
            bool jg = tcp.SLSendData(send, nodeNo, trandType, ref xml);
            return jg.ToString();
        }


        static public string BS30600107(BS30600107 b, string nodeNo, string trandType, ref XmlDocument xml)
        {
            head s = new head();
            s.ServiceNo="P0730001";
            s.SceneNo="17";
            s.TradeCode = "WYPJ30600107";
            XmlHelper xhs = setHead(s);
            string send = "";
            xhs.setBody("applicantAcctNo", b.ApplicantAcctNo);
            xhs.setBody("transNo", b.TransNo);
            xhs.setBody("remitter", b.Remitter);
            xhs.setBody("payee", b.Payee);
            xhs.setBody("draweeBankName", b.DraweeBankName);
            xhs.setBody("billNo", b.BillNo);
            xhs.setBody("maxAcptDt", b.MaxAcptDt);
            xhs.setBody("minAcptDt", b.MinAcptDt);
            xhs.setBody("maxDueDt", b.MaxDueDt);
            xhs.setBody("minDueDt", b.MinDueDt);
            xhs.setBody("maxBillMoney", b.MaxBillMoney);
            xhs.setBody("minBillMoney", b.MinBillMoney);
            xhs.setBody("reserve1", b.Reserve1);
            xhs.setBody("pageSize", b.PageSize);
            xhs.setBody("currentPage", b.CurrentPage);
            send = xhs.GetXmlDoc().OuterXml.Length.ToString().PadLeft(8, '0') + xhs.GetXmlDoc().OuterXml;
            XmlDocument recv = new XmlDocument();
            bool jg = tcp.SLSendData(send, nodeNo, trandType, ref xml);
            return jg.ToString();
        }

        static public string BS30600002(BS30600002 b, string nodeNo, string trandType, ref XmlDocument xml)
        {
            head s = new head();
            s.SceneNo = "04";
            s.ServiceNo = "P0720001";
            s.TradeCode = "WYPJ30600002";
            XmlHelper xhs = setHead(s);
            string send = "";
            xhs.setBody("applicantAcctNo", b.ApplicantAcctNo);
            xhs.setBody("billId", b.BillId);
            xhs.setBody("flag", b.Flag);
            xhs.setBody("signature", b.Signature);
            xhs.setBody("source", b.Source);
            xhs.setBody("reserve1", b.Reserve1);
          
            send = xhs.GetXmlDoc().OuterXml.Length.ToString().PadLeft(8, '0') + xhs.GetXmlDoc().OuterXml;
            XmlDocument recv = new XmlDocument();
            bool jg = tcp.SLSendData(send, nodeNo, trandType, ref xml);
            return jg.ToString();
        }



        static public string BS30600123(BS30600123 b, string nodeNo, string trandType, ref XmlDocument xml)
        {
            head s = new head();
            s.ServiceNo = "P0720002";
            s.SceneNo = "09";
            s.TradeCode = "WYPJ30600123";
            XmlHelper xhs = setHead(s);
            string send = "";
            xhs.setBody("applicantAcctNo", b.ApplicantAcctNo);
            xhs.setBody("billId", b.BillId);
            xhs.setBody("signUpMark", b.SignUpMark);
            xhs.setBody("signature", b.Signature);
            xhs.setBody("transNo", b.TransNo);
            xhs.setBody("reserve1", b.Reserve1);
            xhs.setBody("remark1", b.Remark1);
            xhs.setBody("remark2", b.Remark2);

            send = xhs.GetXmlDoc().OuterXml.Length.ToString().PadLeft(8, '0') + xhs.GetXmlDoc().OuterXml;
            XmlDocument recv = new XmlDocument();
            bool jg = tcp.SLSendData(send, nodeNo, trandType, ref xml);
            return jg.ToString();
        }

        static public string BS30600122(BS30600122 b, string nodeNo, string trandType, ref XmlDocument xml)
        {
            head s = new head();
            s.ServiceNo = "P0720002";
            s.SceneNo = "08";
            s.TradeCode = "WYPJ30600122";
            XmlHelper xhs = setHead(s);
            string send = "";
            xhs.setBody("applicantAcctNo", b.ApplicantAcctNo);
            xhs.setBody("billId", b.BillId);
            xhs.setBody("signature", b.Signature);
            xhs.setBody("source", b.Source);
            xhs.setBody("reserve1", b.Reserve1);

            send = xhs.GetXmlDoc().OuterXml.Length.ToString().PadLeft(8, '0') + xhs.GetXmlDoc().OuterXml;
            XmlDocument recv = new XmlDocument();
            bool jg = tcp.SLSendData(send, nodeNo, trandType, ref xml);
            return jg.ToString();
        }

        static public string BS30600016(BS30600016 b, string nodeNo, string trandType, ref XmlDocument xml)
        {
            head s = new head();
            if (trandType == "ET0131")
            {
                s.ServiceNo = "P0720002";
                s.SceneNo = "01";
                s.TradeCode = "WYPJ30600016";
            }
            else if (trandType == "ET0141")
            {
                s.ServiceNo = "P0720002";
                s.SceneNo = "02";
                s.TradeCode = "WYPJ30600026";
            }
            else if (trandType == "ET0111")
            {
                s.ServiceNo = "P0720002";
                s.SceneNo = "03";
                s.TradeCode = "WYPJ30600006";
 
            }
            XmlHelper xhs = setHead(s);
            string send = "";
            xhs.setBody("applicantAcctNo", b.ApplicantAcctNo);
            xhs.setBody("billId", b.BillId);
            xhs.setBody("signature", b.Signature);
            xhs.setBody("reserve1", b.Reserve1);

            send = xhs.GetXmlDoc().OuterXml.Length.ToString().PadLeft(8, '0') + xhs.GetXmlDoc().OuterXml;
            bool jg= tcp.SLSendData(send, nodeNo,trandType,ref xml);
            return jg.ToString();
        }

        static public string BS30600006(BS30600006 b, string nodeNo, string trandType, ref XmlDocument xml)
        {
            head s = new head();
            s.ServiceNo = "P0720002";
            s.SceneNo = "03";
            s.TradeCode = "WYPJ30600006";
            XmlHelper xhs = setHead(s);
            string send = "";
            xhs.setBody("applicantAcctNo", b.ApplicantAcctNo);
            xhs.setBody("billId", b.BillId);
            xhs.setBody("signature", b.Signature);
            xhs.setBody("source", b.Source);
            xhs.setBody("reserve1", b.Reserve1);

            send = xhs.GetXmlDoc().OuterXml.Length.ToString().PadLeft(8, '0') + xhs.GetXmlDoc().OuterXml;
            bool jg = tcp.SLSendData(send, nodeNo, trandType, ref xml);
            return jg.ToString();
        }
    }
}
