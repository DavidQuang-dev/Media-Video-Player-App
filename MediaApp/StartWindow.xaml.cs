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
using video_media_player;

namespace MediaApp
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
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
