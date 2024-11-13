﻿using MediaApp.BLL.Services;
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
    /// Interaction logic for PlaylistManage.xaml
    /// </summary>
    public partial class PlaylistManage : Window
    {
        private PlaylistService _playlistService = new();
        private SongService _songService = new();
        private PlaylistSongService _playlistSongService = new();
        public PlaylistManage()
        {
            InitializeComponent();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PlayListDataGrid.ItemsSource = _playlistService.GetAllPlayList();
        }

        private void FillDataGrid(List<TbPlaylist> list)
        {
            PlayListDataGrid.ItemsSource = null;
            PlayListDataGrid.ItemsSource = list;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            PlaylistDetail plDetail = new();
            plDetail.ShowDialog();

            FillDataGrid(_playlistService.GetAllPlayList());
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            TbPlaylist? selected = PlayListDataGrid.SelectedItem as TbPlaylist;
            if (selected == null)
            {
                MessageBox.Show("Please select a playlist before editing!!", "Select One", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            PlaylistDetail plDetail = new();
            plDetail.EditedOne = selected;
            plDetail.ShowDialog();

            FillDataGrid(_playlistService.GetAllPlayList());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TbPlaylist? selected = PlayListDataGrid.SelectedItem as TbPlaylist;
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
            FillDataGrid(_playlistService.GetAllPlayList());
        }
    }
}
