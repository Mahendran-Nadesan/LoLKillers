USE [LoLKillers]
GO

/****** Object:  Table [dbo].[Config]    Script Date: 2022/12/28 17:34:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Config](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ConfigKey] [varchar](50) NOT NULL,
	[ConfigValue] [varchar](50) NOT NULL
) ON [PRIMARY]
GO


