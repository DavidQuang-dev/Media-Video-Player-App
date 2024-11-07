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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace video_media_player
{
    /// <summary>
    /// Interaction logic for SongsPage.xaml
    /// </summary>
    public partial class SongsPage : Page
    {
        private SongService _songService = new();
        public TbSong ChooseSong { get; set; }

        public SongsPage()
        {
            InitializeComponent();
        }

        public string Artist { get; internal set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //SongsList.ItemsSource = _songService.GetAll();
            List<TbSong> songs = _songService.GetAll();
            int number = 0;
            foreach (var song in songs)
            {
                var songItem = new video_media_player.UserControls.SongItem
                {
                    Title = song.SongName,
                    Number = (++number).ToString(),
                    Time = ConvertTimeFormat(TagLib.File.Create(song.FilePath).Properties.Duration.TotalSeconds),
                    Tag = song.FilePath
                };
                songItem.Click += SongItem_Click;
                SongItemList.Children.Add(songItem);
            }
        }

        private void SongItem_Click(object sender, RoutedEventArgs e)
        {
            video_media_player.UserControls.SongItem songItem = (video_media_player.UserControls.SongItem)sender;
            string songName = songItem.Title.ToString();
            if (!string.IsNullOrEmpty(songName))
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.SetChosenSong(_songService.GetSongByName(songName));
            }
        }

        private string ConvertTimeFormat(double value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            string timeFormated = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            return timeFormated;
        }
    }
}