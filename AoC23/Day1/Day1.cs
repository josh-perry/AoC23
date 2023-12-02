using System.Text.RegularExpressions;

namespace AoC23.Day1;

public class Day1 : IDay
{
    public int Day => 1;

    public string Part1(string input)
    {
        var regex = new Regex(@"\d");
        var sum = 0;

        foreach (var line in input.Split(Environment.NewLine))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            var matches = regex.Matches(line);

            var first = matches[0].ToString();
            var last = matches[^1].ToString();

            sum += int.Parse(first + last);
        }

        return sum.ToString();
    }

    public string Part2(string input)
    {
        var numberStrings = new[]
        {
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
        };
        
        var regex = new Regex(@"\d");
        var sum = 0;
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var numbersInLine = new List<(int Position, int Digit)>();

            foreach (var number in numberStrings)
            {
                var firstIndex = line.IndexOf(number);
                if (firstIndex != -1)
                {
                    numbersInLine.Add((firstIndex, Array.IndexOf(numberStrings, number)));
                }

                var lastIndex = line.LastIndexOf(number);
                if (lastIndex != -1)
                {
                    numbersInLine.Add((lastIndex, Array.IndexOf(numberStrings, number)));
                }
            }
            
            var matches = regex.Matches(line);

            foreach (Match match in matches)
            {
                numbersInLine.Add((match.Index, int.Parse(match.Value)));
            }

            var first = numbersInLine.OrderBy(x => x.Position).First().Digit;
            var last = numbersInLine.OrderBy(x => x.Position).Last().Digit;
            
            sum += int.Parse(first.ToString() + last.ToString());
        }

        return sum.ToString();
    }
}