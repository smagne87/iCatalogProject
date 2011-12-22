using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iCatalogSite.Models
{
    public class UserAccountModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool isGeneralAdmin { get; set; }

        internal bool validateUser()
        {
            this.isGeneralAdmin = true;
            return true;
        }
    }
}