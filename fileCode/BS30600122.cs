using System;
using System.Collections.Generic;
using System.Text;

namespace ebankGateway
{
   public class BS30600122
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
        private string source = "";

        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        private string reserve1 = "";

        public string Reserve1
        {
            get { return reserve1; }
            set { reserve1 = value; }
        }
    }
}
