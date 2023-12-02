using System.Reflection;

namespace AoC23.Test;

public class TestDay
{
    [Theory]
    [InlineData(1, "142")]
    public void should_give_expected_output_for_mini_input_part_1(int dayNumber, string expected)
    {
        // Arrange
        var day = Assembly.GetAssembly(typeof(IDay))
            ?.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
            .Select(t => (IDay) Activator.CreateInstance(t)!)
            .First(x => x.Day == dayNumber);
        
        // Act
        var result = day.Part1(File.ReadAllText($"Inputs\\day{dayNumber}_mini.txt"));
        
        // Assert
        Assert.Equal(expected, result);
    }
}