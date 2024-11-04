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
    /// Interaction logic for PlaylistDetail.xaml
    /// </summary>
    public partial class PlaylistDetail : Window
    {
        private PlaylistService _playlistService = new();
        private SongService _songService = new();
        private PlaylistSongService _playlistSongService = new();
        private ArtistService _artistService = new();
        public TbPlaylist EditedOne { get; set; }
        public PlaylistDetail()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongComboBox != null)
            {
                if (!SongDataGrid.Items.Contains((TbSong)SongComboBox.SelectedItem))
                    SongDataGrid.Items.Add(SongComboBox.SelectedItem);
            }
            else
            {
                MessageBox.Show("Please select a song before add!!");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TbPlaylist tbPlaylist = new();
            tbPlaylist.PlaylistName = PlaylistNameTextBox.Text;
            if (EditedOne == null)
            {
                _playlistService.CreatePlayList(tbPlaylist);
                MessageBox.Show($"Add playlist {PlaylistNameTextBox.Text} successfully !", "Add playlist", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                tbPlaylist.PlaylistId = EditedOne.PlaylistId;
                _playlistService.UpdatePlayList(tbPlaylist);
                MessageBox.Show($"Update playlist {PlaylistNameTextBox.Text} successfully !", "Update playlist", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // Lấy lại Playlist đã tạo hoặc cập nhật từ DB
            var savedPlaylist = EditedOne;
            if (savedPlaylist != null)
            {
                foreach (var item in SongDataGrid.Items)
                {
                    if (item is TbSong song)
                    {
                        // Tạo mới liên kết giữa Playlist và Song
                        TbPlaylistSong playlistSong = new()
                        {
                            PlaylistId = savedPlaylist.PlaylistId,
                            SongId = song.SongId
                        };
                        if (playlistSong.Equals(EditedOne.TbPlaylistSongs))
                        {
                            playlistSong.PlaylistId = EditedOne.PlaylistId;
                            _playlistSongService.UpdatePlaylistSongs(playlistSong);
                        } else
                        {
                            _playlistSongService.CreatePlaylistSongs(playlistSong);
                        }
                        // Sử dụng _playlistSongService để lưu vào DB
                    }
                }
            }

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure ?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //SongDataGrid.ItemsSource = _artistService.GetAll();
            //SongDataGrid.DisplayMemberPath = "ArtistName";
            //SongDataGrid.SelectedValuePath = "ArtistId";
            SongComboBox.ItemsSource = _songService.GetAll();
            SongComboBox.DisplayMemberPath = "SongName";
            SongComboBox.SelectedValuePath = "SongId";

            if (EditedOne != null)
            {
                TitleTextBlock.Text = "Edit Playlist";
                MessageBox.Show($"{EditedOne.PlaylistName}");
                PlaylistNameTextBox.Text = EditedOne.PlaylistName;
                foreach (TbSong song in _songService.GetSongsByPlaylist(EditedOne))
                {
                    SongDataGrid.Items.Add(song);
                }
                
            } 
            else
            {
                TitleTextBlock.Text = "Add Playlist";
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a song to delete!", "Select one", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Get the selected song
                TbSong selectedSong = SongDataGrid.SelectedItem as TbSong;

                if (selectedSong != null)
                {
                    // Confirm deletion
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to remove '{selectedSong.SongName}' from this playlist?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Find the playlist-song association to remove
                        var playlistSong = _playlistSongService.GetByPlaylistAndSong(EditedOne.PlaylistId, selectedSong.SongId);
                        
                        MessageBox.Show($"Giá trị của playlistSong: {playlistSong.PlaylistSongsId}");
                        if (playlistSong != null)
                        {
                            // Delete the association from the database
                            _playlistSongService.DeletePlaylistSongs(playlistSong);

                            // Remove the song from the DataGrid
                            SongDataGrid.Items.Remove(selectedSong);

                            // Refresh the ComboBox items if needed
                            SongComboBox.ItemsSource = _songService.GetAll();
                        }

                        
                    }
                }
            }
        }


    }
}
