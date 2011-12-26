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
            if (IdCountry.Equals(0))
            {
                _countriesContext.InsertCountry(CountryName);
            }
            else
            {
                _countriesContext.UpdateCountry(IdCountry, CountryName);
            }
        }

        public List<iCatalogData.Country> GetAllCountries()
        {
            return _countriesContext.GetAllCountries();
        }
    }
}