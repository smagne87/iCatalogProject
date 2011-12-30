using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository = iCatalogData.iCatalogdbDataContext;
using RepositoryCity = iCatalogData.City;

namespace iCatalogBB
{
    public class BBCities
    {
        public void InsertCity(string CityName, int IdCountry)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepositoryCity c = new RepositoryCity();
                    c.CityName = CityName;
                    c.IdCountry = IdCountry;
                    r.Cities.InsertOnSubmit(c);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCity(int idCity, string CityName, int IdCountry)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepositoryCity c = r.Cities.Where<RepositoryCity>(co => co.IdCity.Equals(idCity)).SingleOrDefault();
                    c.CityName = CityName;
                    c.IdCountry = IdCountry;
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCity(int IdCity)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepositoryCity c = r.Cities.Where<RepositoryCity>(co => co.IdCity.Equals(IdCity)).SingleOrDefault();
                    r.Cities.DeleteOnSubmit(c);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<City> GetAllCities()
        {
            try
            {
                using (Repository r = new Repository())
                {
                    List<City> lst = new List<City>();
                    foreach (RepositoryCity aCity in r.Cities.ToList())
                    {
                        lst.Add(new City() { CityName = aCity.CityName, IdCity = aCity.IdCity, IdCountry = (int)aCity.IdCountry, CountryName = aCity.Country.CountryName });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<City> GetAllCitiesByIdCountry(int idCountry)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    List<City> lst = new List<City>();
                    foreach (RepositoryCity aCity in r.Cities.Where<RepositoryCity>(c => c.IdCountry.Equals(idCountry)).ToList())
                    {
                        lst.Add(new City() { CityName = aCity.CityName, IdCity = aCity.IdCity, IdCountry = (int)aCity.IdCountry, CountryName = aCity.Country.CountryName });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public City GetCityById(int id)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    City c = new City();
                    RepositoryCity rc = r.Cities.Where<RepositoryCity>(ci => ci.IdCity.Equals(id)).SingleOrDefault();
                    if (rc != null)
                    {
                        c.IdCity = rc.IdCity;
                        c.IdCountry = (int)rc.IdCountry;
                        c.CityName = rc.CityName;
                        c.CountryName = rc.Country.CountryName;
                    }
                    return c;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CityExist(string CityName, int idCity, int idCountry)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Cities.Any(c => c.CityName.ToLower().Equals(CityName.ToLower()) && !c.IdCity.Equals(idCity) && !c.IdCountry.Equals(idCountry));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class City
    {
        public int IdCity { get; set; }
        public string CityName { get; set; }
        public int IdCountry { get; set; }
        public string CountryName { get; set; }
    }
}
