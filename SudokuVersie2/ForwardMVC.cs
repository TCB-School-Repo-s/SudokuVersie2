﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuVersie2
{
    internal class ForwardMVC : SudokuSolver
    {

        public ForwardMVC(Cell[,] puzzle)
        {
            this.puzzle = puzzle;
        }

        public static ForwardMVC FromString(string str)
        {
            ForwardMVC solver = new ForwardMVC(new Cell[9, 9]);

            string[] values = str.Split(' ');
            int index = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int value = int.Parse(values[index]);
                    bool isFixed = (value != 0);
                    List<int> list = new List<int>() { };
                    list.AddRange(Enumerable.Range(1, 9));
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
                if (puzzle[row, x].vast)
                {
                    puzzle[row, col].Domain.Remove(puzzle[row, x].val);
                }
                if (puzzle[x, col].vast)
                {
                    puzzle[row, col].Domain.Remove(puzzle[x, col].val);
                }
            }

            // Check the 3x3 part of the sudoku for the same value
            int x_l = (row / 3) * 3;
            int y_l = (col / 3) * 3;

            int x_r = x_l + 2;
            int y_r = y_l + 2;

            for (int r = x_l; r < x_r; r++)
            {
                for (int c = y_l; c < y_r; c++)
                {
                    if (this.puzzle[r, c].vast)
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

            int x_r = x_l + 2;
            int y_r = y_l + 2;

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

        /*private List<(int, int)> MVCSort()
        {
            List<(int, int)> vars = new List<(int, int)>();

            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    vars.Add((i,j));
                }
            }

            vars.Sort((a,b) => )


        }*/

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

            foreach (int val in puzzle[row, col].Domain)
            {
                if (ConstraintCheck(row, col, val))
                {
                    puzzle[row, col].val = val;

                    if (SolveSudoku())
                    {
                        return true;
                    }


                    puzzle[row, col].val = 0;
                }
            }
            return false;
        }
    }
}