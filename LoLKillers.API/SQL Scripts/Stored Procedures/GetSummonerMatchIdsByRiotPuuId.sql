USE [LoLKillers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Mahen
-- Create date: 2023-03-25
-- Description:	Gets all stored Riot Match Ids by a summoner's Riot PuuId
-- =============================================
CREATE PROCEDURE [dbo].[GetSummonerMatchIdsByRiotPuuId] 
	@Region varchar(10)
	, @RiotPuuId varchar(100)
	, @Queue varchar(30)
AS

BEGIN

	SET NOCOUNT ON;

    SELECT	RiotMatchID

	FROM	SummonerMatchSummaryStats

	WHERE	Region = @Region
			AND RiotPuuId = @RiotPuuId
			AND QueueType = @Queue

	ORDER BY RiotMatchID DESC

END


