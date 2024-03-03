using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace pharmacy
{
    internal class DBConnection
    {
        // TODO: вынести в конфиг файл?
        static string dbconnect = "server = localhost; user = root; password = root; database = pharmacy";
        static public MySqlDataAdapter dataAdapter;
        static MySqlConnection connect;
        static public MySqlCommand command;

        public static bool ConnectionDB()
        {
            try
            {
                connect = new MySqlConnection(dbconnect);
                connect.Open();
                command = new MySqlCommand();
                command.Connection = connect;
                dataAdapter = new MySqlDataAdapter(command);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с базой данных","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static void CloseDB()
        {
            connect.Close();
        }

        public MySqlConnection GetConnection()
        {
            return connect;
        }
    }
}
