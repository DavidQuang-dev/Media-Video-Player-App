using System;
using System.Collections.Generic;
using MediaApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MediaApp.DAL;

public partial class VideoMediaPlayerContext : DbContext
{
    public VideoMediaPlayerContext()
    {
    }

    public VideoMediaPlayerContext(DbContextOptions<VideoMediaPlayerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAlbum> TbAlbums { get; set; }

    public virtual DbSet<TbArtist> TbArtists { get; set; }

    public virtual DbSet<TbPlaylist> TbPlaylists { get; set; }

    public virtual DbSet<TbPlaylistSong> TbPlaylistSongs { get; set; }

    public virtual DbSet<TbSong> TbSongs { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAlbum>(entity =>
        {
            entity.HasKey(e => e.AlbumId);

            entity.ToTable("tb_albums");

            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.CoverImage).HasMaxLength(250).HasColumnName("cover_image");
            entity.Property(e => e.Title).HasMaxLength(250).HasColumnName("title");

            entity.HasOne(d => d.Artist).WithMany(p => p.TbAlbums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_albums_tb_artists");
        });

        modelBuilder.Entity<TbArtist>(entity =>
        {
            entity.HasKey(e => e.ArtistId);

            entity.ToTable("tb_artists");

            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(250)
                .HasColumnName("artist_name");
            entity.Property(e => e.DataOfBirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Description).HasColumnType("text").HasColumnName("description");
        });

        modelBuilder.Entity<TbPlaylist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId);

            entity.ToTable("tb_playlists");

            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.PlaylistName)
                .HasMaxLength(250)
                .HasColumnName("playlist_name");
        });

        modelBuilder.Entity<TbPlaylistSong>(entity =>
        {
            entity.HasKey(e => e.PlaylistSongsId);

            entity.ToTable("tb_playlist_songs");

            entity.Property(e => e.PlaylistSongsId).HasColumnName("playlist_songs_id");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.SongId).HasColumnName("song_id");

            entity.HasOne(d => d.Playlist).WithMany(p => p.TbPlaylistSongs)
                .HasForeignKey(d => d.PlaylistId)
                .HasConstraintName("fk_tb_playlist_songs_tb_playlists");

            entity.HasOne(d => d.Song).WithMany(p => p.TbPlaylistSongs)
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("fk_tb_playlist_songs_tb_songs");
        });

        modelBuilder.Entity<TbSong>(entity =>
        {
            entity.HasKey(e => e.SongId);

            entity.ToTable("tb_songs");

            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.Duration).HasColumnType("numeric(18, 10)").HasColumnName("duration");
            entity.Property(e => e.SongName)
                .HasMaxLength(250)
                .HasColumnName("song_name");
            entity.Property(e => e.Type).HasMaxLength(50).HasColumnName("type");

            entity.HasOne(d => d.Album).WithMany(p => p.TbSongs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("fk_tb_songs_tb_albums");

            entity.HasOne(d => d.Artist).WithMany(p => p.TbSongs)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_songs_tb_artists");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("tb_users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email).HasMaxLength(250).HasColumnName("email");
            entity.Property(e => e.Password).HasMaxLength(250).HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("role");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
