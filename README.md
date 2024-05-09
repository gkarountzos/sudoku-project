Sudoku Solver

This C# program offers a solution to the classic Sudoku puzzle by employing two different data structures: a stack and a queue. Sudoku is a logic-based, combinatorial number-placement puzzle, where the objective is to fill a 9x9 grid with digits so that each column, each row, and each of the nine 3x3 subgrids that compose the grid (also called "boxes", "blocks", or "regions") contain all of the digits from 1 to 9.

Usage
To use this program, follow these steps:

1. Prepare the Sudoku Puzzle: you can use the provided sample text files or create your own. To create your own use '0' to represent blank space.

2. Run the Program: Execute the program by passing the path to the text file containing the Sudoku puzzle and the desired data structure type as command-line arguments, e.g.:

        SudokuSolver.exe puzzle.txt 1  
      
      The second argument specifies the data structure type:
      
      The number '1' uses stack
      
      The number '2' uses queue

3. View the Solution: The program will output the solution to the console.

Features

1. Sudoku Class

      a. IsValidMove(int row, int col, int num): Checks if placing a number at a specific position on the board is a valid move.
      
      b. ExpendableBoard(): Creates a copy of the current board.
      
      c. IsBoardSolved(): Determines if the Sudoku puzzle is solved.
      
      d. PrintBoard(): Prints the Sudoku board to the console.

2. Solver Class
   
      a. SolveUsingStack(Sudoku initialBoard, Stack<Sudoku> stack): Solves the Sudoku puzzle using a stack-based approach.
      
      b. SolveUsingLinkedListQueue(Sudoku initialBoard, Queue<Sudoku> queue): Solves the Sudoku puzzle using a queue-based approach.

