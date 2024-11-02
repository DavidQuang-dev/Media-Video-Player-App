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

        public void CreateAlbum(TbAlbum album)
        {
            _repository.Create(album);
        }

        public void UpdateAlbum(TbAlbum album)
        {
            _repository.Update(album);
        }
    }
}
