using System.Data;
using System.Text.RegularExpressions;

namespace AoC23.Day6;

public class Day6 : IDay
{
    public int Day => 6;

    private class Race
    {
        public int Time { get; set; }
        
        public int Record { get; set; }
    }
    
    private List<Race> ParseInput(string input)
    {
        var regex = new Regex(@"\d+");
        var races = new List<Race>();
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            var matches = regex.Matches(line);
            
            if (line.StartsWith("Time:"))
            {
                for (var matchIndex = 0; matchIndex < matches.Count; matchIndex++)
                {
                    var match = matches[matchIndex];

                    var race = new Race();
                    race.Time = int.Parse(match.Value);
                    
                    races.Add(race);
                }
            }
            
            if (line.StartsWith("Distance:"))
            {
                for (var matchIndex = 0; matchIndex < matches.Count; matchIndex++)
                {
                    var match = matches[matchIndex];
                    races[matchIndex].Record = int.Parse(match.Value);
                }
            }
        }

        return races;
    }

    private int CalculateNumberOfWaysToWin(Race race)
    {
        var winningStrategyCount = 0;
        
        for (var buttonHoldTime = 1; buttonHoldTime <= race.Time; buttonHoldTime++)
        {
            var remainingTime = race.Time - buttonHoldTime;
            var distanceTravelled = remainingTime * buttonHoldTime;

            if (distanceTravelled > race.Record)
            {
                winningStrategyCount++;
            }
        }
        
        return winningStrategyCount;
    }
    
    public string Part1(string input)
    {
        var races = ParseInput(input);
        return races.Select(CalculateNumberOfWaysToWin).Aggregate((a, b) => a * b).ToString();
    }

    public string Part2(string input)
    {
        return string.Empty;
    }
}