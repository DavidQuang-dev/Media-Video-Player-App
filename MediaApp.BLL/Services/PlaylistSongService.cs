using MediaApp.DAL.Entities;
using MediaApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.BLL.Services
{
    public class PlaylistSongService
    {
        private PlaylistSongRepository _repo = new();
        public void DeletePlaylistSongs(TbPlaylistSong obj)
        {
            _repo.Delete(obj);
        }
        public void UpdatePlaylistSongs(TbPlaylistSong obj)
        {
            _repo.Update(obj);
        }
        public void CreatePlaylistSongs(TbPlaylistSong obj)
        {
            _repo.Create(obj);
        }
        public List<TbPlaylistSong> GetAllPlaylistSongs()
        {
            return _repo.GetAll();
        }
        public List<TbPlaylistSong> GetSongsByPlaylistId(int playlistId)
        {
            return _repo.GetSongByPlaylistId(playlistId);
        }
        public TbPlaylistSong GetByPlaylistAndSong(int playlistId, int songId)
        {
            return _repo.GetByPlaylistAndSong(playlistId, songId);
        }
    }
}
