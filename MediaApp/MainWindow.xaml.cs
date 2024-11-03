using MediaApp.BLL.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace video_media_player
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HomePage());
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

        private void ToggleWindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            { 
                this.WindowState = WindowState.Normal;
                MainBorder.CornerRadius = new CornerRadius(40);
                PlayerBorder.CornerRadius = new CornerRadius(40, 0, 40, 0);
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                MainBorder.CornerRadius = new CornerRadius(0);
                PlayerBorder.CornerRadius = new CornerRadius(40, 0, 0, 0);
            }
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Kiểm tra nếu MediaElement tồn tại
            if (mediaElement != null)
            {
                // Đặt âm lượng của MediaElement bằng với giá trị của Slider
                mediaElement.Volume = e.NewValue / 100; // Chuyển đổi từ 0-100 về 0-1 cho MediaElement
            }
        }
    }
}
