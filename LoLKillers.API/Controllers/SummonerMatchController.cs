using LoLKillers.API.Configuration;
using LoLKillers.API.Interfaces;
using LoLKillers.API.Models.EF;
using LoLKillers.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RiotSharp.Misc;
using System;
using System.Linq;
using System.Collections.Generic;
using RiotSharp.Endpoints.MatchEndpoint.Enums;
using RiotSharp.Endpoints.TournamentEndpoint;
using Microsoft.AspNetCore.Razor.Language;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoLKillers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerMatchController : ControllerBase
    {
        private readonly IRiotApiRepository _riotApiRepository;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly int _searchNumber;

        public SummonerMatchController(IRiotApiRepository riotApiRepository, IDatabaseRepository databaseRepository, IOptions<AppConfig> options)
        {
            _riotApiRepository = riotApiRepository;
            _databaseRepository = databaseRepository;
            _searchNumber = options.Value.DefaultSearchNumber; // I don't like injecting options in like this, as it's in ConfigRepository which is in RiotApiRepository
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="riotPuuId"></param>
        /// <param name="queue"></param>
        /// <param name="update"></param>
        /// <param name="getAllAvailableData"></param>
        /// <returns></returns>
        /// <remarks>getAllAvailableData can only be true if update is true</remarks>
        // returns IEnumerable<SummonerMatchSummaryStat>
        // GET: api/<SummonerMatchController>
        // api/summonermatch/Euw/hmXHjKKRHPgaazsZMySVeh8RcJOxR3riiu7dxaGkUHj9ikvAtwVsbt7JYC9T9TNg1KJODf3jQwN2MA/ranked/false/false
        [HttpGet("{region}/{riotPuuId}/{queue?}/{update?}/{getAllAvailableData?}")]
        public async Task<IActionResult> Get(string region, string riotPuuId, string queue = "all", bool update = false, bool getAllAvailableData = false)
        //public IEnumerable<RiotSharp.Endpoints.MatchEndpoint.Match> Get(string region, string summonerPuuId, string queue = "All")
        {
            //// get last stored match id
            //var summonerLastStoredMatchId = _databaseRepository.GetLastSummonerMatchId(region, summonerPuuId, queue);

            LoLKillersResponse response = new();

            // maybe remove this check
            if (getAllAvailableData && !update)
            {
                response.Message = "Cannot get all historic data if update parameter is false.";
                return BadRequest(response);
            }

            // get stored match ids
            var storedSummonerMatchIds = _databaseRepository.GetSummonerMatchIds(region, riotPuuId, queue);

            // get and save new matches from Riot (for all summoners in this summoner's matches)
            MatchFilterType? matchFilterType = null;

            if (queue == "normal" || queue == "ranked")
            {
                matchFilterType = Enum.Parse<MatchFilterType>(queue, true);
            }

            if (update)
            {
                var riotMatchList = await _riotApiRepository.GetMatchList(Enum.Parse<Region>(region, true), riotPuuId, _searchNumber, 0, matchFilterType);

                if (riotMatchList != null && riotMatchList.Any() && getAllAvailableData == true)
                {
                    var newIndex = _searchNumber + 1;

                    while (true)
                    {
                        var additionalMatchList = await _riotApiRepository.GetMatchList(Enum.Parse<Region>(region, true), riotPuuId, _searchNumber, newIndex, matchFilterType);

                        if (!additionalMatchList.Any())
                        {
                            break;
                        }

                        if (additionalMatchList.Count == _searchNumber)
                        {
                            riotMatchList.AddRange(additionalMatchList);
                            newIndex += _searchNumber;
                        }
                        else
                        {
                            riotMatchList.AddRange(additionalMatchList);
                            break;
                        }
                    }
                }

                var newMatchesMatchList = riotMatchList.Where(matchId => !storedSummonerMatchIds.Any(id => id == matchId));

                if (newMatchesMatchList.Any())
                {
                    var matches = await _riotApiRepository.GetMatches(Enum.Parse<Region>(region, true), newMatchesMatchList);

                    foreach (var match in matches)
                    {
                        var riotMatchId = match.Metadata.MatchId;
                        var mappedQueueType = _databaseRepository.GetQueueNameByQueueId(match.Info.QueueId);
                        var team1Id = match.Info.Teams[0].TeamId;
                        var team2Id = match.Info.Teams[1].TeamId;

                        TeamMatchSummaryStat team1Stats = new()
                        {
                            Region = region,
                            RiotMatchId = riotMatchId,
                            QueueType = mappedQueueType,
                            RiotTeamId = team1Id,
                            TeamKills = Convert.ToInt32(match.Info.Participants.Where(p => p.TeamId == team1Id).Sum(t => t.Kills)),
                            TeamDeaths = Convert.ToInt32(match.Info.Participants.Where(p => p.TeamId == team1Id).Sum(t => t.Deaths)),
                            TeamAssists = Convert.ToInt32(match.Info.Participants.Where(p => p.TeamId == team1Id).Sum(t => t.Assists)),
                        };

                        TeamMatchSummaryStat team2Stats = new()
                        {
                            Region = region,
                            RiotMatchId = riotMatchId,
                            QueueType = mappedQueueType,
                            RiotTeamId = team2Id,
                            TeamKills = Convert.ToInt32(match.Info.Participants.Where(p => p.TeamId == team2Id).Sum(t => t.Kills)),
                            TeamDeaths = Convert.ToInt32(match.Info.Participants.Where(p => p.TeamId == team2Id).Sum(t => t.Deaths)),
                            TeamAssists = Convert.ToInt32(match.Info.Participants.Where(p => p.TeamId == team2Id).Sum(t => t.Assists)),
                        };

                        await _databaseRepository.SaveTeamMatchSummaryStat(team1Stats);
                        await _databaseRepository.SaveTeamMatchSummaryStat(team2Stats);

                        foreach (var participant in match.Info.Participants)
                        {
                            
                            // we create summoner data without actually creating a summoner on our end
                            SummonerMatchSummaryStat participantStat = new()
                            {
                                Region = region,
                                RiotPuuId = participant.Puuid,
                                RiotMatchId = riotMatchId,
                                QueueType = mappedQueueType,
                                RiotChampId = participant.ChampionId,
                                RiotChampName = participant.ChampionName,
                                MatchKills = Convert.ToInt32(participant.Kills),
                                MatchDeaths = Convert.ToInt32(participant.Deaths),
                                MatchAssists = Convert.ToInt32(participant.Assists),
                                MinionsKilled = Convert.ToInt32(participant.TotalMinionsKilled),
                                FirstBlood = participant.FirstBloodKill,
                                FirstBloodAssist = participant.FirstBloodAssist,
                                PhysicalDamageDealtToChampions = participant.PhysicalDamageDealtToChampions,
                                MagicDamageDealtToChampions = participant.MagicDamageDealtToChampions,
                                TotalDamageDealtToChampions = participant.TotalDamageDealtToChampions,
                                Spell1Casts = participant.Spell1Casts,
                                Spell2Casts = ,
                                Spell3Casts = ,
                                Spell4Casts = ,
                                SummonerSpell1Casts = ,
                                SummonerSpell2Casts = ,
                                GoldEarned = ,
                                GoldSpent = ,
                                WardsPlaced = ,
                                VisionScore = ,
                                LongestTimeSpentLiving = participant.LongestTimeSpentLiving.TotalSeconds,
                                TimeSpentDead = participant.TotalTimeSpentDead.TotalSeconds,
                                MatchDuration = participant.timePlayed.TotalSeconds,
                                IsWin = participant.Winner,
                            };

                            // save summoner's match summary stat
                            _databaseRepository.SaveSummonerMatchSummaryStat(participantStat); //todo: change to async
                        }
                    }

                    if (matches.Count() != newMatchesMatchList.Count())
                    {
                        response.Message = "Historic Data incomplete, please try again!";
                    }

                    var appSummoner = await _databaseRepository.GetSummonerByRiotPuuId(region, riotPuuId);

                    // no null check, appSummoner should never be null if this endpoint is being hit
                    appSummoner.SummonerMatchesLastUpdatedDate = DateTimeOffset.Now;
                    var summonerSaved = await _databaseRepository.SaveSummoner(appSummoner);

                    if (summonerSaved == 0)
                    {
                        response.Message += "; Summoner not saved!";
                    }

                }
            }

            var summonerMatchSummaryStats = await _databaseRepository.GetSummonerMatchSummaryStats(region, riotPuuId, queue);

            if (summonerMatchSummaryStats != null && summonerMatchSummaryStats.Any())
            {
                // return all stored matches
                response.Data = summonerMatchSummaryStats;
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
