using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuVersie2
{
    internal class ForwardMCV : SudokuSolver
    {

        public ForwardMCV(Cell[,] puzzle)
        {
            this.puzzle = puzzle;
        }

        public static ForwardMCV FromString(string str)
        {
            ForwardMCV solver = new ForwardMCV(new Cell[9, 9]);

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
            //solver.Print();

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
        /// The function <c>updateDomainBackwards</c> removes the number from the domain of the affected cells and stores them in dictionary for backtracking.
        /// </summary>
        /// <param name="row">The row of the changed cell</param>
        /// <param name="col">The column of the changed cell</param>
        /// <param name="num">Value that the cell got</param>
        /// <returns>A dictionary containing three dictionaries, each containing the cell locations and removed number of the domain.</returns>
        private Dictionary<string, Dictionary<(int, int), int>> updateDomainsForward(int row, int col, int num)
        {

            Dictionary<string, Dictionary<(int, int), int>> dict = new Dictionary<string, Dictionary<(int, int), int>>(); // Init the dictionary

            // Init the sub dictionaries
            Dictionary<(int, int), int> rrow = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> ccol = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> block = new Dictionary<(int, int), int>();

            // Update the domains of affected cells after making a move
            for (int x = 0; x < 9; x++)
            {
                if (puzzle[row, x].Domain.Remove(num)) // If the number is removed from the domain, add it to the dictionary
                {
                    rrow.Add((row, x), num);
                }
                if (puzzle[x, col].Domain.Remove(num))
                {
                    ccol.Add((x, col), num);
                }
            }

            // Check the 3x3 part of the sudoku
            int x_l = (row / 3) * 3;
            int y_l = (col / 3) * 3;

            int x_r = x_l + 3;
            int y_r = y_l + 3;

            for (int r = x_l; r < x_r; r++)
            {
                for (int c = y_l; c < y_r; c++)
                {
                    if (puzzle[r, c].Domain.Remove(num))
                    {
                        block.Add((r, c), num);
                    }
                }
            }

            dict.Add("row", rrow);
            dict.Add("col", ccol);
            dict.Add("block", block);

            return dict;
        }

        /// <summary>
        /// The function <c>updateDomainBackwards</c> uses a dictionary to restore the domains to what it was after updating them.
        /// </summary>
        /// <param name="dict">Dictionary containing three dictionarys: row, col and block</param>
        private void updateDomainsBackward(Dictionary<string, Dictionary<(int, int), int>> dict)
        {
            foreach (string index in dict.Keys) // Iterate over the 3 dictionaries
            {
                foreach ((int, int) x in dict[index].Keys)
                { // Iterate over the items in that dictionary
                    puzzle[x.Item1, x.Item2].Domain.Add(dict[index][x]); // Add it back into the domain
                    puzzle[x.Item1, x.Item2].Domain.Sort(); // Sort the domain in chronological order
                }
            }
        }

        /// <summary>
        /// Solves the Sudoku puzzle using Chronological Backtracking.
        /// </summary>
        /// <returns>True if a solution is found, otherwise false.</returns>
        public bool SolveSudokuMCV(List<(int, int)> doms)
        {
            int row, col;

            if (!IsSolved(out row, out col))
            {
                return true;
            }

            row = doms[0].Item1;
            col = doms[0].Item2;
           
            //Console.WriteLine($"{row}, {col}");

            foreach(int num in puzzle[row, col].Domain.ToList())
            {
                puzzle[row, col].val = num;
                var dict = updateDomainsForward(row, col, num);
                var new_doms = GetSortedDomains();

                if (SolveSudokuMCV(new_doms))
                {
                    return true;
                }

                puzzle[row, col].val = 0;
                updateDomainsBackward(dict);
                //puzzle[row, col].Domain.Remove(num);

            }
            

            return false;

        }

        public List<(int, int)> GetSortedDomains()
        {
            List<(int, int)> sort = new List<(int, int)>();

            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (puzzle[i,j].val == 0)
                    {
                        sort.Add((i, j));
                    }
                }
            }

            sort.Sort((a, b) => puzzle[a.Item1, a.Item2].Domain.Count.CompareTo(puzzle[b.Item1, b.Item2].Domain.Count));

            return sort;
        }

        public override bool SolveSudoku()
        {
            throw new NotImplementedException();
        }
    }

}
