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
using System.Windows.Shapes;

namespace MediaApp
{
    /// <summary>
    /// Interaction logic for DetailArtist.xaml
    /// </summary>
    public partial class DetailArtist : Window
    {
        private readonly ArtistService _service = new();
        public TbArtist EditArtist { get; set; }
        public DetailArtist()
        {
            InitializeComponent();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are You Sure ?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckValidate())
            {
                return;
            } 

            TbArtist artist = EditArtist ?? new();
            

            artist.ArtistName = txtArtistName.Text;
            artist.DataOfBirth = date.SelectedDate.Value;
            artist.Description = txtDescription.Text;
            if (EditArtist == null)
            {
                _service.Create(artist);
            }
            else
            {
                _service.Update(artist);
            }
            MessageBox.Show("Artist has been saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (EditArtist != null)
            {
                txtArtistName.Text = EditArtist.ArtistName;
                date.Text = EditArtist.DataOfBirth.ToString();
                txtDescription.Text = EditArtist.Description;
                Header.Text = $"Update Artist {EditArtist.ArtistName}";
            }
        }

        private bool CheckValidate()
        {
            // Kiểm tra nếu artist name bị để trống
            if (string.IsNullOrWhiteSpace(txtArtistName.Text))
            {
                System.Windows.MessageBox.Show("Artist Name cannot be empty. Please enter a name for the artist.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtArtistName.Focus();
                return false;
            }

            if (date.SelectedDate.HasValue)
            {
                DateTime birthDate = date.SelectedDate.Value;
                DateTime today = DateTime.Today;
                int age = today.Year - birthDate.Year;

                // Nếu nghệ sĩ chưa đủ 16 tuổi
                if (birthDate > today.AddYears(-age)) age--; // Điều chỉnh nếu chưa đến sinh nhật trong năm hiện tại
                if (age < 16)
                {
                    System.Windows.MessageBox.Show("Artist must be at least 16 years old.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    date.Focus();
                    return false;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a valid date of birth.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                date.Focus();
                return false;
            }

            // Kiểm tra độ dài của artist name không vượt quá 30 ký tự
            if (txtArtistName.Text.Length > 30)
            {
                System.Windows.MessageBox.Show("Artist Name cannot exceed 30 characters. Please shorten the name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtArtistName.Focus();
                return false;
            }

            // Kiểm tra artist name không được chứa toàn bộ là số
            if (txtArtistName.Text.All(char.IsDigit))
            {
                System.Windows.MessageBox.Show("Artist Name cannot contain only numbers. Please enter a valid name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtArtistName.Focus();
                return false;
            }

            //kiểm tra chữ cái đầu tên artist phải là chữ hoa
            if (!char.IsUpper(txtArtistName.Text[0]))
            {
                System.Windows.MessageBox.Show("The first letter of the Artist Name must be uppercase. Please correct the name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtArtistName.Focus();
                return false;
            }

            // Kiểm tra độ dài của description không vượt quá 50 ký tự
            if (txtDescription.Text.Length > 50)
            {
                System.Windows.MessageBox.Show("Description cannot exceed 50 characters. Please shorten the description.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDescription.Focus();
                return false;
            }

            // Kiểm tra nếu description bị để trống hoặc ít hơn 10 ký tự
            if (string.IsNullOrWhiteSpace(txtDescription.Text) || txtDescription.Text.Length < 10)
            {
                System.Windows.MessageBox.Show("Description cannot be empty and must contain at least 10 characters. Please provide more details.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDescription.Focus();
                return false;
            }

            if (!date.SelectedDate.HasValue)
            {
                System.Windows.MessageBox.Show("Date of birth is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDescription.Focus();
                return false;
            }
            return true;
        }

    }
}
