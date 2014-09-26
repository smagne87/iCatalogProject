using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using iCatalogBB;
using System.Web.Script.Serialization;

namespace iCatalogWS
{
    /// <summary>
    /// Summary description for iCatalogService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class iCatalogService : System.Web.Services.WebService
    {

        [WebMethod]
        public string LoginUser(string userName, string password)
        {
            string[][] contentArray = new string[2][];
            BBUserAccount ua = new BBUserAccount();
            if (ua.validateUserPassword(userName, password))
            {
                UserAccount user = ua.getUserAccountByUserName(userName);
                contentArray[0] = new string[] { "OK" };
                contentArray[1] = new string[] { user.IdUser.ToString(), user.UserName, user.FirstName, user.LastName, user.IdCity.ToString(), user.IdCountry.ToString(), user.CountryName, user.CityName };
            }
            else
            {
                contentArray[0] = new string[] { "NOT OK" };
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = js.Serialize(contentArray);
            return json;
        }
    }
}
