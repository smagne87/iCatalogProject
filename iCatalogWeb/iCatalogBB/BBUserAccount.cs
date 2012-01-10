using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository = iCatalogData.iCatalogdbDataContext;
using User = iCatalogData.User;

namespace iCatalogBB
{
    public class BBUserAccount
    {
        public bool existsUserName(string UserName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Users.Any<User>(u => u.UserName.Equals(UserName));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool existsEmail(string Email)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Users.Any<User>(u => u.Email.Equals(Email));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void registerUser(UserAccount user)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    User u = new User();
                    u.UserName = user.UserName;
                    u.FirstName = user.FirstName;
                    u.LastName = user.LastName;
                    u.Password = getEncryptedPassword(user.Password);
                    u.IsGeneralAdmin = user.isGeneralAdmin;
                    u.Email = user.Email;
                    r.Users.InsertOnSubmit(u);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validateUserPassword(string UserName, string Password)
        {
            UserAccount ua = getUserAccount(UserName);
            bool result = false;
            if (ua != null)
            {
                string encryptPassword = getEncryptedPassword(Password);
                result = encryptPassword.Equals(ua.Password);
            }
            return result;
        }

        public UserAccount getUserAccountByUserName(string UserName)
        {
            return getUserAccount(UserName);
        }

        private UserAccount getUserAccount(string userName)
        {
            User u = getUserByUserName(userName);
            UserAccount ua = null;
            if (u != null)
            {
                ua = new UserAccount();
                ua.IdUser = u.IdUser;
                ua.UserName = u.UserName;
                ua.Email = u.Email;
                ua.FirstName = u.FirstName;
                ua.LastName = u.LastName;
                ua.Password = u.Password;
                ua.isGeneralAdmin = u.IsGeneralAdmin.HasValue ? (bool)u.IsGeneralAdmin : false;
                if (u.IdCity.HasValue)
                {
                    ua.IdCity = u.IdCity.Value;
                    ua.CityName = u.City.CityName;
                }
                if (u.IdCountry.HasValue)
                {
                    ua.IdCountry = u.IdCountry.Value;
                    ua.CountryName = u.Country.CountryName;
                }
            }
            return ua;
        }

        private User getUserByUserName(string userName)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    return r.Users.Where<User>(ru => ru.UserName.Equals(userName)).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getDecryptedPassword(string password)
        {
            return CryptorEngine.Decrypt(password, true);
        }

        private string getEncryptedPassword(string password)
        {
            return CryptorEngine.Encrypt(password, true);
        }

        public void savePassword(string userName, string newPassword)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    User user = r.Users.Where<User>(ru => ru.UserName.Equals(userName)).SingleOrDefault();
                    user.Password = getEncryptedPassword(newPassword);
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class UserAccount
    {
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdCountry { get; set; }
        public string CountryName { get; set; }
        public int IdCity { get; set; }
        public string CityName { get; set; }
        public bool isGeneralAdmin { get; set; }

    }
}
