﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Interfaces;
using Microsoft.Extensions.Options;
using RiotSharp;
using LoLKillers.API.Configuration;
using RiotSharp.Misc;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Endpoints.MatchEndpoint;
using LoLKillers.API.Models;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using LoLKillers.API.Utilities;
using RiotSharp.Endpoints.MatchEndpoint.Enums;

namespace LoLKillers.API.Repositories
{
    public class RiotApiRepository : IRiotApiRepository
    {
        private RiotApi _riotApi { get; set; } 
        private readonly IConfigRepository _configRepository;

        public RiotApiRepository(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
            var riotApiKey = _configRepository.GetRiotApiKey();
            _riotApi = RiotApi.GetDevelopmentInstance(riotApiKey); // this will have to change if we get a production key
        }

        public async Task<Summoner> GetSummoner(string summonerName, Region region)
        {
            try
            {
                return await _riotApi.Summoner.GetSummonerByNameAsync(region, summonerName);
            }
            catch (Exception e)
            {
                //todo: log exception
                return null;
            }
        }

        public async Task<List<string>> GetMatchList(Region region, string riotPuuId, long numberOfMatches, long? startMatchId = null, MatchFilterType? matchFilterType = null)
        {
            Region routingRegion = RegionConverter.ConvertToRoutingRegion(region);

            try
            {
                return await _riotApi.Match.GetMatchListAsync(routingRegion, riotPuuId, startMatchId, numberOfMatches, null, matchFilterType); // queue = null for now
            }
            catch (Exception)
            {
                //todo: log exception
                return null;
            }
            
        }

        //public Match GetMatch(string matchId)
        //{
        //    return _riotApi.Match.GetMatchAsync(matchReference.Region, matchId).Result;
        //}

        public async Task<IEnumerable<Match>> GetMatches(Region region, IEnumerable<string> matchIdsList)
        {
            Region routingRegion = RegionConverter.ConvertToRoutingRegion(region);

            List<Match> matches = new();

            //todo: exclude remakes

            try
            {
                foreach (var matchId in matchIdsList)
                {
                    var newMatch = await _riotApi.Match.GetMatchAsync(routingRegion, matchId);
                    matches.Add(newMatch);
                }

                return matches;
            }
            //catch (RiotSharpException rEx)
            //{
            //    Console.WriteLine(rEx.Message);
            //    return matches;
            //}          
            //catch (RiotSharpRateLimitException rlEx)
            //{
            //    Console.WriteLine(rEx.Message);
            //    return matches;
            //}
            catch (Exception e)
            {
                return matches;
            }
        }

        //public MatchTimeline GetMatchTimeline(string matchId)
        //{
        //    return _riotApi.Match.GetMatchTimelineAsync(matchReference.Region, matchId).Result;
        //}

        //public IEnumerable<MatchTimeline> GetMatchTimelines(IEnumerable<MatchReference> matchList)
        //{
        //    List<MatchTimeline> matchTimelines = new List<MatchTimeline>();

        //    //todo: exclude remakes

        //    foreach (var match in matchList)
        //    {
        //        var newMatchTimeline = _riotApi.Match.GetMatchTimelineAsync(match.Region, match.GameId).Result;
        //        matchTimelines.Add(newMatchTimeline);
        //    }

        //    return matchTimelines;
        //}

        //public SummonerMatchSummaryStat GetSummonerMatchStats(Summoner summoner, Match match, ChampionListStatic champions)
        //{
        //    var summonerParticipantId = match.ParticipantIdentities.Single(c => c.Player.AccountId == summoner.AccountId).ParticipantId;
        //    var summonerChampionId = match.Participants.Single(c => c.ParticipantId == summonerParticipantId).ChampionId;
        //    var summonerChampionName = champions.Champions.Single(c => c.Value.Id == summonerChampionId).Value.Name;
        //    var summonerMatchStats = match.Participants.Single(c => c.ParticipantId == summonerParticipantId).Stats;
        //    var isWin = summonerMatchStats.Winner;
        //    string queue;

        //    if (match.QueueId == 400 || match.QueueId == 430)
        //    {
        //        queue = "normal";
        //    }
        //    else if (match.QueueId == 420 || match.QueueId == 440)
        //    {
        //        queue = "ranked";
        //    }
        //    else
        //    {
        //        queue = "other";
        //    }

        //    SummonerMatchSummaryStat summonerMatchChampionStat = new SummonerMatchSummaryStat
        //    {
        //        AccountId = summoner.AccountId,
        //        RiotMatchId = match.GameId,
        //        Region = summoner.Region.ToString(),
        //        Queue = queue,
        //        RiotChampId = summonerChampionId,
        //        RiotChampName = summonerChampionName,
        //        IsWin = isWin,
        //        MatchKills = Convert.ToInt32(summonerMatchStats.Kills),
        //        MatchDeaths = Convert.ToInt32(summonerMatchStats.Deaths),
        //        MatchAssists = Convert.ToInt32(summonerMatchStats.Assists)
        //    };

        //    return summonerMatchChampionStat;
        //}

        // Calls to the static API
        //public ChampionListStatic GetChampions()
        //{
        //    // get latest data dragon version from web, or db if that fails
        //    var latestVersion = _configRepository.GetLatestDataDragonVersion();

        //    return _riotApi.DataDragon.Champions.GetAllAsync(latestVersion, Language.en_US, false).Result;
        //}
    }
}
