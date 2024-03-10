using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pharmacy.service
{
    public class StatusService : IService<List<string>>
    {
        private StatusService()
        {
            SQLExecutor = new SQLExecutor("Ошибка получения перечня статусов");
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
            string query = @"INSERT INTO `pharmacy`.`status`
                                                        (`name`)
                                                     VALUES
                                                        ('" + name + @"');
                                                        ";
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка добавления значений в базу");
        }

        public void Delete(int id) //Удалить статус
        {
            string query = @"DELETE FROM `pharmacy`.`status`
                                                     WHERE id = " + id + ";";
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка удаления значений из базы");
        }

        public List<string> GetAll()
        {
            string query = @"SELECT * FROM pharmacy.status;";
            return SQLExecutor.ExecuteSelectQuery(query, "id", "name");
        }


        public List<string> GetAllName()
        {
            string query = @"SELECT name FROM pharmacy.status;";
            return SQLExecutor.ExecuteSelectQuery(query, "name");
        }
    }
}
