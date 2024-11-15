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
    public class PlaylistService
    {
        //khứa này cung cấp/nhận data cho/từ GUI,
        private PlaylistRepository _repo = new();
        public void DeletePlayList(TbPlaylist obj)
        {
            _repo.Delete(obj);
        }
        public void UpdatePlayList(TbPlaylist obj)
        {
            _repo.Update(obj);
        }
        public void CreatePlayList(TbPlaylist obj)
        {
            _repo.Create(obj);
        }
        public List<TbPlaylist> GetAllPlayList()
        {
            return _repo.GetAll();
        }
        public List<TbPlaylist> Get2Playlist()
        {
            return _repo.Get2Playlist();
        }

        public List<TbPlaylist> GetAllWithSongs()
        {
            return _repo.GetAllWithSongs();
        }

        //public TbPlaylist GetPlaylistByName(string name)
        //{
        //    return _repo.GetPlaylistByName(name);
        //}

        public List<TbPlaylist> GetPlaylistByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<TbPlaylist>();

            // Gọi phương thức GetPlaylistsByName từ _repo
            return _repo.GetPlaylistByName(name);
        }

    }
}
