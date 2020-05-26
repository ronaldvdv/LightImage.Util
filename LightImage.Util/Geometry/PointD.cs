using System;

namespace LightImage.Util.Geometry
{
    public readonly struct PointD
    {
        public readonly double X;
        public readonly double Y;

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length => Math.Sqrt(LengthSquared);

        public double LengthSquared => X * X + Y * Y;

        public static PointD operator -(PointD a, PointD b)
        {
            return new PointD(a.X - b.X, a.Y - b.Y);
        }

        public static PointD operator +(PointD a, PointD b)
        {
            return new PointD(a.X + b.X, a.Y + b.Y);
        }

        public PointD Clamp(double minX, double maxX, double minY, double maxY)
        {
            return new PointD(X.Clamp(minX, maxX), Y.Clamp(minY, maxY));
        }

        public PointD Closest(PointD a, PointD b)
        {
            return (a - this).LengthSquared > (b - this).LengthSquared ? b : a;
        }

        public double DistanceTo(PointD point)
        {
            return (this - point).Length;
        }

        public PointD Project(PointD line1, PointD line2)
        {
            if (Math.Abs(line1.X - line2.X) < double.Epsilon)
                return this;
            var m = (line2.Y - line1.Y) / (line2.X - line1.X);
            var b = line1.Y - m * line1.X;

            var x = (m * Y + X - m * b) / (m * m + 1);
            var y = (m * m * Y + m * X + b) / (m * m + 1);

            return new PointD(x, y);
        }

        public PointD ProjectHorizontally(PointD refA, PointD refB)
        {
            if (Math.Abs(refB.Y - refA.Y) < double.Epsilon)
                return this;
            var t = (Y - refA.Y) / (refB.Y - refA.Y);
            return new PointD(refA.X + t * (refB.X - refA.X), Y);
        }

        public PointD ProjectVertically(PointD refA, PointD refB)
        {
            if (Math.Abs(refB.X - refA.X) < double.Epsilon)
                return this;
            var t = (X - refA.X) / (refB.X - refA.X);
            return new PointD(X, refA.Y + t * (refB.Y - refA.Y));
        }

        public override string ToString()
        {
            return $"{X:F4},{Y:F4}";
        }
    }
}