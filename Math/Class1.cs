using static System.Math;

namespace Math
{
    public static class Math
    {
        // Stałe
        public const double E = System.Math.E;
        public const double PI = System.Math.PI;

        // Metody
        public static double Abs(double value) => System.Math.Abs(value);
        public static decimal Abs(decimal value) => System.Math.Abs(value);
        public static float Abs(float value) => System.Math.Abs(value);
        public static int Abs(int value) => System.Math.Abs(value);
        public static long Abs(long value) => System.Math.Abs(value);
        public static short Abs(short value) => System.Math.Abs(value);
        public static sbyte Abs(sbyte value) => System.Math.Abs(value);

        public static double Acos(double d) => System.Math.Acos(d);
        public static double Asin(double d) => System.Math.Asin(d);
        public static double Atan(double d) => System.Math.Atan(d);
        public static double Atan2(double y, double x) => System.Math.Atan2(y, x);

        public static double Ceiling(double a) => System.Math.Ceiling(a);
        public static decimal Ceiling(decimal d) => System.Math.Ceiling(d);

        public static double Cos(double d) => System.Math.Cos(d);
        public static double Cosh(double value) => System.Math.Cosh(value);

        public static double Exp(double d) => System.Math.Exp(d);

        public static double Floor(double d) => System.Math.Floor(d);
        public static decimal Floor(decimal d) => System.Math.Floor(d);

        public static double Log(double d) => System.Math.Log(d);
        public static double Log(double a, double newBase) => System.Math.Log(a, newBase);
        public static double Log10(double d) => System.Math.Log10(d);

        public static double Max(double val1, double val2) => System.Math.Max(val1, val2);
        public static int Max(int val1, int val2) => System.Math.Max(val1, val2);
        public static long Max(long val1, long val2) => System.Math.Max(val1, val2);
        public static float Max(float val1, float val2) => System.Math.Max(val1, val2);
        public static decimal Max(decimal val1, decimal val2) => System.Math.Max(val1, val2);

        public static double Min(double val1, double val2) => System.Math.Min(val1, val2);
        public static int Min(int val1, int val2) => System.Math.Min(val1, val2);
        public static long Min(long val1, long val2) => System.Math.Min(val1, val2);
        public static float Min(float val1, float val2) => System.Math.Min(val1, val2);
        public static decimal Min(decimal val1, decimal val2) => System.Math.Min(val1, val2);

        public static double Pow(double x, double y) => System.Math.Pow(x, y);

        public static double Round(double a) => System.Math.Round(a);
        public static double Round(double value, int digits) => System.Math.Round(value, digits);
        public static decimal Round(decimal d) => System.Math.Round(d);
        public static decimal Round(decimal d, int decimals) => System.Math.Round(d, decimals);

        public static int Sign(double value) => System.Math.Sign(value);
        public static int Sign(decimal value) => System.Math.Sign(value);
        public static int Sign(float value) => System.Math.Sign(value);
        public static int Sign(int value) => System.Math.Sign(value);
        public static int Sign(long value) => System.Math.Sign(value);
        public static int Sign(short value) => System.Math.Sign(value);
        public static int Sign(sbyte value) => System.Math.Sign(value);

        public static double Sin(double a) => System.Math.Sin(a);
        public static double Sinh(double value) => System.Math.Sinh(value);

        public static double Sqrt(double d) => System.Math.Sqrt(d);

        public static double Tan(double a) => System.Math.Tan(a);
        public static double Tanh(double value) => System.Math.Tanh(value);

        public static double Truncate(double d) => System.Math.Truncate(d);
        public static decimal Truncate(decimal d) => System.Math.Truncate(d);
        public static long silnia(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "Silnia nie jest zdefiniowana dla liczb ujemnych.");
            }
            return n == 0 ? 1 : n * silnia(n - 1);
        }
    }
}