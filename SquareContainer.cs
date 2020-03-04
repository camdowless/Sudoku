using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SquareContainer
    {
        public int Id { get; }
        private List<Square> Squares { get; set; }
        public String Type { get; }
                        //SquareContainer has 3 types:
                        //Row, Column, & Block
        public SquareContainer(int id, String type) {
            this.Squares = new List<Square>();
            this.Id = id;
            this.Type = type;
        }

        public SquareContainer(int id, List<Square> squares, String type) {
            this.Id = id;
            this.Squares = squares;
            this.Type = type;
        }

        public void setSquares(List<Square> s) {
            Squares = s;
        }

        public List<Square> getSquares() {
            return Squares;
        }

        public void addSquare(Square s) {
            Squares.Add(s);
        }
    }
}