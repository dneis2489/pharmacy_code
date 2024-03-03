using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace pharmacy.data
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int PharmacyId { get; set; }

        public User(int userId, string name, string role, int pharmacyId)
        {
            UserId = userId;
            Name = name;
            Role = role;
            PharmacyId = pharmacyId;
        }
    }
}
