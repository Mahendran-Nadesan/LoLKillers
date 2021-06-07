using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models
{
    public class SummonerMatchChampionStat : SummonerChampionBase
    {
        public string RiotMatchID { get; set; }
        public int MatchKills { get; set; }
        public int MatchDeaths { get; set; }
        public int MatchAssists { get; set; }
    }
}
