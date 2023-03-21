using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoLKillers.API.Models.EF;

namespace LoLKillers.API.Repositories
{
    public class LoLKillersDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public LoLKillersDbContext(DbContextOptions<LoLKillersDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<LoLKillersConfig> LoLKillersConfigs { get; set; }
        public DbSet<SummonerMatchChampStat> SummonerMatchChampStats { get; set; }
        public DbSet<SummonerMatchSummaryStat> SummonerMatchSummaryStats { get; set; }

        //todo: add seed data

    }
}
