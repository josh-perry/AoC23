using System.Text.RegularExpressions;

namespace AoC23.Day2;

public class Day2 : IDay
{
    public int Day => 2;

    private class RevealedSet
    {
        public int Red { get; set; }

        public int Green { get; set; }
        
        public int Blue { get; set; }
    }

    private class Game
    {
        public int Id { get; set; }
        
        public List<RevealedSet> RevealedSets { get; set; }
    }

    private List<Game> LoadGames(string input)
    {
        var games = new List<Game>();
        var gameRegex = new Regex(@"Game (?<id>\d+): (?<games>.*)$");
        var setRegex = new Regex(@"(?<red>\d+) red|(?<green>\d+) green|(?<blue>\d+) blue");

        foreach (var line in input.Split(Environment.NewLine))
        {
            var game = new Game();
            
            var lineMatch = gameRegex.Match(line);
            game.Id = int.Parse(lineMatch.Groups["id"].Value);
            game.RevealedSets = new List<RevealedSet>();
            
            var revealedSets = lineMatch.Groups["games"].Value.Split(";");

            foreach (var set in revealedSets)
            {
                var matches = setRegex.Matches(set);

                var red = 0;
                var green = 0;
                var blue = 0;
                
                foreach (Match setMatch in matches)
                {
                    red = setMatch.Groups["red"].Success ? int.Parse(setMatch.Groups["red"].Value) : red;
                    green = setMatch.Groups["green"].Success ? int.Parse(setMatch.Groups["green"].Value) : green;
                    blue = setMatch.Groups["blue"].Success ? int.Parse(setMatch.Groups["blue"].Value) : blue;
                }
                
                game.RevealedSets.Add(new()
                {
                    Red = red,
                    Green = green,
                    Blue = blue
                });
            }
            
            games.Add(game);
        }

        return games;
    }

    public string Part1(string input)
    {
        var games = LoadGames(input);

        var requiredRed = 12;
        var requiredGreen = 13;
        var requiredBlue = 14;

        var sumOfIds = 0;

        foreach (var game in games)
        {
            var gamePossible = true;
            
            foreach (var set in game.RevealedSets)
            {
                gamePossible = set.Red <= requiredRed && set.Blue <= requiredBlue && set.Green <= requiredGreen;
                
                if (!gamePossible)
                {
                    break;
                }
            }

            if (gamePossible)
            {
                sumOfIds += game.Id;
            }
        }
        
        return sumOfIds.ToString();
    }

    public string Part2(string input)
    {
        var games = LoadGames(input);

        var sum = 0;
        
        foreach (var game in games)
        {
            var minRedNeeded = game.RevealedSets.Max(x => x.Red);
            var minGreenNeeded = game.RevealedSets.Max(x => x.Green);
            var minBlueNeeded = game.RevealedSets.Max(x => x.Blue);

            sum += minRedNeeded * minGreenNeeded * minBlueNeeded;
        }

        return sum.ToString();
    }
}