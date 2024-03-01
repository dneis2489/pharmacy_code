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

        public void Add(string opening, string ending, string openingOnWeekands, string endingOnWeekands)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
