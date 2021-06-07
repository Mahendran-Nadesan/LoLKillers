using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Interfaces
{
    public interface IConfigRepository
    {
        string GetRiotApiKey();
        string GetLatestDataDragonVersion();
    }
}
