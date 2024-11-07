using MediaApp.DAL;
using MediaApp.DAL.Entities;
using MediaApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.BLL.Services
{
    public class AlbumService
    {
        private AlbumRepository _repository = new();

        public List<TbAlbum> GetAllAlbums()
        {
            return _repository.GetAll();
        }

        public TbAlbum GetAlbumsById(int id)
        {
            return _repository.GetAlbumById(id);
        }
        public void CreateAlbum(TbAlbum album)
        {
            _repository.Create(album);
        }

        public void UpdateAlbum(TbAlbum album)
        {
            _repository.Update(album);
        }

        public void DeleteAlbum(TbAlbum album)
        {
            _repository.Delete(album);
        }

        public TbAlbum? GetCreatedAlbum()
        {
            return _repository.GetCreatedAlbum();
        }
    }
}
