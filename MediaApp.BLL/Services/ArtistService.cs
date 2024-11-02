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
        private ArtistRepository _repository = new();

        public List<TbArtist> GetAllArtists()
        {
            return _repository.GetAll();
        }
    }
}
