USE [master]
GO
/****** Object:  Database [DB26]    Script Date: 4/12/2019 10:07:52 PM ******/
CREATE DATABASE [DB26]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB26', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DB26.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB26_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DB26_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DB26] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB26].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB26] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB26] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB26] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB26] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB26] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB26] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB26] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB26] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB26] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB26] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB26] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB26] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB26] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB26] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB26] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB26] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB26] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB26] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB26] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB26] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB26] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB26] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB26] SET RECOVERY FULL 
GO
ALTER DATABASE [DB26] SET  MULTI_USER 
GO
ALTER DATABASE [DB26] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB26] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB26] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB26] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB26] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DB26', N'ON'
GO
ALTER DATABASE [DB26] SET QUERY_STORE = OFF
GO
USE [DB26]
GO
/****** Object:  Table [dbo].[CancelOrders]    Script Date: 4/12/2019 10:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CancelOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[FoodName] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CancelOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodItems]    Script Date: 4/12/2019 10:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodItems](
	[FoodID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[Picture] [image] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FoodItems] PRIMARY KEY CLUSTERED 
(
	[FoodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderFood]    Script Date: 4/12/2019 10:07:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderFood](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[FoodName] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_OrderFood] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 4/12/2019 10:07:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[Order_Date] [datetime] NOT NULL,
	[Bill] [float] NOT NULL,
	[Items] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Delivery_Time] [time](7) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 4/12/2019 10:07:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Cell_No] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Discriminator] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CancelOrders]  WITH CHECK ADD  CONSTRAINT [FK_CancelOrders_OrderFood] FOREIGN KEY([ProductID])
REFERENCES [dbo].[OrderFood] ([ProductID])
GO
ALTER TABLE [dbo].[CancelOrders] CHECK CONSTRAINT [FK_CancelOrders_OrderFood]
GO
ALTER TABLE [dbo].[CancelOrders]  WITH CHECK ADD  CONSTRAINT [FK_CancelOrders_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[CancelOrders] CHECK CONSTRAINT [FK_CancelOrders_Orders]
GO
ALTER TABLE [dbo].[OrderFood]  WITH CHECK ADD  CONSTRAINT [FK_OrderFood_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderFood] CHECK CONSTRAINT [FK_OrderFood_Orders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Person] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Person]
GO
USE [master]
GO
ALTER DATABASE [DB26] SET  READ_WRITE 
GO
