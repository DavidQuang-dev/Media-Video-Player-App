using System.Windows;
using System.Windows.Input;
using video_media_player;

namespace MediaApp
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
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

        private void MP3_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.ShowDialog();
        }

        private void MP4_Click(object sender, RoutedEventArgs e)
        {
            MusicVideosPage musicVideosPage = new();
            musicVideosPage.ShowDialog();
        }

        private void Storage_Click(object sender, RoutedEventArgs e)
        {
            StorePage storePage = new();
            storePage.ShowDialog();
        }
    }
}
