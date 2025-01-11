using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaApp.DAL.Entities;

[Table("tb_song")]
public partial class TbSong
{
    [NotMapped]
    public bool IsSelected { get; set; } // Not mapped to the database.

    [Key]
    [Column("song_id")]
    public int SongId { get; set; }

    [Column("song_name")]
    [Required]
    [MaxLength(255)]
    public string SongName { get; set; } = null!;

    [Column("duration")]
    public decimal Duration { get; set; }

    [Column("file_path")]
    [Required]
    [MaxLength(500)]
    public string FilePath { get; set; } = null!;

    [Column("type")]
    [MaxLength(50)]
    public string? Type { get; set; }

    [Column("plays")]
    public int? Plays { get; set; }

    [Column("artist_id")]
    public int ArtistId { get; set; }

    [Column("album_id")]
    public int? AlbumId { get; set; }

    [ForeignKey("AlbumId")]
    public virtual TbAlbum? Album { get; set; }

    [ForeignKey("ArtistId")]
    public virtual TbArtist Artist { get; set; } = null!;

    public virtual ICollection<TbPlaylistSong> TbPlaylistSongs { get; set; } = new List<TbPlaylistSong>();
}
