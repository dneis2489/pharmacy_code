using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy.service
{
    public class PharmacyService:IService<List<string>>
    {
        //Добавить аптеку
        public void Add(string name, string adress, string phone_number, int pharmacy_schedule)
        {
            try
            {
                DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`pharmacy`
                                                        (`name`,
                                                        `address`,
                                                        `phone_number`,
                                                        `pharmacy_schedule_id`)
                                                     VALUES
                                                        ('" + name + @"',
                                                        '" + adress + @"',
                                                        '" + phone_number + @"',
                                                        " + pharmacy_schedule + @");
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

        //Удалить аптеку
        public void Delete(int id) 
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`pharmacy`
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

        //Подгрузка магазинов для добавления пользователя
        public List<string> GetAll()
        {
            List<string> result = new List<string>();

            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.pharmacy;";
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


        public List<string> GetAllName()
        {
            throw new NotImplementedException();
        }

    }
}
