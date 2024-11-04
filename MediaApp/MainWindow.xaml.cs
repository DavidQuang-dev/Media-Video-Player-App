//using MahApps.Metro.IconPacks;
//using MediaApp.BLL.Services;
//using MediaApp.DAL.Entities;
//using NAudio.Gui;
//using NAudio.Wave;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Threading;

//namespace video_media_player
//{
//    public enum PlaybackMode
//    {
//        RepeatOff,
//        RepeatOn,
//        RepeatOne
//        //Loop,
//        //LoopInList
//    }

//    public enum PlayMode
//    {
//        Sequential,
//        Shuffle
//    }
//    public partial class MainWindow : Window
//    {
//        public List<TbSong> ListSongs { get; set; }
//        private SongService _songService = new();
//        private PlaybackMode backMode = PlaybackMode.RepeatOff;
//        private PlayMode playMode = PlayMode.Sequential;
//        //private bool isRandom = false;
//        private int currentIndex = 1;
//        private DispatcherTimer _timer;
//        private Mp3FileReader _reader;
//        //private bool isPlayed = false;


//        public MainWindow()
//        {
//            InitializeComponent();
//            ListSongs = new();
//            MainFrame.Navigate(new HomePage());
//            _timer = new DispatcherTimer
//            {
//                Interval = TimeSpan.FromMilliseconds(1000)
//            };
//            _timer.Tick += Timer_Tick;
//        }

//        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
//        {
//            if (e.ChangedButton == MouseButton.Left)
//                this.DragMove();
//        }
//        private void CloseButton_Click(object sender, RoutedEventArgs e)
//        {
//            Application.Current.Shutdown();
//        }

//        private void ToggleWindowStateButton_Click(object sender, RoutedEventArgs e)
//        {
//            if (this.WindowState == WindowState.Maximized)
//            {
//                this.WindowState = WindowState.Normal;
//                MainBorder.CornerRadius = new CornerRadius(40);
//                PlayerBorder.CornerRadius = new CornerRadius(40, 0, 40, 0);
//            }
//            else
//            {
//                this.WindowState = WindowState.Maximized;
//                MainBorder.CornerRadius = new CornerRadius(0);
//                PlayerBorder.CornerRadius = new CornerRadius(40, 0, 0, 0);
//            }
//        }
//        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
//        {
//            this.WindowState = WindowState.Minimized;
//        }

//        private void HomeButton_Click(object sender, RoutedEventArgs e)
//        {
//            MainFrame.Navigate(new HomePage());
//        }
//        private void PlaylistsButton_Click(object sender, RoutedEventArgs e)
//        {
//            MainFrame.Navigate(new PlaylistsPage());

//        }
//        private void AlbumsButton_Click(object sender, RoutedEventArgs e)
//        {
//            MainFrame.Navigate(new AlbumsPage());
//        }
//        private void SongsButton_Click(object sender, RoutedEventArgs e)
//        {
//            MainFrame.Navigate(new SongsPage());
//        }
//        private void StoreButton_Click(object sender, RoutedEventArgs e)
//        {
//            MainFrame.Navigate(new StorePage());
//        }

//        private void VolumeButton_Click(object sender, RoutedEventArgs e)
//        {
//            volumePopup.IsOpen = !volumePopup.IsOpen;
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {

//        }

//        private void PlayButton_Click(object sender, RoutedEventArgs e)
//        {
//            if (PlayIcon.Kind == PackIconMaterialKind.Pause)
//            {
//                PlayIcon.Kind = PackIconMaterialKind.Play;
//                PlayerMediaElement.Pause();
//                _timer.Stop();
//                return;
//            }

//            if (ListSongs.Count == 0)
//            {
//                PlayFirstSong();
//            }
//            else
//            {
//                PlayerMediaElement.Play();
//            }

//            PlayIcon.Kind = PackIconMaterialKind.Pause;
//            _timer.Start();
//        }

//        public void PlayFirstSong()
//        {
//            ListSongs = _songService.GetAll();
//            string source = GetSongByIndex(currentIndex, out double duration).FilePath;
//            TimeSlider.Maximum = duration;
//            MaxTimeTextBlock.Text = convertTimeFormat(duration);
//            PlayerMediaElement.Source = new Uri(source);
//            PlayerMediaElement.MediaOpened += MediaElement_MediaOpened;
//            _reader = new Mp3FileReader(source);
//        }
//        public void PlayNextSong()
//        {
//            if (ListSongs.Count == 0)
//                return;

//            Random random = new Random();
//            switch (backMode)
//            {
//                case PlaybackMode.RepeatOff:
//                    {
//                        if (playMode == PlayMode.Sequential)
//                        {
//                            currentIndex = (currentIndex + 1) % ListSongs.Count;
//                            if (currentIndex == 0) // Nếu đã phát hết danh sách
//                            {
//                                _timer.Stop();
//                                PlayIcon.Kind = PackIconMaterialKind.Play;
//                                return;
//                            }
//                        }
//                        else if (playMode == PlayMode.Shuffle)
//                        {
//                            currentIndex = random.Next(ListSongs.Count);
//                        }
//                        break;
//                    }
//                case PlaybackMode.RepeatOn:
//                    {
//                        if (playMode == PlayMode.Sequential)
//                        {
//                            currentIndex = (currentIndex + 1) % ListSongs.Count;
//                        }
//                        else if (playMode == PlayMode.Shuffle)
//                        {
//                            currentIndex = random.Next(ListSongs.Count);
//                        }
//                        break;
//                    }
//                case PlaybackMode.RepeatOne:
//                    // currentIndex giữ nguyên
//                    break;
//            }

//            string source = GetSongByIndex(currentIndex, out double duration)?.FilePath;
//            if (string.IsNullOrEmpty(source))
//            {
//                _timer.Stop();
//                return;
//            }

//            TimeSlider.Maximum = duration;
//            MaxTimeTextBlock.Text = convertTimeFormat(duration);
//            PlayerMediaElement.Source = new Uri(source);
//            PlayerMediaElement.Play();
//            PlayIcon.Kind = PackIconMaterialKind.Pause;
//            _timer.Start();
//        }

//        //public void PlayNextSong()
//        //{
//        //    Random random = new Random();
//        //    switch (backMode)
//        //    {
//        //        case PlaybackMode.RepeatOff:
//        //            {
//        //                if (playMode == PlayMode.Sequential)
//        //                {
//        //                    if (currentIndex + 1 == ListSongs.Count)
//        //                        currentIndex = ListSongs.Count;
//        //                    else if (currentIndex + 1 < ListSongs.Count)
//        //                        currentIndex = (currentIndex + 1) % ListSongs.Count;
//        //                }
//        //                else
//        //                    currentIndex = random.Next(ListSongs.Count);
//        //                break;
//        //            }
//        //        case PlaybackMode.RepeatOn:
//        //            {
//        //                if (playMode == PlayMode.Sequential)
//        //                {
//        //                    if (currentIndex + 1 == ListSongs.Count)
//        //                        currentIndex = ListSongs.Count;
//        //                    else if (currentIndex + 1 < ListSongs.Count)
//        //                        currentIndex = (currentIndex + 1) % ListSongs.Count;
//        //                }
//        //                else
//        //                    currentIndex = random.Next(ListSongs.Count);
//        //                break;
//        //            }
//        //        case PlaybackMode.RepeatOne:
//        //            // currentIndex stays the same
//        //            break;
//        //    }
//        //    MessageBox.Show("current index : " + currentIndex);
//        //    string source = GetSongByIndex(currentIndex, out double duration).FilePath;
//        //    TimeSlider.Maximum = duration;
//        //    MaxTimeTextBlock.Text = convertTimeFormat(duration);
//        //    PlayerMediaElement.Source = new Uri(source);
//        //    PlayerMediaElement.MediaOpened += MediaElement_MediaOpened;
//        //    _reader = new Mp3FileReader(source);
//        //}

//        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
//        {
//            if (PlayerMediaElement.NaturalDuration.HasTimeSpan)
//            {
//                TimeSlider.Maximum = PlayerMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
//                //MessageBox.Show($"Duration: {MediaTest.NaturalDuration.TimeSpan}");
//            }
//        }


//        private TbSong? GetSongByIndex(int index, out double duration)
//        {
//            int count = 1;
//            foreach (TbSong song in ListSongs)
//            {
//                if (count == index)
//                {
//                    duration = (double)song.Duration;
//                    return song;
//                }
//                count++;
//            }
//            duration = 0;
//            return null;
//        }

//        private void Timer_Tick(object? sender, EventArgs e)
//        {
//            if (_reader != null)
//            {
//                if (_reader.CurrentTime < _reader.TotalTime)
//                {
//                    TimeSlider.Value = PlayerMediaElement.Position.TotalSeconds;
//                    string currentTimeFM = convertTimeFormat(PlayerMediaElement.Position.TotalSeconds);
//                    CurrentTimeTextBlock.Text = currentTimeFM;
//                }
//                else
//                {
//                    _timer.Stop();
//                }
//            }
//        }

//        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
//        {
//            if (TimeSlider.Value == TimeSlider.Maximum)
//            {
//                PlayNextSong();
//                return;
//            }
//            TimeSpan timeSpan = TimeSpan.FromSeconds(TimeSlider.Value);
//            PlayerMediaElement.Position = timeSpan;
//        }

//        private string convertTimeFormat(double value)
//        {
//            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
//            string timeFormated = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
//            return timeFormated;
//        }

//        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
//        {
//            if (VolumeSlider != null)
//            {
//                PlayerMediaElement.Volume = VolumeSlider.Value / 100;
//            }
//        }

//        private void RepeatButton_Click(object sender, RoutedEventArgs e)
//        {
//            if (backMode == PlaybackMode.RepeatOff)
//            {
//                BackModeIcon.Kind = PackIconMaterialKind.Repeat;
//                backMode = PlaybackMode.RepeatOn;
//            }
//            else if (backMode == PlaybackMode.RepeatOn)
//            {
//                BackModeIcon.Kind = PackIconMaterialKind.RepeatOnce;
//                backMode = PlaybackMode.RepeatOne;
//            }
//            else
//            {
//                BackModeIcon.Kind = PackIconMaterialKind.RepeatOff;
//                backMode = PlaybackMode.RepeatOff;
//            }
//        }

//        private void PlayModeButton_Click(object sender, RoutedEventArgs e)
//        {
//            if (playMode == PlayMode.Sequential)
//            {
//                PlayModeIcon.Kind = PackIconMaterialKind.Shuffle;
//                playMode = PlayMode.Shuffle;
//            }
//            else
//            {
//                PlayModeIcon.Kind = PackIconMaterialKind.ShuffleDisabled;
//                playMode = PlayMode.Sequential;
//            }

//        }

//        private void PlayNextButton_Click(object sender, RoutedEventArgs e)
//        {
//            PlayNextSong();
//            PlayerMediaElement.Play();
//            PlayIcon.Kind = PackIconMaterialKind.Pause;
//            _timer.Stop();
//            _timer.Start();
//        }
//    }
//}

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
            }
            else
            {
                WindowState = WindowState.Maximized;
                MainBorder.CornerRadius = new CornerRadius(0);
                PlayerBorder.CornerRadius = new CornerRadius(40, 0, 0, 0);
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
                            PausePlayback();
                            return;
                        }
                    }
                    else
                    {
                        currentIndex = random.Next(ListSongs.Count);
                    }
                    break;
                case PlaybackMode.RepeatOn:
                    currentIndex = playMode == PlayMode.Sequential ? (currentIndex + 1) % ListSongs.Count : random.Next(ListSongs.Count);
                    break;
                case PlaybackMode.RepeatOne:
                    // Stay on the same song
                    break;
            }

            LoadSong(currentIndex);
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
            PlayerMediaElement.Play();
            PlayIcon.Kind = PackIconMaterialKind.Pause;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (PlayerMediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSlider.Value = PlayerMediaElement.Position.TotalSeconds;
                CurrentTimeTextBlock.Text = FormatTime(PlayerMediaElement.Position.TotalSeconds);
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
            PlayNextSong();
        }
    }
}
