using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;

namespace ebankGateway
{

    public class Listen
    {
        private TcpClient tcpClient;
        private ShowData sdata = new ShowData();
        
        public Listen(TcpClient tcp)
        {
            this.tcpClient = tcp;
            tcpClient.SendTimeout = 10000;
            tcpClient.ReceiveTimeout = 10000;
            this.tcpClient.LingerState.Enabled = false;
            this.tcpClient.LingerState.LingerTime = 0;
            this.tcpClient.NoDelay = true;            

        }
        public Listen()
        {

        }       
        
        
        

        public void startAccept()
        {
            try
            {
                NetworkStream ns = tcpClient.GetStream();
                int l1 = 6;
                byte[] cRead = new byte[l1];

                int it1 = 0;
                while (it1 < l1)
                {
                    int ct1 = ns.Read(cRead, it1, l1 - it1);
                    if (ct1 <= 0)
                    {
                        break;
                    }
                    it1 += ct1;
                }

               
                string head = System.Text.Encoding.GetEncoding("GBK").GetString(cRead);
                int len = int.Parse(head);
                byte[] text = new byte[len];
                int it = 0;
                while (it < len)
                {
                    int ct = ns.Read(text, it, len - it);
                    if (ct <= 0)
                    {
                        break;
                    }
                    it += ct;
                }

                string body = Encoding.GetEncoding("GBK").GetString(text);
                sdata.Jysj = DateTime.Now.ToString("HH:mm:ss");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(body);   
                XmlNode tranCode = doc.SelectSingleNode("//ebank//tranCode");
                XmlNode nodeNo = doc.SelectSingleNode("//ebank//nodeNo");
                string trandType = "";//�ڲ����״���
                string nodeno = "";//������
                string sendEbank = "";//���ر���
                if (tranCode != null && nodeNo != null)
                {
                    string index = ConfigApp.Lsh;
                    trandType = tranCode.InnerText;
                    nodeno = nodeNo.InnerText.Replace("|", ""); 
                   
                    LogWrite.WriteLog(head + body, "����(" + trandType + ")" + "[" + index + "]", nodeno);

                   

                    ET bm = new ET(doc, trandType, nodeno);
                    sendEbank = bm.Done();

                    //Eticket bm1 = new Eticket(doc, trandType, nodeno);
                    //sendEbank = bm1.Done();

                    if (ConfigApp.Show)//��ʾ���׽�� 
                    {
                        sdata.Wd = nodeno;
                        sdata.Wdm = MultPara.getBankName(nodeno);
                        sdata.Jym = trandType;
                        sdata.Jysj1 = DateTime.Now.ToString("HH:mm:ss");
                        try
                        {
                            XmlDocument xxd = new XmlDocument();
                            xxd.LoadXml(sendEbank.Substring(6));
                            sdata.Errcode = xxd.SelectSingleNode("//ebank//hostReturnCode").InnerText.Trim();
                            sdata.Errmsg = xxd.SelectSingleNode("//ebank//hostErrorMessage").InnerText.Trim();
                        }
                        catch
                        {
                            sdata.Errcode = "";
                            sdata.Errmsg = sendEbank;
                        }
                    }

                    byte[] sendData = Encoding.GetEncoding("GBK").GetBytes(sendEbank);
                    if (ns.CanWrite)
                    {
                        ns.Write(sendData, 0, sendData.Length);
                        LogWrite.WriteLog(sendEbank, "����(" + trandType + ")" + "[" + index + "]", nodeno);

                    }
                    else
                    {
                        LogWrite.WriteLog("", "����������ʱͨѶ�쳣(" + trandType + ")" + "[" + index + "]", nodeno);
                        sdata.Errcode = "9999";
                        sdata.Errmsg = "ͨѶ�쳣";
                    }
                }
                else
                {
                    LogWrite.WriteLog(body, "�Ƿ���������:");
                    sdata.Errcode = "9999";
                    sdata.Errmsg = "��Ч������";
                }
                ns.Close();
                if (ConfigApp.Show) 
                {
                    try { Form1.FinalQueue.Enqueue(sdata); }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                LogWrite.WriteLog(ex.ToString(), "����ʧ��:");
            }
            finally
            {
                tcpClient.Close();
            }
        } 
    }
}
