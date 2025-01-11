using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaApp.DAL.Entities;

[Table("tb_artist")]
public partial class TbArtist
{
    [Key]
    [Column("artist_id")]
    public int ArtistId { get; set; }

    [Column("artist_name")]
    [Required]
    [MaxLength(255)]
    public string ArtistName { get; set; } = null!;

    [Column("date_of_birth")]
    public DateTime DataOfBirth { get; set; }

    [Column("description")]
    [MaxLength(1000)]
    public string? Description { get; set; }

    public virtual ICollection<TbAlbum> TbAlbums { get; set; } = new List<TbAlbum>();

    public virtual ICollection<TbSong> TbSongs { get; set; } = new List<TbSong>();
}
