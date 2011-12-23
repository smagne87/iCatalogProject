using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;

namespace iCatalogSite.Controllers
{
    public class BackEndController : Controller
    {
        //
        // GET: /BackEnd/

        public ActionResult IndexBackEnd()
        {
            UserAccountModel model = (UserAccountModel)TempData["UserModel"];
            ViewData["UserModel"] = model;
            return View(model);
        }

        public ActionResult CountriesPage()
        {
            GetAllCountries();
            return View();
        }

        public ActionResult EditCountry(int id)
        {
            ViewData["Title"] = "New Country";
            if (id > 0)
            {
                ViewData["Title"] = "Edit Country";
            }
            return View();
        }

        private void GetAllCountries()
        {
            List<CountryModel> lst = new List<CountryModel>();
            lst.Add(new CountryModel { IdCountry = 1, CountryName = "Argentina" });
            lst.Add(new CountryModel { IdCountry = 2, CountryName = "Brasil" });
            lst.Add(new CountryModel { IdCountry = 3, CountryName = "Paraguay" });
            ViewData["CountriesList"] = lst;
        }
    }
}
