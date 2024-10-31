using System;
using System.Collections.Generic;

namespace MediaApp.DAL.Entities;

public partial class TbPlaylist
{
    public int PlaylistId { get; set; }

    public string? PlaylistName { get; set; }

    public virtual ICollection<TbPlaylistSong> TbPlaylistSongs { get; set; } = new List<TbPlaylistSong>();
}
