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
            PlaylistDetailWindow detail = new();
            detail.ShowDialog();

            //f5 lưới
            FillListBox(_playlistService.GetAllPlayList());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PlaylistsListBox.ItemsSource = _playlistService.GetAllPlayList();
        }

        //helper function

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TbPlaylist? selected = PlaylistsListBox.SelectedItem as TbPlaylist;
            if (selected == null)
            {
                MessageBox.Show("Please select a playlist before editing!!", "Select One", MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }
            PlaylistDetailWindow detail = new();
            detail.EditedOne = selected;
            detail.ShowDialog();

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
    }
}
