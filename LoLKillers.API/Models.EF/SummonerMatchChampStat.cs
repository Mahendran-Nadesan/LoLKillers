using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoLKillers.API.Models.EF
{
    public class SummonerMatchChampStat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string RiotPuuId { get;set; }    // Riot's Id
        [Required, MaxLength(10), Column(TypeName = "VARCHAR")]
        public string Region { get; set; }
        [Required, MaxLength(20), Column(TypeName = "VARCHAR")]
        public long RiotMatchId { get; set; }
        [Required, MaxLength(30), Column(TypeName = "VARCHAR")]
        public string QueueType { get; set; }
        [Required]
        public int RiotChampId { get; set; }
        [Required, MaxLength(30), Column(TypeName = "VARCHAR")]
        public string RiotChampName { get; set; }
        [Required]
        public int RiotEnemyChampId { get; set; }
        [Required, MaxLength(30), Column(TypeName = "VARCHAR")]
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
