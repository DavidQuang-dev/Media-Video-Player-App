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
        public List<TbSong> GetPopularSongs()
        {
            _context = new();
            return _context.TbSongs.Take(4).ToList();
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
        public List<TbSong> GetSongByPlaylist(TbPlaylist playlist)
        {
            _context = new VideoMediaPlayerContext();
            var songs = _context.TbPlaylistSongs
                .Include(ps => ps.Song)             // Include Song first
                    .ThenInclude(song => song.Artist) // Then Include Artist from Song
                .Where(ps => ps.PlaylistId == playlist.PlaylistId)
                .Select(ps => ps.Song)
                .ToList();
            return songs;
        }
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

        public TbArtist GetArtist(int id)
        {
            _context = new();
            var obj = _context.TbArtists.FirstOrDefault(a => a.ArtistId == id);
            return obj;
        }

        public List<TbSong> GetAllSongWithOutAlbum()
        {
            _context = new VideoMediaPlayerContext();
            var songs = _context.TbSongs
            .Include(song => song.Artist)
            .Where(song => song.AlbumId == null) // Lọc những bài hát có AlbumId là null
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

        public TbSong GetSongById(int songId)
        {
            _context = new();
            return _context.TbSongs.Find(songId);
        }
    }
}
