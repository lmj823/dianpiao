using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ebankGateway
{
    class ET
    {
         private XmlDocument xdoc;
        private const string errMsg = "系统出现未知错误，如果您正在做动账交易，请查询余额确认交易是否成功！";
        string trandType = "";
        string nodeNo = "";
        XmlHelper xher;
        public ET(XmlDocument doc, string trandtype, string nodeno)
        {
            xdoc = doc;
            trandType = trandtype;
            nodeNo = nodeno;
            xher = new XmlHelper(XmlDocByFile());
        }
        
        public string Done()
        {
            string retdata = null;

            switch (trandType)
            {
                case "ET0102"://新增票据信息                    
                    retdata = ET0102();
                    break;
                case "ET0103"://修改票据信息                    
                    retdata = ET0103();
                    break;
                case "ET0104"://删除票据信息                    
                    retdata = ET0104();
                    break;
                case "ET0106"://查询待出票登记的票据                  
                    retdata = ET0106();
                    break;
                case "ET0107"://出票登记                  
                    retdata = ET0107();
                    break;               
                case "ET0131"://提示收票               
                    retdata = ET0131();
                    break;
                case "ET0141"://撤票申请               
                    retdata = ET0141();
                    break;
                case "ET0111"://提示承兑             
                    retdata = ET0111();
                    break;
                case "ET0121"://承兑同意签收             
                    retdata = ET0121();
                    break;
                case "ET0122"://承兑拒绝签收             
                    retdata = ET0122();
                    break;
                case "ET0113"://撤消提示承兑    
                    retdata = ET0113();
                    break;
                case "ET0140"://撤消提示承兑    
                    retdata = ET0140();
                    break;
                default:
                    string s1 = @"<?xml version=""1.0"" encoding=""GBK""?><ebank><hostReturnCode>9999</hostReturnCode><hostErrorMessage>未定义的交易类型</hostErrorMessage></ebank>";
                    retdata = Encoding.GetEncoding("GBK").GetBytes(s1).Length.ToString().PadLeft(6, '0') + s1;
                    break;
            }
            return retdata;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////           交易开始            ///////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////      
        private string ET0140()
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600004 b = new BS30600004();//实际为30600003
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillType = xdoc.SelectSingleNode("//ebank//billType").InnerText.Trim();
                b.BillClass = xdoc.SelectSingleNode("//ebank//billClass").InnerText.Trim();
                b.AcptDt = xdoc.SelectSingleNode("//ebank//acptDt").InnerText.Trim().Replace("-", "");
                b.DueDt = xdoc.SelectSingleNode("//ebank//dueDt").InnerText.Trim().Replace("-", "");
                b.BillMoney = xdoc.SelectSingleNode("//ebank//billMoney").InnerText.Trim();
                b.Remitter = xdoc.SelectSingleNode("//ebank//remitterCustName").InnerText.Trim();
                b.RemitterAcctNo = xdoc.SelectSingleNode("//ebank//remitterCustAcct").InnerText.Trim();
                b.RemitterBankName = xdoc.SelectSingleNode("//ebank//remitterCustBank").InnerText.Trim();
                b.RemitterBankNo = xdoc.SelectSingleNode("//ebank//remitterCustBankNo").InnerText.Trim();
                b.Payee = xdoc.SelectSingleNode("//ebank//payee").InnerText.Trim();
                b.PayeeAcctNo = xdoc.SelectSingleNode("//ebank//payeeAcct").InnerText.Trim();
                b.PayeeBankName = xdoc.SelectSingleNode("//ebank//payeeBank").InnerText.Trim();
                b.PayeeBankNo = xdoc.SelectSingleNode("//ebank//payeeBankNo").InnerText.Trim();
                b.Acceptor = xdoc.SelectSingleNode("//ebank//acceptor").InnerText.Trim();
                b.AcceptorAcctNo = xdoc.SelectSingleNode("//ebank//acceptorAcct").InnerText.Trim();
                b.AcceptorBankName = xdoc.SelectSingleNode("//ebank//acceptorBank").InnerText.Trim();
                b.AcceptorBankNo = xdoc.SelectSingleNode("//ebank//acceptorBankNo").InnerText.Trim();
                b.ForbidFlag = xdoc.SelectSingleNode("//ebank//protEndors").InnerText.Trim();
                b.ConferNo = xdoc.SelectSingleNode("//ebank//conferNo").InnerText.Trim();
                b.InvoiceNo = "";
                b.Origin = "1";

                if (BSET.BS30600004(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                    if (doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim() == "000000")
                    {
                        xher = new XmlHelper(xdoc);
                        xher.UpdateContent("/ebank/rgct_id", doc1.SelectSingleNode("//root//body//billId").InnerText.Trim());
                        xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                        xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                    }
                }

                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }
        
        private string ET0102()
        {
            try
            {
                XmlDocument doc1=new XmlDocument();
                BS30600004 b=new BS30600004();//实际为30600003
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillType = xdoc.SelectSingleNode("//ebank//billType").InnerText.Trim();
                b.BillClass = xdoc.SelectSingleNode("//ebank//billClass").InnerText.Trim();
                b.AcptDt = xdoc.SelectSingleNode("//ebank//acptDt").InnerText.Trim().Replace("-","");
                b.DueDt = xdoc.SelectSingleNode("//ebank//dueDt").InnerText.Trim().Replace("-","");
                b.BillMoney = xdoc.SelectSingleNode("//ebank//billMoney").InnerText.Trim();
                b.Remitter = xdoc.SelectSingleNode("//ebank//remitterCustName").InnerText.Trim();
                b.RemitterAcctNo = xdoc.SelectSingleNode("//ebank//remitterCustAcct").InnerText.Trim();
                b.RemitterBankName = xdoc.SelectSingleNode("//ebank//remitterCustBank").InnerText.Trim();
                b.RemitterBankNo = xdoc.SelectSingleNode("//ebank//remitterCustBankNo").InnerText.Trim();
                b.Payee = xdoc.SelectSingleNode("//ebank//payee").InnerText.Trim();
                b.PayeeAcctNo = xdoc.SelectSingleNode("//ebank//payeeAcct").InnerText.Trim();
                b.PayeeBankName = xdoc.SelectSingleNode("//ebank//payeeBank").InnerText.Trim();
                b.PayeeBankNo = xdoc.SelectSingleNode("//ebank//payeeBankNo").InnerText.Trim();
                b.Acceptor = xdoc.SelectSingleNode("//ebank//acceptor").InnerText.Trim();
                b.AcceptorAcctNo = xdoc.SelectSingleNode("//ebank//acceptorAcct").InnerText.Trim();
                b.AcceptorBankName = xdoc.SelectSingleNode("//ebank//acceptorBank").InnerText.Trim();
                b.AcceptorBankNo = xdoc.SelectSingleNode("//ebank//acceptorBankNo").InnerText.Trim();
                b.ForbidFlag = xdoc.SelectSingleNode("//ebank//protEndors").InnerText.Trim();
                b.ConferNo = xdoc.SelectSingleNode("//ebank//conferNo").InnerText.Trim();
                b.InvoiceNo ="";
                b.Origin = "1";

                if (BSET.BS30600004(b,nodeNo,trandType,ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                    if (doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim() == "000000")
                    {
                        xher = new XmlHelper(xdoc);
                        xher.UpdateContent("/ebank/rgct_id",doc1.SelectSingleNode("//root//body//billId").InnerText.Trim());
                        xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                        xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                    }
                }
                
                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }


        private string ET0103()
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600004 b = new BS30600004();
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillType = xdoc.SelectSingleNode("//ebank//billType").InnerText.Trim();
                b.BillClass = xdoc.SelectSingleNode("//ebank//billClass").InnerText.Trim();
                b.AcptDt = xdoc.SelectSingleNode("//ebank//acptDt").InnerText.Trim().Replace("-", "");
                b.DueDt = xdoc.SelectSingleNode("//ebank//dueDt").InnerText.Trim().Replace("-", "");
                b.BillMoney = xdoc.SelectSingleNode("//ebank//billMoney").InnerText.Trim();
                b.Remitter = xdoc.SelectSingleNode("//ebank//remitterCustName").InnerText.Trim();
                b.RemitterAcctNo = xdoc.SelectSingleNode("//ebank//remitterCustAcct").InnerText.Trim();
                b.RemitterBankName = xdoc.SelectSingleNode("//ebank//remitterCustBank").InnerText.Trim();
                b.RemitterBankNo = xdoc.SelectSingleNode("//ebank//remitterCustBankNo").InnerText.Trim();
                b.Payee = xdoc.SelectSingleNode("//ebank//payee").InnerText.Trim();
                b.PayeeAcctNo = xdoc.SelectSingleNode("//ebank//payeeAcct").InnerText.Trim();
                b.PayeeBankName = xdoc.SelectSingleNode("//ebank//payeeBank").InnerText.Trim();
                b.PayeeBankNo = xdoc.SelectSingleNode("//ebank//payeeBankNo").InnerText.Trim();
                b.Acceptor = xdoc.SelectSingleNode("//ebank//acceptor").InnerText.Trim();
                b.AcceptorAcctNo = xdoc.SelectSingleNode("//ebank//acceptorAcct").InnerText.Trim();
                b.AcceptorBankName = xdoc.SelectSingleNode("//ebank//acceptorBank").InnerText.Trim();
                b.AcceptorBankNo = xdoc.SelectSingleNode("//ebank//acceptorBankNo").InnerText.Trim();
                b.ForbidFlag = xdoc.SelectSingleNode("//ebank//protEndors").InnerText.Trim();
                b.ConferNo = xdoc.SelectSingleNode("//ebank//conferNo").InnerText.Trim();
                b.InvoiceNo = "";
                b.BillId = xdoc.SelectSingleNode("//ebank//rgct_id").InnerText.Trim();

                if (BSET.BS30600004(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                    if (doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim() == "000000")
                    {
                        xher = new XmlHelper(xdoc);
                        xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                        xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                    }
                }

                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }


        private string ET0104()
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600004 b = new BS30600004();
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillId = xdoc.SelectSingleNode("//ebank//rgct_ids").InnerText.Trim();


                if (BSET.BS30600007(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                }

                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }

        private string ET0106()
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600107 b = new BS30600107();
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.MaxBillMoney = xdoc.SelectSingleNode("//ebank//billMoney").InnerText.Trim();
                b.MinBillMoney = xdoc.SelectSingleNode("//ebank//billMoney").InnerText.Trim();
                b.MaxAcptDt = xdoc.SelectSingleNode("//ebank//acptDt").InnerText.Trim().Replace("-","");
                b.MinAcptDt = xdoc.SelectSingleNode("//ebank//acptDt").InnerText.Trim().Replace("-", "");
                b.MaxDueDt = xdoc.SelectSingleNode("//ebank//dueDt").InnerText.Trim().Replace("-", "");
                b.MinDueDt = xdoc.SelectSingleNode("//ebank//dueDt").InnerText.Trim().Replace("-", "");
                b.Remitter = xdoc.SelectSingleNode("//ebank//remitter").InnerText.Trim();
                b.Payee = xdoc.SelectSingleNode("//ebank//payee").InnerText.Trim();
                b.PageSize = xdoc.SelectSingleNode("//ebank//pageSize").InnerText.Trim();
                b.CurrentPage = xdoc.SelectSingleNode("//ebank//currentPage").InnerText.Trim();
                b.TransNo = "200101";
                if (BSET.BS30600107(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    if (doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim() == "")
                    {
                        string recode = doc1.SelectSingleNode("//root//body//error_code").InnerText.Trim();
                        if (recode != "")
                        {
                            xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//body//error_code").InnerText.Trim());
                            xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//body//error_text").InnerText.Trim());
                        }
                        else
                        {
                            xher.UpdateContent("/ebank/hostReturnCode", "99999");
                            xher.UpdateContent("/ebank/hostErrorMessage", "未知错误");
                        }
                    }
                    else if (doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim() != "000000")
                    {
                        xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                        xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                    }
                    else
                    {
                        xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                        xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());

                    }
                }
                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }

        private string ET0107()
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600002 b = new BS30600002();
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillId = xdoc.SelectSingleNode("//ebank//rgct_ids").InnerText.Trim();
                b.Flag = "2";
                b.Signature = xdoc.SelectSingleNode("//ebank//warteeSign").InnerText.Trim();

                if (BSET.BS30600002(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                }

                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }

        private string ET0131()
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600016 b = new BS30600016();
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillId = xdoc.SelectSingleNode("//ebank//rgct_ids").InnerText.Trim();
                b.Signature = xdoc.SelectSingleNode("//ebank//warteeSign").InnerText.Trim();

                if (BSET.BS30600016(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                }

                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }

        private string ET0141()
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600016 b = new BS30600016();//字段一样，实际为核心30600026
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillId = xdoc.SelectSingleNode("//ebank//rgct_ids").InnerText.Trim();
                b.Signature = xdoc.SelectSingleNode("//ebank//warteeSign").InnerText.Trim();

                if (BSET.BS30600016(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                }

                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }

        private string ET0111()
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600006 b = new BS30600006();//字段一样，实际为核心30600006
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillId = xdoc.SelectSingleNode("//ebank//rgct_ids").InnerText.Trim();
                b.Signature = xdoc.SelectSingleNode("//ebank//warteeSign").InnerText.Trim();
                b.Source = "1";
                if (BSET.BS30600006(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                }

                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }

        private string ET0121()
        {
            string signUpMark = "1";
            string transNo = "200202";
            return ET30600123(signUpMark,transNo);
        }


        //统一拒绝同意接口
        private string ET30600123(string signUpMark,string transNo)
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600123 b = new BS30600123();
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillId = xdoc.SelectSingleNode("//ebank//rgctIds").InnerText.Trim();
                b.Signature = xdoc.SelectSingleNode("//ebank//warteeSign").InnerText.Trim();
                b.SignUpMark = signUpMark;
                b.TransNo = transNo;

                if (BSET.BS30600123(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                }

                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }

        private string ET0122()
        {
            string signUpMark = "0";
            string transNo = "200202";
            return ET30600123(signUpMark, transNo);
        }


        private string ET0113()
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                BS30600122 b = new BS30600122();
                b.ApplicantAcctNo = xdoc.SelectSingleNode("//ebank//cust_account").InnerText.Trim();
                b.BillId = xdoc.SelectSingleNode("//ebank//rgct_ids").InnerText.Trim();
                b.Signature = xdoc.SelectSingleNode("//ebank//warteeSign").InnerText.Trim();
                b.Source = "1";
                if (BSET.BS30600122(b, nodeNo, trandType, ref doc1) == "error")
                {
                    retSysError(errMsg);
                }
                else
                {
                    xher.UpdateContent("/ebank/hostReturnCode", doc1.SelectSingleNode("//root//head//ErrorCode").InnerText.Trim());
                    xher.UpdateContent("/ebank/hostErrorMessage", doc1.SelectSingleNode("//root//head//ErrorMsg").InnerText.Trim());
                }

                return formatRetStr(xher);
            }
            catch (Exception ex)
            {
                return retCatch(ex);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
       
        private XmlDocument XmlDocByFile()
        {
            XmlDocument doc = XmlHelper.CreateXML("ebank");

            return doc;
        }

        private string retCatch(Exception ex)
        {
            LogWrite.WriteLog(ex.Message, "交易处理失败（" + trandType + "），原因:", nodeNo);

            retSysError(errMsg);
            return DocToStr(xher.GetXmlDoc());
        }
        private void retSysError(string s)
        {
            xher.UpdateContent("/ebank/hostReturnCode", "9998");
            xher.UpdateContent("/ebank/hostErrorMessage", s);
        }

        private string formatRetStr(XmlHelper xh)
        {
            return DocToStr(xh.GetXmlDoc());
        }
        private string DocToStr(XmlDocument doc)
        {
            string s = doc.OuterXml;
            string head = Encoding.GetEncoding("GBK").GetBytes(s).Length.ToString().PadLeft(6, '0');
            string ret = head + s;
            return ret;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////           交易开始            ///////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        //public string billEdit()
        //{
        //    try
        //    {
        //        string ResponseName = MultPara.getTargetName(trandType) + "Response";
        //        string isList = MultPara.getisList(trandType);
        //        XmlHelper xh1 = new XmlHelper(xdoc);
        //        string skey = "";
        //        try
        //        {
        //            if (System.Configuration.ConfigurationManager.AppSettings["encryptor"] == "1")
        //            {
        //                string ip = System.Configuration.ConfigurationManager.AppSettings["encryptorIP"];
        //                int port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["encryptorPORT"]);
        //                int timeout = 60;
        //                string gunionIDOfUnionAPI = "CC";
        //                string macData = MultPara.getBankCode(nodeNo);
        //                string nodeName = "ccfccb." + macData;
        //                int lenOfMacData = macData.Length;

        //                CSH.NodeAPI nApi = new CSH.NodeAPI(ip, port, timeout, gunionIDOfUnionAPI);
        //                skey = nApi.NodeGenerateMac(nodeName, lenOfMacData, Encoding.Default.GetBytes(macData));

        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            LogWrite.WriteLog(ex.Message, "获取secretKey失败，原因:", nodeNo);
        //        }
        //        xh1.UpdateContent(@"/ebank/secretKey", skey);

        //        XmlDocument xmldoc = sendXmlByFile();

        //        string send;
        //        if (isList == "1")
        //        {
        //            send = xmldoc.OuterXml.Replace("para1", xh1.GetBodyPage()).Replace("para", xh1.GetBody());
        //        }
        //        else if (isList == "0" || isList == "")
        //        {
        //            send = xmldoc.OuterXml.Replace("para", xh1.GetBody());
        //        }
        //        else
        //        {
        //            send = xmldoc.OuterXml.Replace("para1", xh1.GetBodyPage(isList)).Replace("para", xh1.GetBody());
        //        }

        //        string recv = "";
        //        LogWrite.WriteLogUTF8(send, "[SEND]", nodeNo);
        //        if (Ihttp.Send(MultPara.getUrl(nodeNo,trandType), send, ref recv))
        //        {
        //            LogWrite.WriteLogUTF8(recv, "[RECV]", nodeNo);

        //            XmlDocument recvDoc = new XmlDocument();
        //            recvDoc.LoadXml(recv);
        //            XmlNamespaceManager xnm = new XmlNamespaceManager(recvDoc.NameTable);
        //            xnm.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        //            xnm.AddNamespace("ns2", MultPara.getNameSpace(trandType));

        //            XmlNode recvNode = recvDoc.SelectSingleNode("/soap:Envelope/soap:Body/ns2:" + ResponseName + "/return", xnm);
        //            XmlNode errcode = recvNode.SelectSingleNode("errCode");
        //            XmlNode errmsg = recvNode.SelectSingleNode("errMsg");
        //            if (errcode != null)
        //            {
        //                string err1 = errcode.InnerText.Trim();
        //                string err2 = "";
        //                if (errmsg != null)
        //                {
        //                    err2 = errmsg.InnerText.Trim();
        //                }
        //                if (err1 == "1" && err2.Length == 0)
        //                {
        //                    xher.UpdateContent(@"/ebank/hostReturnCode", "0000");
        //                    xher.UpdateContent(@"/ebank/hostErrorMessage", "交易成功");
        //                    if (isList == "1")
        //                    {
        //                        xher.UpdateContent(@"/ebank/totalRows", recvNode.SelectSingleNode("totalRows").InnerText.Trim());
        //                        xher.UpdateContent(@"/ebank/totalPages", recvNode.SelectSingleNode("totalPages").InnerText.Trim());
        //                        int count = 0;
        //                        xher.AddList(recvNode.SelectNodes("list"), ref count);
        //                        xher.UpdateContent(@"/ebank/listnm", count.ToString());
        //                    }
        //                    else
        //                    {
        //                        XmlNode nd = recvNode.SelectSingleNode("list");
        //                        if (nd != null && nd.ChildNodes.Count > 0)
        //                        {
        //                            xher.AddChild(recvNode.SelectSingleNode("list"));
        //                        }
        //                    }                            
        //                }
        //                else
        //                {
        //                    xher.UpdateContent(@"/ebank/hostReturnCode", err1);
        //                    xher.UpdateContent(@"/ebank/hostErrorMessage", err2);                            
        //                }
        //            }
        //            else
        //            {
        //                retSysError(errMsg);                        
        //            }
        //        }
        //        else
        //        {
        //            retSysError(errMsg);                    
        //        }

        //        return formatRetStr(xher);
        //    }
        //    catch (Exception ex)
        //    {
        //        return retCatch(ex);
        //    }
        //}
    }
}
