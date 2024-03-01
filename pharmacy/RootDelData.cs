using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace pharmacy
{
    internal class RootDelData// TODO: удалить в дальнейшем
    {
        public static void DelUser(int id) //Удалить пользователя
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`users`
                                                     WHERE id = "+id+";";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка удаления значений из базы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }

        public static void DelShedule(int id) //Удалить график работы
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`pharmacy_schedule`
                                                     WHERE id = " + id + ";";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка удаления значений из базы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }

        public static void DelPharmacy(int id) //Удалить аптеку
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`pharmacy`
                                                     WHERE id = " + id + ";";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка удаления значений из базы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }

        public static void DelStat(int id) //Удалить статус
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`status`
                                                     WHERE id = " + id + ";";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка удаления значений из базы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }

        public static void DelCategory(int id) //Удалить категорию
        {
            try
            {
                DBConnection.command.CommandText = @"DELETE FROM `pharmacy`.`category`
                                                     WHERE id = " + id + ";";
                if (DBConnection.command.ExecuteNonQuery() < 0)
                {
                    MessageBox.Show("Ошибка удаления значений из базы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }
    }
}
