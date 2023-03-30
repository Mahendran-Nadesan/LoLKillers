
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mahen
-- Create date: 2023-03-30
-- Description:	Gets all summary data per champion per queue by a summoner's Riot PuuId
--				Maps to object Models.SummonerChampSummaryStat
-- =============================================
CREATE PROCEDURE GetSummonerChampSummaryStatsByRiotPuuId
	@Region varchar(10)
	, @RiotPuuId varchar(100)
	, @Queue varchar(30)
AS

BEGIN

	SET NOCOUNT ON;

	SELECT	RiotPuuId
			, Region
			, QueueType
			, COUNT(*) AS NumberOfMatches
			, RiotChampId
			, RiotChampName
			, SUM(CAST(IsWin AS INT)) AS Wins
			, SUM(MatchKills) AS TotalKills
			, SUM(MatchDeaths) AS TotalDeaths
			, SUM(MatchAssists) AS TotalAssists

	FROM	SummonerMatchSummaryStats

	WHERE	Region = @Region
			AND RiotPuuId = @RiotPuuId
			AND 1 = 
				CASE 
					WHEN @Queue = 'all' THEN 1 
					WHEN QueueType = @Queue THEN 1 
					ELSE 0 
				END 

	GROUP BY RiotPuuId, Region, QueueType, RiotChampId, RiotChampName

END
