using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Net.Sockets;

namespace CSH
{
    public class CommWithEsscSvr
    {
        public string hsmip;
        public int hsmport;
        public string returnPackage;
        public string gunionIDOfEsscAPI;
        public int timeOut;
        public CommWithEsscSvr(string ip, int port, int timeOut, string gunionIDOfEsscAPI)
        {
            this.hsmip = ip;
            this.hsmport = port;
            this.returnPackage = "";
            this.timeOut = timeOut;
            this.gunionIDOfEsscAPI = gunionIDOfEsscAPI;
        }

        public int UnionBitCommWithEsscSvr(string serviceCode, byte[] reqStr, int lenOfReqStr)
        {
            ///*  82 */     ByteArrayOutputStream bout = new ByteArrayOutputStream();

            int len = lenOfReqStr + 6;
            byte[] head = new byte[2];
            int h1 = len / 256;
            int h2 = len % 256;
            head[0] = (byte)h1;
            head[1] = (byte)h2;
            MemoryStream ms = new MemoryStream();
            ms.Write(head, 0, head.Length);
            ms.Write(Encoding.Default.GetBytes(this.gunionIDOfEsscAPI), 0, Encoding.Default.GetBytes(this.gunionIDOfEsscAPI).Length);
            ms.Write(Encoding.Default.GetBytes(serviceCode), 0, Encoding.Default.GetBytes(serviceCode).Length);
            ms.Write(Encoding.Default.GetBytes("1"), 0, Encoding.Default.GetBytes("1").Length);
            ms.Write(reqStr, 0, reqStr.Length);
            ///*  94 */     bout.write(head);
            ///*  95 */     bout.write(Encoding.Default.GetBytes(this.gunionIDOfEsscAPI));
            ///*  96 */     bout.write(Encoding.Default.GetBytes(serviceCode));
            ///*  97 */     bout.write(Encoding.Default.GetBytes("1"));
            ///*  98 */     bout.write(reqStr);
            /*     */
            /* 101 */
            SocketIO hsm = new SocketIO();
            string outStr = "";
            try
            {
                Console.WriteLine("正在连接....");
                hsm.connectHSM(this.hsmip, this.hsmport);
                Console.WriteLine("连接成功....");
                outStr = hsm.HSMCmd(Encoding.GetEncoding("ISO-8859-1").GetString(ms.ToArray()));
                hsm.allClose();
            }
            catch (SocketException e)
            {
                throw e;
            }
            catch (HSMException e)
            {
                throw e;
            }
              catch (Exception localException)
                 {
            }
            if (outStr.Length == 0)
                throw new Exception("in UnionCommWithHsmSvr:: the result of hsmCmd is empty!");
            int outStrLen = char.Parse(outStr.Substring(0, 1)) * 'Ā' + char.Parse(outStr.Substring(1, 1));
            if (outStr.Length - 2 != outStrLen)
                throw new Exception("in UnionCommWithHsmSvr:: the length of outStr does not right!");
            if ((char.Parse(outStr.Substring(7, 1)) != '0') || (outStr.Length < 14))
                throw new Exception("in UnionCommWithHsmSvr:: outStr does not tally with request of its style!");
            if ((!outStr.Substring(2, 2).Equals(this.gunionIDOfEsscAPI)) || (!outStr.Substring(4, 3).Equals(serviceCode)))
                throw new Exception("in UnionCommWithHsmSvr:: outStr does not tally with request!");
            if (int.Parse(outStr.Substring(8, 6)) < 0)
                throw new Exception("in UnionCommWithHsmSvr:: the error num of outStr does not right!");
            this.returnPackage = outStr.Substring(14);
            return outStrLen - 14;
        }


    }
}
