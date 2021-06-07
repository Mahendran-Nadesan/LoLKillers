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
        //RiotApi RiotApi { get; set; }
        Summoner GetSummoner(string summonerName, Region region);
        MatchList GetMatchList(Summoner summoner, long numberOfMatches, List<int> queueList); // eventually add support for queue types
        Match GetMatch(MatchReference matchReference);
        IEnumerable<Match> GetMatches(IEnumerable<MatchReference> matchList);
        MatchTimeline GetMatchTimeline(MatchReference match);
        IEnumerable<MatchTimeline> GetMatchTimelines(IEnumerable<MatchReference> matchList);
        SummonerMatchSummaryStat GetSummonerMatchStats(Summoner summoner, Match match, ChampionListStatic champions);


        // Static Data
        ChampionListStatic GetChampions();
    }
}
