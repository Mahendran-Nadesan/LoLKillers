using LoLKillers.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Models;
using RiotSharp.Misc;
using RiotSharp.Endpoints.MatchEndpoint;
using RiotSharp.Endpoints.MatchEndpoint.Enums;
using Microsoft.Extensions.Options;
using LoLKillers.API.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoLKillers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerChampionController : ControllerBase
    {
        //private readonly ISummonerChampionStatRepository _summonerChampionStatRepository;
        private readonly IRiotApiRepository _riotApiRepository;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly int _searchNumber;

        public SummonerChampionController(IRiotApiRepository riotApiRepository, IDatabaseRepository databaseRepository, IOptions<AppConfig> options)
        {
            _riotApiRepository = riotApiRepository;
            _databaseRepository = databaseRepository;
            _searchNumber = options.Value.DefaultSearchNumber; // I don't like injecting options in like this, as it's in ConfigRepository which is in RiotApiRepository
        }

        // GET: api/<SummonerChampionSummary>
        [HttpGet("{region}/{riotPuuId}/{queue?}")]
        public async Task<IActionResult> Get(string region, string riotPuuId, string queue = "all")
        {
            LoLKillersResponse response = new();

            var summonerChampSummaryStats = await _databaseRepository.GetSummonerChampSummaryStatsByRiotPuuId(region, riotPuuId, queue);

            if (summonerChampSummaryStats != null && summonerChampSummaryStats.Any())
            {
                response.Data = summonerChampSummaryStats;

                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<SummonerChampionSummary>/5
        //[HttpGet("{region}/{summonerName}/{queue}/{riotChampionId}")]
        //public IEnumerable<SummonerChampVsChampSummaryStat> Get(Region region, string summonerName, string queue, int riotChampionId)
        //{
        //    // Get summoner PUUID
        //    var summoner = _riotApiRepository.GetSummoner(summonerName, region);

        //    // get saved matches' Ids from db
        //    IEnumerable<long> matchIds = _databaseRepository.GetSummonerMatchIdsByAccountId(summoner.AccountId, region, queue);

        //    // set up queues
        //    // find a way to pull from http://static.developer.riotgames.com/docs/lol/queues.json
        //    // for now simulate normal games
        //    // normal SR: 400, 430
        //    // ranked SR: 420, 440
        //    var queueList = new List<int>();

        //    // if not specified, defaults to all queues
        //    if (queue == "normal")
        //    {
        //        queueList.Add(400);
        //        queueList.Add(430);
        //    }
        //    else if (queue == "ranked")
        //    {
        //        queueList.Add(420);
        //        queueList.Add(440);
        //    }

        //    // get matchlist
        //    // todo: if we have a match recorded for this summoner, get the last game id and send it through. if not, get all matches
        //    List<string> matchList = new List<string>();

        //    //todo: query db to find matches by summoner



        //    var matchListAll = _riotApiRepository.GetMatchList(summoner, _searchNumber); // replace numberOfMatches with const?

        //    //todo: filter by queue if necessary

        //    // filter out ones we've stored
        //    if (matchIds.Any())
        //    {
        //        matchList = matchListAll.Matches.Where(item => !matchIds.Any(id => id.Equals(item.GameId))).ToList();
        //    }
        //    else
        //    {
        //        matchList = matchListAll.Matches;
        //    }

        //    // get champion list every time we start parsing a list of matches
        //    var champions = _riotApiRepository.GetChampions();

        //    if (matchList.Any())
        //    {
        //        // parse matches to get timelines
        //        // retrieving stats and saving stats are separated                

        //        foreach (var matchId in matchList)
        //        {
        //            var match = _riotApiRepository.GetMatch(matchId);

        //            List<SummonerChampVsChampMatchStat> summonerChampMatchStats = new List<SummonerChampVsChampMatchStat>();

        //            // summoner stuff
        //            var summonerParticipantId = match.ParticipantIdentities.Single(c => c.Player.AccountId == summoner.AccountId).ParticipantId;
        //            var summonerTeamId = match.Participants.Single(c => c.ParticipantId == summonerParticipantId).TeamId;
        //            var summonerChampionId = match.Participants.Single(c => c.ParticipantId == summonerParticipantId).ChampionId;
        //            var summonerChampionName = champions.Champions.Single(c => c.Value.Id == summonerChampionId).Value.Name;
        //            var isWin = match.Participants.Single(c => c.ParticipantId == summonerParticipantId).Stats.Winner;

        //            // enemy stuff
        //            List<ParticipantChampion> enemyTeamParticipantChampions = match.Participants.Where(c => c.TeamId != summonerTeamId)
        //                .Select(d => new ParticipantChampion
        //                {
        //                    ParticipantId = d.ParticipantId,
        //                    RiotChampId = d.ChampionId,
        //                    RiotChampName = champions.Champions
        //                .Where(e => e.Value.Id == d.ChampionId).Select(f => f.Value.Name).Single()
        //                }).ToList();
        //            List<int> enemyTeamParticipantIdsOnly = match.Participants.Where(c => c.TeamId != summonerTeamId).Select(d => d.ParticipantId).ToList();

        //            var matchTimeline = _riotApiRepository.GetMatchTimeline(matchReference);
        //            var frames = matchTimeline.Frames;

        //            foreach (var enemyChamp in enemyTeamParticipantChampions)
        //            {
        //                //var killEvents = frames.Select(c => c.Events.Where(ev => ev.EventType == MatchEventType.ChampionKill && ev.KillerId == summonerParticipantId && ev.VictimId == enemyChamp.ParticipantId)).Count();
        //                //var deathEvents = frames.Select(c => c.Events.Where(ev => ev.EventType == MatchEventType.ChampionKill && ev.KillerId == enemyChamp.ParticipantId && ev.VictimId == summonerParticipantId)).Count();
        //                //var assistEvents = frames.Select(c => c.Events.Where(ev => ev.EventType == MatchEventType.ChampionKill && ev.VictimId == enemyChamp.ParticipantId && ev.AssistingParticipantIds.Contains(summonerParticipantId))).Count();

        //                var summonerChampMatchSummaryStat = new SummonerChampVsChampMatchStat
        //                {
        //                    AccountId = summoner.AccountId,
        //                    Queue = queue,
        //                    Region = region.ToString(),
        //                    RiotMatchId = match.GameId,
        //                    IsWin = isWin,
        //                    RiotChampId = summonerChampionId,
        //                    RiotChampName = summonerChampionName
        //                };

        //                int killEvents = 0;
        //                int deathEvents = 0;
        //                int assistEvents = 0;

        //                foreach (var frame in frames)
        //                {
        //                    var kills = frame.Events.Where(c => c.EventType == MatchEventType.ChampionKill && c.KillerId == summonerParticipantId && c.VictimId == enemyChamp.ParticipantId).Count();
        //                    var deaths = frame.Events.Where(c => c.EventType == MatchEventType.ChampionKill && c.KillerId == enemyChamp.ParticipantId && c.VictimId == summonerParticipantId).Count();
        //                    var assists = frame.Events.Where(c => c.EventType == MatchEventType.ChampionKill && c.VictimId == enemyChamp.ParticipantId && c.AssistingParticipantIds.Contains(summonerParticipantId)).Count();

        //                    killEvents += kills;
        //                    deathEvents += deaths;
        //                    assistEvents += assists;
        //                }

        //                summonerChampMatchSummaryStat.RiotEnemyChampId = enemyChamp.RiotChampId;
        //                summonerChampMatchSummaryStat.RiotEnemyChampName = enemyChamp.RiotChampName;
        //                summonerChampMatchSummaryStat.KillsAgainstEnemyChamp = killEvents;
        //                summonerChampMatchSummaryStat.DeathsToEnemyChamp = deathEvents;
        //                summonerChampMatchSummaryStat.AssistsAgainstEnemyChamp = assistEvents;

        //                summonerChampMatchStats.Add(summonerChampMatchSummaryStat);
        //            }

        //            foreach (var champStat in summonerChampMatchStats)
        //            {
        //                _databaseRepository.InsertSummonerChampVsChampStat(champStat);
        //            }

        //        }

        //    }

        //    var summonerChampVsChampStats = _databaseRepository.GetSummonerChampVsChampSummaryStats(summoner.AccountId, region, queue, riotChampionId);

        //    return summonerChampVsChampStats;
        //}

        // POST api/<SummonerChampionSummary>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SummonerChampionSummary>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SummonerChampionSummary>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
