using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy.service
{
    public class PharmacyService:IService<List<string>>
    {
        private PharmacyService()
        {
            SQLExecutor = new SQLExecutor("Не удалось получить перечень аптек!");
        }
        private static PharmacyService instance;

        public static PharmacyService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PharmacyService();
                }
                return instance;
            }
        }


        private SQLExecutor SQLExecutor;        

        //Добавить аптеку
        public void Add(string name, string adress, string phone_number, int pharmacy_schedule)
        {
            string query = @"INSERT INTO `pharmacy`.`pharmacy`
                                                        (`name`,
                                                        `address`,
                                                        `phone_number`,
                                                        `pharmacy_schedule_id`)
                                                     VALUES
                                                        ('" + name + @"',
                                                        '" + adress + @"',
                                                        '" + phone_number + @"',
                                                        " + pharmacy_schedule + @");
                                                     ";
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка добавления значений в базу");
        }

        //Удалить аптеку
        public void Delete(int id) 
        {
            string query = @"DELETE FROM `pharmacy`.`pharmacy`
                                                     WHERE id = " + id + ";";
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка удаления значений из базы");
        }

        //Подгрузка магазинов для добавления пользователя
        public List<string> GetAll()
        {
            string query = @"SELECT * FROM pharmacy.pharmacy;";
            return SQLExecutor.ExecuteSelectQuery(query,"id", "name");
        }


        public List<string> GetAllName()
        {
            string query = @"SELECT name FROM pharmacy.pharmacy;";
            return SQLExecutor.ExecuteSelectQuery(query, "name");
        }

    }
}
