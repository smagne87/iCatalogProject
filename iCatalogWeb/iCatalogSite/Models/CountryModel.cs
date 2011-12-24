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

        public void SaveCountry(int idCountry, string countryName)
        {
        }
    }
}