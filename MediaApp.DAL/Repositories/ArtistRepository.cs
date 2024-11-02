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
        public List<TbArtist> GetAllArtists()
        {
            _context = new();
            return _context.TbArtists.ToList();
        }
        public TbArtist GetArtistById(int id)
        {
            _context = new();
            var obj = _context.TbArtists.FirstOrDefault(a => a.ArtistId == id);
            return obj;
        }
        public void CreateArtist(TbArtist artist)
        {
            _context = new();
            _context.TbArtists.Add(artist);
            _context.SaveChanges();
        }
        public void UpdateArtist(TbArtist artist)
        {
            _context = new();
            _context.TbArtists.Update(artist);
            _context.SaveChanges();
        }
        public void DeleteArtist(TbArtist artist)
        {
            _context = new();
            _context.TbArtists.Remove(artist);
            _context.SaveChanges();
        }
    }
}
