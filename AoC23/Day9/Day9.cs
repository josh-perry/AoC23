namespace AoC23.Day9;

public class Day9 : IDay
{
    public int Day => 9;

    private class Report
    {
        public List<List<int>> History { get; set; } = new();
    }
    
    private Report ParseInput(string input)
    {
        var report = new Report();
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            var values = line.Split(' ').Select(int.Parse).ToList();
            report.History.Add(values);
        }

        return report;
    }
    
    private void GetDifferences(List<int> history, List<List<int>> allDifferences)
    {
        var differences = new List<int>();

        for (var index = 0; index < history.Count - 1; index++)
        {
            var value = history[index];
            var nextValue = history[index + 1];

            differences.Add(nextValue - value);
        }
        
        allDifferences.Add(differences);
        if (differences.All(x => x == 0))
        {
            return;
        }
        
        GetDifferences(differences, allDifferences);
    }

    private int PredictNextValue(List<int> history)
    {
        var differences = new List<List<int>>();
        
        GetDifferences(history, differences);
        differences.Insert(0, history);

        for (var index = differences.Count - 1; index >= 0; index--)
        {
            var difference = differences[index];
            
            if (index == differences.Count - 1)
            {
                difference.Add(0);
                continue;
            }

            var lastValue = difference.Last();
            difference.Add(lastValue + differences[index + 1].Last());
        }

        return differences.First().Last();
    }
    
    private int PredictLastValue(List<int> history)
    {
        var differences = new List<List<int>>();
        
        GetDifferences(history, differences);
        differences.Insert(0, history);

        for (var index = differences.Count - 1; index >= 0; index--)
        {
            var difference = differences[index];
            
            if (index == differences.Count - 1)
            {
                difference.Insert(0, 0);
                continue;
            }

            var firstValue = difference.First();
            difference.Insert(0, firstValue - differences[index + 1].First());
        }

        return differences.First().First();
    }
    
    public string Part1(string input)
    {
        var report = ParseInput(input);
        var sum = report.History.Sum(PredictNextValue);

        return sum.ToString();
    }
    
    public string Part2(string input)
    {
        var report = ParseInput(input);
        var sum = report.History.Sum(PredictLastValue);

        return sum.ToString();
    }
}