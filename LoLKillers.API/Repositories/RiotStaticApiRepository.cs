using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoLKillers.API.Interfaces;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;

namespace LoLKillers.API.Repositories
{
    public class RiotStaticApiRepository : IRiotStaticApiRepository
    {
        private readonly IConfigRepository _configRepository;
        private readonly IRiotApiRepository _riotApiRepository;

        public RiotStaticApiRepository(IConfigRepository configRepository, IRiotApiRepository riotApiRepository)
        {
            _configRepository = configRepository;
            _riotApiRepository = riotApiRepository;
        }

        public ChampionListStatic GetChampions()
        {

            return null;
        }
    }
}
