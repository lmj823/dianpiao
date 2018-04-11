using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ebankGateway
{
    class ini
    {
       
       [System.Runtime.InteropServices.DllImport("kernel32")]
       private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

       
       [System.Runtime.InteropServices.DllImport("kernel32")]
       private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
       
        private string sPath = null;
       
      
        public ini(string path)
        {
            sPath = path;
        }
        public ini()
        {
            sPath = System.Windows.Forms.Application.StartupPath + "\\Config.ini";           
        }

      
       public void Write(string section, string key, string value)
       {
           
           WritePrivateProfileString(section, key, value, sPath);
       }

      
       public string ReadValue(string section, string key)
       {
           
           System.Text.StringBuilder temp = new System.Text.StringBuilder(255);

           
           GetPrivateProfileString(section, key, "", temp, 255, sPath);
           return temp.ToString();
       }
    }
}
