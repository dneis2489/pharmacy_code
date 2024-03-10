using MySql.Data.MySqlClient;
using OfficeOpenXml;
using pharmacy.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Windows.Forms;

namespace pharmacy
{
    public class BasketService
    {
        private BasketService()
        {
            dataColumns = new List<string>() {
                "Наименование:", "Стоимость:", "Количество:", "Рецепт:",
                "Срок годности:", "Объём:", "Первичная упаковка:",
                "Активное вещество:", "Специальные свойства:", "Форма выпуска:",
                "Производитель:" };

            orderDataColumns = new List<string>() {
                "Наименование:", "Стоимость:", "Количество:", "Рецепт:",
                "Срок годности:", "Объём:", "Первичная упаковка:",
                "Активное вещество:", "Специальные свойства:", "Форма выпуска:",
                "Производитель:", "Информация по заказу:" };
        }
        private static BasketService instance;

        public static BasketService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BasketService();
                }
                return instance;
            }
        }

        public static string date;
        public static int adressId, number_basket, medicineId, pharmacyId, countInPharmacy;
        public static bool equalDate = false;
        public static int id, supId, pharmId = 0;
        public DataTable dtBasket = new DataTable();
        public List<string> dataColumns;
        public List<string> orderDataColumns;


        //TODO: разбить метод мб
        public string OrerDate(int[] id, string adress) //Расчет даты доставки
        {
            DBConnection.command.CommandText =
                   @"SELECT id FROM pharmacy.pharmacy
                     where name like '"+ adress +"'";
            Object result = DBConnection.command.ExecuteScalar();
            if (result == null)
            {
                MessageBox.Show("Аптека с таким названием не найдена!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                adressId = Int32.Parse(result.ToString());
            }

            DBConnection.command.CommandText =
                   @"SELECT * FROM pharmacy.medicines_has_pharmacy
                     WHERE pharmacy_id = "+ adressId;

            using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        medicineId = reader.GetInt32(reader.GetOrdinal("medicines_id"));
                        pharmacyId = reader.GetInt32(reader.GetOrdinal("pharmacy_id"));
                        countInPharmacy = reader.GetInt32(reader.GetOrdinal("count"));
                        if (id.Contains(medicineId))
                        {
                            int[] newID = id.Where(x => x != medicineId).ToArray();
                            id = newID;
                        }
                    }
                }
                else
                {
                    
                }
            }
            if (!id.Any())
            {
                DateTime dateNow = DateTime.Now;
                date = dateNow.ToString("dd.MM.yyyy");
            }
            else
            {
                DBConnection.command.CommandText =
                   @"SELECT * FROM pharmacy.medicines_has_pharmacy
                     WHERE pharmacy_id != " + adressId;

                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            medicineId = reader.GetInt32(reader.GetOrdinal("medicines_id"));
                            pharmacyId = reader.GetInt32(reader.GetOrdinal("pharmacy_id"));
                            countInPharmacy = reader.GetInt32(reader.GetOrdinal("count"));
                            if (id.Contains(medicineId))
                            {
                                int[] newID = id.Where(x => x != medicineId).ToArray();
                                id = newID;
                            }
                        }
                    }
                }
                if (!id.Any())
                {
                    DateTime dateNow = DateTime.Now;
                    DateTime date1 = dateNow.AddDays(1);
                    date = date1.ToString("dd.MM.yyyy");
                }
                else
                {
                    DateTime dateNow = DateTime.Now;
                    DateTime date1 = dateNow.AddDays(5);
                    date = date1.ToString("dd.MM.yyyy");
                }
            }
            return date;
        }

        public static int BasketNumber() //Получение номера заказа
        {
            DBConnection.command.CommandText =
                    @"SELECT MAX(basket_number) FROM pharmacy.basket_has_users";
            Object result = DBConnection.command.ExecuteScalar();
            if (result == null)
            {
                number_basket = 1;
            }
            else
            {
                number_basket = Int32.Parse(result.ToString()) + 1;
            }
            return number_basket;
        }

        public void AddBasketInDB(BindingList<Medicine> med, string adress, string date, string basketNumber, int userId) //Добавление заказа в БД
        {
            int[] medCount = new int[med.Count];
            DateTime dateNow = DateTime.Now;

            equalDate = dateNow.ToString("dd.MM.yyyy") == date ? true : false;
            DateTime originalDate = DateTime.ParseExact(date, "dd.MM.yyyy", null);
            date = originalDate.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {

                DBConnection.command.CommandText =
                     @"SELECT id FROM pharmacy.pharmacy WHERE name like '" + adress + "'";
                Object result = DBConnection.command.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show("Аптека с таким id не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    pharmId = Int32.Parse(result.ToString()) + 1;
                }

                foreach (Medicine medicine in med)
                {
                    DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`basket_has_users`
                                                            (`date`,
                                                            `count`,
                                                            `costs`,
                                                            `pharmacy_id`,
                                                            `basket_number`,
                                                            `users_id`,
                                                            `status_id`,
                                                            `medicines_id`)
                                                         VALUES
                                                            ('" + date + @"',
                                                            " + medicine.Count + @",
                                                            " + medicine.Costs + @",
                                                            " + pharmId + @",
                                                            " + basketNumber + @",
                                                            " + userId + @",
                                                            " + (equalDate ? 2 : 1) + @",
                                                            " + medicine.Id + ");";
                    if (DBConnection.command.ExecuteNonQuery() <= 0)
                    {
                        MessageBox.Show("Ошибка добавления значений в базу basket_has_users", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }
        }

        public void GetBasketMedicines(int id) //Получение истории заказов
        {
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                     SELECT 
                                                        m.id,
                                                        m.name AS 'Наименование:',
                                                        m.costs AS 'Стоимость:',
                                                        b.count AS 'Количество:',
                                                        CASE WHEN m.on_prescription = 0 THEN 'Не требуется' ELSE 'Требуется' END AS 'Рецепт:',
                                                        m.expiration_date AS 'Срок годности:',
                                                        concat(m.volume, ' ' ,m.units_of_measurement) AS 'Объём:',
                                                        m.primary_packaging AS 'Первичная упаковка:',
                                                        m.active_substance AS 'Активное вещество:',
                                                        m.special_properties AS 'Специальные свойства:',
                                                        m.release_form AS 'Форма выпуска:',
                                                        f.name AS 'Производитель:'
                                                     FROM pharmacy.medicines m
                                                     JOIN medicine_factory f on f.id = m.medicine_factory_id
                                                     JOIN basket_has_users b on b.medicines_id = m.id
                                                     where b.basket_number = " + id + ";";
                dtBasket.Clear();
                DBConnection.dataAdapter.SelectCommand = DBConnection.command;
                DBConnection.dataAdapter.Fill(dtBasket);
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные по заказу. Приносим извинения за предоставленные неудобства!", "Ошибка при получении данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public List<string> GetOrdersInfosByPharmacyId(int id) 
        {
            List<string> result = new List<string>();

            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                     SELECT DISTINCT 
                                                        b.basket_number,
                                                        u.name AS user,
                                                        s.name AS status_id, 
                                                        b.date
                                                     FROM pharmacy.basket_has_users b
                                                     JOIN users u on b.users_id = u.id
                                                     JOIN status s on b.status_id = s.id
                                                     JOIN pharmacy p on b.pharmacy_id = p.id
                                                     WHERE p.id = " + id;
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add("Номер заказа: " + reader.GetString(reader.GetOrdinal("basket_number")) + "\r\n Дата доставки: " + reader.GetString(reader.GetOrdinal("date"))
                                + "\r\n Статус заказа: " + reader.GetString(reader.GetOrdinal("status_id")) + "\r\n Имя заказчика: " + reader.GetString(reader.GetOrdinal("user")) + "\r\n");                            
                        }
                    }                    
                }
            }
            catch
            {
                MessageBox.Show("Не удалось получить историю заказов!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return result;
        }

        public void UpdateStatusByNameAndBasketNumber(string number, string name) 
        {
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                         UPDATE `pharmacy`.`basket_has_users`
                                                         SET
                                                            `status_id` = (SELECT id FROM status WHERE name like '"  + name + @"') 
                                                         WHERE basket_number = " + number + ";";
                DBConnection.command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Не удалось обновить статус заказа!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public List<string> GetOrdersInfosByUserId(int userId)
        {
            List<string> result = new List<string>();

            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                         SELECT DISTINCT 
                                                           b.basket_number, 
                                                           s.name AS status_id, 
                                                           b.date, 
                                                           p.name AS pharmacy_id
                                                         FROM pharmacy.basket_has_users b
                                                         JOIN status s on b.status_id = s.id
                                                         JOIN pharmacy p on b.pharmacy_id = p.id
                                                         WHERE users_id = " + userId + ";";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add("Номер заказа: " + reader.GetString(reader.GetOrdinal("basket_number")) + "\r\nДата доставки: " + reader.GetString(reader.GetOrdinal("date"))
                                + "\r\nСтатус заказа: " + reader.GetString(reader.GetOrdinal("status_id")) + "\r\nАдрес доставки: " + reader.GetString(reader.GetOrdinal("pharmacy_id")));
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось получить историю заказов!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return result;
        }

    }
}
