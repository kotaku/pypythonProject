using System;
using System.Windows.Media;


namespace pypythonProject
{
    public class ColorEventArgs : EventArgs
    {
        private Color _color;

        public Color Color
        {
            get { return _color; }
        }


        public ColorEventArgs(Color color)
        {
            _color = color;
        }
    }
}
