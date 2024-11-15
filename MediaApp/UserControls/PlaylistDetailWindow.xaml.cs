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
        public TbPlaylist EditedOne { get; set; }
        public ObservableCollection<TbSong> Songs { get; set; }
        public PlaylistDetailWindow(SongService songService) { 
            InitializeComponent(); 
            _songService = songService; 
            LoadSongs(); 
            DataContext = this; 
        }

        public PlaylistDetailWindow()
        {
            InitializeComponent();
        }

        private void LoadSongs() { 
            var songs = _songService.GetAll(); 
            Songs = new ObservableCollection<TbSong>(songs); 
        }

        private void SavePlaylistButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
