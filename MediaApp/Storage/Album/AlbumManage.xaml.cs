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
    /// Interaction logic for AlbumManage.xaml
    /// </summary>
    public partial class AlbumManage : Window
    {
        private AlbumService _albumService = new();
        private SongService _songService = new();
        public AlbumManage()
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
            AlbumDetail albumDetail = new AlbumDetail();
            albumDetail.ShowDialog();
            Helper(_albumService.GetAllAlbums());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AlbumDataGrid.ItemsSource = _albumService.GetAllAlbums();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlbumDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an album before update", "Select", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AlbumDetail albumDetail = new AlbumDetail();
            albumDetail.EditOne = AlbumDataGrid.SelectedItem as TbAlbum;
            albumDetail.ShowDialog();
            Helper(_albumService.GetAllAlbums());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAlbum = AlbumDataGrid.SelectedItem as TbAlbum;
            if (selectedAlbum == null)
                MessageBox.Show("Vui lòng chọn 1 album để xóa", "Quit");
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure to delete album : " + selectedAlbum.Title, "Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                    return;
                else
                {
                    List<TbSong> deleteSongs = _songService.GetSongsByAlbum(AlbumDataGrid.SelectedItem as TbAlbum);
                    foreach (var item in deleteSongs)
                    {
                        TbSong song = item as TbSong;
                        song.AlbumId = null;
                        _songService.Update(song);
                    }
                    _albumService.DeleteAlbum(AlbumDataGrid.SelectedItem as TbAlbum);
                    Helper(_albumService.GetAllAlbums());
                }
            }
        }

        private void Helper(List<TbAlbum> arr)
        {
            if (arr != null)
            {
                AlbumDataGrid.ItemsSource = null;
                AlbumDataGrid.ItemsSource = arr;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchAlbumTextBox.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox
            if (string.IsNullOrWhiteSpace(searchQuery)) // Nếu không có từ khóa tìm kiếm
            {
                System.Windows.MessageBox.Show("Please enter a search term.", "No Search Term", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Giả sử _playlistService có phương thức GetPlaylistsByName để tìm Playlist theo tên
            var filteredAlbum = _albumService.GetAlbumByName(searchQuery);
            if (filteredAlbum == null || filteredAlbum.Count == 0) // Nếu không tìm thấy Playlist nào
            {
                System.Windows.MessageBox.Show($"No playlists found with the keyword '{searchQuery}'.", "No Results", MessageBoxButton.OK, MessageBoxImage.Information);
                FillData(new List<TbAlbum>()); // Xóa dữ liệu khỏi DataGrid nếu không có kết quả
            }
            else
            {
                FillData(filteredAlbum); // Chuyển TbPlaylist thành List<TbPlaylist>

            }
        }

        private void SearchAlbumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = ((System.Windows.Controls.TextBox)sender).Text.Trim(); // Lấy văn bản từ TextBox            
        }

        private void FillData(List<TbAlbum> songs)
        {
            // Cập nhật UI với danh sách Playlist
            AlbumDataGrid.ItemsSource = songs;
        }
    }
}
