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

namespace LoLKillers.API.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly string _connectionString;

        public DatabaseRepository(IOptions<AppConfig> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public IEnumerable<long> GetSummonerChampSummaryMatchIdsByAccountId(string summonerAccountId, Region region, string queue)
        {
            var sqlParams = new
            {
                AccountId = summonerAccountId,
                Region = region.ToString(),
                Queue = queue
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<long>("GetSummonerChampMatchIdsByAccountId", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SummonerChampSummaryStat> GetSummonerChampSummaryStats(string summonerAccountId, Region region, string queue)
        {
            var sqlParams = new
            {
                AccountId = summonerAccountId,
                Region = region.ToString(),
                Queue = queue
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<SummonerChampSummaryStat>("GetSummonerChampSummaryStats", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public IEnumerable<SummonerChampVsChampSummaryStat> GetSummonerChampVsChampSummaryStats(string summonerAccountId, Region region, string queue, int riotChampId)
        {
            var sqlParams = new
            {
                AccountId = summonerAccountId,
                Region = region.ToString(),
                Queue = queue,
                RiotChampId = riotChampId
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<SummonerChampVsChampSummaryStat>("GetSummonerChampVsChampSummaryStats", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<long> GetSummonerMatchIdsByAccountId(string summonerAccountId, Region region, string queue)
        {
            var sqlParams = new
            {
                AccountId = summonerAccountId,
                Region = region.ToString(),
                Queue = queue
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<long>("GetSummonerMatchIdsByAccountId", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void InsertSummonerMatchSummaryStat(SummonerMatchSummaryStat summonerMatchChampionStat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("InsertSummonerMatchSummaryStat", summonerMatchChampionStat, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void InsertSummonerChampVsChampStat(SummonerChampVsChampMatchStat summonerChampVsChampMatchStat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("InsertSummonerChampVsChampMatchStat", summonerChampVsChampMatchStat, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
