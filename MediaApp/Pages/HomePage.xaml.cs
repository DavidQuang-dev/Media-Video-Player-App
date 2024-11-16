using MediaApp;
using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly SongService songService = new();
        private readonly PlaylistService playlistService = new();
        public TbSong ChooseSong { get; set; }
        public HomePage()
        {
            InitializeComponent();
        }
        private double GetDurationFromUrl(string url)
        {
            using (var mf = new MediaFoundationReader(url))
            {
                return mf.TotalTime.TotalSeconds;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<TbSong> songs = songService.GetAllSongs();
            int number = 0;
            foreach (var song in songs)
            {
                string filePath = song.FilePath;
                if (string.IsNullOrEmpty(filePath))
                {
                    // Handle the case where the file path is invalid or the file could not be downloaded
                    continue;
                }

                var songItem = new video_media_player.UserControls.SongItem
                {
                    Title = song.SongName,
                    Number = (++number).ToString(),
                    Time = ConvertTimeFormat(GetDurationFromUrl(filePath)),
                    Tag = filePath
                };
                songItem.Click += SongItem_Click;
                SongItemList.Children.Add(songItem);
            }

            List<TbSong> popularSongs = songService.GetPopularSong();
            foreach (var song in popularSongs)
            {
                var popularSongItem = new video_media_player.UserControls.PopularSong
                {
                    Title = song.SongName,
                    Time = ConvertTimeFormat(GetDurationFromUrl(song.FilePath)),
                    Tag = song.FilePath
                };
                popularSongItem.Click += PopularSongItem_Click;
                PopularSongItemList.Children.Add(popularSongItem);
            }
            PlaylistsListBox.ItemsSource = playlistService.Get2Playlist();
        }

        private void SongItem_Click(object sender, RoutedEventArgs e)
        {
            video_media_player.UserControls.SongItem songItem = (video_media_player.UserControls.SongItem)sender;
            string songName = songItem.Title.ToString();
            if (!string.IsNullOrEmpty(songName))
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.SetChosenSong(songService.GetSongByName(songName));
                }
                else
                {
                    // Handle the case where MainWindow is not set or is of a different type
                    MessageBox.Show("MainWindow is not set correctly.");
                }
            }
        }
        private void PopularSongItem_Click(object sender, RoutedEventArgs e)
        {
            video_media_player.UserControls.PopularSong popularSong = (video_media_player.UserControls.PopularSong)sender;
            string songName = popularSong.Title.ToString();
            if (!string.IsNullOrEmpty(songName))
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.SetChosenSong(songService.GetSongByName(songName));
            }
        }

        private void Playlist_Click(object sender, RoutedEventArgs e)
        {
            if (sender is video_media_player.UserControls.Playlist playlist)
            {
                string playlistName = playlist.Title;
                if (!string.IsNullOrEmpty(playlistName))
                {
                    PlaylistsPage playlistsPage = new();
                    // playlistsPage.SetChosenPlaylist(playlistService.GetPlaylistByName(playlistName));
                    NavigationService.Navigate(playlistsPage);
                }
            }
        }

        private string ConvertTimeFormat(double value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            string timeFormated = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            return timeFormated;
        }

        private void PlaylistsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlaylistsListBox.SelectedItem is TbPlaylist selectedPlaylist)
            {
                PlaylistsPage playlistsPage = new();
                NavigationService.Navigate(playlistsPage);
                playlistsPage.SetChosenPlaylist(selectedPlaylist);
            }
        }
    }
}
