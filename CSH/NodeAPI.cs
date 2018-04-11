using System;
using System.Collections.Generic;

using System.Text;

namespace CSH
{
    public class NodeAPI
    {
        string hsmIP;
        int hsmPort;
        int timeOut;
        string gunionIDOfUnionAPI;

        public NodeAPI(String ip, int port, int timeOut, String gunionIDOfUnionAPI)
        {
            this.hsmIP = ip;
            this.hsmPort = port;
            this.timeOut = timeOut;
            this.gunionIDOfUnionAPI = gunionIDOfUnionAPI;
        }


        public String NodeGenerateMac(String nodeName, int lenOfMacData, byte[] macData)
        {
            try
            {

                NodeKeyMng nodeKeyMng = new NodeKeyMng(this.gunionIDOfUnionAPI);
                string fullKeyName = nodeKeyMng.UnionGetNodeKeyName(nodeName, "zak");
                UnionAPI unionAPI = new UnionAPI(this.hsmIP, this.hsmPort, this.timeOut, this.gunionIDOfUnionAPI);
                return unionAPI.UnionGenerateMac(fullKeyName, lenOfMacData, macData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
