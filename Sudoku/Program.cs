using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class Sudoku
{
    private int[,] board;

    public Sudoku(int[,] initialBoard)
    {
        board = initialBoard;
    }

    public bool IsValidMove(int row, int col, int num)
    {

        for (int i = 0; i < 9; i++)
        {
            if (board[row, i] == num)
            {
                return false; 
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (board[i, col] == num)
            {
                return false; 
            }
        }

        int initalRow = row - row % 3;
        int initalCol = col - col % 3;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[initalRow + i, initalCol + j] == num)
                {
                    return false; 
                }
            }
        }

        return true; 
    }

    public int[,] CloneBoard()
    {
        int[,] clonedBoard = new int[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                clonedBoard[i, j] = board[i, j];
            }
        }
        return clonedBoard;
    }

    public bool IsBoardSolved()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board[i, j] == 0)
                {
                    return false; 
                }
            }
        }
        return true; 
    }

    public void PrintBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

class Solver
{
    static void Main(string[] args)
    {
        string filename = args[0];
        int dataStructureType = int.Parse(args[1]);

        static int[,] FileReader(string filename)
        {
            int[,] sudokuBoard = new int[9, 9];

            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    for (int i = 0; i < 9; i++)
                    {
                        string line = reader.ReadLine();

                        string[] values = line.Split(' ');

                        for (int j = 0; j < 9; j++)
                        {
                            sudokuBoard[i, j] = int.Parse(values[j]);
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"Error reading file: {e.Message}");
            }

            return sudokuBoard;
        }

        int[,] initialBoard = FileReader(filename);
        Sudoku sudoku = new Sudoku(initialBoard);

        Queue<Sudoku> queue = new Queue<Sudoku>();
        Stack<Sudoku> stack = new Stack<Sudoku>();

        switch (dataStructureType)
        {
            case 1:
                SolveWithStackClass(sudoku, stack);
                break;
            case 2:
                SolveWithLinkedListQueue(sudoku, queue);
                break;
            default:
                Console.WriteLine("Invalid data structure type.");
                break;
        }
    }

    static void SolveWithStackClass(Sudoku initialSudoku, Stack<Sudoku> stack)
    {
        stack.Push(initialSudoku);

        while (stack.Count > 0)
        {
            Sudoku currentSudoku = stack.Pop();

            if (currentSudoku.IsBoardSolved())
            {
                Console.WriteLine("Sudoku Solved!");
                currentSudoku.PrintBoard();
                return;
            }

            int[] emptyCell = FindEmptyCell(currentSudoku);

            for (int num = 1; num <= 9; num++)
            {
                if (currentSudoku.IsValidMove(emptyCell[0], emptyCell[1], num))
                {
                    int[,] newBoard = currentSudoku.CloneBoard();
                    newBoard[emptyCell[0], emptyCell[1]] = num;
                    Sudoku newSudoku = new Sudoku(newBoard);
                    stack.Push(newSudoku);
                }
            }
        }

        Console.WriteLine("No solution found for the Sudoku puzzle.");
    }

    static void SolveWithLinkedListQueue(Sudoku initialSudoku, Queue<Sudoku> queue)
    {
        queue.Enqueue(initialSudoku);

        while (queue.Count > 0)
        {
            Sudoku currentSudoku = queue.Dequeue();

            if (currentSudoku.IsBoardSolved())
            {
                Console.WriteLine("Sudoku Solved!");
                currentSudoku.PrintBoard();
                return;
            }

            // Find the first empty cell
            int[] emptyCell = FindEmptyCell(currentSudoku);

            // Try placing numbers in the empty cell
            for (int num = 1; num <= 9; num++)
            {
                if (currentSudoku.IsValidMove(emptyCell[0], emptyCell[1], num))
                {
                    int[,] newBoard = currentSudoku.CloneBoard();
                    newBoard[emptyCell[0], emptyCell[1]] = num;
                    Sudoku newSudoku = new Sudoku(newBoard);
                    queue.Enqueue(newSudoku);
                }
            }
        }

        Console.WriteLine("No solution found for the Sudoku puzzle.");
    }

    static int[] FindEmptyCell(Sudoku sudoku)
    {
        // Find the first empty cell in the Sudoku grid
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (sudoku.CloneBoard()[i, j] == 0)
                {
                    return new int[] { i, j };
                }
            }
        }
        return null; // All cells are filled
    }
}
