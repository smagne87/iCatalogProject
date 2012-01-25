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

        #region CategoryOne
        public ActionResult CategoriesOne()
        {
            getCategoriesOne();
            if (Request.IsAjaxRequest())
            {
                return PartialView("CategoriesOneList", (List<CategoryOne>)ViewData["CategoriesList"]);
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
                return PartialView("CategoryOneList", (List<CategoryOne>)ViewData["CategoryList"]);
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

        #endregion

        #region CategoryTwo
        public ActionResult CategoriesTwo()
        {
            getCategoriesTwo();
            if (Request.IsAjaxRequest())
            {
                return PartialView("CategoriesTwoList", (List<CategoryTwo>)ViewData["CategoriesList"]);
            }
            else
            {
                return View();
            }
        }

        public ActionResult CategoryTwo(int IdCompany, string CategoryTwoName)
        {
            List<CategoryTwo> lst = new List<CategoryTwo>();
            CategoryTwoModel com = new CategoryTwoModel();
            com.IdCompany = IdCompany;
            com.CategoryTwoName = CategoryTwoName;
            lst.Add(new CategoryTwo { IdCategoryTwo = 0, CategoryTwoName = "", CategoryTwoDescription = "" });
            lst.AddRange(com.getAllCategoryTwoByCategoryName());
            ViewData["CategoryList"] = lst;
            ViewData["IdCompany"] = IdCompany;
            ViewData["CategoryTwoName"] = CategoryTwoName;
            if (Request.IsAjaxRequest())
            {
                return PartialView("CategoryTwoList", (List<CategoryTwo>)ViewData["CategoryList"]);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CategoryTwoSave(CategoryTwoModel model)
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
        public ActionResult DeleteSingleCategoryTwo(CategoryTwoModel model)
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
        public ActionResult DeleteAllCategoryTwo(CategoryTwoModel model)
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

        private void getCategoriesTwo()
        {
            if (Session["UserModel"] != null)
            {
                List<CategoryTwo> lst = new List<CategoryTwo>();
                CategoryTwoModel com = new CategoryTwoModel();
                CompanyAccountModel ca = (CompanyAccountModel)Session["UserModel"];
                com.IdCompany = ca.IdCompany;
                lst.Add(new CategoryTwo { IdCategoryTwo = 0, CategoryTwoName = "", CategoryTwoDescription = "" });
                lst.AddRange(com.getAllCategoryTwoByIdCompany());
                ViewData["CategoriesList"] = lst;
                ViewData["IdCompany"] = ca.IdCompany;
            }
        }

        #endregion

        #region CategoryThree
        public ActionResult CategoriesThree()
        {
            getCategoriesThree();
            if (Request.IsAjaxRequest())
            {
                return PartialView("CategoriesThreeList", (List<CategoryThree>)ViewData["CategoriesList"]);
            }
            else
            {
                return View();
            }
        }

        public ActionResult CategoryThree(int IdCompany, string CategoryThreeName)
        {
            List<CategoryThree> lst = new List<CategoryThree>();
            CategoryThreeModel com = new CategoryThreeModel();
            com.IdCompany = IdCompany;
            com.CategoryThreeName = CategoryThreeName;
            lst.Add(new CategoryThree { IdCategoryThree = 0, CategoryThreeName = "", CategoryThreeDescription = "" });
            lst.AddRange(com.getAllCategoryThreeByCategoryName());
            ViewData["CategoryList"] = lst;
            ViewData["IdCompany"] = IdCompany;
            ViewData["CategoryThreeName"] = CategoryThreeName;
            if (Request.IsAjaxRequest())
            {
                return PartialView("CategoryThreeList", (List<CategoryThree>)ViewData["CategoryList"]);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CategoryThreeSave(CategoryThreeModel model)
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
        public ActionResult DeleteSingleCategoryThree(CategoryThreeModel model)
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
        public ActionResult DeleteAllCategoryThree(CategoryThreeModel model)
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

        private void getCategoriesThree()
        {
            if (Session["UserModel"] != null)
            {
                List<CategoryThree> lst = new List<CategoryThree>();
                CategoryThreeModel com = new CategoryThreeModel();
                CompanyAccountModel ca = (CompanyAccountModel)Session["UserModel"];
                com.IdCompany = ca.IdCompany;
                lst.Add(new CategoryThree { IdCategoryThree = 0, CategoryThreeName = "", CategoryThreeDescription = "" });
                lst.AddRange(com.getAllCategoryThreeByIdCompany());
                ViewData["CategoriesList"] = lst;
                ViewData["IdCompany"] = ca.IdCompany;
            }
        }

        #endregion
    }
}