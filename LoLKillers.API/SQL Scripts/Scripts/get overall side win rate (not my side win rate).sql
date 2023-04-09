/****** Script for SelectTopNRows command from SSMS  ******/
--delete 
select	*
FROM	[LoLKillersDev].[dbo].[TeamMatchSummaryStats]

select	RiotTeamId
		, SUM(CAST(IsWin AS INT))
from	TeamMatchSummaryStats
Group by RiotTeamId