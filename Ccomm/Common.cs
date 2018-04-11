using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.IO.Compression;
//using Microsoft.VisualBasic.Devices;
using System.Threading;

namespace ebankGateway
{
    class Common
    {
        public static void DeleteFile(string filePath, int totalHours)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo[] fi = di.GetFiles("*.*");
                DateTime dtNow = DateTime.Now;
                foreach (FileInfo tmpfi in fi)
                {
                    //if (tmpfi.Name != "BankList.txt")
                    //{
                    TimeSpan ts = dtNow.Subtract(tmpfi.LastWriteTime);
                    if (ts.TotalHours > totalHours)//距现在totalHours小时以上
                    {
                        tmpfi.Delete();//删除文件
                    }
                    //}
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static void MoveFile(string filePath, int totalHours)
        {
            try
            {
                string p = System.Windows.Forms.Application.StartupPath + "\\SystemLog\\" + DateTime.Now.ToString("yyyyMM") + "\\wyftp";

                if (!Directory.Exists(p))
                {
                    Directory.CreateDirectory(p);
                }
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo[] fi = di.GetFiles("*.*");
                DateTime dtNow = DateTime.Now;
                foreach (FileInfo tmpfi in fi)
                {
                    if (tmpfi.Name == "Ebanklist.txt")
                    {
                        tmpfi.Delete();
                        continue;
                    }
                    if (tmpfi.Name != "BankList.txt")
                    {
                        TimeSpan ts = dtNow.Subtract(tmpfi.LastWriteTime);
                        if (ts.TotalHours > totalHours)//距现在totalHours小时以上
                        {
                            string fileName = tmpfi.Name;
                            
                            if (File.Exists(p + @"\" + fileName))
                            {
                                tmpfi.MoveTo(p + @"\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileName);
                            }
                            else
                            {
                                tmpfi.MoveTo(p + @"\" + fileName);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }        
        //public static void Download()
        //{
        //    try
        //    {                
        //        FTP.FtpClient ftp = new FTP.FtpClient(ConfigApp.LhhftpIP, ConfigApp.LhhftpPath, ConfigApp.LhhftpUser, ConfigApp.LhhftpPassWord, 21, FTP.FtpClient.FtpMode.Passive);
        //        ftp.Connect();
        //        ftp.TransferType = FTP.FtpClient.FtpTransferType.Binary;
        //        ftp.Get(ConfigApp.LhhfileName, ConfigApp.FtpPath, ConfigApp.LhhfileName);
        //        LogWrite.WriteLog("", "下载联行号更新文件");
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

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
        
        public static string[] SplitAndTrim(string input, char[] separator)
        {
            string[] tmp = input.Split(separator);
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = tmp[i].Trim();
            }
            return tmp;
        }
        /// <summary>
        /// 获取当前程序集的版本号
        /// </summary>
        /// <returns></returns>
        public static string AssemblyFileVersion()
        {
            Version ApplicationVersion = new Version(Application.ProductVersion);
            
            return ApplicationVersion.ToString();
        }
        //public static void ReFileName(string oldfilepath, string newfile)
        //{
        //    try
        //    {
        //        string file = ConfigApp.FtpPath + "\\" + newfile;
        //        Computer mp = new Computer();
        //        if (System.IO.File.Exists(file))
        //        {
        //            mp.FileSystem.DeleteFile(file);
        //        }
        //        mp.FileSystem.RenameFile(oldfilepath, newfile);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// 判断指定年份是否为闰年
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>返回布尔值true为闰年，反之不是</returns>
        public static bool isLeapYear(int year)
        {
            return ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0);
        }
        public static bool isAllowIP(string ipport)
        {
            try
            {
                string ip = ipport.Split(':')[0];
                foreach (string s in ConfigApp.ClientIP)
                {
                    if (s == "*.*.*.*")
                    {
                        return true;
                    }
                    if (s == ip)
                    {
                        return true;
                    }
                }
                
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 检查文件是否被占用
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>true表示正在使用,false没有使用</returns>
        public static bool IsFileInUse(string fileName)
        {
            bool inUse = true;

            FileStream fs = null;
            try
            {

                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,

                FileShare.None);

                inUse = false;
            }
            catch
            {

            }
            finally
            {
                if (fs != null)

                    fs.Close();
            }
            return inUse;
        }

        private static object _syncRoot1;
        private static object SyncRoot1
        {
            get
            {
                if (_syncRoot1 == null)
                {
                    Interlocked.CompareExchange(ref  _syncRoot1, new object(), null);
                }
                return _syncRoot1;
            }
        }

        public static string GbkToUtf8(string s)
        {
            byte[] buffer = Encoding.GetEncoding("GBK").GetBytes(s);
            return Encoding.UTF8.GetString(buffer);
        }


        public static bool isIP(string addr)
        {
            try
            {
                IPAddress.Parse(addr);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
