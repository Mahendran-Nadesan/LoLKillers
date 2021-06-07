using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models
{
    public class SummonerChampionDetailKillStat : SummonerChampionBase
    {
        public int TotalMatches { get; set; }
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalAssists { get; set; }
        public int MaxKills { get; set; }
        public int MaxDeaths { get; set; }
        public int MaxAssists { get; set; }

        //todo: add an enumerable of match history?

    }
}
