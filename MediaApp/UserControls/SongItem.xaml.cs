using System.Windows;
using System.Windows.Controls;

namespace video_media_player.UserControls
{
    public partial class SongItem : UserControl
    {
        public SongItem()
        {
            InitializeComponent();
        }
        // Định nghĩa sự kiện Click
        public event RoutedEventHandler Click;

        // Phương thức để gọi sự kiện Click
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Click?.Invoke(this, new RoutedEventArgs());
        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SongItem), new PropertyMetadata(string.Empty));

        public string Number
        {
            get { return (string)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(string), typeof(SongItem), new PropertyMetadata(string.Empty));

        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(SongItem));

        public new string Tag
        {
            get => (string)GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }

        public static new readonly DependencyProperty TagProperty = DependencyProperty.Register("Tag", typeof(string), typeof(SongItem));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(SongItem));

        private void TextBlock_ToolTipClosing(object sender, ToolTipEventArgs e)
        {

        }
    }
}
