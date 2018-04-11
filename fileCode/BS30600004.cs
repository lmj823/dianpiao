using System;
using System.Collections.Generic;
using System.Text;

namespace ebankGateway
{
    public class BS30600004:BS30600003
    {
        private string billId = "";

        public string BillId
        {
            get { return billId; }
            set { billId = value; }
        }
    }
}
