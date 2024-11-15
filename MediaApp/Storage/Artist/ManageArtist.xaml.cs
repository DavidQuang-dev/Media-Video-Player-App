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
    /// Interaction logic for ManageArtist.xaml
    /// </summary>
    public partial class ManageArtist : Window
    {
        private ArtistService _service = new ();
        private SongService songService = new();
        public ManageArtist()
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
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DetailArtist detailArtist = new(); 
            detailArtist.ShowDialog();
            FillData(_service.GetAll());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ArtistsDataGrid.ItemsSource = _service.GetAll();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            TbArtist? selected = ArtistsDataGrid.SelectedItem as TbArtist;
            if (selected == null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Please select an artist", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    return;
                }
            }

            DetailArtist detailArtist = new();
            detailArtist.EditArtist = selected;
            detailArtist.ShowDialog();
            FillData(_service.GetAll());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TbArtist? selected = ArtistsDataGrid.SelectedItem as TbArtist;
            if (selected == null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Please select an artist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if(messageBoxResult == MessageBoxResult.OK)
                {
                    return;
                }
            }

            MessageBoxResult result = MessageBox.Show("Are You Sure ?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _service.Delete(selected);
                FillData(_service.GetAll());
            }
        }

        private void FillData(List<TbArtist> tbArtists)
        {
            ArtistsDataGrid.ItemsSource = null;
            ArtistsDataGrid.ItemsSource = tbArtists;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                FillData(_service.GetAll());
                return;
            }

            var filteredArtists = _service.GetArtistsByName(searchQuery);
            if (filteredArtists == null || filteredArtists.Count == 0)
            {
                MessageBox.Show($"No artist found with the keyword '{searchQuery}'.", "No Results", MessageBoxButton.OK, MessageBoxImage.Information);
                FillData(new List<TbArtist>()); // Xóa dữ liệu khỏi DataGrid nếu không có kết quả
            }
            else
            {
                FillData(filteredArtists); // Hiển thị danh sách kết quả
            }
        }
        // Định nghĩa phương thức SearchTextBox_TextChanged
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Thêm logic tìm kiếm ở đây
            // Ví dụ: lấy text từ TextBox và thực hiện tìm kiếm hoặc lọc
            var searchText = ((TextBox)sender).Text;
            // TODO: Thực hiện tìm kiếm dựa trên searchText
        }
    }
}
