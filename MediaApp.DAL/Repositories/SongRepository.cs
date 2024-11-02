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

        public List<TbSong> GetSongByAlbum(TbAlbum album)
        {
            _context = new VideoMediaPlayerContext();
            var songs = _context.TbSongs.Where(song => song.Album == album).Include(o => o.Artist)
                .Select(song => new TbSong
                {
                    SongId = song.SongId,
                    ArtistId = song.ArtistId,
                    Duration = song.Duration,
                    FilePath = song.FilePath,
                    SongName = song.SongName,
                    Artist = song.Artist
                });
            return songs.ToList();
        }

        public List<TbSong> GetAll()
        {
            _context = new VideoMediaPlayerContext();
            var songs = _context.TbSongs.Include(o => o.Artist)
                .Select(song => new TbSong
                {
                    SongId = song.SongId,
                    ArtistId = song.ArtistId,
                    Duration = song.Duration,
                    FilePath = song.FilePath,
                    SongName = song.SongName,
                    Artist = song.Artist
                });
            return songs.ToList();
        }

        public List<TbSong> GetSongWithArtist(TbSong tbSong)
        {
            _context = new VideoMediaPlayerContext();
            return _context.TbSongs.ToList();
        }

        public TbSong GetSong(int songId)
        {
            _context = new();
            return _context.TbSongs.Find(songId);
        }

        public void UpdateSong(TbSong song)
        {
            _context = new();
            _context.TbSongs.Update(song);
            _context.SaveChanges();
        }
    }
}
