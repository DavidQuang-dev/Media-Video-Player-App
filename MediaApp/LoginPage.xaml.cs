﻿using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using video_media_player;

namespace MediaApp
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private UserService _service = new();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailAddressTextBox.Text.Trim();
            string pass = PasswordTextBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Both email and password are required!", "Field required!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TbUser? acc = _service.Authenticated(email, pass);

            if (acc == null)
            {
                MessageBox.Show("Invalid email or password!", "Wrong credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            StartWindow startWindow = new();
            startWindow.AuthenticatedUser = acc;
            this.Close();
            startWindow.ShowDialog();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RegisterPage detail = new();
            detail.ShowDialog();            
            this.Show();
            EmailAddressTextBox.Text = detail.Registered?.Email;
        }
    }
}
