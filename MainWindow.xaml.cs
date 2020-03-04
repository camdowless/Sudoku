using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Controls;
using System;

namespace Sudoku
{
    public partial class MainWindow : Window
    {
        Label selected;
        Board board = new Board();

        public MainWindow()
        {
            InitializeComponent();
        }

        private new void MouseEnter(object sender, MouseEventArgs e)
        {
            Label sel = (Label)sender;
            sel.Effect = getHoverEffect();
            selected = sel;
        }

        private new void MouseLeave(object sender, MouseEventArgs e)
        {
            Label sel = (Label)sender;
            sel.Effect = null;
            selected = null;
        }

        private Effect getHoverEffect() {
            Effect e = new DropShadowEffect();
            Color color = (Color)ColorConverter.ConvertFromString("#FF0936FF");
            var colorProp = typeof(DropShadowEffect).GetProperty("Color");
            colorProp.SetValue(e, color, null);
            var shadowDepth = typeof(DropShadowEffect).GetProperty("ShadowDepth");
            shadowDepth.SetValue(e, 2, null);
            return e;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (selected != null) {
                switch (e.Key)
                {
                    case Key.D1:
                        SetValue(1);
                        break;
                    case Key.D2:
                        SetValue(2);
                        break;
                    case Key.D3:
                        SetValue(3);
                        break;
                    case Key.D4:
                        SetValue(4);
                        break;
                    case Key.D5:
                        SetValue(5);
                        break;
                    case Key.D6:
                        SetValue(6);
                        break;
                    case Key.D7:
                        SetValue(7);
                        break;
                    case Key.D8:
                        SetValue(8);
                        break;
                    case Key.D9:
                        SetValue(9);
                        break;
                    case Key.Enter:
                        printSquares();
                        break;
                }
            }
        }

        void SetValue(int val) {
            Square s = board.find(selected.Name);
            //If the square value is predetermined, i.e. at the start of a game,
            //You cannt change the value
            //Which is why I called it Fixed
            if(!s.Fixed){
                s.Value = val;
                selected.Content = val.ToString();
            }
        }

        void printSquares() { 
            foreach(Square s in board.GetSquares()){
                Console.WriteLine(s.LabelID + "  " + s.Value);
            }
        }
    }
}