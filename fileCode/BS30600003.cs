using System;
using System.Collections.Generic;
using System.Text;

namespace ebankGateway
{
   public class BS30600003
    {
       private string applicantAcctNo = "";
        /// <summary>
       /// 当前登陆客户账号
        /// </summary>
       public string ApplicantAcctNo
        {
            get { return applicantAcctNo; }
            set { applicantAcctNo = value; }
        }

       private string billType = "";

       public string BillType
       {
           get { return billType; }
           set { billType = value; }
       }

       private string billClass = "";

       public string BillClass
       {
           get { return billClass; }
           set { billClass = value; }
       }
       private string acptDt = "";

       public string AcptDt
       {
           get { return acptDt; }
           set { acptDt = value; }
       }
       private string dueDt = "";

       public string DueDt
       {
           get { return dueDt; }
           set { dueDt = value; }
       }
       private string billMoney = "";

       public string BillMoney
       {
           get { return billMoney; }
           set { billMoney = value; }
       }
       private string remitter = "";

       public string Remitter
       {
           get { return remitter; }
           set { remitter = value; }
       }
       private string remitterAcctNo = "";

       public string RemitterAcctNo
       {
           get { return remitterAcctNo; }
           set { remitterAcctNo = value; }
       }
       private string remitterBankName = "";


       public string RemitterBankName
       {
           get { return remitterBankName; }
           set { remitterBankName = value; }
       }
       private string remitterBankNo = "";

       public string RemitterBankNo
       {
           get { return remitterBankNo; }
           set { remitterBankNo = value; }
       }
       private string payee = "";

       public string Payee
       {
           get { return payee; }
           set { payee = value; }
       }
       private string payeeAcctNo = "";

       public string PayeeAcctNo
       {
           get { return payeeAcctNo; }
           set { payeeAcctNo = value; }
       }
       private string payeeBankName = "";

       public string PayeeBankName
       {
           get { return payeeBankName; }
           set { payeeBankName = value; }
       }
       private string payeeBankNo = "";

       public string PayeeBankNo
       {
           get { return payeeBankNo; }
           set { payeeBankNo = value; }
       }
       private string acceptor = "";

       public string Acceptor
       {
           get { return acceptor; }
           set { acceptor = value; }
       }
       private string acceptorAcctNo = "";

       public string AcceptorAcctNo
       {
           get { return acceptorAcctNo; }
           set { acceptorAcctNo = value; }
       }
       private string acceptorBankName = "";

       public string AcceptorBankName
       {
           get { return acceptorBankName; }
           set { acceptorBankName = value; }
       }
       private string acceptorBankNo = "";

       public string AcceptorBankNo
       {
           get { return acceptorBankNo; }
           set { acceptorBankNo = value; }
       }
       private string forbidFlag = "";

       public string ForbidFlag
       {
           get { return forbidFlag; }
           set { forbidFlag = value; }
       }
       private string conferNo = "";

       public string ConferNo
       {
           get { return conferNo; }
           set { conferNo = value; }
       }
       private string invoiceNo = "";

       public string InvoiceNo
       {
           get { return invoiceNo; }
           set { invoiceNo = value; }
       }
       private string reserve1 = "";

       public string Reserve1
       {
           get { return reserve1; }
           set { reserve1 = value; }
       }
       private string origin = "";

       public string Origin
       {
           get { return origin; }
           set { origin = value; }
       }
    }
}
