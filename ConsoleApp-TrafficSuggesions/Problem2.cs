using ConsoleApp_TrafficSuggesions.Entities;
using ConsoleApp_TrafficSuggesions.Entities.Enum;
using ConsoleApp_TrafficSuggesions.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_TrafficSuggesions
{
    public static class Problem2
    {
        public static void FindTrafficSuggesions()
        {
            List<Orbit> orbits = new List<Orbit>()
            {
                new Orbit("Orbit1", 18, 20, 14),
                new Orbit("Orbit2", 20, 10, 15),
                new Orbit("Orbit3", 30, 15, 18),
                new Orbit("Orbit4", 15, 18, 13)
            };

            List<Vehicle> vehicles = new List<Vehicle>()
            {
                new Vehicle("Bike", 10, 2),
                new Vehicle("Tuktuk", 12, 1),
                new Vehicle("Car", 20, 3)
            };

            List<WeatherType> weatherTypeList = new List<WeatherType>() {
                new WeatherType("Sunny", new List<string>(){ "Car","Bike","Tuktuk" }, Growth.Decreased, 10),
                new WeatherType("Rainy", new List<string>(){ "Car","Tuktuk" }, Growth.Increased, 20),
                new WeatherType("Windy", new List<string>(){ "Car","Bike"}, Growth.NoChange, 0)
            };

            List<TrafficSuggesion> trafficSuggesionList = new List<TrafficSuggesion>();
            foreach (WeatherType weatherType in weatherTypeList)
            {
                List<VehicleOrbitTimeDetails> vehicleOrbitTimeDetails = new List<VehicleOrbitTimeDetails>();
                foreach (string availableVehicle in weatherType.AvailableVehicles)
                {
                    string vehicleName = availableVehicle;
                    Vehicle vehicle = vehicles.First(v => v.Name.ToLower() == vehicleName.ToLower());
                    var possibleOrbitPaths = new Dictionary<string, List<string>>() {
                            {"Path1",new List<string>() { "Orbit1", "Orbit4" }},
                            {"Path2",new List<string>() { "Orbit2", "Orbit4" }},
                            {"Path3", new List<string>() { "Orbit3", "Orbit4" }}
                          };
                    foreach (var orbitPath in possibleOrbitPaths)
                    {
                        List<string> orbitNames = orbitPath.Value;
                        int timeTaken = 0;
                        foreach (string orbitName in orbitNames)
                        {
                            Orbit orbit = orbits.First(o => o.Name == orbitName);
                            int craters = orbit.Craters + (weatherType.GrowthInCraters == Growth.NoChange ? 0 : (weatherType.GrowthInCraters == Growth.Increased 
                                ? orbit.Craters * weatherType.PerOfGrowthInCraters / 100 : (-orbit.Craters * weatherType.PerOfGrowthInCraters / 100)));

                            timeTaken = (orbit.Distance / ((vehicle.Speed > orbit.SpeedLimit ? orbit.SpeedLimit : vehicle.Speed) * 60)) + (craters * vehicle.TimeTakenToCrossCrater);
                        }
                        vehicleOrbitTimeDetails.Add(new VehicleOrbitTimeDetails()
                        {
                            OrbitCombinations = orbitNames,
                            Vehicle = vehicle.Name,
                            TimeTaken = timeTaken
                        });
                    }
                }

                var shortestPossibleWays = vehicleOrbitTimeDetails.Where((s => s.TimeTaken == vehicleOrbitTimeDetails.Min(c => c.TimeTaken)));
                if (shortestPossibleWays.Count() > 1)
                {
                    var bike = shortestPossibleWays.FirstOrDefault(s => s.Vehicle.ToLower() == "bike");
                    var tuktuk = shortestPossibleWays.FirstOrDefault(s => s.Vehicle.ToLower() == "tuktuk");
                    var car = shortestPossibleWays.FirstOrDefault(s => s.Vehicle.ToLower() == "car");
                    if (bike != null)
                        trafficSuggesionList.Add(TrafficSuggesionHelper.ConvertToTrafficSuggesion(weatherType.Name, bike));
                    else if (tuktuk != null)
                        trafficSuggesionList.Add(TrafficSuggesionHelper.ConvertToTrafficSuggesion(weatherType.Name, tuktuk));
                    else
                        trafficSuggesionList.Add(TrafficSuggesionHelper.ConvertToTrafficSuggesion(weatherType.Name, car));
                }
                else
                    trafficSuggesionList.Add(TrafficSuggesionHelper.ConvertToTrafficSuggesion(weatherType.Name, shortestPossibleWays.First()));
            }
            Console.WriteLine("Shortest possible ways to reach the destination are:");
            foreach (TrafficSuggesion trafficSuggesion in trafficSuggesionList)
                Console.WriteLine(string.Format("Weather: {0} - Orbits: {1} - Vehicle:{2} - Time(Mins):{3}", trafficSuggesion.Weather, trafficSuggesion.Orbit, trafficSuggesion.Vehicle, trafficSuggesion.Time));
        }
    }
}
