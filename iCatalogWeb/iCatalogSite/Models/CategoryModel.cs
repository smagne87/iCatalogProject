using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iCatalogBB;

namespace iCatalogSite.Models
{
    public class CategoryOneModel
    {
        public int IdCategoryOne { get; set; }
        public string CategoryOneName { get; set; }
        public string CategoryOneDescription { get; set; }
        public int IdCompany { get; set; }
        public string CompanyName { get; set; }

        private BBCategories _contextCategories;

        public CategoryOneModel()
        {
            _contextCategories = new BBCategories();
        }

        internal List<CategoryOne> getAllCategoryOneByIdCompany()
        {
            return _contextCategories.getAllCategoryOneByIdCompany(IdCompany);
        }

        internal List<CategoryOne> getAllCategoryOneByCategoryName()
        {
            return _contextCategories.getAllCategoryOneByCategoryName(CategoryOneName, IdCompany);
        }

        internal CategoryOne getCategoryOneByIdCategoryOne()
        {
            return _contextCategories.getCategoryOneByIdCategory(IdCategoryOne);
        }

        internal bool existsCategoryOne()
        {
            return _contextCategories.existsCategoryOne(CategoryOneName, IdCompany);
        }

        internal void Save()
        {
            if (IdCategoryOne.Equals(0))
            {
                _contextCategories.insertCategoryOne(CategoryOneName, CategoryOneDescription, IdCompany);
            }
            else
            {
                _contextCategories.updateCategoryOne(IdCategoryOne, CategoryOneDescription);
            }
        }

        internal void DeleteAll()
        {
            _contextCategories.deleteAllCategoryOne(CategoryOneName, IdCompany);
        }

        internal void DeleteSingle()
        {
            _contextCategories.deleteSingleCategoryOne(IdCategoryOne, IdCompany);
        }
    }

    public class CategoryTwoModel
    {
        public int IdCategoryTwo { get; set; }
        public string CategoryTwoName { get; set; }
        public string CategoryTwoDescription { get; set; }
        public int IdCompany { get; set; }
        public string CompanyName { get; set; }

        private BBCategories _contextCategories;

        public CategoryTwoModel()
        {
            _contextCategories = new BBCategories();
        }

        internal List<CategoryTwo> getAllCategoryTwoByIdCompany()
        {
            return _contextCategories.getAllCategoryTwoByIdCompany(IdCompany);
        }

        internal List<CategoryTwo> getAllCategoryTwoByCategoryName()
        {
            return _contextCategories.getAllCategoryTwoByCategoryName(CategoryTwoName, IdCompany);
        }

        internal CategoryTwo getCategoryTwoByIdCategoryTwo()
        {
            return _contextCategories.getCategoryTwoByIdCategory(IdCategoryTwo);
        }

        internal bool existsCategoryTwo()
        {
            return _contextCategories.existsCategoryTwo(CategoryTwoName, IdCompany);
        }

        internal void Save()
        {
            if (IdCategoryTwo.Equals(0))
            {
                _contextCategories.insertCategoryTwo(CategoryTwoName, CategoryTwoDescription, IdCompany);
            }
            else
            {
                _contextCategories.updateCategoryTwo(IdCategoryTwo, CategoryTwoDescription);
            }
        }

        internal void DeleteAll()
        {
            _contextCategories.deleteAllCategoryTwo(CategoryTwoName, IdCompany);
        }

        internal void DeleteSingle()
        {
            _contextCategories.deleteSingleCategoryTwo(IdCategoryTwo, IdCompany);
        }
    }

    public class CategoryThreeModel
    {
        public int IdCategoryThree { get; set; }
        public string CategoryThreeName { get; set; }
        public string CategoryThreeDescription { get; set; }
        public int IdCompany { get; set; }
        public string CompanyName { get; set; }

        private BBCategories _contextCategories;

        public CategoryThreeModel()
        {
            _contextCategories = new BBCategories();
        }

        internal List<CategoryThree> getAllCategoryThreeByIdCompany()
        {
            return _contextCategories.getAllCategoryThreeByIdCompany(IdCompany);
        }

        internal List<CategoryThree> getAllCategoryThreeByCategoryName()
        {
            return _contextCategories.getAllCategoryThreeByCategoryName(CategoryThreeName, IdCompany);
        }

        internal CategoryThree getCategoryThreeByIdCategoryThree()
        {
            return _contextCategories.getCategoryThreeByIdCategory(IdCategoryThree);
        }

        internal bool existsCategoryThree()
        {
            return _contextCategories.existsCategoryThree(CategoryThreeName, IdCompany);
        }

        internal void Save()
        {
            if (IdCategoryThree.Equals(0))
            {
                _contextCategories.insertCategoryThree(CategoryThreeName, CategoryThreeDescription, IdCompany);
            }
            else
            {
                _contextCategories.updateCategoryThree(IdCategoryThree, CategoryThreeDescription);
            }
        }

        internal void DeleteAll()
        {
            _contextCategories.deleteAllCategoryThree(CategoryThreeName, IdCompany);
        }

        internal void DeleteSingle()
        {
            _contextCategories.deleteSingleCategoryThree(IdCategoryThree, IdCompany);
        }
    }
}