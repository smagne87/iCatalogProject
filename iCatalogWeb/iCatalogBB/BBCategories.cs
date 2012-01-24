using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository = iCatalogData.iCatalogdbDataContext;
using RCategoryOne = iCatalogData.CategoryOne;
using RCategoryTwo = iCatalogData.CategoryTwo;
using RCategoryThree = iCatalogData.CategoryThree;

namespace iCatalogBB
{
    public class BBCategories
    {
        #region Category One
        public List<CategoryOne> getAllCategoryOneByIdCompany(int idCompany)
        {
            List<CategoryOne> lst = new List<CategoryOne>();
            try
            {
                using (Repository r = new Repository())
                {
                    List<RCategoryOne> lstr = r.CategoryOnes.Where(co => co.IdCompany.Equals(idCompany)).ToList();
                    foreach (RCategoryOne acat in lstr)
                    {
                        lst.Add(new CategoryOne
                        {
                            CategoryOneName = acat.CategoryName,
                            IdCompany = acat.Company.IdCompany
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst.Distinct(new DistinctCategoryOne()).ToList();
        }

        public List<CategoryOne> getAllCategoryOneByCategoryName(string CategoryOneName, int idCompany)
        {
            List<CategoryOne> lst = new List<CategoryOne>();
            try
            {
                using (Repository r = new Repository())
                {
                    List<RCategoryOne> lstr = r.CategoryOnes.Where(co => co.CategoryName.Equals(CategoryOneName) && co.IdCompany.Equals(idCompany)).ToList();
                    foreach (RCategoryOne acat in lstr)
                    {
                        lst.Add(new CategoryOne
                        {
                            IdCategoryOne = acat.IdCategoryOne,
                            CategoryOneName = acat.CategoryName,
                            CategoryOneDescription = acat.CategoryDescription,
                            CompanyName = acat.Company.CompanyName,
                            IdCompany = acat.Company.IdCompany
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public CategoryOne getCategoryOneByIdCategory(int IdCategoryOne)
        {
            CategoryOne c = new CategoryOne();
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryOne acat = r.CategoryOnes.Where(co => co.IdCategoryOne.Equals(IdCategoryOne)).SingleOrDefault();
                    c.IdCategoryOne = acat.IdCategoryOne;
                    c.CategoryOneName = acat.CategoryName;
                    c.CategoryOneDescription = acat.CategoryDescription;
                    c.CompanyName = acat.Company.CompanyName;
                    c.IdCompany = acat.Company.IdCompany;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }
        public void insertCategoryOne(string categoryName, string categoryDescription, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryOne c = new RCategoryOne();
                    c.CategoryName = categoryName;
                    c.CategoryDescription = categoryDescription;
                    c.IdCompany = idCompany;
                    r.CategoryOnes.InsertOnSubmit(c);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void deleteSingleCategoryOne(int idCategoryOne, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryOne c = r.CategoryOnes.Where(ca=> ca.IdCategoryOne.Equals(idCategoryOne)).SingleOrDefault();
                    if (c != null)
                    {
                        r.CategoryOnes.DeleteOnSubmit(c);
                        r.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void deleteAllCategoryOne(string categoryName, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    List<RCategoryOne> c = r.CategoryOnes.Where(ca => ca.CategoryName.Equals(categoryName) && ca.IdCompany.Equals(idCompany)).ToList();
                    if (c.Count > 0)
                    {
                        r.CategoryOnes.DeleteAllOnSubmit(c);
                        r.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateCategoryOne(int idCategoryOne, string categoryDescription)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryOne c = r.CategoryOnes.Where(ca=> ca.IdCategoryOne.Equals(idCategoryOne)).SingleOrDefault();
                    if (c != null)
                    {
                        c.CategoryDescription = categoryDescription;
                        r.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool existsCategoryOne(string categoryName, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.CategoryOnes.Any(ca => ca.CategoryName.Equals(categoryName) && ca.IdCompany.Equals(idCompany));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Category Two
        public List<CategoryTwo> getAllCategoryTwoByIdCompany(int idCompany)
        {
            List<CategoryTwo> lst = new List<CategoryTwo>();
            try
            {
                using (Repository r = new Repository())
                {
                    List<RCategoryTwo> lstr = r.CategoryTwos.Where(co => co.IdCompany.Equals(idCompany)).ToList();
                    foreach (RCategoryTwo acat in lstr)
                    {
                        lst.Add(new CategoryTwo
                        {
                            CategoryTwoName = acat.CategoryName,
                            IdCompany = acat.Company.IdCompany
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst.Distinct(new DistinctCategoryTwo()).ToList();
        }

        public List<CategoryTwo> getAllCategoryTwoByCategoryName(string CategoryTwoName, int idCompany)
        {
            List<CategoryTwo> lst = new List<CategoryTwo>();
            try
            {
                using (Repository r = new Repository())
                {
                    List<RCategoryTwo> lstr = r.CategoryTwos.Where(co => co.CategoryName.Equals(CategoryTwoName) && co.IdCompany.Equals(idCompany)).ToList();
                    foreach (RCategoryTwo acat in lstr)
                    {
                        lst.Add(new CategoryTwo
                        {
                            IdCategoryTwo = acat.IdCategoryTwo,
                            CategoryTwoName = acat.CategoryName,
                            CategoryTwoDescription = acat.CategoryDescription,
                            CompanyName = acat.Company.CompanyName,
                            IdCompany = acat.Company.IdCompany
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public CategoryTwo getCategoryTwoByIdCategory(int IdCategoryTwo)
        {
            CategoryTwo c = new CategoryTwo();
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryTwo acat = r.CategoryTwos.Where(co => co.IdCategoryTwo.Equals(IdCategoryTwo)).SingleOrDefault();
                    c.IdCategoryTwo = acat.IdCategoryTwo;
                    c.CategoryTwoName = acat.CategoryName;
                    c.CategoryTwoDescription = acat.CategoryDescription;
                    c.CompanyName = acat.Company.CompanyName;
                    c.IdCompany = acat.Company.IdCompany;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }
        public void insertCategoryTwo(string categoryName, string categoryDescription, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryTwo c = new RCategoryTwo();
                    c.CategoryName = categoryName;
                    c.CategoryDescription = categoryDescription;
                    c.IdCompany = idCompany;
                    r.CategoryTwos.InsertOnSubmit(c);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void deleteSingleCategoryTwo(int idCategoryTwo, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryTwo c = r.CategoryTwos.Where(ca=> ca.IdCategoryTwo.Equals(idCategoryTwo)).SingleOrDefault();
                    if (c != null)
                    {
                        r.CategoryTwos.DeleteOnSubmit(c);
                        r.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void deleteAllCategoryTwo(string categoryName, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    List<RCategoryTwo> c = r.CategoryTwos.Where(ca => ca.CategoryName.Equals(categoryName) && ca.IdCompany.Equals(idCompany)).ToList();
                    if (c.Count < 0)
                    {
                        r.CategoryTwos.DeleteAllOnSubmit(c);
                        r.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateCategoryTwo(int idCategoryTwo, string categoryDescription)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryTwo c = r.CategoryTwos.Where(ca=> ca.IdCategoryTwo.Equals(idCategoryTwo)).SingleOrDefault();
                    if (c != null)
                    {
                        c.CategoryDescription = categoryDescription;
                        r.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool existsCategoryTwo(string categoryName, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.CategoryTwos.Any(ca => ca.CategoryName.Equals(categoryName) && ca.IdCompany.Equals(idCompany));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Category Three
        public List<CategoryThree> getAllCategoryThreeByIdCompany(int idCompany)
        {
            List<CategoryThree> lst = new List<CategoryThree>();
            try
            {
                using (Repository r = new Repository())
                {
                    List<RCategoryThree> lstr = r.CategoryThrees.Where(co => co.IdCompany.Equals(idCompany)).ToList();
                    foreach (RCategoryThree acat in lstr)
                    {
                        lst.Add(new CategoryThree
                        {
                            CategoryThreeName = acat.CategoryName,
                            IdCompany = acat.Company.IdCompany
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst.Distinct(new DistinctCategoryThree()).ToList();
        }

        public List<CategoryThree> getAllCategoryThreeByCategoryName(string CategoryThreeName, int idCompany)
        {
            List<CategoryThree> lst = new List<CategoryThree>();
            try
            {
                using (Repository r = new Repository())
                {
                    List<RCategoryThree> lstr = r.CategoryThrees.Where(co => co.CategoryName.Equals(CategoryThreeName) && co.IdCompany.Equals(idCompany)).ToList();
                    foreach (RCategoryThree acat in lstr)
                    {
                        lst.Add(new CategoryThree
                        {
                            IdCategoryThree = acat.IdCategoryThree,
                            CategoryThreeName = acat.CategoryName,
                            CategoryThreeDescription = acat.CategoryDescription,
                            CompanyName = acat.Company.CompanyName,
                            IdCompany = acat.Company.IdCompany
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public CategoryThree getCategoryThreeByIdCategory(int IdCategoryThree)
        {
            CategoryThree c = new CategoryThree();
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryThree acat = r.CategoryThrees.Where(co => co.IdCategoryThree.Equals(IdCategoryThree)).SingleOrDefault();
                    c.IdCategoryThree = acat.IdCategoryThree;
                    c.CategoryThreeName = acat.CategoryName;
                    c.CategoryThreeDescription = acat.CategoryDescription;
                    c.CompanyName = acat.Company.CompanyName;
                    c.IdCompany = acat.Company.IdCompany;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }
        public void insertCategoryThree(string categoryName, string categoryDescription, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryThree c = new RCategoryThree();
                    c.CategoryName = categoryName;
                    c.CategoryDescription = categoryDescription;
                    c.IdCompany = idCompany;
                    r.CategoryThrees.InsertOnSubmit(c);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void deleteSingleCategoryThree(int idCategoryThree, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryThree c = r.CategoryThrees.Where(ca => ca.IdCategoryThree.Equals(idCategoryThree)).SingleOrDefault();
                    if (c != null)
                    {
                        r.CategoryThrees.DeleteOnSubmit(c);
                        r.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void deleteAllCategoryThree(string categoryName, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    List<RCategoryThree> c = r.CategoryThrees.Where(ca => ca.CategoryName.Equals(categoryName) && ca.IdCompany.Equals(idCompany)).ToList();
                    if (c.Count < 0)
                    {
                        r.CategoryThrees.DeleteAllOnSubmit(c);
                        r.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateCategoryThree(int idCategoryThree, string categoryDescription)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RCategoryThree c = r.CategoryThrees.Where(ca => ca.IdCategoryThree.Equals(idCategoryThree)).SingleOrDefault();
                    if (c != null)
                    {
                        c.CategoryDescription = categoryDescription;
                        r.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool existsCategoryThree(string categoryName, int idCompany)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.CategoryThrees.Any(ca => ca.CategoryName.Equals(categoryName) && ca.IdCompany.Equals(idCompany));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

    public class CategoryOne
    {
        public int IdCategoryOne { get; set; }
        public string CategoryOneName { get; set; }
        public string CategoryOneDescription { get; set; }
        public int IdCompany { get; set; }
        public string CompanyName { get; set; }
    }

    public class CategoryTwo
    {
        public int IdCategoryTwo { get; set; }
        public string CategoryTwoName { get; set; }
        public string CategoryTwoDescription { get; set; }
        public int IdCompany { get; set; }
        public string CompanyName { get; set; }
    }

    public class CategoryThree
    {
        public int IdCategoryThree { get; set; }
        public string CategoryThreeName { get; set; }
        public string CategoryThreeDescription { get; set; }
        public int IdCompany { get; set; }
        public string CompanyName { get; set; }
    }

    public class DistinctCategoryOne : IEqualityComparer<CategoryOne>
    {
        public bool Equals(CategoryOne x, CategoryOne y)
        {
            return x.CategoryOneName.Equals(y.CategoryOneName);
        }

        public int GetHashCode(CategoryOne obj)
        {
            return obj.CategoryOneName.GetHashCode();
        }
    }

    public class DistinctCategoryTwo : IEqualityComparer<CategoryTwo>
    {
        public bool Equals(CategoryTwo x, CategoryTwo y)
        {
            return x.CategoryTwoName.Equals(y.CategoryTwoName);
        }

        public int GetHashCode(CategoryTwo obj)
        {
            return obj.CategoryTwoName.GetHashCode();
        }
    }

    public class DistinctCategoryThree : IEqualityComparer<CategoryThree>
    {
        public bool Equals(CategoryThree x, CategoryThree y)
        {
            return x.CategoryThreeName.Equals(y.CategoryThreeName);
        }

        public int GetHashCode(CategoryThree obj)
        {
            return obj.CategoryThreeName.GetHashCode();
        }
    }
}