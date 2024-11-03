using Microsoft.Win32;
using NAudio.Gui;
using NAudio.Wave;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MediaApp
{
    /// <summary>
    /// Interaction logic for TestMediaElement.xaml
    /// </summary>
    public partial class TestMediaElement : Window
    {
        private DispatcherTimer _timer;
        private Mp3FileReader _reader;

        public TestMediaElement()
        {
            InitializeComponent();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            _timer.Tick += Timer_Tick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };
        }

        private void PauseButtonClick_Click(object sender, RoutedEventArgs e)
        {
            MediaTest.Pause();
        }

        private void PlayButtonClick_Click(object sender, RoutedEventArgs e)
        {
            if (MediaTest.NaturalDuration.HasTimeSpan)
                if (MediaTest.Position == MediaTest.NaturalDuration.TimeSpan)
                    MediaTest.Stop();
            MediaTest.Play();
            _timer.Start();
            MessageBox.Show("Playing");
        }

        private void StopButtonClick_Click(object sender, RoutedEventArgs e)
        {
            MediaTest.Stop();
            _timer.Stop();
            TimeSlider.Value = 0;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (VolumnSlider != null)
            {
                MediaTest.Volume = VolumnSlider.Value / 100;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*",
                Title = "Select an MP3 File",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    FilePathTextBox.Text = openFileDialog.FileName;
                    MediaTest.Source = new Uri(openFileDialog.FileName);
                    MediaTest.MediaOpened += MediaElement_MediaOpened;

                    _reader = new Mp3FileReader(openFileDialog.FileName);

                    TimeSlider.Maximum = _reader.TotalTime.TotalSeconds;
                    MessageBox.Show("Time Slider Maximum: " + TimeSlider.Maximum);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (MediaTest.NaturalDuration.HasTimeSpan)
            {
                TimeSlider.Maximum = MediaTest.NaturalDuration.TimeSpan.TotalSeconds;
                MessageBox.Show($"Duration: {MediaTest.NaturalDuration.TimeSpan}");
            }
        }

        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(TimeSlider.Value);
            MediaTest.Position = timeSpan;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_reader != null)
            {
                if (_reader.CurrentTime < _reader.TotalTime)
                {
                    TimeSlider.Value = MediaTest.Position.TotalSeconds;
                    OutputTextBlock.Text = $"Current Value: {TimeSlider.Value} |Media Test Value: {MediaTest.Position.TotalSeconds}|  Max Time: {TimeSlider.Maximum} | Date: {DateTime.Now}";
                    // double totalSeconds = MediaTest.Position.TotalSeconds;
                    //TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
                    //TimeSpan timeSpanMaximum = TimeSpan.FromSeconds(TimeSlider.Maximum);
                    //string timeformatted = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
                    //string maxTimeFormated = string.Format("{0}:{1:D2}", (int)timeSpanMaximum.TotalMinutes, timeSpanMaximum.Seconds);
                    string currentTimeFM = convertTimeFormat(MediaTest.Position.TotalSeconds);
                    string maxTimeFM = convertTimeFormat(TimeSlider.Maximum);
                    DurationLabel.Content = currentTimeFM + "/" + maxTimeFM;
                }
                else
                {
                    _timer.Stop();
                }
            }
        }

        private string convertTimeFormat(double value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            string timeFormated = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            return timeFormated;
        }
    }
}

