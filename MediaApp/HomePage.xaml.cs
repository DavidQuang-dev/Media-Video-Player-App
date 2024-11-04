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
using video_media_player.UserControls;

namespace video_media_player
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly SongService songService = new();
        private readonly PlaylistService playlistService = new();
        public TbSong ChooseSong { get; set; }
        public HomePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<TbSong> songs = songService.GetAll();
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

            List<TbSong> popularSongs = songService.GetPopularSong();
            foreach (var song in popularSongs)
            {
                var popularSongItem = new video_media_player.UserControls.PopularSong
                {
                    Title = song.SongName,
                    Time = ConvertTimeFormat(TagLib.File.Create(song.FilePath).Properties.Duration.TotalSeconds),
                    Tag = song.FilePath
                };
                popularSongItem.Click += PopularSongItem_Click;
                PopularSongItemList.Children.Add(popularSongItem);
            }

            List<TbPlaylist> playlists = playlistService.Get2Playlist();
            foreach (var playlist in playlists)
            {
                var playlistItem = new video_media_player.UserControls.Playlist
                {
                    Title = playlist.PlaylistName,
                };
                PlaylistItems.Children.Add(playlistItem);
            }
        }

        private void SongItem_Click(object sender, RoutedEventArgs e)
        {
            video_media_player.UserControls.SongItem songItem = (video_media_player.UserControls.SongItem)sender;
            string songName = songItem.Title.ToString();
            if (!string.IsNullOrEmpty(songName))
            {
                ChooseSong = songService.GetSongByName(songName);
                //MessageBox.Show(ChooseSong.SongName);
            }
        }
        private void PopularSongItem_Click(object sender, RoutedEventArgs e)
        {
            video_media_player.UserControls.PopularSong popularSong = (video_media_player.UserControls.PopularSong)sender;
            string filePath = popularSong.Tag.ToString();
            if (!string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show(filePath);
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
