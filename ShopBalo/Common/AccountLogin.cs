using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBalo.Common
{
    [Serializable]
    public class AccountLogin
    {
        public long AccountID { set; get; }
        public string Email { set; get; }
        public int GroupID { set; get; }
    }
}