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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(UserAccountModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (model.validateUserPassword())
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else if (model.isGeneralAdmin)
                    {
                        TempData.Add("UserModel", model);
                        return RedirectToAction("IndexBackEnd", "BackEnd");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return Json(new { Message = "The user name or password is incorrect." });
                }
            }
            return View(model);
        }
    }
}
