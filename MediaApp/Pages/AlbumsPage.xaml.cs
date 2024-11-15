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

namespace video_media_player
{
    /// <summary>
    /// Interaction logic for AlbumsPage.xaml
    /// </summary>
    public partial class AlbumsPage : Page
    {
        private AlbumService _albumService = new();
        private SongService _songService = new();

        public AlbumsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AlbumsListBox.ItemsSource = _albumService.GetAllAlbums();
            //SongItemsControl.ItemsSource = _songService.GetSongsByAlbum(_albumService.GetAllAlbums()[0]);
        }

        private void AlbumsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Kiểm tra xem có album nào được chọn hay không
            if (AlbumsListBox.SelectedItem is TbAlbum selectedAlbum)
            {
                PlayAlbumButton.Visibility = Visibility.Visible;
                // Cập nhật dữ liệu trong AlbumDetail nếu cần
                AlbumDetail.DataContext = selectedAlbum; // Cập nhật DataContext để hiển thị chi tiết album
            }
        }


        private void PlayAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem album có được chọn hay không
            if (AlbumsListBox.SelectedItem is TbAlbum selectedAlbum)
            {
                // Thực hiện hành động với album đã chọn
                //MessageBox.Show($"Đang phát album: {selectedAlbum.Title}");
                List<TbSong> songs = _songService.GetSongsByAlbum(selectedAlbum);
                //MessageBox.Show("Số lượng trong danh sách : " + songs.Count);
                // Nếu cần, bạn có thể gọi một phương thức trong ViewModel để phát album
                //viewModel.PlayAlbum(selectedAlbum);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ListSongs = songs;
                mainWindow.CurrentIndex = 0;
                mainWindow.LoadSong(0);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một album để phát.");
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.WindowState == WindowState.Normal)
            {
                InfoStackPanel.SetValue(Grid.ColumnProperty, 0);
                InfoStackPanel.VerticalAlignment = VerticalAlignment.Bottom;
            }
            else
            {
                InfoStackPanel.SetValue(Grid.ColumnProperty, 1);
                InfoStackPanel.VerticalAlignment = VerticalAlignment.Center;
            }
            AlbumDetailNameTextBlock.MaxWidth = 600;

        }

        private void SongItem_Click(object sender, RoutedEventArgs e)
        {
            video_media_player.UserControls.SongItem songItem = (video_media_player.UserControls.SongItem)sender;
            string songName = songItem.Title.ToString();
            if (!string.IsNullOrEmpty(songName))
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.SetChosenSong(_songService.GetSongByName(songName));
            }
        }
    }
}
