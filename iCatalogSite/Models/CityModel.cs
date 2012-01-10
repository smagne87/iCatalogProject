using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iCatalogBB;

namespace iCatalogSite.Models
{
    public class CityModel
    {
        public int IdCity { get; set; }
        public string CityName { get; set; }
        public int IdCountry { get; set; }
        private BBCities _contextCities;

        public CityModel()
        {
            _contextCities = new BBCities();
        }

        public List<City> GetAllCities()
        {
            return _contextCities.GetAllCities();
        }

        public List<City> GetAllCitiesByIdCountry()
        {
            return _contextCities.GetAllCitiesByIdCountry(IdCountry);
        }

        public List<City> GetAllCitiesByIdCountry(int idCountry)
        {
            return _contextCities.GetAllCitiesByIdCountry(idCountry);
        }

        public void Save()
        {
            if (!_contextCities.CityExist(CityName, IdCity, IdCountry))
            {
                if (IdCity.Equals(0))
                {
                    _contextCities.InsertCity(CityName, IdCountry);
                }
                else
                {
                    _contextCities.UpdateCity(IdCity, CityName, IdCountry);
                }
            }
            else
            {
                throw new Exception("The City Already Exists.");
            }
        }

        public void Delete()
        {
            try
            {
                _contextCities.DeleteCity(IdCity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}