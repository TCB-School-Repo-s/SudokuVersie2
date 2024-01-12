using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuVersie2
{
    internal class Sudoku
    {

        private (int, Boolean)[,] puzzle = new (int, Boolean)[9, 9];
        private int timeTaken = 0;

        public Sudoku(int[,] puzzle)
        {
            puzzle = puzzle;
        }


        public (int, Boolean)[,] Puzzle
        {
            get { return puzzle; }
        }


        //// <summary>
        /// Method <c>FromString</c> generates a sudoku object from a string.
        /// </summary>
        /// <param name="str">The string to generate sudoku from</param>
        /// <returns>Sudoku object</returns>
        public static Sudoku FromString(string str)
        {
            Sudoku sudoku = new Sudoku(new int[9, 9]);

            string[] values = str.Split(' ');
            int index = 0;

            // fill in the numbers in each row, and add a bool to show whether it is a fixed or swappable number.
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int value = int.Parse(values[index]);
                    bool isFixed = (value != 0); // If the value is not 0, it's fixed
                    sudoku.Puzzle[i, j] = (value, isFixed);
                    index++;
                }
            }

            return sudoku;
        }

        // prints the entire sudoku with axis to help determine where potential issues were.
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
