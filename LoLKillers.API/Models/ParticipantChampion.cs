using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API.Models
{
    public class ParticipantChampion
    {
        public int ParticipantId { get; set; }
        public int RiotChampId { get; set; }
        public string RiotChampName { get; set; }
        public int KillsAgainstParticipant { get; set; }
        public int DeathsToParticipant { get; set; }
        public int AssistsAgainstParticipant { get; set; }
    }
}
