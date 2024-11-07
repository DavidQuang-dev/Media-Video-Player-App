using MediaApp.DAL.Entities;
using MediaApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.BLL.Services
{
    public class SongService
    {
        private SongRepository _songRepository = new ();
        public List<TbSong> GetAll()
        {
            return _songRepository.GetAllSongs();
        }
        public List<TbSong> GetPopularSong()
        {
            return _songRepository.GetPopularSongs();
        }
        public List<TbSong> GetMusicVideos()
        {
            return _songRepository.GetMusicVideos();
        }
        public void Create(TbSong song)
        {
            _songRepository.CreateSong(song);
        }

        public void Update(TbSong song)
        {
            _songRepository.UpdateSong(song);
        }
        public void UpdatePlaysSong(int plays, int songId)
        {
            _songRepository.UpdatePlaysSong(plays, songId);
        }

        public void Delete(TbSong song) 
        {
            _songRepository.DeleteSong(song);
        }

        public List<TbSong> GetSongsByAlbum(TbAlbum album)
        {
            return _songRepository.GetSongByAlbum(album);
        }
        public List<TbSong> GetSongsByPlaylist(TbPlaylist playlist)
        {
            return _songRepository.GetSongByPlaylist(playlist);
        }

        public List<TbSong> GetAllSongWithOutAlbum()
        {
            return _songRepository.GetAllSongWithOutAlbum();
        }

        public TbSong GetSongById(int id)
        {
            return _songRepository.GetSongById(id);
        }

        public TbSong GetSongByName(string songName)
        {
            return _songRepository.GetSongByName(songName);
        }

        public List<TbSong> GetAvailableSongsForPlaylist(List<int> excludedSongIds)
        {
            return _songRepository.GetAvailableSongsForPlaylist(excludedSongIds);
        }
    }
}
