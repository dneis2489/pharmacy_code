namespace pharmacy
{
    partial class AdminController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Наименование = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Стоимость = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Количество = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Объём = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expiration_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.release_form = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medicine_factory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pharmacy = new System.Windows.Forms.TabPage();
            this.button7 = new System.Windows.Forms.Button();
            this.orders = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.statistic = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.pharmacy.SuspendLayout();
            this.orders.SuspendLayout();
            this.statistic.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Наименование,
            this.Стоимость,
            this.Количество,
            this.Объём,
            this.Column5,
            this.prescription,
            this.expiration_date,
            this.Column1,
            this.Column2,
            this.Column3,
            this.release_form,
            this.medicine_factory});
            this.dataGridView1.Location = new System.Drawing.Point(9, 55);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1174, 435);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "id";
            this.Column4.HeaderText = "id";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            // 
            // Наименование
            // 
            this.Наименование.DataPropertyName = "Наименование:";
            this.Наименование.HeaderText = "Наименование:";
            this.Наименование.Name = "Наименование";
            this.Наименование.ReadOnly = true;
            // 
            // Стоимость
            // 
            this.Стоимость.DataPropertyName = "Стоимость:";
            this.Стоимость.HeaderText = "Стоимость:";
            this.Стоимость.Name = "Стоимость";
            this.Стоимость.ReadOnly = true;
            // 
            // Количество
            // 
            this.Количество.DataPropertyName = "Количество:";
            this.Количество.HeaderText = "Количество:";
            this.Количество.Name = "Количество";
            this.Количество.ReadOnly = true;
            // 
            // Объём
            // 
            this.Объём.DataPropertyName = "Объём:";
            this.Объём.HeaderText = "Объём:";
            this.Объём.Name = "Объём";
            this.Объём.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Магазин:";
            this.Column5.HeaderText = "Магазин";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            // 
            // prescription
            // 
            this.prescription.DataPropertyName = "Рецепт:";
            this.prescription.HeaderText = "Рецепт:";
            this.prescription.Name = "prescription";
            this.prescription.ReadOnly = true;
            // 
            // expiration_date
            // 
            this.expiration_date.DataPropertyName = "Срок годности:";
            this.expiration_date.HeaderText = "Срок годности:";
            this.expiration_date.Name = "expiration_date";
            this.expiration_date.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Первичная упаковка:";
            this.Column1.HeaderText = "Первичная упаковка";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Активное вещество:";
            this.Column2.HeaderText = "Активное вещество";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Специальные свойства:";
            this.Column3.HeaderText = "Специальные свойства";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // release_form
            // 
            this.release_form.DataPropertyName = "Форма выпуска:";
            this.release_form.HeaderText = "Форма выпуска:";
            this.release_form.Name = "release_form";
            this.release_form.ReadOnly = true;
            // 
            // medicine_factory
            // 
            this.medicine_factory.DataPropertyName = "Производитель:";
            this.medicine_factory.HeaderText = "Производитель:";
            this.medicine_factory.Name = "medicine_factory";
            this.medicine_factory.ReadOnly = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(140, 9);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(741, 501);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(884, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Изменить статус заказа:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(883, 329);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(209, 21);
            this.comboBox1.TabIndex = 31;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1098, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(887, 93);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(304, 20);
            this.textBox1.TabIndex = 33;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(887, 138);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(304, 20);
            this.textBox2.TabIndex = 34;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(887, 182);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(304, 20);
            this.textBox3.TabIndex = 35;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(887, 226);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(304, 20);
            this.textBox4.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(884, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Заказчик";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(884, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Стоимость заказа";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(884, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Номер заказа";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(884, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Дата доставки";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(884, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Статус заказа";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(887, 274);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(304, 20);
            this.textBox5.TabIndex = 42;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(191, 4);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1003, 486);
            this.chart1.TabIndex = 44;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(170, 40);
            this.button2.TabIndex = 45;
            this.button2.Text = "Количество купленного товара в аптеке";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 148);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(170, 36);
            this.button3.TabIndex = 46;
            this.button3.Text = "Количество покупок в аптеке";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 95);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(170, 36);
            this.button4.TabIndex = 47;
            this.button4.Text = "Рейтинг покупателей";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeColumns = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(191, 3);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(1003, 487);
            this.dataGridView3.TabIndex = 48;
            this.dataGridView3.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellContentClick);
            // 
            // mySqlCommand1
            // 
            this.mySqlCommand1.CacheAge = 0;
            this.mySqlCommand1.Connection = null;
            this.mySqlCommand1.EnableCaching = false;
            this.mySqlCommand1.Transaction = null;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(251, 20);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(187, 21);
            this.comboBox2.TabIndex = 49;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Поиск:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(248, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Срок годности:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(9, 21);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(224, 20);
            this.textBox6.TabIndex = 52;
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged_1);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(456, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 54;
            this.label10.Text = "Производитель:";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(459, 20);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(182, 21);
            this.comboBox3.TabIndex = 53;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(653, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "Форма выпуска:";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(656, 20);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(178, 21);
            this.comboBox4.TabIndex = 55;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1028, 26);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(155, 23);
            this.button5.TabIndex = 59;
            this.button5.Text = "Сбросить фильтры";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pharmacy);
            this.tabControl1.Controls.Add(this.orders);
            this.tabControl1.Controls.Add(this.statistic);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1205, 548);
            this.tabControl1.TabIndex = 60;
            // 
            // pharmacy
            // 
            this.pharmacy.Controls.Add(this.button7);
            this.pharmacy.Controls.Add(this.label8);
            this.pharmacy.Controls.Add(this.textBox6);
            this.pharmacy.Controls.Add(this.button5);
            this.pharmacy.Controls.Add(this.label9);
            this.pharmacy.Controls.Add(this.label11);
            this.pharmacy.Controls.Add(this.comboBox4);
            this.pharmacy.Controls.Add(this.comboBox2);
            this.pharmacy.Controls.Add(this.label10);
            this.pharmacy.Controls.Add(this.comboBox3);
            this.pharmacy.Controls.Add(this.dataGridView1);
            this.pharmacy.Location = new System.Drawing.Point(4, 22);
            this.pharmacy.Name = "pharmacy";
            this.pharmacy.Padding = new System.Windows.Forms.Padding(3);
            this.pharmacy.Size = new System.Drawing.Size(1197, 522);
            this.pharmacy.TabIndex = 0;
            this.pharmacy.Text = "Лекарства в аптеке";
            this.pharmacy.UseVisualStyleBackColor = true;
            this.pharmacy.Click += new System.EventHandler(this.pharmacy_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1037, 496);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(146, 23);
            this.button7.TabIndex = 60;
            this.button7.Text = "Выгрузить в Excel";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // orders
            // 
            this.orders.Controls.Add(this.flowLayoutPanel1);
            this.orders.Controls.Add(this.button1);
            this.orders.Controls.Add(this.dataGridView2);
            this.orders.Controls.Add(this.label2);
            this.orders.Controls.Add(this.comboBox1);
            this.orders.Controls.Add(this.label3);
            this.orders.Controls.Add(this.textBox1);
            this.orders.Controls.Add(this.textBox5);
            this.orders.Controls.Add(this.textBox2);
            this.orders.Controls.Add(this.label7);
            this.orders.Controls.Add(this.textBox3);
            this.orders.Controls.Add(this.label6);
            this.orders.Controls.Add(this.textBox4);
            this.orders.Controls.Add(this.label5);
            this.orders.Controls.Add(this.label4);
            this.orders.Location = new System.Drawing.Point(4, 22);
            this.orders.Name = "orders";
            this.orders.Padding = new System.Windows.Forms.Padding(3);
            this.orders.Size = new System.Drawing.Size(1197, 522);
            this.orders.TabIndex = 1;
            this.orders.Text = "Заказы";
            this.orders.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 9);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(128, 501);
            this.flowLayoutPanel1.TabIndex = 43;
            // 
            // statistic
            // 
            this.statistic.Controls.Add(this.groupBox1);
            this.statistic.Controls.Add(this.button8);
            this.statistic.Controls.Add(this.dataGridView3);
            this.statistic.Controls.Add(this.chart1);
            this.statistic.Location = new System.Drawing.Point(4, 22);
            this.statistic.Name = "statistic";
            this.statistic.Size = new System.Drawing.Size(1197, 522);
            this.statistic.TabIndex = 2;
            this.statistic.Text = "Статистика";
            this.statistic.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 486);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите статистику";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(1048, 496);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(146, 23);
            this.button8.TabIndex = 61;
            this.button8.Text = "Выгрузить в Excel";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1044, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(166, 23);
            this.button6.TabIndex = 61;
            this.button6.Text = "Выход";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // AdminController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 569);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.tabControl1);
            this.Name = "AdminController";
            this.Text = "AdminForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminController_FormClosing);
            this.Load += new System.EventHandler(this.AdminForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.pharmacy.ResumeLayout(false);
            this.pharmacy.PerformLayout();
            this.orders.ResumeLayout(false);
            this.orders.PerformLayout();
            this.statistic.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView3;
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pharmacy;
        private System.Windows.Forms.TabPage orders;
        private System.Windows.Forms.TabPage statistic;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Наименование;
        private System.Windows.Forms.DataGridViewTextBoxColumn Стоимость;
        private System.Windows.Forms.DataGridViewTextBoxColumn Количество;
        private System.Windows.Forms.DataGridViewTextBoxColumn Объём;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn prescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn expiration_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn release_form;
        private System.Windows.Forms.DataGridViewTextBoxColumn medicine_factory;
    }
}