using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;
using System.Web.Script.Serialization;

namespace iCatalogSite.Controllers
{
    public class CountriesController : Controller
    {
        //
        // GET: /Countries/

        public ActionResult EditCountry(int id)
        {
            ViewData["Title"] = "New Country";
            if (id > 0)
            {
                ViewData["Title"] = "Edit Country";
            }
            return View();
        }

        [HttpPost]
        public ActionResult SaveCountry(CountryModel model)
        {
            string message = string.Empty;
            try
            {
                model.SaveCountry();

                message = "The Country Was Saved!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        public ActionResult CountriesPage()
        {
            GetAllCountries();
            if (Request.IsAjaxRequest())
            {
                List<CountryModel> lst = (List<CountryModel>)ViewData["CountriesList"];
                var jsonCountries = new JavaScriptSerializer().Serialize(lst);
                return Json(jsonCountries, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteCountry(CountryModel model)
        {
            string message = string.Empty;
            try
            {
                model.DeleteCountry();

                message = "The Country Was Deleted!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        private void GetAllCountries()
        {
            List<CountryModel> lst = new List<CountryModel>();
            CountryModel cm = new CountryModel();
            lst = cm.GetAllCountries();
            //if (lst.Count <= 0)
            //{
            //    lst.Add(new iCatalogData.Country() { IdCountry = 0, CountryName = "" });
            //}
            ViewData["CountriesList"] = lst;
        }
    }
}
