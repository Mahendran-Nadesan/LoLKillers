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
        [HttpGet("{region}/{summonerName}/{update?}")]
        public async Task<IActionResult> Get(string region, string summonerName, bool update = false)
        {
            LoLKillersResponse response = new();
            Models.EF.Summoner appSummoner = null;
            RiotSharp.Endpoints.SummonerEndpoint.Summoner riotSummoner = null;

            // do we already have this summoner?
            appSummoner = await _databaseRepository.GetSummonerBySummonerName(region, summonerName);

            // get data from Riot if this is a completely new summoner or if an update is requested
            if (appSummoner == null || update)
            {
                // get summoner from Riot - note that we don't have summoner level or any other cool info
                riotSummoner = await _riotApiRepository.GetSummoner(summonerName, Enum.Parse<Region>(region, true));
            }

            if (riotSummoner == null)   // could be null due to transient error, issue with API, summoner not existing, or not updating the summoner
            {
                if (appSummoner == null)
                {
                    return NotFound("Summoner Not Found");
                }

                response.Data = appSummoner;

                return Ok(response);
            }
            else
            {
                bool updateSummoner = false;
                appSummoner = await _databaseRepository.GetSummonerByRiotAccountId(region, riotSummoner.AccountId);

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

                appSummoner.SummonerLastUpdatedDate = DateTimeOffset.Now;

                // only save app summoner if we have received some data from Riot
                var summonerSaved = await _databaseRepository.SaveSummoner(appSummoner, updateSummoner);

                if (summonerSaved > 0)
                {
                    response.Data = appSummoner; // will this appSummoner have the id if it's a new save?

                    return Ok(response);
                }
                else
                {
                    response.Message = "Summoner not saved! Riot summoner returned.";
                    response.Data = riotSummoner;

                    return Ok(response);
                }
            }
        }
    }
}
