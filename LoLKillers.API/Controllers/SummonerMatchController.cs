using LoLKillers.API.Configuration;
using LoLKillers.API.Interfaces;
using LoLKillers.API.Models.EF;
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
using LoLKillers.API.Models.DTOs;

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
        private readonly int _maxSearchNumber;

        public SummonerMatchController(IRiotApiRepository riotApiRepository, IDatabaseRepository databaseRepository, IOptions<AppConfig> options)
        {
            _riotApiRepository = riotApiRepository;
            _databaseRepository = databaseRepository;
            _searchNumber = options.Value.DefaultSearchNumber; // I don't like injecting options in like this, as it's in ConfigRepository which is in RiotApiRepository
            _maxSearchNumber = options.Value.MaxSearchNumber;
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
        {
            LoLKillersResponseDTO response = new();

            // maybe remove this check
            if (getAllAvailableData && !update)
            {
                response.Message = "Cannot get all historic data if update parameter is false.";
                return BadRequest(response);
            }

            // get stored match ids for summoner - if we get it for all summoners there may be too many matches to parse
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
                        var additionalMatchList = await _riotApiRepository.GetMatchList(Enum.Parse<Region>(region, true), riotPuuId, _maxSearchNumber, newIndex, matchFilterType);

                        if (!additionalMatchList.Any())
                        {
                            break;
                        }

                        if (additionalMatchList.Count == _maxSearchNumber)
                        {
                            riotMatchList.AddRange(additionalMatchList);
                            newIndex += _maxSearchNumber;
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
                        List<MatchTeamSummaryStat> teamStatsToSave = new();
                        List<SummonerMatchSummaryStat> participantsToSave = new();

                        var riotMatchId = match.Metadata.MatchId;
                        var mappedQueueType = _databaseRepository.GetQueueNameByQueueId(match.Info.QueueId);
                        var team1Id = match.Info.Teams[0].TeamId;
                        var team2Id = match.Info.Teams[1].TeamId;
                        var participants = match.Info.Participants;

                        MatchTeamSummaryStat team1Stats = new()
                        {
                            Region = region,
                            RiotMatchId = riotMatchId,
                            QueueType = mappedQueueType,
                            RiotTeamId = team1Id,
                            TeamKills = Convert.ToInt32(participants.Where(p => p.TeamId == team1Id).Sum(t => t.Kills)),
                            TeamDeaths = Convert.ToInt32(participants.Where(p => p.TeamId == team1Id).Sum(t => t.Deaths)),
                            TeamAssists = Convert.ToInt32(participants.Where(p => p.TeamId == team1Id).Sum(t => t.Assists)),
                            TeamMinionsKilled = Convert.ToInt32(participants.Where(p => p.TeamId == team1Id).Sum(t => t.TotalMinionsKilled)),
                            TeamFirstBlood = participants.Where(p => p.TeamId == team1Id).Any(c => c.FirstBloodKill),
                            TeamPhysicalDamageDealtToChampions = participants.Where(p => p.TeamId == team1Id).Sum(t => t.PhysicalDamageDealtToChampions),
                            TeamMagicDamageDealtToChampions = participants.Where(p => p.TeamId == team1Id).Sum(t => t.MagicDamageDealtToChampions),
                            TeamTotalDamageDealtToChampions = participants.Where(p => p.TeamId == team1Id).Sum(t => t.TotalDamageDealtToChampions),
                            TeamGoldEarned = Convert.ToInt32(participants.Where(p => p.TeamId == team1Id).Sum(t => t.GoldEarned)),
                            TeamGoldSpent = Convert.ToInt32(participants.Where(p => p.TeamId == team1Id).Sum(t => t.GoldSpent)),
                            TeamWardsPlaced = Convert.ToInt32(participants.Where(p => p.TeamId == team1Id).Sum(t => t.WardsPlaced)),
                            TeamVisionScore = Convert.ToInt32(participants.Where(p => p.TeamId == team1Id).Sum(t => t.VisionScore)),
                            MatchDuration = Convert.ToInt32(participants.FirstOrDefault().timePlayed.TotalSeconds),
                            IsWin = match.Info.Teams.Where(t => t.TeamId == team1Id).FirstOrDefault().Win,
                        };

                        MatchTeamSummaryStat team2Stats = new()
                        {
                            Region = region,
                            RiotMatchId = riotMatchId,
                            QueueType = mappedQueueType,
                            RiotTeamId = team2Id,
                            TeamKills = Convert.ToInt32(participants.Where(p => p.TeamId == team2Id).Sum(t => t.Kills)),
                            TeamDeaths = Convert.ToInt32(participants.Where(p => p.TeamId == team2Id).Sum(t => t.Deaths)),
                            TeamAssists = Convert.ToInt32(participants.Where(p => p.TeamId == team2Id).Sum(t => t.Assists)),
                            TeamMinionsKilled = Convert.ToInt32(participants.Where(p => p.TeamId == team2Id).Sum(t => t.TotalMinionsKilled)),
                            TeamFirstBlood = participants.Where(p => p.TeamId == team2Id).Any(c => c.FirstBloodKill),
                            TeamPhysicalDamageDealtToChampions = participants.Where(p => p.TeamId == team2Id).Sum(t => t.PhysicalDamageDealtToChampions),
                            TeamMagicDamageDealtToChampions = participants.Where(p => p.TeamId == team2Id).Sum(t => t.MagicDamageDealtToChampions),
                            TeamTotalDamageDealtToChampions = participants.Where(p => p.TeamId == team2Id).Sum(t => t.TotalDamageDealtToChampions),
                            TeamGoldEarned = Convert.ToInt32(participants.Where(p => p.TeamId == team2Id).Sum(t => t.GoldEarned)),
                            TeamGoldSpent = Convert.ToInt32(participants.Where(p => p.TeamId == team2Id).Sum(t => t.GoldSpent)),
                            TeamWardsPlaced = Convert.ToInt32(participants.Where(p => p.TeamId == team2Id).Sum(t => t.WardsPlaced)),
                            TeamVisionScore = Convert.ToInt32(participants.Where(p => p.TeamId == team2Id).Sum(t => t.VisionScore)),
                            MatchDuration = Convert.ToInt32(participants.FirstOrDefault().timePlayed.TotalSeconds),
                            IsWin = match.Info.Teams.Where(t => t.TeamId == team2Id).FirstOrDefault().Win,
                        };

                        // save both team stats
                        teamStatsToSave.Add(team1Stats);
                        teamStatsToSave.Add(team2Stats);
                        
                        foreach (var participant in participants)
                        {
                            // we create summoner data without actually creating a summoner on our end
                            SummonerMatchSummaryStat participantStat = new()
                            {
                                Region = region,
                                RiotPuuId = participant.Puuid,
                                SummonerName = participant.SummonerName,
                                RiotMatchId = riotMatchId,
                                QueueType = mappedQueueType,
                                RiotTeamId = participant.TeamId,
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
                                Spell1Casts = Convert.ToInt32(participant.Spell1Casts),
                                Spell2Casts = Convert.ToInt32(participant.Spell2Casts),
                                Spell3Casts = Convert.ToInt32(participant.Spell3Casts),
                                Spell4Casts = Convert.ToInt32(participant.Spell4Casts),
                                SummonerSpell1Id = participant.Summoner1Id,
                                SummonerSpell2Id = participant.Summoner2Id,
                                SummonerSpell1Casts = Convert.ToInt32(participant.Summoner1Casts),
                                SummonerSpell2Casts = Convert.ToInt32(participant.Summoner2Casts),
                                GoldEarned = Convert.ToInt32(participant.GoldEarned),
                                GoldSpent = Convert.ToInt32(participant.GoldSpent),
                                WardsPlaced = Convert.ToInt32(participant.WardsPlaced),
                                VisionScore = Convert.ToInt32(participant.VisionScore),
                                LongestTimeSpentLiving = Convert.ToInt32(participant.LongestTimeSpentLiving.TotalSeconds),
                                TimeSpentAlive = Convert.ToInt32(participant.timePlayed.TotalSeconds) - Convert.ToInt32(participant.TotalTimeSpentDead.TotalSeconds),
                                TimeSpentDead = Convert.ToInt32(participant.TotalTimeSpentDead.TotalSeconds),
                                IsWin = participant.Winner,
                            };

                            participantsToSave.Add(participantStat);
                        }

                        // save both team stats and ten summoner's match summary stats
                        await _databaseRepository.SaveTeamMatchSummaryStats(teamStatsToSave);
                        await _databaseRepository.SaveSummonerMatchSummaryStats(participantsToSave);
                    }

                    if (getAllAvailableData && matches.Count() != newMatchesMatchList.Count())
                    {
                        response.Message = "Historic Data incomplete, please try again!";
                    }

                    var appSummoner = await _databaseRepository.GetSummonerByRiotPuuId(region, riotPuuId);

                    // no null check, appSummoner should never be null if this endpoint is being hit
                    appSummoner.SummonerMatchesLastUpdatedDate = DateTimeOffset.Now;
                    var summonerSaved = await _databaseRepository.SaveSummoner(appSummoner, true);

                    if (summonerSaved == 0)
                    {
                        response.Message += "; Summoner not saved!";
                    }
                }
            }

            // get all data to return
            List<MatchSummaryStatDTO> summonerAllMatchSummariesStats = new();
            var summonerMatchesSummaryStats = await _databaseRepository.GetSummonerMatchesSummaryStats(region, riotPuuId, queue);

            if (summonerMatchesSummaryStats != null && summonerMatchesSummaryStats.Any())
            {
                var summonerMatches = summonerMatchesSummaryStats.GroupBy(g => g.RiotMatchId).Select(s => s.ToList()).ToList();

                foreach (var matchStats in summonerMatches)
                {
                    var matchSummaryStatDTO = new MatchSummaryStatDTO()
                    {
                        Region = region,
                        RiotMatchId = matchStats.FirstOrDefault().RiotMatchId,
                        QueueType = matchStats.FirstOrDefault().QueueType,
                        MatchDuration = matchStats.FirstOrDefault().MatchDuration,
                    };

                    // all rows have team stats, so we can take the first entry per team
                    var blueSide = matchStats.FirstOrDefault(s => s.RiotTeamId == 100);
                    var redSide = matchStats.FirstOrDefault(s => s.RiotTeamId == 200);

                    matchSummaryStatDTO.BlueSideMatchSummaryTeamStats = new()
                    {
                        RiotTeamId = blueSide.RiotTeamId,
                        MapSide = blueSide.MapSide,
                        TeamKills = blueSide.TeamKills,
                        TeamDeaths = blueSide.TeamDeaths,
                        TeamAssists = blueSide.TeamAssists,
                        TeamMinionsKilled = blueSide.TeamMinionsKilled,
                        TeamFirstBlood = Convert.ToBoolean(matchStats.Where(p => p.RiotTeamId == 100).Sum(t => Convert.ToInt32(t.FirstBlood))),
                        TeamPhysicalDamageDealtToChampions = blueSide.TeamPhysicalDamageDealtToChampions,
                        TeamMagicDamageDealtToChampions = blueSide.TeamMagicDamageDealtToChampions,
                        TeamTotalDamageDealtToChampions = blueSide.TeamTotalDamageDealtToChampions,
                        TeamGoldEarned = blueSide.TeamGoldEarned,
                        TeamGoldSpent = blueSide.TeamGoldSpent,
                        TeamWardsPlaced = blueSide.TeamWardsPlaced,
                        TeamVisionScore = blueSide.TeamVisionScore,
                        MatchSummaryChampStats = new(),
                    };

                    matchSummaryStatDTO.RedSideMatchSummaryTeamStats = new()
                    {
                        RiotTeamId = redSide.RiotTeamId,
                        MapSide = redSide.MapSide,
                        TeamKills = redSide.TeamKills,
                        TeamDeaths = redSide.TeamDeaths,
                        TeamAssists = redSide.TeamAssists,
                        TeamMinionsKilled = redSide.TeamMinionsKilled,
                        TeamFirstBlood = !matchSummaryStatDTO.BlueSideMatchSummaryTeamStats.TeamFirstBlood,
                        TeamPhysicalDamageDealtToChampions = redSide.TeamPhysicalDamageDealtToChampions,
                        TeamMagicDamageDealtToChampions = redSide.TeamMagicDamageDealtToChampions,
                        TeamTotalDamageDealtToChampions = redSide.TeamTotalDamageDealtToChampions,
                        TeamGoldEarned = redSide.TeamGoldEarned,
                        TeamGoldSpent = redSide.TeamGoldSpent,
                        TeamWardsPlaced = redSide.TeamWardsPlaced,
                        TeamVisionScore = redSide.TeamVisionScore,
                        MatchSummaryChampStats = new(),
                    };

                    foreach (var blueMatchStat in matchStats.Where(c => c.RiotTeamId == 100))
                    {
                        matchSummaryStatDTO.BlueSideMatchSummaryTeamStats.MatchSummaryChampStats.Add(new MatchSummaryChampStatDTO()
                        {
                            RiotPuuId = blueMatchStat.RiotPuuId,
                            SummonerName = blueMatchStat.SummonerName,
                            RiotTeamId = blueMatchStat.RiotTeamId,
                            MapSide = blueMatchStat.MapSide,
                            RiotChampId = blueMatchStat.RiotChampId,
                            RiotChampName = blueMatchStat.RiotChampName,
                            MatchKills = blueMatchStat.MatchKills,
                            MatchDeaths = blueMatchStat.MatchDeaths,
                            MatchAssists = blueMatchStat.MatchAssists,
                            KDA = blueMatchStat.KDA,
                            KD = blueMatchStat.KD,
                            AD = blueMatchStat.AD,
                            PercentageKillParticipation = blueMatchStat.PercentageKillParticipation,
                            MinionsKilled = blueMatchStat.MinionsKilled,
                            MinionsKilledPerSecond = blueMatchStat.MinionsKilledPerSecond,
                            MinionsKilledPerMinute = blueMatchStat.MinionsKilledPerMinute,
                            FirstBlood = blueMatchStat.FirstBlood,
                            FirstBloodAssist = blueMatchStat.FirstBloodAssist,
                            PhysicalDamageDealtToChampions = blueMatchStat.PhysicalDamageDealtToChampions,
                            MagicDamageDealtToChampions = blueMatchStat.MagicDamageDealtToChampions,
                            TotalDamageDealtToChampions = blueMatchStat.TotalDamageDealtToChampions,
                            Spell1Casts = blueMatchStat.Spell1Casts,
                            Spell2Casts = blueMatchStat.Spell2Casts,
                            Spell3Casts = blueMatchStat.Spell3Casts,
                            Spell4Casts = blueMatchStat.Spell4Casts,
                            SummonerSpell1Id = blueMatchStat.SummonerSpell1Id,
                            SummonerSpell2Id = blueMatchStat.SummonerSpell2Id,
                            SummonerSpell1Casts = blueMatchStat.SummonerSpell1Casts,
                            SummonerSpell2Casts = blueMatchStat.SummonerSpell2Casts,
                            GoldEarned = blueMatchStat.GoldEarned,
                            GoldEarnedPerSecond = blueMatchStat.GoldEarnedPerSecond,
                            GoldEarnedPerMinute = blueMatchStat.GoldEarnedPerMinute,
                            GoldSpent = blueMatchStat.GoldSpent,
                            PercentageGoldSpent = blueMatchStat.PercentageGoldSpent,
                            LongestTimeSpentLiving = blueMatchStat.LongestTimeSpentLiving,
                            TimeSpentAlive = blueMatchStat.TimeSpentAlive,
                            TimeSpentDead = blueMatchStat.TimeSpentDead,
                            PercentageTimeSpentAlive = blueMatchStat.PercentageTimeSpentAlive,
                            WardsPlaced = blueMatchStat.WardsPlaced,
                            VisionScore = blueMatchStat.VisionScore,
                            IsWin = blueMatchStat.IsWin,
                        });
                    }

                    foreach (var redMatchStat in matchStats.Where(c => c.RiotTeamId == 200))
                    {
                        matchSummaryStatDTO.RedSideMatchSummaryTeamStats.MatchSummaryChampStats.Add(new MatchSummaryChampStatDTO()
                        {
                            RiotPuuId = redMatchStat.RiotPuuId,
                            SummonerName = redMatchStat.SummonerName,
                            RiotTeamId = redMatchStat.RiotTeamId,
                            MapSide = redMatchStat.MapSide,
                            RiotChampId = redMatchStat.RiotChampId,
                            RiotChampName = redMatchStat.RiotChampName,
                            MatchKills = redMatchStat.MatchKills,
                            MatchDeaths = redMatchStat.MatchDeaths,
                            MatchAssists = redMatchStat.MatchAssists,
                            KDA = redMatchStat.KDA,
                            KD = redMatchStat.KD,
                            AD = redMatchStat.AD,
                            PercentageKillParticipation = redMatchStat.PercentageKillParticipation,
                            MinionsKilled = redMatchStat.MinionsKilled,
                            MinionsKilledPerSecond = redMatchStat.MinionsKilledPerSecond,
                            MinionsKilledPerMinute = redMatchStat.MinionsKilledPerMinute,
                            FirstBlood = redMatchStat.FirstBlood,
                            FirstBloodAssist = redMatchStat.FirstBloodAssist,
                            PhysicalDamageDealtToChampions = redMatchStat.PhysicalDamageDealtToChampions,
                            MagicDamageDealtToChampions = redMatchStat.MagicDamageDealtToChampions,
                            TotalDamageDealtToChampions = redMatchStat.TotalDamageDealtToChampions,
                            Spell1Casts = redMatchStat.Spell1Casts,
                            Spell2Casts = redMatchStat.Spell2Casts,
                            Spell3Casts = redMatchStat.Spell3Casts,
                            Spell4Casts = redMatchStat.Spell4Casts,
                            SummonerSpell1Id = redMatchStat.SummonerSpell1Id,
                            SummonerSpell2Id = redMatchStat.SummonerSpell2Id,
                            SummonerSpell1Casts = redMatchStat.SummonerSpell1Casts,
                            SummonerSpell2Casts = redMatchStat.SummonerSpell2Casts,
                            GoldEarned = redMatchStat.GoldEarned,
                            GoldEarnedPerSecond = redMatchStat.GoldEarnedPerSecond,
                            GoldEarnedPerMinute = redMatchStat.GoldEarnedPerMinute,
                            GoldSpent = redMatchStat.GoldSpent,
                            PercentageGoldSpent = redMatchStat.PercentageGoldSpent,
                            LongestTimeSpentLiving = redMatchStat.LongestTimeSpentLiving,
                            TimeSpentAlive = redMatchStat.TimeSpentAlive,
                            TimeSpentDead = redMatchStat.TimeSpentDead,
                            PercentageTimeSpentAlive = redMatchStat.PercentageTimeSpentAlive,
                            WardsPlaced = redMatchStat.WardsPlaced,
                            VisionScore = redMatchStat.VisionScore,
                            IsWin = redMatchStat.IsWin,
                        });
                    }

                    summonerAllMatchSummariesStats.Add(matchSummaryStatDTO);
                }
            }

            if (summonerAllMatchSummariesStats != null && summonerAllMatchSummariesStats.Any())
            {
                // return all stored matches
                response.Data = summonerAllMatchSummariesStats;
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
