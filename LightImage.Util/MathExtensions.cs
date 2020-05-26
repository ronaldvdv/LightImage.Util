namespace System
{
    public static class MathExtensions
    {
        private static double C_MAX_INT = (double)int.MaxValue;

        private static double C_MIN_INT = (double)int.MinValue;

        public static double Clamp(this double x, double min, double max)
        {
            if (max < min)
            {
                max = min;
            }
            if (x <= min)
                x = min;
            if (x >= max)
                x = max;
            return x;
        }

        public static float Clamp(this float x, float min, float max)
        {
            if (max < min)
            {
                max = min;
            }
            if (x <= min)
                x = min;
            if (x >= max)
                x = max;
            return x;
        }

        public static int Clamp(this int x, double min, double max)
        {
            int imin = (min < C_MIN_INT) ? int.MinValue : (int)min;
            int imax = (max > C_MAX_INT) ? int.MaxValue : (int)max;
            return x.Clamp(imin, imax);
        }

        public static int Clamp(this int x, int min, int max)
        {
            if (max < min)
            {
                max = min;
            }
            if (x < min)
                x = min;
            if (x > max)
                x = max;
            return x;
        }
    }
}