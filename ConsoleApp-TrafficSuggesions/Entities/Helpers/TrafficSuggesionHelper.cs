
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp_TrafficSuggesions.Entities.Helpers
{
    public static class TrafficSuggesionHelper
    {
        public static TrafficSuggesion ConvertToTrafficSuggesion(string weather,
           VehicleOrbitTimeDetails details)
        {
            return new TrafficSuggesion()
            {
                Weather = weather,
                Orbit = (details.OrbitCombinations != null && details.OrbitCombinations.Any()) ? string.Join("-", details.OrbitCombinations) : details.Orbit,
                Vehicle = details.Vehicle,
                Time = details.TimeTaken
            };
        }
    }
}
