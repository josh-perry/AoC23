namespace AoC23.Day3;

public class Day3 : IDay
{
    public int Day => 3;
    
    private List<List<char>> BuildMap(string input)
    {
        return input.Split(Environment.NewLine).Select(line => line.ToList()).ToList();
    }
    
    private List<List<char>> Map { get; set; } = new();
    
    private HashSet<(int X, int Y)> NumberStartingIndexes { get; set; } = new();
    
    private char GetCharacterAtPosition(int x, int y) => Map[y][x];
    
    private bool IsPositionOutOfBounds(int x, int y) => x < 0 || y < 0 || x >= Map[0].Count || y >= Map.Count;

    private int? GetCompleteNumber(int x, int y)
    {
        var c = GetCharacterAtPosition(x, y);
        
        while (char.IsDigit(c))
        {
            x--;

            if (IsPositionOutOfBounds(x, y))
            {
                break;
            }
            
            c = GetCharacterAtPosition(x, y);
        }

        x++;
        
        if (NumberStartingIndexes.Contains((x, y)))
        {
            return null;
        }
        
        NumberStartingIndexes.Add((x, y));

        var numberString = string.Empty;
        while (true)
        {
            
            if (IsPositionOutOfBounds(x, y))
            {
                break;
            }
            
            c = GetCharacterAtPosition(x, y);

            if (!char.IsDigit(c))
            {
                break;
            }
            
            numberString += c;
            x++;
        }
        
        return int.Parse(numberString);
    }

    private List<int> GetNeighbouringNumbers(List<List<char>> map, int x, int y)
    {
        var neighbourOffsets = new (int X, int Y)[]
        {
            (0, -1),
            (-1, 0),
            (1, 0),
            (0, 1),
            (-1, -1),
            (1, -1),
            (-1, 1),
            (1, 1)
        };
        
        var neighbouringNumbers = new List<int>();
        
        foreach (var neighbourOffset in neighbourOffsets)
        {
            if (IsPositionOutOfBounds(x + neighbourOffset.X, y + neighbourOffset.Y))
            {
                continue;
            }
            
            var c = GetCharacterAtPosition(x + neighbourOffset.X, y + neighbourOffset.Y);
            if (!char.IsDigit(c))
            {
                continue;
            }
            
            var number = GetCompleteNumber(x + neighbourOffset.X, y + neighbourOffset.Y);

            if (number.HasValue)
            {
                neighbouringNumbers.Add(number.Value);
            }
        }

        return neighbouringNumbers;
    }

    public string Part1(string input)
    {
        Map = BuildMap(input);
        NumberStartingIndexes = new();
        var sum = 0;
        
        for(var y = 0; y < Map.Count; y++)
        {
            for(var x = 0; x < Map[y].Count; x++)
            {
                var character = GetCharacterAtPosition(x, y);
                
                if (character == '.' || char.IsDigit(character))
                {
                    continue;
                }

                var neighbouringNumbers = GetNeighbouringNumbers(Map, x, y);
                sum += neighbouringNumbers.Sum();
            }
        }
        
        return sum.ToString();
    }

    public string Part2(string input)
    {
        Map = BuildMap(input);
        NumberStartingIndexes = new();
        var sum = 0;
        
        for(var y = 0; y < Map.Count; y++)
        {
            for(var x = 0; x < Map[y].Count; x++)
            {
                var character = GetCharacterAtPosition(x, y);

                if (character != '*')
                {
                    continue;
                }

                var neighbouringNumbers = GetNeighbouringNumbers(Map, x, y);
                if (neighbouringNumbers.Count > 1)
                {
                    sum += neighbouringNumbers.Aggregate((a, b) => a * b);
                }
            }
        }
        
        return sum.ToString();
    }
}