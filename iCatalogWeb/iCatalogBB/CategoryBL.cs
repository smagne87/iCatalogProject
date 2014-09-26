using iCatalogBB.Controllers;
using iCatalogData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iCatalogBB
{
    public class CategoryBL : RepositoryBase<Category, iCatalogDatabaseEntities>
    {
        public CategoryBL(iCatalogDatabaseEntities context):base(context)
        {

        }
    }
}
