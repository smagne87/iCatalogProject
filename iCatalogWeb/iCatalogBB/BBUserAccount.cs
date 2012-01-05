using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository = iCatalogData.iCatalogdbDataContext;
using User = iCatalogData.User;
using System.Security.Cryptography;

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

    internal class CryptorEngine
    {

        public static string Encrypt(string ToEncrypt, bool useHasing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(ToEncrypt);
            //System.Configuration.AppSettingsReader settingsReader = new     AppSettingsReader(); 
            string Key = "iCaTaLog";
            if (useHasing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(Key);
            }
            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = keyArray;
            tDes.Mode = CipherMode.ECB;
            tDes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tDes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tDes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cypherString, bool useHasing)
        {
            byte[] keyArray;
            byte[] toDecryptArray = Convert.FromBase64String(cypherString);
            //byte[] toEncryptArray = Convert.FromBase64String(cypherString); 
            //System.Configuration.AppSettingsReader settingReader = new     AppSettingsReader(); 
            string key = "iCaTaLog";
            if (useHasing)
            {
                MD5CryptoServiceProvider hashmd = new MD5CryptoServiceProvider();
                keyArray = hashmd.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = keyArray;
            tDes.Mode = CipherMode.ECB;
            tDes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tDes.CreateDecryptor();
            try
            {
                byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);

                tDes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
