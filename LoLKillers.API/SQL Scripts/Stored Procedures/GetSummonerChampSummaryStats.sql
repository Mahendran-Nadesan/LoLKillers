USE [LoLKillers]
GO

/****** Object:  StoredProcedure [dbo].[GetSummonerChampSummaryStats]    Script Date: 2022/12/28 17:25:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSummonerChampSummaryStats]
	-- Add the parameters for the stored procedure here
	@AccountId VARCHAR(100)
	, @Region VARCHAR(10)
	, @Queue VARCHAR(10)
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
	, SUM(CAST(IsWin AS INT)) AS Wins
	, (COUNT(RiotMatchID) - SUM(CAST(IsWin AS INT))) AS Losses
	, AVG(CAST(MatchKills AS DECIMAL(6,2))) AS AverageKills
	, AVG(CAST(MatchDeaths AS DECIMAL(6,2))) AS AverageDeaths
	, AVG(CAST(MatchAssists AS DECIMAL(6,2))) AS AverageAssists
	FROM SummonerMatchSummaryStat
	WHERE AccountID = @AccountId
	AND Region = @Region
	AND Queue LIKE 
		CASE WHEN @Queue = 'all' THEN '%' ELSE @Queue END
	GROUP BY AccountID, Region, Queue, RiotChampID, RiotChampName
END
GO


