using MediaApp;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog _openFileDialog = new()
            {
                Multiselect = true
            };
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            ManageSong manageSong = new();
            manageSong.ShowDialog();
            //DetailSong _detailSong = new();
            //_detailSong.ShowDialog();
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            ManageSong manageSong = new();
            manageSong.ShowDialog();
        }
    }
}
