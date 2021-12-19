using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            char menuChoice;

            PrintWelcomeMessage();

            while(running)
            {
                PrintMenu();
                Char.TryParse(Console.ReadLine(), out menuChoice);

                switch(menuChoice)
                {
                    case 'q':
                        Console.WriteLine("\nYou chose to exit the game. Bye!");
                        running = false;
                        break;

                    case 'p':
                        Console.WriteLine("\nLet's play a game of hangman!");
                        HangmanGame game = new HangmanGame();
                        game.PlayGame();
                        break;

                    default:
                        Console.WriteLine("\ninvalid menu choice!");
                        break;
                }
            }
        }

        private static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the hangman game!");
        }

        private static void PrintMenu()
        {
            Console.WriteLine("p: Play game");
            Console.WriteLine("q: Quit");
        }
    }
}
