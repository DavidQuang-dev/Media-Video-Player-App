using MediaApp.DAL.Entities;
using MediaApp.DAL.Repositories;
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

        public void Create(TbSong song)
        {
            _songRepository.CreateSong(song);
        }

        public void Update(TbSong song)
        {
            _songRepository.UpdateSong(song);
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
            //Chỉ lấy những bài hát chưa có trong songdatagrid trong trang playlistDetail
            return _songRepository.GetAllSongs()
                   .Where(song => !excludedSongIds.Contains(song.SongId))
                   .ToList();
        }
    }
}
