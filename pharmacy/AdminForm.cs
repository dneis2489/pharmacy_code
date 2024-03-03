using pharmacy.service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace pharmacy
{
    public partial class AdminForm : Form
    {

        static public DataTable dtShop = new DataTable();
        public static bool changeDate = false;
        public static bool changeFactory = false;
        public static bool changeForm = false;
        public static bool changePrescription = false;

        public ShopService ShopService { get; }
        public BasketService BasketService { get; }
        public StatusService StatusService { get; }


        public AdminForm()
        {
            InitializeComponent();
            ShopService = new ShopService();
            BasketService = new BasketService();
            StatusService = new StatusService();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            textBox6.TextChanged += textBox6_TextChanged;


            //Подгрузка срока годности для фильтра в разделе "Лекарства в аптеке"
            ShopService.GetMedicinesExpirationDate().ForEach(item => comboBox2.Items.Add(item));

            //Подгрузка производителей для фильтра в разделе "Лекарства в аптеке"
            ShopService.GetMedicineWithFactory().ForEach(item => comboBox3.Items.Add(item));

            //Подгрузка формы выпуска для фильтра в разделе "Лекарства в аптеке"
            ShopService.GetAllReleaseForm().ForEach(item => comboBox4.Items.Add(item));
            
            //Магазин
            ShopService.getMedicinesInAdmin(AuthorizationService.pharmacy_id);
            dtShop = ShopService.dtShop;
            dataGridView1.DataSource = dtShop;

            //Заказы
            menuStrip2.Items.Clear();
            System.Windows.Forms.TextBox textBox;
            ToolStripControlHost host;

            List<string> ordersInfos = BasketService.GetOrdersInfosByPharmacyId(AuthorizationService.pharmacy_id);

            foreach (var item in ordersInfos)
            {
                textBox = new System.Windows.Forms.TextBox()
                {
                    Multiline = true,
                    Size = new System.Drawing.Size(160, 70), // Устанавливаем размеры текстового поля
                    ReadOnly = true
                };

                textBox.Text = item;

                host = new ToolStripControlHost(textBox)
                {
                    AutoSize = false // Отключаем автоматическое определение размеров
                };

                menuStrip2.Items.Add(host);
                textBox.Click += TextBox_Admin_Orders_Click;
            }

            //Статистика
            Series series = new Series("DataPoints");
            series.ChartType = SeriesChartType.Line;
            series.MarkerStyle = MarkerStyle.Circle;
            DataTable dummyData = StatisticsService.AdminGetCountBuyMedicinesStat();

            chart1.Series.Clear();

            foreach (DataRow row in dummyData.Rows)
            {
                DateTime date = (DateTime)row["PurchaseDate"];
                int quantity = (int)row["Quantity"];
                DateTime monthYearDate = new DateTime(date.Year, date.Month, 1);

                series.Points.AddXY(monthYearDate.ToString("MM.yyyy"), quantity);
            }

            chart1.Series.Add(series);

            StatusService.GetAllName().ForEach(item => comboBox1.Items.Add(item)); //перенесено из метода TextBox_Admin_Orders_Click
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ЛЕКАРСТВА В АПТЕКЕ
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) //Обновление списка лекарств после выбора фильтра по сроку годности
        {
            if (comboBox2.SelectedIndex != -1)
            {
                string selectedValue = comboBox2.SelectedItem.ToString();
                if (dtShop != null)
                {
                    DataView dv = new DataView(dtShop);
                    dv.RowFilter = $"expiration_date LIKE '%{selectedValue}%'";
                    dataGridView1.DataSource = dv;
                }

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) //Обновление списка лекарств после выбора фильтра по производителю
        {
            if (comboBox3.SelectedIndex != -1)
            {
                string selectedValue = comboBox3.SelectedItem.ToString();
                if (dtShop != null)
                {
                    DataView dv = new DataView(dtShop);
                    dv.RowFilter = $"medicine_factory LIKE '%{selectedValue}%'";
                    dataGridView1.DataSource = dv;

                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) //Обновление списка лекарств после выбора фильтра по форме выпуска
        {
            if (comboBox4.SelectedIndex != -1)
            {
                string selectedValue = comboBox4.SelectedItem.ToString();
                if (dtShop != null)
                {
                    DataView dv = new DataView(dtShop);
                    dv.RowFilter = $"release_form LIKE '%{selectedValue}%'";
                    dataGridView1.DataSource = dv;

                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e) //Обновление списка лекарств после ввода текста с клавиатуры
        {
            if (textBox6.Text != "")
            {
                string selectedValue = textBox6.Text;
                if (dtShop != null)
                {
                    DataView dv = new DataView(dtShop);
                    dv.RowFilter = $"Наименование LIKE '%{selectedValue}%'";
                    dataGridView1.DataSource = dv;

                }
            }
            else
            {
                ShopService.getMedicinesInAdmin(AuthorizationService.pharmacy_id);
                dtShop = ShopService.dtShop;
                dataGridView1.DataSource = dtShop;
            }
        }

        private void button5_Click(object sender, EventArgs e) //Товары в магазине - сбросить фильтры
        {
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            textBox6.Text = "";

            changeDate = false;
            changeFactory = false;
            changeForm = false;
            changePrescription = false;

            ShopService.getMedicinesInAdmin(AuthorizationService.pharmacy_id);
            dtShop = ShopService.dtShop;
            dataGridView1.DataSource = dtShop;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ЗАКАЗЫ
        private void TextBox_Admin_Orders_Click(object sender, EventArgs e) //Выбор заказа в разделе Заказы и подгрузка содержимого заказа
        {
            // Обработка события Click элемента TextBox
            System.Windows.Forms.TextBox clickedTextBox = (System.Windows.Forms.TextBox)sender;

            int startIndex = clickedTextBox.Text.IndexOf("Номер заказа: ") + "Номер заказа: ".Length;
            int endIndex = clickedTextBox.Text.IndexOf(Environment.NewLine, startIndex);
            int id;

            if (startIndex != -1 && endIndex != -1)
            {
                id = Int32.Parse(clickedTextBox.Text.Substring(startIndex, endIndex - startIndex));
                BasketService.GetBasketMedicines(id);
            }

            startIndex = clickedTextBox.Text.IndexOf("Имя заказчика: ") + "Имя заказчика: ".Length;
            endIndex = clickedTextBox.Text.IndexOf(Environment.NewLine, startIndex);
            textBox1.Text = clickedTextBox.Text.Substring(startIndex, endIndex - startIndex);

            

            startIndex = clickedTextBox.Text.IndexOf("Номер заказа: ") + "Номер заказа: ".Length;
            endIndex = clickedTextBox.Text.IndexOf(Environment.NewLine, startIndex);
            textBox3.Text = clickedTextBox.Text.Substring(startIndex, endIndex - startIndex);

            startIndex = clickedTextBox.Text.IndexOf("Дата доставки: ") + "Дата доставки: ".Length;
            endIndex = clickedTextBox.Text.IndexOf(Environment.NewLine, startIndex);
            textBox4.Text = clickedTextBox.Text.Substring(startIndex, endIndex - startIndex);

            startIndex = clickedTextBox.Text.IndexOf("Статус заказа: ") + "Статус заказа: ".Length;
            endIndex = clickedTextBox.Text.IndexOf(Environment.NewLine, startIndex);
            textBox5.Text = clickedTextBox.Text.Substring(startIndex, endIndex - startIndex);

            dataGridView2.DataSource = BasketService.dtBasket;
            dataGridView2.Columns["id"].Visible = false;

            textBox2.Text = dataGridView2.Rows
            .Cast<DataGridViewRow>()
                .Sum(row => Convert.ToDecimal(row.Cells["Стоимость:"].Value)).ToString();
        }

        private void button1_Click(object sender, EventArgs e) //Кнопка "Сохранить новый статус" в разделе "Заказы"
        {
            if(comboBox1.SelectedItem.ToString() != "" && textBox3.Text != "")
            {
                BasketService.UpdateStatusByNameAndBasketNumber(textBox3.Text, comboBox1.SelectedItem.ToString());                
            }

            System.Windows.Forms.TextBox textBox;
            ToolStripControlHost host;
            menuStrip2.Items.Clear();

            List<string> ordersInfos = BasketService.GetOrdersInfosByPharmacyId(AuthorizationService.pharmacy_id);

            foreach (var item in ordersInfos)
            {
                textBox = new System.Windows.Forms.TextBox()
                {
                    Multiline = true,
                    Size = new System.Drawing.Size(160, 70), // Устанавливаем размеры текстового поля
                    ReadOnly = true
                };

                textBox.Text = item;

                host = new ToolStripControlHost(textBox)
                {
                    AutoSize = false // Отключаем автоматическое определение размеров
                };

                menuStrip2.Items.Add(host);
                textBox.Click += TextBox_Admin_Orders_Click;
            }           
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел СТАТИСТИКА
        private void button2_Click(object sender, EventArgs e) //Статистика - Количество купленного товара в магазине
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            chart1.Visible = true;
            dataGridView3.Visible = false;


            Series series = new Series("DataPoints");
            series.ChartType = SeriesChartType.Line;
            series.MarkerStyle = MarkerStyle.Circle;
            DataTable dummyData = StatisticsService.AdminGetCountBuyMedicinesStat();

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

        private void button3_Click(object sender, EventArgs e) //Статистика - Количество покупок в магазине
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            chart1.Visible = true;
            dataGridView3.Visible = false;

            Series series = new Series("DataPoints");
            series.ChartType = SeriesChartType.Line;
            series.MarkerStyle = MarkerStyle.Circle;
            DataTable dummyData = StatisticsService.AdminGetCountBasketStat();

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

        private void button4_Click(object sender, EventArgs e) //Рейтинг покупателей
        {
            chart1.Visible = false;
            dataGridView3.Visible = true;
            StatisticsService.getTopUsersInPharmacy();
            dataGridView3.DataSource = StatisticsService.dtStat;

        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //ВЫХОД
        private void button6_Click(object sender, EventArgs e) //Кнопка ВЫХОД
        {
            this.Hide();
            AuthorizationForm form = new AuthorizationForm();
            form.Show();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //ПУСТЫЕ МЕТОДЫ БЕЗ РЕАЛИЗАЦИИ
        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
