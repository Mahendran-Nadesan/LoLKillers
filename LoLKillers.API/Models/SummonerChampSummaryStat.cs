using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models
{
    public class SummonerChampSummaryStat
    {
        public string RiotPuuId { get; set; }
        public string Region { get; set; }
        public string QueueType { get; set; }
        public int TotalMatches { get; set; }
        public int RiotChampId { get; set; }
        public string RiotChampName { get; set; }
        public int Wins { get; set; }
        public int Losses => TotalMatches - Wins;
        public decimal WinRate => Math.Round(((Convert.ToDecimal(Wins) / Convert.ToDecimal(TotalMatches)) * 100), 1);
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalAssists { get; set; }
        public int TotalMinionsKilled { get; set; }
        public int TotalFirstBloods { get; set; }
        public int TotalFirstBloodAssists { get; set; }
        public decimal AverageKills => Math.Round((Convert.ToDecimal(TotalKills) / Convert.ToDecimal(TotalMatches)), 1);
        public decimal AverageDeaths => Math.Round((Convert.ToDecimal(TotalDeaths) / Convert.ToDecimal(TotalMatches)), 1);
        public decimal AverageAssists => Math.Round((Convert.ToDecimal(TotalAssists) / Convert.ToDecimal(TotalMatches)), 1);
        public decimal KDA => Math.Round((Convert.ToDecimal(TotalKills) + Convert.ToDecimal(TotalAssists)) / Convert.ToDecimal(TotalDeaths), 1);
        public decimal KD => Math.Round(Convert.ToDecimal(TotalKills) / Convert.ToDecimal(TotalDeaths), 1);
        public decimal KA => Math.Round(Convert.ToDecimal(TotalAssists) / Convert.ToDecimal(TotalDeaths), 1);
        public decimal AverageMinionsKilled => Math.Round(Convert.ToDecimal(TotalMinionsKilled) / Convert.ToDecimal(TotalMatches), 1);
        public decimal AverageFirstBloods => Math.Round(Convert.ToDecimal(TotalFirstBloods) / Convert.ToDecimal(TotalMatches), 1);
        public decimal AverageFirstBloodAssists => Math.Round(Convert.ToDecimal(TotalFirstBloodAssists) / Convert.ToDecimal(TotalMatches), 1);
        public decimal AverageFirstBloodParticipation => Math.Round((Convert.ToDecimal(TotalFirstBloods) + Convert.ToDecimal(TotalFirstBloodAssists)) / Convert.ToDecimal(TotalMatches), 1);
    }
}
