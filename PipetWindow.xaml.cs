using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace pypythonProject
{
    public delegate void ColorEventHandler(byte index, ColorEventArgs arg);


    public partial class PipetWindow : Window
    {
        private byte _screenIndex;


        public event ColorEventHandler ChoiceColor;


        public PipetWindow(byte index)
        {
            _screenIndex = index;
            InitializeComponent();

            RefreshElementPosition(FalsePipet);
            RefreshElementPosition(Pipet);
            Pipet.Fill = new SolidColorBrush(ColorFromScreen.MediaFromScreen(System.Windows.Forms.Cursor.Position));
        }


        private void Plot_MouseMove(object sender, MouseEventArgs e)
        {
            RefreshElementPosition(FalsePipet);
            RefreshElementPosition(Pipet);
            Pipet.Fill = new SolidColorBrush(ColorFromScreen.MediaFromScreen(System.Windows.Forms.Cursor.Position));
        }

        private void Plot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            OnChoiceColor(new ColorEventArgs(ColorFromScreen.MediaFromScreen(System.Windows.Forms.Cursor.Position)));
        }


        protected virtual void OnChoiceColor(ColorEventArgs arg)
        {
            if (ChoiceColor != null)
            {
                ChoiceColor(_screenIndex, arg);
            }
        }


        private void RefreshElementPosition(UIElement element)
        {
            System.Drawing.Point position = System.Windows.Forms.Cursor.Position;
            Canvas.SetLeft(element, position.X);
            Canvas.SetTop(element, position.Y);
        }
    }
}
