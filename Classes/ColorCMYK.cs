using System;
using System.Windows.Media;

namespace pypythonProject
{
    struct ColorCMYK
    {
        private byte _c;
        private byte _m;
        private byte _y;
        private byte _k;

        public byte C
        {
            get
            {
                return _c;
            }
            set
            {
                _c = ColorCheck(value);
            }
        }
        public byte M
        {
            get
            {
                return _m;
            }
            set
            {
                _m = ColorCheck(value);
            }
        }
        public byte Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = ColorCheck(value);
            }
        }
        public byte K
        {
            get
            {
                return _k;
            }
            set
            {
                _k = ColorCheck(value);
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


        static public ColorCMYK FromArgs(byte c, byte m, byte y, byte k)
        {
            ColorCMYK cmyk = new ColorCMYK();
            cmyk.C = c;
            cmyk.M = m;
            cmyk.Y = y;
            cmyk.K = k;
            return cmyk;
        }
        static public ColorCMYK FromRGB(byte r, byte g, byte b)
        {
            //RGB=>CMY
            double c, m, y;
            c = 1.0 - (r / 255.0);
            m = 1.0 - (g / 255.0);
            y = 1.0 - (b / 255.0);
            //CMY=>CMYK
            double k = Math.Min(c, Math.Min(m, y));
            if (k == 1.0)
            {
                c = 0;
                m = 0;
                y = 0;
                k *= 100;
            }
            else
            {
                c = ((c - k) / (1.0 - k)) * 100 + 0.5;
                m = ((m - k) / (1.0 - k)) * 100 + 0.5;
                y = ((y - k) / (1.0 - k)) * 100 + 0.5;
                k *= 100 + 0.5;
            }

            ColorCMYK cmyk = FromArgs((byte)c, (byte)m, (byte)y, (byte)k);
            return cmyk;
        }
        static public ColorCMYK FromColor(Color color)
        {
            return FromRGB(color.R, color.G, color.B);
        }
    }
}
