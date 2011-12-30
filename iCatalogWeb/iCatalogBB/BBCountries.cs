using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository = iCatalogData.iCatalogdbDataContext;
using RepoCountry = iCatalogData.Country;

namespace iCatalogBB
{
    public class BBCountries
    {
        public void InsertCountry(string countryName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepoCountry c = new RepoCountry();
                    c.CountryName = countryName;
                    r.Countries.InsertOnSubmit(c);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCountry(int idCountry, string countryName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepoCountry c = r.Countries.Where<RepoCountry>(co => co.IdCountry.Equals(idCountry)).SingleOrDefault();
                    c.CountryName = countryName;
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Country> GetAllCountries()
        {
            try
            {
                using (Repository r = new Repository())
                {
                    List<Country> lst = new List<Country>();
                    foreach (RepoCountry aCountry in r.Countries.ToList())
                    {
                        lst.Add(new Country() { IdCountry = aCountry.IdCountry, CountryName = aCountry.CountryName });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Country GetCountryById(int id)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    Country co = new Country();
                    RepoCountry rco = r.Countries.Where<RepoCountry>(c => c.IdCountry.Equals(id)).SingleOrDefault();
                    if (rco != null)
                    {
                        co.CountryName = rco.CountryName;
                        co.IdCountry = rco.IdCountry;
                    }
                    return co;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CountryExist(string countryName, int idCountry)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Countries.Any(c => c.CountryName.ToLower().Equals(countryName.ToLower()) && !c.IdCountry.Equals(idCountry));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCountry(int IdCountry)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepoCountry c = r.Countries.Where<RepoCountry>(co => co.IdCountry.Equals(IdCountry)).SingleOrDefault();
                    r.Countries.DeleteOnSubmit(c);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Country
    {
        public int IdCountry { get; set; }
        public string CountryName { get; set; }
    }
}