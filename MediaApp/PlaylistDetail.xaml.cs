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

        //khi thêm/xóa bài hát thì lưu tạm vào 2 mảng này
        private List<TbSong> _songsToAdd = new();
        private List<TbSong> _songsToRemove = new();
        public PlaylistDetail()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongComboBox.SelectedItem != null)
            {
                var selectedItem = SongComboBox.SelectedItem as TbSong;

                if (selectedItem != null && !SongDataGrid.Items.Contains(selectedItem))
                {
                    SongDataGrid.Items.Add(selectedItem);
                    _songsToAdd.Add(selectedItem);

                    //xóa bài đc thêm vào khỏi ds bài hát cbi xóa
                    _songsToRemove.Remove(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select a song before adding!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongDataGrid.SelectedItem is TbSong selectedSong)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to remove '{selectedSong.SongName}' from this playlist?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // bài hát này (selectedSong) đc xóa khỏi data grid & thêm vào mảng cbi đc xóa
                    SongDataGrid.Items.Remove(selectedSong);
                    _songsToRemove.Add(selectedSong);
                    // xóa bài hát này khỏi mảng cbi đc add (nếu có)
                    _songsToAdd.Remove(selectedSong);
                }
            }
            else
                MessageBox.Show("Please select a song to delete!", "Select one", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TbPlaylist tbPlaylist = new()
            {
                PlaylistName = PlaylistNameTextBox.Text
            };

            if (EditedOne == null)
            {
                _playlistService.CreatePlayList(tbPlaylist);
                MessageBox.Show($"Add playlist {PlaylistNameTextBox.Text} successfully!", "Add playlist", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                tbPlaylist.PlaylistId = EditedOne.PlaylistId;
                _playlistService.UpdatePlayList(tbPlaylist);
                MessageBox.Show($"Update playlist {PlaylistNameTextBox.Text} successfully!", "Update playlist", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //lưu lại biến playlist đang edit hoặc mới tạo
            var savedPlaylist = EditedOne ?? tbPlaylist;

            // Thêm các bài hát có trong mảng _songsToAdd ở DB 
            foreach (var song in _songsToAdd)
            {
                var existingPlaylistSong = _playlistSongService.GetByPlaylistAndSong(savedPlaylist.PlaylistId, song.SongId);

                if (existingPlaylistSong == null)
                {
                    TbPlaylistSong playlistSong = new()
                    {
                        PlaylistId = savedPlaylist.PlaylistId,
                        SongId = song.SongId
                    };

                    _playlistSongService.CreatePlaylistSongs(playlistSong);
                }
            }

            // Xóa các bài hát có trong mảng _songsToRemove ở DB 
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
            MessageBoxResult result = MessageBox.Show("Are you sure ?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (EditedOne != null)
            {
                TitleTextBlock.Text = "Edit Playlist";
                PlaylistNameTextBox.Text = EditedOne.PlaylistName;

                // Load các bài hát thuộc playlist EditedOne vào SongDataGrid
                foreach (TbSong song in _songService.GetSongsByPlaylist(EditedOne))
                {
                    SongDataGrid.Items.Add(song);
                }

                // Lấy các songId đã có trong DataGrid
                var existingSongs = SongDataGrid.Items.Cast<TbSong>().Select(s => s.SongId).ToList();

                // Lấy ra các bài hát (chưa có trong playlist) và fill vào SongComboBox
                SongComboBox.ItemsSource = _songService.GetAvailableSongsForPlaylist(existingSongs);
            }
            else
            {
                TitleTextBlock.Text = "Add Playlist";
                SongComboBox.ItemsSource = _songService.GetAll();
            }

            SongComboBox.DisplayMemberPath = "SongName";
            SongComboBox.SelectedValuePath = "SongId";
        }

    }
}
