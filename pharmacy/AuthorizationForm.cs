﻿using pharmacy.controller;
using pharmacy.data;
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
        private AuthorizationController AuthorizationController;
        private User authorizedUser;
        public AuthorizationForm()
        {
            InitializeComponent();
            AuthorizationController = new AuthorizationController();
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
            string login = textBox1.Text;
            string password = textBox2.Text;
            authorizedUser = AuthorizationController.Authorize(login, password);
            if (authorizedUser != null)
            {
                OpenForm();
            }
            else
            {
                label3.Text = AuthorizationController.CheckLoginPassword(login, password);
            }           
        }

        public void OpenForm()
        {
            switch (authorizedUser.Role)
            {
                case ("Пользователь"):
                    this.Hide();
                    UserForm userForm = new UserForm();
                    userForm.Show();
                    break;
                case ("Администратор"):
                    this.Hide();
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                    break;
                case ("Супер администратор"):
                    this.Hide();
                    RootForm1 rooForm = new RootForm1();
                    rooForm.Show();
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
