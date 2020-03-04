using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Board
    {
        private int[] Values { get; set; }
        private List<Square> Squares { get; }
        private List<SquareContainer> Blocks { get; }
        private List<SquareContainer> Rows { get; }
        private List<SquareContainer> Columns { get; }
        private int[] StartingBoard { get; set; }
        public Board() {
            this.Squares = new List<Square>();
            this.Blocks = new List<SquareContainer>();
            this.Rows = new List<SquareContainer>();
            this.Columns = new List<SquareContainer>();

            initSquares();
            setValues();
            initColumns();
            initRows();
            initBlocks();
            
        }

        public void setValues() {
            Generator gen = new Generator();
            Values = gen.GenerateSolution();
            int i = 0;
            foreach (Square s in Squares) {
                s.setValue(Values[i]);
                i++;
            }
            StartingBoard = gen.GenerateBoard();
        }

        public void initSquares() {
            int count = 1;
            for (int x = 1; x <= 9; x++) {
                for (int y = 1; y <= 9; y++) {
                    Square s = new Square(count, x, y);
                    Squares.Add(s);
                    count++;
                }
            }
        }

        public void initColumns() {
            for (int c = 1; c <= 9; c++) {
                SquareContainer container = new SquareContainer(c, "col");
                List<Square> squares = new List<Square>();
                for (int r = 1; r <= 9; r++) {
                    squares.Add(find(r, c));
                }
                container.setSquares(squares);
                Columns.Add(container);
            }
        }

        public void initRows() {
            for (int r = 1; r <= 9; r++) {
                SquareContainer container = new SquareContainer(r, "row");
                List<Square> squares = new List<Square>();
                for (int c = 1; c <= 9; c++) {
                    squares.Add(find(r, c));
                }
                container.setSquares(squares);
                Rows.Add(container);
            }
        }

        public void initBlocks() {
            int currentBlock = 1;
            for (int r = 1; r <= 9; r++) {
                SquareContainer block = new SquareContainer(r, "block");
                Blocks.Add(block);
            }
            foreach (Square s in Squares) {
                int index = s.Index;
                if (index % 27 == 0)
                {
                    AddToBlock(currentBlock, s);
                    currentBlock++;
                }
                else if (index % 9 == 0)
                {
                    AddToBlock(currentBlock, s);
                    currentBlock -= 2;
                }
                else if (index % 3 == 0)
                {
                    AddToBlock(currentBlock, s);
                    currentBlock++;
                }
                else {
                    AddToBlock(currentBlock, s);
                }
            }
        }

        void AddToBlock(int currentBlock, Square s) {
            SquareContainer b = Blocks[currentBlock - 1];
            b.addSquare(s);
        }

        private Square find(int x, int y)
        {
            foreach(Square s in Squares)
            {
                if (s.X == x && s.Y == y)
                {
                    return s;
                }
            }
            return null;
        }

        private Square find(int index) {
            foreach (Square s in Squares) {
                if (s.Index == index) {
                    return s;                
                }
            }
            return null;
        }

        public Square find(String id) {
            foreach (Square s in Squares) {
                if (s.LabelID.Equals(id)) {
                    return s;
                }
            }
            return null;
        }

        public List<Square> GetSquares() {
            return Squares;
        }

        public List<SquareContainer> getRows() {
            return Rows;
        }

        public List<SquareContainer> getCols()
        {
            return Columns;
        }

        public List<SquareContainer> getBlocks()
        {
            return Blocks;
        }

        public int[] getStartBoard() {
            return StartingBoard;
        }

        public bool isValueLegal(Square s) {
            bool legal = true;
            if (Values[s.Index - 1] != s.Value) {
                legal = false;
            }
            return legal;
        }

        //Probably going to delete this method, or reuse it in generator 

        /*public bool isValueLegal(Square s) {
            bool legal = false;
             //Gets the square list from the block that Square s is in
             //The blocks are numbered 1-9, so we have to subtract 1 to get the correct list index
            foreach (Square current in Blocks[s.Block-1].getSquares()) {
                if (current.Value == s.Value) {
                    legal = false;
                }
            }

            foreach (Square current in Rows[s.Y - 1].getSquares())
            {
                if (current.Value == s.Value)
                {
                    legal = false;
                }
            }

            foreach (Square current in Columns[s.X - 1].getSquares())
            {
                if (current.Value == s.Value)
                {
                    legal = false;
                }
            }
            return legal;
        }*/
    }
}
