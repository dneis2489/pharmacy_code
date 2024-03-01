using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy.service
{
    public class StatusService : IService<List<string>>
    {
        public void Add(string name)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAll()
        {
            List<string> result = new List<string>();

            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.status;";
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
