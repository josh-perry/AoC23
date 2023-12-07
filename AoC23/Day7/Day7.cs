namespace AoC23.Day7;

public class Day7 : IDay
{
    public int Day => 7;

    public enum HandType
    {
        HighCard = 1,
        OnePair,
        TwoPairs,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    public class Hand : IComparable<Hand>
    {
        public List<int> Cards { get; set; }
        
        public int Bet { get; set; }
        
        public HandType Type { get; set; }

        public int CompareTo(Hand? hand)
        {
            if (Type < hand.Type)
            {
                return 1;
            }

            if (Type > hand.Type)
            {
                return -1;
            }
            
            for (var i = 0; i < Cards.Count; i++)
            {
                if (Cards[i] < hand.Cards[i])
                {
                    return 1;
                }
                
                if (Cards[i] > hand.Cards[i])
                {
                    return -1;
                }
            }
            
            return 0; 
        }
    }
    
    private List<Hand> ParseInput(string input)
    {
        var hands = new List<Hand>();

        var cardRanks = new Dictionary<char, int>
        {
            { 'A', 14 },
            { 'K', 13 },
            { 'Q', 12 },
            { 'J', 11 },
            { 'T', 10 },
            { '9', 9 },
            { '8', 8 },
            { '7', 7 },
            { '6', 6 },
            { '5', 5 },
            { '4', 4 },
            { '3', 3 },
            { '2', 2 }
        };
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            var split = line.Split(" ");

            var rawCards = split[0];
            var rawBet = split[1];

            hands.Add(new()
            {
                Cards = rawCards.Select(c => cardRanks[c]).ToList(),
                Bet = int.Parse(rawBet)
            });
        }

        return hands;
    }
    
    private class CardCollection
    {
        public int Value { get; set; }
        
        public int Count { get; set; }
    }

    public void DetermineHandType(Hand hand, bool jokerRule = false)
    {
        var valueFrequencies = hand.Cards.GroupBy(card => card)
            .Select(group => new CardCollection { Value = group.Key, Count = group.Count() })
            .OrderByDescending(count => count.Count)
            .ToList();

        if (jokerRule)
        {
            var jokers = valueFrequencies.FirstOrDefault(x => x.Value == 0);

            if (jokers?.Count > 0)
            {
                if (jokers.Count == 5)
                {
                    // All jokers
                    hand.Type = HandType.FiveOfAKind;
                    return;
                }
                
                var highestNonJoker = valueFrequencies.OrderByDescending(x => x.Count).ThenByDescending(x => x.Value).First(x => x.Value != 0);
                highestNonJoker.Count += jokers.Count;
                
                valueFrequencies = valueFrequencies
                    .Where(x => x.Value != 0)
                    .OrderByDescending(x => x.Count)
                    .ThenByDescending(x => x.Value)
                    .ToList();
            }
        }

        hand.Type = valueFrequencies[0].Count switch
        {
            5 => HandType.FiveOfAKind,
            4 => HandType.FourOfAKind,
            3 when valueFrequencies[1].Count == 2 => HandType.FullHouse,
            3 => HandType.ThreeOfAKind,
            2 when valueFrequencies[1].Count == 2 => HandType.TwoPairs,
            2 => HandType.OnePair,
            _ => HandType.HighCard
        };
    }

    public string Part1(string input)
    {
        var hands = ParseInput(input);
        hands.ForEach(x => DetermineHandType(x));

        var total = 0;
        var rank = 0;

        var orderedHands = hands.OrderByDescending(x => x);

        foreach (var hand in orderedHands)
        {
            rank++;
            total += hand.Bet * rank;
        }

        return total.ToString();
    }

    public string Part2(string input)
    {
        var hands = ParseInput(input);
        hands.ForEach(h =>
        {
            h.Cards = h.Cards.Select(c => c == 11 ? 0 : c).ToList();
        });
        hands.ForEach(x => DetermineHandType(x, true));

        var total = 0;
        var rank = 0;

        var orderedHands = hands.OrderByDescending(x => x);

        foreach (var hand in orderedHands)
        {
            rank++;
            total += hand.Bet * rank;
        }

        return total.ToString();
    }
}