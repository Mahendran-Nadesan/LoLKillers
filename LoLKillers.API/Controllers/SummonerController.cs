using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Interfaces;
using RiotSharp.Misc;
using RiotSharp.Endpoints.MatchEndpoint;
using LoLKillers.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoLKillers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        private readonly IRiotApiRepository _riotApiRepository;
        //private readonly IRiotStaticApiRepository _riotStaticApiRepository;
        private readonly IDatabaseRepository _databaseRepository;

        public SummonerController(IRiotApiRepository riotApiRepository, IDatabaseRepository databaseRepository)
        {
            _riotApiRepository = riotApiRepository;
            _databaseRepository = databaseRepository;
        }

        // GET: api/<SummonerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SummonerController>/5
        [HttpGet("{region}/{summonerName}/{queue}")]
        public IEnumerable<SummonerChampSummaryStat> Get(Region region, string summonerName, string queue)
        {
            // get summoner
            var summoner = _riotApiRepository.GetSummoner(summonerName, region);

            // get saved matches' Ids from db
            IEnumerable<long> matchIds = _databaseRepository.GetSummonerMatchIdsByAccountId(summoner.AccountId, region, queue);
            
            // set up queues
            // find a way to pull from http://static.developer.riotgames.com/docs/lol/queues.json
            // for now simulate normal games
            // normal SR: 400, 430
            // ranked SR: 420, 440
            var queueList = new List<int>();

            // if not specified, defaults to all queues
            if (queue == "normal")
            {
                queueList.Add(400);
                queueList.Add(430);
            }
            else if (queue == "ranked")
            {
                queueList.Add(420);
                queueList.Add(440);
            }

            // get matchlist
            List<MatchReference> matchList = new List<MatchReference>();
            var matchListAll = _riotApiRepository.GetMatchList(summoner, 10, queueList); // replace numberOfMatches with const?

            // filter out ones we've stored
            if (matchIds.Any())
            {
                matchList = matchListAll.Matches.Where(item => !matchIds.Any(id => id.Equals(item.GameId))).ToList();
            }
            else
            {
                matchList = matchListAll.Matches;
            }

            // get champion list every time we start parsing a list of matches
            var champions = _riotApiRepository.GetChampions();

            if (matchList.Any())
            {
                // parse matches to get regular stats
                // retrieving stats and saving stats are separated
                IEnumerable<Match> matches = _riotApiRepository.GetMatches(matchList);
                List<SummonerMatchSummaryStat> summonerMatchStats = new List<SummonerMatchSummaryStat>();

                // get stats
                foreach (var match in matches)
                {
                    var matchStat = _riotApiRepository.GetSummonerMatchStats(summoner, match, champions);
                    summonerMatchStats.Add(matchStat);
                }

                // save new data to db
                foreach (var summonerMatchSummary in summonerMatchStats)
                {
                    _databaseRepository.InsertSummonerMatchSummaryStat(summonerMatchSummary);
                }
            }

            // pull all data for summoner
            var summonerChampionSummaryStats = _databaseRepository.GetSummonerChampSummaryStats(summoner.AccountId, region, queue);

            return summonerChampionSummaryStats;
        }

        // POST api/<SummonerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SummonerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SummonerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
