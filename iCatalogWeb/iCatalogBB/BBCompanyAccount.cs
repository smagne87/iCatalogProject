using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository = iCatalogData.iCatalogdbDataContext;
using Company = iCatalogData.Company;

namespace iCatalogBB
{
    public class BBCompanyAccount
    {
        public bool existsCompanyUserName(string companyUserName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Companies.Any<Company>(c => c.CompanyUserName.ToLower().Equals(companyUserName.ToLower()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool existsEmail(string email)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Companies.Any<Company>(c => c.Email.ToLower().Equals(email.ToLower()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void registerCompany(CompanyAccount company)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    Company c = new Company();
                    c.CompanyUserName = company.CompanyUserName;
                    c.IdCountry = company.IdCountry;
                    c.IdCity = company.IdCity;
                    c.CompanyName = company.CompanyName;
                    c.Password = getEncryptedPassword(company.Password);
                    c.Email = company.Email;
                    c.Phone = company.Phone;
                    c.Address = company.Address;
                    c.WebUrl = company.WebUrl;
                    r.Companies.InsertOnSubmit(c);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CompanyAccount getCompanyAccountByCompanyUserName(string CompanyUserName)
        {
            return getCompanyAccount(CompanyUserName);
        }

        private CompanyAccount getCompanyAccount(string companyUserName)
        {
            Company c = getCompanyByUserName(companyUserName);
            CompanyAccount ca = null;
            if (ca != null)
            {
                ca = new CompanyAccount();
                ca.IdCompany = c.IdCompany;
                ca.CompanyUserName = c.CompanyUserName;
                ca.Email = c.Email;
                ca.CompanyName = c.CompanyName;
                ca.Password = c.Password;
                ca.Phone = c.Phone;
                ca.WebUrl = c.WebUrl;
                if (c.IdCity.HasValue)
                {
                    ca.IdCity = c.IdCity.Value;
                    ca.CityName = c.City.CityName;
                }
                if (c.IdCountry.HasValue)
                {
                    ca.IdCountry = c.IdCountry.Value;
                    ca.CountryName = c.Country.CountryName;
                }
            }
            return ca;
        }

        private Company getCompanyByUserName(string companyUserName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Companies.Where<Company>(c => c.CompanyUserName.Equals(companyUserName)).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validateUserPassword(string CompanyUserName, string Password)
        {
            Company c = getCompanyByUserName(CompanyUserName);
            bool result = false;
            if (c != null)
            {
                string encryptPassword = getEncryptedPassword(Password);
                result = encryptPassword.Equals(c.Password);
            }
            return result;
        }

        private string getDecryptedPassword(string password)
        {
            return CryptorEngine.Decrypt(password, true);
        }

        private string getEncryptedPassword(string password)
        {
            return CryptorEngine.Encrypt(password, true);
        }
    }

    public class CompanyAccount
    {
        public int IdCompany { get; set; }
        public string CompanyName { get; set; }
        public string CompanyUserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int IdCountry { get; set; }
        public string CountryName { get; set; }
        public int IdCity { get; set; }
        public string CityName { get; set; }
        public string WebUrl { get; set; }
    }
}
