USE [LoLKillersDev]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[GetSummonerChampSummaryStatsByRiotPuuId]
		@Region = N'euw',
		@RiotPuuId = N'hmXHjKKRHPgaazsZMySVeh8RcJOxR3riiu7dxaGkUHj9ikvAtwVsbt7JYC9T9TNg1KJODf3jQwN2MA',
		@Queue = N'all',
		@MapSide = null

SELECT	'Return Value' = @return_value

GO
