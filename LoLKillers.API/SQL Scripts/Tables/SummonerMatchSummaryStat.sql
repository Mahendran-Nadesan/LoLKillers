USE [LoLKillers]
GO

/****** Object:  Table [dbo].[SummonerMatchSummaryStat]    Script Date: 2022/12/28 17:38:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SummonerMatchSummaryStat](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [varchar](100) NOT NULL,
	[Region] [varchar](10) NOT NULL,
	[RiotMatchID] [bigint] NOT NULL,
	[RiotChampID] [int] NOT NULL,
	[RiotChampName] [varchar](50) NOT NULL,
	[MatchKills] [int] NULL,
	[MatchDeaths] [int] NULL,
	[MatchAssists] [int] NULL,
	[Queue] [varchar](10) NOT NULL,
	[IsWin] [bit] NOT NULL
) ON [PRIMARY]
GO


