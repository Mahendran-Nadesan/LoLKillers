using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Interfaces;
using LoLKillers.API.Models;
using RiotSharp.Misc;

namespace LoLKillers.API.Repositories
{
    public class SummonerChampionStatRepository : ISummonerChampionStatRepository
    {
        public SummonerChampionDetailKillStat GetSummonerChampionDetailKillStats(string summonerPUUID, Region region, int riotChampionID, int riotEnemyChampionID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SummonerChampionSummaryKillStat> GetSummonerChampionSummaryKillStats(string summonerPUUID, Region region)
        {
            // test stuff
            SummonerChampionSummaryKillStat testStat = new SummonerChampionSummaryKillStat
            {
                AverageAssists = 10,
                AverageDeaths = 5,
                AverageKills = 8,
                Region = Region.Euw,
                RiotChampionID = 11,
                RiotChampionName = "Kassawin",
                RiotEnemyChampionID = 54,
                RiotEnemyChampionName = "Hasakey",
                RiotSummonerPUUID = "uuJJnnsafi398_jna",
                TotalMatches = 25
            };

            List<SummonerChampionSummaryKillStat> testList = new List<SummonerChampionSummaryKillStat>();

            testList.Add(testStat);

            return testList.AsEnumerable();
        }
    }
}
