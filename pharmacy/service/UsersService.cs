using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pharmacy.service
{
    public class UsersService:IService<List<string>>
    {
        public UsersService()
        {
            
        }

        Medicine Medicine { get; set; } // TODO: а зачем?

        //Добавить пользователя
        public void AddUser(string name, string birth_day, string phone_number, string login, string password, int role_id, int pharmacy_id)
        {
            DateTime originalDate = DateTime.ParseExact(birth_day, "dd.MM.yyyy", null);
            birth_day = originalDate.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                object pharmacy_id_obj = pharmacy_id;

                if (pharmacy_id == 0)
                {
                    pharmacy_id_obj = null;
                }

                DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`users`
                                                         (`name`,
                                                         `birth_day`,
                                                         `phone_number`,
                                                         `login`,
                                                         `password`,
                                                         `role_id`,
                                                         `pharmacy_id`)
                                                     VALUES
                                                         ('" + name + @"',
                                                         '" + birth_day + @"',
                                                         '" + phone_number + @"',
                                                         '" + login + @"',
                                                         '" + password + @"',
                                                         " + role_id + @",
                                                         " + pharmacy_id_obj + @");
                                                         ";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка добавления значений в базу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch
            {
                MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Удалить пользователя
        public void Delete(int id)
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`users`
                                                     WHERE id = " + id + ";";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка удаления значений из базы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
