using System;
using System.Collections.Generic;

namespace MediaApp.DAL.Entities;

public partial class TbArtist
{
    public int ArtistId { get; set; }

    public string ArtistName { get; set; } = null!;

    public DateTime DataOfBirth { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<TbAlbum> TbAlbums { get; set; } = new List<TbAlbum>();

    public virtual ICollection<TbSong> TbSongs { get; set; } = new List<TbSong>();
}
