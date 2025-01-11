using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaApp.DAL.Entities;

[Table("tb_playlist_song")]
public partial class TbPlaylistSong
{
    [Key]
    [Column("playlist_songs_id")]
    public int PlaylistSongsId { get; set; }

    [Column("playlist_id")]
    public int? PlaylistId { get; set; }

    [Column("song_id")]
    public int? SongId { get; set; }

    [ForeignKey("PlaylistId")]
    public virtual TbPlaylist? Playlist { get; set; }

    [ForeignKey("SongId")]
    public virtual TbSong? Song { get; set; }
}
