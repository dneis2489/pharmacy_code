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
            string query =
                DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`category`
                                                        (`name`)
                                                     VALUES
                                                        ('" + name + @"');
                                                        ";
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка добавления значений в базу");
        }

        //Удалить категорию
        public void Delete(int id) 
        {
            string query = @"DELETE FROM `pharmacy`.`category`
                                                     WHERE id = " + id + ";";
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка удаления значений из базы");
        }

        public List<string> GetAll()
        {
            string query = @"SELECT * FROM pharmacy.category;";
            return SQLExecutor.ExecuteSelectQuery(query, "id", "name");
        }


        public List<string> GetAllName()
        {
            string query = "SELECT name FROM pharmacy.category;";
            return SQLExecutor.ExecuteSelectQuery(query, "name");
        }
    }
}
