using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace pypythonProject
{
    public partial class MainWindow : Window
    {
        private List<Polygon> _colourTringles;



        public MainWindow()
        {
            InitializeComponent();

            _colourTringles = new List<Polygon> { ColourTriangle_1, ColourTriangle_2, ColourTriangle_3, ColourTriangle_4, ColourTriangle_5 };
            ActiveTriangle(2);
        }



        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MinimazeLable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitLable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExitLable_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitLable.Background = new SolidColorBrush(Colors.DarkGray);
        }

        private void ExitLable_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitLable.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void MinimazeLable_MouseEnter(object sender, MouseEventArgs e)
        {
            MinimazeLable.Background = new SolidColorBrush(Colors.DarkGray);
        }

        private void MinimazeLable_MouseLeave(object sender, MouseEventArgs e)
        {
            MinimazeLable.Background = new SolidColorBrush(Colors.Transparent);
        }



        private void ColourTriangle_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PipetMode(0);
        }

        private void ColourTriangle_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PipetMode(1);
        }

        private void ColourTriangle_3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PipetMode(2);
        }

        private void ColourTriangle_4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PipetMode(3);
        }

        private void ColourTriangle_5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PipetMode(4);
        }

        private void PipetMode(byte index)
        {
            ActiveTriangle(index);
            PipetWindow window = new PipetWindow(index);
            window.Show();
            window.ChoiceColor += new ColorEventHandler(ColorTriangle_ColorChange);
        }


        private void ActiveTriangle(int number)
        {
            ResetTriangleState();
            _colourTringles[number].Stroke = new SolidColorBrush(Colors.White);
            _colourTringles[number].StrokeThickness = 2;
            Canvas.SetZIndex(_colourTringles[number], 2);
            FillColourTextBox(((SolidColorBrush)(_colourTringles[number].Fill)).Color);
        }

        private void ResetTriangleState()
        {
            foreach (Polygon x in _colourTringles)
            {
                x.StrokeThickness = 0;
                Canvas.SetZIndex(x, 1);
            }
        }

        private void ColorTriangle_ColorChange(byte index, ColorEventArgs arg)
        {
            _colourTringles[index].Fill = new SolidColorBrush(arg.Color);
            FillColourTextBox(arg.Color);
        }

        private void FillColourTextBox(Color colour)
        {
            RGB_R.Text = Convert.ToString(colour.R);
            RGB_G.Text = Convert.ToString(colour.G);
            RGB_B.Text = Convert.ToString(colour.B);

            ColorCMYK cmyk = ColorCMYK.FromColor(colour);
            CMYK_C.Text = Convert.ToString(cmyk.C);
            CMYK_M.Text = Convert.ToString(cmyk.M);
            CMYK_Y.Text = Convert.ToString(cmyk.Y);
            CMYK_K.Text = Convert.ToString(cmyk.K);

            ColorLAB lab = ColorLAB.FromColor(colour);
            LAB_L.Text = Convert.ToString(lab.L);
            LAB_A.Text = Convert.ToString(lab.A);
            LAB_B.Text = Convert.ToString(lab.B);

            ColorHSB hsb = ColorHSB.FromColor(colour);
            HSB_H.Text = Convert.ToString(hsb.H);
            HSB_S.Text = Convert.ToString(hsb.S);
            HSB_B.Text = Convert.ToString(hsb.B);

            HEX.Text = colour.ToString().Remove(0, 3);
        }



        private void dotACOTriangle_MouseEnter(object sender, MouseEventArgs e)
        {
            dotACOTriangle.Fill = new SolidColorBrush(Color.FromArgb(255, 26, 26, 29));
        }

        private void dotACOTriangle_MouseLeave(object sender, MouseEventArgs e)
        {
            dotACOTriangle.Fill = new SolidColorBrush(Color.FromArgb(255, 16, 16, 17));
        }

        private void dotACOTriangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<Color> colors = new List<Color>
            {
                ((SolidColorBrush)(_colourTringles[0].Fill)).Color,
                ((SolidColorBrush)(_colourTringles[1].Fill)).Color,
                ((SolidColorBrush)(_colourTringles[2].Fill)).Color,
                ((SolidColorBrush)(_colourTringles[3].Fill)).Color,
                ((SolidColorBrush)(_colourTringles[4].Fill)).Color
            };
            SaveACO(colors);
        }

        static private void SaveACO(List<Color> colors)
        {
            byte[] ar = DotACO.FiveColors(colors);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Palette(*.aco)|*.aco" + "|All Files(*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                using (FileStream fstream = new FileStream(sfd.FileName, FileMode.Create))
                {
                    fstream.Write(ar, 0, ar.Length);
                }
            }
        }
    }
}
