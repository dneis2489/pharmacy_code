using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Data;

namespace pharmacy
{
    public class StatisticsService
    {
        private StatisticsService()
        {
            dtStat = new DataTable();
            dtStat2 = new DataTable();
        }
        private static StatisticsService instance;

        public static StatisticsService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StatisticsService();
                }
                return instance;
            }
        }

        static public DataTable dtStat;
        static public DataTable dtStat2;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //СТАТИСТИКА ДЛЯ АДМИНА
        public DataTable AdminGetCountBuyMedicinesStat(int pharmacyId) //Количество купленного товара в магазине для Админа
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
            dataTable.Columns.Add("Quantity", typeof(int));

            string[,] data = { { }, { } };

            try
            {
                DBConnection.command.CommandText =
                   @"USE pharmacy;    
                     SELECT 
                         DATE_FORMAT(date, '%Y-%m-01') AS PurchaseMonth,
                         SUM(count) AS TotalCount
                     FROM 
                         basket_has_users
                     WHERE pharmacy_id = " + pharmacyId + @"
                     GROUP BY 
                         DATE_FORMAT(date, '%Y-%m-01') 
                     ORDER BY 
                         PurchaseMonth;";

                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataTable.Rows.Add(reader.GetDateTime("PurchaseMonth"), reader.GetInt32("TotalCount"));
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
        public DataTable AdminGetCountBasketStat(int pharmacyId) //Количество покупок в магазине для Админа
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
            dataTable.Columns.Add("Quantity", typeof(int));

            string[,] data = { { }, { } };

            try
            {
                DBConnection.command.CommandText =
                   @"USE pharmacy;    
                     SELECT 
                         DATE_FORMAT(date, '%Y-%m-01') AS PurchaseMonth,
                         COUNT(distinct(basket_number)) AS TotalCount
                     FROM 
                         basket_has_users
                     WHERE pharmacy_id = " + pharmacyId + @"
                     GROUP BY 
                         DATE_FORMAT(date, '%Y-%m-01') 
                     ORDER BY 
                         PurchaseMonth;";

                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataTable.Rows.Add(reader.GetDateTime("PurchaseMonth"), reader.GetInt32("TotalCount"));
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
        public void getTopUsersInPharmacy(int pharmacyId) //Рейтинг покупателей для Админа
        {
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                    SELECT 
	                                                    u.name AS 'ФИО', 
                                                        COUNT(distinct(b.basket_number)) as 'Количество заказов'
                                                    FROM basket_has_users b
                                                    JOIN users u on b.users_id = u.id
                                                    where b.pharmacy_id = " + pharmacyId + @"
                                                    GROUP BY users_id
                                                    ORDER BY 'Количество заказов' DESC;";
                dtStat.Clear();
                dtStat.Columns.Clear();
                DBConnection.dataAdapter.SelectCommand = DBConnection.command;
                DBConnection.dataAdapter.Fill(dtStat);
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные. Приносим извинения за предоставленные неудобства!", "Ошибка при получении данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //СТАТИСТИКА ДЛЯ СУПЕР-ПОЛЬЗОВАТЕЛЯ
        public DataTable RootGetCountBuyMedicinesStat() //Количество проданной продукции
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
            dataTable.Columns.Add("Quantity", typeof(int));

            string[,] data = { { }, { } };

            try
            {
                DBConnection.command.CommandText =
                   @"USE pharmacy;    
                     SELECT 
                         DATE_FORMAT(date, '%Y-%m-01') AS PurchaseMonth,
                         SUM(count) AS TotalCount
                     FROM 
                         basket_has_users
                     GROUP BY 
                         DATE_FORMAT(date, '%Y-%m-01') 
                     ORDER BY 
                         PurchaseMonth;";

                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataTable.Rows.Add(reader.GetDateTime("PurchaseMonth"), reader.GetInt32("TotalCount"));
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
        public DataTable RootGetCountBasketStat() //Количество заказов
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
            dataTable.Columns.Add("Quantity", typeof(int));

            string[,] data = { { }, { } };

            try
            {
                DBConnection.command.CommandText =
                   @"USE pharmacy;    
                     SELECT 
                         DATE_FORMAT(date, '%Y-%m-01') AS PurchaseMonth,
                         COUNT(distinct(basket_number)) AS TotalCount
                     FROM 
                         basket_has_users
                     GROUP BY 
                         DATE_FORMAT(date, '%Y-%m-01') 
                     ORDER BY 
                         PurchaseMonth;";

                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataTable.Rows.Add(reader.GetString("PurchaseMonth"), reader.GetInt32("TotalCount"));
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
        public DataTable RootGetRevenueByMonth() //Доходы
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
            dataTable.Columns.Add("Quantity", typeof(int));

            string[,] data = { { }, { } };

            try
            {
                DBConnection.command.CommandText =
                   @"USE pharmacy;    
                     SELECT 
                         DATE_FORMAT(date, '%Y-%m') AS OrderDate,
                         SUM(m.costs * bhu.count) AS Revenue
                     FROM 
                             basket_has_users bhu
                     JOIN 
                             medicines m ON bhu.medicines_id = m.id
                     GROUP BY 
                             OrderDate
                     ORDER BY 
                             OrderDate";

                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataTable.Rows.Add(reader.GetString("OrderDate"), reader.GetInt32("Revenue"));
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
        public void getTopPharmacy() //Рейтинг магазинов
        {
            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.top_pharmacy;";
                dtStat.Clear();
                dtStat.Columns.Clear();
                DBConnection.dataAdapter.SelectCommand = DBConnection.command;
                DBConnection.dataAdapter.Fill(dtStat);
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные. Приносим извинения за предоставленные неудобства!", "Ошибка при получении данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        public void getTopMedicines() //Рейтинг лекарств
        {
            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.top_medicines;";
                dtStat2.Clear();
                dtStat.Columns.Clear();
                DBConnection.dataAdapter.SelectCommand = DBConnection.command;
                DBConnection.dataAdapter.Fill(dtStat2);
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные. Приносим извинения за предоставленные неудобства!", "Ошибка при получении данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
