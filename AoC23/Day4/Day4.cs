using System.Text.RegularExpressions;

namespace AoC23.Day4;

public class Day4 : IDay
{
    public int Day => 4;

    private class Card
    {
        public List<int> WinningNumbers { get; init; } = new();

        public List<int> RevealedNumbers { get; init; } = new();
    }

    private List<Card> ParseCards(string input)
    {
        var cards = new List<Card>();
        
        var regex = new Regex(@"Card\s+(?<id>\d+): (?<winning>.+)\|(?<revealed>.+)");
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            var match = regex.Match(line);
            cards.Add(new()
            {
                WinningNumbers = match
                    .Groups["winning"]
                    .Value
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList(),
                RevealedNumbers = match
                    .Groups["revealed"]
                    .Value
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList()
            });
        }

        return cards;
    }
    
    public string Part1(string input)
    {
        var cards = ParseCards(input);
        var pointTotal = 0;
        
        foreach (var card in cards)
        {
            var matches = card.WinningNumbers.Intersect(card.RevealedNumbers).ToList();
            
            if (matches.Count == 0)
            {
                continue;
            }

            pointTotal += (int)Math.Pow(2, matches.Count - 1);
        }
        
        return pointTotal.ToString();
    }

    public string Part2(string input)
    {
        throw new NotImplementedException();
    }
}