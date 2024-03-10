using pharmacy.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AuthorizationController : Form
    {
        private AuthorizationService AuthorizationService;
        private User authorizedUser;
        private UserController userForm;
        private AdminController adminForm;
        private RootController rootForm;

        public AuthorizationController()
        {
            InitializeComponent();
            AuthorizationService = AuthorizationService.Instance;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBConnection.ConnectionDB();
            button2.Image = Properties.Resources.close_eye;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Кнопка авторизоваться
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            if (login != null && password != null)
            {
                authorizedUser = AuthorizationService.AuthorizationUser(login, password);
                if (authorizedUser != null)
                {
                    OpenForm();
                }
            }
            else
            {
                label3.Text = "Заполните все обязательные поля!";
            }           
        }

       


        public void OpenForm()
        {
            switch (authorizedUser.Role)
            {
                case ("Пользователь"):
                    userForm = new UserController(authorizedUser);
                    userForm.Show();
                    this.Hide();
                    break;
                case ("Администратор"): 
                    
                    adminForm = new AdminController(authorizedUser);
                    adminForm.Show();
                    this.Hide();
                    break;
                case ("Супер администратор"):
                    
                    rootForm = new RootController();
                    rootForm.Show();
                    this.Hide();
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
                button2.Image = Properties.Resources.close_eye;
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
                button2.Image = Properties.Resources.open_eye;
            }

        }

        private void AuthorizationController_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (userForm != null) 
            {
                userForm.Close();
            }

            if (adminForm != null)
            {
                adminForm.Close();
            }

            if (rootForm != null)
            {
                rootForm.Close();
            }
        }
    }
}
