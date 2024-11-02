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
        private SongRepository _repository = new();

        public List<TbSong> GetSongsByAlbum(TbAlbum album)
        {
            return _repository.GetSongByAlbum(album);
        }

        public List<TbSong> GetAllSongs()
        {
            return _repository.GetAll();
        }

        public TbSong GetSong(int id)
        {
            return _repository.GetSong(id);
        }

        public void Update(TbSong song)
        {
            _repository.UpdateSong(song);
        }
    }
}
