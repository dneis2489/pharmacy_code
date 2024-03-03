using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy
{
    public class SQLExecutor
    {
        public SQLExecutor(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

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
                MessageBox.Show(ErrorMessage, "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return result;
        }
        
        public void ExecuteSelectQueryWithFill(string query, DataTable dataTable)
        {
            try
            {
                DBConnection.command.CommandText = query;
                dataTable.Clear();
                DBConnection.dataAdapter.SelectCommand = DBConnection.command;
                DBConnection.dataAdapter.Fill(dataTable);
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные. Приносим извинения за предоставленные неудобства!", "Ошибка при получении данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
    }    
}

