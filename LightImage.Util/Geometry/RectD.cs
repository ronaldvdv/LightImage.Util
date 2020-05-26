namespace LightImage.Util.Geometry
{
    public readonly struct RectD
    {
        public readonly double Height;
        public readonly double Left;
        public readonly double Top;
        public readonly double Width;

        public RectD(double left, double top, double width, double height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
    }
}