using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Board
    {
        private List<Square> Squares { get; }
        private List<SquareContainer> Blocks { get; }
        private List<SquareContainer> Rows { get; }
        private List<SquareContainer> Columns { get; }

        public Board() {
            this.Squares = new List<Square>();
            this.Blocks = new List<SquareContainer>();
            this.Rows = new List<SquareContainer>();
            this.Columns = new List<SquareContainer>();

            initSquares();
            initColumns();
            initRows();
            initBlocks();
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
    }
}
