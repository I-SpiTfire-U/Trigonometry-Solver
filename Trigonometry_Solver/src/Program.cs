namespace Trigonometry_Solver;
internal class Program
{
    private const float DivideByPI = 57.2957795130823F;
    private const float DivideBy180 = 0.0174532925199433F;

    internal static void Main()
    {
        Console.Title = "Trigonometry Solver";
        while (true)
        {
            int triangleType = CreateMenu("Triangle Type:", ["Right Angle", "Isosceles"]);
            int optionSolvingFor = CreateMenu("Solving For:", ["Side", "Angle"]);

            double result = triangleType == 0
                ? RightAngle(optionSolvingFor, CreateMenu("Function:", ["Sine", "Cosine", "Tangent"]))
                : Isosceles(optionSolvingFor, CreateMenu("Function:", ["Sine Law", "Cosine Law"]));

            Console.Write("Result: ");
            WriteLineColor(ConsoleColor.DarkYellow, result);
            _ = Console.ReadKey();

            Console.Clear();
        }
    }

    private static double RightAngle(int optionSolvingFor, int function)
    {
        WriteLineColor(ConsoleColor.Magenta,
                       "+-----------+-----------------+---------------------------------------+---------------+\n" +
                       "| Sides:    | Angles:         | Finding Sides:                        | SOH = Opp/Hyp |\n" +
                       "| --------- | --------------- | ------------------------------------- | CAH = Adj/Hyp |\n" +
                       "| X = Angle | X = Top Side    | Reg = func(angle) * side  < [X/Value] | TOA = Opp/Adj |\n" +
                       "| Z = Side  | Z = Bottom Side | Inv = side / func(angle)  < [Value/X] |               |\n" +
                       "+-----------+-----------------+---------------------------------------+---------------+\n");

        float x = GetValidFloatInput("X");
        float z = GetValidFloatInput("Z");

        if (optionSolvingFor == 1)
        {
            return CalculateAngle(x, z, function) * DivideByPI;
        }
        
        string input = GetValidStringInput("Inverse [true/false]");
        if (input.Equals("true", StringComparison.CurrentCultureIgnoreCase))
        {
            function += 4;
        }
        return CalculateSide(ConvertToDegrees(x), z, function);
    }

    private static double Isosceles(int optionSolvingFor, int function)
    {
        WriteLineColor(ConsoleColor.Magenta, 
                       "+----------------+-----------------+------------------+-------------------+\n" +
                       "| Sine Law Side: | Sine Law Angle: | Cosine Law Side: | Cosine Law Angle: |\n" +
                       "| -------------- | --------------- | ---------------- | ----------------- |\n" +
                       "| X = Angle A    | X = Angle A     | X = Side A       | X = Side A        |\n" +
                       "| Y = Angle B    | Y = Side A      | Y = Side B       | Y = Side B        |\n" +
                       "| Z = Side A     | Z = Side B      | Z = Angle C      | Z = Side C        |\n" +
                       "+----------------+-----------------+------------------+-------------------+\n");

        float x = GetValidFloatInput("X");
        float y = GetValidFloatInput("Y");
        float z = GetValidFloatInput("Z");

        return function == 0
            ? SineLaw(x, y, z, optionSolvingFor)
            : CosineLaw(x, y, z, optionSolvingFor);
    }

    private static double SineLaw(float x, float y, float z, int optionSolvingFor)
    {
        return optionSolvingFor == 0
            ? Math.Sin(ConvertToDegrees(y)) * z / Math.Sin(ConvertToDegrees(x))
            : Math.Asin(Math.Sin(ConvertToDegrees(x)) / y * z) * DivideByPI;
    }

    private static double CosineLaw(float x, float y, float z, int optionSolvingFor)
    {
        return optionSolvingFor == 0
            ? Math.Sqrt((x * x) + (y * y) - (2 * x * y * Math.Cos(ConvertToDegrees(z))))
            : Math.Acos(((x * x) + (y * y) - (z * z)) / (2 * x * y)) * DivideByPI;
    }

    private static double CalculateSide(float angle, float side, int function)
    {
        return function switch
        {
            0 => side * Math.Sin(angle),
            1 => side * Math.Cos(angle),
            2 => side * Math.Tan(angle),
            4 => side / Math.Sin(angle),
            5 => side / Math.Cos(angle),
            _ => side / Math.Tan(angle),
        };
    }

    private static double CalculateAngle(float side1, float side2, int function)
    {
        return function switch
        {
            0 => Math.Asin(side1 / side2),
            1 => Math.Acos(side1 / side2),
            _ => Math.Atan(side1 / side2)
        };
    }

    private static float GetValidFloatInput(string prompt)
    {
        while (true)
        {
            string? input = GetPromptedInput(prompt);
            if (float.TryParse(input, out float result))
            {
                return result;
            }
            WriteLineColor(ConsoleColor.Red, "Invalid Decimal Input!");
        }
    }

    private static string GetValidStringInput(string prompt)
    {
        while (true)
        {
            string? input = GetPromptedInput(prompt);
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            WriteLineColor(ConsoleColor.Red, "Invalid Text Input!");
        }
    }

    private static string? GetPromptedInput(string prompt = "")
    {
        ConsoleColor defaultColor = Console.ForegroundColor;

        Console.Write($"{prompt} > ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        string? result = Console.ReadLine();
        Console.ForegroundColor = defaultColor;

        return result;
    }

    private static float ConvertToDegrees(float value)
    {
        return (float)(value * DivideBy180);
    }

    private static int CreateMenu(string title, string[] options)
    {
        while (true)
        {
            Console.WriteLine(title);
            for (int i = 0; i < options.Length; i++)
            {
                Console.Write($"[{i}] ");
                WriteLineColor(ConsoleColor.Green, options[i]);
            }

            string? input = GetPromptedInput();
            if (int.TryParse(input, out int index) && index >= 0 && index < options.Length)
            {
                Console.Clear();
                return index;
            }

            WriteLineColor(ConsoleColor.Red, "Invalid Option Selected!");
            _ = Console.ReadKey();
            Console.Clear();
        }
    }

    private static void WriteLineColor(ConsoleColor color, object? value)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(value);
        Console.ForegroundColor = defaultColor;
    }
}