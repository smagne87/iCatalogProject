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
        public void InsertProduct(int? idCategoryOne, int? idCategoryThree, int? idCategoryTwo, int? idCompany, string productDescription, string productName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepoProduct p = new RepoProduct();
                    p.IdCategoryOne = idCategoryOne;
                    p.IdCategoryThree = idCategoryThree;
                    p.IdCategoryTwo = idCategoryTwo;
                    p.IdCompany = idCompany;
                    p.ProductDescription = productDescription;                   
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

        public void UpdateProduct(int idProduct, int? idCategoryOne, int? idCategoryThree, int? idCategoryTwo, int? idCompany, string productDescription, string productName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepoProduct p = r.Products.Where<RepoProduct>(pr => pr.IdProduct.Equals(idProduct)).SingleOrDefault();
                    p.IdCategoryOne = idCategoryOne;
                    p.IdCategoryThree = idCategoryThree;
                    p.IdCategoryTwo = idCategoryTwo;
                    p.IdCompany = idCompany;
                    p.ProductDescription = productDescription;
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
                        lst.Add(new Product()
                        {
                            IdProduct = aProduct.IdProduct,
                            ProductName = aProduct.ProductName,
                            ProductDescription = aProduct.ProductDescription,
                            IdCategoryOne = aProduct.IdCategoryOne.HasValue ? aProduct.CategoryOne.IdCategoryOne : 0,
                            CategoryOneName = aProduct.IdCategoryOne.HasValue ? aProduct.CategoryOne.CategoryName : string.Empty,
                            CategoryOneDescription = aProduct.IdCategoryOne.HasValue ? aProduct.CategoryOne.CategoryDescription : string.Empty,
                            IdCategoryTwo = aProduct.IdCategoryTwo.HasValue ? aProduct.CategoryTwo.IdCategoryTwo : 0,
                            CategoryTwoName = aProduct.IdCategoryTwo.HasValue ? aProduct.CategoryTwo.CategoryName : string.Empty,
                            CategoryTwoDescription = aProduct.IdCategoryTwo.HasValue ? aProduct.CategoryTwo.CategoryDescription : string.Empty,
                            IdCategoryThree = aProduct.IdCategoryThree.HasValue ? aProduct.CategoryThree.IdCategoryThree : 0,
                            CategoryThreeName = aProduct.IdCategoryThree.HasValue ? aProduct.CategoryThree.CategoryName : string.Empty,
                            CategoryThreeDescription = aProduct.IdCategoryThree.HasValue ? aProduct.CategoryThree.CategoryDescription : string.Empty,
                            IdCompany = aProduct.Company.IdCompany,
                            CompanyName = aProduct.Company.CompanyName
                        });
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
                    RepoProduct aProduct = r.Products.Where<RepoProduct>(pr => pr.IdProduct.Equals(id)).SingleOrDefault();
                    if (aProduct != null)
                    {
                            product.IdProduct = aProduct.IdProduct;
                            product.ProductName = aProduct.ProductName;
                            product.ProductDescription = aProduct.ProductDescription;
                            product.IdCategoryOne = aProduct.IdCategoryOne.HasValue ? aProduct.CategoryOne.IdCategoryOne : 0;
                            product.CategoryOneName = aProduct.IdCategoryOne.HasValue ? aProduct.CategoryOne.CategoryName : string.Empty;
                            product.CategoryOneDescription = aProduct.IdCategoryOne.HasValue ? aProduct.CategoryOne.CategoryDescription : string.Empty;
                            product.IdCategoryTwo = aProduct.IdCategoryTwo.HasValue ? aProduct.CategoryTwo.IdCategoryTwo : 0;
                            product.CategoryTwoName = aProduct.IdCategoryTwo.HasValue ? aProduct.CategoryTwo.CategoryName : string.Empty;
                            product.CategoryTwoDescription = aProduct.IdCategoryTwo.HasValue ? aProduct.CategoryTwo.CategoryDescription : string.Empty;
                            product.IdCategoryThree = aProduct.IdCategoryThree.HasValue ? aProduct.CategoryThree.IdCategoryThree : 0;
                            product.CategoryThreeName = aProduct.IdCategoryThree.HasValue ? aProduct.CategoryThree.CategoryName : string.Empty;
                            product.CategoryThreeDescription = aProduct.IdCategoryThree.HasValue ? aProduct.CategoryThree.CategoryDescription : string.Empty;
                            product.IdCompany = aProduct.Company.IdCompany;
                            product.CompanyName = aProduct.Company.CompanyName;
                    }
                    return product;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ProductExist(string productName, int idProduct, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Products.Any(p => p.ProductName.ToLower().Equals(productName.ToLower()) && p.IdCompany.Equals(idCompany) && !p.IdProduct.Equals(idProduct));
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

        public List<Product> GetAllProductsByIdCompany(int IdCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    List<Product> lst = new List<Product>();
                    foreach (RepoProduct aProduct in r.Products.Where<RepoProduct>(p=>p.IdCompany.Equals(IdCompany)).ToList())
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
    }

    public class Product
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int IdCategoryOne { get; set; }
        public string CategoryOneName { get; set; }
        public string CategoryOneDescription { get; set; }
        public int IdCategoryTwo { get; set; }
        public string CategoryTwoName { get; set; }
        public string CategoryTwoDescription { get; set; }
        public int IdCategoryThree { get; set; }
        public string CategoryThreeName { get; set; }
        public string CategoryThreeDescription { get; set; }
        public int IdCompany { get; set; }
        public string CompanyName { get; set; }      

    }
}
