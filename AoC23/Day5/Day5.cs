using System.Text.RegularExpressions;

namespace AoC23.Day4;

public class Day5 : IDay
{
    public int Day => 5;

    private class Category
    {
        public string Name { get; set; }
        
        public List<(long DestinationRangeStart, long SourceRangeStart, long RangeLength)> Mappings { get; set; } = new();

        public Category? ChildCategory { get; set; }
    }

    private class Almanac
    {
        public List<long> Seeds { get; set; } = new();

        public Category? ChildCategory { get; set; }
    }
    
    private Almanac ParseInput(string input)
    {
        var almanac = new Almanac();
        var categoryRegex = new Regex(@"(?<source>\w+)-to-(?<destination>\w+) map:");
        var mappingRegex = new Regex(@"(?<destinationStart>\d+) (?<sourceStart>\d+) (?<length>\d+)");
        
        Category? currentCategory = null;
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            
            if (line.StartsWith("seeds:"))
            {
                var seeds = line.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
                almanac.Seeds.AddRange(seeds);
                continue;
            }
            
            var categoryMatch = categoryRegex.Match(line);
            if (!categoryMatch.Success)
            {
                var mappingMatch = mappingRegex.Match(line);
                currentCategory.Mappings.Add((
                    long.Parse(mappingMatch.Groups["destinationStart"].Value),
                    long.Parse(mappingMatch.Groups["sourceStart"].Value),
                    long.Parse(mappingMatch.Groups["length"].Value)));
                continue;
            }

            if (currentCategory is null)
            {
                currentCategory = new();
                almanac.ChildCategory = currentCategory;
            }
            else
            {
                currentCategory.ChildCategory = new();
                currentCategory = currentCategory.ChildCategory;
            }
            
            var destination = categoryMatch.Groups["destination"].Value;
            currentCategory.Name = destination;
        }

        return almanac;
    }

    private List<long> GetLocationsForSeeds(Almanac almanac)
    {
        var locations = new List<long>();
        
        foreach (var seed in almanac.Seeds)
        {
            var sourceValue = seed;
            var category = almanac.ChildCategory;
            
            while (category is not null)
            {
                var v = category
                    .Mappings
                    .FirstOrDefault(x => sourceValue >= x.SourceRangeStart && sourceValue <= x.SourceRangeStart + x.RangeLength);
                
                sourceValue = v.Equals(default) ? sourceValue : v.DestinationRangeStart + (sourceValue - v.SourceRangeStart);
                category = category.ChildCategory;
            }
            
            locations.Add(sourceValue);
        }

        return locations;
    }
    
    public string Part1(string input)
    {
        var almanac = ParseInput(input);
        var locations = GetLocationsForSeeds(almanac);

        return locations.Min().ToString();
    }

    public string Part2(string input)
    {
        return string.Empty;
    }
}