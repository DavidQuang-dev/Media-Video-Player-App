﻿USE master
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE NAME='video_media_player')
		DROP database video_media_player
GO
/****** Object:  Database [video_media_player]    Script Date: 11/7/2024 2:28:31 PM ******/
CREATE DATABASE [video_media_player]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'video_media_player', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\video_media_player.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'video_media_player_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\video_media_player_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
USE video_media_player
GO
ALTER DATABASE [video_media_player] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [video_media_player].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [video_media_player] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [video_media_player] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [video_media_player] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [video_media_player] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [video_media_player] SET ARITHABORT OFF 
GO
ALTER DATABASE [video_media_player] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [video_media_player] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [video_media_player] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [video_media_player] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [video_media_player] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [video_media_player] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [video_media_player] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [video_media_player] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [video_media_player] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [video_media_player] SET  ENABLE_BROKER 
GO
ALTER DATABASE [video_media_player] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [video_media_player] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [video_media_player] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [video_media_player] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [video_media_player] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [video_media_player] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [video_media_player] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [video_media_player] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [video_media_player] SET  MULTI_USER 
GO
ALTER DATABASE [video_media_player] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [video_media_player] SET DB_CHAINING OFF 
GO
ALTER DATABASE [video_media_player] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [video_media_player] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [video_media_player] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [video_media_player] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [video_media_player] SET QUERY_STORE = OFF
GO
USE [video_media_player]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/7/2024 2:28:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Albums]    Script Date: 11/7/2024 2:28:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Albums](
	[Album_Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[CoverImage] [nvarchar](250) NULL,
	[Artist_Id] [int] NOT NULL,
 CONSTRAINT [PK_tb_Albums] PRIMARY KEY CLUSTERED 
(
	[Album_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Artists]    Script Date: 11/7/2024 2:28:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Artists](
	[Artist_Id] [int] IDENTITY(1,1) NOT NULL,
	[Artist_Name] [nvarchar](250) NOT NULL,
	[Data_Of_Birth] [datetime] NOT NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_tb_Artists] PRIMARY KEY CLUSTERED 
(
	[Artist_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Playlist_Songs]    Script Date: 11/7/2024 2:28:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Playlist_Songs](
	[Playlist_Songs_Id] [int] IDENTITY(1,1) NOT NULL,
	[Playlist_Id] [int] NULL,
	[Song_Id] [int] NULL,
 CONSTRAINT [PK_tb_Playlist_Songs] PRIMARY KEY CLUSTERED 
(
	[Playlist_Songs_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Playlists]    Script Date: 11/7/2024 2:28:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Playlists](
	[Playlist_Id] [int] IDENTITY(1,1) NOT NULL,
	[Playlist_Name] [nvarchar](250) NULL,
 CONSTRAINT [PK_tb_Playlists] PRIMARY KEY CLUSTERED 
(
	[Playlist_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Songs]    Script Date: 11/7/2024 2:28:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Songs](
	[Song_Id] [int] IDENTITY(1,1) NOT NULL,
	[Song_Name] [nvarchar](250) NOT NULL,
	[Duration] [decimal](18, 10) NOT NULL,
	[FilePath] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[Plays] [int] NULL,
	[Artist_Id] [int] NOT NULL,
	[Album_Id] [int] NULL,
 CONSTRAINT [PK_tb_Songs] PRIMARY KEY CLUSTERED 
(
	[Song_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_User]    Script Date: 11/7/2024 2:28:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_User](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [nvarchar](50) NULL,
	[Email] [nvarchar](250) NULL,
	[Password] [nvarchar](250) NULL,
	[Role] [nchar](10) NULL,
 CONSTRAINT [PK_tb_User] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_Albums]  WITH CHECK ADD  CONSTRAINT [FK_tb_Albums_tb_Artists] FOREIGN KEY([Artist_Id])
REFERENCES [dbo].[tb_Artists] ([Artist_Id])
GO
ALTER TABLE [dbo].[tb_Albums] CHECK CONSTRAINT [FK_tb_Albums_tb_Artists]
GO
ALTER TABLE [dbo].[tb_Playlist_Songs]  WITH CHECK ADD  CONSTRAINT [FK_tb_Playlist_Songs_tb_Playlists] FOREIGN KEY([Playlist_Id])
REFERENCES [dbo].[tb_Playlists] ([Playlist_Id])
GO
ALTER TABLE [dbo].[tb_Playlist_Songs] CHECK CONSTRAINT [FK_tb_Playlist_Songs_tb_Playlists]
GO
ALTER TABLE [dbo].[tb_Playlist_Songs]  WITH CHECK ADD  CONSTRAINT [FK_tb_Playlist_Songs_tb_Songs] FOREIGN KEY([Song_Id])
REFERENCES [dbo].[tb_Songs] ([Song_Id])
GO
ALTER TABLE [dbo].[tb_Playlist_Songs] CHECK CONSTRAINT [FK_tb_Playlist_Songs_tb_Songs]
GO
ALTER TABLE [dbo].[tb_Songs]  WITH CHECK ADD  CONSTRAINT [FK_tb_Songs_tb_Albums] FOREIGN KEY([Album_Id])
REFERENCES [dbo].[tb_Albums] ([Album_Id])
GO
ALTER TABLE [dbo].[tb_Songs] CHECK CONSTRAINT [FK_tb_Songs_tb_Albums]
GO
ALTER TABLE [dbo].[tb_Songs]  WITH CHECK ADD  CONSTRAINT [FK_tb_Songs_tb_Artists] FOREIGN KEY([Artist_Id])
REFERENCES [dbo].[tb_Artists] ([Artist_Id])
GO
ALTER TABLE [dbo].[tb_Songs] CHECK CONSTRAINT [FK_tb_Songs_tb_Artists]
GO
USE [master]
GO
ALTER DATABASE [video_media_player] SET  READ_WRITE 
GO