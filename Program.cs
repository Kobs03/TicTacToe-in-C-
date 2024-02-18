using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class Program
    {
        static void Main()
        {

            string[,] board = new string[3, 3]; // 2D array representing the board
            List<int> occupiedSlot = new List<int>();
            bool turnOrder = true;
            bool gameOver = false;
            string player;
            int numOfTurns = 1;
            int position;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Do You Want to Play TicTacToe? (y/n) ");
            Console.ResetColor();

            char input = Console.ReadLine()[0];

            if (input == 'n')
            {
                Console.WriteLine("Exiting Game");
            }
            else if (input == 'y')
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome to the Game!");
                Console.ResetColor();

                // Initialize Fresh Board
                InitializeBoard(board);
                PrintBoard(board);

                // Main Game Loop
                do
                {
                    player = turnOrder ? "x" : "o";
                    Console.WriteLine($"Player {player} Turn");

                    Console.WriteLine("Enter the positon 1 - 9:");

                    // Check if the input is valid integer or within range
                    while (true)
                    {
                        string gameInput = Console.ReadLine(); // Use the existing 'input' variable declared outside the loop

                        if (gameInput == "q")
                        {
                            Console.WriteLine("Exiting Game...");
                            return; // Exit the Main method and the program
                        }

                        if (!int.TryParse(gameInput, out position) || position < 1 || position > 9)
                        {
                            Console.WriteLine("Invalid Input! Please input a valid integer between 1 and 9");
                        }
                        else
                        {
                            break; // Exit the loop if input is valid
                        }
                    }

                    if (!occupiedSlot.Contains(position))
                    {
                        occupiedSlot.Add(position);

                        position--;

                        int row = position / 3;
                        int column = position % 3;

                        board[row, column] = turnOrder ? "X" : "O";
                        board[row, column] = board[row, column];
                        turnOrder = !turnOrder;

                        Console.WriteLine($"Turn no. {numOfTurns}");

                        numOfTurns++;

                        // Display the initial board
                        PrintBoard(board);
                    }
                    else
                    {
                        Console.WriteLine("Position taken, Please enter another.");

                        Console.WriteLine("List of Unavailable slots: ");

                        // lambda expression of foreach
                        occupiedSlot.ForEach(slot =>
                        {
                            Console.Write($"{slot},");
                        });
                        Console.WriteLine();
                    }

                    // Function to inspect the board who is the winner

                    if (CheckWinner(board, player.ToUpper()))
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Player '{player.ToUpper()}' Wins!");
                        Console.ResetColor();

                        gameOver = !gameOver;
                    }

                    if (numOfTurns >= 10)
                    {
                        gameOver = !gameOver;
                        Console.WriteLine("Match Draw!");
                    }

                } while (!gameOver);
                ;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Input! Please enter (y/n) only");
            }
            // ----------
            // | Methods|
            // ----------

            // Initialize Board without Inputs
            static void InitializeBoard(string[,] board)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        board[i, j] = "-";
                    }

                }
            }

            // Display Updated Board with player inputs
            static void PrintBoard(string[,] board)
            {
                Console.WriteLine("Updated Game Board");
                Console.WriteLine("----------");
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write("| " + board[i, j] + " "); // output : | - | - | - 
                    }
                    Console.WriteLine("|");
                }
                Console.WriteLine("----------");
            }

            // Check the winner function
            static bool CheckWinner(string[,] board, string playerSymbol)
            {
                // Check row
                for (int i = 0; i < 3; i++)
                {
                    if (board[i, 0] == playerSymbol && board[i, 1] == playerSymbol && board[i, 2] == playerSymbol)
                    {
                        return true;
                    }
                }

                // Check col
                for (int j = 0; j < 3; j++)
                {
                    if (board[0, j] == playerSymbol && board[1, j] == playerSymbol && board[2, j] == playerSymbol)
                    {
                        return true;
                    }
                }

                // Check diagonals
                if (board[0, 0] == playerSymbol && board[1, 1] == playerSymbol && board[2, 2] == playerSymbol ||
                board[0, 2] == playerSymbol && board[1, 1] == playerSymbol && board[2, 0] == playerSymbol)
                {
                    return true;
                }

                return false;
            }

        }
    }
}