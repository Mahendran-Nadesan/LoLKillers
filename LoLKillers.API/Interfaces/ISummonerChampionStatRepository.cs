using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Models;
using RiotSharp.Misc;

namespace LoLKillers.API.Interfaces
{
    public interface ISummonerChampionStatRepository
    {
        IEnumerable<SummonerChampionSummaryKillStat> GetSummonerChampionSummaryKillStats(string summonerPUUID, Region region);
        SummonerChampionDetailKillStat GetSummonerChampionDetailKillStats(string summonerPUUID, Region region, int riotChampionID, int riotEnemyChampionID);
    }
}
