using System;
using System.Windows.Media;

namespace pypythonProject
{
    struct ColorHSB
    {
        private short _h;
        private byte _s;
        private byte _b;

        public short H
        {
            get
            {
                return _h;
            }
            set
            {
                if (value >= 0.0 && value <= 360.0)
                {
                    _h = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        public byte S
        {
            get
            {
                return _s;
            }
            set
            {
                _s = ColorCheck(value);
            }
        }
        public byte B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = ColorCheck(value);
            }
        }


        static private byte ColorCheck(byte c)
        {
            if (c >= 0.0 && c <= 100.0)
            {
                return c;
            }
            else
            {
                throw new ArgumentException();
            }
        }


        static public ColorHSB FromArgs(short h, byte s, byte b)
        {
            ColorHSB hsb = new ColorHSB();
            hsb.H = h;
            hsb.S = s;
            hsb.B = b;
            return hsb;
        }
        static public ColorHSB FromRGB(byte r, byte g, byte b)
        {
            //Normalize red, green and blue values
            double red = r / 255.0;
            double green = g / 255.0;
            double blue = b / 255.0;

            //Conversion start
            double max = Math.Max(red, Math.Max(green, blue));
            double min = Math.Min(red, Math.Min(green, blue));

            double h = 0.0;
            if (max == red && green >= blue)
            {
                h = 60 * (green - blue) / (max - min);
            }
            else if (max == red && green < blue)
            {
                h = 60 * (green - blue) / (max - min) + 360;
            }
            else if (max == green)
            {
                h = 60 * (blue - red) / (max - min) + 120;
            }
            else if (max == blue)
            {
                h = 60 * (red - green) / (max - min) + 240;
            }
            double s = (max == 0) ? 0.0 : (1.0 - (min / max));

            ColorHSB hsb = new ColorHSB();
            hsb.H = (short)(h + 0.5);
            hsb.S = (byte)(s * 100 + 0.5);
            hsb.B = (byte)(max * 100 + 0.5);

            return hsb;
        }
        static public ColorHSB FromColor(Color color)
        {
            return FromRGB(color.R, color.G, color.B);
        }
    }
}
