USE [LoLKillers]
GO

/****** Object:  Table [dbo].[SummonerMatchChampStat]    Script Date: 2022/12/28 17:37:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SummonerMatchChampStat](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [varchar](100) NOT NULL,
	[Region] [varchar](10) NOT NULL,
	[Queue] [varchar](10) NOT NULL,
	[RiotMatchID] [bigint] NOT NULL,
	[IsWin] [bit] NOT NULL,
	[RiotChampID] [int] NOT NULL,
	[RiotChampName] [varchar](50) NOT NULL,
	[RiotEnemyChampID] [int] NOT NULL,
	[RiotEnemyChampName] [varchar](50) NOT NULL,
	[KillsAgainstEnemyChamp] [int] NOT NULL,
	[DeathsToEnemyChamp] [int] NOT NULL,
	[AssistsAgainstEnemyChamp] [int] NOT NULL
) ON [PRIMARY]
GO


