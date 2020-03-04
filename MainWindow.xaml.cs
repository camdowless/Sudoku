using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Controls;
using System;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace Sudoku
{
    public partial class MainWindow : Window
    {
        Label selected;
        Board board = new Board();
        public MainWindow()
        {
            start();
        }
        void start()
        {
            InitializeComponent();
            showBoard();
        }
        private void showBoard()
        {
            int[] startBoard = board.getStartBoard();
            for (int i = 0; i < 80; i++)
            {
                if (startBoard[i] != 0)
                {
                    Square s = board.GetSquares()[i + 1];
                    s.Fixed = true;
                    Label l = (Label)FindName(s.LabelID);
                    l.Content = s.Value;
                }
            }
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
            if (board.checkWin())
            {
                Console.WriteLine("Win");
                MessageBox.Show("You win!", "Fuck you", MessageBoxButton.OK);

                InitializeComponent();
            }
            else {
                Console.WriteLine("No");
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
                if (!board.isValueLegal(s))
                {
                    makeRed();
                }
                else {
                    
                    _ = makeGreenAsync();
                    s.Fixed = true;
                }
            }
        }

         async Task makeGreenAsync() {
            Brush brush = new SolidColorBrush(Color.FromRgb(29, 255, 21));
            selected.Background = brush;
            await Task.Delay(400);
            selected.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        void makeRed() {
            Brush brush = new SolidColorBrush(Color.FromRgb(241, 0, 0));
            selected.Background = brush;
        }

        

        void printSquares() { 
            foreach(Square s in board.GetSquares()){
                Console.WriteLine(s.LabelID + "  " + s.Value);
            }
        }
    }
}