using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iCatalogSite.Areas.Admin.Models
{
    public class CategoryModel : BaseModel<CategoryModel, iCatalogData.Category>
    {
        public int IdCategory { get; set; }

        [DisplayName("Name")]
        public string CategoryName { get; set; }

        [DisplayName("Description")]
        public string CategoryDescription { get; set; }

    }
}