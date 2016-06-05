using System;
using System.Collections.Generic;


namespace pypythonProject
{
    static class DotACO
    {
        static public byte[] FiveColors(List<System.Windows.Media.Color> colors)
        {
            if (colors.Count > 5)
            {
                throw new ArgumentOutOfRangeException();
            }

            byte[] array = { 0, 1, 0, 5, 0, 0, 48, 48, 7, 7, 133, 133, 0, 0, 0, 0, 90, 90, 37, 37, 209, 209, 0, 0, 0, 0, 217, 217, 138, 138, 55, 55, 0, 0, 0, 0, 9, 9, 158, 158, 31, 31, 0, 0, 0, 0, 37, 37, 133, 133, 51, 51, 0, 0, 0, 2, 0, 5, 0, 0, 48, 48, 7, 7, 133, 133, 0, 0, 0, 0, 0, 10, 4, 30, 4, 49, 4, 64, 4, 48, 4, 55, 4, 53, 4, 70, 0, 32, 0, 49, 0, 0, 0, 0, 90, 90, 37, 37, 209, 209, 0, 0, 0, 0, 0, 10, 4, 30, 4, 49, 4, 64, 4, 48, 4, 55, 4, 53, 4, 70, 0, 32, 0, 50, 0, 0, 0, 0, 217, 217, 138, 138, 55, 55, 0, 0, 0, 0, 0, 10, 4, 30, 4, 49, 4, 64, 4, 48, 4, 55, 4, 53, 4, 70, 0, 32, 0, 51, 0, 0, 0, 0, 9, 9, 158, 158, 31, 31, 0, 0, 0, 0, 0, 10, 4, 30, 4, 49, 4, 64, 4, 48, 4, 55, 4, 53, 4, 70, 0, 32, 0, 52, 0, 0, 0, 0, 37, 37, 133, 133, 51, 51, 0, 0, 0, 0, 0, 10, 4, 30, 4, 49, 4, 64, 4, 48, 4, 55, 4, 53, 4, 70, 0, 32, 0, 53, 0, 0 };

            for (int c = 0, i = 6, j = 60; c < colors.Count; c++, i += 10, j += 34)
            {
                array[i] = colors[c].R;
                array[i + 1] = colors[c].R;

                array[i + 2] = colors[c].G;
                array[i + 3] = colors[c].G;

                array[i + 4] = colors[c].B;
                array[i + 5] = colors[c].B;


                array[j] = colors[c].R;
                array[j + 1] = colors[c].R;

                array[j + 2] = colors[c].G;
                array[j + 3] = colors[c].G;

                array[j + 4] = colors[c].B;
                array[j + 5] = colors[c].B;
            }
            return array;
        }
    }
}

