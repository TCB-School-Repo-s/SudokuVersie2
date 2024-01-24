using System.Diagnostics;
using Microsoft.Data.Analysis;



namespace SudokuVersie2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Experiment code
            /*var timeList = new List<Single>() { 0 };
            var timeTickList = new List<Single>() { 0 };
            // To run the code below you need to install the Microsoft.Data.Analysis; package.
            var timeColumn = new SingleDataFrameColumn("timeTakenMs", timeList);
            var timeColum2 = new SingleDataFrameColumn("timeTakenTicks", timeTickList);
            var dataframe = new DataFrame(timeColumn, timeColum2);

            string sud = "0 2 0 8 1 0 7 4 0 7 0 0 0 0 3 1 0 0 0 9 0 0 0 2 8 0 5 0 0 9 0 4 0 0 8 7 4 0 0 2 0 8 0 0 3 1 6 0 0 3 0 2 0 0 3 0 2 7 0 0 0 6 0 0 0 5 6 0 0 0 0 8 0 7 6 0 5 1 0 9 0";

            List<int> ms = new List<int>();
            List<int> ts = new List<int>();

            for(int i = 0; i < 100; i++)
            {
                ForwardMCV sudoku = ForwardMCV.FromString(sud);

                Stopwatch stopwatch = Stopwatch.StartNew();

                List<(int, int)> doms = sudoku.GetSortedDomains();

                stopwatch.Start();
                sudoku.SolveSudokuMCV(doms);
                stopwatch.Stop();

                Console.WriteLine($"Iteration: {i}, time taken: {stopwatch.ElapsedMilliseconds}ms or {stopwatch.ElapsedTicks}ts");


                List<KeyValuePair<string, object>> newRowData = new()
                {

                    new KeyValuePair<string, object>("timeTakenMs", stopwatch.ElapsedMilliseconds),
                    new KeyValuePair<string, object>("timeTakenTicks", stopwatch.ElapsedTicks),
                };

                dataframe.Append(newRowData, inPlace: true);
            }

            DataFrame.SaveCsv(dataframe, $"ResultMCVSudoku5.csv", ';');*/

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