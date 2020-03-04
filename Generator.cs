using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Generator
    {
        int[] Reference = {8,6,1,7,9,4,3,5,2
                          ,3,5,2,1,6,8,7,4,9
                          ,4,9,7,2,5,3,1,8,6
                          ,2,1,8,9,7,5,6,3,4
                          ,6,7,5,3,4,1,9,2,8
                          ,9,3,4,6,8,2,5,1,7
                          ,5,2,6,8,1,9,4,7,3
                          ,7,4,3,5,2,6,8,9,1
                          ,1,8,9,4,3,7,2,6,5};

        int[] Board = new int[81];
        int[] Solution { get; set; }
        public Generator() {
            this.Solution = Reference;
        }

        public int[] GenerateSolution() {
            //TODO make this shit actually generate a solution
            return Solution;
        }
        public int[] GenerateBoard()
        {
            //Returns only the shown numbers at the start of a game.
            //The solution contains all 81 numbers
            //This method is done after generating the solution, which is the whole solved board

            List<int> showed_number_indices = new List<int>();
            for (int i = 0; i < 80; i++) {
                int next = getRandom();
                do
                {
                    next = getRandom();
                } while (showed_number_indices.Contains(next));
                showed_number_indices.Add(next);
            }

            for (int i = 0; i < 81; i++) {
                if (showed_number_indices.Contains(i))
                {
                    Board[i] = Solution[i];
                }
                else {
                    //0 for a hidden number
                    Board[i] = 0;
                }
            }
            return Board;
        }

        private int getRandom() {
            var rand = new Random();
            return rand.Next(0, 81);
        }
    }
}
