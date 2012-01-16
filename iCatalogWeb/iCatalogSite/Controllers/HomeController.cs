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

        [HttpPost]
        public ActionResult LogOn(UserAccountModel model, string returnUrl)
        {
            if (model.validateUserPassword())
            {
                Uri ur = null;
                TempData.Add("UserModel", model);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Uri.TryCreate(Request.Url, returnUrl, out ur);
                    return Json(new { Url = ur.AbsolutePath });
                }
                else if (model.isGeneralAdmin)
                {
                    Uri.TryCreate(Request.Url, "/BackEnd/IndexBackEnd", out ur);
                    return Json(new { Url = ur.AbsolutePath });
                }
                else
                {
                    Uri.TryCreate(Request.Url, "/UserAccount/UserHome", out ur);
                    return Json(new { Url = ur.AbsolutePath });
                }
            }
            else
            {
                return Json(new { Message = "The user name or password is incorrect." });
            }
        }
    }
}
