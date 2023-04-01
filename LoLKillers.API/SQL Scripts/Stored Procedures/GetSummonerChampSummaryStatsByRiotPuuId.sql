
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
			, COUNT(*) AS TotalMatches
			, RiotChampId
			, RiotChampName
			, SUM(CAST(IsWin AS INT)) AS Wins
			, SUM(MatchKills) AS TotalKills
			, SUM(MatchDeaths) AS TotalDeaths
			, SUM(MatchAssists) AS TotalAssists
			, SUM(MinionsKilled) AS TotalMinionsKilled
			, SUM(CAST(FirstBlood AS INT)) AS TotalFirstBloods
			, SUM(CAST(FirstBloodAssist AS INT)) AS TotalFirstBloodAssists
			, SUM(PhysicalDamageDealtToChampions) AS TotalPhysicalDamageDealtToChampions
			, SUM(MagicDamageDealtToChampions) AS TotalMagicDamageDealtToChampions
			, SUM(TotalDamageDealtToChampions) AS TotalDamageDealtToChampions
			, SUM(Spell1Casts) AS TotalSpell1Casts
			, SUM(Spell2Casts) AS TotalSpell2Casts
			, SUM(Spell3Casts) AS TotalSpell3Casts
			, SUM(Spell4Casts) AS TotalSpell4Casts
			, SUM(SummonerSpell1Casts) AS TotalSummonerSpell1Casts
			, SUM(SummonerSpell2Casts) AS TotalSummonerSpell2Casts
			, SUM(GoldEarned) AS TotalGoldEarned
			, SUM(GoldSpent) AS TotalGoldSpent
			, SUM(WardsPlaced) AS TotalWardsPlaced
			, SUM(VisionScore) AS TotalVisionScore
			, SUM(LongestTimeSpentLiving) AS TotalLongestTimeSpentLiving
			, SUM(TimeSpentDead) AS TotalTimeSpentDead
			, SUM(MatchDuration) AS TotalMatchesDuration
			, ROUND(AVG(CAST(MatchKills AS decimal(5,2))), 1) AS AverageKills
			, ROUND(AVG(CAST(MatchDeaths AS decimal(5,2))), 1) AS AverageDeaths
			, ROUND(AVG(CAST(MatchAssists AS decimal(5,2))), 1) AS AverageAssists
			, ROUND(CAST((SUM(MatchKills) + SUM(MatchAssists)) AS decimal(10,2)) / CAST((CASE WHEN SUM(MatchDeaths) < 1 THEN 1 ELSE SUM(MatchDeaths) END) AS decimal(10,2)), 2) AS KDA
			, ROUND(CAST(SUM(MatchKills) AS decimal(10,2)) / CAST((CASE WHEN SUM(MatchDeaths) < 1 THEN 1 ELSE SUM(MatchDeaths) END) AS decimal(10,2)), 2) AS KD
			, ROUND(CAST(SUM(MatchAssists) AS decimal(10,2)) / CAST((CASE WHEN SUM(MatchDeaths) < 1 THEN 1 ELSE SUM(MatchDeaths) END) AS decimal(10,2)), 2) AS AD
			, ROUND(AVG(CAST(MinionsKilled AS decimal(5,2))), 1) AS AverageMinionsKilled
			, ROUND(AVG(CAST(FirstBlood AS decimal(5,2))), 1) AS AverageFirstBloods
			, ROUND(AVG(CAST(FirstBloodAssist AS decimal(5,2))), 1) AS AverageFirstBloodAssists
			, ROUND(AVG(CAST(FirstBlood AS decimal(5,2)) + CAST(FirstBloodAssist AS decimal(5,2))), 1) AS AverageFirstBloodParticipation
			, ROUND(AVG(CAST(Spell1Casts AS decimal(6,2))), 1) AS AverageSpell1Casts
			, ROUND(AVG(CAST(Spell2Casts AS decimal(6,2))), 1) AS AverageSpell2Casts
			, ROUND(AVG(CAST(Spell3Casts AS decimal(6,2))), 1) AS AverageSpell3Casts
			, ROUND(AVG(CAST(Spell4Casts AS decimal(6,2))), 1) AS AverageSpell4Casts
			, ROUND(AVG(CAST(SummonerSpell1Casts AS decimal(6,2))), 1) AS AverageSummonerSpell1Casts
			, ROUND(AVG(CAST(SummonerSpell2Casts AS decimal(6,2))), 1) AS AverageSummonerSpell2Casts
			, ROUND(AVG(CAST(GoldEarned AS decimal(10,2))), 1) AS AverageGoldEarned
			, ROUND(AVG(CAST(GoldSpent AS decimal(10,2))), 1) AS AverageGoldSpent
			, ROUND(AVG(CAST(WardsPlaced AS decimal(5,2))), 1) AS AverageWardsPlaced
			, ROUND(AVG(CAST(VisionScore AS decimal(5,2))), 1) AS AverageVisionScore
			, ROUND(AVG(CAST(LongestTimeSpentLiving AS decimal(5,2))), 1) AS AverageLongestTimeSpentLiving
			, ROUND(AVG(CAST(TimeSpentDead AS decimal(5,2))), 1) AS AverageTimeSpentDead
			, ROUND(AVG(CAST(MatchDuration AS decimal(5,2))), 1) AS AverageMatchDuration
			, ROUND((CAST(SUM(MatchKills) AS decimal(10,2)) / CAST(SUM(MatchDuration) AS decimal(15,2))) * 60, 2) AS AverageKillsPerSecond

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
