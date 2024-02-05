using System;
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

    public int[,] ExpendableBoard()
    {
        int[,] expendableBoard = new int[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                expendableBoard[i, j] = board[i, j];
            }
        }
        return expendableBoard;
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
    static int[] FindEmptyCell(Sudoku sudoku)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (sudoku.ExpendableBoard()[i, j] == 0)
                {
                    return new int[] { i, j };
                }
            }
        }
        return null;
    }

    static void SolveUsingStack(Sudoku initialBoard, Stack<Sudoku> stack)
    {
        stack.Push(initialBoard);

        while (stack.Count > 0)
        {
            Sudoku currentBoard = stack.Pop();

            if (currentBoard.IsBoardSolved())
            {
                Console.WriteLine("The given Sudoku board has been successfully solved using stack! Here is the outcome: ");
                currentBoard.PrintBoard();
                return;
            }

            int[] emptyCell = FindEmptyCell(currentBoard);

            for (int num = 1; num <= 9; num++)
            {
                if (currentBoard.IsValidMove(emptyCell[0], emptyCell[1], num))
                {
                    int[,] newBoard = currentBoard.ExpendableBoard();
                    newBoard[emptyCell[0], emptyCell[1]] = num;
                    Sudoku newSudokuBoard = new Sudoku(newBoard);
                    stack.Push(newSudokuBoard);
                }
            }
        }

        Console.WriteLine("No solution found.");
    }

    static void SolveUsingLinkedListQueue(Sudoku initialBoard, Queue<Sudoku> queue)
    {
        queue.Enqueue(initialBoard);

        while (queue.Count > 0)
        {
            Sudoku currentBoard = queue.Dequeue();

            if (currentBoard.IsBoardSolved())
            {
                Console.WriteLine("The given Sudoku board has been successfully solved using queues! Here is the outcome: ");
                currentBoard.PrintBoard();
                return;
            }

            int[] emptyCell = FindEmptyCell(currentBoard);

            for (int num = 1; num <= 9; num++)
            {
                if (currentBoard.IsValidMove(emptyCell[0], emptyCell[1], num))
                {
                    int[,] newBoard = currentBoard.ExpendableBoard();
                    newBoard[emptyCell[0], emptyCell[1]] = num;
                    Sudoku newSudokuBoard = new Sudoku(newBoard);
                    queue.Enqueue(newSudokuBoard);
                }
            }
        }

        Console.WriteLine("No solution found.");
    }
    static void Main(string[] args)
    {
        string filename = args[0];
        int dataStructureType = int.Parse(args[1]);

        if (dataStructureType != 1 && dataStructureType != 2)
        {
            Console.WriteLine("Invalid data structure type. Please choose 1 for Stack or 2 for Queue.");
            return;
        }

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
                        if (values.Length != 9)
                        {
                            Console.WriteLine($"Error: Line {i + 1} does not contain exactly 9 integers separated by a space.");
                            return null; 
                        }

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
        Sudoku board = new Sudoku(initialBoard);

        Queue<Sudoku> queue = new Queue<Sudoku>();
        Stack<Sudoku> stack = new Stack<Sudoku>();

        switch (dataStructureType)
        {
            case 1:
                SolveUsingStack(board, stack);
                break;
            case 2:
                SolveUsingLinkedListQueue(board, queue);
                break;
            default:
                Console.WriteLine("Incorrect data type.");
                break;
        }
    }
}
