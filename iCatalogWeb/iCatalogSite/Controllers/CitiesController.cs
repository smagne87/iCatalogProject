using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogBB;
using iCatalogSite.Models;
namespace iCatalogSite.Controllers
{
    public class CitiesController : Controller
    {
        //
        // GET: /Cities/

        public JsonResult GetAllCitiesByCountryId(CityModel cm)
        {
            List<City> lst = new List<City>();
            lst.AddRange(cm.GetAllCitiesByIdCountry());
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CitiesPage()
        {
            GetAllCountries();
            GetAllCities();
            if (Request.IsAjaxRequest())
            {
                return PartialView("CitiesList", (List<City>)ViewData["CitiesList"]);
            }
            else
            {
                return View();
            }
        }

        private void GetAllCities()
        {
            List<City> lst = new List<City>();
            CityModel cm = new CityModel();
            lst.Add(new City { IdCity = 0, CityName = "", IdCountry = 0, CountryName = "" });
            lst.AddRange(cm.GetAllCities());
            ViewData["CitiesList"] = lst;
        }

        private void GetAllCountries()
        {
            List<Country> lst = new List<Country>();
            CountryModel cm = new CountryModel();
            lst.Add(new Country { IdCountry = 0, CountryName = "" });//This row will be deleted after the datatable is created.
            lst.AddRange(cm.GetAllCountries());
            ViewData["CountriesList"] = new SelectList(lst, "IdCountry", "CountryName");
        }

        [HttpPost]
        public ActionResult SaveCity(CityModel model)
        {
            string message = string.Empty;
            try
            {
                model.Save();

                message = "The City Was Saved!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        [HttpPost]
        public ActionResult DeleteCity(CityModel model)
        {
            string message = string.Empty;
            try
            {
                model.Delete();

                message = "The City Was Deleted!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }
    }
}
