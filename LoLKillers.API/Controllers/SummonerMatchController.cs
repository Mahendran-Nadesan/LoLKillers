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

        // returns IEnumerable<SummonerMatchSummaryStat>
        // GET: api/<SummonerMatchController>
        // api/summonermatch/Euw/hmXHjKKRHPgaazsZMySVeh8RcJOxR3riiu7dxaGkUHj9ikvAtwVsbt7JYC9T9TNg1KJODf3jQwN2MA/ranked/true
        [HttpGet("{region}/{summonerPuuId}/{queue}/{getAllAvailableData}")]
        public IActionResult Get(string region, string summonerPuuId, string queue = "all", bool getAllAvailableData = false)
        //public IEnumerable<RiotSharp.Endpoints.MatchEndpoint.Match> Get(string region, string summonerPuuId, string queue = "All")
        {
            //// get last stored match id
            //var summonerLastStoredMatchId = _databaseRepository.GetLastSummonerMatchId(region, summonerPuuId, queue);

            // get stored match ids
            var storedSummonerMatchIds = _databaseRepository.GetSummonerMatchIds(region, summonerPuuId, queue);

            // get and save new matches from Riot (for all summoners in this summoner's matches)
            MatchFilterType? matchFilterType = null;

            if (queue == "normal" || queue == "ranked")
            {
                matchFilterType = Enum.Parse<MatchFilterType>(queue, true);
            }

            var riotMatchList = _riotApiRepository.GetMatchList(Enum.Parse<Region>(region, true), summonerPuuId, _searchNumber, 0, matchFilterType);
           
            if (riotMatchList != null && riotMatchList.Any() && getAllAvailableData == true)
            {
                var newIndex = _searchNumber + 1;

                while (true)
                {
                    var additionalMatchList = _riotApiRepository.GetMatchList(Enum.Parse<Region>(region, true), summonerPuuId, _searchNumber, newIndex, matchFilterType);

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

            var newMatchesMatchList = riotMatchList.Where(matchId => !storedSummonerMatchIds.Any(id => id.Equals(matchId)));
            
            if (newMatchesMatchList.Any())
            {
                var matches = _riotApiRepository.GetMatches(Enum.Parse<Region>(region, true), riotMatchList);
                
                foreach (var match in matches)
                {
                    foreach (var participant in match.Info.Participants)
                    {
                        // we create summoner data without actually creating a summoner on our end
                        SummonerMatchSummaryStat participantStat = new()
                        {
                            Region= region,
                            RiotPuuId = participant.Puuid,
                            RiotMatchId = match.Metadata.MatchId,
                            QueueType= _databaseRepository.GetQueueNameByQueueId(match.Info.QueueId),
                            RiotChampId = participant.ChampionId,
                            RiotChampName = participant.ChampionName,
                            MatchKills = Convert.ToInt32(participant.Kills),
                            MatchDeaths = Convert.ToInt32(participant.Deaths),
                            MatchAssists = Convert.ToInt32(participant.Assists),
                            IsWin = participant.Winner,
                        };

                        // save summoner's match summary stat
                        _databaseRepository.SaveSummonerMatchSummaryStat(participantStat);
                    }
                }
            }

            var summonerMatchSummaryStats = _databaseRepository.GetSummonerMatchSummaryStats(region, summonerPuuId, queue);

            if (summonerMatchSummaryStats != null && summonerMatchSummaryStats.Any())
            {
                // return all stored matches
                return Ok(summonerMatchSummaryStats);
            }
            else
            {
                return NoContent();
            }
            
            

        }

        // GET api/<SummonerMatchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SummonerMatchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SummonerMatchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SummonerMatchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
