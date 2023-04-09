using System.Collections.Generic;

namespace LoLKillers.API.Models.DTOs
{
    public class MatchSummaryTeamStatDTO
    {
        public int RiotTeamId { get; set; }
        public string MapSide { get; set; }
        public int TeamKills { get; set; }
        public int TeamDeaths { get; set; }
        public int TeamAssists { get; set; }
        public int TeamMinionsKilled { get; set; }
        public bool TeamFirstBlood { get; set; }
        public long TeamPhysicalDamageDealtToChampions { get; set; }
        public long TeamMagicDamageDealtToChampions { get; set; }
        public long TeamTotalDamageDealtToChampions { get; set; }
        public int TeamGoldEarned { get; set; }
        public int TeamGoldSpent { get; set; }
        public int TeamWardsPlaced { get; set; }
        public int TeamVisionScore { get; set; }
        public List<MatchSummaryChampStatDTO> MatchSummaryChampStats { get; set; }
    }
}
