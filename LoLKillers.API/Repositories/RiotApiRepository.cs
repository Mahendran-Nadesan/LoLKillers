using System;
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

namespace LoLKillers.API.Repositories
{
    public class RiotApiRepository : IRiotApiRepository
    {
        private RiotApi _riotApi { get; set; } //{ get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //private readonly string _connectionString;
        private readonly IConfigRepository _configRepository;

        public RiotApiRepository(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
            var riotApiKey = _configRepository.GetRiotApiKey();
            _riotApi = RiotApi.GetDevelopmentInstance(riotApiKey); // this will have to change if we get a production key
        }

        public Summoner GetSummoner(string summonerName, Region region)
        {
            return _riotApi.Summoner.GetSummonerByNameAsync(region, summonerName).Result;

        }

        public MatchList GetMatchList(Summoner summoner, long numberOfMatches, List<int> queueList)
        {
            return _riotApi.Match.GetMatchListAsync(summoner.Region, summoner.AccountId, null, queueList, null, null, null, 0, numberOfMatches).Result;
        }

        public Match GetMatch(MatchReference matchReference)
        {
            return _riotApi.Match.GetMatchAsync(matchReference.Region, matchReference.GameId).Result;
        }

        public IEnumerable<Match> GetMatches(IEnumerable<MatchReference> matchList)
        {
            List<Match> matches = new List<Match>();

            //todo: exclude remakes

            foreach (var matchReference in matchList)
            {
                var newMatch = _riotApi.Match.GetMatchAsync(matchReference.Region, matchReference.GameId).Result;
                matches.Add(newMatch);
            }

            return matches;
        }

        public MatchTimeline GetMatchTimeline(MatchReference matchReference)
        {
            return _riotApi.Match.GetMatchTimelineAsync(matchReference.Region, matchReference.GameId).Result;
        }

        public IEnumerable<MatchTimeline> GetMatchTimelines(IEnumerable<MatchReference> matchList)
        {
            List<MatchTimeline> matchTimelines = new List<MatchTimeline>();

            //todo: exclude remakes

            foreach (var match in matchList)
            {
                var newMatchTimeline = _riotApi.Match.GetMatchTimelineAsync(match.Region, match.GameId).Result;
                matchTimelines.Add(newMatchTimeline);
            }

            return matchTimelines;
        }

        public SummonerMatchSummaryStat GetSummonerMatchStats(Summoner summoner, Match match, ChampionListStatic champions)
        {
            //var champions = GetChampions();

            var summonerParticipantId = match.ParticipantIdentities.Single(c => c.Player.AccountId == summoner.AccountId).ParticipantId;
            var summonerChampionId = match.Participants.Single(c => c.ParticipantId == summonerParticipantId).ChampionId;
            var summonerChampionName = champions.Champions.Single(c => c.Value.Id == summonerChampionId).Value.Name;
            var summonerMatchStats = match.Participants.Single(c => c.ParticipantId == summonerParticipantId).Stats;
            var isWin = summonerMatchStats.Winner;
            string queue;

            if (match.QueueId == 400 || match.QueueId == 430)
            {
                queue = "normal";
            }
            else if (match.QueueId == 420 || match.QueueId == 440)
            {
                queue = "ranked";
            }
            else
            {
                queue = "other";
            }

            SummonerMatchSummaryStat summonerMatchChampionStat = new SummonerMatchSummaryStat
            {
                AccountId = summoner.AccountId,
                RiotMatchId = match.GameId,
                Region = summoner.Region.ToString(),
                Queue = queue,
                RiotChampId = summonerChampionId,
                RiotChampName = summonerChampionName,
                IsWin = isWin,
                MatchKills = Convert.ToInt32(summonerMatchStats.Kills),
                MatchDeaths = Convert.ToInt32(summonerMatchStats.Deaths),
                MatchAssists = Convert.ToInt32(summonerMatchStats.Assists)
            };

            return summonerMatchChampionStat;
        }

        // Calls to the static API
        public ChampionListStatic GetChampions()
        {
            // get latest data dragon version from web, or db if that fails
            var latestVersion = _configRepository.GetLatestDataDragonVersion();

            return _riotApi.DataDragon.Champions.GetAllAsync(latestVersion, Language.en_US, false).Result;
        }
    }
}
