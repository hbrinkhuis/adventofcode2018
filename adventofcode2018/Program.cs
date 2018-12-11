using System;
using System.IO;
using System.Reflection;

namespace adventofcode2018
{
    class Program
    {
        private static string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        static void Main(string[] args)
        {
            DayOne dayOne = new DayOne($@"{currentPath}\Resources\day1_input");
            Console.WriteLine($"Answer day 1: {dayOne.PartOne()}");
            Console.WriteLine($"Answer day 1b: {dayOne.PartTwo()}");
            DayTwo dayTwo = new DayTwo($@"{currentPath}\Resources\day2_input");
            Console.WriteLine($"Answer day 2: {dayTwo.PartOne()}");
            Console.WriteLine($"Answer day 2b: {dayTwo.PartTwo()}");
            DayThree dayThree = new DayThree($@"{currentPath}\Resources\day3_input");
            Console.WriteLine($"Answer day 3: {dayThree.PartOne()}");
            Console.WriteLine($"Answer day 3b: {dayThree.PartTwo()}");
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey(false);
        }
    }
}
