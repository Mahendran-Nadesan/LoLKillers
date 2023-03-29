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
        // gets
        Models.EF.Summoner GetSummonerByRiotAccountId(string region, string riotAccountId);
        Models.EF.Summoner GetSummonerBySummonerName(string region, string summonerName);

        string GetLastSummonerMatchId(string region, string summonerPuuId, string queue);
        IEnumerable<string> GetSummonerMatchIds(string region, string summonerPuuId, string queue);

        IEnumerable<Models.EF.SummonerMatchSummaryStat> GetSummonerMatchSummaryStats(string region, string summonerPuuId, string queue);

        string GetQueueNameByQueueId(int queueId);

        //IEnumerable<long> GetSummonerMatchIdsByAccountId(string region, string summonerPuuId, string queue);
        ////IEnumerable<long> GetSummonerChampSummaryMatchIdsByAccountId(string summonerAccountId, Region region, string queue);
        //IEnumerable<SummonerChampSummaryStat> GetSummonerChampSummaryStats(string summonerAccountId, Region region, string queue);
        //IEnumerable<SummonerChampVsChampSummaryStat> GetSummonerChampVsChampSummaryStats(string summonerAccountId, Region region, string queue, int riotChampId);

        // sets
        void SaveSummoner(Models.EF.Summoner appSummoner);
        void SaveSummonerMatchSummaryStat(Models.EF.SummonerMatchSummaryStat summonerMatchSummaryStat);
        //void InsertSummonerMatchSummaryStat(SummonerMatchSummaryStat summonerMatchChampionStat);
        //void InsertSummonerChampVsChampStat(SummonerChampVsChampMatchStat summonerChampVsChampMatchStat);


    }
}
