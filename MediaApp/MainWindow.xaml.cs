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
        private int currentIndex = 0;
        private readonly DispatcherTimer _timer;
        private Mp3FileReader _reader;

        public MainWindow()
        {
            InitializeComponent();
            ListSongs = new List<TbSong>();
            MainFrame.Navigate(new HomePage());

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
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
            LoadSong(currentIndex);
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
                        currentIndex = (currentIndex + 1) % ListSongs.Count;
                        if (currentIndex == 0) // Reached the end
                        {
                            ListSongs.Clear();
                            PausePlayback();
                            return;
                        }
                    }
                    else
                    {
                        currentIndex = randomIndex();
                    }
                    break;
                case PlaybackMode.RepeatOn:
                    currentIndex = playMode == PlayMode.Sequential ? (currentIndex + 1) % ListSongs.Count : randomIndex();
                    break;
                case PlaybackMode.RepeatOne:
                    break;
            }
            LoadSong(currentIndex);
        }

        private int randomIndex()
        {
            Random random = new Random();
            int index;
            do
            {
                index = random.Next(ListSongs.Count);
            } while (index == currentIndex);
            return index;
        }

        private void LoadSong(TbSong song)
        {
            if (song == null) return;
            MessageBox.Show("Current Index Of Load Song: " + currentIndex);
            TimeSlider.Maximum = double.Parse(song.Duration.ToString());
            MaxTimeTextBlock.Text = FormatTime(double.Parse(song.Duration.ToString()));
            PlayerMediaElement.Source = new Uri(song.FilePath);
            SongNameTextBlock.Text = song.SongName;
            ArtistNameTextBlock.Text = song.Artist.ArtistName;
            PlayerMediaElement.Play();
            PlayIcon.Kind = PackIconMaterialKind.Pause;
            _timer.Start();
        }

        private void LoadSong(int index)
        {
            var song = GetSongByIndex(index, out double duration);
            if (song == null) return;

            TimeSlider.Maximum = duration;
            MaxTimeTextBlock.Text = FormatTime(duration);
            PlayerMediaElement.Source = new Uri(song.FilePath);
            SongNameTextBlock.Text = song.SongName;
            ArtistNameTextBlock.Text = song.Artist.ArtistName;
            // MessageBox.Show("Current Index Of Load Song: " + currentIndex);
            PlayerMediaElement.Play();
            PlayIcon.Kind = PackIconMaterialKind.Pause;
            _timer.Start();
        }

        private void LoadSingleSong(TbSong song)
        {
            if (song == null) return;

            TimeSlider.Maximum = double.Parse(song.Duration.ToString());
            MaxTimeTextBlock.Text = FormatTime(double.Parse(song.Duration.ToString()));
            PlayerMediaElement.Source = new Uri(song.FilePath);
            SongNameTextBlock.Text = song.SongName;
            ArtistNameTextBlock.Text = song.Artist.ArtistName;
            PlayerMediaElement.Play();
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
            if (currentIndex >= 1)
                --currentIndex;
            //MessageBox.Show("Current index : " + currentIndex);
            LoadSong(currentIndex);
        }
    }
}
