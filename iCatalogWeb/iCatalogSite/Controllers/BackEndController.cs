using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;

namespace iCatalogSite.Controllers
{
    public class BackEndController : Controller
    {
        //
        // GET: /BackEnd/

        public ActionResult IndexBackEnd()
        {
            UserAccountModel model = (UserAccountModel)TempData["UserModel"];
            return View(model);
        }
    }
}
