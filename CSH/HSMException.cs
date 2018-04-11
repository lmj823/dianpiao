using System;
using System.Collections.Generic;

using System.Text;

namespace CSH
{
    public class HSMException :Exception
    {
        private const long serialVersionUID = 1L;
/* 11 */   private string errCode = null;

/*    */   HSMException()
/*    */   {
/*    */   }
/*    */ 
/*    */   HSMException(string errCode):base(errCode)
/*    */   {
/* 24 */     this.errCode = errCode;
/*    */   }
/*    */ 
/*    */   public string getErrorCode()
/*    */   {
/* 31 */     return this.errCode;
/*    */   }

    }
}
