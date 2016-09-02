USE [EdwarShortener]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 9/2/2016 4:33:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[idUser] [uniqueidentifier] NOT NULL,
	[userPass] [nvarchar](max) NOT NULL,
	[userName] [nvarchar](max) NOT NULL,
	[imgScr] [varchar](200) NULL,
	[realName] [varchar](200) NULL,
	[gender] [varchar](200) NULL,
	[dateB] [varchar](200) NULL,
	[mail] [varchar](200) NULL,
	[phone] [varchar](200) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[ShortedUrl](
	[idShortedUrl] [int] IDENTITY(1,1) NOT NULL,
	[shortedUrl] [nvarchar](max) NULL,
	[longUrl] [nvarchar](max) NULL,
	[created] [datetime] NOT NULL CONSTRAINT [DF_ShortedUrl_created]  DEFAULT (getdate()),
	[pageStatus] [int] NULL,
	[lastStatusChange] [datetime] NULL,
	[Fk_idUsers] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ShortedUrl] PRIMARY KEY CLUSTERED 
(
	[idShortedUrl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ShortedUrl]  WITH CHECK ADD  CONSTRAINT [Fk_idUsers] FOREIGN KEY([Fk_idUsers])
REFERENCES [dbo].[Users] ([idUser])
GO

ALTER TABLE [dbo].[ShortedUrl] CHECK CONSTRAINT [Fk_idUsers]
GO

CREATE TABLE [dbo].[Clicks](
	[idClciks] [int] IDENTITY(1,1) NOT NULL,
	[created] [datetime] NOT NULL CONSTRAINT [DF_Clicks_created]  DEFAULT (getdate()),
	[Fk_idShortedUrl] [int] NOT NULL,
 CONSTRAINT [PK_Clicks] PRIMARY KEY CLUSTERED 
(
	[idClciks] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Clicks]  WITH CHECK ADD  CONSTRAINT [FK_idShortedUrl] FOREIGN KEY([Fk_idShortedUrl])
REFERENCES [dbo].[ShortedUrl] ([idShortedUrl])
GO

ALTER TABLE [dbo].[Clicks] CHECK CONSTRAINT [FK_idShortedUrl]
GO

INSET INTO [dbo].Users] (idUSer,userPass,userName) VALUES (66da80d6-e8cd-48a3-aebe-a31bdef3bafc, AOZCwN/1/7BAx6mBWsJ2Lg==, dummyDefault)
GO 
