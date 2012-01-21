using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCatalogSite.Models;
using Product = iCatalogBB.Product;

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
            GetAllProducts();
            if (Request.IsAjaxRequest())
            {
                return PartialView("ProductsList", (List<Product>)ViewData["CountriesList"]);
            }
            else
            {
                return View();
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
            List<Product> lst = new List<Product>();
            Product c = new Product();
            ProductModel pm = new ProductModel();
            lst.Add(new Product { IdProduct = 0, ProductName = "" });//This row will be deleted after the datatable is created.
            lst.AddRange(pm.GetAllProducts());
            ViewData["ProductsList"] = lst;
        }

    }
}
