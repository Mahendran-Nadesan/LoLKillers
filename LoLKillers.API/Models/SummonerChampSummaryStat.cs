using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models
{
    public class SummonerChampSummaryStat
    {
        public string AccountId { get; set; }
        public string Region { get; set; }
        public string Queue { get; set; }
        public int NumberOfMatches { get; set; }
        public int RiotChampId { get; set; }
        public string RiotChampName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double AverageKills { get; set; }
        public double AverageDeaths { get; set; }
        public double AverageAssists { get; set; }
    }
}
