using RiotSharp.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models
{
    public class SummonerMatchChampion : SummonerChampionBase
    {
        public string RiotMatchID { get; set; }
    }
}
