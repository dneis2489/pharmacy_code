using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pharmacy
{
    public partial class UserForm : Form
    {
        bool categoryMenu = false;
        static public DataTable dtShop = new DataTable();

        //Препарат в корзине
        public class medicine
        {
            public int id { get; set; }
            public string name { get; set; }
            public int costs { get; set; }
            public string on_prescription { get; set; }
            public int count { get; set; }
            public string best_before_date { get; set; }
            public string volume { get; set; }
            public string primary_packaging { get; set; }
            public string active_substance { get; set; }
            public string special_properties { get; set; }
            public string release_form { get; set; }

        }

        //Перечень препаратов в корзине
        List <medicine> med = new List<medicine>();

        public static string EditOnPrescriptionMedicines, 
            EditNameMedicines, 
            EditBestBeforeDateMedicines, 
            EditVolumeMedicines, 
            EditPrimaryPackagingMedicines, 
            EditActiveSubstanceMedicines, 
            EditSpecialPropertiesMedicines, 
            EditReleaseFormMedicines;

        public static int Editcount, EditIdMedicines, EditCostsMedicines;

        public UserForm()
        {
            InitializeComponent();

            ToolStripControlHost host;
            System.Windows.Forms.TextBox textBox;
            ShopService.getMedicines();
            dtShop = ShopService.dtShop;
            dataGridView1.DataSource = dtShop;
            dataGridView1.Columns["id"].Visible = false;
            textBox7.TextChanged += textBox7_TextChanged;
            comboBox1.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            
            //Подгрузка и запись категорий товаров
                try
                {
                    menuStrip3.Items.Clear();
                    DBConnection.command.CommandText = "SELECT name FROM pharmacy.category;";
                    using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                textBox = new System.Windows.Forms.TextBox()
                                {
                                    Multiline = true,
                                    Size = new System.Drawing.Size(126, 50), // Устанавливаем размеры текстового поля
                                    ReadOnly = true
                                };
                                textBox.Text = reader.GetString(reader.GetOrdinal("name"));
                                host = new ToolStripControlHost(textBox)
                                {
                                    AutoSize = false // Отключаем автоматическое определение размеров
                                };
                                menuStrip3.Items.Add(host);
                                textBox.Click += TextBox_Click;
                            }
                            categoryMenu = true;

                        }
                        else
                        {
                            MessageBox.Show("Неправильный логин или пароль!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch
                {

                }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            ToolStripControlHost host;
            System.Windows.Forms.TextBox textBox;
            ShopService.getMedicines();
            dtShop = ShopService.dtShop;
            dataGridView1.DataSource = dtShop;
            dataGridView1.Columns["id"].Visible = false;
            textBox7.TextChanged += textBox7_TextChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;

            //Подгрузка сроков годности для фильтров
            try
            {
                DBConnection.command.CommandText = @"SELECT distinct(expiration_date) FROM pharmacy.medicines;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader.GetString("expiration_date"));
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

            //Подгрузка производителей для фильтров
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                     SELECT distinct(f.name) FROM pharmacy.medicines m
                                                     JOIN medicine_factory f on m.medicine_factory_id = f.id;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox3.Items.Add(reader.GetString("name"));
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

            //Подгрузка форм выпуска для фильтра
            try
            {
                DBConnection.command.CommandText = @"SELECT distinct(release_form) FROM pharmacy.medicines;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox4.Items.Add(reader.GetString("release_form"));
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

            //Подгрузка категорий
            try
            {
                DBConnection.command.CommandText = "SELECT name FROM pharmacy.category;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox = new System.Windows.Forms.TextBox()
                            {
                                Multiline = true,
                                Size = new System.Drawing.Size(126, 50), // Устанавливаем размеры текстового поля
                                ReadOnly = true
                            };
                            textBox.Text = reader.GetString(reader.GetOrdinal("name"));
                            host = new ToolStripControlHost(textBox)
                            {
                                AutoSize = false // Отключаем автоматическое определение размеров
                            };
                            menuStrip3.Items.Add(host);
                            textBox.Click += TextBox_Click;
                        }
                        categoryMenu = true;

                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные по категориям товаров!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }

            //Подгрузка истории заказов
            menuStrip2.Items.Clear();
            System.Windows.Forms.TextBox textBox1;
            ToolStripControlHost host1;
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                         SELECT DISTINCT 
                                                           b.basket_number, 
                                                           s.name AS status_id, 
                                                           b.date, 
                                                           p.name AS pharmacy_id
                                                         FROM pharmacy.basket_has_users b
                                                         JOIN status s on b.status_id = s.id
                                                         JOIN pharmacy p on b.pharmacy_id = p.id
                                                         WHERE users_id = " + AuthorizationService.id + ";";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox1 = new System.Windows.Forms.TextBox()
                            {
                                Multiline = true,
                                Size = new System.Drawing.Size(126, 70), // Устанавливаем размеры текстового поля
                                ReadOnly = true
                            };
                            textBox1.Text = "Номер заказа: " + reader.GetString(reader.GetOrdinal("basket_number")) + "\r\nДата доставки: " + reader.GetString(reader.GetOrdinal("date"))
                                + "\r\nСтатус заказа: " + reader.GetString(reader.GetOrdinal("status_id")) + "\r\nАдрес доставки: " + reader.GetString(reader.GetOrdinal("pharmacy_id"));
                            host1 = new ToolStripControlHost(textBox1)
                            {
                                AutoSize = false // Отключаем автоматическое определение размеров
                            };
                            menuStrip2.Items.Add(host1);
                            textBox1.Click += TextBox_My_Orders_Click;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные по категориям товаров!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }

            //Подгрузка аптек для совершения заказа
            try
            {
                DBConnection.command.CommandText = @"SELECT name FROM pharmacy.pharmacy;";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString(reader.GetOrdinal("name")));
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
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ЛЕКАРСТВА

        private void TextBox_Click(object sender, EventArgs e) //Обновление перечня лекарств при новой категории
        {
            // Обработка события Click элемента TextBox
            System.Windows.Forms.TextBox clickedTextBox = (System.Windows.Forms.TextBox)sender;
            if (clickedTextBox.Text == "Все")
            {
                ShopService.getMedicines();
                dataGridView1.DataSource = ShopService.dtShop;
                dataGridView1.Columns["id"].Visible = false;
                //dataGridView1.Columns["category"].Visible = false;
            }
            else
            {
                ShopService.updateViaCategory(clickedTextBox.Text);
                dataGridView1.DataSource = ShopService.dtShop;
                dataGridView1.Columns["id"].Visible = false;
            }
        }

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

        private void textBox7_TextChanged(object sender, EventArgs e) //Обновление списка лекарств после ввода текста с клавиатуры
        {
            if (textBox7.Text != "")
            {
                string selectedValue = textBox7.Text;
                if (dtShop != null)
                {
                    DataView dv = new DataView(dtShop);
                    dv.RowFilter = $"Наименование LIKE '%{selectedValue}%'";
                    dataGridView1.DataSource = dv;

                }
            }
            else
            {
                ShopService.getMedicines();
                dtShop = ShopService.dtShop;
                dataGridView1.DataSource = dtShop;
            }
        }

        private void button5_Click(object sender, EventArgs e) //Товары в магазине - сбросить фильтры
        {
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            textBox7.Text = "";

            ShopService.getMedicines();
            dtShop = ShopService.dtShop;
            dataGridView1.DataSource = dtShop;
        }

        private void button1_Click(object sender, EventArgs e) //Добавление товара в корзину
        {
            if (textBox1.Text != "")
            {
                EditIdMedicines = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                EditNameMedicines = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                EditCostsMedicines = Int32.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                EditOnPrescriptionMedicines = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                EditBestBeforeDateMedicines = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                EditVolumeMedicines = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                EditPrimaryPackagingMedicines = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                EditActiveSubstanceMedicines = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                EditSpecialPropertiesMedicines = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                EditReleaseFormMedicines = dataGridView1.CurrentRow.Cells[11].Value.ToString();


                Editcount = Int32.Parse(textBox1.Text);
                med.Add(new medicine
                {
                    id = EditIdMedicines,
                    name = EditNameMedicines,
                    costs = EditCostsMedicines,
                    on_prescription = EditOnPrescriptionMedicines,
                    count = Editcount,
                    best_before_date = EditBestBeforeDateMedicines,
                    volume = EditVolumeMedicines,
                    primary_packaging = EditPrimaryPackagingMedicines,
                    active_substance = EditActiveSubstanceMedicines,
                    special_properties = EditSpecialPropertiesMedicines,
                    release_form = EditReleaseFormMedicines
                }
                );
                textBox1.Text = null;
            }
            else
            {
                MessageBox.Show("Ошибка", "Пожалуйста, установите количество покупаемого товара", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            basketCountingValues();
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ЗАКАЗЫ

        private void TextBox_My_Orders_Click(object sender, EventArgs e) //Отображение данных по выбранному заказу
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

            dataGridView3.DataSource = BasketService.dtBasket;
            dataGridView3.Columns["id"].Visible = false;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел КОРЗИНА

        
        public void basketCountingValues() //Заполнение основных данных по покупке в корзине
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = med;
            int basket_costs = 0;
            int[] medicineId = new int[med.Count];

            for (int i = 0; i < med.Count; i++)
            {
                basket_costs += med[i].costs * med[i].count;
            }

            for (int i = 0; i < med.Count; i++)
            {
                medicineId[i] = med[i].id;
            }
            textBox3.Text = basket_costs.ToString();
            textBox2.Text = null;
            comboBox1.Text = null;
            if (comboBox1.SelectedItem != null && med.Count != 0)
            {
                textBox2.Text = BasketService.OrerDate(medicineId, comboBox1.SelectedItem.ToString());
            }
            textBox5.Text = BasketService.BasketNumber().ToString();
            textBox6.Text = AuthorizationService.name;
        }

        private void button2_Click(object sender, EventArgs e) //Кнопка "удалить из списка"
        {
            EditIdMedicines = Int32.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            medicine itemToRemove = med.Find(x => x.id == EditIdMedicines);
            med.Remove(itemToRemove);
            basketCountingValues();

        }

        private void button4_Click(object sender, EventArgs e) //Кнопка "Изменить количество товара"
        {
            if(textBox4.Text != "")
            {
                EditIdMedicines = Int32.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
                medicine itemToUpdate = med.Find(x => x.id == EditIdMedicines);
                itemToUpdate.count = Int32.Parse(textBox4.Text);
                basketCountingValues();
                textBox4.Text = "";
            }
            else
            {
                MessageBox.Show("Ошибка", "Пожалуйста, установите количество товара", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button3_Click(object sender, EventArgs e) //Кнопка "Сделать заказ"
        {
            BasketService.AddBasketInDB(med, comboBox1.SelectedItem.ToString(), textBox2.Text, textBox5.Text);
            dataGridView2.DataSource = null;
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            med.Clear();
            menuStrip2.Items.Clear();
            System.Windows.Forms.TextBox textBox1;
            ToolStripControlHost host1;
            //Обновление истории заказов
            try
            {
                DBConnection.command.CommandText = @"USE pharmacy;
                                                         SELECT DISTINCT 
                                                           b.basket_number, 
                                                           s.name AS status_id, 
                                                           b.date, 
                                                           p.name AS pharmacy_id
                                                         FROM pharmacy.basket_has_users b
                                                         JOIN status s on b.status_id = s.id
                                                         JOIN pharmacy p on b.pharmacy_id = p.id
                                                         WHERE users_id = " + AuthorizationService.id + ";";
                using (MySqlDataReader reader = DBConnection.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox1 = new System.Windows.Forms.TextBox()
                            {
                                Multiline = true,
                                Size = new System.Drawing.Size(126, 70), // Устанавливаем размеры текстового поля
                                ReadOnly = true
                            };
                            textBox1.Text = "Номер заказа: " + reader.GetString(reader.GetOrdinal("basket_number")) + "\r\nДата доставки: " + reader.GetString(reader.GetOrdinal("date"))
                                + "\r\nСтатус заказа: " + reader.GetString(reader.GetOrdinal("status_id")) + "\r\nАдрес доставки: " + reader.GetString(reader.GetOrdinal("pharmacy_id"));
                            host1 = new ToolStripControlHost(textBox1)
                            {
                                AutoSize = false // Отключаем автоматическое определение размеров
                            };
                            menuStrip2.Items.Add(host1);
                            textBox1.Click += TextBox_My_Orders_Click;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные по категориям товаров!", "Пожалуйста, попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {

            }
            basketCountingValues();
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e) //Перерасчет даты доставки в зависимости от выбранного пункта выдачи
        {
            int[] medicineId = new int[med.Count];
            for (int i = 0; i < med.Count; i++)
            {
                medicineId[i] = med[i].id;
            }
            if (comboBox1.SelectedItem != null || med.Count == 0)
            {
                textBox2.Text = BasketService.OrerDate(medicineId, comboBox1.SelectedItem.ToString());
            }
        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {

        }



        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ВЫХОД
        private void button6_Click(object sender, EventArgs e) //Кнопка выход
        {
            this.Hide();
            AuthorizationForm form = new AuthorizationForm();
            form.Show();
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //ПУСТЫЕ МЕТОДЫ БЕЗ РЕАЛИЗАЦИИ
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
