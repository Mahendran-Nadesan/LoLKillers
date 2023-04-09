

--delete
SELECT	*
FROM	[LoLKillersDev].[dbo].[SummonerMatchSummaryStats]

select	RiotChampName
		, RiotTeamId
		, SUM(CAST(IsWin AS INT)) 
		, COUNT(*)
		, CAST(SUM(CAST(IsWin AS INT)) AS decimal(6,2)) / CAST(COUNT(*) as decimal(6,2))
from	SummonerMatchSummaryStats
where	RiotPuuId = 'hmXHjKKRHPgaazsZMySVeh8RcJOxR3riiu7dxaGkUHj9ikvAtwVsbt7JYC9T9TNg1KJODf3jQwN2MA'
--where	RiotPuuId = 'PjIlCXDUQjZrFyxwV6k12zPWK7-GfgwK3JJxe2wmvVKV8YDGUBwCbOcZMnlSsxFVkhX3G9ZRI8IM1Q'
group by RiotChampName, RiotTeamId
order by RiotChampName, RiotTeamId