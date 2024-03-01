using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pharmacy.service
{
    public class UsersService:IService<List<string>>
    {
        public UsersService()
        {
            
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

        //Подгрузка системных ролей
        public List<string> GetAllRoles()
        {
            List<string> result = new List<string>();

            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.role;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetInt32("id").ToString() + ". " + reader.GetString("name").ToString());

                        }
                    }                    
                }
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return result;
        }

        public List<string> GetAll()
        {
            List<string> result = new List<string>();

            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.users;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetInt32("id").ToString() + ". " + reader.GetString("name").ToString());
                        }
                    }                    
                }
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return result;
        }        
    }
}
