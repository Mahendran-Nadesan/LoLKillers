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

        public ConfigRepository(IOptions<AppConfig> options)
        {
            _connectionString = options.Value.ConnectionString;
            _dataDragonURL = options.Value.DataDragonVersionsURL;
            //_dataDragonVersion = options.Value.DataDragonVersion;
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

                using (var connection = new SqlConnection(_connectionString))
                {
                    var dbDataDragonVersion = connection.ExecuteScalar<string>("EXEC GetDataDragonVersion;");

                    return dbDataDragonVersion;
                }
            }
        }

        public string GetRiotApiKey()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var riotApiKey = connection.ExecuteScalar<string>("EXEC GetRiotApiKey;");

                return riotApiKey;
            }
        }
    }
}
