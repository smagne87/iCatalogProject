using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iCatalogSite
{
    public class SecurityHelper
    {

        public static bool CustomAuthorizeCore(AuthorizationContext context = null)
        {
            return SessionHelper.IsSignedIn;
        }
   }
}