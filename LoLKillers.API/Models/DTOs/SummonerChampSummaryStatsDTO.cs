using LoLKillers.API.Models.DAL;

namespace LoLKillers.API.Models.DTOs
{
    public class SummonerChampSummaryStatsDTO
    {
        public int RiotChampId { get; set; }
        public string RiotChampName { get; set; }
        public string ChampDisplayName { get; set; } //todo: either parse RiotChampName (e.g. 'MissFortune' -> 'Miss Fortune') or use data dragon?
        public SummonerChampSummaryStat TotalStats { get; set; }
        public SummonerChampSummaryStat BlueSideStats { get; set; }
        public SummonerChampSummaryStat RedSideStats { get; set; }
    }
}
