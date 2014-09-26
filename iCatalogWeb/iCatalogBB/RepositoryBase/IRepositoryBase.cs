using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections;
using System.Data.Objects;

namespace iCatalogBB.Controllers
{
    public interface IRepositoryBase<T> where T : class
    {
        void CreateNew(T entity);
        void Delete(object key);
        void Delete(T entity);
        IQueryable<T> Find(string propertyName, object value);
        ObjectQuery<T> GetAll();
        T GetById(object id);
    }
}
