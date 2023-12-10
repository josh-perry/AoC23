namespace AoC23.Day10;

public class Day10 : IDay
{
    public int Day => 10;
    
    private List<List<char>> ParseInput(string input)
    {
        return input.Split(Environment.NewLine).Select(x => x.ToCharArray().ToList()).ToList();
    }
    
    private char? GetChar(List<List<char>> map, (int X, int Y) position)
    {
        var x = position.X;
        var y = position.Y;
        
        if (x < 0 || y < 0 || y >= map.Count || x >= map[y].Count)
        {
            return null;
        }

        return map[y][x];
    }

    private (int X, int Y) FindStartingPosition(List<List<char>> map)
    {
        var startX = 0;
        var startY = 0;
        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] != 'S')
                    continue;
                
                startX = x;
                startY = y;
            }
        }

        return (startX, startY);
    }

    private void PopulateDepthMap(List<List<char>> map, (int X, int Y) startingPosition, List<List<int>> depthMap)
    {
        var queue = new Queue<((int X, int Y) Position, int Depth)>();
        queue.Enqueue((startingPosition, 0));

        while (queue.Count > 0)
        {
            var (currentPosition, depth) = queue.Dequeue();
            var current = GetChar(map, currentPosition);

            if (current == '.')
            {
                continue;
            }

            if (currentPosition.X < 0 || currentPosition.X >= map[0].Count || currentPosition.Y < 0 || currentPosition.Y >= map.Count)
            {
                continue;
            }

            if (depthMap[currentPosition.Y][currentPosition.X] != int.MinValue)
            {
                depthMap[currentPosition.Y][currentPosition.X] = Math.Min(depthMap[currentPosition.Y][currentPosition.X], depth);
                continue;
            }

            depthMap[currentPosition.Y][currentPosition.X] = depth;
            
            switch (current)
            {
                case 'J':
                    queue.Enqueue(((currentPosition.X, currentPosition.Y - 1), depth + 1));
                    queue.Enqueue(((currentPosition.X - 1, currentPosition.Y), depth + 1));
                    break;
                case 'L':
                    queue.Enqueue(((currentPosition.X, currentPosition.Y - 1), depth + 1));
                    queue.Enqueue(((currentPosition.X + 1, currentPosition.Y), depth + 1));
                    break;
                case '7':
                    queue.Enqueue(((currentPosition.X, currentPosition.Y + 1), depth + 1));
                    queue.Enqueue(((currentPosition.X - 1, currentPosition.Y), depth + 1));
                    break;
                case 'F':
                    queue.Enqueue(((currentPosition.X + 1, currentPosition.Y), depth + 1));
                    queue.Enqueue(((currentPosition.X, currentPosition.Y + 1), depth + 1));
                    break;
                case '|':
                    queue.Enqueue(((currentPosition.X, currentPosition.Y - 1), depth + 1));
                    queue.Enqueue(((currentPosition.X, currentPosition.Y + 1), depth + 1));
                    break;
                case '-':
                    queue.Enqueue(((currentPosition.X + 1, currentPosition.Y), depth + 1));
                    queue.Enqueue(((currentPosition.X - 1, currentPosition.Y), depth + 1));
                    break;
                case 'S':
                    var left = GetChar(map, (currentPosition.X - 1, currentPosition.Y));
                    if (left is '-' or 'L' or 'F')
                    {
                        queue.Enqueue(((currentPosition.X - 1, currentPosition.Y), depth + 1));
                    }
                    
                    var right = GetChar(map, (currentPosition.X + 1, currentPosition.Y));
                    if (right is '-' or 'J' or '7')
                    {
                        queue.Enqueue(((currentPosition.X + 1, currentPosition.Y), depth + 1));
                    }
                    
                    var up = GetChar(map, (currentPosition.X, currentPosition.Y - 1));
                    if (up is '|' or '7' or 'F')
                    {
                        queue.Enqueue(((currentPosition.X, currentPosition.Y - 1), depth + 1));
                    }
                    
                    var down = GetChar(map, (currentPosition.X, currentPosition.Y + 1));
                    if (down is '|' or 'L' or 'J')
                    {
                        queue.Enqueue(((currentPosition.X, currentPosition.Y + 1), depth + 1));
                    }
                    
                    break;
            }
        }
    }

    public string Part1(string input)
    {
        var map = ParseInput(input);
        var position = FindStartingPosition(map);
        var depthMap = new List<List<int>>();
        
        for (var y = 0; y < map.Count; y++)
        {
            depthMap.Add(new List<int>());
            for (var x = 0; x < map[y].Count; x++)
            {
                depthMap[y].Add(int.MinValue);
            }
        }
        
        PopulateDepthMap(map, position, depthMap);
        
        var max = int.MinValue;
        for (var y = 0; y < depthMap.Count; y++)
        {
            for (var x = 0; x < depthMap[y].Count; x++)
            {
                if (depthMap[y][x] == int.MinValue)
                    continue;

                if (depthMap[y][x] > max)
                    max = depthMap[y][x];
            }
        }
        
        return max.ToString();
    }

    public string Part2(string input)
    {
        return string.Empty;
    }
}