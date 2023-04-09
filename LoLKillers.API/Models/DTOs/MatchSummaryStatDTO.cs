using System.Collections.Generic;

namespace LoLKillers.API.Models.DTOs
{
    public class MatchSummaryStatDTO
    {
        public string Region { get; set; }
        public string RiotMatchId { get; set; }
        public string QueueType { get; set; }
        public int MatchDuration { get; set; }
        public MatchSummaryTeamStatDTO BlueSideMatchSummaryTeamStats { get; set; }
        public MatchSummaryTeamStatDTO RedSideMatchSummaryTeamStats { get; set; }
    }
}
