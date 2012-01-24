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
            getCategoriesOne();
            if (Request.IsAjaxRequest())
            {
                return PartialView("CategoriesList", (List<CategoryOne>)ViewData["CategoriesList"]);
            }
            else
            {
                return View();
            }
        }

        public ActionResult CategoryOne(int IdCompany, string CategoryOneName)
        {
            List<CategoryOne> lst = new List<CategoryOne>();
            CategoryOneModel com = new CategoryOneModel();
            com.IdCompany = IdCompany;
            com.CategoryOneName = CategoryOneName;
            lst.Add(new CategoryOne { IdCategoryOne = 0, CategoryOneName = "", CategoryOneDescription = "" });
            lst.AddRange(com.getAllCategoryOneByCategoryName());
            ViewData["CategoryList"] = lst;
            ViewData["IdCompany"] = IdCompany;
            ViewData["CategoryOneName"] = CategoryOneName;
            if (Request.IsAjaxRequest())
            {
                return PartialView("CategoryList", (List<CategoryOne>)ViewData["CategoryList"]);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CategoryOneSave(CategoryOneModel model)
        {
            string message = string.Empty;
            try
            {
                model.Save();

                message = "The Category Was Saved!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        [HttpPost]
        public ActionResult DeleteSingleCategoryOne(CategoryOneModel model)
        {
            string message = string.Empty;
            try
            {
                model.DeleteSingle();

                message = "The Category Was Deleted!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        [HttpPost]
        public ActionResult DeleteAllCategoryOne(CategoryOneModel model)
        {
            string message = string.Empty;
            try
            {
                model.DeleteAll();

                message = "The Category Was Deleted!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        private void getCategoriesOne()
        {
            if (Session["UserModel"] != null)
            {
                List<CategoryOne> lst = new List<CategoryOne>();
                CategoryOneModel com = new CategoryOneModel();
                CompanyAccountModel ca = (CompanyAccountModel)Session["UserModel"];
                com.IdCompany = ca.IdCompany;
                lst.Add(new CategoryOne { IdCategoryOne = 0, CategoryOneName = "", CategoryOneDescription = "" });
                lst.AddRange(com.getAllCategoryOneByIdCompany());
                ViewData["CategoriesList"] = lst;
                ViewData["IdCompany"] = ca.IdCompany;
            }
        }
    }
}
