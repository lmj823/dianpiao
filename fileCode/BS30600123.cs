using System;
using System.Collections.Generic;
using System.Text;

namespace ebankGateway
{
   public class BS30600123
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
        private string signUpMark = "";

        public string SignUpMark
        {
            get { return signUpMark; }
            set { signUpMark = value; }
        }
        private string transNo = "";

        public string TransNo
        {
            get { return transNo; }
            set { transNo = value; }
        }
        private string reserve1 = "";

        public string Reserve1
        {
            get { return reserve1; }
            set { reserve1 = value; }
        }
        private string remark1 = "";

        public string Remark1
        {
            get { return remark1; }
            set { remark1 = value; }
        }
        private string remark2 = "";

        public string Remark2
        {
            get { return remark2; }
            set { remark2 = value; }
        }
    }
}
