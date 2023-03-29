using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoLKillers.API.Models.EF
{
    public class Summoner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string RiotId { get; set; }
        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string RiotAccountId { get; set; }
        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string RiotPuuId { get; set; }
        [Required, MaxLength(100), Column(TypeName = "NVARCHAR")]   // account for unicode characters in names
        public string Name { get; set; }
        [Required, MaxLength(10), Column(TypeName = "VARCHAR")]
        public string Region { get; set; }
    }
}
