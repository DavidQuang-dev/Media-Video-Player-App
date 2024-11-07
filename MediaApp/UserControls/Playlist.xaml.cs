using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace video_media_player.UserControls
{
    public partial class Playlist : UserControl
    {
        public Playlist()
        {
            InitializeComponent();
        }
        public event RoutedEventHandler Click;

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Click?.Invoke(this, new RoutedEventArgs());
        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Playlist));

        public string Desc
        {
            get { return (string)GetValue(DescProperty); }
            set { SetValue(DescProperty, value); }
        }

        public static readonly DependencyProperty DescProperty = DependencyProperty.Register("Desc", typeof(string), typeof(Playlist));

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(Playlist));
    }
}
