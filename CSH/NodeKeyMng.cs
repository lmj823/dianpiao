using System;
using System.Collections.Generic;

using System.Text;

namespace CSH
{
   public class NodeKeyMng
    {
        string gunionIDOfEsscAPI;
        public NodeKeyMng(string gunionIDOfEsscAPI)
        {
            this.gunionIDOfEsscAPI = gunionIDOfEsscAPI;
        }

        public String UnionGetNodeKeyName(String node, String type)
        {
            try
            {
                string[] desKeyType = { "zpk", "zak", "zmk", "tmk", "tpk", "tak", "pvk", "cvk", "zek", "wwk", "bdk", "edk", "" };
                if ((node.Length == 0) || (type.Length == 0) || (type.Length > 3))
                    throw new Exception("in NodeKeyMng:: UnionGetNodeKeyName parameter error!");
                String typeLower = type.ToLower();

                for (int i = 0; i < desKeyType.Length; i++)
                {
                    if (desKeyType[i].Equals(typeLower))
                    {
                        return node + "." + typeLower;
                    }
                }
                throw new Exception("in UnionGetNodeKeyName:: can find type in desKeyType!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       }
}
