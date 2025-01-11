using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
using NAudio.Wave;
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
using System.Windows.Threading;

namespace video_media_player
{
    /// <summary>
    /// Interaction logic for SongsPage.xaml
    /// </summary>
    public partial class SongsPage : Page
    {
        private SongService _songService = new();
        public TbSong ChooseSong { get; set; }

        public bool IsTyping { get; set; }

        public SongsPage()
        {
            InitializeComponent();
        }

        public string Artist { get; internal set; }
        private double GetDurationFromUrl(string url)
        {
            Dictionary<string, double> _cache = new Dictionary<string, double>();
            // Check if duration is already cached
            if (_cache.ContainsKey(url))
                return _cache[url];

            // If not cached, fetch the duration
            using (var mf = new MediaFoundationReader(url))
            {
                double duration = mf.TotalTime.TotalSeconds;
                _cache[url] = duration; // Cache the result
                return duration;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<TbSong> songs = _songService.GetAllSongs();
            int number = 0;
            foreach (var song in songs)
            {
                var songItem = new video_media_player.UserControls.SongItem
                {
                    Title = song.SongName,
                    Number = (++number).ToString(),
                    Time = ConvertTimeFormat(GetDurationFromUrl(song.FilePath)),
                    Tag = song.FilePath
                };
                songItem.Click += SongItem_Click;
                SongItemList.Children.Add(songItem);
            }
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

        private string ConvertTimeFormat(double value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            string timeFormated = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            return timeFormated;
        }

        private void SearchSongTextBox_TextChanged(Object sender, TextChangedEventArgs e)
        {
            IsTyping = true;
            string searchText = ((System.Windows.Controls.TextBox)sender).Text.Trim(); // Lấy văn bản từ TextBox    
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchSongTextBox.Text = string.Empty;
        }

        private void FillData(List<TbSong> songList)
        {
            if (songList == null) return;
            SongItemList.Children.Clear();
            int number = 0;
            foreach (var song in songList)
            {
                var songItem = new video_media_player.UserControls.SongItem
                {
                    Title = song.SongName,
                    Number = (++number).ToString(),
                    Time = ConvertTimeFormat(TagLib.File.Create(song.FilePath).Properties.Duration.TotalSeconds),
                    Tag = song.FilePath
                };
                songItem.Click += SongItem_Click;
                SongItemList.Children.Add(songItem);
            }
        }

        private void SearchButton_Click(Object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchSongTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                FillData(_songService.GetAll());
                return;
            }

            var filteredSongs = _songService.GetSongsByName(searchQuery);
            if (filteredSongs == null || filteredSongs.Count == 0)
            {
                System.Windows.MessageBox.Show($"No songs found with the keyword '{searchQuery}'.", "No Results", MessageBoxButton.OK, MessageBoxImage.Information);
                FillData(new List<TbSong>());
            }
            else
            {
                FillData(filteredSongs); // Chuyển TbPlaylist thành List<TbPlaylist>
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow.WindowState == WindowState.Maximized)
            {
                SongListBorder.MaxHeight = 600;
            }
            else
            {
                SongListBorder.MaxHeight = 350;
            }
        }
        private void SearchSongTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsTyping = true;
        }
    }
}
