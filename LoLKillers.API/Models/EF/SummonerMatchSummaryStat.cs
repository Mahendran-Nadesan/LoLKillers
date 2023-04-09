using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoLKillers.API.Models.EF
{
    public class SummonerMatchSummaryStat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string RiotPuuId { get; set; }    // Riot's Id
        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string SummonerName { get; set; }
        [Required, MaxLength(10), Column(TypeName = "VARCHAR")]
        public string Region { get; set; }
        [Required, MaxLength(20), Column(TypeName = "VARCHAR")]
        public string RiotMatchId { get; set; }
        [Required, MaxLength(30), Column(TypeName = "VARCHAR")] //todo: maxlength and column type was added after initial migration
        public string QueueType { get; set; }
        [Required]
        public int RiotTeamId { get; set; }
        [Required]
        public int RiotChampId { get; set; }
        [Required, MaxLength(30), Column(TypeName = "VARCHAR")] //todo: maxlength and column type was added after initial migration
        public string RiotChampName { get; set; }
        [Required]
        public int MatchKills { get; set; } // this is a long in RiotSharp
        [Required]
        public int MatchDeaths { get; set; }
        [Required]
        public int MatchAssists { get; set; }
        [Required]
        public int MinionsKilled { get; set; }
        [Required]
        public bool FirstBlood { get; set; }
        [Required]
        public bool FirstBloodAssist { get; set; }
        [Required]
        public long PhysicalDamageDealtToChampions { get; set; }
        [Required]
        public long MagicDamageDealtToChampions { get; set; }
        [Required]
        public long TotalDamageDealtToChampions { get; set; }
        [Required]
        public int Spell1Casts { get; set; }
        [Required]
        public int Spell2Casts { get; set; }
        [Required]
        public int Spell3Casts { get; set; }
        [Required]
        public int Spell4Casts { get; set; }
        [Required]
        public int SummonerSpell1Id { get; set; }
        [Required]
        public int SummonerSpell2Id { get; set; }
        [Required]
        public int SummonerSpell1Casts { get; set; }
        [Required]
        public int SummonerSpell2Casts { get; set; }
        [Required]
        public int GoldEarned { get; set; }
        [Required]
        public int GoldSpent { get; set; }
        [Required]
        public int WardsPlaced { get; set; }
        [Required]
        public int VisionScore { get; set; }
        [Required]
        public int LongestTimeSpentLiving { get; set; }     // in seconds
        [Required]
        public int TimeSpentAlive { get; set; }             // in seconds
        [Required]
        public int TimeSpentDead { get; set; }              // in seconds
        [Required]
        public bool IsWin { get; set; }

    }
}
