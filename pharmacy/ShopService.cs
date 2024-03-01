using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy
{
    internal class ShopService
    {
        static public DataTable dtShop = new DataTable();

        public static void getMedicines() //Подгрузить перечень лекарств из всех аптек
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

        public static void getMedicinesInAdmin(int id) //Получить список препаратов в конкретной аптеке
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

        public static void updateViaCategory(string name) //Обновить список лекарств в соответствии с выбранным фильтром
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
    }
}
