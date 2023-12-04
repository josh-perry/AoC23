using System.Text.RegularExpressions;

namespace AoC23.Day4;

public class Day4 : IDay
{
    public int Day => 4;

    private class Card
    {
        public int Id { get; set; }
        
        public List<int> WinningNumbers { get; init; } = new();

        public List<int> RevealedNumbers { get; init; } = new();

        public int Copies { get; set; } = 1;

        public override string ToString()
        {
            return Id.ToString();
        }
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
                Id = int.Parse(match.Groups["id"].Value),
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
        var cards = ParseCards(input);

        foreach (var card in cards)
        {
            for (var i = 0; i < card.Copies; i++)
            {
                var matches = card.WinningNumbers.Intersect(card.RevealedNumbers).ToList();
                
                for (var copyIndex = 0; copyIndex < matches.Count; copyIndex++)
                {
                    cards[card.Id + copyIndex].Copies++;
                }
            }
        }
        
        return cards.Sum(x => x.Copies).ToString();
    }
}