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
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

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
            entity.Property(e => e.DataOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("Data_Of_Birth");
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
            entity.Property(e => e.Duration).HasColumnType("decimal(18, 10)");
            entity.Property(e => e.SongName)
                .HasMaxLength(250)
                .HasColumnName("Song_Name");
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Album).WithMany(p => p.TbSongs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK_tb_Songs_tb_Albums");

            entity.HasOne(d => d.Artist).WithMany(p => p.TbSongs)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_Songs_tb_Artists");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("tb_User");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
