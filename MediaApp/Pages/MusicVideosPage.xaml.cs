using MahApps.Metro.IconPacks;
using MediaApp;
using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
    /// Interaction logic for MusicVideosPage.xaml
    /// </summary>
    public partial class MusicVideosPage : Window
    {
        private SongService songService = new();
        private DispatcherTimer _timer;
        private bool isDragging = false;
        private bool isZoom = false;
        private DispatcherTimer _timeMouseEnter;
        private List<TbSong> videoList;
        private DispatcherTimer _skipTimer;
        private Button selectedButton = null;
        public MusicVideosPage()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            _skipTimer = new DispatcherTimer();
            _skipTimer.Interval = TimeSpan.FromSeconds(1);
            _skipTimer.Tick += SkipTimer_Tick;
        }

        void SkipTimer_Tick(object sender, EventArgs e)
        {
            PreviousStackPanel.Visibility = Visibility.Hidden;
            VolumeStackPanel.Visibility = Visibility.Hidden;
            ForwardStackPanel.Visibility = Visibility.Hidden;
            _skipTimer.Stop();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
            videoList = songService.GetMusicVideos();
            int number = 0;
            foreach (var song in videoList)
            {
                var songItem = new video_media_player.UserControls.SongItem
                {
                    Title = song.SongName,
                    Number = (++number).ToString(),
                    Time = ConvertTimeFormat(TagLib.File.Create(song.FilePath).Properties.Duration.TotalSeconds),
                    Tag = song.FilePath
                };
                songItem.Click += SongItem_Click;
                ListVideos.Children.Add(songItem);
            }

            selectedButton = SpeedX1Button;
            ChangeSelectedColorButton(selectedButton);
        }

        private void SongItem_Click(object sender, RoutedEventArgs e)
        {
            video_media_player.UserControls.SongItem songItem = (video_media_player.UserControls.SongItem)sender;
            string songName = songItem.Title.ToString();
            if (!string.IsNullOrEmpty(songName))
            {
                //MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                //mainWindow.SetChosenSong(songService.GetSongByName(songName));
                TbSong selectedSong = songService.GetSongByName(songName);

                if (selectedSong != null)
                {
                    VideoMediaPlayer.Source = new Uri(selectedSong.FilePath);
                    TimeSlider.Maximum = (double)selectedSong.Duration;
                    MaxTimeLabel.Content = "/ " + ConvertTimeFormat((double)selectedSong.Duration);
                    VideoNameLabel.Content = selectedSong.SongName;
                    VideoMediaPlayer.Play();
                    _timer.Start();
                }
            }
        }

        private string ConvertTimeFormat(double value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            string timeFormated = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            return timeFormated;
        }

        private void Pause()
        {
            PlayIcon.Kind = PackIconMaterialKind.Play;
            VideoMediaPlayer.Pause();
            _timer.Stop();
        }

        private void StartPlayback()
        {
            VideoMediaPlayer.Play();
            PlayIcon.Kind = PackIconMaterialKind.Pause;
            _timer.Start();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayIcon.Kind == PackIconMaterialKind.Pause)
            {
                Pause();
            }
            else
            {
                StartPlayback();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Content = FormatTime(VideoMediaPlayer.Position.TotalSeconds);
            if (VideoMediaPlayer.NaturalDuration.HasTimeSpan)
            {
                TimeSlider.Value = VideoMediaPlayer.Position.TotalSeconds;
            }
        }

        private string FormatTime(double seconds)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return $"{(int)timeSpan.TotalMinutes}:{timeSpan.Seconds:D2}";
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VideoMediaPlayer.Volume = VolumeSlider.Value / 100;
            if (VolumeSlider.Value > 70) VolumeIcon.Kind = PackIconMaterialKind.VolumeHigh;
            else if (VolumeSlider.Value >= 30) VolumeIcon.Kind = PackIconMaterialKind.VolumeMedium;
            else if (VolumeSlider.Value > 0) VolumeIcon.Kind = PackIconMaterialKind.VolumeLow;
            else VolumeIcon.Kind = PackIconMaterialKind.VolumeMute;
        }

        private void volumeButton_Click(object sender, RoutedEventArgs e)
        {
            //volumePopup.IsOpen = !volumePopup.IsOpen;
            if (volumePopup.IsOpen == true)
            {
                volumePopup.IsOpen = false;
                MuteEvent();
            }
            else
            {
                volumePopup.IsOpen = true;
            }
        }

        private void TimeSlider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
        }

        private void TimeSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            VideoMediaPlayer.Position = TimeSpan.FromSeconds(TimeSlider.Value);
        }

        private void ZoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isZoom)
            {
                WindowState = WindowState.Maximized;
                FullLayOutGrid.RowDefinitions.Clear();
                FullLayOutGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                FullLayOutGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0) });
                FullLayOutGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0) });

                PlayerScreenGrid.RowDefinitions.Clear();
                PlayerScreenGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0) });
                PlayerScreenGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                PlayerScreenGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0) });
                FullScreenScrollView.Margin = new Thickness(0, 0, 0, 0);
                FullScreenScrollView.ScrollToEnd();
                isZoom = true;
            }
            else
            {
                WindowState = WindowState.Normal;
                FullLayOutGrid.RowDefinitions.Clear();
                FullLayOutGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2.5, GridUnitType.Star) });
                FullLayOutGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                FullLayOutGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                PlayerScreenGrid.RowDefinitions.Clear();
                PlayerScreenGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                PlayerScreenGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                PlayerScreenGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                FullScreenScrollView.Margin = new Thickness(30, 20, 30, 30);
                FullScreenScrollView.ScrollToHome();
                isZoom = false;
            }
        }

        private void ToggleWindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                MainBorder.CornerRadius = new CornerRadius(40);
            }
            else
            {
                WindowState = WindowState.Maximized;
                MainBorder.CornerRadius = new CornerRadius(0);
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            VideoMediaPlayer.Stop();
            this.Close();
        }

        private void ScreenBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TaskBarGrid.Visibility = Visibility.Hidden;
        }

        private void ScreenBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TaskBarGrid.Visibility = Visibility.Visible;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    {
                        PlayButton_Click(sender, e);
                        break;
                    }
                case Key.M:
                    {
                        MuteEvent();
                        break;
                    }
                case Key.F:
                    {
                        ZoomButton_Click(sender, e);
                        break;
                    }
                case Key.Escape:
                    {
                        if (WindowState == WindowState.Maximized)
                            ZoomButton_Click(sender, e);
                        break;
                    }
                case Key.Right:
                    {
                        UpdateTimeKeyDown(e.Key);
                        break;
                    }
                case Key.Left:
                    {
                        UpdateTimeKeyDown(e.Key);
                        break;
                    }
                case Key.Up:
                    {
                        VolumeSlider.Value = VolumeSlider.Value + 5;
                        VolumePressUpLabel.Content = VolumeSlider.Value + "%";
                        VolumeStackPanel.Visibility = Visibility.Visible;
                        _skipTimer.Start();
                        break;
                    }
                case Key.Down:
                    {
                        VolumeSlider.Value = VolumeSlider.Value - 5;
                        VolumePressUpLabel.Content = VolumeSlider.Value + "%";
                        VolumeStackPanel.Visibility = Visibility.Visible;
                        _skipTimer.Start();
                        break;
                    }
            }
        }

        private void UpdateTimeKeyDown(Key key)
        {
            if (key == Key.Left)
            {
                TimeSlider.Value = TimeSlider.Value - 10;
                PreviousStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                TimeSlider.Value = TimeSlider.Value + 10;
                ForwardStackPanel.Visibility = Visibility.Visible;
            }
            _skipTimer.Start();
            VideoMediaPlayer.Position = TimeSpan.FromSeconds(TimeSlider.Value);
            TimeLabel.Content = ConvertTimeFormat(TimeSlider.Value);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MuteEvent()
        {
            if (VolumeIcon.Kind != PackIconMaterialKind.VolumeMute)
            {
                VideoMediaPlayer.Volume = 0;
                VolumeIcon.Kind = PackIconMaterialKind.VolumeMute;
            }
            else
            {
                VideoMediaPlayer.Volume = 50;
                VolumeIcon.Kind = PackIconMaterialKind.VolumeMedium;
            }
        }

        private void ScreenBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (VideoMediaPlayer.Source == null) return;

            // Start timer when mouse enters
            TaskBarGrid.Visibility = Visibility.Visible;
            if (_timeMouseEnter == null)
            {
                _timeMouseEnter = new DispatcherTimer();
                _timeMouseEnter.Interval = TimeSpan.FromSeconds(3);
                _timeMouseEnter.Tick += TimeMouseEnter_Tick;
            }
            _timeMouseEnter.Start();
        }

        private void TimeMouseEnter_Tick(object sender, EventArgs e)
        {
            // Perform action after 3 seconds
            TaskBarGrid.Visibility = Visibility.Hidden;
            _timeMouseEnter.Stop();
        }

        private void ScreenBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            // Hide TaskBarGrid when mouse leaves
            TaskBarGrid.Visibility = Visibility.Hidden;
            if (_timeMouseEnter != null)
            {
                _timeMouseEnter.Stop();
            }
        }

        private void ScreenBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (VideoMediaPlayer.Source == null) return;

            // Show TaskBarGrid and start/restart timer on mouse move
            if (TaskBarGrid.Visibility != Visibility.Visible)
                TaskBarGrid.Visibility = Visibility.Visible;
            if (_timeMouseEnter == null)
            {
                _timeMouseEnter = new DispatcherTimer();
                _timeMouseEnter.Interval = TimeSpan.FromSeconds(3);
                _timeMouseEnter.Tick += TimeMouseEnter_Tick;
            }
            _timeMouseEnter.Stop();
            _timeMouseEnter.Start();
        }

        private void VideoMediaPlayer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PlayButton_Click(sender, e);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                e.Handled = true;
            } else if (e.Key == Key.Down)
            {
                e.Handled = true;
            } else if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void SpeedButton_Click(object sender, RoutedEventArgs e)
        {
            SpeedPopup.IsOpen = !SpeedPopup.IsOpen;
        }

        private bool isTimerRunning = false;  // Biến để theo dõi trạng thái của timer

        private void SpeedX2Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeSpeed(2, sender);
        }

        private void SpeedX175Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeSpeed(1.75, sender); 
        }

        private void SpeedX15Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeSpeed(1.5, sender);
        }

        private void SpeedX1Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeSpeed(1,sender);
        }

        private void SpeedX05Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeSpeed(0.5, sender);
        }

        // Hàm thay đổi tốc độ và Interval của timer
        private void ChangeSpeed(double speed, Object sender)
        {
            if (isTimerRunning) 
            {
                _timer.Stop(); 
                isTimerRunning = false;  
            }
            VideoMediaPlayer.SpeedRatio = speed;
            _timer.Interval = TimeSpan.FromSeconds(1 / speed);
            _timer.Start(); 
            isTimerRunning = true;  
            ChangeSelectedColorButton(sender);
        }

        private void ChangeSelectedColorButton(object sender)
        {
            
            Button clickedButton = sender as Button;

            if (selectedButton != null)
            {
                selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3b3c36"));
            }

            clickedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#080808"));
            selectedButton = clickedButton;

            // Set lai isOpen
            SpeedPopup.IsOpen = false;
        }
    }
}
