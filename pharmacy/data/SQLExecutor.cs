using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy
{
    public class SQLExecutor
    {

        /// <summary>
        /// Выполняет select запрос с одним возвращаемым полем 
        /// </summary>      
        public List<string> ExecuteSelectQuery(string query, string returnedField)
        {
            List<string> result = new List<string>();

            try
            {
                DBConnection.command.CommandText = query;
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(returnedField));
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

