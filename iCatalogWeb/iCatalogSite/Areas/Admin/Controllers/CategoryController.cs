using iCatalogSite.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iCatalogSite.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        //
        // GET: /Admin/Category/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            ViewData.Model = CategoryModel.ConvertToModelList(ModelController.CategoryBL.GetAll().ToList());
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryModel model)
        {

            return View();
        }
    }
}
