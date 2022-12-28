USE [LoLKillers]
GO

/****** Object:  StoredProcedure [dbo].[GetSummonerMatchIdsByAccountId]    Script Date: 2022/12/28 17:26:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSummonerMatchIdsByAccountId]
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
	SELECT RiotMatchId
	FROM SummonerMatchSummaryStat
	WHERE AccountID = @AccountId
	AND Region = @Region
	AND Queue LIKE 
		CASE WHEN @Queue = 'all' THEN '%' ELSE @Queue END
	ORDER BY RiotMatchID DESC
END
GO


