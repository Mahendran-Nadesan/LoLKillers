using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dapper;
using LoLKillers.API.Configuration;
using LoLKillers.API.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace LoLKillers.API.Repositories
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly string _connectionString;
        private readonly string _dataDragonURL;
        private readonly LoLKillersDbContext _lolKillersDbContext;

        public ConfigRepository(IOptions<AppConfig> options, LoLKillersDbContext loLKillersDbContext)
        {
            //_connectionString = options.Value.ConnectionString;
            _dataDragonURL = options.Value.DataDragonVersionsURL;
            //_dataDragonVersion = options.Value.DataDragonVersion;
            _lolKillersDbContext= loLKillersDbContext;
        }

        public string GetLatestDataDragonVersion()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var result = httpClient.GetAsync(_dataDragonURL).Result;
                    var jsonContent = result.Content.ReadAsStringAsync().Result;
                    var versionList = JsonConvert.DeserializeObject<List<string>>(jsonContent);
                    return versionList[0];
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Error getting latest data dragon version from web");

                return _lolKillersDbContext.LoLKillersConfigs.Where(c => c.ConfigKey == "DataDragonVersion").FirstOrDefault().ConfigValue;
                //using (var connection = new SqlConnection(_connectionString))
                //{
                //    var dbDataDragonVersion = connection.ExecuteScalar<string>("EXEC GetDataDragonVersion;");

                //    return dbDataDragonVersion;
                //}
            }
        }

        public string GetRiotApiKey()
        {
            return _lolKillersDbContext.LoLKillersConfigs.Where(c => c.ConfigKey == "RiotAPIKey").FirstOrDefault().ConfigValue;
            
            
            //return "RGAPI-42c698ba-763a-409a-8052-bfc87bf73b1d";

            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var riotApiKey = connection.ExecuteScalar<string>("EXEC GetRiotApiKey;");

            //    return riotApiKey;
            //}
        }
    }
}
