using MediaApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.DAL.Repositories
{
    public class ArtistRepository
    {
        private VideoMediaPlayerContext _context;

        public List<TbArtist> GetAll()
        {
            _context = new VideoMediaPlayerContext();
            return _context.TbArtists.ToList();
        }
    }
}
