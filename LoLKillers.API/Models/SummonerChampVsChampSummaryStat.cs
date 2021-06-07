using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models
{
    public class SummonerChampVsChampSummaryStat : SummonerChampSummaryStat
    {
        public int RiotEnemyChampId { get; set; }
        public string RiotEnemyChampName { get; set; }
    }
}
