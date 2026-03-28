using System;

namespace PitPr4.App;

/// <summary>Формулы 14-го варианта (ПР4).</summary>
public static class Variant14Math
{
    private const double Eps = 1e-12;

    public static bool TryComputeG(double x, double y, double z, out double result, out string? error)
    {
        result = 0;
        error = null;
        if (Math.Abs(y - 2) < Eps)
        {
            error = "y не может быть 2 (знаменатель обращается в 0).";
            return false;
        }

        if (Math.Abs(x + y) < Eps)
        {
            error = "x + y не может быть 0.";
            return false;
        }

        if (Math.Abs(Math.Sin(z)) < Eps)
        {
            error = "sin(z) не может быть 0 (показатель степени не определён).";
            return false;
        }

        double num = Math.Pow(y, x + 1);
        double den = Math.Cbrt(Math.Abs(y - 2));
        double t1 = num / den;
        double t2 = (x + y / 2) / (2 * Math.Abs(x + y)) * Math.Pow(x + 1, -1 / Math.Sin(z));
        result = t1 + t2;
        if (double.IsNaN(result) || double.IsInfinity(result))
        {
            error = "Результат не определён для вещественных чисел (проверьте знак основания степени y^(x+1)).";
            return false;
        }

        return true;
    }

    public enum FKind
    {
        Sh,
        Square,
        Exp,
    }

    public static double F(FKind kind, double x) =>
        kind switch
        {
            FKind.Sh => Math.Sinh(x),
            FKind.Square => x * x,
            FKind.Exp => Math.Exp(x),
            _ => throw new ArgumentOutOfRangeException(nameof(kind)),
        };

    public static double ComputeD(double x, double y, FKind kind)
    {
        double fx = F(kind, x);
        if (x > y)
            return Math.Pow(fx - y, 3) + Math.Atan(fx);
        if (y > x)
            return Math.Pow(y - fx, 3) + Math.Atan(fx);
        return Math.Pow(y + fx, 3) + 0.5;
    }

    public static bool TrySamplePlot(double xFrom, double xTo, double step, double b, out List<(double X, double Y)> points, out string? error)
    {
        points = new List<(double, double)>();
        error = null;
        if (xFrom > xTo)
        {
            error = "Начало отрезка не может быть больше конца.";
            return false;
        }

        if (step <= 0)
        {
            error = "Шаг должен быть положительным.";
            return false;
        }

        if (xFrom < 0 || xTo < 0)
        {
            error = "Для x^(5/2) в вещественных числах используйте x ≥ 0.";
            return false;
        }

        for (double x = xFrom; x <= xTo + Eps; x += step)
        {
            if (x > xTo + Eps)
                break;
            double y = (Math.Pow(x, 2.5) - b) * Math.Log(x * x + 12.7);
            points.Add((x, y));
        }

        return true;
    }
}
