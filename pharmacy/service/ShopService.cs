using System.Collections.Generic;
using System.Data;

namespace pharmacy
{
    public class ShopService
    {
        private ShopService()
        {
            dtShop = new DataTable();
            SQLExecutor = new SQLExecutor("Ошибка подключения к базе с аптеками");
            dataColumns = new List<string>() {
                "Наименование:", "Стоимость:", "Количество:", "Объём:", "Рецепт:",
                "Срок годности:", "Первичная упаковка:",
                "Активное вещество:", "Специальные свойства:", "Форма выпуска:",
                "Производитель:" };
        }
        private static ShopService instance;

        public static ShopService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShopService();                   
                }
                return instance;
            }
        }

       
        public DataTable dtShop { get; }
        public List<string> dataColumns;
        private SQLExecutor SQLExecutor { get;}
        

        public void GetMedicines() //Подгрузить перечень лекарств из всех аптек
        {
            string query = @"USE pharmacy;
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
            SQLExecutor.ExecuteSelectQueryWithFill(query, dtShop);
        }

        public void GetMedicinesInAdmin(int id) //Получить список препаратов в конкретной аптеке
        {
            string query = @"USE pharmacy;
                                                     SELECT 
	                                                     m.name AS 'Наименование:',
                                                         m.costs AS 'Стоимость:',
                                                         mp.count AS 'Количество:',
                                                         CASE WHEN m.on_prescription = 0 then 'Не требуется' ELSE 'Требуется' END AS 'Рецепт:',
                                                         m.expiration_date AS 'Срок годности:',
                                                         concat(m.volume, ' ' ,m.units_of_measurement) AS 'Объём:',
                                                         m.primary_packaging AS 'Первичная упаковка:',
                                                         m.active_substance AS 'Активное вещество:',
                                                         m.special_properties AS 'Специальные свойства:',
                                                         m.release_form AS 'Форма выпуска:',
                                                         mf.name AS 'Производитель:'
                                                     FROM pharmacy.medicines m
                                                     JOIN pharmacy.medicines_has_pharmacy mp ON m.id = mp.medicines_id
                                                     JOIN pharmacy.medicine_factory mf ON m.medicine_factory_id = mf.id
                                                     where mp.pharmacy_id = " + id;
            SQLExecutor.ExecuteSelectQueryWithFill(query, dtShop);
        }

        public void UpdateViaCategory(string name) //Обновить список лекарств в соответствии с выбранным фильтром
        {
            string query = @"USE pharmacy;
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
            SQLExecutor.ExecuteSelectQueryWithFill(query, dtShop);
        }

        //Подгрузка срока годности для фильтра в разделе "Лекарства в аптеке"
        public List<string> GetMedicinesExpirationDate()
        {
            string query = @"SELECT distinct(expiration_date) FROM pharmacy.medicines;";
            return SQLExecutor.ExecuteSelectQueryWithError(query, "Не удалось получить данные для фильтра по сроку годности!", "expiration_date");
        }

        //Подгрузка производителей для фильтра в разделе "Лекарства в аптеке"
        public List<string> GetMedicineWithFactory()
        {
            string query = @"USE pharmacy;
                                SELECT distinct(f.name) FROM pharmacy.medicines m
                                    JOIN medicine_factory f on m.medicine_factory_id = f.id;";

            return SQLExecutor.ExecuteSelectQueryWithError(query, "Не удалось получить данные для фильтра производителей!", "name");
        }

        public List<string> GetAllReleaseForm() 
        {
            string query = @"SELECT distinct(release_form) FROM pharmacy.medicines;";            

            return SQLExecutor.ExecuteSelectQueryWithError(query,"Не удалось получить данные для фильтра форм выпуска!", "release_form");
        }
    }
}
