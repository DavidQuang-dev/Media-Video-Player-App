using MahApps.Metro.IconPacks;
using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using NAudio.Wave;
using MediaApp;

namespace video_media_player
{
    public enum PlaybackMode
    {
        RepeatOff,
        RepeatOn,
        RepeatOne
    }

    public enum PlayMode
    {
        Sequential,
        Shuffle
    }

    public partial class MainWindow : Window
    {
        public List<TbSong> ListSongs { get; set; }
        public TbSong ChooseSong { get; set; }
        private readonly SongService _songService = new();
        private PlaybackMode backMode = PlaybackMode.RepeatOff;
        private PlayMode playMode = PlayMode.Sequential;
        public int CurrentIndex { get; set; }

        private readonly DispatcherTimer _timer;
        private Mp3FileReader _reader;
        public TbUser AuthenticatedUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ListSongs = new List<TbSong>();
            MainFrame.Navigate(new HomePage());
            CurrentIndex = 0;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //case: user chưa login
            if(AuthenticatedUser == null)
            {
                LoginPage loginPage = new();
                loginPage.ShowDialog();
                this.Close();
            }
        }
        public void SetChosenSong(TbSong song)
        {
            ChooseSong = song;
            if (ChooseSong != null)
            {
                LoadSingleSong(ChooseSong);
            }

        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToggleWindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                MainBorder.CornerRadius = new CornerRadius(40);
                PlayerBorder.CornerRadius = new CornerRadius(40, 0, 40, 0);
                // Cài đặt lại định nghĩa cột cho trạng thái Normal
                SongNameTextBlock.MaxWidth = 300;
                TimeGrid.Width = 380;
                //TimeSlider.Width = 350;
                VolumeGrid.HorizontalAlignment = HorizontalAlignment.Left;
                FooterGrid.ColumnDefinitions.Clear();
                FooterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                FooterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                FooterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            else
            {
                WindowState = WindowState.Maximized;
                MainBorder.CornerRadius = new CornerRadius(0);
                PlayerBorder.CornerRadius = new CornerRadius(40, 0, 0, 0);
                SongNameTextBlock.MaxWidth = 500;
                TimeGrid.Width = 600;
                //TimeSlider.Width = 600;
                VolumeGrid.HorizontalAlignment = HorizontalAlignment.Left;
                FooterGrid.ColumnDefinitions.Clear();
                FooterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                FooterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
                FooterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }

        private void PlaylistsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PlaylistsPage());
        }

        private void AlbumsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AlbumsPage());
        }

        private void SongsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SongsPage());
        }
        private void MVButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MusicVideosPage());
        }
        private void StoreButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StorePage());
        }

        private void VolumeButton_Click(object sender, RoutedEventArgs e)
        {
            volumePopup.IsOpen = !volumePopup.IsOpen;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayIcon.Kind == PackIconMaterialKind.Pause)
            {
                PausePlayback();
            }
            else
            {
                StartPlayback();
            }
        }

        private void PausePlayback()
        {
            PlayIcon.Kind = PackIconMaterialKind.Play;
            PlayerMediaElement.Pause();
            _timer.Stop();
        }

        private void StartPlayback()
        {
            if (ListSongs.Count == 0)
            {
                PlayFirstSong();
            }
            else
            {
                PlayerMediaElement.Play();
            }

            PlayIcon.Kind = PackIconMaterialKind.Pause;
            _timer.Start();
        }

        private void PlayFirstSong()
        {
            ListSongs = _songService.GetAll();
            LoadSong(CurrentIndex);
        }

        private void PlayNextSong()
        {
            if (ListSongs.Count == 0) return;

            Random random = new Random();
            switch (backMode)
            {
                case PlaybackMode.RepeatOff:
                    if (playMode == PlayMode.Sequential)
                    {
                        CurrentIndex = (CurrentIndex + 1) % ListSongs.Count;
                        if (CurrentIndex == 0) // Reached the end
                        {
                            ListSongs.Clear();
                            PausePlayback();
                            return;
                        }
                    }
                    else
                    {
                        if ((CurrentIndex + 1) % ListSongs.Count == 0)
                        {
                            ListSongs.Clear();
                            PausePlayback();
                            return;
                        }
                        CurrentIndex = randomIndex();
                    }
                    break;
                case PlaybackMode.RepeatOn:
                    CurrentIndex = playMode == PlayMode.Sequential ? (CurrentIndex + 1) % ListSongs.Count : randomIndex();
                    break;
                case PlaybackMode.RepeatOne:
                    {
                        if (ChooseSong != null)
                        {
                            LoadSingleSong(ChooseSong);
                            return;
                        }
                    }
                    break;
            }
            LoadSong(CurrentIndex);
        }

        private int randomIndex()
        {
            Random random = new Random();
            int index;
            do
            {
                index = random.Next(ListSongs.Count);
            } while (index == CurrentIndex);
            return index;
        }

        private void LoadSong(TbSong song)
        {
            if (song == null) return;
            TimeSlider.Maximum = double.Parse(song.Duration.ToString());
            MaxTimeTextBlock.Text = FormatTime(double.Parse(song.Duration.ToString()));
            SongNameTextBlock.Text = song.SongName;
            ArtistNameTextBlock.Text = song.Artist.ArtistName;
            if(song.Type == "mp3")
            {
                PlayerMediaElement.Source = new Uri(song.FilePath);
                PlayerMediaElement.Play();
            } else
            {
                MusicVideosPage musicVideosPage = new();
                MainFrame.Navigate(musicVideosPage);
                musicVideosPage.VideoMediaPlayer.Source = new Uri(song.FilePath);
                musicVideosPage.VideoMediaPlayer.Play();
            }
            int plays = song.Plays.HasValue ? song.Plays.Value : 0;
            _songService.UpdatePlaysSong(plays + 1, song.SongId);
            PlayIcon.Kind = PackIconMaterialKind.Pause;
            _timer.Start();
        }

        public void LoadSong(int index)
        {
            var song = GetSongByIndex(index, out double duration);
            if (song == null) return;

            TimeSlider.Maximum = duration;
            MaxTimeTextBlock.Text = FormatTime(duration);
            SongNameTextBlock.Text = song.SongName;
            ArtistNameTextBlock.Text = song.Artist.ArtistName;
            if (song.Type == "mp3")
            {
                PlayerMediaElement.Source = new Uri(song.FilePath);
                PlayerMediaElement.Play();
            }
            else
            {
                MusicVideosPage musicVideosPage = new();
                MainFrame.Navigate(musicVideosPage);
                musicVideosPage.VideoMediaPlayer.Source = new Uri(song.FilePath);
                musicVideosPage.VideoMediaPlayer.Play();
            }
            int plays = song.Plays.HasValue ? song.Plays.Value : 0;
            _songService.UpdatePlaysSong(plays + 1, song.SongId);
            PlayIcon.Kind = PackIconMaterialKind.Pause;
            _timer.Start();
        }

        private void LoadSingleSong(TbSong song)
        {
            if (song == null) return;

            TimeSlider.Maximum = double.Parse(song.Duration.ToString());
            MaxTimeTextBlock.Text = FormatTime(double.Parse(song.Duration.ToString()));
            SongNameTextBlock.Text = song.SongName;
            ArtistNameTextBlock.Text = song.Artist.ArtistName;
            if (song.Type == "mp3")
            {
                PlayerMediaElement.Source = new Uri(song.FilePath);
                PlayerMediaElement.Play();
            }
            else
            {
                MusicVideosPage musicVideosPage = new();
                MainFrame.Navigate(musicVideosPage);
                musicVideosPage.VideoMediaPlayer.Source = new Uri(song.FilePath);
                musicVideosPage.VideoMediaPlayer.Play();
            }
            int plays = song.Plays.HasValue ? song.Plays.Value : 0;
            _songService.UpdatePlaysSong(plays + 1, song.SongId);
            PlayIcon.Kind = PackIconMaterialKind.Pause;
            _timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentTimeTextBlock.Text = FormatTime(PlayerMediaElement.Position.TotalSeconds);
            if (PlayerMediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSlider.Value = PlayerMediaElement.Position.TotalSeconds;
            }
        }

        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PlayerMediaElement.Position = TimeSpan.FromSeconds(TimeSlider.Value);
            if (PlayerMediaElement.NaturalDuration == PlayerMediaElement.Position)
            {
                if (ChooseSong != null && backMode != PlaybackMode.RepeatOne)
                {
                    ChooseSong = null;
                    if (CurrentIndex == 0)
                    {
                        LoadSong(0);
                        return;
                    }
                }
                PlayNextSong();
                return;
            }
        }

        private string FormatTime(double seconds)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return $"{(int)timeSpan.TotalMinutes}:{timeSpan.Seconds:D2}";
        }

        private TbSong GetSongByIndex(int index, out double duration)
        {
            if (index >= 0 && index < ListSongs.Count)
            {
                var song = ListSongs[index];
                duration = double.Parse(song.Duration.ToString());
                return song;
            }

            duration = 0;
            return null;
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (VolumeSlider != null)
            {
                PlayerMediaElement.Volume = VolumeSlider.Value / 100;
                VolumeTextBlock.Text = (int)VolumeSlider.Value + "%";
                //popupSlider = (int)VolumeSlider.Value;
            }
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            backMode = backMode switch
            {
                PlaybackMode.RepeatOff => PlaybackMode.RepeatOn,
                PlaybackMode.RepeatOn => PlaybackMode.RepeatOne,
                _ => PlaybackMode.RepeatOff
            };

            BackModeIcon.Kind = backMode switch
            {
                PlaybackMode.RepeatOn => PackIconMaterialKind.Repeat,
                PlaybackMode.RepeatOne => PackIconMaterialKind.RepeatOnce,
                _ => PackIconMaterialKind.RepeatOff
            };
        }

        private void PlayModeButton_Click(object sender, RoutedEventArgs e)
        {
            playMode = playMode == PlayMode.Sequential ? PlayMode.Shuffle : PlayMode.Sequential;
            PlayModeIcon.Kind = playMode == PlayMode.Shuffle ? PackIconMaterialKind.Shuffle : PackIconMaterialKind.ShuffleDisabled;
        }

        private void PlayNextButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseSong = null;
            if (backMode == PlaybackMode.RepeatOne)
            {
                backMode = PlaybackMode.RepeatOn;
                PlayNextSong();
                backMode = PlaybackMode.RepeatOne;
                return;
            }
            PlayNextSong();
        }

        private void PlayBackButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseSong = null;
            if (CurrentIndex >= 1)
                --CurrentIndex;
            //MessageBox.Show("Current index : " + CurrentIndex);
            LoadSong(CurrentIndex);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            UserInfoPopup.IsOpen = !UserInfoPopup.IsOpen;
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        { // Implement logout logic here
            AuthenticatedUser = null;
            MessageBox.Show("You have been logged out.", "Logout", MessageBoxButton.OK, MessageBoxImage.Information); UserInfoPopup.IsOpen = false; }
        }
}
