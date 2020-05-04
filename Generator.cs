using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Generator
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
        public int[] Solution { get; set; }
        int[,] mat = new int[9,9];
        public Generator() {
            Solution = new int[81];
            GenerateSolution();
        }


        public void GenerateSolution() {
            FillDiagonal();
            FillRemaining(0, 3);
            TranslateSolution();
        }

        void FillDiagonal() {
            for(int i = 0; i < 9; i += 3){
                FillBox(i, i);
            }
        }

        Boolean UnUsedInBox(int row, int col, int num)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (mat[row + i, col + j] == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        void FillBox(int row, int col) {
            int num;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    do
                    {
                        num = getRandom9();
                    } while (!UnUsedInBox(row, col, num));
                    mat[row + i, col + j] = num;
                }
            }
        }

        Boolean IsSafe(int i, int j, int num) {
            return (UnUsedInRow(i, num) && UnUsedInCol(j, num) && UnUsedInBox(i - i % 3, j - j % 3, num));
        }

        Boolean UnUsedInRow(int i, int num) {
            for (int j = 0; j < 9; j++) {
                if (mat[i, j] == num) {
                    return false;
                }
            }
            return true;
        }
        Boolean UnUsedInCol(int j, int num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (mat[i, j] == num)
                {
                    return false;
                }
            }
            return true;
        }

        Boolean FillRemaining(int i, int j)
        {
            //  System.out.println(i+" "+j); 
            if (j >= 9 && i < 9 - 1)
            {
                i = i + 1;
                j = 0;
            }
            if (i >= 9 && j >= 9)
                return true;

            if (i < 3)
            {
                if (j < 3)
                    j = 3;
            }
            else if (i < 9 - 3)
            {
                if (j == (int)(i / 3) * 3)
                    j = j + 3;
            }
            else
            {
                if (j == 9 - 3)
                {
                    i = i + 1;
                    j = 0;
                    if (i >= 9)
                        return true;
                }
            }

            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(i, j, num))
                {
                    mat[i,j] = num;
                    if (FillRemaining(i, j + 1))
                        return true;

                    mat[i,j] = 0;
                }
            }
            return false;
        }

        void TranslateSolution() {
            //Converts solution from a matrix to a 1D array
            int index = 0;
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    Solution[index] = mat[i, j];
                    index++;
                }
            }
        }

        public int[] GenerateBoard()
        {
            //Returns only the shown numbers at the start of a game.
            //The solution contains all 81 numbers
            //This method is done after generating the solution, which is the whole solved board

            List<int> showed_number_indices = new List<int>();
            for (int i = 0; i < 80; i++) {
                int next = getRandom81();
                do
                {
                    next = getRandom81();
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

        private int getRandom81() {
            var rand = new Random();
            return rand.Next(0, 81);
        }

        private int getRandom9()
        {
            var rand = new Random();
            return rand.Next(1, 10);
        }

    }
}
