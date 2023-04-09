-- =============================================
-- Author:		Mahen
-- Create date: 2023-04-08
-- Description:	Gets all match data per participant per queue by a summoner's Riot PuuId
--				Maps to object Models.SummonerMatchSummaryChampStat
-- =============================================


CREATE PROCEDURE [dbo].[GetSummonerMatchSummaryStatsByRiotPuuId]
	@Region varchar(10)
	, @RiotPuuId varchar(100)
	, @Queue varchar(30)
AS

BEGIN
	
	SET NOCOUNT ON;

	SELECT	Id
			, RiotPuuId
			, Region
			, RiotMatchId
			, QueueType
			, RiotTeamId
			, RiotChampId
			, RiotChampName
			, MatchKills
			, MatchDeaths
			, MatchAssists
			, MinionsKilled
			, FirstBlood
			, FirstBloodAssist
			, PhysicalDamageDealtToChampions
			, MagicDamageDealtToChampions
			, TotalDamageDealtToChampions
			, Spell1Casts
			, Spell2Casts
			, Spell3Casts
			, Spell4Casts
			, SummonerSpell1Id
			, SummonerSpell2Id
			, SummonerSpell1Casts
			, SummonerSpell2Casts
			, GoldEarned
			, GoldSpent
			, ROUND((CAST(GoldSpent AS decimal(10,2)) * 100)/ CAST(GoldEarned AS decimal(10,2)), 2) AS PercentageGoldSpent
			, WardsPlaced
			, VisionScore
			, LongestTimeSpentLiving
			, TimeSpentAlive
			, TimeSpentDead
			, MatchDuration
			, ROUND((CAST(TimeSpentAlive AS decimal(10,2)) * 100)/ CAST(MatchDuration AS decimal(10,2)), 2) AS PercentageTimeSpentAlive
			, IsWin

	FROM	SummonerMatchSummaryStats

	WHERE	RiotMatchId IN 
			(
				SELECT	RiotMatchId 
				FROM	SummonerMatchSummaryStats
				WHERE	Region = @Region
						AND RiotPuuId = @RiotPuuId
						AND 1 = 
							CASE 
								WHEN @Queue = 'all' THEN 1 
								WHEN QueueType = @Queue THEN 1 
								ELSE 0 
							END
			)


END
	
