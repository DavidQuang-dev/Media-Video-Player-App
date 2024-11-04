using MediaApp.DAL.Entities;
using MediaApp.DAL.Repositories;
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
        public TbPlaylist? GetCreatedPlaylist()
        {
            return _repo.GetCreatePlaylist();
        }
    }
}
