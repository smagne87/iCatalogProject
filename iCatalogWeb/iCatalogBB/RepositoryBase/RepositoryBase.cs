using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data.Metadata.Edm;

namespace iCatalogBB.Controllers
{
    public abstract class RepositoryBase<T, DbCtxt> : IRepositoryBase<T>
        where T : class
        where DbCtxt : ObjectContext
    {
        // Fields
        private DbCtxt _context;
        private static Func<MetadataProperty, bool> CachedAnonymousMethodDelegate1;

        // Methods
        public RepositoryBase(DbCtxt context)
        {
            this._context = context;
        }

        public virtual void CreateNew(T entity)
        {
            string name = entity.GetType().Name;
            Type baseType = entity.GetType();
            while (!(baseType.BaseType == typeof(EntityObject)))
            {
                baseType = baseType.BaseType;
            }
            name = baseType.Name;
            this.DBContext.AddObject(name, entity);
        }

        public virtual void Delete(T entity)
        {
            this.DBContext.DeleteObject(entity);
        }

        public virtual void Delete(object key)
        {
            this.Delete(this.GetById(key));
        }

        public virtual IQueryable<T> Find(string propertyName, object value)
        {
            return Queryable.Where<T>(this.GetQuery<T>().AsQueryable<T>(), this.GetWhereExpression(propertyName, value));
        }

        public virtual ObjectQuery<T> GetAll()
        {
            return this.GetQuery<T>();
        }

        private Type GetBaseType(Type type)
        {
            Type baseType = type.BaseType;
            if ((baseType != null) && (baseType != typeof(EntityObject)))
            {
                return this.GetBaseType(type.BaseType);
            }
            return type;
        }

        public virtual T GetById(object id)
        {
            string key = this.GetKey(typeof(T));
            return Queryable.Where<T>(this.GetQuery<T>().AsQueryable<T>(), this.GetWhereExpression(key, id)).FirstOrDefault<T>();
        }

        private string GetKey(Type type)
        {
            Type type2;
            string typeName;
            if (this.HasBaseType(type, out type2))
            {
                typeName = this.GetTypeName(type2);
            }
            else
            {
                typeName = this.GetTypeName(type);
            }
            if (RepositoryBase<T, DbCtxt>.CachedAnonymousMethodDelegate1 == null)
            {
                //RepositoryBase<T, DbCtxt>.CachedAnonymousMethodDelegate1 = new Func<MetadataProperty, bool>(null, (IntPtr) RepositoryBase<T, DbCtxt>.<GetKey>b__0);
            }
            IEnumerable<EdmMember> source = Enumerable.First<MetadataProperty>(this.DBContext.CreateQuery<T>(typeName, new ObjectParameter[0]).GetResultType().EdmType.MetadataProperties).Value as IEnumerable<EdmMember>;
            return source.First<EdmMember>().Name;
        }

        private ObjectQuery<T> GetQuery<T1>()
        {
            Type type;
            if (this.HasBaseType(typeof(T), out type))
            {
                return this.DBContext.CreateQuery<T>(this.GetTypeName(type), new ObjectParameter[0]).OfType<T>();
            }
            return this.DBContext.CreateQuery<T>(this.GetTypeName(typeof(T)), new ObjectParameter[0]);
        }

        private string GetTypeName(Type type)
        {
            if (type.Name.Contains(this.DBContext.DefaultContainerName))
            {
                return string.Format("[{0}]", typeof(T).Name);
            }
            return string.Format("{0}.[{1}]", this.DBContext.DefaultContainerName, type.Name);
        }

        private Expression<Func<T, bool>> GetWhereExpression(string propertyName, object value)
        {
            ParameterExpression expression;
            try
            {
                Type propertyType = typeof(T).GetProperty(propertyName).PropertyType;
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("Field not found", exception.Message.ToString());
            }
            return Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(T), "e"), propertyName), Expression.Constant(value)), new ParameterExpression[] { expression });
        }

        private bool HasBaseType(Type type, out Type baseType)
        {
            Type type2 = type.GetType();
            baseType = this.GetBaseType(type);
            return (baseType != type2);
        }

        // Properties
        protected DbCtxt DBContext
        {
            get
            {
                return this._context;
            }
        }
    }
}
