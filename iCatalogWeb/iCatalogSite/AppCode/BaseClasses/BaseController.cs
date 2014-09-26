using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using iCatalogBB;

namespace iCatalogSite
{
    public class BaseController : Controller
    {
        public ModelController ModelController = new ModelController();

        public JsonResult GetJsonErrors(ModelStateDictionary modelState)
        {
            var errors = GetErrors(ModelState);
            if (errors.Any())
                return GetJsonForErrors(errors);

            return null;
        }

        private JsonResult GetJsonForErrors(Dictionary<string, ModelErrorCollection> errorList)
        {
            var errorMessage = new StringBuilder();
            foreach (var validationItem in errorList)
            {
                foreach (var error in validationItem.Value)
                {
                    if (errorMessage.Length > 0)
                        errorMessage.Append(string.Format("<br/>{0}", error.ErrorMessage));
                    else
                        errorMessage.Append(string.Format("{0}", error.ErrorMessage));
                }
            }
            return Json(new BaseResponse(false, errorMessage.ToString()), JsonRequestBehavior.AllowGet);
        }

        private Dictionary<string, ModelErrorCollection> GetErrors(ModelStateDictionary modelState)
        {
            var errors = modelState.Where(x => x.Value.Errors.Any())
                                        .Select(x => new { x.Key, x.Value.Errors })
                                        .ToDictionary(o => o.Key, o => o.Errors);

            return errors;
        }

        protected virtual ViewResult GetCleanView()
        {
            ModelState.Clear();
            return View();
        }
    }
}