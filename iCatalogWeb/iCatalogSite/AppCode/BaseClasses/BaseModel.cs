using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace iCatalogSite
{
    public class BaseModel<T1, T2> where T1 : class, new() 
                                   where T2 : class, new()
    {
        public static T2 ModelToEF(T1 m)
        {
            T2 ef = new T2();
            PropertyInfo[] efProperties = m.GetType().GetProperties();
            foreach (PropertyInfo pi in efProperties)
            {
                PropertyInfo propertyInfo = m.GetType().GetProperties().Where(p => p.Name.ToLower().Equals(pi.Name.ToLower())).SingleOrDefault();
                if (propertyInfo != null)
	            {
                    if (propertyInfo.GetType() == pi.GetType())
                    {
                        object value = propertyInfo.GetValue(m, null);
                        if (value != null)
                        {
                            pi.SetValue(ef, value, null);
                        }
                    }
                }
            }
            return ef;
        }

        public static T1 EFToModel(T2 ef)
        {
            T1 m = new T1();
            PropertyInfo[] efProperties = m.GetType().GetProperties();
            foreach (PropertyInfo pi in efProperties)
            {
                PropertyInfo propertyInfo = ef.GetType().GetProperties().Where(p => p.Name.ToLower().Equals(pi.Name.ToLower())).SingleOrDefault();
                if (propertyInfo != null)
                {
                    if (propertyInfo.GetType() == pi.GetType())
                    {
                        object value = propertyInfo.GetValue(ef, null);
                        if (value != null)
                        {
                            pi.SetValue(m, value, null);
                        }
                    }
                }
            }
            return m;
        }

        public static List<T2> ConvertToEFList(List<T1> listModel)
        {
            return listModel.Select(i => ModelToEF(i)).ToList();
        }

        public static List<T1> ConvertToModelList(List<T2> listEF)
        {
            return listEF.Select(i => EFToModel(i)).ToList();
        }
    }
}