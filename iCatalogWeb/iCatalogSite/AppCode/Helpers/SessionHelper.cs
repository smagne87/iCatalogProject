using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Diagnostics;
using iCatalogData.DTOs;

namespace iCatalogSite
{
    public class SessionHelper
    {
        private const string REMEMBERME_COOKIE = "REMEMBERME_COOKIE";

        public static bool IsSignedIn
        {
            [DebuggerStepThrough]
            get
            {
                if (UserSession != null)
                    return !string.IsNullOrEmpty(UserSession.Email);

                return false;
            }
        }

        private static HttpSessionState currentSession
        {
            [DebuggerStepThrough]
            get
            {
                if (HttpContext.Current.Session == null)
                    throw new Exception("Session is not available in the current context.");
                else
                    return HttpContext.Current.Session;
            }
        }

        public static UserSessionDTO UserSession
        {
            get
            {
                return (UserSessionDTO)currentSession["UserSession"];
            }
            set
            {
                currentSession["UserSession"] = value;
            }
        }

        public static void RemoveRememberMe()
        {
            ForgetUser();
        }
        
        private static void ForgetUser()
        {
            HttpCookie cookieLoguin = RememberMe_Cookie;
            if (cookieLoguin != null)
            {
                HttpCookie expireCookie = new HttpCookie(REMEMBERME_COOKIE);
                expireCookie.Expires = DateTime.Now.AddDays(-1);
                RememberMe_Cookie = expireCookie;
            }
        }

        public static void RememberMe()
        {
            HttpCookie cookie = new HttpCookie(REMEMBERME_COOKIE, SessionHelper.UserSession.IdUsuario.ToString());
            cookie.Expires = DateTime.Now.AddDays(20);
            SessionHelper.RememberMe_Cookie = cookie;
        }

        public static HttpCookie RememberMe_Cookie
        {
            get
            {
                return HttpContext.Current.Request.Cookies[REMEMBERME_COOKIE];
            }
            set
            {
                HttpContext.Current.Response.Cookies.Remove(REMEMBERME_COOKIE);
                HttpContext.Current.Response.Cookies.Add(value);
            }
        }
    }
}