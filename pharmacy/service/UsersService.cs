using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmacy.service
{
    internal class UsersService:IService<string>
    {
        public UsersService(Medicine medicine)
        {
            Medicine = medicine;
        }

        Medicine Medicine { get; set; } // TODO: а зачем?

        public void Add(string name, string birth_day, string phone_number, string login, string password, int role_id, int pharmacy_id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public string GetAll()
        {
            throw new NotImplementedException();
        }        
    }
}
