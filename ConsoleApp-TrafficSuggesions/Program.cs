using System;

namespace ConsoleApp_TrafficSuggesions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------Problem 1------------");
            Console.WriteLine("");

            Problem1.DisplayTrafficSuggesions();

            Console.WriteLine("");
            Console.WriteLine("----------Problem 2------------");

            Problem2.FindTrafficSuggesions();

            Console.ReadKey();
        }
    }
}
