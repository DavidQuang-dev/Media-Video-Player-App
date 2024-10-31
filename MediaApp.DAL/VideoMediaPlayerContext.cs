using System;
using System.Collections.Generic;
using MediaApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=video_media_player;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAlbum>(entity =>
        {
            entity.HasKey(e => e.AlbumId);

            entity.ToTable("tb_Albums");

            entity.Property(e => e.AlbumId).HasColumnName("Album_Id");
            entity.Property(e => e.ArtistId).HasColumnName("Artist_Id");
            entity.Property(e => e.CoverImage).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Artist).WithMany(p => p.TbAlbums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_Albums_tb_Artists");
        });

        modelBuilder.Entity<TbArtist>(entity =>
        {
            entity.HasKey(e => e.ArtistId);

            entity.ToTable("tb_Artists");

            entity.Property(e => e.ArtistId).HasColumnName("Artist_Id");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(250)
                .HasColumnName("Artist_Name");
            entity.Property(e => e.DataOfBirth).HasColumnName("Data_Of_Birth");
            entity.Property(e => e.Description).HasColumnType("text");
        });

        modelBuilder.Entity<TbPlaylist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId);

            entity.ToTable("tb_Playlists");

            entity.Property(e => e.PlaylistId).HasColumnName("Playlist_Id");
            entity.Property(e => e.PlaylistName)
                .HasMaxLength(250)
                .HasColumnName("Playlist_Name");
        });

        modelBuilder.Entity<TbPlaylistSong>(entity =>
        {
            entity.HasKey(e => e.PlaylistSongsId);

            entity.ToTable("tb_Playlist_Songs");

            entity.Property(e => e.PlaylistSongsId).HasColumnName("Playlist_Songs_Id");
            entity.Property(e => e.PlaylistId).HasColumnName("Playlist_Id");
            entity.Property(e => e.SongId).HasColumnName("Song_Id");

            entity.HasOne(d => d.Playlist).WithMany(p => p.TbPlaylistSongs)
                .HasForeignKey(d => d.PlaylistId)
                .HasConstraintName("FK_tb_Playlist_Songs_tb_Playlists");

            entity.HasOne(d => d.Song).WithMany(p => p.TbPlaylistSongs)
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("FK_tb_Playlist_Songs_tb_Songs");
        });

        modelBuilder.Entity<TbSong>(entity =>
        {
            entity.HasKey(e => e.SongId);

            entity.ToTable("tb_Songs");

            entity.Property(e => e.SongId).HasColumnName("Song_Id");
            entity.Property(e => e.AlbumId).HasColumnName("Album_Id");
            entity.Property(e => e.ArtistId).HasColumnName("Artist_Id");
            entity.Property(e => e.SongName)
                .HasMaxLength(250)
                .HasColumnName("Song_Name");

            entity.HasOne(d => d.Album).WithMany(p => p.TbSongs)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_Songs_tb_Albums");

            entity.HasOne(d => d.Artist).WithMany(p => p.TbSongs)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_Songs_tb_Artists");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
