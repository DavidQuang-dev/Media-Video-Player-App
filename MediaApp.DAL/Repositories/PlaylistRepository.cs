using MediaApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.DAL.Repositories
{
    //class này gọi db context để giúp thao tác crud trên table tblPlaylist
    public class PlaylistRepository
    {
        private VideoMediaPlayerContext _context;

        public void Delete(TbPlaylist obj)
        {
            _context = new();
            _context.TbPlaylists.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(TbPlaylist obj)
        {
            _context = new();
            _context.TbPlaylists.Update(obj);
            _context.SaveChanges();
        }
        public void Create(TbPlaylist obj)
        {
            _context = new();
            _context.TbPlaylists.Add(obj);
            _context.SaveChanges();
        }

        public TbPlaylist? GetCreatePlaylist()
        {
            return _context.TbPlaylists
                 .OrderByDescending(playlist => playlist.PlaylistId)
                 .FirstOrDefault();
        }
        public List<TbPlaylist> GetAllWithSongs()
        {
            return _context.TbPlaylists
                .Include(p => p.TbPlaylistSongs)
                .ThenInclude(ps => ps.Song)
                .ThenInclude(s => s.Artist) // Include artist for each song
                .ToList();
        }
        public List<TbPlaylist> GetAll()
        {
            _context = new();
            return _context.TbPlaylists.Include(p => p.TbPlaylistSongs).ThenInclude(ps => ps.Song).ToList();
        }

    public List<TbPlaylist> Get2Playlist()
        {
            _context = new();
            return _context.TbPlaylists.Take(2).ToList();
        }
    }
}
