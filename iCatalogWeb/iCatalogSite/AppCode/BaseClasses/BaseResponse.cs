using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iCatalogSite
{
    public class BaseResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
        //public ViewResult View { get; set; }

        public BaseResponse()
        {
            Succeeded = true;
            Message = string.Empty;
            RedirectUrl = string.Empty;
        }

        public BaseResponse(bool succeeded = true, string message = "", string redirectUrl = "")
        {
            Succeeded = succeeded;
            Message = message;
            RedirectUrl = redirectUrl;
            //View = view;
        }
    }

    public class BaseResponse<T> : BaseResponse where T : new()
    {
        public T Data { get; set; }

        public BaseResponse()
            : base()
        {
            Data = new T();
        }

        public BaseResponse(bool succeeded, string message, string redirectUrl, T data = default(T))
            : base(succeeded, message, redirectUrl)
        {
            if (Equals(data, default(T)))
                Data = new T();
            else
                Data = data;
        }
    }
}
