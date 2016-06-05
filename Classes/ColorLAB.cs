using System;
using System.Windows.Media;

namespace pypythonProject
{
    struct ColorLAB
    {
        private byte _l;
        private short _a;
        private short _b;

        public byte L
        {
            get
            {
                return _l;
            }
            set
            {
                if (value >= 0.0 && value <= 100.0)
                {
                    _l = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        public short A
        {
            get
            {
                return _a;
            }
            set
            {
                _a = ColorCheck(value);
            }
        }
        public short B
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


        static private short ColorCheck(short c)
        {
            if (c >= -128.0 && c <= 127.0)
            {
                return c;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        static public double[] RGBToXYZ(byte red, byte green, byte blue)
        {
            //Converts RGB to CIE XYZ(CIE 1931 color space).

            //normalize red, green, blue values
            double rLinear, gLinear, bLinear;
            rLinear = red / 255.0;
            gLinear = green / 255.0;
            bLinear = blue / 255.0;

            //RGB=>sRGB
            double r, g, b;
            r = (rLinear > 0.04045) ? Math.Pow((rLinear + 0.055) / (1 + 0.055), 2.4) : (rLinear / 12.92);
            g = (gLinear > 0.04045) ? Math.Pow((gLinear + 0.055) / (1 + 0.055), 2.4) : (gLinear / 12.92);
            b = (bLinear > 0.04045) ? Math.Pow((bLinear + 0.055) / (1 + 0.055), 2.4) : (bLinear / 12.92);

            r *= 100;
            g *= 100;
            b *= 100;

            //Converts
            return new double[] {
                (r * 0.4124 + g * 0.3576 + b * 0.1805),
                (r * 0.2126 + g * 0.7152 + b * 0.0722),
                (r * 0.0193 + g * 0.1192 + b * 0.9505)
                };

        }
        static public ColorLAB XYZToLAB(double[] xyz)
        {
            if (xyz.Length == 3)
            {
                double x, y, z, l, a, b;
                x = xyz[0] / 95.047;
                y = xyz[1] / 100.0;
                z = xyz[2] / 108.883;

                x = Fxyz(x);
                y = Fxyz(y);
                z = Fxyz(z);

                l = 116.0 * y - 16;
                a = 500.0 * (x - y);
                b = 200.0 * (y - z);

                ColorLAB lab = new ColorLAB();

                lab.L = (byte)(l + 0.5);
                lab.A = (short)((a > 0) ? a + 0.5 : a - 0.5);
                lab.B = (short)((b > 0) ? b + 0.5 : b - 0.5);

                return lab;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        static private double Fxyz(double t)
        {
            //XYZ to L*a*b* transformation function.
            return ((t > 0.008856) ? Math.Pow(t, (1.0 / 3.0)) : (7.787 * t + 16.0 / 116.0));
        }


        static public ColorLAB FromArgs(byte l, byte a, byte b)
        {
            ColorLAB lab = new ColorLAB();
            lab.L = l;
            lab.A = a;
            lab.B = b;
            return lab;
        }
        static public ColorLAB FromRGB(byte r, byte g, byte b)
        {
            return XYZToLAB(RGBToXYZ(r, g, b));
        }
        static public ColorLAB FromColor(Color color)
        {
            return FromRGB(color.R, color.G, color.B);
        }
    }
}
