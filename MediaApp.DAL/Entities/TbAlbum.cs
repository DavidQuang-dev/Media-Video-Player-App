using System;
using System.Collections.Generic;

namespace MediaApp.DAL.Entities;

public partial class TbAlbum
{
    public int AlbumId { get; set; }

    public string Title { get; set; } = null!;

    public string? CoverImage { get; set; }

    public int ArtistId { get; set; }

    public virtual TbArtist Artist { get; set; } = null!;

    public virtual ICollection<TbSong> TbSongs { get; set; } = new List<TbSong>();
}
