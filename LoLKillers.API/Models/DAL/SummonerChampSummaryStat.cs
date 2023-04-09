using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models.DAL
{
    public class SummonerChampSummaryStat
    {
        //todo: consider splitting out blue and red side stats into their own class(es), and own stored procs? and then just name the variables allStats vs. blueSideStats vs. redSideStats
        public string RiotPuuId { get; set; }
        public string Region { get; set; }
        public string QueueType { get; set; }
        public string MapSide { get; set; }
        public int TotalMatches { get; set; }
        public int RiotChampId { get; set; }
        public string RiotChampName { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
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
        public int TotalTimeSpentAlive { get; set; }            // in seconds
        public int TotalMatchesDuration { get; set; }            // in seconds
        // match related stats
        public int TotalTeamKills { get; set; }
        public int TotalTeamDeaths { get; set; }
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
        public decimal AverageKillParticipation { get; set; }
        public decimal AverageDeathParticipation { get; set; }
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
        public decimal AverageTimeSpentAlive { get; set; }
        public decimal AverageMatchDuration { get; set; }   // in seconds
        // averages per duration (seconds)
        public decimal AverageKillsPerSecond { get; set; }
        public decimal AverageDeathsPerSecond { get; set; }
        public decimal AverageAssistsPerSecond { get; set; }
        public decimal AverageKDAPerSecond { get; set; }
        public decimal AverageKDPerSecond { get; set; }
        public decimal AverageADPerSecond { get; set; }
        public decimal AverageMinionsPerSecond { get; set; }
        // percentage stats
        public decimal TotalWinRate { get; set; }
        public decimal TotalKillParticipation { get; set; }
        //// Blue side stats
        //public int BlueSideTotalMatches { get; set; }
        //public int BlueSideWins { get; set; }
        //public int BlueSideWinRate { get; set; }
        //// Red side stats
        //public int RedSideTotalMatches { get; set; }
        //public int RedSideWins { get; set; }
        //public int RedSideWinRate { get; set; }
    }
}
