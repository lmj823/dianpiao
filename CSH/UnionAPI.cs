using System;
using System.Collections.Generic;

using System.Text;

namespace CSH
{
    public class UnionAPI
    {
        public string esscIp;
        public int esscPort;
        public int timeOut;
        public string gunionIDOfEsscAPI;

        public UnionAPI(string ip, int port, int timeOut, string gunionIDOfEsscAPI)
        {
            this.esscIp = ip;
            this.esscPort = port;
            this.timeOut = timeOut;
            this.gunionIDOfEsscAPI = gunionIDOfEsscAPI;
        }

        public string printCStyle(int len, int value)
        {
            string result = value.ToString();
            while (result.Length < len)
                result = "0" + result;
            return result;
        }

        public string UnionPutFldIntoStr(int fldTag, string value, int lenOfValue)
        {
            try
            {
                if ((lenOfValue < 0) || (value.Length == 0))
                {
                    throw new Exception("in UnionPutFldIntoStr:: parameter error!\n");
                }
                if ((fldTag < 0) || (fldTag > 999) || (lenOfValue > 9999))
                    throw new Exception(
                      "in UnionPutFldIntoStr:: int parameter error!\n");
                string tmpBuf = printCStyle(3, fldTag) + printCStyle(4, lenOfValue) +
                  value;
                return tmpBuf;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string UnionReadSpecFldFromStr(string data, int len, int fldTag)
        {
            try
            {
                /*   71 */
                if ((data.Length == 0) || (len < 3))
                    /*   72 */
                    throw new Exception(
                        /*   73 */         "in UnionReadSpecFldFromStr:: parameter error!\n");
                /*   74 */
                int fldNum = getLenOfFld(data, 0, 3);
                /*   75 */
                if (fldNum <= 0)
                    /*   76 */
                    throw new Exception(
                        /*   77 */         "in UnionReadSpecFldFromStr:: lenOfFld error!\n");
                /*   78 */
                int offset = 3;
                /*   79 */
                for (int index = 0; index < fldNum; index++)
                {
                    /*   80 */
                    if (offset + 7 > len)
                    {
                        /*   81 */
                        throw new Exception("in UnionReadSpecFldFromStr:: fldTag " +
                            /*   82 */           fldTag + " not exists!\n");
                        /*      */
                    }
                    /*   84 */
                    int tmpFldTag = getLenOfFld(data, offset, 3);
                    /*   85 */
                    offset += 3;
                    /*   86 */
                    int fldLen = getLenOfFld(data, offset, 4);
                    /*   87 */
                    offset += 4;
                    /*   88 */
                    if (tmpFldTag == fldTag)
                    {
                        /*   89 */
                        if (fldLen < 0)
                            /*   90 */
                            throw new Exception("in UnionReadSpecFldFromStr:: fldLen < 0");
                        /*   92 */
                        byte[] tmpData = Encoding.Default.GetBytes(data);
                        byte[] newA = new byte[fldLen];
                        for (int i = 0; i < fldLen; i++)
                        {
                            newA[i] = tmpData[i + offset];
                        }
                        //byte[] newA = tmpData.Skip(offset).Take(fldLen).ToArray();
                        /*   93 */
                        string result = Encoding.Default.GetString(newA);
                        /*   94 */
                        return result;
                        /*      */
                    }
                    /*   96 */
                    offset += fldLen;
                    /*      */
                }
                /*   98 */
                throw new Exception("in UnionReadSpecFldFromStr:: fldTag " + fldTag +
                    /*   99 */       " does not exists!\n");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int getLenOfFld(string s, int bengin, int len)
           {
            byte[] result = Encoding.Default.GetBytes(s);

            byte[] newA = new byte[len];
            for (int i = 0; i < len; i++)
            {
                newA[i] = result[i + bengin];
            }

            //byte[] newA = result.Skip(bengin).Take(len).ToArray();
            string ks = Encoding.Default.GetString(newA);
            int ts = int.Parse(ks);
            return ts;
        }

        public string UnionGenerateMac(string fullKeyName,int lenOfMacData, byte[] macData)
        {
            try
            {
                if ((fullKeyName.Length == 0) || (macData.Length == 0) || (lenOfMacData <= 0))
                {
                    throw new Exception("in UnionGenerateMac:: parameter error!\n");
                }
                string packageBuf = printCStyle(3, 2);
                int offset = 3;
                esscFldTagDef fldTag = new esscFldTagDef();
                string packageBufFld = UnionPutFldIntoStr(fldTag.conEsscFldKeyName, fullKeyName, fullKeyName.Length);
                if (packageBufFld.Length == 0)
                {
                    throw new Exception("in UnionGenerateMac:: UnionPutFldIntoStr for conEsscFldKeyName!\n");
                }
                offset += packageBufFld.Length;
                packageBuf = packageBuf + packageBufFld;
                byte[] packageBufFldBit = UnionBitPutFldIntoStr(fldTag.conEsscFldMacData, macData,lenOfMacData);
                if (packageBufFldBit.Length == 0)
                {
                    throw new Exception("in UnionGenerateMac:: UnionBitPutFldIntoStr for conEsscFldMacData!");
                }
                offset += packageBufFldBit.Length;
                byte[] packageBufBit = bytePlusByte(Encoding.Default.GetBytes(packageBuf), packageBufFldBit);
                CommWithEsscSvr commWithEsscSvr = new CommWithEsscSvr(this.esscIp, this.esscPort,this.timeOut, this.gunionIDOfEsscAPI);
                commWithEsscSvr.returnPackage = "";
                int MacLen = commWithEsscSvr.UnionBitCommWithEsscSvr("302", packageBufBit,offset);
                string mac = "";
                mac = UnionReadSpecFldFromStr(commWithEsscSvr.returnPackage, MacLen,fldTag.conEsscFldMac);
                return mac;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public byte[] UnionBitPutFldIntoStr(int fldTag, byte[] value, int lenOfValue)
        {
            try
            {
                if ((lenOfValue < 0) || (value.Length == 0))
                {
                    throw new Exception("in UnionBitPutFldIntoStr:: parameter error!\n");
                }
                if ((fldTag < 0) || (fldTag > 999) || (lenOfValue > 9999))
                    throw new Exception(
                      "in UnionBitPutFldIntoStr:: int parameter error!\n");
                byte[] tmpBuf = bytePlusByte(Encoding.Default.GetBytes(printCStyle(3, fldTag) + printCStyle(4, lenOfValue)), value);
                return tmpBuf;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public byte[] bytePlusByte(byte[] res, byte[] req)
        {
            try
            {
                byte[] tmpres = new byte[res.Length + req.Length];
                int i = 0; int j = 0;
                for (i = 0; i < res.Length; i++)
                    tmpres[i] = res[i];
                for (j = 0; j < req.Length; j++)
                    tmpres[(i + j)] = req[j];
                return tmpres;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
