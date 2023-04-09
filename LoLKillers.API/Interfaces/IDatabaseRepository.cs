using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Models.DAL;
using RiotSharp.Misc;

namespace LoLKillers.API.Interfaces
{
    public interface IDatabaseRepository
    {
        // gets
        Task<Models.EF.Summoner> GetSummonerByRiotAccountId(string region, string riotAccountId);
        Task<Models.EF.Summoner> GetSummonerBySummonerName(string region, string summonerName);
        Task<Models.EF.Summoner> GetSummonerByRiotPuuId(string region, string riotPuuId);

        string GetLastSummonerMatchId(string region, string summonerPuuId, string queue);
        IEnumerable<string> GetSummonerMatchIds(string region, string summonerPuuId, string queue);
        //bool MatchRecordExists(string region, string matchId);

        Task<IEnumerable<SummonerMatchSummaryChampStat>> GetSummonerMatchesSummaryStats(string region, string riotPuuId, string queue);


        string GetQueueNameByQueueId(int queueId);

        Task<IEnumerable<SummonerChampSummaryStat>> GetSummonerChampSummaryStatsByRiotPuuId(string region, string riotPuuId, string queue, string mapSide = null);

        //IEnumerable<long> GetSummonerMatchIdsByAccountId(string region, string summonerPuuId, string queue);
        ////IEnumerable<long> GetSummonerChampSummaryMatchIdsByAccountId(string summonerAccountId, Region region, string queue);

        //IEnumerable<SummonerChampVsChampSummaryStat> GetSummonerChampVsChampSummaryStats(string summonerAccountId, Region region, string queue, int riotChampId);

        // sets
        Task<int> SaveSummoner(Models.EF.Summoner appSummoner, bool update = false);
        Task<int> SaveTeamMatchSummaryStat(Models.EF.MatchTeamSummaryStat teamMatchSummaryStat);
        Task<int> SaveTeamMatchSummaryStats(List<Models.EF.MatchTeamSummaryStat> teamMatchSummaryStats);
        Task<int> SaveSummonerMatchSummaryStat(Models.EF.SummonerMatchSummaryStat summonerMatchSummaryStat);
        Task<int> SaveSummonerMatchSummaryStats(List<Models.EF.SummonerMatchSummaryStat> summonerMatchSummaryStats);
        //void InsertSummonerMatchSummaryStat(SummonerMatchSummaryStat summonerMatchChampionStat);
        //void InsertSummonerChampVsChampStat(SummonerChampVsChampMatchStat summonerChampVsChampMatchStat);


    }
}
