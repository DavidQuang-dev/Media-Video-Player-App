using MediaApp;
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
using video_media_player.UserControls;

namespace video_media_player
{
    /// <summary>
    /// Interaction logic for PlaylistsPage.xaml
    /// </summary>
    public partial class PlaylistsPage : Page
    {
        private PlaylistService _playlistService = new();
        private PlaylistSongService _playlistSongService = new();
        private SongService _songService = new();
        public TbPlaylist ChoosePlaylist { get; set; }
        public PlaylistsPage()
        {
            InitializeComponent();

        }

        public void SetChosenPlaylist(TbPlaylist playlist)
        {
            ChoosePlaylist = playlist;
            if (ChoosePlaylist != null)
            {
                PlaylistsListBox.SelectedItem = ChoosePlaylist;
                PlaylistDetail.DataContext = ChoosePlaylist;
                PlayButton.Visibility = Visibility.Visible;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            //f5 lưới
            PlaylistDetail detail = new();
            detail.ShowDialog();
            FillListBox(_playlistService.GetAllPlayList());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PlaylistsListBox.ItemsSource = _playlistService.GetAllWithSongs();
        }

        //helper function

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FillListBox(_playlistService.GetAllPlayList());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TbPlaylist? selected = PlaylistsListBox.SelectedItem as TbPlaylist;
            if (selected == null)
            {
                MessageBox.Show("Please select a playlist before deleting!!", "Select One", MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }

            MessageBoxResult answer = MessageBox.Show($"Are you sure about delete playlist {selected.PlaylistName}", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (answer == MessageBoxResult.No)
            {
                return;
            }
            _playlistService.DeletePlayList(selected);
            FillListBox(_playlistService.GetAllPlayList());
        }
        private void FillListBox(List<TbPlaylist> list)
        {
            PlaylistsListBox.ItemsSource = null;
            PlaylistsListBox.ItemsSource = list;
        }

        private void PlaylistsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlaylistsListBox.SelectedItem is TbPlaylist selectedPlaylist)
            {
                PlaylistDetail.DataContext = selectedPlaylist;
                PlayButton.Visibility = Visibility.Visible;
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistsListBox.SelectedItem is TbPlaylist selectedPlaylist)
            {
                List<TbSong> songs = _songService.GetSongsByPlaylist(selectedPlaylist);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ListSongs = songs;
                mainWindow.CurrentIndex = 0;
                mainWindow.LoadSong(0);

            }
        }

        private void SongItem_Click_1(object sender, RoutedEventArgs e)
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
