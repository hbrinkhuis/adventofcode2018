using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2018
{
    internal class DayThree
    {
        private readonly string[] claims;

        internal DayThree(string filePath)
        {
            claims = File.ReadAllLines(filePath);
        }

        internal int PartOne()
        {
            // left, top, width, height
            List<Tuple<int, int, int, int>> claimValues = new List<Tuple<int, int, int, int>>();
            int sheetWidth = 0;
            int sheetHeight = 0;

            Regex pattern = new Regex(@"#\d+ @ (\d+),(\d+): (\d+)x(\d+)");
            // determine the size of the sheet
            foreach(string claim in claims)
            {
                Match m = pattern.Match(claim);
                int left = int.Parse(m.Groups[1].Value);
                int top = int.Parse(m.Groups[2].Value);
                int width = int.Parse(m.Groups[3].Value);
                int height = int.Parse(m.Groups[4].Value);
                claimValues.Add(new Tuple<int, int, int, int>(left, top, width, height));
                if(left + width > sheetWidth)
                {
                    sheetWidth = left + width;
                }
                if(top + height > sheetHeight)
                {
                    sheetHeight = top + height;
                }
            }

            int[,] sheet = new int[sheetWidth, sheetHeight];
            foreach (var claimValue in claimValues)
            {
                for(int x = claimValue.Item1; x < (claimValue.Item1 + claimValue.Item3); ++x)
                {
                    for(int y = claimValue.Item2; y < (claimValue.Item2 + claimValue.Item4); ++y)
                    {
                        sheet[x, y]++;
                    }
                }
            }

            int sqCm = 0;
            var enumerator = sheet.GetEnumerator();
            int count = 0;
            while (enumerator.MoveNext())
            {
                count++;
                if ((int)enumerator.Current > 1)
                {
                    sqCm++;
                }
            }
            return sqCm;
        }

        internal int PartTwo()
        {
            // left, top, width, height, id
            List<Tuple<int, int, int, int, int>> claimValues = new List<Tuple<int, int, int, int, int>>();

            int sheetWidth = 0;
            int sheetHeight = 0;

            Regex pattern = new Regex(@"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)");
            // determine the size of the sheet
            foreach (string claim in claims)
            {
                Match m = pattern.Match(claim);
                int id = int.Parse(m.Groups[1].Value);
                int left = int.Parse(m.Groups[2].Value);
                int top = int.Parse(m.Groups[3].Value);
                int width = int.Parse(m.Groups[4].Value);
                int height = int.Parse(m.Groups[5].Value);
                claimValues.Add(new Tuple<int, int, int, int, int>(left, top, width, height, id));
                if (left + width > sheetWidth)
                {
                    sheetWidth = left + width;
                }
                if (top + height > sheetHeight)
                {
                    sheetHeight = top + height;
                }
            }

            int[,] sheet = new int[sheetWidth, sheetHeight];
            List<int> idsOverlapping = new List<int>();
            foreach (var claimValue in claimValues)
            {
                for (int x = claimValue.Item1; x < (claimValue.Item1 + claimValue.Item3); ++x)
                {
                    for (int y = claimValue.Item2; y < (claimValue.Item2 + claimValue.Item4); ++y)
                    {
                        if(sheet[x, y] == 0)
                        {
                            sheet[x, y] = claimValue.Item5;
                        }
                        else
                        {
                            if(!idsOverlapping.Contains(sheet[x, y]))
                            {
                                idsOverlapping.Add(sheet[x, y]);
                            }
                            if(!idsOverlapping.Contains(claimValue.Item5))
                            {
                                idsOverlapping.Add(claimValue.Item5);
                            }
                        }
                    }
                }
            }

            IEnumerable<int> idNotOverlapping = claimValues.Select(c => c.Item5).Except(idsOverlapping);
            return idNotOverlapping.Count() == 1 ? idNotOverlapping.First() : -1;
        }
    }
}
