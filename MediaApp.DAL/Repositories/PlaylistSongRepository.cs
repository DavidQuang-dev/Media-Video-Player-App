using MediaApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.DAL.Repositories
{
    public class PlaylistSongRepository
    {
        private VideoMediaPlayerContext _context;

        public void Delete(TbPlaylistSong obj)
        {
            _context = new();
            _context.TbPlaylistSongs.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(TbPlaylistSong obj)
        {
            _context = new();
            _context.TbPlaylistSongs.Update(obj);
            _context.SaveChanges();
        }
        public void Create(TbPlaylistSong obj)
        {
            _context = new ();
            _context.TbPlaylistSongs.Add (obj);
            _context.SaveChanges();
        }
        public List<TbPlaylistSong> GetAll()
        {
            _context = new ();
            return _context.TbPlaylistSongs.ToList();
        }
        public List<TbPlaylistSong> GetSongByPlaylistId(int playlistId)
        {
            _context = new();
            return _context.TbPlaylistSongs
                .Where(ps => ps.PlaylistId == playlistId)
                .ToList();
        }
        public TbPlaylistSong GetByPlaylistAndSong(int playlistId, int songId)
        {
            _context = new();
                return _context.TbPlaylistSongs
                .FirstOrDefault(ps => ps.PlaylistId == playlistId && ps.SongId == songId);
        }
    }
}
