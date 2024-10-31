using System;
using System.Collections.Generic;

namespace MediaApp.DAL.Entities;

public partial class TbPlaylistSong
{
    public int PlaylistSongsId { get; set; }

    public int? PlaylistId { get; set; }

    public int? SongId { get; set; }

    public virtual TbPlaylist? Playlist { get; set; }

    public virtual TbSong? Song { get; set; }
}
