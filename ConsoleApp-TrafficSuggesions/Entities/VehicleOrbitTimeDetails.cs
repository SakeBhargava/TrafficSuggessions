using System.Collections.Generic;

namespace ConsoleApp_TrafficSuggesions.Entities
{
    public class VehicleOrbitTimeDetails
    {
        public int TimeTaken { get; set; }

        public string Orbit { get; set; }

        public List<string> OrbitCombinations { get; set; }

        public string Vehicle { get; set; }
    }
}
