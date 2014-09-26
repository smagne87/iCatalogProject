using iCatalogData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iCatalogBB
{
    public class ModelController
    {
        private CategoryBL categoryBL;

        public CategoryBL CategoryBL
        {
            get {
                if (categoryBL == null)
                    categoryBL = new CategoryBL(DBContext);
                return categoryBL; 
            }
        }

        private iCatalogDatabaseEntities _DBContext;

        public iCatalogDatabaseEntities DBContext
        {
            get
            {
                if (_DBContext == null)
                {
                    _DBContext = new iCatalogDatabaseEntities();
                }
                return _DBContext;
            }
        }

        public void SubmitChanges()
        {
            DBContext.SaveChanges();
        }
    }
}
