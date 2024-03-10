using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pharmacy.service
{
    public class UsersService:IService<List<string>>
    {
        private UsersService()
        {
            SQLExecutor = new SQLExecutor("Ошибка получения данных по пользователю");
        }
        private static UsersService instance;

        public static UsersService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UsersService();
                }
                return instance;
            }
        }

        private SQLExecutor SQLExecutor;

        //Добавить пользователя
        public void AddUser(string name, string birth_day, string phone_number, string login, string password, int role_id, int pharmacy_id)
        {
            DateTime originalDate = DateTime.ParseExact(birth_day, "dd.MM.yyyy", null);
            birth_day = originalDate.ToString("yyyy-MM-dd");
            string pharmacy_id_obj = pharmacy_id.ToString();

            if (role_id != 2)
            {
                pharmacy_id_obj = "null";
            }
            string query = @"INSERT INTO `pharmacy`.`users`
                                                         (`name`,
                                                         `birth_day`,
                                                         `phone_number`,
                                                         `login`,
                                                         `password`,
                                                         `role_id`,
                                                         `pharmacy_id`)
                                                     VALUES
                                                         ('" + name + @"',
                                                         '" + birth_day + @"',
                                                         '" + phone_number + @"',
                                                         '" + login + @"',
                                                         '" + password + @"',
                                                         " + role_id + @",
                                                         " + pharmacy_id_obj + @");";
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка добавления значений в базу");
        }

        //Удалить пользователя
        public void Delete(int id)
        {
            string query = @"DELETE FROM `pharmacy`.`users`
                                                     WHERE id = " + id + ";";
            SQLExecutor.ExecuteInsertOrDelete(query, "Ошибка удаления значений из базы");
        }

        //Подгрузка системных ролей
        public List<string> GetAllRoles()
        {
            string query = @"SELECT * FROM pharmacy.role;";
            return SQLExecutor.ExecuteSelectQuery(query, "id", "name");
        }

        public List<string> GetAll()
        {
            string query = @"SELECT * FROM pharmacy.users;";
            return SQLExecutor.ExecuteSelectQuery(query, "id", "name");
        }       
    }
}
