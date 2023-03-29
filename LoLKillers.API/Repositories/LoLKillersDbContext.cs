using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoLKillers.API.Models.EF;
using System.Data;

namespace LoLKillers.API.Repositories
{
    public class LoLKillersDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public LoLKillersDbContext(DbContextOptions<LoLKillersDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<LoLKillersConfig> LoLKillersConfigs { get; set; }
        public DbSet<Summoner> Summoners { get; set; }
        public DbSet<SummonerMatchChampStat> SummonerMatchChampStats { get; set; }
        public DbSet<SummonerMatchSummaryStat> SummonerMatchSummaryStats { get; set; }
        public DbSet<QueueTypeMapping> QueueTypeMappings { get; set; }

        //todo: add seed data

    }
}
