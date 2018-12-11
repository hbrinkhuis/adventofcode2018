using System.Collections.Generic;
using System.IO;

namespace adventofcode2018
{
    internal class DayTwo
    {
        private string[] boxIds;

        internal DayTwo(string filePath)
        {
            boxIds = File.ReadAllLines(filePath);
        }
        internal string PartOne()
        {
            int threeChar = 0;
            int twoChar = 0;

            foreach(string boxId in boxIds)
            {
                Dictionary<char, int> charCounts = new Dictionary<char, int>();
                var boxIdChars = boxId.ToCharArray();
                foreach (var boxIdChar in boxIdChars)
                {
                    charCounts[boxIdChar] = charCounts.ContainsKey(boxIdChar) ? ++charCounts[boxIdChar] : 1;
                }

                if (charCounts.ContainsValue(2))
                {
                    twoChar++;
                }

                if (charCounts.ContainsValue(3))
                {
                    threeChar++;
                }
            }

            return $"{threeChar * twoChar}";
        }

        internal string PartTwo()
        {
            for(int i = 0; i < boxIds.Length; ++i)
            {
                for (int j = i + 1; j < boxIds.Length; ++j)
                {
                    int diff = AreEqualButOne(boxIds[i], boxIds[j]);
                    if(diff > -1)
                    {
                        return boxIds[i].Remove(diff, 1);                        
                    }
                }
            }

            return "no answer";
        }

        private static int AreEqualButOne(string a, string b)
        {
            int difference = int.MinValue;
            for (int k = 0; k < a.Length; ++k)
            {
                if (a[k] != b[k])
                {
                    if (difference == int.MinValue)
                    {
                        difference = k;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            return difference;
        }
    }
}
