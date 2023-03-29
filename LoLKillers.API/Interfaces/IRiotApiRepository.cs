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
using RiotSharp.Endpoints.MatchEndpoint.Enums;

namespace LoLKillers.API.Interfaces
{
    public interface IRiotApiRepository
    {
        // Summoner
        Summoner GetSummoner(string summonerName, Region region);

        // Matches
        List<string> GetMatchList(Region region, string riotPuuId, long numberOfMatches, long? startMatchId = null, MatchFilterType? matchFilterType = null);
        //Match GetMatch(string matchId);
        IEnumerable<Match> GetMatches(Region region, IEnumerable<string> matchIdsList);
        //SummonerMatchSummaryStat GetSummonerMatchStats(Summoner summoner, Match match, ChampionListStatic champions);

        // Timelines
        //MatchTimeline GetMatchTimeline(MatchReference match);
        //IEnumerable<MatchTimeline> GetMatchTimelines(IEnumerable<MatchReference> matchList);
        

        // Static Data
        ChampionListStatic GetChampions();
    }
}
