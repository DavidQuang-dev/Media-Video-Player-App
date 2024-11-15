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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaApp
{
    /// <summary>
    /// Interaction logic for ManageSong.xaml
    /// </summary>
    public partial class ManageSong : Window
    {
        private SongService _service = new();
        public ManageSong()
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
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DetailSong _detailSong = new();
            _detailSong.ShowDialog();
            FillData(_service.GetAll());
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            TbSong? selected = SongsDataGrid.SelectedItem as TbSong;
            if (selected != null)
            {
                DetailSong _detailSong = new();
                _detailSong.EditSong = selected;
                _detailSong.ShowDialog();
                FillData(_service.GetAll());
            }
            else
            {
                MessageBoxResult message = System.Windows.MessageBox.Show("Please select a song to update", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TbSong? selected = SongsDataGrid.SelectedItem as TbSong;
            if (selected != null)
            {
                MessageBoxResult message = System.Windows.MessageBox.Show("Are You Sure ?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (message == MessageBoxResult.No)
                {
                    return;
                }
                _service.Delete(selected);
                FillData(_service.GetAll());
            }
            else
            {
                MessageBoxResult message = System.Windows.MessageBox.Show("Please select a song to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SongsDataGrid.ItemsSource = _service.GetAll();
        }

        private void FillData(List<TbSong> tbSongs)
        {
            SongsDataGrid.ItemsSource = null;
            SongsDataGrid.ItemsSource = tbSongs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchSongsTextBox.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox
            if (string.IsNullOrWhiteSpace(searchQuery)) // Nếu không có từ khóa tìm kiếm
            {
                FillData(_service.GetAll());
                return;
            }

            // Giả sử _playlistService có phương thức GetPlaylistsByName để tìm Playlist theo tên
            var filteredSongs = _service.GetSongsByName(searchQuery);
            if (filteredSongs == null || filteredSongs.Count == 0) // Nếu không tìm thấy Playlist nào
            {
                System.Windows.MessageBox.Show($"No playlists found with the keyword '{searchQuery}'.", "No Results", MessageBoxButton.OK, MessageBoxImage.Information);
                FillData(new List<TbSong>()); // Xóa dữ liệu khỏi DataGrid nếu không có kết quả
            }
            else
            {
                FillData(filteredSongs); // Chuyển TbPlaylist thành List<TbPlaylist>

            }
        }
        private void SearchSongsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = ((System.Windows.Controls.TextBox)sender).Text.Trim(); // Lấy văn bản từ TextBox            
        }

        //private void FillData(List<TbSong> songs)
        //{
        //    // Cập nhật UI với danh sách Playlist
        //    SongsDataGrid.ItemsSource = songs;
        //}

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchSongsTextBox.Text = string.Empty;
        }
    }
}
