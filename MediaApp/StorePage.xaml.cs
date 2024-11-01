//using Microsoft.Win32;
//using NAudio.Wave;
//using System.Reflection.PortableExecutable;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Threading;

//namespace video_media_player
//{
//    /// <summary>
//    /// Interaction logic for StorePage.xaml
//    /// </summary>
//    public partial class StorePage : Page
//    {
//        private DispatcherTimer _timer;
//        private Mp3FileReader _reader;
//        public StorePage()
//        {
//            InitializeComponent();
//            _timer = new DispatcherTimer();
//            _timer.Interval = TimeSpan.FromMilliseconds(1000);
//            _timer.Tick += Timer_Tick;
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog();
//            openFileDialog.Multiselect = true;
//        }

//        private void PauseButtonClick_Click(object sender, RoutedEventArgs e)
//        {
//            MediaTest.Pause();
//        }

//        private void PlayButtonClick_Click(object sender, RoutedEventArgs e)
//        {
//            MediaTest.Play();
//            MessageBox.Show("Played");
//            _timer.Start();
//        }

//        private void StopButtonClick_Click(object sender, RoutedEventArgs e)
//        {
//            MediaTest.Stop();
//        }

//        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
//        {
//            MediaTest.Volume = VolumnSlider.Value / 100;
//        }

//        private void Button_Click_1(object sender, RoutedEventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog
//            {
//                Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*",
//                Title = "Select a MP3 File"
//            };
//            openFileDialog.Multiselect = true;
//            if (openFileDialog.ShowDialog() == true)
//            {
//                FilePathTextBox.Text = openFileDialog.FileName;
//                MediaTest.Source = new Uri(openFileDialog.FileName);
//                MediaTest.MediaOpened += MediaElement_MediaOpened;

//                _reader = new Mp3FileReader(openFileDialog.FileName);
//                TimeSlider.Maximum = _reader.TotalTime.TotalSeconds; // Thiết lập giá trị tối đa cho 
//                MessageBox.Show("Time Slider : " + TimeSlider.Maximum);
//                // Bắt đầu timer để cập nhật Slider
//                //mediaElement.Source = new Uri(openFileDialog.FileName);
//                //mediaElement.MediaOpened += MediaElement_MediaOpened;
//                //mediaElement.Play();
//            }
//        }

//        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
//        {
//            // Lấy thời lượng file MP4
//            if (MediaTest.NaturalDuration.HasTimeSpan)
//            {
//                TimeSpan duration = MediaTest.NaturalDuration.TimeSpan;
//                MessageBox.Show($"Thời lượng của file MP4 là: {duration}");
//            }
//            // mediaElement.Stop(); // Dừng phát sau khi lấy thời lượng
//        }


//        private void Timer_Tick(object? sender, EventArgs e)
//        {
//            if (_reader != null && _reader.CurrentTime < _reader.TotalTime)
//            {
//                TimeSlider.Value = _reader.CurrentTime.TotalSeconds; // Cập nhật giá trị Slider
//                                                                     //MessageBox.Show("current total second Time_Tick: " + _reader.TotalTime.TotalSeconds);
//                OutputTextBlock.Text = "Curent time slider : " + TimeSlider.Value.ToString() + "| Curent reader : " + _reader.CurrentTime.TotalSeconds + "| Maximum : " + TimeSlider.Maximum + "| Date time : " + DateTime.Now.ToString();
//            }
//            else
//            {
//                _timer.Stop(); // Dừng timer nếu file đã phát xong
//            }
//        }
//    }
//}

using MediaApp;
using Microsoft.Win32;
using NAudio.Wave;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace video_media_player
{
    /// <summary>
    /// Interaction logic for StorePage.xaml
    /// </summary>
    public partial class StorePage : Page
    {

        public StorePage()
        {
            InitializeComponent();
        }

        private void ManageAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            AlbumManage albumManage = new AlbumManage();
            albumManage.ShowDialog();
        }
    }
}

