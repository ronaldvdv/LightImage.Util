using System;

namespace LightImage.Util.Geometry
{
    public readonly struct SizeD
    {
        public readonly double Height;
        public readonly double Width;

        public SizeD(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public SizeD EnlargeToAspectRatio(double ratio)
        {
            if (ratio < double.Epsilon)
                return this;
            var w = Math.Max(Width, Height * ratio);
            var h = Math.Max(Height, Width / ratio);
            return new SizeD(w, h);
        }
    }
}