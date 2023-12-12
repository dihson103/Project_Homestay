--drop database [Project_HomeStay]
--create database Project_HomeStay
USE [Project_HomeStay]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[HomestayId] [int] NOT NULL,
	[Content] [ntext] NOT NULL,
	[CommentId] [int] NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discounts](
	[DiscountId] [int] IDENTITY(1,1) NOT NULL,
	[HomstayId] [int] NOT NULL,
	[Discount] [float] NOT NULL,
	[DateStart] [datetime] NOT NULL,
	[DateEnd] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DiscountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Homestays]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Homestays](
	[HomestayId] [int] IDENTITY(1,1) NOT NULL,
	[HomestayName] [nvarchar](255) NOT NULL,
	[Country] [nvarchar](255) NOT NULL,
	[City] [nvarchar](255) NOT NULL,
	[District] [nvarchar](255) NOT NULL,
	[Commune] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Status] [bit] NOT NULL,
	[Type] [nvarchar](255) NOT NULL,
	[Price] [money] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HomestayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[HomstayId] [int] NOT NULL,
	[Link] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[PriceWhenSell] [money] NOT NULL,
	[IsPayment] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[HomestayId] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[Status] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderServices]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderServices](
	[OrderId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Role] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Fullname] [nvarchar](100) NOT NULL,
	[Gender] [bit] NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Votes]    Script Date: 12/3/2023 12:02:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Votes](
	[UserId] [int] NOT NULL,
	[HomestayId] [int] NOT NULL,
	[rating] [int] NOT NULL,
	[review] [ntext] NULL,
 CONSTRAINT [PK_Votes] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[HomestayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Comments] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comments] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Comments]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Homestays] FOREIGN KEY([HomestayId])
REFERENCES [dbo].[Homestays] ([HomestayId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Homestays]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD FOREIGN KEY([HomstayId])
REFERENCES [dbo].[Homestays] ([HomestayId])
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD FOREIGN KEY([HomstayId])
REFERENCES [dbo].[Homestays] ([HomestayId])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([HomestayId])
REFERENCES [dbo].[Homestays] ([HomestayId])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[OrderServices]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderServices]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([ServiceId])
GO
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_Votes_Homestays] FOREIGN KEY([HomestayId])
REFERENCES [dbo].[Homestays] ([HomestayId])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_Votes_Homestays]
GO
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_Votes_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_Votes_Users]
GO

INSERT INTO [dbo].[Users] (Username, Email, Role, Password, Fullname, Gender, Status)
VALUES ('user1', 'user1@example.com', 'admin', '1', 'John Doe', 1, 1),
       ('user2', 'user2@example.com', 'user', '1', 'Jane Smith', 0, 1);

	   INSERT INTO [dbo].[Homestays] (HomestayName, Country, City, District, Commune, Address, Status, Type, Price, Description)
VALUES ('Lovely Stay', 'Country1', 'City1', 'District1', 'Commune1', '123 Street', 1, 'Type1', 100.00, 'A cozy place to stay'),
       ('Adventure Home', 'Country2', 'City2', 'District2', 'Commune2', '456 Avenue', 1, 'Type2', 150.00, 'Experience the adventure');

INSERT INTO [dbo].[Orders] (UserId, HomestayId, OrderDate, Status)
VALUES (1, 1, '2023-12-01', 'Confirmed'),
       (2, 2, '2023-12-02', 'Pending');
INSERT INTO [dbo].[Comments] (UserId, HomestayId, Content, Time)
VALUES ( 1, 1, 'Great place to stay!', '2023-12-01'),
       ( 2, 1, 'Had a wonderful experience!', '2023-12-02');
INSERT INTO [dbo].[Discounts] (HomstayId, Discount, DateStart, DateEnd)
VALUES (1, 10.5, '2023-12-01', '2023-12-31'),
       (2, 15.0, '2023-12-01', '2023-12-31');

	   INSERT INTO [dbo].[Images] (HomstayId, Link)
VALUES (1, 'http://example.com/image1.jpg'),
       (2, 'http://example.com/image2.jpg');

	   INSERT INTO [dbo].[OrderDetails] (OrderId, FromDate, EndDate, PriceWhenSell, IsPayment)
VALUES (1, '2023-12-01', '2023-12-05', 100.00, 1),
       (2, '2023-12-02', '2023-12-06', 150.00, 0);

	   INSERT INTO [dbo].[OrderServices] (OrderId, ServiceId)
VALUES (1, 1),
       (2, 2);

	   INSERT INTO [dbo].[Services] (ServiceName, Description, Price)
VALUES ('Cleaning', 'Daily room cleaning service', 20.00),
       ('Food', 'Home cooked meals', 30.00);

	   INSERT INTO [dbo].[Votes] (UserId, HomestayId, rating, review)
VALUES (1, 1, 5, 'Excellent!'),
       (2, 1, 4, 'Very good, but room for improvement.');
