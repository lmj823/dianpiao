using System;
using System.Collections.Generic;

using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace CSH
{
   public class SocketIO
    {
            private TcpClient h;
            private NetworkStream stream;
/*  14 */   public bool ok = false;
/*  15 */   public string conerrmsg = null;
/*     */ 
/*     */   public bool connectHSM(String ip, int port)
/*     */   {
/*     */     try
/*     */     {
    /*  31 */   this.h = new TcpClient();
                IPAddress IP = IPAddress.Parse(ip);                
                this.h.SendTimeout = 3000;
                this.h.Connect(new IPEndPoint(IP, port));
                this.stream = h.GetStream();
                this.stream.WriteTimeout = 3000;
                this.stream.ReadTimeout = 3000;

//                this.h.setSoLinger(true, 0);


///*  36 */       this.iss = new BufferedReader(new InputStreamReader(this.h.getInputStream(), "ISO-8859-1"));
///*  37 */       this.os = new PrintWriter(new OutputStreamWriter(this.h.getOutputStream(), "ISO-8859-1"));
/*  38 */       this.ok = true;
/*     */     }
/*     */     catch (SocketException e)
/*     */     {
/*  42 */       this.ok = false;
/*  43 */       this.conerrmsg = ("Possible Reason：" + e.Message);
/*  46 */       throw e;
/*     */     }
/*     */     finally
/*     */     {
/*  51 */       if (!this.ok)
/*     */       {
/*  53 */         allClose();
/*     */       }
/*     */     }
/*     */ 
/*  60 */     return true;
/*     */   }
/*     */
            public string HSMCmd(string inn)
             {
                try
                 {
                    string outstr = null;
                    SendToHSM(inn);
                    try
                      {
                          outstr = Encoding.GetEncoding("ISO-8859-1").GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(ReceFromHSM()));
                       }
                    catch (SocketException e)
                     {
                          throw e;
                      }
                    catch (Exception e)
                     {
                           throw new Exception("通讯失败!");
                     }
                   return outstr;
                  }
                   catch (Exception ex)
                     {
                         throw new Exception(ex.Message);
                      }
                  }
              
              private void SendToHSM(string str)
/*     */     {
                 try
                 {
                     this.stream.Write(Encoding.Default.GetBytes(str), 0, Encoding.Default.GetBytes(str).Length);
                     this.stream.Flush();
/*  88 */          Thread.Sleep(100);
                 }
               catch (Exception ex)
                     {
                         throw new Exception(ex.Message);
                      }
/*     */   }
/*     */ 
/*     */   private string ReceFromHSM()
/*     */   {
/*  93 */     string outt = "";
/*  94 */     string tmpstr = "";
/*  95 */     int i = 0;
/*     */     try
/*     */     {
/*  99 */       char[] out1 = new char[2048];
                byte[] out2 =new byte[2048];
                /* 100 */
                Console.WriteLine("####################");
/* 101 */       i = this.stream.Read(out2,0,out2.Length);
/*     */       out1=Encoding.GetEncoding("ISO-8859-1").GetChars(out2);
/* 103 */       Console.WriteLine("接收报文长度->>>:" + i);
/* 104 */       for (int ii = 0; ii < i; ii++)
/*     */       {
/* 106 */         tmpstr = Convert.ToString(out1[ii],16);
/* 107 */         if (tmpstr.Length == 1) {
/* 108 */           tmpstr = "0" + tmpstr;
/*     */         }
/* 110 */         outt = outt + tmpstr;
/*     */       }
/*     */ 
/*     */     }
/*     */     catch (Exception e)
/*     */     {
/* 116 */       throw new Exception("接收数据失败!");
/*     */     }
/*     */ 
/* 119 */     return  Encoding.GetEncoding("ISO-8859-1").GetString(DecodeHex(outt));
/*     */   }
/*     */ 
/*     */   public void allClose()
/*     */   {
/*     */     try
/*     */     {
                this.stream.Close();
/* 129 */       this.h.Close();
/*     */     }
/*     */     catch (Exception localException)
/*     */     {
/*     */     }
            }

public byte[] DecodeHex(string strHexString)
{
    int len = strHexString.Length / 2;
    byte[] bytes = new byte[len];

    for (int i = 0; i < len; i++)
    {
        bytes[i] = Convert.ToByte(strHexString.Substring(i * 2, 2), 16);
    }
    return bytes;
}

       }
}