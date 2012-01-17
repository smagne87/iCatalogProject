using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;

namespace iCatalogSite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (TempData["UserModel"] != null)
            {
                UserAccountModel model = (UserAccountModel)TempData["UserModel"];
                return RedirectToAction("UserHome", "UserAccount", model);
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}
