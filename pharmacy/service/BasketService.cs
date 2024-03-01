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
    internal class BasketService
    {
        public static string date;
        public static int adressId, number_basket, medicineId, pharmacyId, countInPharmacy;
        public static bool equalDate = false;
        public static int id, supId, pharmId = 0;
        static public DataTable dtBasket = new DataTable();



        public static string OrerDate(int[] id, string adress) //Расчет даты доставки
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

        public static void AddBasketInDB(List<UserForm.medicine> med, string adress, string date, string basketNumber) //Добавление заказа в БД
        {
            int[] medCount = new int[med.Count];
            DateTime dateNow = DateTime.Now;

            equalDate = dateNow.ToString("dd.MM.yyyy") == date ? true : false;
            DateTime originalDate = DateTime.ParseExact(date, "dd.MM.yyyy", null);
            date = originalDate.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                DBConnection.command.CommandText =
                     @"SELECT MAX(id) FROM pharmacy.basket";
                Object result = DBConnection.command.ExecuteScalar();
                if (result == null)
                {
                    id = 1;
                }
                else
                {
                    id = Int32.Parse(result.ToString()) + 1;
                }

                supId = id;
                int i = 0;
                foreach (var obj in med)
                {
                    DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`basket`
                                                            (`id`,
                                                            `name`,
                                                            `costs`,
                                                            `on_prescription`,
                                                            `expiration_date`,
                                                            `volume`,
                                                            `packaged`,
                                                            'units_of_measurement',
                                                            `active_substance`,
                                                            `special_properties`,
                                                            `release_form`)
                                                         VALUES
                                                            (" + id + @",
                                                            '"+ obj.name + @"',
                                                            "+ obj.costs + @",
                                                            "+ (obj.on_prescription == "По рецепту" ? 1 : 0) + @",
                                                            '"+ obj.best_before_date + @"',
                                                            "+ obj.volume + @",
                                                            "+ obj.volume + @",  
                                                            '"+ obj.primary_packaging + @"',
                                                            '"+ obj.active_substance + @"',
                                                            '"+ obj.special_properties + @"',
                                                            '"+ obj.release_form + @"');";
                    if (DBConnection.command.ExecuteNonQuery() < 0)
                    {
                        MessageBox.Show("Ошибка добавления значений в базу basket_has_users", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    id++;
                    medCount[i] = obj.count;
                    i++;
                }

                DBConnection.command.CommandText =
                     @"SELECT id FROM pharmacy.pharmacy WHERE name like '"+ adress +"'";
                result = DBConnection.command.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show("Аптека с таким id не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    pharmId = Int32.Parse(result.ToString()) + 1;
                }

                for (i = 0; i < medCount.Length; i++)
                {
                    DBConnection.command.CommandText = @"INSERT INTO `pharmacy`.`basket_has_users`
                                                            (`basket_id`,
                                                            `users_id`,
                                                            `basket_number`,
                                                            `date`,
                                                            `status_id`,
                                                            `count`,
                                                            `pharmacy_id`)
                                                         VALUES
                                                            (" + supId + @",
                                                            " + AuthorizationService.id +@",
                                                            " + basketNumber + @",
                                                            '" + date + @"',
                                                            " + (equalDate ? 2 : 1) + @",
                                                            "+ medCount[i] + @",
                                                            "+ pharmId + ");";
                    if(DBConnection.command.ExecuteNonQuery() < 0)
                    {
                        MessageBox.Show("Ошибка добавления значений в базу basket_has_users", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    supId++;
                }

            }
            catch 
            { 

            }
        }

        public static void GetBasketMedicines(int id) //Получение истории заказов
        {
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                     SELECT 
                                                        m.id,
                                                        m.name AS 'Наименование:',
                                                        m.costs AS 'Стоимость:',
                                                        CASE WHEN m.on_prescription = 0 THEN 'Не требуется' ELSE 'Требуется' END AS 'Рецепт:',
                                                        m.expiration_date AS 'Срок годности:',
                                                        m.volume AS 'Объем:',
                                                        m.primary_packaging AS 'Первичная упаковка:',
                                                        m.active_substance AS 'Активное вещество:',
                                                        m.special_properties AS 'Специальные свойства:',
                                                        m.release_form AS 'Форма выпуска:',
                                                        f.name AS 'Производитель:',
                                                        b.count AS 'Количество:'
                                                     FROM pharmacy.medicines m
                                                     JOIN medicine_factory f on f.id = m.medicine_factory_id
                                                     JOIN basket_has_users b on b.basket_id = m.id
                                                     where b.basket_number = " + id +";";
                dtBasket.Clear();
                DBConnection.dataAdapter.SelectCommand = DBConnection.command;
                DBConnection.dataAdapter.Fill(dtBasket);
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные. Приносим извинения за предоставленные неудобства!", "Ошибка при получении данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        string GetOrdersInfosByPharmacyId(int id) 
        {
            throw new NotImplementedException();
        }

        string UpdateStatusByNameAndBasketNumber(int number, string name) 
        {
            throw new NotImplementedException();
        }

    }

}
