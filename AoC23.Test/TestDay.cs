using System.Reflection;

namespace AoC23.Test;

public class TestDay
{
    [Theory]
    [InlineData(1, "142")]
    [InlineData(2, "8")]
    public void should_give_expected_output_for_mini_input_part_1(int dayNumber, string expected)
    {
        // Arrange
        var day = Assembly.GetAssembly(typeof(IDay))
            ?.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
            .Select(t => (IDay) Activator.CreateInstance(t)!)
            .First(x => x.Day == dayNumber);
        
        // Act
        var result = day.Part1(File.ReadAllText($"Inputs\\day{dayNumber}_mini_part1.txt"));
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(1, "281")]
    [InlineData(2, "2286")]
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