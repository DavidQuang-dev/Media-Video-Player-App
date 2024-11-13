using MediaApp.BLL.Services;
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

namespace MediaApp
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        private UserService _userService = new();
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UserNameTextBox.Text;
            string email = EmailAddressTextBox.Text;
            string pass = PasswordTextBox.Password;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Both email and password are required!", "Field required!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_userService.CheckEmailExists(email))
            {
                MessageBox.Show("Email already exists!", "Duplicate Email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            TbUser newUser = new TbUser { UserName = username, Email = email, Password = pass, Role = "customer" };
            _userService.CreateUser(newUser);
            MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoginPage loginPage = new();
            loginPage.Show();
            this.Close();
        }
    }
}
