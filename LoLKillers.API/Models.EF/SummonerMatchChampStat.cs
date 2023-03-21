using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoLKillers.API.Models.EF
{
    public class SummonerMatchChampStat
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
        public int RiotEnemyChampId { get; set; }
        [Required]
        public string RiotEnemyChampName { get; set; }
        [Required]
        public int KillsAgainstEnemyChamp { get; set; }
        [Required]
        public int DeathsToEnemyChamp { get; set; }
        [Required]
        public int AssistsAgainstEnemyChampMatchAssists { get; set; }
        [Required]
        public bool IsWin { get; set; }
    }
}
