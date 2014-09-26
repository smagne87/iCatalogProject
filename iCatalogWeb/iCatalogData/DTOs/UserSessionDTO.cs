using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iCatalogData.DTOs
{
    public class UserSessionDTO
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public UserSessionDTO(iCatalogData.Companies user)
        {
            IdUsuario = user.IdCompany;
            Email = user.Email;
            Name = user.CompanyName;
            UserName = user.CompanyUserName;
        }
    }
}
