using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iCatalogBB;

namespace iCatalogSite.Models
{
    public class UserAccountModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdCountry { get; set; }
        public string CountryName { get; set; }
        public int IdCity { get; set; }
        public string CityName { get; set; }
        public bool RememberMe { get; set; }
        public bool isGeneralAdmin { get; set; }
        public string NewPassword { get; set; }
        public int IdUser { get; set; }

        private BBUserAccount _contextUserAccount;

        public UserAccountModel()
        {
            _contextUserAccount = new BBUserAccount();
        }

        internal bool validateUserPassword()
        {
            bool result = false;
            if (_contextUserAccount.validateUserPassword(UserName, Password))
            {
                result = true;
                GetUserAccount();
            }
            return result;
        }

        private void GetUserAccount()
        {
            UserAccount user = _contextUserAccount.getUserAccountByUserName(UserName);
            IdUser = user.IdUser;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Email = user.Email;
            isGeneralAdmin = user.isGeneralAdmin;
            IdCountry = user.IdCountry;
            CountryName = user.CountryName;
            IdCity = user.IdCity;
            CityName = user.CityName;
        }

        internal bool existsUserName()
        {
            return _contextUserAccount.existsUserName(UserName);
        }

        internal bool existsEmail()
        {
            return _contextUserAccount.existsEmail(Email);
        }

        internal void register()
        {
            UserAccount user = new UserAccount();
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Password = Password;
            user.UserName = UserName;
            user.Email = Email;
            user.isGeneralAdmin = false;
            _contextUserAccount.registerUser(user);
        }

        internal void SavePassword()
        {
            _contextUserAccount.savePassword(UserName, NewPassword);
        }

        internal void saveData()
        {
            _contextUserAccount.saveData(IdUser, FirstName, LastName, IdCity, IdCountry);
        }
    }
}