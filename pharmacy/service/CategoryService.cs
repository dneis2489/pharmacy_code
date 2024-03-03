using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy.service
{
    public class CategoryService:IService<List<string>>
    {
        private CategoryService()
        {
            SQLExecutor = new SQLExecutor("Не удалось получить перечень категорий лекарств");
        }
        private static CategoryService instance;

        public static CategoryService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryService();
                }
                return instance;
            }
        }

        public SQLExecutor SQLExecutor { get; }

        //Добавить категорию товаров
        public void Add(string name)
        {
            try
            {
                DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`category`
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

        //Удалить категорию
        public void Delete(int id) 
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`category`
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
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.category;";
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
            string query = "SELECT name FROM pharmacy.category;";
            return SQLExecutor.ExecuteSelectQuery(query, "name");
        }
    }
}
