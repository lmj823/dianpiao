using System;
using System.Collections.Generic;
using System.Text;

namespace ebankGateway
{
   public class BS30600006:BS30600016
    {
        private string source = "";

        public string Source
        {
            get { return source; }
            set { source = value; }
        }
    }
}
