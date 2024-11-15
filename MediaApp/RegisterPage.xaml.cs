using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
using System.Windows;
using System.Windows.Controls;


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
            string userName = UserNameTextBox.Text.Trim();
            string email = EmailAddressTextBox.Text.Trim();
            string pass = PasswordBox.Password.Trim();
            string confirmPass = ConfirmPasswordBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(confirmPass))
            {
                MessageBox.Show("All fields are required!", "Field required!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pass != confirmPass)
            {
                MessageBox.Show("Passwords do not match!", "Password mismatch!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_userService.CheckEmailExists(email))
            {
                MessageBox.Show("Email already exists!", "Email exists!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TbUser user = new()
            {
                UserName = userName,
                Email = email,
                Password = pass
            };
            _userService.CreateUser(user);
            MessageBox.Show("User created successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
