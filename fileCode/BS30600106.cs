using System;
using System.Collections.Generic;
using System.Text;

namespace ebankGateway
{
   public  class BS30600106
    {
        private string billId = "";

        public string BillId
        {
            get { return billId; }
            set { billId = value; }
        }
        private string applicantAcctNo = "";

        public string ApplicantAcctNo
        {
            get { return applicantAcctNo; }
            set { applicantAcctNo = value; }
        }
        private string billNo = "";

        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }
        private string status = "";

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string startTransDt = "";

        public string StartTransDt
        {
            get { return startTransDt; }
            set { startTransDt = value; }
        }
        private string endTransDt = "";

        public string EndTransDt
        {
            get { return endTransDt; }
            set { endTransDt = value; }
        }
        private string minStartTransDt = "";

        public string MinStartTransDt
        {
            get { return minStartTransDt; }
            set { minStartTransDt = value; }
        }
        private string maxStartTransDt = "";

        public string MaxStartTransDt
        {
            get { return maxStartTransDt; }
            set { maxStartTransDt = value; }
        }
        private string minEndTransDt = "";

        public string MinEndTransDt
        {
            get { return minEndTransDt; }
            set { minEndTransDt = value; }
        }
        private string maxEndTransDt = "";

        public string MaxEndTransDt
        {
            get { return maxEndTransDt; }
            set { maxEndTransDt = value; }
        }
        private string transType = "";

        public string TransType
        {
            get { return transType; }
            set { transType = value; }
        }
        private string signFlag = "";

        public string SignFlag
        {
            get { return signFlag; }
            set { signFlag = value; }
        }
        private string transNo = "";

        public string TransNo
        {
            get { return transNo; }
            set { transNo = value; }
        }
        private string xTransNo = "";

        public string XTransNo
        {
            get { return xTransNo; }
            set { xTransNo = value; }
        }
        private string statusCode = "";

        public string StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }
        private string transId = "";

        public string TransId
        {
            get { return transId; }
            set { transId = value; }
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
