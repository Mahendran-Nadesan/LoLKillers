using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [Required, MaxLength(10), Column(TypeName = "VARCHAR")]
        public string Region { get; set; }
        [Required, MaxLength(20), Column(TypeName = "VARCHAR")]
        public string RiotMatchId { get; set; }
        [Required, MaxLength(30), Column(TypeName = "VARCHAR")] //todo: maxlength and column type was added after initial migration
        public string QueueType { get; set; }
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
        public bool IsWin { get; set; }

    }
}
