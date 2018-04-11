using System;
using System.Threading;
using System.Collections;
using System.Configuration;


namespace ebankGateway
{
    /// <summary>
    /// ConfigApp 的摘要说明。
    /// </summary>
    public class ConfigApp
    {

        private static object _syncRoot;
        private static object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    Interlocked.CompareExchange(ref  _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
        }

        private static object _syncRootLSH;
        private static object SyncRootLSH
        {
            get
            {
                if (_syncRootLSH == null)
                {
                    Interlocked.CompareExchange(ref  _syncRootLSH, new object(), null);
                }
                return _syncRootLSH;
            }
        }

        private static long xtgzh = 1;
        public static string Xtgzh
        {
            get
            {
                lock (SyncRootLSH)
                {
                    ini myIni = new ini();
                    string tmp = myIni.ReadValue("Para", "xtgzh").ToString();
                    if (tmp.Length > 0)
                    {
                        try
                        {
                            if (int.Parse(tmp) > 999999997)
                                tmp = "1";
                            else
                                tmp = (int.Parse(tmp) + 1).ToString();

                            myIni.Write("Para", "xtgzh", tmp);
                            return tmp.PadLeft(12, '0');
                        }
                        catch
                        {
                            return "999999999999";
                        }

                    }
                    return xtgzh.ToString().PadLeft(12, '0');
                }
            }
        }

        private static long lsh = 1;

        public static string Lsh
        {
            get 
            {
                lock (SyncRoot)
                {
                    try
                    {
                        if (lsh > 98)
                        {
                            lsh = 1;
                        }
                        return (++lsh).ToString().PadLeft(2,'0');
                    }
                    catch
                    {
                        lsh = 1;
                        return lsh.ToString().PadLeft(2, '0');
                    }
                }               
            }
            
        }



      
       


        public ConfigApp()
        {

        }

        
      
       

        private static string myPort = ConfigurationManager.AppSettings["MYPORT"].ToString().Trim();
        public static string MyPort
        {
            get { return ConfigApp.myPort; }
            set { ConfigApp.myPort = value; }
        }

       

        private static ArrayList clientIP = getAllowIPlist();

        public static ArrayList ClientIP
        {
            get { return ConfigApp.clientIP; }            
        }
        private static ArrayList getAllowIPlist()
        {
            ArrayList al = new ArrayList();
            try
            {
                string file = System.Windows.Forms.Application.StartupPath + "\\hosts";
                System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Open);
                System.IO.StreamReader sr = new System.IO.StreamReader(fs, System.Text.Encoding.GetEncoding("GBK"));

                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    else if (line.IndexOf(';') == 0)
                    {
                        continue;
                    }
                    else
                    {
                        al.Add(line.Trim());
                    }
                }

                sr.Close();
                fs.Close();
            }
            catch(Exception ex)
            {
                LogWrite.WriteLog(ex.Message, "加载hosts出错");
                al.Add("*.*.*.*");
            }
            return al;
            
        }

       


        private static string lhhUpdateTtime = ConfigurationManager.AppSettings["LHHUPDATETIME"].ToString().Trim();//

        public static string LhhUpdateTtime
        {
            get { return ConfigApp.lhhUpdateTtime; }
            set { ConfigApp.lhhUpdateTtime = value; }
        }
        
        private static string timerTick = ConfigurationManager.AppSettings["TIMERTICK"].ToString().Trim();//

        public static string TimerTick
        {
            get { return ConfigApp.timerTick; }
            set { ConfigApp.timerTick = value; }
        }
        private static string show = ConfigurationManager.AppSettings["SHOW"].ToString().Trim();

        public static bool Show
        {
            get
            {
                if (ConfigApp.show == "1")
                    return true;
                else
                    return false;
            }
        }
    }
}
