using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iCatalogBB;

namespace iCatalogSite.Models
{
    public class ProductModel
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        private BBProducts _productsContext;

        public ProductModel()
        {
            _productsContext = new BBProducts();
        }

        public void SaveProduct()
        {
            if (!_productsContext.ProductExist(ProductName, IdProduct))
            {
                if (IdProduct.Equals(0))
                {
                    _productsContext.InsertProduct(ProductName);
                }
                else
                {
                    _productsContext.UpdateProduct(IdProduct, ProductName);
                }
            }
            else
            {
                throw new Exception("This Country Already Exists.");
            }
        }

        public List<Product> GetAllProducts()
        {
            return _productsContext.GetAllProducts();
        }

        internal void DeleteProduct()
        {
            _productsContext.DeleteProduct(IdProduct);
        }
    }
}