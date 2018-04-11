using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Xml;

namespace ebankGateway
{
    class tcp
    {
        private static int timeOut = 60000;
        private static int sendSize = 1024;
       
        public static bool SLSendData(string sendtxt, string nodeNo,string trantype,ref XmlDocument rec)
        {
            TcpClient sendTcp = new TcpClient();
            byte[] mss = Encoding.GetEncoding("utf-8").GetBytes(sendtxt);
            int hostTimeOut = timeOut;
            int apphostport = int.Parse(MultPara.getHostPort(nodeNo));
            
            string ip = MultPara.getHostIP(nodeNo);


            sendTcp.SendTimeout = timeOut;

            try
            {
                if (Common.isIP(ip))
                {
                    sendTcp.Connect(IPAddress.Parse(ip), apphostport);
                }
                else
                {
                    sendTcp.Connect(ip, apphostport);
                }
            }
            catch (Exception exc)
            {
                sendTcp.Close();
                LogWrite.WriteLogUTF8(exc.Message, "连接服务器失败,原因(" + trantype + "):", nodeNo);
                return false;
            }
            NetworkStream ns = sendTcp.GetStream();

            ns.WriteTimeout = timeOut;
            ns.ReadTimeout = timeOut;
            try
            {
                LogWrite.WriteLogUTF8(sendtxt, "发送核心报文(" + trantype + ")：", nodeNo);
                if (ns.CanWrite)
                {
                    int len1 = mss.Length;
                    if (len1 > sendSize)
                    {
                        int a = len1 / sendSize;
                        int b = len1 % sendSize;
                        for (int i = 0; i < a; i++)
                        {
                            ns.Write(mss, i * sendSize, sendSize);
                        }
                        if (b > 0)
                        {
                            ns.Write(mss, len1 - b, b);
                        }
                    }
                    else
                    {
                        ns.Write(mss, 0, len1);
                    }

                    if (ns.CanRead)
                    {
                        byte[] len = new byte[8];

                        ns.Read(len, 0, 8);
                        int L = int.Parse(Encoding.GetEncoding("utf-8").GetString(len));

                        byte[] recbuff = new byte[L];

                        int index = 0;
                        while (index < L)
                        {
                            int ct = ns.Read(recbuff, index, L - index);
                            if (ct <= 0)
                            {
                                break;
                            }
                            index += ct;
                        }
                        ns.Close();
                        sendTcp.Close();
                        string recv = Encoding.GetEncoding("utf-8").GetString(recbuff);
                        LogWrite.WriteLogUTF8(recv, "接收核心报文(" + trantype + ")：", nodeNo);
                        try
                        {
                            XmlDocument doc=new XmlDocument();
                            doc.LoadXml(recv);
                            rec = doc;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            LogWrite.WriteLogUTF8(ex.Message, "解析报文出错（" + trantype + "）：", nodeNo);
                            return false;
                        }

                    }
                    else
                    {
                        LogWrite.WriteLogUTF8("连接不可用", "接收报文错误，原因(" + trantype + ")：", nodeNo);
                        ns.Close();
                        sendTcp.Close();

                        return false;
                    }

                }
                else
                {
                    LogWrite.WriteLogUTF8("连接不可用", "往服务器发数据失败，原因(" + trantype + ")：", nodeNo);
                    ns.Close();
                    sendTcp.Close();

                    return false;
                }
            }
            catch (Exception ex)
            {
                ns.Close();
                sendTcp.Close();
                LogWrite.WriteLogUTF8(ex.Message, "通讯时出现异常(" + trantype + ")：", nodeNo);
            }
            return false;
        }
        
    

    }
}
