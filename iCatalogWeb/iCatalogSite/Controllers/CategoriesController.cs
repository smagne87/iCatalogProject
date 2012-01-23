using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;
using iCatalogBB;

namespace iCatalogSite.Controllers
{
    public class CategoriesController : Controller
    {
        //
        // GET: /Categories/

        public ActionResult CategoriesOne()
        {
            getCategories();
            return View();
        }

        private void getCategories()
        {
            if (Session["UserModel"] != null)
            {
                List<CategoryOne> lst = new List<CategoryOne>();
                CompanyAccountModel ca = (CompanyAccountModel)Session["UserModel"];
                CategoryOneModel com = new CategoryOneModel();
                com.IdCompany = ca.IdCompany;
                lst.Add(new CategoryOne { IdCategoryOne = 0, CategoryOneName = "", CategoryOneDescription = "" });
                lst.AddRange(com.getAllCategoryOneByIdCompany());
                ViewData["CategoriesList"] = lst;
            }
        }
    }
}
