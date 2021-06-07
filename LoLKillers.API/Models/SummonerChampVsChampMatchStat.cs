using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models
{
    public class SummonerChampVsChampMatchStat
    {
        public string AccountId { get; set; }
        public string Region { get; set; }
        public string Queue { get; set; }
        public long RiotMatchId { get; set; }
        public int RiotChampId { get; set; }
        public string RiotChampName { get; set; }
        public int RiotEnemyChampId { get; set; }
        public string RiotEnemyChampName { get; set; }
        public bool IsWin { get; set; }
        public int KillsAgainstEnemyChamp { get; set; }
        public int DeathsToEnemyChamp { get; set; }
        public int AssistsAgainstEnemyChamp { get; set; }
    }
}
