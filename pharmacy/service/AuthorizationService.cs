using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using pharmacy.data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy
{

    internal class AuthorizationService
    {
        private AuthorizationService()
        {
        }
        private static AuthorizationService instance;

        public static AuthorizationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthorizationService();
                }
                return instance;
            }
        }


        public User AuthorizationUser(string login, string password)
        {
            User user = null;
            try
            {
                DBConnection.command.CommandText =
                    @"SELECT 
                        p.id,
	                    p.name,
		                b.name AS 'role',
		                p.pharmacy_id AS 'pharmacy_id'  
                    FROM 
                        users p
                    JOIN 
                        pharmacy.role b ON p.role_id = b.id
                    WHERE 
                        p.login LIKE '" + login + "' AND p.password LIKE '" + password + "';";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            string role = reader.GetString(reader.GetOrdinal("role"));
                            int pharmacyId = 0;
                            if (role == "Администратор")
                            {
                                pharmacyId = reader.GetInt32(reader.GetOrdinal("pharmacy_id"));
                            }
                             user = new User(id, name, role, pharmacyId);
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Неправильный логин или пароль!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }
            return user;
        }
    }
}
