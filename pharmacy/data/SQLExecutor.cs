using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
        public List<string> ExecuteSelectQuery(string query, params string[] returnedField)
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
                            string res = "";
                            foreach (var filed in returnedField)
                            {
                                if (returnedField.Length > 1 && Array.IndexOf(returnedField, filed) != returnedField.Length-1)
                                {
                                    res += reader.GetString(filed) + ". ";
                                }
                                else
                                {
                                    res += reader.GetString(filed);
                                }
                            }
                            result.Add(res);
                            //result.Add(reader.GetString(returnedField));
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

        public List<string> ExecuteSelectQueryWithError(string query, string error, params string[] returnedField)
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
                            string res = "";
                            foreach (var filed in returnedField)
                            {
                                if (returnedField.Length > 1 && Array.IndexOf(returnedField, filed) != returnedField.Length - 1)
                                {
                                    res += reader.GetString(filed) + ". ";
                                }
                                else
                                {
                                    res += reader.GetString(filed);
                                }
                            }
                            result.Add(res);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(error.ToString(), "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public void ExecuteInsertOrDelete(string query, string error)
        {
            try
            {
                DBConnection.command.CommandText = query;
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(ErrorMessage, "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public DataTable ExecuteQueryWithNewData(string query, string firstColumn, string lastColumn, DataTable dataTable)
        {
            try
            {
                DBConnection.command.CommandText = query;

                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataTable.Rows.Add(reader.GetDateTime(firstColumn), reader.GetInt32(lastColumn));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось получить данные. Приносим извинения за предоставленные неудобства!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }
            return dataTable;
        }
    }    
}

