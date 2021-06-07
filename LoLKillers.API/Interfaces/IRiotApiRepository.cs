using RiotSharp;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiotSharp.Endpoints.MatchEndpoint;
using LoLKillers.API.Models;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;

namespace LoLKillers.API.Interfaces
{
    public interface IRiotApiRepository
    {
        // Summoner
        Summoner GetSummoner(string summonerName, Region region);

        // Matches
        MatchList GetMatchList(Summoner summoner, long numberOfMatches, List<int> queueList);
        Match GetMatch(MatchReference matchReference);
        IEnumerable<Match> GetMatches(IEnumerable<MatchReference> matchList);
        SummonerMatchSummaryStat GetSummonerMatchStats(Summoner summoner, Match match, ChampionListStatic champions);

        // Timelines
        MatchTimeline GetMatchTimeline(MatchReference match);
        IEnumerable<MatchTimeline> GetMatchTimelines(IEnumerable<MatchReference> matchList);
        

        // Static Data
        ChampionListStatic GetChampions();
    }
}
