using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoLKillers.API.Models.EF
{
    public class SummonerMatchSummaryStat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public long RiotMatchId { get; set; }
        [Required]
        public string QueueType { get; set; }
        [Required]
        public int RiotChampId { get; set; }
        [Required]
        public string RiotChampName { get; set; }
        [Required]
        public int MatchKills { get; set; }
        [Required]
        public int MatchDeaths { get; set; }
        [Required]
        public int MatchAssists { get; set; }
        [Required]
        public bool IsWin { get; set; }

    }
}
