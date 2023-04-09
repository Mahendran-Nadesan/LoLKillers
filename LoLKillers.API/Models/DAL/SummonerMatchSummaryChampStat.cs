namespace LoLKillers.API.Models.DAL
{
    public class SummonerMatchSummaryChampStat
    {
        // results of IDatabaseRepository.GetSummonerMatchesSummaryStats
        public int Id { get; set; }
        public string RiotPuuId { get; set; }    // Riot's Id
        public string SummonerName { get; set; }
        public string Region { get; set; }
        public string RiotMatchId { get; set; }
        public string QueueType { get; set; }
        public int RiotTeamId { get; set; }
        public string MapSide { get; set; }
        public int RiotChampId { get; set; }
        public string RiotChampName { get; set; }
        public int MatchKills { get; set; } // this is a long in RiotSharp
        public int MatchDeaths { get; set; }
        public int MatchAssists { get; set; }
        public decimal KDA { get; set; }
        public decimal KD { get; set; }
        public decimal AD { get; set; }
        public decimal PercentageKillParticipation { get; set; }
        public int MinionsKilled { get; set; }
        public decimal MinionsKilledPerSecond { get; set; }
        public decimal MinionsKilledPerMinute { get; set; }
        public bool FirstBlood { get; set; }
        public bool FirstBloodAssist { get; set; }
        public long PhysicalDamageDealtToChampions { get; set; }
        public long MagicDamageDealtToChampions { get; set; }
        public long TotalDamageDealtToChampions { get; set; }
        public int Spell1Casts { get; set; }
        public int Spell2Casts { get; set; }
        public int Spell3Casts { get; set; }
        public int Spell4Casts { get; set; }
        public int SummonerSpell1Id { get; set; }
        public int SummonerSpell2Id { get; set; }
        public int SummonerSpell1Casts { get; set; }
        public int SummonerSpell2Casts { get; set; }
        public int GoldEarned { get; set; }
        public decimal GoldEarnedPerSecond { get; set; }
        public decimal GoldEarnedPerMinute { get; set; }
        public int GoldSpent { get; set; }
        public decimal PercentageGoldSpent { get; set; }
        public int WardsPlaced { get; set; }
        public int VisionScore { get; set; }
        public int LongestTimeSpentLiving { get; set; }     // in seconds
        public int TimeSpentAlive { get; set; }             // in seconds
        public int TimeSpentDead { get; set; }              // in seconds
        public int MatchDuration { get; set; }
        public decimal PercentageTimeSpentAlive { get; set; }
        public bool IsWin { get; set; }
        public int TeamKills { get; set; } 
        public int TeamDeaths { get; set; }
        public int TeamAssists { get; set; }
        public int TeamMinionsKilled { get; set; }
        public bool TeamFirstBlood { get; set; }
        public long TeamPhysicalDamageDealtToChampions { get; set; }
        public long TeamMagicDamageDealtToChampions { get; set; }
        public long TeamTotalDamageDealtToChampions { get; set; }
        public int TeamGoldEarned { get; set; }
        public int TeamGoldSpent { get; set; }
        public int TeamWardsPlaced { get; set; }
        public int TeamVisionScore { get; set; }
    }
}
