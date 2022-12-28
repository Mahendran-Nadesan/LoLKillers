USE [LoLKillers]
GO

/****** Object:  StoredProcedure [dbo].[GetLastSummonerMatchIdByAccountId]    Script Date: 2022/12/28 17:23:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetLastSummonerMatchIdByAccountId] 
	-- Add the parameters for the stored procedure here
	@AccountId varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 RiotMatchID
	FROM SummonerMatchSummaryStat
	WHERE AccountID = @AccountId
	ORDER BY RiotMatchID DESC
END
GO


