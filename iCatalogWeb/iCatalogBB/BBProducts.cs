using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository = iCatalogData.iCatalogdbDataContext;
using RepoProduct = iCatalogData.Product;

namespace iCatalogBB
{
    public class BBProducts
    {
        public void InsertProduct(string productName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepoProduct p = new RepoProduct();
                    p.ProductName = productName;
                    r.Products.InsertOnSubmit(p);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProduct(int idProduct, string productName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepoProduct p = r.Products.Where<RepoProduct>(pr => pr.IdProduct.Equals(idProduct)).SingleOrDefault();
                    p.ProductName = productName;
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                using (Repository r = new Repository())
                {
                    List<Product> lst = new List<Product>();
                    foreach (RepoProduct aProduct in r.Products.ToList())
                    {
                        lst.Add(new Product() { IdProduct = aProduct.IdProduct, ProductName = aProduct.ProductName });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    Product product = new Product();
                    RepoProduct p = r.Products.Where<RepoProduct>(pr => pr.IdProduct.Equals(id)).SingleOrDefault();
                    if (p != null)
                    {
                        product.ProductName = p.ProductName;
                        product.IdProduct = p.IdProduct;
                    }
                    return product;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ProductExist(string productName, int idProduct)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Products.Any(p => p.ProductName.ToLower().Equals(productName.ToLower()) && !p.IdProduct.Equals(idProduct));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProduct(int IdProduct)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepoProduct p = r.Products.Where<RepoProduct>(pr => pr.IdProduct.Equals(IdProduct)).SingleOrDefault();
                    r.Products.DeleteOnSubmit(p);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Product
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
    }
}
