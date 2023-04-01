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
        public long TotalPhysicalDamageDealtToChampions { get; set; }
        public long TotalMagicDamageDealtToChampions { get; set; }
        public long TotalDamageDealtToChampions { get; set; }
        public int TotalSpell1Casts { get; set; }
        public int TotalSpell2Casts { get; set; }
        public int TotalSpell3Casts { get; set; }
        public int TotalSpell4Casts { get; set; }
        public int TotalSummonerSpell1Casts { get; set; }
        public int TotalSummonerSpell2Casts { get; set; }
        public int TotalGoldEarned { get; set; }
        public int TotalGoldSpent { get; set; }
        public int TotalWardsPlaced { get; set; }
        public int TotalVisionScore { get; set; }
        public int TotalLongestTimeSpentLiving { get; set; }    // in seconds
        public int TotalTimeSpentDead { get; set; }             // in seconds
        public int TotalMatchesDuration { get; set; }            // in seconds
        // averages per match
        public decimal AverageKills { get; set; }
        public decimal AverageDeaths { get; set; }
        public decimal AverageAssists { get; set; }
        public decimal KDA { get; set; }
        public decimal KD { get; set; }
        public decimal AD { get; set; }
        public decimal AverageMinionsKilled { get; set; }
        public decimal AverageFirstBloods { get; set; }
        public decimal AverageFirstBloodAssists { get; set; }
        public decimal AverageFirstBloodParticipation { get; set; }
        public decimal AverageSpell1Casts { get; set; }
        public decimal AverageSpell2Casts { get; set; }
        public decimal AverageSpell3Casts { get; set; }
        public decimal AverageSpell4Casts { get; set; }
        public decimal AverageSummonerSpell1Casts { get; set; }
        public decimal AverageSummonerSpell2Casts { get; set; }
        public decimal AverageGoldEarned { get; set; }
        public decimal AverageGoldSpent { get; set; }
        public decimal AverageWardsPlaced { get; set; }
        public decimal AverageVisionScore { get; set; }
        public decimal AverageLongestTimeSpentLiving { get; set; }    // in seconds
        public decimal AverageTimeSpentDead { get; set; }   // in seconds
        public decimal AverageMatchDuration { get; set; }   // in seconds

        // computed fields - average per duration
        public decimal AverageKillsPerMinute { get; set; }
    }
}
