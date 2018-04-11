using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ebankGateway
{
    class JNKY
    {
        static string filename = System.Windows.Forms.Application.StartupPath + "\\JNKY.ini";
        private static XmlHelper setCommPara(string jydm, string nodeNo)
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\files\\" + jydm + ".xml";
            XmlHelper xher = new XmlHelper(path, true);

            ini myini = new ini(filename);
            /* head */
            xher.UpdateContent(@"/union/head/transTime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            xher.UpdateContent(@"/union/head/userInfo", DateTime.Now.ToString("yyyyMMddHHmmss"));

            xher.UpdateContent(@"/union/head/sysID", myini.ReadValue(nodeNo, "sysID").ToString());
            xher.UpdateContent(@"/union/head/appID", myini.ReadValue(nodeNo, "appID").ToString());
            xher.UpdateContent(@"/union/head/clientIPAddr", myini.ReadValue(nodeNo, "clientIPAddr").ToString());
            xher.UpdateContent(@"/union/head/transFlag", myini.ReadValue(nodeNo, "transFlag").ToString());

            /* body */
            xher.UpdateContent(@"/union/body/mode", myini.ReadValue(nodeNo, "mode").ToString());
            xher.UpdateContent(@"/union/body/keyName", myini.ReadValue(nodeNo, "keyName").ToString());
            xher.UpdateContent(@"/union/body/format", myini.ReadValue(nodeNo, "format").ToString());

            return xher;
        }
        private static bool checkRecv(string recv)
        {
            if (recv.IndexOf("error") == 0)
                return false;
            XmlHelper xher1 = new XmlHelper(recv);
            if (xher1.ReadByNode(@"/union/head/responseCode") == "000000")
                return true;
            else
                return false;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="zhdh"></param>
        /// <param name="pswd"></param>
        /// <param name="nodeNo"></param>
        /// <returns></returns>
        public static string Encrypt(string zhdh, string pswd, string nodeNo)
        {
            string s = "error";
            try
            {
                XmlHelper xher = setCommPara("E140", nodeNo);
                xher.UpdateContent(@"/union/body/plainPin", pswd);
                xher.UpdateContent(@"/union/body/accNo", zhdh);


                string send = xher.GetXmlDoc().OuterXml;
                string recv = SendData(send, nodeNo);

                if (recv.IndexOf("error") != 0)
                {
                    XmlHelper xher1 = new XmlHelper(recv);
                    if (xher1.ReadByNode(@"/union/head/responseCode") == "000000")
                    {
                        s = xher1.ReadByNode(@"/union/body/pinBlock");
                    }
                }
            }
            catch (Exception e)
            {
                LogWrite.WriteLog("账号：[" + zhdh + "]，原因：[" + e.Message + "]", "调用加密机【加密】失败", nodeNo);
            }
            return s;
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="zhdh"></param>
        /// <param name="pinBlock"></param>
        /// <param name="nodeNo"></param>
        /// <returns></returns>
        public static string Decrypt(string zhdh, string pinBlock, string nodeNo)
        {
            string s = "error";
            try
            {
                XmlHelper xher = setCommPara("E141", nodeNo);
                xher.UpdateContent(@"/union/body/pinBlock", pinBlock);
                xher.UpdateContent(@"/union/body/accNo", zhdh);
                string send = xher.GetXmlDoc().OuterXml;
                string recv = SendData(send, nodeNo);

                if (recv.IndexOf("error") != 0)
                {
                    XmlHelper xher1 = new XmlHelper(recv);
                    if (xher1.ReadByNode(@"/union/head/responseCode") == "000000")
                    {
                        s = xher1.ReadByNode(@"/union/body/plainPin");
                    }
                }
            }
            catch (Exception e)
            {
                LogWrite.WriteLog("账号：[" + zhdh + "]，原因：[" + e.Message + "]", "调用加密机【解密】失败", nodeNo);
            }
            return s;
        }
        private static string SendData(string s, string nodeNo)
        {
            string recv = "error";
            byte[] body = Encoding.GetEncoding("GBK").GetBytes(s);

            byte[] head = new byte[2];
            head[0] = Convert.ToByte(body.Length >> 8);
            head[1] = Convert.ToByte(body.Length % 256);
            TcpClient sendTcp = new TcpClient();
            //string Restr = "";
            ini myini = new ini(filename);
            int apphostport = Convert.ToInt32(myini.ReadValue(nodeNo, "hostPort").ToString());
            IPAddress ip = IPAddress.Parse(myini.ReadValue(nodeNo, "hostIP").ToString());
            IPEndPoint ipe = new IPEndPoint(ip, apphostport);

            sendTcp.SendBufferSize = 10240;
            sendTcp.SendTimeout = 10 * 1000;

            try
            {
                sendTcp.Connect(ip, apphostport);
            }
            catch (Exception ex)
            {
                sendTcp.Close();
                LogWrite.WriteLog("原因：" + ex.Message, "[加密机]连接服务器失败", nodeNo);
                return recv;
            }
            NetworkStream ns = sendTcp.GetStream();

            ns.WriteTimeout = 10 * 1000;
            ns.ReadTimeout = 10 * 1000;
            try
            {
                if (ns.CanWrite)
                {
                    ns.Write(head, 0, head.Length);
                    ns.Write(body, 0, body.Length);
                    //LogWrite.WriteLog(s, "[加密机]发送数据:", nodeNo);
                    if (ns.CanRead)
                    {

                        byte[] len = new byte[2];
                        ns.Read(len, 0, 2);
                        int L = len[0] * 256 + len[1];
                        byte[] recbuff = new byte[L];

                        int index = 0;
                        while (index < L)
                        {
                            int cout = ns.Read(recbuff, index, L - index);
                            if (cout <= 0)
                            {
                                break;
                            }
                            index += cout;
                        }

                        recv = Encoding.GetEncoding("gbk").GetString(recbuff);
                        //LogWrite.WriteLog(recv, "[加密机]接收数据:", nodeNo);
                    }
                }
                else
                {
                    LogWrite.WriteLog("连接不可用", "[加密机]发数据失败:", nodeNo);
                }
            }
            catch (Exception ex)
            {
                LogWrite.WriteLog(ex.Message, "[加密机]通讯故障，原因：", nodeNo);
            }
            finally
            {
                ns.Close();
                sendTcp.Close();
            }
            return recv;
        }
    }
}
