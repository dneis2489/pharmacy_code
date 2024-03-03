using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pharmacy
{
    public class ShopService
    {
        public ShopService()
        {
            dtShop = new DataTable();
            SQLExecutor = new SQLExecutor("Ошибка подключения к базе с аптеками");
        }


        public DataTable dtShop { get; }
        private SQLExecutor SQLExecutor { get;}


        public void GetMedicines() //Подгрузить перечень лекарств из всех аптек
        {
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                     SELECT
                                                         m.id,
	                                                     m.name AS 'Наименование',
                                                         m.costs AS 'Стоимость',
                                                         mp.count AS 'Количество',
                                                         p.name AS 'Магазин',
                                                         CASE WHEN m.on_prescription = 0 then 'Не требуется' ELSE 'Требуется' END AS prescription,
                                                         expiration_date,
                                                         concat(m.volume, ' ' ,m.units_of_measurement) AS 'Объём',
                                                         m.primary_packaging AS 'Первичная упаковка',
                                                         m.active_substance AS 'Активное вещество',
                                                         m.special_properties AS 'Специальные свойства',
                                                         m.release_form AS release_form,
                                                         mf.name AS medicine_factory
                                                     FROM pharmacy.medicines m
                                                     JOIN pharmacy.medicines_has_pharmacy mp ON m.id = mp.medicines_id
                                                     JOIN pharmacy.pharmacy p ON mp.pharmacy_id = p.id
                                                     JOIN pharmacy.medicine_factory mf ON m.medicine_factory_id = mf.id";
                dtShop.Clear();
                DBConnection.dataAdapter.SelectCommand = DBConnection.command;
                DBConnection.dataAdapter.Fill(dtShop);
            }
            catch 
            {
                MessageBox.Show("Не удалось получить данные. Приносим извинения за предоставленные неудобства!", "Ошибка при получении данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void GetMedicinesInAdmin(int id) //Получить список препаратов в конкретной аптеке
        {
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                     SELECT 
	                                                     m.name AS 'Наименование',
                                                         m.costs AS 'Стоимость',
                                                         mp.count AS 'Количество',
                                                         CASE WHEN m.on_prescription = 0 then 'Не требуется' ELSE 'Требуется' END AS prescription,
                                                         expiration_date,
                                                         concat(m.volume, ' ' ,m.units_of_measurement) AS 'Объём',
                                                         m.primary_packaging AS 'Первичная упаковка',
                                                         m.active_substance AS 'Активное вещество',
                                                         m.special_properties AS 'Специальные свойства',
                                                         m.release_form AS release_form,
                                                         mf.name AS medicine_factory
                                                     FROM pharmacy.medicines m
                                                     JOIN pharmacy.medicines_has_pharmacy mp ON m.id = mp.medicines_id
                                                     JOIN pharmacy.medicine_factory mf ON m.medicine_factory_id = mf.id
                                                     where mp.pharmacy_id = " + id;
                dtShop.Clear();
                DBConnection.dataAdapter.SelectCommand = DBConnection.command;
                DBConnection.dataAdapter.Fill(dtShop);
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные. Приносим извинения за предоставленные неудобства!", "Ошибка при получении данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void UpdateViaCategory(string name) //Обновить список лекарств в соответствии с выбранным фильтром
        {
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                     SELECT
                                                         m.id,
	                                                     m.name AS 'Наименование',
                                                         m.costs AS 'Стоимость',
                                                         mp.count AS 'Количество',
                                                         p.name AS 'Магазин',
                                                         CASE WHEN m.on_prescription = 0 then 'Не требуется' ELSE 'Требуется' END AS prescription,
                                                         expiration_date,
                                                         concat(m.volume, ' ' ,m.units_of_measurement) AS 'Объём',
                                                         m.primary_packaging AS 'Первичная упаковка',
                                                         m.active_substance AS 'Активное вещество',
                                                         m.special_properties AS 'Специальные свойства',
                                                         m.release_form AS release_form,
                                                         mf.name AS medicine_factory
                                                     FROM pharmacy.medicines m
                                                     JOIN pharmacy.medicines_has_pharmacy mp ON m.id = mp.medicines_id
                                                     JOIN pharmacy.pharmacy p ON mp.pharmacy_id = p.id
                                                     JOIN pharmacy.medicine_factory mf ON m.medicine_factory_id = mf.id
                                                     JOIN pharmacy.category c ON c.id = m.category_id
                                                     Where c.name like '" + name + "'";
                dtShop.Clear();
                DBConnection.dataAdapter.SelectCommand = DBConnection.command;
                DBConnection.dataAdapter.Fill(dtShop);
            }
            catch 
            {

            }
        }

        //Подгрузка срока годности для фильтра в разделе "Лекарства в аптеке"
        public List<string> GetMedicinesExpirationDate() 
        {
            List<string> result = new List<string>();

            //Подгрузка срока годности для фильтра в разделе "Лекарства в аптеке"
            try
            {
                DBConnection.command.CommandText = @"SELECT distinct(expiration_date) FROM pharmacy.medicines;";    // в shopService  GetMedicinesExpirationDate()

                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString("expiration_date"));
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

        //Подгрузка производителей для фильтра в разделе "Лекарства в аптеке"
        public List<string> GetMedicineWithFactory()
        {
            string query = @"USE pharmacy;
                                SELECT distinct(f.name) FROM pharmacy.medicines m
                                    JOIN medicine_factory f on m.medicine_factory_id = f.id;";

            return SQLExecutor.ExecuteSelectQuery(query, "name");
        }

        public List<string> GetAllReleaseForm() 
        {
            string query = @"SELECT distinct(release_form) FROM pharmacy.medicines;";

            return SQLExecutor.ExecuteSelectQuery(query, "release_form");
        }
    }
}
