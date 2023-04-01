using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoLKillers.API.Models.EF
{
    public class TeamMatchSummaryStat
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
        //todo: add damage stats, ward and vision stats, gold stats, minion stats

        [Required]
        public bool IsWin { get; set; }
    }
}
