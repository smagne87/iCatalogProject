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
        public string CountryName { get; set; }
        public int IdCity { get; set; }
        public string CityName { get; set; }
        public string WebUrl { get; set; }
        public string Street { get; set; }
        public string NumberST { get; set; }
        public string PostalCode { get; set; }
        public string NewPassword { get; set; }

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
            CountryName = company.CountryName;
            IdCity = company.IdCity;
            CityName = company.CityName;
            if (!string.IsNullOrEmpty(Address))
            {
                string[] arrAddress = Address.Split(' ');
                bool pasoNum = false;
                int num = 0;
                for (int i = 0; i < arrAddress.Length; i++)
                {
                    if (int.TryParse(arrAddress[i], out num) && !pasoNum)
                    {
                        NumberST = arrAddress[i];
                        pasoNum = true;
                    }
                    else if (pasoNum)
                    {
                        PostalCode = arrAddress[i];
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Street))
                        {
                            Street += arrAddress[i];
                        }
                        else
                        {
                            Street += " " + arrAddress[i];
                        }
                    }
                }
            }
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

        internal void saveData()
        {
            if (!string.IsNullOrEmpty(Street))
            {
                Address = string.Format("{0} {1} {2}", Street, NumberST, PostalCode);
            }
            _contextCompanyAccount.saveData(IdCompany, CompanyName, Phone, WebUrl, Address, IdCity, IdCountry);
        }

        internal void SavePassword()
        {
            _contextCompanyAccount.savePassword(CompanyUserName, NewPassword);
        }
    }
}