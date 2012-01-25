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
        public int? IdCategoryOne { get; set; }
        public int? IdCategoryThree { get; set; }
        public int? IdCategoryTwo { get; set; }
        public int IdCompany { get; set; }
        public string ProductDescription { get; set; }
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
                    _productsContext.InsertProduct(IdCategoryOne, IdCategoryThree, IdCategoryTwo, IdCompany, ProductDescription, ProductName);
                }
                else
                {
                    _productsContext.UpdateProduct(IdProduct, IdCategoryOne, IdCategoryThree, IdCategoryTwo, IdCompany, ProductDescription, ProductName);
                }
            }
            else
            {
                throw new Exception("This Product Already Exists.");
            }
        }

        public List<Product> GetAllProducts()
        {
            return _productsContext.GetAllProducts();
        }

        public List<Product> GetAllProductsByIdCompany()
        {
            return _productsContext.GetAllProductsByIdCompany(IdCompany);
        }

        internal void DeleteProduct()
        {
            _productsContext.DeleteProduct(IdProduct);
        }
    }
}