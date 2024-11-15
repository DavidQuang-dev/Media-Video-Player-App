using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using video_media_player.UserControls;

namespace MediaApp.UserControls
{
    /// <summary>
    /// Interaction logic for PlaylistDetailWindow.xaml
    /// </summary>
    public partial class PlaylistDetailWindow : Window
    {
        private SongService _songService = new();
        private PlaylistService _playlistService = new();
        private PlaylistSongService _playlistSongService = new();
        public TbPlaylist EditedOne { get; set; }
        public ObservableCollection<TbSong> Songs { get; set; }
        // Temporary lists to track songs to add or remove
        private List<TbSong> _songsToAdd = new();
        private List<TbSong> _songsToRemove = new();
        public PlaylistDetailWindow()
        {
            InitializeComponent();
            LoadSongs();
            DataContext = this;
        }

        private void LoadSongs() { 
            var songs = _songService.GetAll(); 
            Songs = new ObservableCollection<TbSong>(songs); 
        }

        private void SavePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            var playlist = new TbPlaylist
            {
                PlaylistName = PlaylistNameTextBox.Text
            };

            if (EditedOne == null)
            {
                _playlistService.CreatePlayList(playlist);
                MessageBox.Show($"Playlist '{PlaylistNameTextBox.Text}' added successfully.", "Add Playlist", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                playlist.PlaylistId = EditedOne.PlaylistId;
                _playlistService.UpdatePlayList(playlist);
                MessageBox.Show($"Playlist '{PlaylistNameTextBox.Text}' updated successfully.", "Update Playlist", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            var savedPlaylist = EditedOne ?? playlist;
            foreach (var song in _songsToAdd)
            {
                var existingPlaylistSong = _playlistSongService.GetByPlaylistAndSong(savedPlaylist.PlaylistId, song.SongId);
                if (existingPlaylistSong == null)
                {
                    _playlistSongService.CreatePlaylistSongs(new TbPlaylistSong
                    {
                        PlaylistId = savedPlaylist.PlaylistId,
                        SongId = song.SongId
                    });
                }
            }

            foreach (var song in _songsToRemove)
            {
                var playlistSong = _playlistSongService.GetByPlaylistAndSong(savedPlaylist.PlaylistId, song.SongId);
                if (playlistSong != null)
                {
                    _playlistSongService.DeletePlaylistSongs(playlistSong);
                }
            }

            this.Close();
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(EditedOne == null)
            {
                TitleTextBlock.Text = "Add a playlist";

            } else
            {
                TitleTextBlock.Text = $"Edit playlist {EditedOne.PlaylistName}";
                PlaylistNameTextBox.Text = EditedOne.PlaylistName;

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TbSong selectedSong)
            {
                if (checkBox.IsChecked == true)
                {
                    _songsToAdd.Add(selectedSong);
                    _songsToRemove.Remove(selectedSong);
                }
                else
                {
                    _songsToRemove.Add(selectedSong);
                    _songsToAdd.Remove(selectedSong);
                }
            }
        }
    }
}
