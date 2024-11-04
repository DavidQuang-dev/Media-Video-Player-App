using MediaApp;
using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
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
using video_media_player.UserControls;

namespace video_media_player
{
    /// <summary>
    /// Interaction logic for PlaylistsPage.xaml
    /// </summary>
    public partial class PlaylistsPage : Page
    {
        private PlaylistService _playlistService = new();
        public PlaylistsPage()
        {
            InitializeComponent();
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            //f5 lưới
            PlaylistDetail detail = new();
            detail.ShowDialog();
            FillListBox(_playlistService.GetAllPlayList());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PlaylistsListBox.ItemsSource = _playlistService.GetAllPlayList();
        }

        //helper function

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FillListBox(_playlistService.GetAllPlayList());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TbPlaylist? selected = PlaylistsListBox.SelectedItem as TbPlaylist;
            if (selected == null)
            {
                MessageBox.Show("Please select a playlist before deleting!!", "Select One", MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }

            MessageBoxResult answer = MessageBox.Show($"Are you sure about delete playlist {selected.PlaylistName}", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (answer == MessageBoxResult.No)
            {
                return;
            }
            _playlistService.DeletePlayList(selected);
            FillListBox(_playlistService.GetAllPlayList());
        }
        private void FillListBox(List<TbPlaylist> list)
        {
            PlaylistsListBox.ItemsSource = null;
            PlaylistsListBox.ItemsSource = list;
        }

        private void PlaylistsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected playlist
            var selectedPlaylist = PlaylistsListBox.SelectedItem as Playlist; // Make sure to replace 'Playlist' with the actual type you are using for your playlists.

            if (selectedPlaylist != null)
            {
                // Assuming that your Playlist class has a property that gives you the list of songs.
                //var songs = selectedPlaylist.Songs; // Replace with your actual property that holds the song list.

                // Update the ItemsControl with the songs
                // Assuming your ItemsControl is called 'PlaylistDetail' and it has been set to show songs.
                //PlaylistDetail.ItemsSource = songs; // You may need to set the DataContext if you are binding
            }
        }

    }
}
