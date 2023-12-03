namespace AoC23.Day3;

public class Day3 : IDay
{
    public int Day => 3;
    
    private List<List<char>> BuildTwoDimensionalList(string input)
    {
        return input.Split(Environment.NewLine).Select(line => line.ToList()).ToList();
    }

    private HashSet<(int X, int Y)> NumberStartingIndexes { get; set; } = new();
    
    private char GetCharacterAtPosition(List<List<char>> twoDimensionalList, int x, int y) => twoDimensionalList[y][x];
    
    private bool IsPositionOutOfBounds(List<List<char>> twoDimensionalList, int x, int y) => x < 0 || y < 0 || x >= twoDimensionalList[0].Count || y >= twoDimensionalList.Count;

    private int? GetCompleteNumber(List<List<char>> twoDimensionalList, int x, int y)
    {
        var c = GetCharacterAtPosition(twoDimensionalList, x, y);
        
        while (char.IsDigit(c))
        {
            x--;

            if (IsPositionOutOfBounds(twoDimensionalList, x, y))
            {
                break;
            }
            
            c = GetCharacterAtPosition(twoDimensionalList, x, y);
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
            
            if (IsPositionOutOfBounds(twoDimensionalList, x, y))
            {
                break;
            }
            
            c = GetCharacterAtPosition(twoDimensionalList, x, y);

            if (!char.IsDigit(c))
            {
                break;
            }
            
            numberString += c;
            x++;
        }
        
        return int.Parse(numberString);
    }

    private List<int> GetNeighbouringNumbers(List<List<char>> twoDimensionalList, int x, int y)
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
            if (IsPositionOutOfBounds(twoDimensionalList, x + neighbourOffset.X, y + neighbourOffset.Y))
            {
                continue;
            }
            
            var c = GetCharacterAtPosition(twoDimensionalList, x + neighbourOffset.X, y + neighbourOffset.Y);
            if (!char.IsDigit(c))
            {
                continue;
            }
            
            var number = GetCompleteNumber(twoDimensionalList, x + neighbourOffset.X, y + neighbourOffset.Y);
            neighbouringNumbers.Add(number ?? 0);
        }

        return neighbouringNumbers;
    }

    public string Part1(string input)
    {
        var twoDimensionalList = BuildTwoDimensionalList(input);
        var sum = 0;
        
        for(var y = 0; y < twoDimensionalList.Count; y++)
        {
            for(var x = 0; x < twoDimensionalList[y].Count; x++)
            {
                var character = GetCharacterAtPosition(twoDimensionalList, x, y);
                
                if (character == '.' || char.IsDigit(character))
                {
                    continue;
                }

                var neighbouringNumbers = GetNeighbouringNumbers(twoDimensionalList, x, y);
                sum += neighbouringNumbers.Sum();
            }
        }
        
        return sum.ToString();
    }

    public string Part2(string input)
    {
        return string.Empty;
    }
}