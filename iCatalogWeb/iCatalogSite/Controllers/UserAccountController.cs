using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;

namespace iCatalogSite.Controllers
{
    public class UserAccountController : Controller
    {
        //
        // GET: /UserAccount/

        public ActionResult UserHome()
        {
            UserAccountModel model = (UserAccountModel)TempData["UserModel"];
            return View(model);
        }

        public ActionResult RegisterUser(UserAccountModel model)
        {
            string message = "User Register Successfully";
            try
            {
                model.register();
                Uri ur = null;
                TempData.Add("UserModel", model);
                Uri.TryCreate(Request.Url, "/UserAccount/UserHome", out ur);
                return Json(new { Url = ur.AbsolutePath });
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        public ActionResult UserAvailavility(UserAccountModel model)
        {
            string message = "OK";
            try
            {
                if (model.existsUserName())
                {
                    message = "Not OK";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        public ActionResult UserEmailAvailavility(UserAccountModel model)
        {
            string message = "OK";
            try
            {
                if (model.existsEmail())
                {
                    message = "Not OK";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }
    }
}
