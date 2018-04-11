using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ebankGateway
{
    class MultPara
    {
        static string filename = Application.StartupPath + "\\MultSet.ini";
      
        public static string getHead(string nodeNo,string channel)
        {            
            ini myIni = new ini(filename);
            string areaNo = myIni.ReadValue(nodeNo, "areaNo").ToString();
            string instNo = myIni.ReadValue(nodeNo, "instNo").ToString();
            string teller = myIni.ReadValue(nodeNo, "teller").ToString();          
            string term = myIni.ReadValue(nodeNo, "term").ToString();

            switch (channel)
            {
                case "N":
                    teller = "9999";
                    break;
                case "M":
                    teller = "9994";
                    break;
                case "W":
                    teller = "9993";
                    break;
                default:
                    break;
            }

            return areaNo + instNo + teller + term;
            
        }
       
        public static string getInstNo(string nodeNo)
        {
            ini myIni = new ini(filename);
            return myIni.ReadValue(nodeNo, "instNo").ToString();
        }
       
        public static string getTeller(string nodeNo)
        {
            ini myIni = new ini(filename);
            return myIni.ReadValue(nodeNo, "teller").ToString();
        }
       
        public static string getHostIP(string nodeNo)
        {
            ini myIni = new ini(filename);
            return myIni.ReadValue(nodeNo, "hostIP").ToString();
        }      
       
        public static string getHostPort(string nodeNo)
        {
            ini myIni = new ini(filename);
            return myIni.ReadValue(nodeNo, "hostPort").ToString();
        }
       
        public static string getBankNo(string nodeNo)
        {
            ini myIni = new ini(filename);
            return myIni.ReadValue(nodeNo, "bankNo").ToString();
        }
      
        public static string getBankName(string nodeNo)
        {
            ini myIni = new ini(filename);
            return myIni.ReadValue(nodeNo, "name").ToString();
        }
      
        public static string getAreaNo(string nodeNo)
        {
            ini myIni = new ini(filename);
            return myIni.ReadValue(nodeNo, "areaNo").ToString();
        }
       
        public static string getKaBin(string nodeNo)
        {
            ini myIni = new ini(filename);
            return myIni.ReadValue(nodeNo, "kaBin").ToString();
        }
      
        public static string getSmsBankNo(string nodeNo)
        {
            ini myIni = new ini(filename);
            return myIni.ReadValue(nodeNo, "smsBankNo").ToString();
        }

    }
}
