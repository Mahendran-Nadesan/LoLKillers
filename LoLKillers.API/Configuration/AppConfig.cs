using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Configuration
{
    public class AppConfig
    {
        public List<string> ConnectionStrings { get; set; }
        public string DataDragonVersionsURL { get; set; }
        public int DefaultSearchNumber { get; set; }
    }
}
