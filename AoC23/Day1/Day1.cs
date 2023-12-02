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
        return string.Empty;
    }
}