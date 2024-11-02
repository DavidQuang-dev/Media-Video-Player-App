using MediaApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.DAL.Repositories
{
    public class AlbumRepository
    {
        private VideoMediaPlayerContext _context;

        public List<TbAlbum> GetAll()
        {
            _context = new VideoMediaPlayerContext();
            var orders = _context.TbAlbums.Include(o => o.Artist).ToList();
            return orders;
        }

        public void Create(TbAlbum album)
        {
            _context = new VideoMediaPlayerContext();
            _context.TbAlbums.Add(album);
            _context.SaveChanges();
        }

        public void Update(TbAlbum album)
        {
            _context = new VideoMediaPlayerContext();
            _context.TbAlbums.Update(album);
            _context.SaveChanges();
        }
    }
}
