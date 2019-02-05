
namespace ConsoleApp_TrafficSuggesions.Entities
{
    public class Vehicle
    {
        public Vehicle(string name, int speed, int timetaken)
        {
            Name = name;
            Speed = speed;
            TimeTakenToCrossCrater = timetaken;
        }

        public string Name { get; set; }

        public int Speed { get; set; }

        public int TimeTakenToCrossCrater { get; set; }
    }
}
