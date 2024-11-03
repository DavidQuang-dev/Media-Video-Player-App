using System;
using System.Collections.Generic;

namespace MediaApp.DAL.Entities;

public partial class TbSong
{
    public int SongId { get; set; }

    public string SongName { get; set; } = null!;

    public decimal Duration { get; set; }

    public string FilePath { get; set; } = null!;

    public int ArtistId { get; set; }

    public int? AlbumId { get; set; }

    public virtual TbAlbum? Album { get; set; } = null!;

    public virtual TbArtist Artist { get; set; } = null!;

    public virtual ICollection<TbPlaylistSong> TbPlaylistSongs { get; set; } = new List<TbPlaylistSong>();
}
