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
using Microsoft.EntityFrameworkCore;

namespace LoLKillers.API.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly string _connectionString;
        private LoLKillersDbContext _lolKillersDbContext;

        public DatabaseRepository(IOptions<AppConfig> options, LoLKillersDbContext lolKillersDbContext)
        {
            //_connectionString = options.Value.ConnectionString;
            _lolKillersDbContext = lolKillersDbContext;
            _connectionString = _lolKillersDbContext.Connection.ConnectionString;
        }

        //todo: change EVERYTHING

        #region Summoners

        public async Task<Models.EF.Summoner> GetSummonerByRiotAccountId(string region, string riotAccountId)
        {
            return await _lolKillersDbContext.Summoners.Where(s => s.Region == region && s.RiotAccountId == riotAccountId).FirstOrDefaultAsync();
        }

        public async Task<Models.EF.Summoner> GetSummonerBySummonerName(string region, string summonerName)
        {
            // note, cannot use .ToLowerInvariant() - https://github.com/dotnet/efcore/issues/18995
            return await _lolKillersDbContext.Summoners.Where(s => s.Region == region && s.Name.ToLower() == summonerName.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<Models.EF.Summoner> GetSummonerByRiotPuuId(string region, string riotPuuId)
        {
            return await _lolKillersDbContext.Summoners.Where(s => s.Region == region && s.RiotPuuId == riotPuuId).FirstOrDefaultAsync();
        }

        public async Task<int> SaveSummoner(Models.EF.Summoner appSummoner, bool update = false) 
        {
            //todo: wrap in transaction, either here, or in calling code (but probably here to reduce code duplication)
            if (update)
            {
                _lolKillersDbContext.Summoners.Update(appSummoner);
            }
            else
            {
                _lolKillersDbContext.Summoners.Add(appSummoner);
            }
            
            return await _lolKillersDbContext.SaveChangesAsync();
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

        public async Task<IEnumerable<Models.EF.SummonerMatchSummaryStat>> GetSummonerMatchSummaryStats(string region, string summonerPuuId, string queue = "all")
        {
            if (queue == "all")
            {
                return await _lolKillersDbContext.SummonerMatchSummaryStats.Where(stat => stat.Region == region && stat.RiotPuuId == summonerPuuId).ToListAsync();
            }
            else
            {
                return await _lolKillersDbContext.SummonerMatchSummaryStats.Where(stat => stat.Region == region && stat.RiotPuuId == summonerPuuId && stat.QueueType == queue).ToListAsync();
            }
        }

        public async Task<int> SaveTeamMatchSummaryStat(Models.EF.TeamMatchSummaryStat teamMatchSummaryStat)
        {
            _lolKillersDbContext.TeamMatchSummaryStats.Add(teamMatchSummaryStat);

            return await _lolKillersDbContext.SaveChangesAsync();
        }

        public void SaveSummonerMatchSummaryStat(Models.EF.SummonerMatchSummaryStat summonerMatchSummaryStat)
        {
            //todo: wrap in transaction, either here, or in calling code (but probably here to reduce code duplication)
            //todo: return an async int for rows committed
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

        public async Task<IEnumerable<SummonerChampSummaryStat>> GetSummonerChampSummaryStatsByRiotPuuId(string region, string riotPuuId, string queue)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<SummonerChampSummaryStat>("GetSummonerChampSummaryStatsByRiotPuuId", new { Region = region, RiotPuuId = riotPuuId, Queue = queue }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

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
