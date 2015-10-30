using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_Game
{
    class WordGame
    {
        public static string Reverse(string word)
        {
            char[] charArray = word.ToArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            // table characteristics
            Console.Write("Rows: ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Cols: ");
            int cols = int.Parse(Console.ReadLine());

            Console.WriteLine("Now fill out the table with letters:\n");


            // declaring and initialzing a string array; this will hold the table's content
            string[,] table = new string[rows, cols];

            // drawing the table

            string horizontalLine = new string('-', cols * 2 + 1);

            for (int i = 0; i <= rows * 2; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(horizontalLine);
                }
                else
                {
                    for (int j = 0; j <= cols; j++)
                    {
                        Console.Write("| ");
                    }
                    Console.WriteLine();
                }
            }

            /* - filling out the string array with values
               - we're also going to need two variables to use
               for cursor movement so that we can move through the table
            */
            int current_row = 5;    // this needs to change if we print to the screen more information before the drawing of the table(currently 5 is a "magic number")
            int current_col = 1;

            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.SetCursorPosition(current_col, current_row);
                    table[row, col] = Console.ReadLine();
                    if (table[row, col].Length > 1)
                    {

                        throw new ArgumentException("ERROR: Each cell can contain only one letter!");
                    }
                    else if (table[row, col] == "")
                    {
                        throw new ArgumentException("ERROR: Each cell must contain a letter!");
                    }

                    current_col += 2;
                }
                current_col = 1;
                current_row += 2;
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            // Now the user will input a word to search for
            Console.Write("Enter a word you want to search for: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string word = Console.ReadLine();
            int counter = 0;    // here we'll count the occurrences

            // now we have to check the string array for the word
            // possible directions left->right(+inverted), top->down(+inverted), top_left->down_right, aka diagonally (+inverted), down_left->up_right, aka diagonally(+inverted)
            string tempWord = "";
            string reverseWord;

            if (word.Length <= cols)    // left->right(+inverted)
            {
                for (int row = 0; row < rows; row++)
                {
                    for (int startCol = 0; startCol <= cols - word.Length; startCol++)
                    {
                        int colLimit = startCol + word.Length;
                        for (int col = startCol; col < colLimit; col++)
                        {
                            tempWord += table[row, col];
                        }

                        reverseWord = Reverse(tempWord);

                        if (tempWord == word)   // left->right
                        {
                            counter++;
                        }
                        if (reverseWord == word)    // right->left
                        {
                            counter++;
                        }

                        tempWord = "";  // clearing the string for the next word
                    }
                }
            }

            if (word.Length <= rows)    // up->down(+inverted)
            {
                for (int col = 0; col < cols; col++)
                {
                    for (int startingRow = 0; startingRow <= rows - word.Length; startingRow++)
                    {
                        int rowLimit = startingRow + word.Length;
                        for (int row = startingRow; row < rowLimit; row++)
                        {
                            tempWord += table[row, col];
                        }

                        reverseWord = Reverse(tempWord);

                        if (tempWord == word)   // up->down
                        {
                            counter++;
                        }
                        if (reverseWord == word)    // down->up
                        {
                            counter++;
                        }

                        tempWord = "";  // clearing the string for the next word
                    }
                }
            }

            if (word.Length <= cols && word.Length <= rows) // top_left->down_right(+inverted), down_left->up_right(+inverted)
            {
                // top_left->down_right(+inverted); diagonally
                for (int startingRow = 0; startingRow <= rows - word.Length; startingRow++)
                {
                    for (int startingCol = 0; startingCol <= cols - word.Length; startingCol++)
                    {
                        int limit = startingCol + word.Length;
                        for (int row = startingRow, col = startingCol; col < limit; row++, col++)
                        {
                            tempWord += table[row, col];
                        }

                        reverseWord = Reverse(tempWord);

                        if (tempWord == word)
                        {
                            counter++;
                        }
                        if (reverseWord == word)
                        {
                            counter++;
                        }

                        tempWord = "";
                    }
                }

                // down_left->up_right(+inverted); diagonally
                for (int startingRow = rows - 1; startingRow >= word.Length - 1; startingRow--)
                {
                    for (int startingCol = 0; startingCol <= cols - word.Length; startingCol++)
                    {
                        int limit = startingCol + word.Length;
                        for (int row = startingRow, col = startingCol; col < limit; row--, col++)
                        {
                            tempWord += table[row, col];
                        }

                        reverseWord = Reverse(tempWord);

                        if (tempWord == word)
                        {
                            counter++;
                        }
                        if (reverseWord == word)
                        {
                            counter++;
                        }

                        tempWord = "";
                    }
                }

            }

            Console.WriteLine();

            // result
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Result: The word ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(word);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" was seen ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (counter <= 1)
            {
                Console.Write("{0} time", counter);
            }
            else
            {
                Console.Write("{0} times", counter);
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Gray;

        }
    }
}
