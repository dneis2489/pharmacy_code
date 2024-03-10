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
        private ScheduleService()
        {
            SQLExecutor = new SQLExecutor("Ошибка подключения к базе с аптеками");
        }
        private static ScheduleService instance;

        public static ScheduleService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScheduleService();
                }
                return instance;
            }
        }

        private SQLExecutor SQLExecutor;

        //Добавить график работы
        public void Add(string opening, string ending, string openingOnWeekands, string endingOnWeekands)
        {
            string query = @"INSERT INTO `pharmacy`.`pharmacy_schedule`
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
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка добавления значений в базу");
        }

        //Удалить график работы
        public void Delete(int id)
        {
            string query = @"DELETE FROM `pharmacy`.`pharmacy_schedule`
                                                     WHERE id = " + id + ";";
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка удаления значений из базы");
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
                MessageBox.Show("Не удалось получить графики работ!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return result;
        }
    }
}
