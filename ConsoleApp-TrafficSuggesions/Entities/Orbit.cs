
namespace ConsoleApp_TrafficSuggesions.Entities
{
    public class Orbit
    {
        public Orbit(string name, int distance, int craters, int speedLimit)
        {
            Name = name;
            Distance = distance;
            Craters = craters;
            SpeedLimit = speedLimit;
        }

        public string Name { get; set; }

        public int Distance { get; set; }

        public int Craters { get; set; }

        public int SpeedLimit { get; set; }
    }
}
