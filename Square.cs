using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Square
    {   
        public int X { get; } //Column number
        public int Y { get; } //Row number
        public int Block { get; set; }
        public int Index { get; }
        public int Value { get; set; }
        public String LabelID { get; }
        public bool Fixed { get; set; }
        public Square(int index, int x, int y) {
            this.Fixed = false;
            this.Index = index;
            this.Y = y;
            this.X = x;
            this.LabelID = "_" + index;
        }

        public void setBlock(int b) {
            this.Block = b;
        }
    }
}
