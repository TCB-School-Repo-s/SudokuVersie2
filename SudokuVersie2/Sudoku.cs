using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuVersie2
{
    public abstract class SudokuSolver
    {

        protected (int, Boolean)[,] puzzle = new (int, Boolean)[9, 9];

        public (int, Boolean)[,] Puzzle
        {
            get { return puzzle; }
        }

        public abstract bool SolveSudoku();


        /// <summary>
        /// Prints the Sudoku board.
        /// </summary>
        public void Print()
        {
            Console.WriteLine("*|-A-|-B-|-C-|-D-|-E-|-F-|-G-|-H-|-I-|*");
            Console.WriteLine($"0| {puzzle[0, 0].Item1} | {puzzle[0, 1].Item1} | {puzzle[0, 2].Item1} | {puzzle[0, 3].Item1} | {puzzle[0, 4].Item1} | {puzzle[0, 5].Item1} | {puzzle[0, 6].Item1} | {puzzle[0, 7].Item1} | {puzzle[0, 8].Item1} |");
            Console.WriteLine(" |---|---|---|---|---|---|---|---|---|");
            Console.WriteLine($"1| {puzzle[1, 0].Item1} | {puzzle[1, 1].Item1} | {puzzle[1, 2].Item1} | {puzzle[1, 3].Item1} | {puzzle[1, 4].Item1} | {puzzle[1, 5].Item1} | {puzzle[1, 6].Item1} | {puzzle[1, 7].Item1} | {puzzle[1, 8].Item1} |");
            Console.WriteLine(" |---|---|---|---|---|---|---|---|---|");
            Console.WriteLine($"2| {puzzle[2, 0].Item1} | {puzzle[2, 1].Item1} | {puzzle[2, 2].Item1} | {puzzle[2, 3].Item1} | {puzzle[2, 4].Item1} | {puzzle[2, 5].Item1} | {puzzle[2, 6].Item1} | {puzzle[2, 7].Item1} | {puzzle[2, 8].Item1} |");
            Console.WriteLine(" |---|---|---|---|---|---|---|---|---|");
            Console.WriteLine($"3| {puzzle[3, 0].Item1} | {puzzle[3, 1].Item1} | {puzzle[3, 2].Item1} | {puzzle[3, 3].Item1} | {puzzle[3, 4].Item1} | {puzzle[3, 5].Item1} | {puzzle[3, 6].Item1} | {puzzle[3, 7].Item1} | {puzzle[3, 8].Item1} |");
            Console.WriteLine(" |---|---|---|---|---|---|---|---|---|");
            Console.WriteLine($"4| {puzzle[4, 0].Item1} | {puzzle[4, 1].Item1} | {puzzle[4, 2].Item1} | {puzzle[4, 3].Item1} | {puzzle[4, 4].Item1} | {puzzle[4, 5].Item1} | {puzzle[4, 6].Item1} | {puzzle[4, 7].Item1} | {puzzle[4, 8].Item1} |");
            Console.WriteLine(" |---|---|---|---|---|---|---|---|---|");
            Console.WriteLine($"5| {puzzle[5, 0].Item1} | {puzzle[5, 1].Item1} | {puzzle[5, 2].Item1} | {puzzle[5, 3].Item1} | {puzzle[5, 4].Item1} | {puzzle[5, 5].Item1} | {puzzle[5, 6].Item1} | {puzzle[5, 7].Item1} | {puzzle[5, 8].Item1} |");
            Console.WriteLine(" |---|---|---|---|---|---|---|---|---|");
            Console.WriteLine($"6| {puzzle[6, 0].Item1} | {puzzle[6, 1].Item1} | {puzzle[6, 2].Item1} | {puzzle[6, 3].Item1} | {puzzle[6, 4].Item1} | {puzzle[6, 5].Item1} | {puzzle[6, 6].Item1} | {puzzle[6, 7].Item1} | {puzzle[6, 8].Item1} |");
            Console.WriteLine(" |---|---|---|---|---|---|---|---|---|");
            Console.WriteLine($"7| {puzzle[7, 0].Item1} | {puzzle[7, 1].Item1} | {puzzle[7, 2].Item1} | {puzzle[7, 3].Item1} | {puzzle[7, 4].Item1} | {puzzle[7, 5].Item1} | {puzzle[7, 6].Item1} | {puzzle[7, 7].Item1} | {puzzle[7, 8].Item1} |");
            Console.WriteLine(" |---|---|---|---|---|---|---|---|---|");
            Console.WriteLine($"8| {puzzle[8, 0].Item1} | {puzzle[8, 1].Item1} | {puzzle[8, 2].Item1} | {puzzle[8, 3].Item1} | {puzzle[8, 4].Item1} | {puzzle[8, 5].Item1} | {puzzle[8, 6].Item1} | {puzzle[8, 7].Item1} | {puzzle[8, 8].Item1} |");
            Console.WriteLine("*|---|---|---|---|---|---|---|---|---|");
        }
    }
}
