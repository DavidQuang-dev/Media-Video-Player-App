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

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SongItem));

        public string Number
        {
            get { return (string)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(string), typeof(SongItem));

        //public string Time
        //{
        //    get { return (string)GetValue(TimeProperty); }
        //    set { SetValue(TimeProperty, value); }
        //}

        //public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(SongItem));

        public string Time
        {
            get { return convertTimeFormat((decimal)GetValue(TimeProperty)); }
            set { SetValue(TimeProperty, convertTimeFormat(decimal.Parse(value))); }
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time", typeof(string), typeof(SongItem));

        // Hàm để chuyển đổi giây sang định dạng phút:giây
        private string convertTimeFormat(decimal value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds((double)value);
            string timeFormatted = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            MessageBox.Show("Time : " + timeFormatted);
            return timeFormatted;
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(SongItem));
    }
}
