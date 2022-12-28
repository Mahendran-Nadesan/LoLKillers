USE [LoLKillers]
GO

/****** Object:  StoredProcedure [dbo].[GetSummonerChampVsChampSummaryStats]    Script Date: 2022/12/28 17:25:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSummonerChampVsChampSummaryStats]
	-- Add the parameters for the stored procedure here
	@AccountId VARCHAR(100)
	, @Region VARCHAR(10)
	, @Queue VARCHAR(10)
	, @RiotChampId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT AccountID
	, Region
	, CASE 
		WHEN @Queue = 'all' THEN 'All'
		ELSE @Queue
		END AS Queue
	, COUNT(RiotMatchID) AS NumberOfMatches
	, RiotChampID
	, RiotChampName
	, RiotEnemyChampID
	, RiotEnemyChampName
	, SUM(CAST(IsWin AS INT)) AS Wins
	, (COUNT(RiotMatchID) - SUM(CAST(IsWin AS INT))) AS Losses
	, AVG(CAST(KillsAgainstEnemyChamp AS DECIMAL(6,2))) AS AverageKills
	, AVG(CAST(DeathsToEnemyChamp AS DECIMAL(6,2))) AS AverageDeaths
	, AVG(CAST(AssistsAgainstEnemyChamp AS DECIMAL(6,2))) AS AverageAssists
	FROM SummonerMatchChampStat
	WHERE AccountID = @AccountId
	AND Region = @Region
	AND Queue LIKE 
		CASE WHEN @Queue = 'all' THEN '%' ELSE @Queue END
	AND RiotChampID = @RiotChampId
	GROUP BY AccountID, Region, Queue, RiotChampID, RiotChampName, RiotEnemyChampID, RiotEnemyChampName
END
GO


