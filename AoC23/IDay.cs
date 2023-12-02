namespace AoC23;

public interface IDay
{
    int Day { get; }
    
    string Part1(string input);

    string Part2(string input);
}