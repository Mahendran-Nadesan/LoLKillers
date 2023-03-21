using RiotSharp.Misc;

namespace LoLKillers.API.Utilities
{
    public static class RegionConverter
    {
        public static Region ConvertToRoutingRegion(Region summonerRegion)
        {
            switch (summonerRegion)
            {
                case Region.Br:
                    return Region.Americas;
                case Region.Eune:
                    return Region.Europe;
                case Region.Euw:
                    return Region.Europe;
                case Region.Na:
                    return Region.Americas;
                case Region.Kr:
                    return Region.Asia;
                case Region.Lan:
                    return Region.Americas;
                case Region.Las:
                    return Region.Americas;
                case Region.Oce:
                    return Region.NoRegion;
                case Region.Ru:
                    return Region.Europe;
                case Region.Tr:
                    return Region.Europe;
                case Region.Jp:
                    return Region.Asia;
                case Region.Global:
                    return Region.NoRegion;
                case Region.Americas:
                    return Region.Americas;
                case Region.Europe:
                    return Region.Europe;
                case Region.Asia:
                    return Region.Asia;
                case Region.NoRegion:
                    return Region.NoRegion;
                case Region.Ap:
                    return Region.NoRegion;
                case Region.Eu:
                    return Region.Europe;
                case Region.Latam:
                    return Region.Americas;
                default:
                    return Region.NoRegion;
            }
        }
    }
}
