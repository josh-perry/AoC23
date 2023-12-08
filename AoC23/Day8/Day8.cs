using System.Text.RegularExpressions;

namespace AoC23.Day8;

public class Day8 : IDay
{
    public int Day => 8;

    public enum Direction
    {
        L,
        R
    }

    public class Node
    {
        public string Name { get; set; }
        
        public string Left { get; set; }
        
        public string Right { get; set; }

        public override string ToString() => Name;
    }
    
    public class Network
    {
        public List<Direction> Directions { get; set; } = new();
        
        public Dictionary<string, Node> Nodes { get; set; } = new();
    }

    public Network ParseInput(string input)
    {
        var network = new Network();
        var nodeRegex = new Regex(@"(?<name>\w+) = \((?<left>\w+), (?<right>\w+)\)");
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            // List of directions
            if (!line.Contains('='))
            {
                foreach (var c in line)
                {
                    network.Directions.Add(c switch
                    {
                        'L' => Direction.L,
                        'R' => Direction.R,
                        _ => throw new ArgumentOutOfRangeException()
                    });
                }

                continue;
            }

            var match = nodeRegex.Match(line);
            
            var name = match.Groups["name"].Value;
            var left = match.Groups["left"].Value;
            var right = match.Groups["right"].Value;

            var node = new Node
            {
                Name = name,
                Left = left,
                Right = right
            };

            network.Nodes.TryAdd(node.Name, node);
        }

        return network;
    }

    public string Part1(string input)
    {
        var network = ParseInput(input);
        var index = 0;
        var currentNode = network.Nodes["AAA"];

        while (true)
        {
            var currentInstruction = network.Directions[index % network.Directions.Count];
            
            switch (currentInstruction)
            {
                case Direction.L:
                    currentNode = network.Nodes[currentNode.Left];
                    break;
                case Direction.R:
                    currentNode = network.Nodes[currentNode.Right];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (currentNode.Name == "ZZZ")
            {
                return (index + 1).ToString();
            }
            
            index++;
        }
    }

    public string Part2(string input)
    {
        return string.Empty;
    }
}