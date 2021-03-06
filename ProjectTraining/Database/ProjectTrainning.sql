USE [master]
GO
/****** Object:  Database [ProjectTraining]    Script Date: 4/24/2021 9:02:59 PM ******/
CREATE DATABASE [ProjectTraining]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjectTraining', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProjectTraining.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjectTraining_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProjectTraining_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ProjectTraining] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectTraining].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectTraining] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectTraining] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectTraining] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectTraining] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectTraining] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectTraining] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjectTraining] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectTraining] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectTraining] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectTraining] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectTraining] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectTraining] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectTraining] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectTraining] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectTraining] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjectTraining] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectTraining] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectTraining] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectTraining] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectTraining] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectTraining] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectTraining] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectTraining] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProjectTraining] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectTraining] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectTraining] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectTraining] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectTraining] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjectTraining] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjectTraining] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProjectTraining] SET QUERY_STORE = OFF
GO
USE [ProjectTraining]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 4/24/2021 9:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[ProductID] [int] NULL,
	[UserID] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[Rate] [int] NULL,
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 4/24/2021 9:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[DiscountID] [int] IDENTITY(1,1) NOT NULL,
	[DiscountPercent] [float] NULL,
	[DiscountStart] [datetime] NULL,
	[DiscountEnd] [datetime] NULL,
	[DiscountDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[DiscountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kind]    Script Date: 4/24/2021 9:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kind](
	[KindID] [int] IDENTITY(1,1) NOT NULL,
	[KindName] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_Kind] PRIMARY KEY CLUSTERED 
(
	[KindID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 4/24/2021 9:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[Total] [float] NULL,
	[Status] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UserID] [int] NULL,
	[Type] [int] NULL,
	[PaymentResult] [int] NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 4/24/2021 9:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailsID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantities] [int] NULL,
	[TotalPrice] [float] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/24/2021 9:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](200) NULL,
	[ProductPrice] [float] NULL,
	[ProductImage] [nvarchar](max) NULL,
	[DiscountID] [int] NULL,
	[KindID] [int] NULL,
	[ProductDescription] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[Quantities] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/24/2021 9:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](200) NULL,
	[Phone] [nvarchar](10) NULL,
	[Address] [nvarchar](max) NULL,
	[Role] [nvarchar](20) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 4/24/2021 9:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouse](
	[ProductID] [int] NOT NULL,
	[Quantities] [int] NULL,
	[ImportPrice] [float] NULL,
	[ExportPrice] [float] NULL,
	[DateImport] [datetime] NULL,
	[Solded] [int] NULL,
	[WarehouseId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED 
(
	[WarehouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([ProductID], [UserID], [Comment], [Rate], [CommentID], [CreateDate]) VALUES (3, 1, N'Được', 4, 4, CAST(N'2020-12-25T07:12:00.000' AS DateTime))
INSERT [dbo].[Comments] ([ProductID], [UserID], [Comment], [Rate], [CommentID], [CreateDate]) VALUES (16, 1, N'Tốt', 4, 6, CAST(N'2021-01-08T10:01:36.393' AS DateTime))
INSERT [dbo].[Comments] ([ProductID], [UserID], [Comment], [Rate], [CommentID], [CreateDate]) VALUES (16, 1, N'Tạm', 3, 13, CAST(N'2021-01-12T15:17:54.320' AS DateTime))
INSERT [dbo].[Comments] ([ProductID], [UserID], [Comment], [Rate], [CommentID], [CreateDate]) VALUES (11, 1, N'Tạm', 3, 18, CAST(N'2021-01-21T09:09:28.103' AS DateTime))
INSERT [dbo].[Comments] ([ProductID], [UserID], [Comment], [Rate], [CommentID], [CreateDate]) VALUES (16, 1, N'ok', 3, 19, CAST(N'2021-01-21T13:52:15.707' AS DateTime))
INSERT [dbo].[Comments] ([ProductID], [UserID], [Comment], [Rate], [CommentID], [CreateDate]) VALUES (16, 1, N'<script>alert("hoa dep trai")</script>', 5, 20, CAST(N'2021-01-21T13:53:14.497' AS DateTime))
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[Discount] ON 

INSERT [dbo].[Discount] ([DiscountID], [DiscountPercent], [DiscountStart], [DiscountEnd], [DiscountDescription]) VALUES (1, 5, CAST(N'2020-12-13T03:00:00.000' AS DateTime), CAST(N'2020-12-14T00:00:00.000' AS DateTime), N'abc')
INSERT [dbo].[Discount] ([DiscountID], [DiscountPercent], [DiscountStart], [DiscountEnd], [DiscountDescription]) VALUES (2, 7, CAST(N'2020-12-15T00:00:00.000' AS DateTime), CAST(N'2021-01-22T00:00:00.000' AS DateTime), N'Thich')
INSERT [dbo].[Discount] ([DiscountID], [DiscountPercent], [DiscountStart], [DiscountEnd], [DiscountDescription]) VALUES (3, 12, CAST(N'2021-01-07T00:00:00.000' AS DateTime), CAST(N'2021-01-12T12:00:00.000' AS DateTime), N'oke')
INSERT [dbo].[Discount] ([DiscountID], [DiscountPercent], [DiscountStart], [DiscountEnd], [DiscountDescription]) VALUES (5, 20, CAST(N'2020-12-24T15:48:00.000' AS DateTime), CAST(N'2020-12-28T15:53:00.000' AS DateTime), N'OKE')
SET IDENTITY_INSERT [dbo].[Discount] OFF
GO
SET IDENTITY_INSERT [dbo].[Kind] ON 

INSERT [dbo].[Kind] ([KindID], [KindName], [CreateDate]) VALUES (1, N'SamSung', CAST(N'2020-12-29T00:00:00.000' AS DateTime))
INSERT [dbo].[Kind] ([KindID], [KindName], [CreateDate]) VALUES (2, N'IPhone', CAST(N'2020-12-13T00:00:00.000' AS DateTime))
INSERT [dbo].[Kind] ([KindID], [KindName], [CreateDate]) VALUES (3, N'Xiaomi', CAST(N'2020-12-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Kind] ([KindID], [KindName], [CreateDate]) VALUES (4, N'Nokia', CAST(N'2020-12-23T09:43:28.000' AS DateTime))
INSERT [dbo].[Kind] ([KindID], [KindName], [CreateDate]) VALUES (5, N'VinSmart', CAST(N'2020-12-23T09:50:00.083' AS DateTime))
INSERT [dbo].[Kind] ([KindID], [KindName], [CreateDate]) VALUES (6, N'Sony', CAST(N'2020-12-23T09:58:28.567' AS DateTime))
SET IDENTITY_INSERT [dbo].[Kind] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (1, 21850000, 1, CAST(N'2020-12-20T00:00:00.000' AS DateTime), 1, 0, 0, N'abc')
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (2, 11761000, 1, CAST(N'2020-12-21T00:00:00.000' AS DateTime), 1, 1, 1, N'ok')
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (23, 6400000, 1, CAST(N'2021-01-20T13:17:39.210' AS DateTime), 1, 1, 1, NULL)
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (24, 6400000, 2, CAST(N'2021-01-20T13:19:19.637' AS DateTime), 1, 1, 1, NULL)
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (29, 12690000, 0, CAST(N'2021-01-20T13:36:59.313' AS DateTime), 1, 0, 0, NULL)
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (1003, 47430000, 0, CAST(N'2021-01-21T08:30:11.337' AS DateTime), 4, 1, 1, N'ok')
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (1005, 65430000, 0, CAST(N'2021-01-21T13:58:41.210' AS DateTime), 1, 0, 0, NULL)
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (1006, 65430000, 0, CAST(N'2021-01-21T13:59:33.263' AS DateTime), 1, 1, 0, NULL)
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (1007, 9600000, 2, CAST(N'2021-01-21T14:00:52.747' AS DateTime), 1, 1, 1, NULL)
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (1008, 4700000, 0, CAST(N'2021-01-21T14:12:14.877' AS DateTime), 1, 1, 0, NULL)
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (1013, 21390000, 0, CAST(N'2021-01-21T14:39:53.410' AS DateTime), 1, 1, 0, N'ok')
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (1014, 4700000, 0, CAST(N'2021-01-21T14:40:20.453' AS DateTime), 1, 0, 0, NULL)
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (2009, 3200000, 0, CAST(N'2021-04-10T21:03:54.737' AS DateTime), 1, 0, 0, NULL)
INSERT [dbo].[Order] ([OrderID], [Total], [Status], [CreateDate], [UserID], [Type], [PaymentResult], [Note]) VALUES (2010, 3200000, 0, CAST(N'2021-04-10T21:04:30.443' AS DateTime), 1, 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1, 1, 2, 1, 21850000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (2, 2, 3, 1, 7011000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (3, 2, 4, 1, 4750000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (30, 23, 10, 2, 6400000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (31, 24, 10, 2, 6400000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (36, 29, 10, 1, 3200000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (37, 29, 9, 1, 9490000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1004, 1003, 6, 2, 47430000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1006, 1005, 16, 6, 18000000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1007, 1005, 6, 2, 47430000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1008, 1006, 16, 6, 18000000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1009, 1006, 6, 2, 47430000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1010, 1007, 10, 3, 9600000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1011, 1008, 11, 1, 4700000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1016, 1013, 2, 1, 21390000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (1017, 1014, 11, 1, 4700000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (2012, 2009, 10, 1, 3200000)
INSERT [dbo].[OrderDetails] ([OrderDetailsID], [OrderID], [ProductID], [Quantities], [TotalPrice]) VALUES (2013, 2010, 10, 1, 3200000)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (2, N'Iphone 12', 23000000, N'ip_3b07.png', 2, 2, N'<p>Iphone 12 xanh</p>
', CAST(N'2020-12-20T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (3, N'Samsung Galaxy A71 Đen', 7380000, N'Samsung_galaxy_A71_1112.png', 1, 1, N'<p>Samsung galaxy A71 Đen</p>
', CAST(N'2020-12-21T00:00:00.000' AS DateTime), 12)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (4, N'Iphone 6s +', 5000000, N'iphone6_32gb_911b.jpg', 2, 2, N'<p>Iphone 6&nbsp;</p>
', CAST(N'2020-12-21T00:00:00.000' AS DateTime), 14)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (5, N'Xiaomi Redmi 9', 2890000, N'Xiaomi Redmi 9_eee1.jpg', 1, 3, N'<p>Đen</p>
', CAST(N'2020-12-21T00:00:00.000' AS DateTime), 12)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (6, N'Iphone 11 Pro Đen', 25500000, N'Iphone11_924f.jpg', 2, 2, N'<p>Iphone 11 Pro max đen</p>
', CAST(N'2020-12-21T00:00:00.000' AS DateTime), 7)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (8, N'Samsung Galaxy S20 plus', 13500000, N'samsungS20_ac0a.png', NULL, 1, N'<p>&aacute;dasdasd</p>
', CAST(N'2020-12-21T00:00:00.000' AS DateTime), 12)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (9, N'Nokia 8.3 5G', 9490000, N'nokia_8.3_5914.jpg', NULL, 4, N'<h2>Điện thoại Nokia 8.3 &ndash; Sở hữu bốn camera tuyệt đỉnh</h2>

<p>Vừa qua người y&ecirc;u c&ocirc;ng nghệ lại đứng ngồi kh&ocirc;ng y&ecirc;n khi Nokia cứ mập mờ về việc cho ra mắt sản phẩm mới, sản phẩm c&oacute; t&ecirc;n gọi&nbsp;<strong>Nokia 8.3</strong>. V&agrave; dường như đ&acirc;y l&agrave; bản n&acirc;ng cấp của d&ograve;ng Nokia 8 vốn đ&atilde; được ra mắt c&aacute;ch đ&acirc;y chưa l&acirc;u. Đ&acirc;y chắc chắn&nbsp;sẽ l&agrave; chiếc điện thoại b&ugrave;ng nổ chưa từng thấy của Nokia.</p>

<h3>Ho&agrave;n thiện từ khung kim loại v&agrave; mặt k&iacute;nh, m&agrave;n h&igrave;nh PureDisplay 6.8 inch</h3>

<p>Nokia 8.3 5G c&oacute; vẻ ngo&agrave;i kh&ocirc;ng c&oacute; kh&aacute;c biệt nhiều so với thế hệ trước, chắc hẳn Nokia đ&atilde; cho d&ograve;ng n&agrave;y một thiết kế đặc trưng. M&aacute;y c&oacute; k&iacute;ch thước 171.9 x 78.56 x 8.99 mm, trọng lượng 220 gram, dường như vừa tay của người d&ugrave;ng, c&oacute; thể cầm nắm thoải m&aacute;i m&agrave; kh&ocirc;ng sợ mỏi.</p>

<p>Th&acirc;n m&aacute;y được ho&agrave;n thiện từ khung kim loại chắc chắn, đảm bảo bạn c&oacute; thể bỏ v&agrave;o trong t&uacute;i x&aacute;ch, t&uacute;i quần m&agrave; kh&ocirc;ng sợ bị cấn hay m&oacute;p. Ở mặt sau vẫn l&agrave; mặt lưng k&iacute;nh b&oacute;ng bẩy tự tin cho bạn khoe được vẻ đẹp của n&oacute;.</p>
', CAST(N'2020-12-23T09:47:21.000' AS DateTime), 14)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (10, N'Vsmart Active 3 Ram 6Gb 64Gb', 3200000, N'vsmart_active3_4da8.jpg', 5, 5, N'<h2 dir="ltr">Vsmart Active 3 - smartphone quốc d&acirc;n cho người Việt</h2>

<h2 dir="ltr">Thiết kế Vsmart Active 3 sang trọng</h2>

<p dir="ltr"><strong>Vsmart Active 3</strong>&nbsp;c&oacute; thiết kế kh&ocirc;ng k&eacute;m c&aacute;c mẫu smartphone của c&aacute;c h&atilde;ng nổi tiếng với những chi tiết được ho&agrave;n thiện sang trọng. Ngo&agrave;i ra, m&aacute;y cho cảm gi&aacute;c cầm đầm tay nhờ khối lượng nhẹ c&ugrave;ng mặt lưng được bo cong về hai cạnh b&ecirc;n.</p>

<p dir="ltr">&nbsp;</p>
', CAST(N'2020-12-23T09:50:43.043' AS DateTime), 2)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (11, N'Sony Xperia XZ2 4GB|64GB ', 4700000, N'sonyXperiaZ2_8351.jpg', 1, 6, N'<p>Điện thoại&nbsp;<strong><a href="https://didongmoi.com.vn/sony-xperia-xz2-moi">Sony XZ2</a></strong>&nbsp;đ&aacute;nh dấu một bước tiến vượt bậc của Sony, gi&uacute;p h&atilde;ng tạo được điểm nhấn tr&ecirc;n thị trường smartphone hiện nay. Sau nhiều năm, cuối c&ugrave;ng, Sony đ&atilde; thỏa hiệp với việc thay đổi thiết kế, đồng thời mở rộng k&iacute;ch thước hiển thị của m&agrave;n h&igrave;nh, khả năng nhiếp ảnh cũng trở n&ecirc;n tuyệt vời hơn v&agrave; sử dụng bộ chip h&agrave;ng đầu từ Qualcomm.&nbsp;</p>

<h2>Sony thay đổi thiết kế tr&ecirc;n Xperia XZ2</h2>

<p>Cuối c&ugrave;ng&nbsp;<strong><a href="https://didongmoi.com.vn/sony">Sony</a></strong>&nbsp;đ&atilde; thỏa hiệp trong việc l&agrave;m mới thiết kế cho d&ograve;ng Xperia. Một v&agrave;i tinh chỉnh nhỏ gi&uacute;p model XZ2 trở n&ecirc;n thời thượng v&agrave; sang trong hơn.&nbsp;</p>

<p>Sony đ&atilde; thay đổi thiết kế mặt sau, ống k&iacute;nh camera được đặt ph&iacute;a tr&ecirc;n, ch&iacute;nh giữa v&agrave; mang đến cảm gi&aacute;c c&acirc;n đối hơn cho mặt lưng. B&ecirc;n cạnh đ&oacute;, việc sử dụng chất liệu k&iacute;nh cường lực cao cấp v&agrave; bo cong nhẹ ở mặt sau gi&uacute;p người d&ugrave;ng cảm thấy thoải m&aacute;i v&agrave; chắc chắn hơn khi cầm nắm. Với c&aacute;c đối tượng c&oacute; th&oacute;i quen cầm ngang th&acirc;n m&aacute;y để chơi game hoặc xem video, XZ2 sẽ mang đến cảm gi&aacute;c dễ d&agrave;ng hơn c&aacute;c model Sony kh&aacute;c.</p>
', CAST(N'2020-12-23T09:59:57.000' AS DateTime), 10)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductPrice], [ProductImage], [DiscountID], [KindID], [ProductDescription], [CreateDate], [Quantities]) VALUES (16, N'Sony Xperia XA1 Pro', 3000000, N'sonyxa1_3549.jpg', NULL, 6, N'<p>&aacute;dasdasd</p>
', CAST(N'2020-12-25T08:22:35.000' AS DateTime), 13)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password], [Phone], [Address], [Role]) VALUES (1, N'Phan Minh', N'Chiến', N'mchien1707@gmail.com', N'$2a$11$azQIzw6tvivqPXleMX0XjOzVdZuC9LH6I5PweEd6Zh18cq9cpidti', N'0965860050', N'725-TH-XL-Đồng Nai', N'Admin')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password], [Phone], [Address], [Role]) VALUES (3, N'a', N'b', N'a@gmail.com', N'$2a$11$LCNOzCy3qR28V9OOO7eW8Ogd1odXCvjBJ/RiJgsgpiNQIB38lkSf2', N'0965860051', N'725-TH-XL-Đồng Nai', N'Khách hàng')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password], [Phone], [Address], [Role]) VALUES (4, N'John', N'The Ripper', N'chien@gmail.com', N'$2a$11$ypks.DKjkhkMywzcIeuVWOKjBNAbwihGpGvtn1fGiMQBLi30G10wy', N'0965860052', N'725-TH-XL-Đồng Nai', N'Khách hàng')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Warehouse] ON 

INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (16, 13, 2850000, 3000000, CAST(N'2020-12-27T16:34:05.803' AS DateTime), 12, 3)
INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (2, 2, 21000000, 23000000, CAST(N'2020-12-20T00:00:00.000' AS DateTime), 8, 4)
INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (3, 11, 7000000, 7380000, CAST(N'2020-12-21T00:00:00.000' AS DateTime), 1, 5)
INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (4, 13, 4500000, 5000000, CAST(N'2020-12-21T00:00:00.000' AS DateTime), 1, 6)
INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (5, 12, 2500000, 2890000, CAST(N'2020-12-21T00:00:00.000' AS DateTime), 0, 7)
INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (6, 7, 24500000, 25500000, CAST(N'2020-12-21T00:00:00.000' AS DateTime), 6, 8)
INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (8, 12, 12500000, 13500000, CAST(N'2020-12-21T00:00:00.000' AS DateTime), 0, 9)
INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (9, 14, 9300000, 9490000, CAST(N'2020-12-23T09:47:21.000' AS DateTime), 1, 10)
INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (10, 2, 3000000, 3200000, CAST(N'2020-12-27T09:50:43.043' AS DateTime), 10, 11)
INSERT [dbo].[Warehouse] ([ProductID], [Quantities], [ImportPrice], [ExportPrice], [DateImport], [Solded], [WarehouseId]) VALUES (11, 10, 3900000, 4700000, CAST(N'2020-12-23T09:59:57.000' AS DateTime), 2, 12)
SET IDENTITY_INSERT [dbo].[Warehouse] OFF
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Product]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Order]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Discount] FOREIGN KEY([DiscountID])
REFERENCES [dbo].[Discount] ([DiscountID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Discount]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Kind] FOREIGN KEY([KindID])
REFERENCES [dbo].[Kind] ([KindID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Kind]
GO
ALTER TABLE [dbo].[Warehouse]  WITH CHECK ADD  CONSTRAINT [FK_Warehouse_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Warehouse] CHECK CONSTRAINT [FK_Warehouse_Product]
GO
USE [master]
GO
ALTER DATABASE [ProjectTraining] SET  READ_WRITE 
GO
