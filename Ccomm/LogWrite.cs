using System;
using System.Xml;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading;
namespace ebankGateway
{

    public class LogWrite
    {

        public LogWrite()
        {

        }
        private static object logxml;        
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

        private static object LogXml
        {
            get
            {
                if (logxml == null)
                {
                    Interlocked.CompareExchange(ref  logxml, new object(), null);
                }
                return logxml;
            }
        }
        private static object _syncRootMult;
        private static object SyncRootMult
        {
            get
            {
                if (_syncRootMult == null)
                {
                    Interlocked.CompareExchange(ref  _syncRootMult, new object(), null);
                }
                return _syncRootMult;
            }
        }
        private static object _syncRootMultUTF8;
        private static object SyncRootMultUTF8
        {
            get
            {
                if (_syncRootMultUTF8 == null)
                {
                    Interlocked.CompareExchange(ref  _syncRootMultUTF8, new object(), null);
                }
                return _syncRootMultUTF8;
            }
        }

        private static object _syncRootHM;
        private static object SyncRootHM
        {
            get
            {
                if (_syncRootHM == null)
                {
                    Interlocked.CompareExchange(ref  _syncRootHM, new object(), null);
                }
                return _syncRootHM;
            }
        }
        
  
        public static void WriteLog(string text, string name)
        {
            lock (SyncRoot)
            { 
                string filepath = Application.StartupPath + "\\SystemLog\\" + DateTime.Now.ToString("yyyyMM");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                string filename = filepath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";

                StreamWriter sw = new StreamWriter(filename , true, System.Text.Encoding.GetEncoding("GBK"), 1000);

                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + name.PadRight(18, ' ') + " " + text);
                sw.Close();
            }

        }
        public static void WriteLog(string text, string name, string nodeNo)
        {
            WriteLog(text, name, "GBK", nodeNo);
        }

        public static void WriteLog(string text, string name,string ecod,string nodeNo)
        {
            lock (SyncRootMult)
            {
                string filepath = Application.StartupPath + "\\SystemLog\\" + DateTime.Now.ToString("yyyyMM");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                string filename = filepath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "(" + nodeNo + ").log";

                StreamWriter sw = new StreamWriter(filename, true, System.Text.Encoding.GetEncoding(ecod), 1000);

                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + name.PadRight(18, ' ') + " " + text);
                sw.Close();
            }

        }

        public static void WriteLogUTF8(string text, string name, string nodeNo)
        {
            lock (SyncRootMultUTF8)
            {
                string filepath = Application.StartupPath + "\\SystemLog\\" + DateTime.Now.ToString("yyyyMM");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                string filename = filepath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "(" + nodeNo + ")ET.log";

                StreamWriter sw = new StreamWriter(filename, true, System.Text.Encoding.GetEncoding("utf-8"), 1000);

                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + name.PadRight(18, ' ') + " " + text);
                sw.Close();
            }
        }
        public static void WriteLogHM(string text, string name)
        {
            lock (SyncRootHM)
            {
                string filepath = Application.StartupPath + "\\SystemLog\\" + DateTime.Now.ToString("yyyyMM");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string filename = filepath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "JMJ.log";

                StreamWriter sw = new StreamWriter(filename, true, System.Text.Encoding.GetEncoding("GBK"), 1000);

                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + name.PadRight(15, ' ') + " " + text);
                sw.Close();
            }

        }

        public static void WriteLog(XmlDocument doc, string name)
        {
            lock (LogXml)
            {
                string filepath = Application.StartupPath + "\\SystemLog\\";
                DirectoryInfo Rootdtif = new DirectoryInfo(filepath);
                string trantype = name.Substring(0, 4);
                string folderName = DateTime.Now.ToString("yyyyMM");                

                DirectoryInfo[] foleder = Rootdtif.GetDirectories(folderName);
                DirectoryInfo folerNode = new DirectoryInfo(filepath + folderName);
                if (foleder.Length <= 0)
                {
                    folerNode.Create();
                }
                string path = filepath + folderName + "\\" + name;

                doc.Save(filepath + folderName + "\\" + name + ".xml");
            }
        }
        



    }
}
