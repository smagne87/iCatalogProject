using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;

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

        public ActionResult RefreshGridData()
        {
            GetAllCountries();
            return PartialView("CountriesPage", (List<iCatalogData.Country>)ViewData["CountriesList"]);
        }

        public ActionResult CountriesPage()
        {
            GetAllCountries();
            return View();
        }

        private void GetAllCountries()
        {
            List<iCatalogData.Country> lst = new List<iCatalogData.Country>();
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
