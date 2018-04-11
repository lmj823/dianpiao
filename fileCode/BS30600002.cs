using System;
using System.Collections.Generic;
using System.Text;

namespace ebankGateway
{
   public class BS30600002
    {
        private string applicantAcctNo = "";

        public string ApplicantAcctNo
        {
            get { return applicantAcctNo; }
            set { applicantAcctNo = value; }
        }
        private string billId = "";

        public string BillId
        {
            get { return billId; }
            set { billId = value; }
        }
        private string signature = "";

        public string Signature
        {
            get { return signature; }
            set { signature = value; }
        }
        private string source = "1";

        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        private string flag = "";

        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        private string reserve1 = "";

        public string Reserve1
        {
            get { return reserve1; }
            set { reserve1 = value; }
        }
    }
}
