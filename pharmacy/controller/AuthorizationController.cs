using pharmacy.data;
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

        public User Authorize(string login, string password)
        {
            if (CheckLoginPassword(login, password) == "")
            {
                return AuthorizationService.AuthorizationUser(login, password);
            }
            return null;
        }

        public string CheckLoginPassword (string login, string password)
        {
            if (login != null && password != null)
            {
                return "";
            }
            else
            {
                return "Заполните все обязательные поля!";
            }
        }
    }
}
