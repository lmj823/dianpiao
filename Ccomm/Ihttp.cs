using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

class Ihttp
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

    public static bool Send(string url, string s,string trantype,ref string recv)
    {
        try
        {
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(s);

            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            if (httpRequest == null)
            {
                ebankGateway.LogWrite.WriteLogUTF8(url, "网络连接失败(" + trantype + ")：", "ERROR");
                
                return false;
            }

            httpRequest.ContentType = "application/soap+xml; charset=utf-8";
            httpRequest.Method = "POST";

            httpRequest.ContentLength = data.Length;
            Stream requestStream = httpRequest.GetRequestStream();
            
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            Stream responseStream;
            try
            {
                responseStream = httpRequest.GetResponse().GetResponseStream();
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                string strHtml = sr.ReadToEnd();
                ebankGateway.LogWrite.WriteLogUTF8("接收电票返回结果失败，返回html(" + trantype + ")：" + strHtml, "【ERROR】(" + trantype + ")", "ERROR");
                return false;
            }

            try
            {
                StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));

                recv = responseReader.ReadToEnd();

                responseReader.Close();

                responseStream.Close();

                return true;
            }
            catch (Exception e)
            {
                ebankGateway.LogWrite.WriteLogUTF8(e.Message, "获取返回报文出错(" + trantype + ")：", "ERROR");
                return false;
            }
        }
        catch(Exception e)
        {
            ebankGateway.LogWrite.WriteLogUTF8(e.Message, "通讯时出现异常(" + trantype + ")：", "ERROR");
            return false;
        }
    }
    public static void Listen()
    {
        HttpListener httpListener = new HttpListener();
        httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
        httpListener.Prefixes.Add(System.Configuration.ConfigurationManager.AppSettings["URLSJ"].ToString().Trim());
        httpListener.Start();
        new Thread(new ThreadStart(delegate
        {
            while (true)
            {
                HttpListenerContext hlc = httpListener.GetContext();
                using (StreamReader reader = new StreamReader(hlc.Request.InputStream, Encoding.GetEncoding("GBK")))
                {
                    try
                    {
                        string recv = reader.ReadToEnd();

                        hlc.Response.StatusCode = 200;
                        using (StreamWriter writer = new StreamWriter(hlc.Response.OutputStream, Encoding.GetEncoding("GBK")))
                        {
                            writer.Write("OK " + recv);
                        }

                    }
                    catch (Exception ex)
                    {
                        hlc.Response.StatusCode = 200;
                        using (StreamWriter writer = new StreamWriter(hlc.Response.OutputStream))
                        {
                            string rpt = "error";
                            writer.Write(rpt);
                        }
                    }
                }
            }
        })).Start();
    }  
}

