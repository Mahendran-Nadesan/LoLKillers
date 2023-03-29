using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Interfaces;
using RiotSharp.Misc;
using RiotSharp.Endpoints.MatchEndpoint;
using LoLKillers.API.Models;
using Microsoft.Extensions.Options;
using LoLKillers.API.Configuration;
using LoLKillers.API.Models.EF;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoLKillers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        private readonly IRiotApiRepository _riotApiRepository;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly int _searchNumber;

        public SummonerController(IRiotApiRepository riotApiRepository, IDatabaseRepository databaseRepository, IOptions<AppConfig> options)
        {
            _riotApiRepository = riotApiRepository;
            _databaseRepository = databaseRepository;
            _searchNumber = options.Value.DefaultSearchNumber; // I don't like injecting options in like this, as it's in ConfigRepository which is in RiotApiRepository
        }

        // GET: api/<SummonerController>
        //[HttpGet]
        //public string Get()
        //{
        //    // temp method to get some data
        //    var karl = _riotApiRepository.GetSummoner("leagueofmouse", Region.Euw);
        //    var mahen = _riotApiRepository.GetSummoner("nadsofmahen", Region.Euw);

        //    var mahenMatchList = _riotApiRepository.GetMatchList(mahen, 80);

        //    var mahenMatches = _riotApiRepository.GetMatches(mahen, mahenMatchList);

        //    int allWins = 0;
        //    int matchesWithKarl = 0;
        //    int winsWithKarl = 0;

        //    foreach (var match in mahenMatches)
        //    {
        //        if (match.Info.Participants.Any(c => c.SummonerId == karl.Id))
        //        {
        //            matchesWithKarl += 1;

        //            if (match.Info.Participants.Where(c => c.SummonerId == karl.Id).FirstOrDefault().Winner)
        //            {
        //                winsWithKarl += 1;
        //            }
        //        }

        //        if (match.Info.Participants.Where(c => c.SummonerId == mahen.Id).FirstOrDefault().Winner)
        //        {
        //            allWins += 1;
        //        }
        //    }

        //    return string.Format("all games: {0}, games with karl: {1}, all wins: {2}, wins with karl: {3}", mahenMatches.Count(), matchesWithKarl, allWins, winsWithKarl);

        //}

        // returns Models.EF.Summoner
        [HttpGet("{region}/{summonerName}")]
        public IActionResult Get(string region, string summonerName)
        {
            Models.EF.Summoner appSummoner;

            // get summoner from Riot & app - note that we don't have summoner level or any other cool info
            var riotSummoner = _riotApiRepository.GetSummoner(summonerName, Enum.Parse<Region>(region, true));

            if (riotSummoner == null)   // could be null due to transient error, issue with API, or summoner not existing
            {
                appSummoner = _databaseRepository.GetSummonerBySummonerName(region, summonerName);

                if (appSummoner == null)
                {
                    return NotFound("Summoner Not Found");
                }
            }
            else
            {
                appSummoner = _databaseRepository.GetSummonerByRiotAccountId(region, riotSummoner.AccountId);

                if (appSummoner == null)
                {
                    appSummoner = new Models.EF.Summoner()
                    {
                        Name = riotSummoner.Name,
                        Region = riotSummoner.Region.ToString(),
                        RiotId = riotSummoner.Id,
                        RiotAccountId = riotSummoner.AccountId,
                        RiotPuuId = riotSummoner.Puuid,
                    };
                }
                else
                {
                    // account for name changes
                    appSummoner.Name = riotSummoner.Name;
                }

                // only save app summoner if we have received some data from Riot
                _databaseRepository.SaveSummoner(appSummoner);
            }
            
            return Ok(appSummoner);
        }


        // GET api/<SummonerController>/5
        //[HttpGet("{region}/{summonerName}/{queue}")]
        //public IEnumerable<SummonerChampSummaryStat> Get(Region region, string summonerName, string queue)
        //{
        //    // get summoner
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
        //    var matchListAll = _riotApiRepository.GetMatchList(summoner, _searchNumber); // replace numberOfMatches with const?

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
        //        // parse matches to get regular stats
        //        // retrieving stats and saving stats are separated
        //        IEnumerable<Match> matches = _riotApiRepository.GetMatches(matchList);
        //        List<SummonerMatchSummaryStat> summonerMatchStats = new List<SummonerMatchSummaryStat>();

        //        // get stats
        //        foreach (var match in matches)
        //        {
        //            var matchStat = _riotApiRepository.GetSummonerMatchStats(summoner, match, champions);
        //            summonerMatchStats.Add(matchStat);
        //        }

        //        // save new data to db
        //        foreach (var summonerMatchSummary in summonerMatchStats)
        //        {
        //            _databaseRepository.InsertSummonerMatchSummaryStat(summonerMatchSummary);
        //        }
        //    }

        //    // pull all data for summoner
        //    var summonerChampionSummaryStats = _databaseRepository.GetSummonerChampSummaryStats(summoner.AccountId, region, queue);

        //    return summonerChampionSummaryStats;
        //}

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
