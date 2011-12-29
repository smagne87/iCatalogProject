﻿using System;
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
                return PartialView("CountriesList", (List<iCatalogSite.Models.CountryModel>)ViewData["CountriesList"]);
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
            lst.Add(new CountryModel { IdCountry = 0, CountryName = "" });//This row will be deleted after the datatable is created.
            lst.AddRange(cm.GetAllCountries());
            ViewData["CountriesList"] = lst;
        }
    }
}
