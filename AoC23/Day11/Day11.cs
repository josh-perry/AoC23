﻿namespace AoC23.Day11;

public class Day11 : IDay
{
    public int Day => 11;

    private List<List<char>> ParseInput(string input)
    {
        return input.Split(Environment.NewLine)
            .Select(line => line.ToCharArray().ToList())
            .ToList();
    }

    private void ExpandSpace(List<List<char>> map)
    {
        for (var x = 0; x < map[0].Count; x++)
        {
            if (!IsColumnEmpty(map, x))
                continue;
            
            foreach (var row in map)
            {
                row.Insert(x, '.');
            }

            x++;
        }
        
        for (var y = 0; y < map.Count; y++)
        {
            if (!IsRowEmpty(map, y))
                continue;
            
            map.Insert(y, new List<char>());
            for (var x = 0; x < map[0].Count; x++)
            {
                map[y].Add('.');
            }

            y++;
        }
    }

    private bool IsColumnEmpty(List<List<char> > map, int column)
    {
        return map.All(row => row[column] == '.');
    }
    
    private bool IsRowEmpty(List<List<char> > map, int row)
    {
        return map[row].All(c => c == '.');
    }
    
    private long GetManhattanDistance(int x1, int y1, int x2, int y2, int emptyRowMultiplier = 1, int emptyColumnMultiplier = 1, List<int>? emptyRows = null, List<int>? emptyColumns = null)
    {
        var rawDistance = Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        
        if (emptyRows is null || emptyColumns is null)
            return rawDistance;
        
        foreach (var emptyRow in emptyRows)
        {
            if (y1 <= emptyRow && y2 >= emptyRow)
            {
                rawDistance += emptyRowMultiplier - 1;
            }
            else if (y1 >= emptyRow && y2 <= emptyRow)
            {
                rawDistance += emptyRowMultiplier - 1;
            }
        }
        
        foreach (var emptyColumn in emptyColumns)
        {
            if (x1 <= emptyColumn && x2 >= emptyColumn)
            {
                rawDistance += emptyColumnMultiplier - 1;
            }
            else if (x1 >= emptyColumn && x2 <= emptyColumn)
            {
                rawDistance += emptyColumnMultiplier - 1;
            }
        }
        
        return rawDistance;
    }

    public string Part1(string input)
    {
        var map = ParseInput(input);
        ExpandSpace(map);

        var galaxies = new List<(int X, int Y)>();
        
        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] == '#')
                {
                    galaxies.Add((x, y));
                }
            }
        }

        var pairs = galaxies.SelectMany(
            (_, index) => galaxies.Skip(index + 1),
            (galaxy, galaxy2) => new { galaxy, galaxy2 }
        );

        var total = pairs.Sum(pair => GetManhattanDistance(pair.galaxy.X, pair.galaxy.Y, pair.galaxy2.X, pair.galaxy2.Y));
        return total.ToString();
    }

    public string Part2(string input)
    {
        var map = ParseInput(input);
        var galaxies = new List<(int X, int Y)>();
        
        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] == '#')
                {
                    galaxies.Add((x, y));
                }
            }
        }

        var pairs = galaxies.SelectMany(
            (_, index) => galaxies.Skip(index + 1),
            (galaxy, galaxy2) => new { galaxy, galaxy2 }
        );

        var emptyRows = new List<int>();
        for (var y = 0; y < map.Count; y++)
        {
            if (IsRowEmpty(map, y))
            {
                emptyRows.Add(y);
            }
        }

        var emptyColumns = new List<int>();
        for (var x = 0; x < map[0].Count; x++)
        {
            if (IsColumnEmpty(map, x))
            {
                emptyColumns.Add(x);
            }
        }
        
        var total = pairs.Sum(pair => GetManhattanDistance(pair.galaxy.X, pair.galaxy.Y, pair.galaxy2.X, pair.galaxy2.Y, 1000000, 1000000, emptyRows, emptyColumns));
        return total.ToString();

    }
}