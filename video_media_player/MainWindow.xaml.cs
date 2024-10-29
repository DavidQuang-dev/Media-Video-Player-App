using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;


namespace video_media_player
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
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
        private void MusicVideosButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MusicVideosPage());
        }
        private void StoreButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StorePage());
        }
    }
}
