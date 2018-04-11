using System;
using System.Collections.Generic;

using System.Text;

namespace CSH
{
    public class esscFldTagDef
    {
        public int conEsscFldKeyName = 1;
        public int conEsscFldMacData = 21;
        public int conEsscFldMac = 22;
        public int conEsscFldDirectHsmCmdReq = 100;
        public int conEsscFldDirectHsmCmdRes = 101;
        public int conEsscFldKeyValue = 61;
        public int conEsscFldKeyCheckValue = 51;
        public int conEsscFldZMKName = 11;
        public int conEsscFldKeyLenFlag = 203;
        public int conEsscFldFirstWKName()
        {
            return this.conEsscFldKeyName;
        }
        public int conEsscFldSecondWKName()
        {
            return this.conEsscFldKeyName + 1;
        }
        public int conEsscFldEncryptedPinByZPK = 33;
        public int conEsscFldEncryptedPinByZPK1()
        {
            return this.conEsscFldEncryptedPinByZPK;
        }
        public int conEsscFldEncryptedPinByZPK2()
        {
            return this.conEsscFldEncryptedPinByZPK + 1;
        }
        public int conEsscFldAccNo = 41;
        public int conEsscFldAccNo1()
        {
            return this.conEsscFldAccNo;
        }
        public int conEsscFldAccNo2()
        {
            return this.conEsscFldAccNo + 1;
        }
        public int conEsscFldCardPeriod = 72;
        public int conEsscFldServiceID = 73;
        public int conEsscFldVisaCVV = 71;

        public int conEsscFldPlainPin = 31;
        public int conEsscFldVisaPVV = 36;
        public int conEsscFldEncryptedPinByLMK0203 = 35;
        public int conEsscFldForPinLength = 205;
        public int conEsscFldIBMPinOffset = 37;
        public int conEsscFldIDOfApp = 207;
        public int conEsscFldSignData = 91;
        public int conEsscFldSignDataPadFlag = 93;
        public int conEsscFldSign = 92;
        public int conEsscFldAlgorithmMode = 214;
        public int conEsscFldPlainData = 81;
        public int conEsscFldIV = 213;
        public int conEsscFldCiperData = 82;
        public int conEsscFldHashData = 83;
        public int conEsscFldHashDegist = 84;

    }
}
