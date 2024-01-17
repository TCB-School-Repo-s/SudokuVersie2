using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuVersie2
{
    internal class ForwardChecking : SudokuSolver
    {

        public ForwardChecking(Cell[,] puzzle)
        {
            this.puzzle = puzzle;
        }

        public static ForwardChecking FromString(string str)
        {
            ForwardChecking solver = new ForwardChecking(new Cell[9, 9]);

            string[] values = str.Split(' ');
            int index = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int value = int.Parse(values[index]);
                    bool isFixed = (value != 0);
                    List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    solver.puzzle[i, j] = new Cell(value, isFixed, list);
                    index++;
                }
            }

            for (int p = 0; p < 9; p++)
            {
                for (int q = 0; q < 9; q++)
                {
                    if (!solver.puzzle[p, q].vast)
                    {
                        solver.updateDomain(p, q);
                    }
                }
            }
            solver.Print();

            return solver;
        }

        /// <summary>
        /// Checks if the sudoku is solved by searching for an 'empty' location on the board.
        /// </summary>
        /// <param name="row">The row index of the empty location.</param>
        /// <param name="col">The column index of the empty location.</param>
        /// <returns>True if an empty location is found, otherwise false.</returns>
        public bool IsSolved(out int row, out int col)
        {
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (puzzle[row, col].val == 0)
                        return true;
                }
            }
            row = col = -1;
            return false;
        }



        public void updateDomain(int row, int col)
        {
            // Check if 'num' is not in the current row and column
            for (int x = 0; x < 9; x++)
            {
                if (puzzle[row, x].val != 0)
                {
                    puzzle[row, col].Domain.Remove(puzzle[row, x].val);
                }
                if (puzzle[x, col].val != 0)
                {
                    puzzle[row, col].Domain.Remove(puzzle[x, col].val);
                }
            }

            // Check the 3x3 part of the sudoku for the same value
            int x_l = (row / 3) * 3;
            int y_l = (col / 3) * 3;

            int x_r = x_l + 3;
            int y_r = y_l + 3;

            for (int r = x_l; r < x_r; r++)
            {
                for (int c = y_l; c < y_r; c++)
                {
                    if (this.puzzle[r, c].val != 0)
                    {
                        puzzle[row, col].Domain.Remove(puzzle[r, c].val);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if it is safe to place a number in a particular cell.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="col">The column index of the cell.</param>
        /// <param name="val">The number to be placed.</param>
        /// <returns>True if it is safe to place the number, otherwise false.</returns>
        public bool ConstraintCheck(int row, int col, int val)
        {
            // Check if 'num' is not in the current row and column
            for (int x = 0; x < 9; x++)
            {
                if (puzzle[row, x].val == val || puzzle[x, col].val == val)
                    return false;
            }

            // Check the 3x3 part of the sudoku for the same value
            int x_l = (row / 3) * 3;
            int y_l = (col / 3) * 3;

            int x_r = x_l + 3;
            int y_r = y_l + 3;

            for (int r = x_l; r < x_r; r++)
            {
                for (int c = y_l; c < y_r; c++)
                {
                    if (this.puzzle[r, c].val == val)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private Dictionary<string, Dictionary<(int, int), List<int>>> updateDomainsForward(int row, int col, int num)
        {

            Dictionary<string, Dictionary<(int, int), List<int>>> dict = new Dictionary<string, Dictionary<(int, int), List<int>>>();

            Dictionary<(int, int), List<int>> rrow = new Dictionary<(int, int), List<int>> ();
            Dictionary<(int, int), List<int>> ccol = new Dictionary<(int, int), List<int>>();
            Dictionary<(int, int), List<int>> block = new Dictionary<(int, int), List<int>>();

            // Update the domains of affected cells after making a move
            for (int x = 0; x < 9; x++)
            {
                rrow.Add((row, x), puzzle[row, x].Domain);
                ccol.Add((x, col), puzzle[x, col].Domain);
                puzzle[row, x].Domain.Remove(num);
                puzzle[x, col].Domain.Remove(num);
            }

            // Update the 3x3 part of the sudoku for the same value
            int x_l = (row / 3) * 3;
            int y_l = (col / 3) * 3;

            int x_r = x_l + 3;
            int y_r = y_l + 3;

            for (int r = x_l; r < x_r; r++)
            {
                for (int c = y_l; c < y_r; c++)
                {
                    block.Add((r,c), puzzle[r,c].Domain);
                    puzzle[r, c].Domain.Remove(num); 
                }
            }

            dict.Add("row", rrow);
            dict.Add("col", ccol);
            dict.Add("block", block);

            return dict;
        }

        private void updateDomainsBackward(Dictionary<string, Dictionary<(int, int), List<int>>> dict)
        {
            foreach(string index in dict.Keys)
            {
                foreach((int, int) x in dict[index].Keys) {
                    puzzle[x.Item1, x.Item2].Domain = dict[index][x];
                }
            }
        }

        /// <summary>
        /// Solves the Sudoku puzzle using Chronological Backtracking.
        /// </summary>
        /// <returns>True if a solution is found, otherwise false.</returns>
        public override bool SolveSudoku()
        {
            int row, col;

            if (!IsSolved(out row, out col))
            {
                return true;
            }

            Console.WriteLine($"{row}, {col}");

            foreach (int num in puzzle[row, col].Domain)
            {
                puzzle[row, col].val = num;

                var dict = updateDomainsForward(row, col, num);

                if (SolveSudoku())
                {
                    return true;
                }

                puzzle[row, col].val = 0;
                updateDomainsBackward(dict);
            }

            return false;
        }
    }
}
