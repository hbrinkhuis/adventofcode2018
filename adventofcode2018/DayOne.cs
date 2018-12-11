using System.Collections.Generic;
using System.IO;

namespace adventofcode2018
{
    internal class DayOne
    {
        private readonly string[] freqChanges;

        internal DayOne(string filePath)
        {
            freqChanges = File.ReadAllLines(filePath);
        }

        internal string PartOne()
        {
            int frequency = 0;
            foreach (string freqChange in freqChanges)
            {
                frequency += int.Parse(freqChange);
            }

            return $"{frequency}";
        }

        internal string PartTwo()
        {
            HashSet<int> freqs = new HashSet<int>();
            int frequency = 0;
            int counter = 0;

            while (!freqs.Contains(frequency))
            {
                freqs.Add(frequency);
                frequency += int.Parse(freqChanges[counter]);
                counter = counter < (freqChanges.Length - 1) ? ++counter : counter = 0;
            }

            return $"{frequency}";
        }
    }
}
