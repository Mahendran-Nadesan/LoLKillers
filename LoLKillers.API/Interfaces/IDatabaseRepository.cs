using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Models;
using RiotSharp.Misc;

namespace LoLKillers.API.Interfaces
{
    public interface IDatabaseRepository
    {
        //// gets
        //IEnumerable<long> GetSummonerMatchIdsByAccountId(string summonerAccountId, Region region, string queue);
        ////IEnumerable<long> GetSummonerChampSummaryMatchIdsByAccountId(string summonerAccountId, Region region, string queue);
        //IEnumerable<SummonerChampSummaryStat> GetSummonerChampSummaryStats(string summonerAccountId, Region region, string queue);
        //IEnumerable<SummonerChampVsChampSummaryStat> GetSummonerChampVsChampSummaryStats(string summonerAccountId, Region region, string queue, int riotChampId);
        
        //// sets
        //void InsertSummonerMatchSummaryStat(SummonerMatchSummaryStat summonerMatchChampionStat);
        //void InsertSummonerChampVsChampStat(SummonerChampVsChampMatchStat summonerChampVsChampMatchStat);

    }
}
