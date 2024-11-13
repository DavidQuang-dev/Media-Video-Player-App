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
    public partial class StorePage : Window
    {

        public StorePage()
        {
            InitializeComponent();
        }

        private void ManageAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            AlbumManage albumManage = new();
            albumManage.ShowDialog();
        }

        private void ManageSongButton_Click(object sender, RoutedEventArgs e)
        {
            ManageSong manageSong = new();
            manageSong.ShowDialog();
        }

        private void ArtistManageButton_Click(object sender, RoutedEventArgs e)
        {
            ManageArtist manageArtist = new();
            manageArtist.ShowDialog();
        }

        private void PlaylistsManageButton_Click(object sender, RoutedEventArgs e)
        {
            PlaylistManage playlistManage = new();
            playlistManage.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = (StartWindow) Application.Current.MainWindow;
            startWindow.Hide();
        }
    }
}

