using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iCatalogBB;

namespace iCatalogSite.Models
{
    public class CountryModel
    {
        public int IdCountry { get; set; }
        public string CountryName { get; set; }
        private BBCountries _countriesContext;

        public CountryModel()
        {
            _countriesContext = new BBCountries();
        }

        public void SaveCountry()
        {
            if (!_countriesContext.CountryExist(CountryName, IdCountry))
            {
                if (IdCountry.Equals(0))
                {
                    _countriesContext.InsertCountry(CountryName);
                }
                else
                {
                    _countriesContext.UpdateCountry(IdCountry, CountryName);
                }
            }
            else
            {
                throw new Exception("This Country Already Exists.");
            }
        }

        public List<Country> GetAllCountries()
        {
            return _countriesContext.GetAllCountries();
        }

        internal void DeleteCountry()
        {
            _countriesContext.DeleteCountry(IdCountry);
        }
    }
}