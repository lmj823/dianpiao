using System;
using System.Collections.Generic;
using System.Text;

namespace ebankGateway
{
   public class BS30600107
    {
        private string applicantAcctNo = "";

        public string ApplicantAcctNo
        {
            get { return applicantAcctNo; }
            set { applicantAcctNo = value; }
        }
        private string transNo = "";

        public string TransNo
        {
            get { return transNo; }
            set { transNo = value; }
        }
        private string remitter = "";

        public string Remitter
        {
            get { return remitter; }
            set { remitter = value; }
        }
        private string payee = "";

        public string Payee
        {
            get { return payee; }
            set { payee = value; }
        }
        private string draweeBankName = "";

        public string DraweeBankName
        {
            get { return draweeBankName; }
            set { draweeBankName = value; }
        }
        private string billNo = "";

        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }
        private string maxAcptDt = "";

        public string MaxAcptDt
        {
            get { return maxAcptDt; }
            set { maxAcptDt = value; }
        }
        private string minAcptDt = "";

        public string MinAcptDt
        {
            get { return minAcptDt; }
            set { minAcptDt = value; }
        }
        private string maxDueDt = "";

        public string MaxDueDt
        {
            get { return maxDueDt; }
            set { maxDueDt = value; }
        }
        private string minDueDt = "";

        public string MinDueDt
        {
            get { return minDueDt; }
            set { minDueDt = value; }
        }
        private string maxBillMoney = "";

        public string MaxBillMoney
        {
            get { return maxBillMoney; }
            set { maxBillMoney = value; }
        }
        private string minBillMoney = "";

        public string MinBillMoney
        {
            get { return minBillMoney; }
            set { minBillMoney = value; }
        }
        private string reserve1 = "";

        public string Reserve1
        {
            get { return reserve1; }
            set { reserve1 = value; }
        }
        private string pageSize = "";

        public string PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        private string currentPage = "";

        public string CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }
    }
}
