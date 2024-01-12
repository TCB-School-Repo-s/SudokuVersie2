using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuVersie2
{
    internal class CBT
    {

        /*private Sudoku sud;
        private (int, int) currentCell;

        public CBT(Sudoku sud)
        {
            this.sud = sud;
            this.currentCell = (0, 0);
        }

        public Sudoku Sudoku
        {
            get { return sud; }
        }


        public void SolveSudoku()
        {
            if(currentCell.Item1 == 8 && currentCell.Item2 == 8)
            {
                return;
            }

            if (sud.Puzzle[currentCell.Item1, currentCell.Item2].Item2 == true)
            {
                currentCell = (currentCell.Item2 != 8) ? (currentCell.Item1, currentCell.Item2++) : (currentCell.Item1++, 0);
                SolveSudoku();
            }
            // solve the sudoku
            for (int i = 1; i <= 9; i++)
            {
                bool hallo = ConstraintSatisfied(currentCell, i);
                if (hallo)
                {
                    currentCell = (currentCell.Item2 != 8) ? (currentCell.Item1, currentCell.Item2++) : (currentCell.Item1++, 0);
                    break;
                }
            }
            SolveSudoku();
        }

        public bool ConstraintSatisfied((int, int) cell, int val)
        {
            for (int i = 0; i < 9; i++)
            {
                if (sud.Puzzle[currentCell.Item1, i].Item1 == val || sud.Puzzle[i, currentCell.Item2].Item1 == val)
                {
                    return false;
                }
            }
            
            int block = cell.Item1 - cell.Item2 / 3;

            for(int j = 0; j<9; j++)
            {
                var cellT = sud.Puzzle[block / 3 * 3 + j / 3, block % 3 * 3 + j % 3];
                if(cellT.Item1 == val)
                {
                    return false;
                }
            }

            return true;
        }
        */

    }
}
