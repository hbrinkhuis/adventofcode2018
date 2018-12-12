using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2018
{
    internal class DayFour
    {
        private string[] logLines;

        internal DayFour(string filePath)
        {
            logLines = File.ReadAllLines(filePath);
        }

        class LogEntry
        {
            internal string Message { get; set; }

            internal DateTime TimeStamp { get; set; }
        }

        private Dictionary<int, int[]> GetSleepingGuardMatrix()
        {
            // parse
            Regex lineRegex = new Regex(@"\[(\d{4}-\d{2}-\d{2} \d{2}:\d{2})\] (.*)");

            IEnumerable<LogEntry> entries = logLines.Select(line => lineRegex.Match(line))
                .Select(match => new LogEntry { Message = match.Groups[2].Value, TimeStamp = DateTime.Parse(match.Groups[1].Value) })
                .OrderBy(entry => entry.TimeStamp);

            Dictionary<int, int[]> sleepingGuards = new Dictionary<int, int[]>();

            int guardId = 0;
            DateTime startToSleepTime = DateTime.MinValue;
            foreach (LogEntry entry in entries)
            {
                if (entry.Message.StartsWith("Guard"))
                {
                    guardId = int.Parse(Regex.Match(entry.Message, @"#(\d+)").Groups[1].Value);
                    if (!sleepingGuards.ContainsKey(guardId))
                    {
                        sleepingGuards[guardId] = new int[60];
                    }
                }
                else if (entry.Message.StartsWith("falls"))
                {
                    startToSleepTime = entry.TimeStamp;
                }
                else if (entry.Message.StartsWith("wakes"))
                {
                    TimeSpan ts = entry.TimeStamp - startToSleepTime;

                    for (int i = 0; i < ts.TotalMinutes; ++i)
                    {
                        sleepingGuards[guardId][startToSleepTime.Minute + i]++;
                    }
                }
            }

            return sleepingGuards;
        }

        internal int PartOne()
        {
            var z = GetSleepingGuardMatrix().Select(c => new { Guard = c.Key, Sleep = c.Value.Sum(), Minute = Array.IndexOf(c.Value, c.Value.Max()) }).OrderByDescending(d => d.Sleep).First();

            return z.Guard * z.Minute;
        }

        internal int PartTwo()
        {
            var z = GetSleepingGuardMatrix().Select(c => new { Guard = c.Key, Max = c.Value.Max(), Minute = Array.IndexOf(c.Value, c.Value.Max()) }).OrderByDescending(d => d.Max).First();

            return z.Guard * z.Minute;
        }
    }
}
