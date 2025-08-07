namespace Trigonometry_Solver;

public static class Color
{
    public static void WriteLineColor(ConsoleColor color, object? value)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;

        Console.ForegroundColor = color;
        Console.WriteLine(value);
        Console.ForegroundColor = defaultColor;
    }

    public static string? ReadLineColor(ConsoleColor color)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;

        Console.ForegroundColor = color;
        string? result = Console.ReadLine();
        Console.ForegroundColor = defaultColor;

        return result;
    }

    public static char ReadKeyColor(ConsoleColor color)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;

        Console.ForegroundColor = color;
        char result = Console.ReadKey().KeyChar;
        Console.ForegroundColor = defaultColor;

        return result;
    }
}