using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoLKillers.API.Models.EF
{
    public class MatchTeamSummaryStat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(10), Column(TypeName = "VARCHAR")]
        public string Region { get; set; }
        [Required, MaxLength(20), Column(TypeName = "VARCHAR")]
        public string RiotMatchId { get; set; }
        [Required, MaxLength(30), Column(TypeName = "VARCHAR")] 
        public string QueueType { get; set; }
        [Required]
        public int RiotTeamId { get; set; }
        [Required]
        public int TeamKills { get; set; }
        [Required]
        public int TeamDeaths { get; set; }
        [Required]
        public int TeamAssists { get; set; }
        [Required]
        public int TeamMinionsKilled { get; set; }
        [Required]
        public bool TeamFirstBlood { get; set; }
        [Required]
        public long TeamPhysicalDamageDealtToChampions { get; set; }
        [Required]
        public long TeamMagicDamageDealtToChampions { get; set; }
        [Required]
        public long TeamTotalDamageDealtToChampions { get; set; }
        [Required]
        public int TeamGoldEarned { get; set; }
        [Required]
        public int TeamGoldSpent { get; set; }
        [Required]
        public int TeamWardsPlaced { get; set; }
        [Required]
        public int TeamVisionScore { get; set; }
        [Required]
        public int MatchDuration { get; set; }
        [Required]
        public bool IsWin { get; set; }
    }
}
