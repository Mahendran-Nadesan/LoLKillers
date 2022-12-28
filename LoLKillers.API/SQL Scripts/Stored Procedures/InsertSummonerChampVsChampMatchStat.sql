USE [LoLKillers]
GO

/****** Object:  StoredProcedure [dbo].[InsertSummonerChampVsChampMatchStat]    Script Date: 2022/12/28 17:26:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertSummonerChampVsChampMatchStat]
	-- Add the parameters for the stored procedure here
	@AccountId VARCHAR(100)
	, @Region VARCHAR(10)
	, @Queue VARCHAR(10)
	, @IsWin BIT
	, @RiotMatchId BIGINT
	, @RiotChampId INT
	, @RiotChampName VARCHAR(50)
	, @RiotEnemyChampId INT
	, @RiotEnemyChampName VARCHAR(50)
	, @KillsAgainstEnemyChamp INT
	, @DeathsToEnemyChamp INT
	, @AssistsAgainstEnemyChamp INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT, XACT_ABORT ON;

	BEGIN TRAN

    -- Insert statements for procedure here
	IF NOT EXISTS(SELECT * FROM SummonerMatchChampStat WITH (UPDLOCK, HOLDLOCK) WHERE AccountID = @AccountId AND Region = @Region AND RiotMatchID = @RiotMatchId AND RiotChampID = @RiotChampId AND RiotEnemyChampID = @RiotEnemyChampId)
	BEGIN
		INSERT INTO
			SummonerMatchChampStat(AccountID, Region, Queue, IsWin, RiotMatchID, RiotChampID, RiotChampName, RiotEnemyChampID, RiotEnemyChampName, KillsAgainstEnemyChamp, DeathsToEnemyChamp, AssistsAgainstEnemyChamp)
		VALUES
			(@AccountId, @Region, @Queue, @IsWin, @RiotMatchId, @RiotChampId, @RiotChampName, @RiotEnemyChampId, @RiotEnemyChampName, @KillsAgainstEnemyChamp, @DeathsToEnemyChamp, @AssistsAgainstEnemyChamp)
	END

	COMMIT
END
GO


