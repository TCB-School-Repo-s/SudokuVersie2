using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuVersie2
{

    internal class Block
    {
        public int x;
        public int y;

        public Block(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    internal class ChronologicalBacktracker : SudokuSolver
    {

        private Block currentBlock = new Block(0, 0);

        public ChronologicalBacktracker((int, Boolean)[,] puzzle)
        {
            this.puzzle = puzzle; 
        }

        public static ChronologicalBacktracker FromString(string str)
        {
            ChronologicalBacktracker solver = new ChronologicalBacktracker(new (int, Boolean)[9, 9]);

            string[] values = str.Split(' ');
            int index = 0;

            for(int i = 0; i< 9 ; i++)
            {
                for(int j = 0; j < 9 ; j++)
                {
                    int value = int.Parse(values[index]);
                    bool isFixed = (value != 0);
                    solver.puzzle[i, j] = (value, isFixed);
                    index++;
                }
            }

            return solver;
        }

        public Block Continue(Block block)
        {
            int x = block.x;
            int y = block.y;

            if(block.x == 8) {
                x = 0;
                y++;
            }
            else
            {
                x++;
            }

            if(y > 8)
            {
                y = 8;
                x = 8;
            }

            return new Block(x, y);


        }

        public bool ConstraintCheck(Block block, int val)
        {
            for (int i = 0; i < 9; i++)
            {
                if (this.puzzle[block.x, i].Item1 == val || this.puzzle[i, block.y].Item1 == val)
                {
                    return false;
                }
            }

            // Check the 3x3 part of the sudoku for the same value
            int x_l = (block.x / 3) * 3;
            int y_l = (block.y / 3) * 3;

            int x_r = x_l + 2;
            int y_r = y_l + 2;

            for (int row = x_l; row < x_r; row++)
            {
                for(int col = y_l; col < y_r; col++)
                {
                    if (this.puzzle[row, col].Item1 == val)
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        public bool IsSolved(out int row, out int col)
        {
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                        return true;
                }
            }
            row = col = -1;
            return false;
        }

        private Block Backtrack(Block block)
        {
            int x = block.x;
            int y = block.y;

            if(block.x == 0)
            {
                x = 8;
                y--;
            }else
            {
                x--;
            }

            if(y < 0)
            {
                y = 0;
                x = 0;
            }

            return new Block(x, y);
        }

        public override void SolveSudoku()
        {
            if (currentBlock.x == 8 && currentBlock.y == 8)
            {
                return;
            }

            if (this.puzzle[currentBlock.x, currentBlock.y].Item2 == true)
            {
                currentBlock = Continue(currentBlock);
                SolveSudoku();
                return;
            }

            for(int i = 1; i <= 9 ; i++)    
            {
                bool constr = ConstraintCheck(currentBlock, i);
                if (constr)
                {
                    this.puzzle[currentBlock.x, currentBlock.y].Item1 = i;
                    currentBlock = Continue(currentBlock);

                    SolveSudoku();

                    if (currentBlock.x != 8 | currentBlock.y != 8)
                    {
                        this.puzzle[currentBlock.x, currentBlock.y].Item1 = 0;
                        currentBlock = Backtrack(currentBlock);
                        if (this.puzzle[currentBlock.x, currentBlock.y].Item2 == true)
                        {
                            currentBlock = Backtrack(currentBlock);
                        }
                    }
                }

            }
            return;
        }
    }
}
