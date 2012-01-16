﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;
using iCatalogBB;

namespace iCatalogSite.Controllers
{
    public class CompanyAccountController : Controller
    {
        //
        // GET: /CompanyAccount/

        public ActionResult CompanyHome()
        {
            CompanyAccountModel model = null;
            if (TempData["UserModel"] != null)
            {
                model = (CompanyAccountModel)TempData["UserModel"];
                Session["UserModel"] = model;
            }
            else if (Session["UserModel"] != null)
            {
                model = (CompanyAccountModel)Session["UserModel"];
            }
            else
            {
                return RedirectToAction("Login", "Home", new { returnUrl = Request.Url.AbsolutePath });
            }
            return View(model);
        }

        private void GetAllCountries()
        {
            List<Country> lst = new List<Country>();
            CountryModel cm = new CountryModel();
            lst.AddRange(cm.GetAllCountries());
            ViewData["CountriesList"] = new SelectList(lst, "IdCountry", "CountryName");
        }

        public ActionResult RegisterCom()
        {
            GetAllCountries();
            return View();
        }


        public ActionResult RegisterCompany(CompanyAccountModel model)
        {
            string message = "User Register Successfully";
            try
            {
                model.register();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserAvailavility(CompanyAccountModel model)
        {
            string message = "OK";
            try
            {
                if (model.existsCompanyUserName())
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

        public ActionResult UserEmailAvailavility(CompanyAccountModel model)
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

        [HttpPost]
        public ActionResult LogOn(CompanyAccountModel model, string returnUrl)
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
                else
                {
                    Uri.TryCreate(Request.Url, "/CompanyAccount/CompanyHome", out ur);
                    return Json(new { Url = ur.AbsolutePath });
                }
            }
            else
            {
                return Json(new { Message = "The company user name or password is incorrect." });
            }
        }

    }
}
