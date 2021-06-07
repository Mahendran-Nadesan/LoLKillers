using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiotSharp.Misc;

namespace LoLKillers.API.Models
{
    public class SummonerChampionBase
    {
        public string RiotSummonerPUUID { get; set; }
        public Region Region { get; set; }
        public int RiotChampionID { get; set; }
        public string RiotChampionName { get; set; }
        public int RiotEnemyChampionID { get; set; }
        public string RiotEnemyChampionName { get; set; }
    }
}
