using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacy
{
    public partial class AuthorizationForm : Form
    {
        static public string loginactive;

        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBConnection.ConnectionDB();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Кнопка авторизоваться
        {
            if(textBox1.Text != "" && textBox2.Text != "")
            {
                AuthorizationService.AuthorizationUser(textBox1.Text, textBox2.Text);
                OpenForm();
            }
            else
            {
                label3.Text = "Заполните все обязательные поля!";
            }
            
        }

        public void OpenForm()
        {
            switch (AuthorizationService.role)
            {
                case ("Пользователь"):
                    this.Hide();
                    UserForm form = new UserForm();
                    form.Show();
                    break;
                case ("Администратор"):
                    this.Hide();
                    AdminForm form1 = new AdminForm();
                    form1.Show();
                    break;
                case ("Супер администратор"):
                    this.Hide();
                    RootForm1 form2 = new RootForm1();
                    form2.Show();
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == false)
            {
                textBox2.UseSystemPasswordChar = true;
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
            }

        }
    }
}
