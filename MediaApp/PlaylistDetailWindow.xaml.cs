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
using System.Windows.Shapes;


namespace MediaApp
{
    /// <summary>
    /// Interaction logic for PlaylistDetailWindow.xaml
    /// </summary>
    public partial class PlaylistDetailWindow : Window
    {
        private PlaylistService _playlistService = new();
        public TbPlaylist EditedOne { get; set; }
        public PlaylistDetailWindow()
        {
            InitializeComponent();
        }

        private void SavePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            TbPlaylist obj = new();

            obj.PlaylistName = PlaylistNameTextBox.Text;
            if (EditedOne != null)
            {
                obj.PlaylistId = EditedOne.PlaylistId;
                _playlistService.UpdatePlayList(obj);
            }
            else
            {
                _playlistService.CreatePlayList(obj);
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (EditedOne != null)
            {
                //PlaylistNameTextBox.Text = EditedOne.PlaylistName.ToString();
            }
        }
    }
}
