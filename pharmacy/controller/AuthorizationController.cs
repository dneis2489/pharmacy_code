using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmacy.controller
{
    internal class AuthorizationController
    {
        private AuthorizationService AuthorizationService;
        
        public AuthorizationController()
        {
            AuthorizationService = new AuthorizationService();
        }

        public string Authorize(string login, string password)
        {
            if (login != null && password != null)
            {
                AuthorizationService.AuthorizationUser(login, password);
                return "";
            }
            else
            {
                return "Заполните все обязательные поля!";
            }
        }
    }
}
