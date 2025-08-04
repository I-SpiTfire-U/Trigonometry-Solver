namespace Trigonometry_Solver;

public class TrigonometrySolver
{
    private const float DivideByPI = 57.2957795130823f;
    private const float DivideBy180 = 0.0174532925199433f;

    public static void Run()
    {
        while (true)
        {
            TriangleType triangleType = (TriangleType)UserInput.GetMenuInput("Triangle Type:", ["Right Angle", "Isosceles"]);
            SolvingOption solvingOption = (SolvingOption)UserInput.GetMenuInput("Solving For:", ["Side", "Angle"]);

            double result;
            switch (triangleType)
            {
                case TriangleType.RightAngle:
                    PrintRightAngleTriangleHint();
                    result = SolveRightAngleTriangle(solvingOption, (TrigFunction)UserInput.GetMenuInput("Function:", ["Sine", "Cosine", "Tangent"]));
                    break;

                case TriangleType.Isosceles:
                    PrintIsoscelesTriangleHint();
                    result = SolveIsoscelesTriangle(solvingOption, (TrigLawFunction)UserInput.GetMenuInput("Function:", ["Sine Law", "Cosine Law"]));
                    break;

                default:
                    result = 0;
                    break;
            }

            Console.Write("Result: ");
            Color.WriteLineColor(ConsoleColor.DarkYellow, result);
            _ = Console.ReadKey();

            Console.Clear();
        }
    }

    public static void PrintRightAngleTriangleHint()
    {
        Color.WriteLineColor(ConsoleColor.Magenta,
                       "+-----------+-----------------+---------------------------------------+---------------+\n" +
                       "| Sides:    | Angles:         | Finding Sides:                        | SOH = Opp/Hyp |\n" +
                       "| --------- | --------------- | ------------------------------------- | CAH = Adj/Hyp |\n" +
                       "| X = Angle | X = Top Side    | Reg = func(angle) * side  < [X/Value] | TOA = Opp/Adj |\n" +
                       "| Z = Side  | Z = Bottom Side | Inv = side / func(angle)  < [Value/X] |               |\n" +
                       "+-----------+-----------------+---------------------------------------+---------------+\n");
    }

    public static void PrintIsoscelesTriangleHint()
    {
        Color.WriteLineColor(ConsoleColor.Magenta,
                       "+----------------+-----------------+------------------+-------------------+\n" +
                       "| Sine Law Side: | Sine Law Angle: | Cosine Law Side: | Cosine Law Angle: |\n" +
                       "| -------------- | --------------- | ---------------- | ----------------- |\n" +
                       "| X = Angle A    | X = Angle A     | X = Side A       | X = Side A        |\n" +
                       "| Y = Angle B    | Y = Side A      | Y = Side B       | Y = Side B        |\n" +
                       "| Z = Side A     | Z = Side B      | Z = Angle C      | Z = Side C        |\n" +
                       "+----------------+-----------------+------------------+-------------------+\n");
    }

    private static double SolveRightAngleTriangle(SolvingOption solvingOption, TrigFunction trigFunction)
    {
        float x = UserInput.GetValidFloatInput("X");
        float z = UserInput.GetValidFloatInput("Z");

        if (solvingOption == SolvingOption.Angle)
        {
            return GetCalculatedAngle(x, z, trigFunction) * DivideByPI;
        }

        bool useInverseCalculation = UserInput.GetValidBooleanInput("Use Inverse Calculation");
        return GetCalculatedSide(ConvertToDegrees(x), z, trigFunction, useInverseCalculation);
    }

    private static double SolveIsoscelesTriangle(SolvingOption solvingOption, TrigLawFunction trigLawFunction)
    {
        float x = UserInput.GetValidFloatInput("X");
        float y = UserInput.GetValidFloatInput("Y");
        float z = UserInput.GetValidFloatInput("Z");

        return trigLawFunction == TrigLawFunction.SineLaw
            ? SolveWithSineLaw(x, y, z, solvingOption)
            : SolveWithCosineLaw(x, y, z, solvingOption);
    }

    private static double SolveWithSineLaw(float x, float y, float z, SolvingOption solvingOption)
    {
        if (solvingOption == SolvingOption.Side)
        {
            y = ConvertToDegrees(y);
            x = ConvertToDegrees(x);

            return Math.Sin(y) * z / Math.Sin(x);
        }

        x = ConvertToDegrees(x);
        return Math.Asin(Math.Sin(x) / y * z) * DivideByPI;
    }

    private static double SolveWithCosineLaw(float x, float y, float z, SolvingOption solvingOption)
    {
        if (solvingOption == SolvingOption.Side)
        {
            z = ConvertToDegrees(z);

            return Math.Sqrt((x * x) + (y * y) - (2 * x * y * Math.Cos(z)));
        }
        
        return Math.Acos(((x * x) + (y * y) - (z * z)) / (2 * x * y)) * DivideByPI;
    }

    private static double GetCalculatedSide(float angle, float side, TrigFunction trigFunction, bool useInverseCalculation)
    {
        if (useInverseCalculation)
        {
            return trigFunction switch
            {
                TrigFunction.Sine => side / Math.Sin(angle),
                TrigFunction.Cosine => side / Math.Cos(angle),
                TrigFunction.Tangent => side / Math.Tan(angle),
                _ => 0
            };
        }

        return trigFunction switch
        {
            TrigFunction.Sine => side * Math.Sin(angle),
            TrigFunction.Cosine => side * Math.Cos(angle),
            TrigFunction.Tangent => side * Math.Tan(angle),
            _ => 0
        };
    }

    private static double GetCalculatedAngle(float side1, float side2, TrigFunction trigFunction)
    {
        return trigFunction switch
        {
            TrigFunction.Sine => Math.Asin(side1 / side2),
            TrigFunction.Cosine => Math.Acos(side1 / side2),
            TrigFunction.Tangent => Math.Atan(side1 / side2),
            _ => 0
        };
    }

    private static float ConvertToDegrees(float value)
    {
        return value * DivideBy180;
    }
}