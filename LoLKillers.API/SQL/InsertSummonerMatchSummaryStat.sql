USE [LoLKillers]
GO

/****** Object:  StoredProcedure [dbo].[InsertSummonerMatchSummaryStat]    Script Date: 2022/12/28 17:26:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertSummonerMatchSummaryStat] 
	-- Add the parameters for the stored procedure here
	@AccountId VARCHAR(100)
	, @Region VARCHAR(10)
	, @Queue VARCHAR(10)
	, @IsWin BIT
	, @RiotMatchId BIGINT
	, @RiotChampId INT
	, @RiotChampName VARCHAR(50)
	, @MatchKills INT
	, @MatchDeaths INT
	, @MatchAssists INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT, XACT_ABORT ON;

	BEGIN TRAN

    -- Insert statements for procedure here
	IF NOT EXISTS(SELECT * FROM SummonerMatchSummaryStat WITH (UPDLOCK, HOLDLOCK) WHERE AccountID = @AccountId AND Region = @Region AND RiotMatchID = @RiotMatchId)
	BEGIN
		INSERT INTO
			SummonerMatchSummaryStat(AccountID, Region, Queue, IsWin, RiotMatchID, RiotChampID, RiotChampName, MatchKills, MatchDeaths, MatchAssists)
		VALUES
			(@AccountId, @Region, @Queue, @IsWin, @RiotMatchId, @RiotChampId, @RiotChampName, @MatchKills, @MatchDeaths, @MatchAssists)
	END

	COMMIT
END
GO


