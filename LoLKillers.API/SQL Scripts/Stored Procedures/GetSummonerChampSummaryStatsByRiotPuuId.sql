
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

	SELECT	Summoner.RiotPuuId
			, Summoner.Region
			, Summoner.QueueType
			, COUNT(*) AS TotalMatches
			, Summoner.RiotChampId
			, Summoner.RiotChampName
			, SUM(CAST(Summoner.IsWin AS INT)) AS Wins
			, SUM(Summoner.MatchKills) AS TotalKills
			, SUM(Summoner.MatchDeaths) AS TotalDeaths
			, SUM(Summoner.MatchAssists) AS TotalAssists
			, SUM(Summoner.MinionsKilled) AS TotalMinionsKilled
			, SUM(CAST(Summoner.FirstBlood AS INT)) AS TotalFirstBloods
			, SUM(CAST(Summoner.FirstBloodAssist AS INT)) AS TotalFirstBloodAssists
			, SUM(Summoner.PhysicalDamageDealtToChampions) AS TotalPhysicalDamageDealtToChampions
			, SUM(Summoner.MagicDamageDealtToChampions) AS TotalMagicDamageDealtToChampions
			, SUM(Summoner.TotalDamageDealtToChampions) AS TotalDamageDealtToChampions
			, SUM(Summoner.Spell1Casts) AS TotalSpell1Casts
			, SUM(Summoner.Spell2Casts) AS TotalSpell2Casts
			, SUM(Summoner.Spell3Casts) AS TotalSpell3Casts
			, SUM(Summoner.Spell4Casts) AS TotalSpell4Casts
			, SUM(Summoner.SummonerSpell1Casts) AS TotalSummonerSpell1Casts
			, SUM(Summoner.SummonerSpell2Casts) AS TotalSummonerSpell2Casts
			, SUM(Summoner.GoldEarned) AS TotalGoldEarned
			, SUM(Summoner.GoldSpent) AS TotalGoldSpent
			, SUM(Summoner.WardsPlaced) AS TotalWardsPlaced
			, SUM(Summoner.VisionScore) AS TotalVisionScore
			, SUM(Summoner.LongestTimeSpentLiving) AS TotalLongestTimeSpentLiving
			, SUM(Summoner.TimeSpentAlive) AS TotalTimeSpentAlive
			, SUM(Summoner.TimeSpentDead) AS TotalTimeSpentDead
			, SUM(Summoner.MatchDuration) AS TotalMatchesDuration
			--// Averages per match
			, ROUND(AVG(CAST(Summoner.MatchKills AS decimal(10,2))), 1) AS AverageKills
			, ROUND(AVG(CAST(Summoner.MatchDeaths AS decimal(10,2))), 1) AS AverageDeaths
			, ROUND(AVG(CAST(Summoner.MatchAssists AS decimal(10,2))), 1) AS AverageAssists
			, ROUND(CAST((SUM(Summoner.MatchKills) + SUM(Summoner.MatchAssists)) AS decimal(10,2)) / CAST((CASE WHEN SUM(Summoner.MatchDeaths) < 1 THEN 1 ELSE SUM(Summoner.MatchDeaths) END) AS decimal(10,2)), 2) AS KDA
			, ROUND(CAST(SUM(Summoner.MatchKills) AS decimal(10,2)) / CAST((CASE WHEN SUM(Summoner.MatchDeaths) < 1 THEN 1 ELSE SUM(Summoner.MatchDeaths) END) AS decimal(10,2)), 2) AS KD
			, ROUND(CAST(SUM(Summoner.MatchAssists) AS decimal(10,2)) / CAST((CASE WHEN SUM(Summoner.MatchDeaths) < 1 THEN 1 ELSE SUM(Summoner.MatchDeaths) END) AS decimal(10,2)), 2) AS AD
			, ROUND(AVG(CAST(Summoner.MinionsKilled AS decimal(10,2))), 1) AS AverageMinionsKilled
			, ROUND(AVG(CAST(Summoner.FirstBlood AS decimal(10,2))), 1) AS AverageFirstBloods
			, ROUND(AVG(CAST(Summoner.FirstBloodAssist AS decimal(10,2))), 1) AS AverageFirstBloodAssists
			, ROUND(AVG(CAST(Summoner.FirstBlood AS decimal(10,2)) + CAST(Summoner.FirstBloodAssist AS decimal(10,2))), 1) AS AverageFirstBloodParticipation
			, ROUND(AVG(CAST(Summoner.Spell1Casts AS decimal(10,2))), 1) AS AverageSpell1Casts
			, ROUND(AVG(CAST(Summoner.Spell2Casts AS decimal(10,2))), 1) AS AverageSpell2Casts
			, ROUND(AVG(CAST(Summoner.Spell3Casts AS decimal(10,2))), 1) AS AverageSpell3Casts
			, ROUND(AVG(CAST(Summoner.Spell4Casts AS decimal(10,2))), 1) AS AverageSpell4Casts
			, ROUND(AVG(CAST(Summoner.SummonerSpell1Casts AS decimal(10,2))), 1) AS AverageSummonerSpell1Casts
			, ROUND(AVG(CAST(Summoner.SummonerSpell2Casts AS decimal(10,2))), 1) AS AverageSummonerSpell2Casts
			, ROUND(AVG(CAST(Summoner.GoldEarned AS decimal(10,2))), 1) AS AverageGoldEarned
			, ROUND(AVG(CAST(Summoner.GoldSpent AS decimal(10,2))), 1) AS AverageGoldSpent
			, ROUND(AVG(CAST(Summoner.WardsPlaced AS decimal(10,2))), 1) AS AverageWardsPlaced
			, ROUND(AVG(CAST(Summoner.VisionScore AS decimal(10,2))), 1) AS AverageVisionScore
			, ROUND(AVG(CAST(Summoner.LongestTimeSpentLiving AS decimal(10,2))), 1) AS AverageLongestTimeSpentLiving
			, ROUND(AVG(CAST(Summoner.TimeSpentDead AS decimal(10,2))), 1) AS AverageTimeSpentDead
			, ROUND(AVG(CAST(Summoner.MatchDuration AS decimal(10,2))), 1) AS AverageMatchDuration
			--// Averages per duration (second)
			, ROUND((CAST(SUM(Summoner.MatchKills) AS decimal(20,10)) / CAST(SUM(Summoner.MatchDuration) AS decimal(20,10))), 10) AS AverageKillsPerSecond
			, ROUND((CAST(SUM(Summoner.MatchDeaths) AS decimal(20,10)) / CAST(SUM(Summoner.MatchDuration) AS decimal(20,10))), 10) AS AverageDeathsPerSecond
			, ROUND((CAST(SUM(Summoner.MatchAssists) AS decimal(20,10)) / CAST(SUM(Summoner.MatchDuration) AS decimal(20,10))), 10) AS AverageAssistsPerSecond


	FROM	SummonerMatchSummaryStats AS Summoner
			INNER JOIN TeamMatchSummaryStats Team ON Team.RiotMatchId = Summoner.RiotMatchId AND Team.Region = Summoner.Region AND Team.QueueType = Summoner.QueueType AND Team.RiotTeamId = Summoner.RiotTeamId

	WHERE	Summoner.Region = @Region
			AND Summoner.RiotPuuId = @RiotPuuId
			AND 1 = 
				CASE 
					WHEN @Queue = 'all' THEN 1 
					WHEN Summoner.QueueType = @Queue THEN 1 
					ELSE 0 
				END 

	GROUP BY Summoner.RiotPuuId, Summoner.Region, Summoner.QueueType, Summoner.RiotChampId, Summoner.RiotChampName

END
