using MediaApp.DAL.Entities;
using MediaApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.BLL.Services
{
    public class ArtistService
    {
        private ArtistRepository _repo = new();

        public List<TbArtist> GetAll()
        {
            return _repo.GetAllArtists();
        }
        public TbArtist GetArtistById(int id)
        {
            return _repo.GetArtistById(id);
        }
        public void Create(TbArtist artist)
        {
            _repo.CreateArtist(artist);
        }
        public void Update(TbArtist artist) {
            _repo.UpdateArtist(artist);
        }
        public void Delete(TbArtist artist)
        {
            _repo.DeleteArtist(artist);
        }
    }
}
