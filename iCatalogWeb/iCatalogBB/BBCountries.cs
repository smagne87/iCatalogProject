using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository = iCatalogData.iCatalogdbDataContext;
using Country = iCatalogData.Country;

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
                    Country c = new Country();
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
                    Country c = r.Countries.Where<Country>(co => co.IdCountry.Equals(idCountry)).SingleOrDefault();
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
                    return r.Countries.ToList();
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
                    return r.Countries.Where<Country>(c => c.IdCountry.Equals(id)).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CountryExist(string countryName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Countries.Any(c => c.CountryName.ToLower().Equals(countryName.ToLower()));
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
                    Country c = r.Countries.Where<Country>(co => co.IdCountry.Equals(IdCountry)).SingleOrDefault();
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
}