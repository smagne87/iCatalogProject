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
        public int IdCity { get; set; }
        public bool RememberMe { get; set; }
        public bool isGeneralAdmin { get; set; }
        private BBUserAccount _contextUserAccount;

        public UserAccountModel()
        {
            _contextUserAccount = new BBUserAccount();
        }

        internal bool validateUserPassword()
        {
            this.isGeneralAdmin = true;
            return true;
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
    }
}