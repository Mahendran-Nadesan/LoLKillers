using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoLKillers.API.Models.EF
{
    public class LoLKillersConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50), Column(TypeName ="VARCHAR")]
        public string ConfigKey { get; set; }
        [Required, MaxLength(100), Column(TypeName = "VARCHAR")]
        public string ConfigValue { get; set; }
    }
}
