using MySql.Data.MySqlClient;
using pharmacy.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy
{
    public class ScheduleService: IService<List<string>>
    {
        public ScheduleService()
        {
        }

        //Добавить график работы
        public void Add(string opening, string ending, string openingOnWeekands, string endingOnWeekands)
        {
            try
            {
                DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`pharmacy_schedule`
                                                        (`opening_time`,
                                                        `ending_time`,
                                                        `opening_time_on_weekands`,
                                                        `ending_time_on_weekands`)
                                                     VALUES
                                                        ('" + opening + @"',
                                                        '" + ending + @"',
                                                        '" + openingOnWeekands + @"',
                                                        '" + endingOnWeekands + @"');
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

        //Удалить график работы
        public void Delete(int id)
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`pharmacy_schedule`
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

        //Подгрузка графиков работ для добавления Магазина
        public List<string> GetAll()
        {
            List<string> result = new List<string>();

            try
            {                
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.pharmacy_schedule;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetInt32("id").ToString() + ". " + reader.GetTimeSpan("opening_time").ToString() + " - " + reader.GetTimeSpan("ending_time").ToString() +
                                " (" + reader.GetTimeSpan("opening_time_on_weekands").ToString() + " - " + reader.GetTimeSpan("ending_time_on_weekands").ToString() + ")");
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
