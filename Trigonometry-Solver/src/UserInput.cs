namespace Trigonometry_Solver;

public static class UserInput
{
    public static bool GetValidBooleanInput(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} [Y/N] > ");
            char input = Color.ReadKeyColor(ConsoleColor.Cyan);
            input = char.ToLower(input);
            Console.WriteLine();
            if (input == 'y' || input == 'n')
            {
                return input == 'y';
            }
            Color.WriteLineColor(ConsoleColor.Red, "Input must be either 'y' or 'n'.");
        }
    }

    public static string GetValidStringInput(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} > ");
            string? input = Color.ReadLineColor(ConsoleColor.Cyan);
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            Color.WriteLineColor(ConsoleColor.Red, "Input can not be null or blank.");
        }
    }

    public static float GetValidFloatInput(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} > ");
            string? input = Color.ReadLineColor(ConsoleColor.Cyan);
            if (string.IsNullOrWhiteSpace(input))
            {
                Color.WriteLineColor(ConsoleColor.Red, "Input can not be null or blank.");
                continue;
            }
            if (float.TryParse(input, out float result))
            {
                return result;
            }
            Color.WriteLineColor(ConsoleColor.Red, "Input must be a valid decimal number.");
        }
    }

    public static int GetMenuInput(string title, string[] options)
    {
        Console.WriteLine(title);
        PrintMenuOptions(options);

        while (true)
        {
            Console.Write($"> ");
            string? input = Color.ReadLineColor(ConsoleColor.Cyan);
            if (string.IsNullOrWhiteSpace(input))
            {
                Color.WriteLineColor(ConsoleColor.Red, "Input can not be null or blank.");
                continue;
            }
            if (!int.TryParse(input, out int index))
            {
                Color.WriteLineColor(ConsoleColor.Red, "Input must be a valid number.");
                continue;
            }
            if (index >= 0 && index < options.Length)
            {
                Console.Clear();
                return index;
            }

            Color.WriteLineColor(ConsoleColor.Red, $"Selected option must be between '0' and '{options.Length}'.");
        }
    }

    private static void PrintMenuOptions(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            Console.Write($"[{i}] ");
            Color.WriteLineColor(ConsoleColor.Green, options[i]);
        }
    }
}