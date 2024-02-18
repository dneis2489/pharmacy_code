using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
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
        static public string name, role;
        static public int id, pharmacy_id;

        static public void AuthorizationUser(string login, string password)
        {
            try
            {
                DBConnection.command.CommandText =
                    @"SELECT 
                        p.id,
                        p.name,
                        b.name AS 'role',
                        COALESCE(ph.pharmacy_id, '') AS 'pharmacy_id' 
                    FROM 
                        users p
                    JOIN 
                        pharmacy.role b ON p.role_id = b.id
                    LEFT JOIN 
                        pharmacy.pharmacy_has_users ph ON p.id = ph.users_id
                    WHERE 
                        p.login LIKE '" + login + "' AND p.password LIKE '" + password + "';";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id"));
                            name = reader.GetString(reader.GetOrdinal("name"));
                            role = reader.GetString(reader.GetOrdinal("role"));
                            if (role == "Администратор")
                            {
                                pharmacy_id = reader.GetInt32(reader.GetOrdinal("pharmacy_id"));
                            }
                            
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
        }
    }
}
