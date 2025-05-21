namespace Trigonometry_Solver
{
    internal class Program
    {
        private const float DivideByPI = 57.2957795130823F;
        private const float DivideBy180 = 0.0174532925199433F;

        internal static void Main()
        {
            Console.Title = "Trigonometry Solver";
            while (true)
            {
                double result;
                bool isRightAngle = Convert.ToBoolean(CreateMenu("Triangle Type:", "Right Angle", "Isosceles"));
                bool isSolvingSide = Convert.ToBoolean(CreateMenu("Solving For:", "Side", "Angle"));

                result = isRightAngle == false ? RightAngle(isSolvingSide, CreateMenu("Function:", "Sine", "Cosine", "Tangent")) : Isosceles(isSolvingSide, CreateMenu("Function:", "Sine Law", "Cosine Law"));

                Console.Write("Result: ");
                WriteLineColor(ConsoleColor.DarkYellow, result);
                _ = Console.ReadKey();
                Console.Clear();
            }
        }

        private static double RightAngle(bool isSolvingSide, byte function)
        {
            WriteLineColor(ConsoleColor.Magenta, "+-----------+-----------------+---------------------------------------+---------------+\n" +
                                                 "| Sides:    | Angles:         | Finding Sides:                        | SOH = Opp/Hyp |\n" +
                                                 "| --------- | --------------- | ------------------------------------- | CAH = Adj/Hyp |\n" +
                                                 "| X = Angle | X = Top Side    | Reg = func(angle) * side  < [X/Value] | TOA = Opp/Adj |\n" +
                                                 "| Z = Side  | Z = Bottom Side | Inv = side / func(angle)  < [Value/X] |               |\n" +
                                                 "+-----------+-----------------+---------------------------------------+---------------+\n");

            float x = GetValidFloat("X");
            float z = GetValidFloat("Z");

            if (isSolvingSide == false)
            {
                Console.Write("Inverse [true/false] > ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (Console.ReadLine() == "true")
                {
                    function += 4;
                }
                Console.ForegroundColor = ConsoleColor.White;
                return CalculateSide(ConvertToDegrees(x), z, function);
            }
            return CalculateAngle(x, z, function) * DivideByPI;
        }

        private static double Isosceles(bool isSolvingSide, byte function)
        {
            WriteLineColor(ConsoleColor.Magenta, "+----------------+-----------------+------------------+-------------------+\n" +
                                                 "| Sine Law Side: | Sine Law Angle: | Cosine Law Side: | Cosine Law Angle: |\n" +
                                                 "| -------------- | --------------- | ---------------- | ----------------- |\n" +
                                                 "| X = Angle A    | X = Angle A     | X = Side A       | X = Side A        |\n" +
                                                 "| Y = Angle B    | Y = Side A      | Y = Side B       | Y = Side B        |\n" +
                                                 "| Z = Side A     | Z = Side B      | Z = Angle C      | Z = Side C        |\n" +
                                                 "+----------------+-----------------+------------------+-------------------+\n");

            float x = GetValidFloat("X");
            float y = GetValidFloat("Y");
            float z = GetValidFloat("Z");

            return function == 0 ? SineLaw(x, y, z, isSolvingSide) : CosineLaw(x, y, z, isSolvingSide);
        }

        private static double SineLaw(float x, float y, float z, bool isSolvingSide)
        {
            return isSolvingSide == false
                ? Math.Sin(ConvertToDegrees(y)) * z / Math.Sin(ConvertToDegrees(x))
                : Math.Asin(Math.Sin(ConvertToDegrees(x)) / y * z) * DivideByPI;
        }

        private static double CosineLaw(float x, float y, float z, bool isSolvingSide)
        {
            return isSolvingSide == false
                ? Math.Sqrt((x * x) + (y * y) - (2 * x * y * Math.Cos(ConvertToDegrees(z))))
                : Math.Acos(((x * x) + (y * y) - (z * z)) / (2 * x * y)) * DivideByPI;
        }

        private static double CalculateSide(float angle, float side, byte function)
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

        private static double CalculateAngle(float side1, float side2, byte function)
        {
            return function == 0 ? Math.Asin(side1 / side2) : function == 1 ? Math.Acos(side1 / side2) : Math.Atan(side1 / side2);
        }

        private static float GetValidFloat(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} > ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (float.TryParse(Console.ReadLine(), out float value))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    return value;
                }
                WriteLineColor(ConsoleColor.Red, "Invalid Input!");
            }
        }

        private static float ConvertToDegrees(float value)
        {
            return (float)(value * DivideBy180);
        }

        private static byte CreateMenu(string title, params string[] options)
        {
            byte length = (byte)options.Length;

            while (true)
            {
                Console.WriteLine(title);
                for (int i = 0; i < length; i++)
                {
                    Console.Write($"[{i}] ");
                    WriteLineColor(ConsoleColor.Green, options[i]);
                }

                Console.Write("> ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (byte.TryParse(Console.ReadLine(), out byte index) && index >= 0 && index < length)
                {
                    Console.ForegroundColor = ConsoleColor.White;
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
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}