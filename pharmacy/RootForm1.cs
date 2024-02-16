using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace pharmacy
{
    public partial class RootForm1 : Form
    {
        public RootForm1()
        {
            InitializeComponent();
        }

        private void RootForm1_Load(object sender, EventArgs e)
        {
            //Вкладка добавить данные
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            //Подгрузка графиков работ для добавления Магазина
            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.pharmacy_schedule;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetInt32("id").ToString() + ". " + reader.GetTimeSpan("opening_time").ToString() + " - " + reader.GetTimeSpan("ending_time").ToString() +
                                " (" + reader.GetTimeSpan("opening_time_on_weekands").ToString() + " - " + reader.GetTimeSpan("ending_time_on_weekands").ToString() + ")");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }

            //Подгрузка системных ролей и магазинов для добавления пользователя
            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.role;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader.GetInt32("id").ToString() + ". " + reader.GetString("name").ToString());

                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.pharmacy;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox3.Items.Add(reader.GetInt32("id").ToString() + ". " + reader.GetString("name").ToString());

                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }

            //Вкладка удалить данные

            comboBox4.Items.Clear(); //Магазины
            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.pharmacy;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox4.Items.Add(reader.GetInt32("id").ToString() + ". " + reader.GetString("name").ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }

            comboBox5.Items.Clear(); //Графики
            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.pharmacy_schedule;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox5.Items.Add(reader.GetInt32("id").ToString() + ". " + reader.GetTimeSpan("opening_time").ToString() + "-" + reader.GetTimeSpan("ending_time").ToString() +
                                " (" + reader.GetTimeSpan("opening_time_on_weekands").ToString() + "-" + reader.GetTimeSpan("ending_time_on_weekands").ToString() + ")");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }

            comboBox6.Items.Clear(); //Пользователи
            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.users;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox6.Items.Add(reader.GetInt32("id").ToString() + ". " + reader.GetString("name").ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }

            comboBox7.Items.Clear(); //Статусы
            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.status;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox7.Items.Add(reader.GetInt32("id").ToString() + ". " + reader.GetString("name").ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }

            comboBox8.Items.Clear(); //Категории
            try
            {
                DBConnection.command.CommandText = @"SELECT * FROM pharmacy.category;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox8.Items.Add(reader.GetInt32("id").ToString() + ". " + reader.GetString("name").ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }

            //Вкладка статусы
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            chart1.Visible = true;

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ДОБАВИТЬ ДАННЫЕ
        private void button1_Click(object sender, EventArgs e) //Добавить магазин - Добавить
        {
            if (comboBox1.SelectedItem != "" && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                string choice = comboBox1.SelectedItem.ToString();
                char id = choice[0];
                int sheduleId = Int32.Parse(id.ToString());

                RootAddData.AddPharmacy(textBox1.Text, textBox2.Text, textBox3.Text, sheduleId);

                comboBox1.SelectedIndex = -1;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            else
            {
                MessageBox.Show("Заполнены не все обязательные поля", "Пожалуйста, заполните все поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)//Добавить магазин - Отчистить
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e) //Добавить категорию - Добавить
        {
            if (textBox5.Text != "")
            {
                RootAddData.AddCategory(textBox5.Text);
                textBox5.Text = "";
            }
            else
            {
                MessageBox.Show("Заполнены не все обязательные поля", "Пожалуйста, заполните все поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e) //Добавить категорию - Отчистить
        {
            textBox5.Text = "";
        }

        private void button6_Click(object sender, EventArgs e) // Добавить график работы - Добавить
        {
            if (textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "")
            {
                RootAddData.AddSchedule(textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
            }
            else
            {
                MessageBox.Show("Заполнены не все обязательные поля", "Пожалуйста, заполните все поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e) // Добавить график работы - Отчитстить
        {
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)//Добавить статус - Добавить
        {
            if (textBox10.Text != "")
            {
                RootAddData.AddStatus(textBox10.Text);
                textBox10.Text = "";
            }
            else
            {
                MessageBox.Show("Заполнены не все обязательные поля", "Пожалуйста, заполните все поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)//Добавить статус - Отчистить
        {
            textBox10.Text = "";
        }

        private void button10_Click(object sender, EventArgs e) //Добавить пользователей - Добавить
        {
            if (textBox4.Text != "" && textBox11.Text != "" && textBox12.Text != "" && textBox13.Text != "" && textBox14.Text != "" && textBox15.Text != "" &&
                comboBox2.SelectedItem != "")
            {
                if (textBox14.Text == textBox15.Text && ((comboBox2.SelectedItem.ToString() == "Администратор" && comboBox3.SelectedIndex != -1) || (comboBox2.SelectedItem.ToString() != "Администратор")))
                {
                    
                    string choiceRole = comboBox2.SelectedItem.ToString();
                    char role = choiceRole[0];
                    int roleId = Int32.Parse(role.ToString());
                    if (comboBox2.SelectedItem.ToString() == "Администратор" && comboBox3.SelectedIndex != -1)
                    {
                        string choicePharm = comboBox3.SelectedItem.ToString();
                        char pharm = choicePharm[0];
                        int pharmId = Int32.Parse(pharm.ToString());
                        RootAddData.AddUser(textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text, roleId, pharmId);
                    }
                    else
                    {
                        RootAddData.AddUser(textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text, roleId, 0);
                    }
                    textBox4.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                    textBox13.Text = "";
                    textBox14.Text = "";
                    textBox15.Text = "";
                    comboBox2.SelectedIndex = -1;
                    comboBox3.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ошибка подключения к базе с аптеками", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)//Добавить пользователей - Отчистить
        {
            textBox4.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел УДАЛИТЬ ДАННЫЕ
        private void button12_Click(object sender, EventArgs e) //Удалить Магазин - Удалить
        {
            if (comboBox4.SelectedIndex != -1)
            {
                string choice = comboBox4.SelectedItem.ToString();
                char id = choice[0];
                int pharmId = Int32.Parse(id.ToString());
                if(pharmId == 1)
                {
                    MessageBox.Show("Невозможно удалить главную аптеку", "Пожалуйста, выберете другого пользователя", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    RootDelData.DelPharmacy(pharmId);
                }


                comboBox4.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Заполнены не все обязательные поля", "Пожалуйста, заполните все поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button11_Click(object sender, EventArgs e) //Удалить Магазин - Отчистить
        {
            comboBox4.SelectedIndex = -1;
        }

        private void button14_Click(object sender, EventArgs e) //Удалить График работы - Удалить
        {
            if (comboBox5.SelectedIndex != -1)
            {
                string choice = comboBox5.SelectedItem.ToString();
                char id = choice[0];
                int cheduleId = Int32.Parse(id.ToString());
                if (cheduleId == 1)
                {
                    MessageBox.Show("Невозможно удалить главный график работы", "Пожалуйста, выберете другой график", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    RootDelData.DelShedule(cheduleId);
                }
                

                comboBox5.SelectedItem = "";
            }
            else
            {
                MessageBox.Show("Заполнены не все обязательные поля", "Пожалуйста, заполните все поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button13_Click(object sender, EventArgs e) //Удалить График работы - Отчистить
        {
            comboBox5.SelectedIndex = -1;
        }

        private void button16_Click(object sender, EventArgs e) //Удалить Пользователя - Удалить
        {
            if (comboBox6.SelectedIndex != -1)
            {
                string choice = comboBox5.SelectedItem.ToString();
                char id = choice[0];
                int userId = Int32.Parse(id.ToString());
                if (userId == 1)
                {
                    MessageBox.Show("Невозможно удалить главного администратора", "Пожалуйста, выберете другого пользователя", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    RootDelData.DelUser(userId);
                }

                comboBox6.SelectedItem = "";
            }
            else
            {
                MessageBox.Show("Заполнены не все обязательные поля", "Пожалуйста, заполните все поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button15_Click(object sender, EventArgs e) //Удалить Пользователя - Отчистить
        {
            comboBox6.SelectedIndex = -1;
        }

        private void button18_Click(object sender, EventArgs e) //Удалить Статус - Удалить 
        {
            if (comboBox7.SelectedIndex != -1)
            {
                string choice = comboBox5.SelectedItem.ToString();
                char id = choice[0];
                int statId = Int32.Parse(id.ToString());

                if(statId == 1)
                {
                    MessageBox.Show("Невозможно удалить основной статус", "Пожалуйста, выберете другой статус", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    RootDelData.DelStat(statId);
                }
                

                comboBox7.SelectedItem = "";
            }
            else
            {
                MessageBox.Show("Заполнены не все обязательные поля", "Пожалуйста, заполните все поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button17_Click(object sender, EventArgs e) //Удалить Статус - Отчистить
        {
            comboBox7.SelectedIndex = -1;
        }

        private void button20_Click(object sender, EventArgs e) //Удалить Категорию - Удалить 
        {
            if (comboBox8.SelectedIndex != -1)
            {
                string choice = comboBox5.SelectedItem.ToString();
                char id = choice[0];
                int catId = Int32.Parse(id.ToString());
                if(catId == 1)
                {
                    MessageBox.Show("Невозможно удалить освновную категорию", "Пожалуйста, выберете другую категорию", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    RootDelData.DelCategory(catId);
                }
                comboBox8.SelectedItem = "";
            }
            else
            {
                MessageBox.Show("Заполнены не все обязательные поля", "Пожалуйста, заполните все поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button19_Click(object sender, EventArgs e) //Удалить Категорию - Отчистить
        {
            comboBox8.SelectedItem = "";
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел СТАТИСТИКА
        private void button24_Click(object sender, EventArgs e) //Статистика - Количество проданной продукции
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            chart1.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            Series series = new Series("DataPoints");
            series.ChartType = SeriesChartType.Line;
            series.MarkerStyle = MarkerStyle.Circle;
            DataTable dummyData = Statist.RootGetCountBuyMedicinesStat();



            foreach (DataRow row in dummyData.Rows)
            {
                DateTime date = (DateTime)row["PurchaseDate"];
                int quantity = (int)row["Quantity"];
                DateTime monthYearDate = new DateTime(date.Year, date.Month, 1);

                series.Points.AddXY(monthYearDate.ToString("MM.yyyy"), quantity);
            }


            chart1.Series.Add(series);
        }

        private void button22_Click(object sender, EventArgs e) //Статистика - Количество заказов
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            chart1.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            Series series = new Series("DataPoints");
            series.ChartType = SeriesChartType.Line;
            series.MarkerStyle = MarkerStyle.Circle;
            DataTable dummyData = Statist.RootGetCountBasketStat();

            chart1.Series.Clear();

            foreach (DataRow row in dummyData.Rows)
            {
                DateTime date = (DateTime)row["PurchaseDate"];
                int quantity = (int)row["Quantity"];
                DateTime monthYearDate = new DateTime(date.Year, date.Month, 1);

                series.Points.AddXY(monthYearDate.ToString("MM.yyyy"), quantity);
            }

            chart1.Series.Add(series);
        }

        private void button23_Click(object sender, EventArgs e) //Статистика - Рейтинг магазинов
        {
            chart1.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            Statist.getTopPharmacy();
            dataGridView1.DataSource = Statist.dtStat;
        }

        private void button21_Click(object sender, EventArgs e) //Статистика - Доходы
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            chart1.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            Series series = new Series("DataPoints");
            series.ChartType = SeriesChartType.Line;
            series.MarkerStyle = MarkerStyle.Circle;
            DataTable dummyData = Statist.RootGetRevenueByMonth();

            chart1.Series.Clear();

            foreach (DataRow row in dummyData.Rows)
            {
                DateTime date = (DateTime)row["PurchaseDate"];
                int quantity = (int)row["Quantity"];
                DateTime monthYearDate = new DateTime(date.Year, date.Month, 1);

                series.Points.AddXY(monthYearDate.ToString("MM.yyyy"), quantity);
            }

            chart1.Series.Add(series);
        }

        private void button25_Click(object sender, EventArgs e) //Рейтинг лекарств
        {
            chart1.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            Statist.getTopMedicines();
            dataGridView2.DataSource = Statist.dtStat2;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ВЫХОД
        private void button27_Click(object sender, EventArgs e) //Кнопка "Выход"
        {
            this.Hide();
            AuthorizationForm form = new AuthorizationForm();
            form.Show();
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ПУСТЫЕ МЕТОДЫ БЕЗ РЕАЛИЗАЦИИ
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }
    }
}
