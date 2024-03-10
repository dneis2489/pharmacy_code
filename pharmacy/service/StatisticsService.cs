using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

namespace pharmacy
{
    public class StatisticsService
    {
        private StatisticsService()
        {
            dtStat = new DataTable();
            dtStat2 = new DataTable();
            SQLExecutor = new SQLExecutor("Ошибка подключения к базе с аптеками");
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

        public DataTable dtStat;
        public DataTable dtStat2;
        private SQLExecutor SQLExecutor;


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //СТАТИСТИКА ДЛЯ АДМИНА
        public DataTable AdminGetCountBuyMedicinesStat(int pharmacyId) //Количество купленного товара в магазине для Админа
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
            dataTable.Columns.Add("Quantity", typeof(int));

            string query = @"USE pharmacy;    
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

            return SQLExecutor.ExecuteQueryWithNewData(query, "PurchaseMonth", "TotalCount", dataTable);
        }

        public DataTable AdminGetCountBasketStat(int pharmacyId) //Количество покупок в магазине для Админа
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
            dataTable.Columns.Add("Quantity", typeof(int));

            string query = @"USE pharmacy;    
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

            return SQLExecutor.ExecuteQueryWithNewData(query, "PurchaseMonth", "TotalCount", dataTable);
        }

        public void getTopUsersInPharmacy(int pharmacyId) //Рейтинг покупателей для Админа
        {
            string query = @"USE pharmacy;
                                                    SELECT 
	                                                    u.name AS 'ФИО', 
                                                        COUNT(distinct(b.basket_number)) as 'Количество заказов'
                                                    FROM basket_has_users b
                                                    JOIN users u on b.users_id = u.id
                                                    where b.pharmacy_id = " + pharmacyId + @"
                                                    GROUP BY users_id
                                                    ORDER BY 'Количество заказов' DESC;";
            SQLExecutor.ExecuteSelectQueryWithFill(query, dtStat);
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //СТАТИСТИКА ДЛЯ СУПЕР-ПОЛЬЗОВАТЕЛЯ
        public DataTable RootGetCountBuyMedicinesStat() //Количество проданной продукции
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
            dataTable.Columns.Add("Quantity", typeof(int));

            string query = @"USE pharmacy;    
                     SELECT 
                         DATE_FORMAT(date, '%Y-%m-01') AS PurchaseMonth,
                         SUM(count) AS TotalCount
                     FROM 
                         basket_has_users
                     GROUP BY 
                         DATE_FORMAT(date, '%Y-%m-01') 
                     ORDER BY 
                         PurchaseMonth;";
            return SQLExecutor.ExecuteQueryWithNewData(query, "PurchaseMonth", "TotalCount", dataTable);
        }

        public DataTable RootGetCountBasketStat() //Количество заказов
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
            dataTable.Columns.Add("Quantity", typeof(int));

            string query = @"USE pharmacy;    
                     SELECT 
                         DATE_FORMAT(date, '%Y-%m-01') AS PurchaseMonth,
                         COUNT(distinct(basket_number)) AS TotalCount
                     FROM 
                         basket_has_users
                     GROUP BY 
                         DATE_FORMAT(date, '%Y-%m-01') 
                     ORDER BY 
                         PurchaseMonth;";
            return SQLExecutor.ExecuteQueryWithNewData(query, "PurchaseMonth", "TotalCount", dataTable);
        }
        public DataTable RootGetRevenueByMonth() //Доходы
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("OrderDate", typeof(DateTime));
            dataTable.Columns.Add("Revenue", typeof(int));

            string query = @"USE pharmacy;    
                     SELECT 
                         DATE_FORMAT(date, '%Y-%m-01') AS OrderDate,
                         SUM(m.costs * bhu.count) AS Revenue
                     FROM 
                             basket_has_users bhu
                     JOIN 
                             medicines m ON bhu.medicines_id = m.id
                     GROUP BY 
                             DATE_FORMAT(date, '%Y-%m-01')
                     ORDER BY 
                             OrderDate;";
            return SQLExecutor.ExecuteQueryWithNewData(query, "OrderDate", "Revenue", dataTable);
        }

        public void GetTopPharmacy() //Рейтинг магазинов
        {
            string query = @"SELECT * FROM pharmacy.top_pharmacy;";
            SQLExecutor.ExecuteSelectQueryWithFill(query, dtStat);

        }
        public void GetTopMedicines() //Рейтинг лекарств
        {
            string query = @"SELECT * FROM pharmacy.top_medicines;";
            SQLExecutor.ExecuteSelectQueryWithFill(query, dtStat2);
        }
    }
}
