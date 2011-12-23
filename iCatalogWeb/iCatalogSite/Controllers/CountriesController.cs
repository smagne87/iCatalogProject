﻿using System;
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
            string message = "volvio";

            return Json(new { Message = message });
        }

        public ActionResult CountriesPage()
        {
            GetAllCountries();
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