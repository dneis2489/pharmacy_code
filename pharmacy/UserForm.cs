using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using OfficeOpenXml;
using Org.BouncyCastle.Utilities;
using pharmacy.data;
using pharmacy.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pharmacy
{
    public partial class UserController : Form
    {
        static public DataTable dtShop = new DataTable();
        private User User;


        //Перечень препаратов в корзине
        BindingList<Medicine> med = new BindingList<Medicine>();

        public static string EditOnPrescriptionMedicines, 
            EditNameMedicines, 
            EditBestBeforeDateMedicines, 
            EditVolumeMedicines, 
            EditPrimaryPackagingMedicines, 
            EditActiveSubstanceMedicines, 
            EditSpecialPropertiesMedicines, 
            EditReleaseFormMedicines;

        public static int Editcount, EditIdMedicines, EditCostsMedicines;
        private ShopService ShopService { get; }
        private PharmacyService PharmacyService { get; }
        private BasketService BasketService { get; }
        private CategoryService CategoryService { get; }
        private AuthorizationController authController { get; }

        private string activeOrder;

        public UserController(User user, AuthorizationController authControl)
        {
            InitializeComponent();
            User = user;
            authController = authControl;
            ShopService = ShopService.Instance;
            PharmacyService = PharmacyService.Instance;
            BasketService = BasketService.Instance;
            CategoryService = CategoryService.Instance;

            ShopService.GetMedicines();
            dtShop = ShopService.dtShop;
            dataGridView1.DataSource = dtShop;
            textBox7.TextChanged += textBox7_TextChanged;
            comboBox1.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            ShopService.GetMedicines();
            dtShop = ShopService.dtShop;
            dataGridView1.DataSource = dtShop;
            textBox7.TextChanged += textBox7_TextChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;

            //Подгрузка сроков годности для фильтров
            comboBox2.Items.AddRange(ShopService.GetMedicinesExpirationDate().ToArray());

            //Подгрузка производителей для фильтров
            comboBox3.Items.AddRange(ShopService.GetMedicineWithFactory().ToArray());

            //Подгрузка форм выпуска для фильтра
            comboBox4.Items.AddRange(ShopService.GetAllReleaseForm().ToArray());

            //Подгрузка категорий
            getCategories();

            //Подгрузка истории заказов
            RefreshOrderList();


            //Подгрузка аптек для совершения заказа
            comboBox1.Items.AddRange(PharmacyService.GetAllName().ToArray());
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ЛЕКАРСТВА

        private void TextBox_Click(object sender, EventArgs e) //Обновление перечня лекарств при новой категории
        {
            // Обработка события Click элемента TextBox
            System.Windows.Forms.TextBox clickedTextBox = (System.Windows.Forms.TextBox)sender;
            if (clickedTextBox.Text == "Все")
            {
                ShopService.GetMedicines();
                dataGridView1.DataSource = ShopService.dtShop;
                dataGridView1.Columns["id"].Visible = false;
                //dataGridView1.Columns["category"].Visible = false;
            }
            else
            {
                ShopService.UpdateViaCategory(clickedTextBox.Text);
                dataGridView1.DataSource = ShopService.dtShop;
            }
        }

        private void getCategories()
        {
            var categories = CategoryService.GetAllName();
            System.Windows.Forms.TextBox textBoxCategory;
            flowLayoutPanel2.Controls.Clear();
            System.Windows.Forms.TextBox textBoxOrderList;
            flowLayoutPanel2.AutoScroll = true;

            var ordersInfos = BasketService.GetOrdersInfosByUserId(User.UserId);

            foreach (var item in categories)
            {
                textBoxCategory = new System.Windows.Forms.TextBox()
                {
                    Multiline = true,
                    Size = new System.Drawing.Size(126, 50), // Устанавливаем размеры текстового поля
                    ReadOnly = true
                };
                textBoxCategory.MouseEnter += (sender, e) => { ((Control)sender).Cursor = Cursors.Hand; };
                textBoxCategory.Text = item;
                textBoxCategory.Click += TextBox_Click; // Обработчик события нажатия на текстовое поле

                flowLayoutPanel2.Controls.Add(textBoxCategory); // Добавляем TextBox в FlowLayoutPanel
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
                ShopService.GetMedicines();
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

            ShopService.GetMedicines();
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


                Editcount = Int32.Parse(textBox1.Text);
                med.Add(new Medicine
                {
                    Id = EditIdMedicines,
                    Name = EditNameMedicines,
                    Costs = EditCostsMedicines,
                    OnPrescription = EditOnPrescriptionMedicines,
                    Count = Editcount,
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
            activeOrder = clickedTextBox.Text;


            int startIndex = clickedTextBox.Text.IndexOf("Номер заказа: ") + "Номер заказа: ".Length;
            int endIndex = clickedTextBox.Text.IndexOf(Environment.NewLine, startIndex);
            int id;

            if (startIndex != -1 && endIndex != -1)
            {
                id = Int32.Parse(clickedTextBox.Text.Substring(startIndex, endIndex - startIndex));
                BasketService.GetBasketMedicines(id);
            }

            dataGridView3.DataSource = BasketService.dtBasket;
            //dataGridView3.Columns["id"].Visible = false;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел КОРЗИНА

        
        public void basketCountingValues() //Заполнение основных данных по покупке в корзине
        {
            dataGridView2.Refresh();
            dataGridView2.DataSource = med;
            

            //dataGridView2.DataSource = null;

            int basket_costs = 0;
            int[] medicineId = new int[med.Count];

            for (int i = 0; i < med.Count; i++)
            {
                basket_costs += med[i].Costs * med[i].Count;
            }

            for (int i = 0; i < med.Count; i++)
            {
                medicineId[i] = med[i].Id;
            }
            textBox3.Text = basket_costs.ToString();
            if(med.Count == 0)
            {
                textBox2.Text = null;
                comboBox1.SelectedIndex = -1;
            }
            if (comboBox1.SelectedIndex !=-1 && med.Count != 0)
            {
                textBox2.Text = BasketService.OrerDate(medicineId, comboBox1.SelectedItem.ToString());
            }
            textBox5.Text = BasketService.BasketNumber().ToString();
            textBox6.Text = User.Name;
        }

        private void button2_Click(object sender, EventArgs e) //Кнопка "удалить из списка"
        {
            EditIdMedicines = Int32.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            Medicine itemToRemove = med.FirstOrDefault(x => x.Id == EditIdMedicines);
            med.Remove(itemToRemove);
            basketCountingValues();

        }

        private void button4_Click(object sender, EventArgs e) //Кнопка "Изменить количество товара"
        {
            if(textBox4.Text != "")
            {
                EditIdMedicines = Int32.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
                Medicine itemToUpdate = med.FirstOrDefault(x => x.Id == EditIdMedicines);
                itemToUpdate.Count = Int32.Parse(textBox4.Text);
                dataGridView2.Refresh();
                textBox4.Text = "";
                basketCountingValues();
            }
            else
            {
                MessageBox.Show("Ошибка", "Пожалуйста, установите количество товара", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        //Подгрузка истории заказов
        public void RefreshOrderList() 
        {
            flowLayoutPanel1.Controls.Clear();
            System.Windows.Forms.TextBox textBoxOrderList;
            flowLayoutPanel1.AutoScroll = true;

            var ordersInfos = BasketService.GetOrdersInfosByUserId(User.UserId);

            foreach (var orderInfo in ordersInfos)
            {
                textBoxOrderList = new System.Windows.Forms.TextBox()
                {
                    Multiline = true,
                    Size = new System.Drawing.Size(126, 90), // Устанавливаем размеры текстового поля
                    ReadOnly = true
                };
                textBoxOrderList.MouseEnter += (sender, e) => { ((Control)sender).Cursor = Cursors.Hand; };

                textBoxOrderList.Text = orderInfo;

                textBoxOrderList.Click += TextBox_My_Orders_Click; // Обработчик события нажатия на текстовое поле

                flowLayoutPanel1.Controls.Add(textBoxOrderList); // Добавляем TextBox в FlowLayoutPanel
            }
        }


        private void button3_Click(object sender, EventArgs e) //Кнопка "Сделать заказ"
        {
            BasketService.AddBasketInDB(med, comboBox1.SelectedItem.ToString(), textBox2.Text, textBox5.Text, User.UserId);
            comboBox1.SelectedIndex = -1;
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            med.Clear();
            dataGridView2.Refresh();

            RefreshOrderList();
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e) //Перерасчет даты доставки в зависимости от выбранного пункта выдачи
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.Title = "Сохранить файл Excel";
            saveFileDialog.FileName = "Отчет по заказу.xlsx"; // Имя файла по умолчанию

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExcelExport.ExportOrderFromDataTable((DataTable)dataGridView3.DataSource, activeOrder, saveFileDialog, BasketService.orderDataColumns);
                }
                catch
                {
                    MessageBox.Show("Ошибка", "Файл не сохранен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                Console.WriteLine("Сохранение файла отменено.");
            }
            Console.WriteLine("Сохранение файла отменено.");
        }

        private void UserController_FormClosing(object sender, FormClosingEventArgs e)
        {
            authController.Show();
        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }



        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Раздел ВЫХОД
        private void button6_Click(object sender, EventArgs e) //Кнопка выход
        {

            authController.Show();
            this.Close();
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

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
