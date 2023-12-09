using System.Reflection;

namespace AoC23.Test;

public class TestDay
{
    [Theory]
    [InlineData(1, "142")]
    [InlineData(2, "8")]
    [InlineData(3, "4361")]
    [InlineData(4, "13")]
    [InlineData(5, "35")]
    [InlineData(6, "288")]
    [InlineData(7, "6440")]
    [InlineData(8, "2")]
    [InlineData(8, "6", "mini_part1_2")]
    [InlineData(9, "114")]
    public void should_give_expected_output_for_mini_input_part_1(int dayNumber, string expected, string inputFileName = "mini_part1")
    {
        // Arrange
        var day = Assembly.GetAssembly(typeof(IDay))
            ?.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
            .Select(t => (IDay) Activator.CreateInstance(t)!)
            .First(x => x.Day == dayNumber);
        
        // Act
        var result = day.Part1(File.ReadAllText($"Inputs\\day{dayNumber}_{inputFileName}.txt"));
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(1, "281")]
    [InlineData(2, "2286")]
    [InlineData(3, "467835")]
    [InlineData(4, "30")]
    [InlineData(6, "71503")]
    [InlineData(7, "5905")]
    [InlineData(8, "6")]
    public void should_give_expected_output_for_mini_input_part_2(int dayNumber, string expected)
    {
        // Arrange
        var day = Assembly.GetAssembly(typeof(IDay))
            ?.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
            .Select(t => (IDay) Activator.CreateInstance(t)!)
            .First(x => x.Day == dayNumber);
        
        // Act
        var result = day.Part2(File.ReadAllText($"Inputs\\day{dayNumber}_mini_part2.txt"));
        
        // Assert
        Assert.Equal(expected, result);
    }
}