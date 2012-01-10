using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iCatalogBB;

namespace iCatalogSite.Models
{
    public class CompanyAccountModel
    {
        public int IdCompany { get; set; }
        public string CompanyUserName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int IdCountry { get; set; }
        //public string CountryName { get; set; }
        public int IdCity { get; set; }
        //public string CityName { get; set; }
        public string WebUrl { get; set; }

        private BBCompanyAccount _contextCompanyAccount;
        
        public CompanyAccountModel()
        {
            _contextCompanyAccount = new BBCompanyAccount();
        }

        internal bool validateUserPassword()
        {
            bool result = false;
            if (_contextCompanyAccount.validateUserPassword(CompanyUserName, Password))
            {
                result = true;
                GetCompanyAccount();
            }
            return result;
        }

        private void GetCompanyAccount()
        {
            CompanyAccount company = _contextCompanyAccount.getCompanyAccountByCompanyUserName(CompanyUserName);
            IdCompany = company.IdCompany;
            CompanyName = company.CompanyName;
            CompanyUserName = company.CompanyUserName;
            Email = company.Email;
            Phone = company.Phone;
            Address = company.Address;
            IdCountry = company.IdCountry;
            IdCity = company.IdCity;
        }

        internal bool existsCompanyUserName()
        {
            return _contextCompanyAccount.existsCompanyUserName(CompanyUserName);
        }

        internal bool existsEmail()
        {
            return _contextCompanyAccount.existsEmail(Email);
        }

        internal void register()
        {
            CompanyAccount company = new CompanyAccount();
            company.CompanyName = CompanyName;
            company.CompanyUserName = CompanyUserName;
            company.Email = Email;
            company.Phone = Phone;
            company.Address = Address;
            company.IdCountry = IdCountry;
            company.IdCity = IdCity;
            company.Password = Password;
            company.WebUrl = WebUrl;
            _contextCompanyAccount.registerCompany(company);
        }
    }
}