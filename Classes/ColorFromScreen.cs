using System.Drawing;

namespace pypythonProject
{
    static class ColorFromScreen
    {
        static public Color DrawingFromScreen(Point point)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics screen = Graphics.FromImage(bmp);
            screen.CopyFromScreen(point.X, point.Y, 0, 0, bmp.Size);
            return bmp.GetPixel(0, 0);
        }

        static public System.Windows.Media.Color MediaFromScreen(Point point)
        {
            Color color = DrawingFromScreen(point);
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
