using System.Collections.Generic;
using System.Linq;
using ConsoleApp_TrafficSuggesions.Entities;
using ConsoleApp_TrafficSuggesions.Entities.Enum;
using ConsoleApp_TrafficSuggesions.Entities.Helpers;
using System;

namespace ConsoleApp_TrafficSuggesions
{
    public static class Problem1
    {
        public static void DisplayTrafficSuggesions()
        {
            List<Orbit> orbits = new List<Orbit>()
            {
                new Orbit("Orbit1", 18, 20, 14),
                new Orbit("Orbit2", 20, 10, 15)
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
                    foreach (Orbit orbit in orbits)
                    {
                        int craters = orbit.Craters + (weatherType.GrowthInCraters == Growth.NoChange ? 0 :
                            (weatherType.GrowthInCraters == Growth.Increased ? orbit.Craters * weatherType.PerOfGrowthInCraters / 100 : (-orbit.Craters * weatherType.PerOfGrowthInCraters / 100)));
                        vehicleOrbitTimeDetails.Add(new VehicleOrbitTimeDetails()
                        {
                            Orbit = orbit.Name,
                            Vehicle = vehicle.Name,
                            TimeTaken = (orbit.Distance / ((vehicle.Speed > orbit.SpeedLimit ? orbit.SpeedLimit  : vehicle.Speed) * 60)) + (craters * vehicle.TimeTakenToCrossCrater)
                        });
                    }
                }

                var shortestPossibleWays = vehicleOrbitTimeDetails.Where(s => s.TimeTaken == vehicleOrbitTimeDetails.Min(c => c.TimeTaken));
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
                Console.WriteLine(string.Format("Weather: {0} - Orbit: {1} - Vehicle:{2} - Time(Mins):{3}", trafficSuggesion.Weather, trafficSuggesion.Orbit, trafficSuggesion.Vehicle, trafficSuggesion.Time));
        }
    }
}
