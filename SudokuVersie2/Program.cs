using System.Diagnostics;

namespace SudokuVersie2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi, I am your Sudoku Solver Assistant! Please input your sudoku, make sure the numbers are seperated by spaces and there are no spaces in front or after the string: ");
            string sudoku = Console.ReadLine();

            var options = new List<MenuOption>()
            {
                new MenuOption("Chronological Backtracking", () => ChronologicalBacktrackingAction(ChronologicalBacktracker.FromString(sudoku))),
                new MenuOption("Forward Checking without MVC", () => ForwardCheckingAction(ForwardChecking.FromString(sudoku))),
                new MenuOption("Forward Checking with MVC", () => ForwardCheckingMVCAction(ForwardMCV.FromString(sudoku)))
            };

            int index = 0;

            PrintMenu(options, options[index]);

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        PrintMenu(options, options[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        PrintMenu(options, options[index]);
                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

        }

        static void ForwardCheckingAction(ForwardChecking sud) {
            Stopwatch stopwatch = new Stopwatch();

            Console.Clear();

            Console.WriteLine("This is your current sudoku input:");
            sud.Print();

            Console.WriteLine("I will now attempt to solve the sudoku...");
            stopwatch.Start();

            bool hey = sud.SolveSudoku();
            if (hey)
            {
                stopwatch.Stop();
                Console.WriteLine($"I have found a solution! It took me: {stopwatch.ElapsedMilliseconds}ms or {stopwatch.ElapsedTicks} ticks");
                sud.Print();
            }
            else
            {
                stopwatch.Stop();
                Console.WriteLine("I am very sorry, the sudoku appears to not be solveable :(");
            }

            Environment.Exit(0);

        }

        static void ForwardCheckingMVCAction(ForwardMCV sud)
        {
            Stopwatch stopwatch = new Stopwatch();

            Console.Clear();

            Console.WriteLine("This is your current sudoku input:");
            sud.Print();

            Console.WriteLine("I will now attempt to solve the sudoku...");
            stopwatch.Start();

            List<(int, int)> doms = sud.GetSortedDomains();
            bool hey = sud.SolveSudokuMCV(doms);
            if (hey)
            {
                stopwatch.Stop();
                Console.WriteLine($"I have found a solution! It took me: {stopwatch.ElapsedMilliseconds}ms or {stopwatch.ElapsedTicks} ticks");
                sud.Print();
            }
            else
            {
                stopwatch.Stop();
                Console.WriteLine("I am very sorry, the sudoku appears to not be solveable :(");
            }

            Environment.Exit(0);

        }

        static void ChronologicalBacktrackingAction(ChronologicalBacktracker sud)
        {
            Stopwatch stopwatch = new Stopwatch();

            Console.Clear();

            Console.WriteLine("This is your current sudoku input:");
            sud.Print();

            Console.WriteLine("I will now attempt to solve the sudoku...");
            stopwatch.Start();

            bool hey = sud.SolveSudoku();
            if (hey)
            {
                stopwatch.Stop();
                Console.WriteLine($"I have found a solution! It took me: {stopwatch.ElapsedMilliseconds}ms or {stopwatch.ElapsedTicks} ticks");
                sud.Print();
            }
            else
            {
                stopwatch.Stop();
                Console.WriteLine("I am very sorry, the sudoku appears to not be solveable :(");
            }

            Environment.Exit(0);
        }

        static void PrintMenu(List<MenuOption> opts, MenuOption selOpt)
        {
            Console.Clear();

            Console.WriteLine("Great, Now select which algorithm you want to use:");

            foreach (MenuOption opt in opts)
            {
                if(opt == selOpt) {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(opt.Name);
            }
        }


        public class MenuOption
        {
            public string Name { get; }
            public Action Selected { get; }

            public MenuOption(string name, Action selected)
            {
                Name = name;
                Selected = selected;
            }
        }
    }
}