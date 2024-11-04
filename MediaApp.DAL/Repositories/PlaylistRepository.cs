﻿using MediaApp.DAL.Entities;
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
        public List<TbPlaylist> GetAll()
        {
            _context = new();
            return _context.TbPlaylists.ToList();
        }

    public List<TbPlaylist> Get2Playlist()
        {
            _context = new();
            return _context.TbPlaylists.Take(2).ToList();
        }
    }
}
