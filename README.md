USE [reservationn]
GO

/****** Object:  Table [dbo].[EnableUser]    Script Date: 3/21/2024 8:06:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EnableUser](
	[Uid] [nchar](100) NOT NULL,
	[FirstName] [nchar](100) NOT NULL,
	[LastName] [nchar](100) NOT NULL,
	[Phone] [nchar](10) NOT NULL,
	[Email] [nchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[password] [nchar](10) NOT NULL,
 CONSTRAINT [PK_EnableUser] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


GO

/****** Object:  Table [dbo].[Image]    Script Date: 3/21/2024 8:07:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Image](
	[ImageId] [nchar](10) NOT NULL,
	[Url] [nchar](100) NOT NULL,
	[Pid] [nchar](100) NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


GO

/****** Object:  Table [dbo].[Property]    Script Date: 3/21/2024 8:07:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Property](
	[Pid] [nchar](100) NOT NULL,
	[Price] [float] NOT NULL,
	[Facilities] [nchar](500) NOT NULL,
	[CountRoom] [int] NOT NULL,
	[landownerid] [nchar](100) NOT NULL,
	[location] [nchar](500) NOT NULL,
	[status] [int] NULL,
	[reason] [nchar](1000) NULL,
 CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED 
(
	[Pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


GO

/****** Object:  Table [dbo].[ReservationRequest]    Script Date: 3/21/2024 8:07:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReservationRequest](
	[Rid] [nchar](100) NOT NULL,
	[noOfStudents] [int] NOT NULL,
	[noOfRooms] [int] NOT NULL,
	[pid] [nchar](500) NOT NULL,
	[uid] [nchar](500) NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_ReservationRequest] PRIMARY KEY CLUSTERED 
(
	[Rid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


