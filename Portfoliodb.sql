USE [master]
GO
/****** Object:  Database [PortfolioDb]    Script Date: 12/2/2021 4:21:39 PM ******/
CREATE DATABASE [PortfolioDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PortfolioDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.LOCAL\MSSQL\DATA\PortfolioDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PortfolioDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.LOCAL\MSSQL\DATA\PortfolioDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PortfolioDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PortfolioDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PortfolioDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PortfolioDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PortfolioDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PortfolioDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [PortfolioDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PortfolioDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PortfolioDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PortfolioDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PortfolioDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PortfolioDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PortfolioDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PortfolioDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PortfolioDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PortfolioDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PortfolioDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PortfolioDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PortfolioDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PortfolioDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PortfolioDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PortfolioDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PortfolioDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PortfolioDb] SET RECOVERY FULL 
GO
ALTER DATABASE [PortfolioDb] SET  MULTI_USER 
GO
ALTER DATABASE [PortfolioDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PortfolioDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PortfolioDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PortfolioDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PortfolioDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PortfolioDb', N'ON'
GO
USE [PortfolioDb]
GO
/****** Object:  Table [dbo].[PortfolioPicture]    Script Date: 12/2/2021 4:21:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortfolioPicture](
	[PicId] [int] IDENTITY(1,1) NOT NULL,
	[PortfolioId] [nvarchar](50) NULL,
	[Picture] [nvarchar](max) NULL,
 CONSTRAINT [PK_PortfolioPicture] PRIMARY KEY CLUSTERED 
(
	[PicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Portfolios]    Script Date: 12/2/2021 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portfolios](
	[PortfolioId] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](1000) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Portfolios] PRIMARY KEY CLUSTERED 
(
	[PortfolioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleActions]    Script Date: 12/2/2021 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleActions](
	[ActionId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[Insert] [int] NULL,
	[Update] [int] NULL,
	[Delete] [int] NULL,
	[View] [int] NULL,
 CONSTRAINT [PK_RoleAction] PRIMARY KEY CLUSTERED 
(
	[ActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/2/2021 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](250) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/2/2021 4:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Email] [nvarchar](250) NULL,
	[RoleId] [int] NULL,
	[Picture] [nvarchar](max) NULL,
	[Password] [nvarchar](200) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PortfolioPicture] ON 

INSERT [dbo].[PortfolioPicture] ([PicId], [PortfolioId], [Picture]) VALUES (1, N'1001', N'/photos/839148.png')
INSERT [dbo].[PortfolioPicture] ([PicId], [PortfolioId], [Picture]) VALUES (3, N'1001', N'/photos/portfolio/486335.png')
INSERT [dbo].[PortfolioPicture] ([PicId], [PortfolioId], [Picture]) VALUES (5, N'1001', N'/photos/portfolio/925561.png')
INSERT [dbo].[PortfolioPicture] ([PicId], [PortfolioId], [Picture]) VALUES (6, N'1001', N'/photos/portfolio/88504.png')
INSERT [dbo].[PortfolioPicture] ([PicId], [PortfolioId], [Picture]) VALUES (7, N'1001', N'/photos/portfolio/967197.png')
INSERT [dbo].[PortfolioPicture] ([PicId], [PortfolioId], [Picture]) VALUES (8, N'1001', N'/photos/portfolio/485889.png')
INSERT [dbo].[PortfolioPicture] ([PicId], [PortfolioId], [Picture]) VALUES (9, N'1001', N'/photos/portfolio/508875.png')
INSERT [dbo].[PortfolioPicture] ([PicId], [PortfolioId], [Picture]) VALUES (11, N'1001', N'/photos/portfolio/920008.png')
INSERT [dbo].[PortfolioPicture] ([PicId], [PortfolioId], [Picture]) VALUES (12, N'1001', N'/photos/portfolio/915803.png')
SET IDENTITY_INSERT [dbo].[PortfolioPicture] OFF
GO
INSERT [dbo].[Portfolios] ([PortfolioId], [Title], [Description]) VALUES (N'1001', N'Portfolio Title', N'Portfolio Description')
GO
SET IDENTITY_INSERT [dbo].[RoleActions] ON 

INSERT [dbo].[RoleActions] ([ActionId], [RoleId], [Insert], [Update], [Delete], [View]) VALUES (1, 1, 1, 1, 1, 1)
INSERT [dbo].[RoleActions] ([ActionId], [RoleId], [Insert], [Update], [Delete], [View]) VALUES (2, 2, 1, 0, 0, 1)
SET IDENTITY_INSERT [dbo].[RoleActions] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Super Admin')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'User 1')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Name], [Email], [RoleId], [Picture], [Password]) VALUES (5, N'Mr. Super Admin', N'superadmin@gmail.com', 1, N'/photos/626933.png', N'123')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
USE [master]
GO
ALTER DATABASE [PortfolioDb] SET  READ_WRITE 
GO
