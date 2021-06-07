using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models
{
    public class SummonerMatchSummaryStat
    {
        public string AccountId { get; set; }
        public string Region { get; set; }
        public string Queue { get; set; }
        public long RiotMatchId { get; set; }
        public int RiotChampId { get; set; }
        public string RiotChampName { get; set; }
        public bool IsWin { get; set; }
        public int MatchKills { get; set; }
        public int MatchDeaths { get; set; }
        public int MatchAssists { get; set; }
    }
}
