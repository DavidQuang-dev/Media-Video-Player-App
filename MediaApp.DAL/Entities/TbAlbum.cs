using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaApp.DAL.Entities;

[Table("tb_album")]
public partial class TbAlbum
{
    [Key]
    [Column("album_id")]
    public int AlbumId { get; set; }

    [Column("title")]
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = null!;

    [Column("cover_image")]
    [MaxLength(500)]
    public string? CoverImage { get; set; }

    [Column("artist_id")]
    public int ArtistId { get; set; }

    [ForeignKey("ArtistId")]
    public virtual TbArtist Artist { get; set; } = null!;

    public virtual ICollection<TbSong> TbSongs { get; set; } = new List<TbSong>();
}
