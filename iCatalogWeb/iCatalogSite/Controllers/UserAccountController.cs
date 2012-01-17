using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;
using iCatalogBB;

namespace iCatalogSite.Controllers
{
    public class UserAccountController : Controller
    {
        //
        // GET: /UserAccount/

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult SaveUserData(UserAccountModel model)
        {
            string message = "Personal data update successfully.";
            try
            {
                model.saveData();
                UserAccountModel oldmodel = null;
                if (TempData["UserModel"] != null)
                {
                    oldmodel = (UserAccountModel)TempData["UserModel"];
                }
                else if (Session["UserModel"] != null)
                {
                    oldmodel = (UserAccountModel)Session["UserModel"];
                }
                model.UserName = oldmodel.UserName;
                Session["UserModel"] = model;                
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        public ActionResult ChangePassword(UserAccountModel model)
        {
            string message = "the password has been changed successfully";
            try
            {
                if (model.validateUserPassword())
                {
                    model.SavePassword();
                }
                else
                {
                    message = "Invalid old Password.";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        public ActionResult UserHome()
        {
            UserAccountModel model = null;
            if (TempData["UserModel"] != null)
            {
                model = (UserAccountModel)TempData["UserModel"];
                Session["UserModel"] = model;
            }
            else if (Session["UserModel"] != null)
            {
                model = (UserAccountModel)Session["UserModel"];
            }
            else
            {
                return RedirectToAction("Login", "Home", new { returnUrl = Request.Url.AbsolutePath });
            }
            return View(model);
        }

        public ActionResult ProfileUser()
        {
            GetAllCountries();
            UserAccountModel model = null;
            if (TempData["UserModel"] != null)
            {
                model = (UserAccountModel)TempData["UserModel"];
                Session["UserModel"] = model;
            }
            else if (Session["UserModel"] != null)
            {
                model = (UserAccountModel)Session["UserModel"];
            }
            else
            {
                return RedirectToAction("Login", "Home", new { returnUrl = Request.Url.AbsolutePath });
            }
            return View(model);
        }

        public ActionResult MyDevices()
        {
            GetAllDevices();
            if (Request.IsAjaxRequest())
            {
                return PartialView("DevicesList", (List<iCatalogBB.Device>)ViewData["DevicesList"]);
            }
            else
            {
                return View();
            }
        }

        public ActionResult RemoveDevice()
        {
            return View();
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

        private void GetAllCountries()
        {
            List<Country> lst = new List<Country>();
            CountryModel cm = new CountryModel();
            lst.AddRange(cm.GetAllCountries());
            ViewData["CountriesList"] = new SelectList(lst, "IdCountry", "CountryName");
        }

        private void GetAllDevices()
        {
            List<Device> lst = new List<Device>();
            Device c = new Device();
            DeviceModel dm = new DeviceModel();
            lst.Add(new Device { IdDevice = 0, DeviceDescription = "" });//This row will be deleted after the datatable is created.
            lst.AddRange(dm.GetAllDevices());
            ViewData["DevicesList"] = lst;
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
