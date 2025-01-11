using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaApp.DAL.Entities;

[Table("tb_playlist")]
public partial class TbPlaylist
{
    [Key]
    [Column("playlist_id")]
    public int PlaylistId { get; set; }

    [Column("playlist_name")]
    [MaxLength(255)]
    public string? PlaylistName { get; set; }

    public virtual ICollection<TbPlaylistSong> TbPlaylistSongs { get; set; } = new List<TbPlaylistSong>();
}
