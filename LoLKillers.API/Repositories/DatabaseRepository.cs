using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Configuration;
using LoLKillers.API.Interfaces;
using LoLKillers.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Dapper;
using RiotSharp.Misc;
using System.Drawing;

namespace LoLKillers.API.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly string _connectionString;
        private LoLKillersDbContext _lolKillersDbContext;

        public DatabaseRepository(IOptions<AppConfig> options, LoLKillersDbContext lolKillersDbContext)
        {
            _connectionString = options.Value.ConnectionString;
            _lolKillersDbContext = lolKillersDbContext;
            _connectionString = _lolKillersDbContext.Connection.ConnectionString;
        }

        //todo: change EVERYTHING

        #region Summoners

        public Models.EF.Summoner GetSummonerByRiotAccountId(string region, string riotAccountId)
        {
            return _lolKillersDbContext.Summoners.Where(c => c.Region == region && c.RiotAccountId == riotAccountId).FirstOrDefault();
        }

        public Models.EF.Summoner GetSummonerBySummonerName(string region, string summonerName)
        {
            // note, cannot use .ToLowerInvariant() - https://github.com/dotnet/efcore/issues/18995
            return _lolKillersDbContext.Summoners.Where(c => c.Region == region && c.Name.ToLower() == summonerName.ToLower()).FirstOrDefault();
        }

        public void SaveSummoner(Models.EF.Summoner appSummoner) 
        {
            //todo: wrap in transaction, either here, or in calling code (but probably here to reduce code duplication)
            _lolKillersDbContext.Summoners.Add(appSummoner);
            _lolKillersDbContext.SaveChanges();
        }

        #endregion

        #region Summoner Matches

        public string GetLastSummonerMatchId(string region, string summonerPuuId, string queue)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<string>("GetLastSummonerMatchIdByRiotPuuId", new { Region = region, RiotPuuId = summonerPuuId, Queue = queue }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<string> GetSummonerMatchIds(string region, string summonerPuuId, string queue)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<string>("GetSummonerMatchIdsByRiotPuuId", new { Region = region, RiotPuuId = summonerPuuId, Queue = queue }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Models.EF.SummonerMatchSummaryStat> GetSummonerMatchSummaryStats(string region, string summonerPuuId, string queue = "all")
        {
            if (queue == "all")
            {
                return _lolKillersDbContext.SummonerMatchSummaryStats.Where(stat => stat.Region == region && stat.RiotPuuId == summonerPuuId);
            }
            else
            {
                return _lolKillersDbContext.SummonerMatchSummaryStats.Where(stat => stat.Region == region && stat.RiotPuuId == summonerPuuId && stat.QueueType == queue);
            }
        }


        public void SaveSummonerMatchSummaryStat(Models.EF.SummonerMatchSummaryStat summonerMatchSummaryStat)
        {
            //todo: wrap in transaction, either here, or in calling code (but probably here to reduce code duplication)
            _lolKillersDbContext.SummonerMatchSummaryStats.Add(summonerMatchSummaryStat);
            _lolKillersDbContext.SaveChanges();
        }

        //public IEnumerable<long> GetSummonerMatchIdsByAccountId(string region, string summonerPuuId, string queue)
        //{
        //    IQueryable<Models.EF.SummonerMatchSummaryStat> summonerMatchesAllQueues = _lolKillersDbContext.SummonerMatchSummaryStats
        //        .Where(c => c.RiotPuuId == summonerAccountId && c.Region == region.ToString());

        //    IQueryable<long> summonerMatchList;

        //    if (queue != "all")
        //    {
        //        summonerMatchList = summonerMatchesAllQueues
        //            .Where(c => c.QueueType == queue)
        //            .Select(i => i.RiotMatchId);
        //    }
        //    else
        //    {
        //        summonerMatchList = summonerMatchesAllQueues
        //            .Select(i => i.RiotMatchId);
        //    }

        //    return summonerMatchList.ToList();

        //    //var sqlParams = new
        //    //{
        //    //    AccountId = summonerAccountId,
        //    //    Region = region.ToString(),
        //    //    Queue = queue
        //    //};

        //    //using (var connection = new SqlConnection(_connectionString))
        //    //{
        //    //    return connection.Query<long>("GetSummonerChampMatchIdsByAccountId", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
        //    //}
        //}

        #endregion

        //public IEnumerable<SummonerChampSummaryStat> GetSummonerChampSummaryStats(string summonerAccountId, Region region, string queue)
        //{
        //    IQueryable<Models.EF.SummonerMatchSummaryStat> summonerMatchesAllQueues = _lolKillersDbContext.SummonerMatchSummaryStats
        //        .Where(c => c.RiotPuuId == summonerAccountId && c.Region == region.ToString());

        //    IQueryable<Models.EF.SummonerMatchSummaryStat> summonerMatchesInQueue;

        //    if (queue != "all")
        //    {
        //        summonerMatchesInQueue = summonerMatchesAllQueues
        //            .Where(c => c.QueueType == queue);
        //    }
        //    else
        //    {
        //        summonerMatchesInQueue = summonerMatchesAllQueues;
        //    }

        //    var summonerChampSummaryStats = summonerMatchesInQueue
        //        .GroupBy(g => g.RiotChampId)
        //        .Select(c => new SummonerChampSummaryStat
        //        {
        //            AccountId = summonerAccountId,
        //            Region = region.ToString(),
        //            Queue = queue,
        //            NumberOfMatches = summonerMatchesInQueue.Count(i => i.RiotChampId == c.Key),
        //            RiotChampId = c.Key,
        //            RiotChampName = summonerMatchesInQueue.Where(i => i.RiotChampId == c.Key).Select(d => d.RiotChampName).First(), //todo: use a lookup here
        //            Wins = summonerMatchesInQueue.Count(i => i.RiotChampId == c.Key && i.IsWin),
        //            Losses = summonerMatchesInQueue.Count(i => i.RiotChampId == c.Key && !i.IsWin),
        //            AverageKills = summonerMatchesInQueue.Where(i => i.RiotChampId == c.Key).Average(a => a.MatchKills),
        //            AverageDeaths = summonerMatchesInQueue.Where(i => i.RiotChampId == c.Key).Average(a => a.MatchDeaths),
        //            AverageAssists = summonerMatchesInQueue.Where(i => i.RiotChampId == c.Key).Average(a => a.MatchAssists),
        //        });

        //    return summonerChampSummaryStats.ToList();

        //    //var summonerChampSummaryStat = new SummonerChampSummaryStat
        //    //{
        //    //    AccountId = summonerAccountId,
        //    //    Region = region.ToString(),
        //    //    Queue = queue,
        //    //    NumberOfMatches = (queue == "all" ? summonerMatchesAllQueues.Count() : summonerMatchesAllQueues.Where(c => c.QueueType == queue).Count(),

        //    //}

        //    //var sqlParams = new
        //    //{
        //    //    AccountId = summonerAccountId,
        //    //    Region = region.ToString(),
        //    //    Queue = queue
        //    //};

        //    //using (var connection = new SqlConnection(_connectionString))
        //    //{
        //    //    return connection.Query<SummonerChampSummaryStat>("GetSummonerChampSummaryStats", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
        //    //}
        //}

        //public IEnumerable<SummonerChampVsChampSummaryStat> GetSummonerChampVsChampSummaryStats(string summonerAccountId, Region region, string queue, int riotChampId)
        //{
        //    // filter to summoner level
        //    IQueryable<Models.EF.SummonerMatchChampStat> summonerMatchesChampRecordsAllQueues = _lolKillersDbContext.SummonerMatchChampStats
        //        .Where(c => c.RiotPuuId == summonerAccountId && c.Region == region.ToString());

        //    // filter to queue level
        //    IQueryable<Models.EF.SummonerMatchChampStat> summonerAllChampRecordsInMatchesInQueues;

        //    if (queue != "all")
        //    {
        //        summonerAllChampRecordsInMatchesInQueues = summonerMatchesChampRecordsAllQueues
        //            .Where(c => c.QueueType == queue);
        //                        }
        //    else
        //    {
        //        summonerAllChampRecordsInMatchesInQueues = summonerMatchesChampRecordsAllQueues;
        //    }

        //    // filter to champ level
        //    IQueryable<Models.EF.SummonerMatchChampStat> summonerChampRecordsInMatchesInQueues = summonerAllChampRecordsInMatchesInQueues
        //        .Where(f => f.RiotChampId == riotChampId);

        //    // group by enemy champs
        //    var summonerChampVsChampSummaryStats = summonerChampRecordsInMatchesInQueues
        //        .GroupBy(g => g.RiotEnemyChampId)
        //        .Select(c => new SummonerChampVsChampSummaryStat
        //        {
        //            AccountId = summonerAccountId,
        //            Region = region.ToString(),
        //            Queue = queue,
        //            NumberOfMatches = summonerChampRecordsInMatchesInQueues.Count(i => i.RiotEnemyChampId == c.Key),
        //            RiotChampId = riotChampId,
        //            RiotChampName = summonerChampRecordsInMatchesInQueues.Where(i => i.RiotChampId == riotChampId).Select(d => d.RiotChampName).First(), //todo: use a lookup here
        //            RiotEnemyChampId = c.Key,
        //            RiotEnemyChampName = summonerChampRecordsInMatchesInQueues.Where(i => i.RiotChampId == riotChampId).Select(d => d.RiotChampName).First(), //todo: use a lookup here
        //            Wins = summonerChampRecordsInMatchesInQueues.Where(i => i.RiotEnemyChampId == c.Key && i.IsWin).ToList().Count,
        //            Losses = summonerChampRecordsInMatchesInQueues.Where(i => i.RiotEnemyChampId == c.Key && !i.IsWin).ToList().Count,
        //            AverageKills = summonerChampRecordsInMatchesInQueues.Where(i => i.RiotEnemyChampId == c.Key).Average(a => a.KillsAgainstEnemyChamp),
        //            AverageDeaths = summonerChampRecordsInMatchesInQueues.Where(i => i.RiotChampId == c.Key).Average(a => a.DeathsToEnemyChamp),
        //            AverageAssists = summonerChampRecordsInMatchesInQueues.Where(i => i.RiotChampId == c.Key).Average(a => a.AssistsAgainstEnemyChampMatchAssists),
        //        });

        //    return summonerChampVsChampSummaryStats.ToList();

        //    //var sqlParams = new
        //    //{
        //    //    AccountId = summonerAccountId,
        //    //    Region = region.ToString(),
        //    //    Queue = queue,
        //    //    RiotChampId = riotChampId
        //    //};

        //    //using (var connection = new SqlConnection(_connectionString))
        //    //{
        //    //    return connection.Query<SummonerChampVsChampSummaryStat>("GetSummonerChampVsChampSummaryStats", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
        //    //}
        //}



        //public IEnumerable<long> GetSummonerMatchIdsByAccountId(string summonerAccountId, Region region, string queue)
        //{
        //    var sqlParams = new
        //    {
        //        AccountId = summonerAccountId,
        //        Region = region.ToString(),
        //        Queue = queue
        //    };

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        return connection.Query<long>("GetSummonerMatchIdsByAccountId", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
        //    }
        //}

        //public void InsertSummonerMatchSummaryStat(SummonerMatchSummaryStat summonerMatchChampionStat)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Execute("InsertSummonerMatchSummaryStat", summonerMatchChampionStat, commandType: System.Data.CommandType.StoredProcedure);
        //    }
        //}

        //public void InsertSummonerChampVsChampStat(SummonerChampVsChampMatchStat summonerChampVsChampMatchStat)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Execute("InsertSummonerChampVsChampMatchStat", summonerChampVsChampMatchStat, commandType: System.Data.CommandType.StoredProcedure);
        //    }
        //}

        #region Miscellaneous

        public string GetQueueNameByQueueId(int queueId)
        {
            if (_lolKillersDbContext.QueueTypeMappings.Any(c => c.RiotQueueId == queueId))
            {
                return _lolKillersDbContext.QueueTypeMappings.Where(c => c.RiotQueueId == queueId).FirstOrDefault().QueueName;
            }
            else
            {
                return "other";
            }
        }

        #endregion
    }
}
