using System.Windows;
using System.Windows.Input;


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

        private void PlaylistsButton_Click(object sender, RoutedEventArgs e)
        {
            PlaylistsWindow playlistsWindow = new PlaylistsWindow();
            playlistsWindow.Show();

        }

        private void ArtistsButton_Click(object sender, RoutedEventArgs e)
        {
            ArtistsWindow artistsWindow = new ArtistsWindow();
            artistsWindow.Show();
        }

        private void AlbumsButton_Click(object sender, RoutedEventArgs e)
        {
            AlbumsWindow albumsWindow = new AlbumsWindow();
            albumsWindow.Show();
        }
        private void SongsButton_Click(object sender, RoutedEventArgs e)
        {
            SongsWindow songsWindow = new SongsWindow();
            songsWindow.Show();
        }
        private void MusicVideosButton_Click(object sender, RoutedEventArgs e)
        {
            MusicVideosWindow musicVideosWindow = new MusicVideosWindow();
            musicVideosWindow.Show();
        }

        private void StoreButton_Click(object sender, RoutedEventArgs e)
        {
            StoreWindow storeWindow = new StoreWindow();
            storeWindow.Show();
        }

        private void ForYouButton_Click(object sender, RoutedEventArgs e)
        {
            ForYouWindow forYouWindow = new ForYouWindow();
            forYouWindow.Show();
        }
    }
}
