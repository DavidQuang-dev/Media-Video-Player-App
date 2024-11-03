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

            if (EditedOne == null)
            {
                _playlistService.CreatePlayList(tbPlaylist);
                MessageBox.Show($"Thêm album {PlaylistNameTextBox.Text} thành công !", "Add playlist", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _playlistService.UpdatePlayList(tbPlaylist);
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
            if(EditedOne != null)
            {
                PlaylistNameTextBox.Text = EditedOne.PlaylistName   .ToString();
            }
        }
    }
}
