using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class Sudoku
{
    private int[,] grid;

    public Sudoku(int[,] initialGrid)
    {
        grid = initialGrid;
    }

    public bool IsSolved()
    {
        // Check if the Sudoku is solved
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (grid[i, j] == 0)
                {
                    return false; // There is an empty cell
                }
            }
        }
        return true; // All cells are filled
    }

    public bool IsValidMove(int row, int col, int num)
    {
        // Check if placing 'num' in the specified position is a valid move

        // Check the row
        for (int i = 0; i < 9; i++)
        {
            if (grid[row, i] == num)
            {
                return false; // Invalid move
            }
        }

        // Check the column
        for (int i = 0; i < 9; i++)
        {
            if (grid[i, col] == num)
            {
                return false; // Invalid move
            }
        }

        // Check the 3x3 square
        int startRow = row - row % 3;
        int startCol = col - col % 3;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[startRow + i, startCol + j] == num)
                {
                    return false; // Invalid move
                }
            }
        }

        return true; // Valid move
    }

    public void PrintGrid()
    {
        // Print the current state of the grid
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public int[,] CloneGrid()
    {
        int[,] clonedGrid = new int[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                clonedGrid[i, j] = grid[i, j];
            }
        }
        return clonedGrid;
    }
}

class Solver
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Solver <filename> <dataStructureType>");
            return;
        }

        string filename = args[0];
        int dataStructureType = int.Parse(args[1]);

        // Read Sudoku puzzle from file
        int[,] initialGrid = ReadSudokuFromFile(filename);
        Sudoku sudoku = new Sudoku(initialGrid);

        // Choose data structure based on input
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

            // Implement solving logic here
            if (currentSudoku.IsSolved())
            {
                Console.WriteLine("Sudoku Solved!");
                currentSudoku.PrintGrid();
                return;
            }

            // Find the first empty cell
            int[] emptyCell = FindEmptyCell(currentSudoku);

            // Try placing numbers in the empty cell
            for (int num = 1; num <= 9; num++)
            {
                if (currentSudoku.IsValidMove(emptyCell[0], emptyCell[1], num))
                {
                    int[,] newGrid = currentSudoku.CloneGrid();
                    newGrid[emptyCell[0], emptyCell[1]] = num;
                    Sudoku newSudoku = new Sudoku(newGrid);
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

            // Implement solving logic here
            if (currentSudoku.IsSolved())
            {
                Console.WriteLine("Sudoku Solved!");
                currentSudoku.PrintGrid();
                return;
            }

            // Find the first empty cell
            int[] emptyCell = FindEmptyCell(currentSudoku);

            // Try placing numbers in the empty cell
            for (int num = 1; num <= 9; num++)
            {
                if (currentSudoku.IsValidMove(emptyCell[0], emptyCell[1], num))
                {
                    int[,] newGrid = currentSudoku.CloneGrid();
                    newGrid[emptyCell[0], emptyCell[1]] = num;
                    Sudoku newSudoku = new Sudoku(newGrid);
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
                if (sudoku.CloneGrid()[i, j] == 0)
                {
                    return new int[] { i, j };
                }
            }
        }
        return null; // All cells are filled
    }

    static int[,] ReadSudokuFromFile(string filename)
    {
        int[,] sudokuGrid = new int[9, 9];

        try
        {
            string[] lines = File.ReadAllLines(filename);

            for (int i = 0; i < 9; i++)
            {
                string[] values = lines[i].Split(' ');

                for (int j = 0; j < 9; j++)
                {
                    sudokuGrid[i, j] = int.Parse(values[j]);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading file: {e.Message}");
        }

        return sudokuGrid;
    }
}
