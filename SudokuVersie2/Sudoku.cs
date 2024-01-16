using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SudokuVersie2
{

    public class Cell
    { 
        public int val;
        public bool vast;
        public List<int> Domain;

        public Cell(int val, bool vast, List<int> domain)
        {
            this.val = val;
            this.vast = vast;
            this.Domain = domain;
        }
    }

    public abstract class SudokuSolver
    {

        protected Cell[,] puzzle = new Cell[9, 9];

        public Cell[,] Puzzle
        {
            get { return puzzle; }
        }

        public abstract bool SolveSudoku();


        /// <summary>
        /// Prints the Sudoku board.
        /// </summary>
        public void Print()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*|-A-|-B-|-C-|-D-|-E-|-F-|-G-|-H-|-I-|*");

            for (int row = 0; row < 9; row++)
            {
                sb.Append($"{row}|");

                for (int col = 0; col < 9; col++)
                {
                    sb.Append($" {puzzle[row, col].val} |");
                }

                sb.AppendLine("\n |---|---|---|---|---|---|---|---|---|");
            }

            Console.WriteLine(sb.ToString());
        }

    }
}
