using System.Diagnostics;

namespace SudokuVersie2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Hi, I am your Sudoku Solver Assistant! Please input your sudoku, make sure the numbers are seperated by spaces and there are no spaces in front or after the string: ");
            string sudoku = Console.ReadLine();

            ForwardChecking sud = ForwardChecking.FromString(sudoku);

            Console.WriteLine("This is your current sudoku input:");
            sud.Print();

            Console.WriteLine("I will now attempt to solve the sudoku...");
            stopwatch.Start();

            bool hey = sud.SolveSudoku();
            if (hey)
            {
                stopwatch.Stop();
                Console.WriteLine($"I have found a solution! It took me: {stopwatch.ElapsedMilliseconds}ms");
                sud.Print();
            }
            else
            {
                stopwatch.Stop();
                Console.WriteLine("I am very sorry, the sudoku appears to not be solveable :(");
            }

        }
    }
}