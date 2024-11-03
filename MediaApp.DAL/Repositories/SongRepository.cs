//using MediaApp.DAL.Entities;
using MediaApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.DAL.Repositories
{
    public class SongRepository
    {
        private VideoMediaPlayerContext _context;

        public List<TbSong> GetAllSongs()
        {
            _context = new();
            return _context.TbSongs.Include(song => song.Artist).Include(al => al.Album).ToList();
        }

        public void CreateSong(TbSong song)
        {
            _context = new();
            _context.TbSongs.Add(song);
            _context.SaveChanges();
        }

        public void UpdateSong(TbSong song)
        {
            _context = new();
            _context.TbSongs.Update(song);
            _context.SaveChanges();
        }

        public void DeleteSong(TbSong song)
        {
            _context = new();
            _context.TbSongs.Remove(song);
            _context.SaveChanges();
        }
        
        public List<TbSong> GetSongByAlbum(TbAlbum album)
        {
            _context = new VideoMediaPlayerContext();
            var songs = _context.TbSongs.Where(song => song.Album == album).Include(o => o.Artist);
            return songs.ToList();
        }

        public TbArtist GetArtist(int id)
        {
            _context = new();
            var obj = _context.TbArtists.FirstOrDefault(a => a.ArtistId == id);
            return obj;
        }
    }
}
