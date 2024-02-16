using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy
{
    internal class RootAddData
    {
        public static void AddSchedule(string opening, string ending, string openingOnWeekands, string endingOnWeekands) //Добавить график работы
        {
            try
            {
                DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`pharmacy_schedule`
                                                        (`opening_time`,
                                                        `ending_time`,
                                                        `opening_time_on_weekands`,
                                                        `ending_time_on_weekands`)
                                                     VALUES
                                                        ('"+opening+@"',
                                                        '"+ending+@"',
                                                        '"+openingOnWeekands+@"',
                                                        '"+endingOnWeekands+@"');
                                                     ";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка добавления значений в базу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch 
            { 

            }
        }

        public static void AddPharmacy(string name, string adress, string phone_number, int pharmacy_schedule) //Добавить аптеку
        {
            try
            {
                DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`pharmacy`
                                                        (`name`,
                                                        `address`,
                                                        `phone_number`,
                                                        `pharmacy_schedule_id`)
                                                     VALUES
                                                        ('"+ name + @"',
                                                        '"+ adress + @"',
                                                        '"+ phone_number + @"',
                                                        "+ pharmacy_schedule + @");
                                                     ";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка добавления значений в базу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }

        public static void AddCategory(string name) //Добавить категорию товаров
        {
            try
            {
                DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`category`
                                                        (`name`)
                                                     VALUES
                                                        ('"+name+@"');
                                                        ";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка добавления значений в базу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }

        public static void AddStatus(string name) //Добавить статус
        {
            try
            {
                DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`status`
                                                        (`name`)
                                                     VALUES
                                                        ('" + name + @"');
                                                        ";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка добавления значений в базу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }

        public static void AddUser(string name, string birth_day, string phone_number, string login, string password, int role_id, int pharmacy_id) //Добавить пользователя
        {
            DateTime originalDate = DateTime.ParseExact(birth_day, "dd.MM.yyyy", null);
            birth_day = originalDate.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                if (pharmacy_id != 0)
                {
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
                                                         " + pharmacy_id + @");
                                                         ";
                    if (DBConnection.command.ExecuteNonQuery() < 0)
                    {
                        MessageBox.Show("Ошибка добавления значений в базу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
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
                                                         " + null + @");
                                                         ";
                    if (DBConnection.command.ExecuteNonQuery() < 0)
                    {
                        MessageBox.Show("Ошибка добавления значений в базу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                
            }
            catch
            {

            }
        }
    }
}
