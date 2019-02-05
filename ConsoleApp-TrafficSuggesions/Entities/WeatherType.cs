using ConsoleApp_TrafficSuggesions.Entities.Enum;
using System.Collections.Generic;

namespace ConsoleApp_TrafficSuggesions.Entities
{
    public class WeatherType
    {
        public WeatherType(string name, List<string> availableVehicles,
            Growth growthInCaters, int perOfGrowthInCraters)
        {
            Name = name;
            AvailableVehicles = availableVehicles;
            PerOfGrowthInCraters = perOfGrowthInCraters;
        }

        public string Name { get; set; }

        public List<string> AvailableVehicles { get; set; }

        public Growth GrowthInCraters { get; set; }

        public int PerOfGrowthInCraters { get; set; }
    }
}
