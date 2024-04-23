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
                Console.Clear();

                double result;
                byte type = Menu("Triangle Type:", "Right Angle", "Isosceles");
                byte part = Menu("Solving For:", "Side", "Angle");

                result = type == 0 ? RightAngle(part, Menu("Function:", "Sine", "Cosine", "Tangent")) : Isosceles(part, Menu("Function:", "Sine Law", "Cosine Law"));

                Console.Write("Result: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(result);
                Console.ForegroundColor = ConsoleColor.White;
                _ = Console.ReadKey();
            }
        }

        private static double RightAngle(byte part, byte function)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("+-----------+-----------------+---------------------------------------+---------------+\n" +
                              "| Sides:    | Angles:         | Finding Sides:                        | SOH = Opp/Hyp |\n" +
                              "| --------- | --------------- | ------------------------------------- | CAH = Adj/Hyp |\n" +
                              "| X = Angle | X = Top Side    | Reg = func(angle) * side  < [X/Value] | TOA = Opp/Adj |\n" +
                              "| Z = Side  | Z = Bottom Side | Inv = side / func(angle)  < [Value/X] |               |\n" +
                              "+-----------+-----------------+---------------------------------------+---------------+\n");
            Console.ForegroundColor = ConsoleColor.White;

            float x = GetValidFloat("X");
            float z = GetValidFloat("Z");

            if (part == 0)
            {
                Console.Write("Inverse [true/false] > ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (Console.ReadLine() == "true")
                {
                    function += 4;
                }
                Console.ForegroundColor = ConsoleColor.White;
                return CalcSideRightAngle(ConvertToDegrees(x), z, function);
            }
            return CalcAngleRightAngle(x, z, function) * DivideByPI;
        }

        private static double Isosceles(byte part, byte function)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("+----------------+-----------------+------------------+-------------------+\n" +
                              "| Sine Law Side: | Sine Law Angle: | Cosine Law Side: | Cosine Law Angle: |\n" +
                              "| -------------- | --------------- | ---------------- | ----------------- |\n" +
                              "| X = Angle A    | X = Angle A     | X = Side A       | X = Side A        |\n" +
                              "| Y = Angle B    | Y = Side A      | Y = Side B       | Y = Side B        |\n" +
                              "| Z = Side A     | Z = Side B      | Z = Angle C      | Z = Side C        |\n" +
                              "+----------------+-----------------+------------------+-------------------+\n");
            Console.ForegroundColor = ConsoleColor.White;

            float x = GetValidFloat("X");
            float y = GetValidFloat("Y");
            float z = GetValidFloat("Z");

            return part == 0 ? CalcSideIsosceles(x, y, z, function) : CalcAngleIsosceles(x, y, z, function);
        }

        private static double CalcSideIsosceles(float x, float y, float z, byte function)
        {
            if (function == 0)
            {
                x = ConvertToDegrees(x);
                y = ConvertToDegrees(y);
                return Math.Sin(y) * z / Math.Sin(x);
            }
            z = ConvertToDegrees(z);
            return Math.Sqrt((x * x) + (y * y) - (2 * x * y * Math.Cos(z)));
        }

        private static float ConvertToDegrees(float value)
        {
            return (float)(value * DivideBy180);
        }

        private static double CalcAngleIsosceles(float x, float y, float z, byte function)
        {
            if (function == 0)
            {
                x = ConvertToDegrees(x);
                return Math.Asin(Math.Sin(x) / y * z) * DivideByPI;
            }
            return Math.Acos(((x * x) + (y * y) - (z * z)) / (2 * x * y)) * DivideByPI;
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Input!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static double CalcSideRightAngle(float angle, float side, byte function)
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

        private static double CalcAngleRightAngle(float side1, float side2, byte function)
        {
            return function == 0 ? Math.Asin(side1 / side2) : function == 1 ? Math.Acos(side1 / side2) : Math.Atan(side1 / side2);
        }

        private static byte Menu(string title, params string[] options)
        {
            byte length = (byte)options.Length;

            while (true)
            {
                Console.WriteLine(title);
                for (int i = 0; i < length; i++)
                {
                    Console.Write($"[{i}] ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(options[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("> ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (byte.TryParse(Console.ReadLine(), out byte index) && index >= 0 && index < length)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return index;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option selected!");
                Console.ForegroundColor = ConsoleColor.White;
                _ = Console.ReadKey();
                Console.Clear();
            }
        }
    }
}