using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;
using Product = iCatalogBB.Product;
using iCatalogBB;

namespace iCatalogSite.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/

        [HttpPost]
        public ActionResult SaveProduct(ProductModel model)
        {
            string message = string.Empty;
            try
            {
                model.IdCompany = ((CompanyAccountModel)Session["UserModel"]).IdCompany;
                model.SaveProduct();

                message = "The Product Was Saved!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        public ActionResult ProductsPage()
        {
            if (Session["UserModel"] == null)
            {
                return RedirectToAction("Login", "Home", new { returnUrl = Request.Url.AbsolutePath });
            }
            GetAllCategoriesOne();
            GetAllCategoriesTwo();
            GetAllCategoriesThree();
            GetAllProducts();
            if (Request.IsAjaxRequest())
            {
                return PartialView("ProductsList", (List<Product>)ViewData["ProductsList"]);
            }
            else
            {
                return View();
            }
        }

        private void GetAllCategoriesThree()
        {
            if (Session["UserModel"] != null)
            {
                CompanyAccountModel ca = (CompanyAccountModel)Session["UserModel"];
                List<CategoryThree> lst = new List<CategoryThree>();
                CategoryThreeModel cm = new CategoryThreeModel();
                cm.IdCompany = ca.IdCompany;
                lst.AddRange(cm.getAllCategoryThreeByIdCompany());
                ViewData["CategoriesThreeList"] = new SelectList(lst, "IdCategoryThree", "CategoryThreeName");
            }
        }

        private void GetAllCategoriesTwo()
        {
            if (Session["UserModel"] != null)
            {
                CompanyAccountModel ca = (CompanyAccountModel)Session["UserModel"];
                List<CategoryTwo> lst = new List<CategoryTwo>();
                CategoryTwoModel cm = new CategoryTwoModel();
                cm.IdCompany = ca.IdCompany;
                lst.AddRange(cm.getAllCategoryTwoByIdCompany());
                ViewData["CategoriesTwoList"] = new SelectList(lst, "IdCategoryTwo", "CategoryTwoName");
            }
        }

        private void GetAllCategoriesOne()
        {
            if (Session["UserModel"] != null)
            {
                CompanyAccountModel ca = (CompanyAccountModel)Session["UserModel"];
                List<CategoryOne> lst = new List<CategoryOne>();
                CategoryOneModel cm = new CategoryOneModel();
                cm.IdCompany = ca.IdCompany;
                lst.AddRange(cm.getAllCategoryOneByIdCompany());
                ViewData["CategoriesOneList"] = new SelectList(lst, "IdCategoryOne", "CategoryOneName");
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(ProductModel model)
        {
            string message = string.Empty;
            try
            {
                model.DeleteProduct();

                message = "The Product Was Deleted!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { Message = message });
        }

        private void GetAllProducts()
        {
            if (Session["UserModel"] != null)
            {
                List<Product> lst = new List<Product>();
                CompanyAccountModel ca = (CompanyAccountModel)Session["UserModel"];
                Product c = new Product();
                ProductModel pm = new ProductModel();
                pm.IdCompany = ca.IdCompany;
                lst.Add(new Product { IdProduct = 0, ProductName = "" });//This row will be deleted after the datatable is created.
                lst.AddRange(pm.GetAllProducts());
                ViewData["ProductsList"] = lst;
                ViewData["IdCompany"] = ca.IdCompany;
                ViewData["CompanyName"] = ca.CompanyName;
            }
        }
    }
}
