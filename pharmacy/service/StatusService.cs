using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pharmacy.service
{
    public class StatusService : IService<List<string>>
    {
        private StatusService()
        {
            SQLExecutor = new SQLExecutor("Ошибка подключения к базе с аптеками");
        }
        private static StatusService instance;

        public static StatusService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StatusService();
                }
                return instance;
            }
        }


        SQLExecutor SQLExecutor { get; }

        //Добавить статус
        public void Add(string name)
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
                MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Delete(int id) //Удалить статус
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`status`
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
            string query = @"SELECT name FROM pharmacy.status;";
            return SQLExecutor.ExecuteSelectQuery(query, "name");
        }
    }
}
