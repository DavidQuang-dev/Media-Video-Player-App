using System;
using System.Collections.Generic;

namespace MediaApp.DAL.Entities;

public partial class TbSong
{
    public bool IsSelected;

    public int SongId { get; set; }

    public string SongName { get; set; } = null!;

    public decimal Duration { get; set; }

    public string FilePath { get; set; } = null!;

    public string? Type { get; set; }

    public int? Plays { get; set; }

    public int ArtistId { get; set; }

    public int? AlbumId { get; set; }

    public virtual TbAlbum? Album { get; set; }

    public virtual TbArtist Artist { get; set; } = null!;

    public virtual ICollection<TbPlaylistSong> TbPlaylistSongs { get; set; } = new List<TbPlaylistSong>();
}
