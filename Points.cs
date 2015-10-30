using System;

namespace Points
{
    class Program
    {
        static void Main()
        {
            // starting position
            int current_x = 0;
            int current_y = 0;

            // directions
            string directions = ">>><<<~>>>~^^^";

            bool inversed = false;

            for (int i = 0; i < directions.Length; i++)
            {
                if (inversed)
                {
                    switch (directions[i])
                    {
                        case '>':
                            current_x--;
                            break;
                        case '<':
                            current_x++;
                            break;
                        case 'v':
                            current_y--;
                            break;
                        case '^':
                            current_y++;
                            break;
                        case '~':
                            inversed = !inversed;
                            break;
                    }
                }
                else
                {
                    switch (directions[i])
                    {
                        case '>':
                            current_x++;
                            break;
                        case '<':
                            current_x--;
                            break;
                        case 'v':
                            current_y++;
                            break;
                        case '^':
                            current_y--;
                            break;
                        case '~':
                            inversed = !inversed;
                            break;
                    }
                }
            }

            Console.WriteLine("Final position: ({0}, {1})", current_x, current_y);

        }
    }
}
