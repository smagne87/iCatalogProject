using System;
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

    }
}
