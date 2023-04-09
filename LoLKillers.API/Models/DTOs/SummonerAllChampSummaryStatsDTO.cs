using System.Collections.Generic;

namespace LoLKillers.API.Models.DTOs
{
    public class SummonerAllChampSummaryStatsDTO
    {
        public string RiotPuuId { get; set; }
        public List<SummonerChampSummaryStatsDTO> ChampSummaryStats { get; set; }
    }
}
