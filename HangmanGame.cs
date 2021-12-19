using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{
    public class HangmanGame
    {
        private Random mRand;
        private bool mGameIsRunning;
        private int mRemainingGuesses;
        private char[] mAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        private string[] mWords = new string[] { "unicycle", "motorcycle", "barspoon", "reindeer", "castle", "house", "submarine" };
        private string mSecrectWord;
        private char[] mRevealedGuesses;
        private StringBuilder mStrbuilderIncorrectGuesses;

        public HangmanGame ()
        {
            mRand = new Random();
            mStrbuilderIncorrectGuesses = new StringBuilder();
        }

        public void PlayGame()
        {
            mRemainingGuesses = 10;

            //Get a random word from array
            mSecrectWord = mWords[mRand.Next(7)];

            //Set all letters to unrevealed '_'
            mRevealedGuesses = Enumerable.Repeat('_', mSecrectWord.Length).ToArray();

            mGameIsRunning = true;

            while (mGameIsRunning)
            {
                if (mRemainingGuesses > 0)
                {
                    PrintGameState();
                    PrintGameInstructions();

                    char menuChoice;

                    Char.TryParse(Console.ReadLine(), out menuChoice);

                    switch (menuChoice)
                    {
                        case 'g':
                            GuessLetter();
                            break;

                        case 's':
                            SolveWord();
                            break;

                        //quit
                        case 'q':
                            mGameIsRunning = false;
                            break;

                        default:
                            Console.WriteLine("Incorrect choice!");
                            break;
                    }
                }
                else
                {
                    //0 guesses left -> game over
                    mGameIsRunning = false;
                    Console.WriteLine("Game over! You ran out of guesses. The secret word was: " + mSecrectWord.ToUpper());
                    Console.WriteLine("Type any key to continue: ");
                    Console.ReadKey();
                }
            }
        }

        private void GuessLetter()
        {
            bool legalGuess = false;
            char guess;

            while(!legalGuess)
            {
                Console.WriteLine("What letter do you want to guess? (A-Z)");
                Char.TryParse(Console.ReadLine(), out guess);
                
                //Check first if guess is A-Z
                if (CheckIfAlphabeticGuess(guess))
                {
                    legalGuess = true;
                    if (CheckIfAlreadyGuessed(guess))
                    {
                        Console.WriteLine("You have already guessed that letter! Pick another one!");
                        break;
                    }
                    else
                    {
                        mRemainingGuesses--;

                        //check if correct guess
                        bool correctGuess = false;
                        for(int j = 0; j < mSecrectWord.Length; j++)
                        {
                            if(mSecrectWord[j].ToString().ToUpper() == guess.ToString().ToUpper())
                            {
                                mRevealedGuesses[j] = char.ToUpper(mSecrectWord[j]);
                                Console.WriteLine("Correct guess!");
                                correctGuess = true;
                            }
                        }

                        if (!correctGuess)
                        {
                            mStrbuilderIncorrectGuesses.Append(guess.ToString().ToUpper());
                        }
                        break;
                    }
                }
            }
        }

        private void SolveWord()
        {
            bool legalGuess = false;
            string guess;

            while (!legalGuess)
            {
                Console.WriteLine("What do you think the secret word is?");
                guess = Console.ReadLine();

                if(guess != string.Empty)   //Maybe add same word length check?
                {
                    legalGuess = true;
                    if (guess.ToUpper() == mSecrectWord.ToUpper())
                    {
                        Console.WriteLine("You guessed the right word! " + guess.ToUpper() + " = " + mSecrectWord.ToUpper());
                        Console.WriteLine("You had " + mRemainingGuesses + " Remaining guesses");
                        mGameIsRunning = false;
                        
                    }
                    else
                    {
                        mRemainingGuesses--;
                        Console.WriteLine("Your word is not the secret word!");
                    }
                }
            }
        }

        /// <summary>
        /// Method for checking if the guessed char is in the allowed alphabeth (A-Z)
        /// </summary>
        /// <param name="guess"></param>
        /// <returns>True if A-Z false otherwise</returns>
        private bool CheckIfAlphabeticGuess(char guess)
        {
            bool isAlphabeticGuess = false;

            for (int i = 0; i < mAlphabet.Length; i++)
            {
                if (guess.ToString().ToUpper() == mAlphabet[i].ToString())
                {
                    isAlphabeticGuess = true;   
                }
            }
            return isAlphabeticGuess;
        }

        /// <summary>
        /// Method for checking if the guessed char has already been guessed before
        /// </summary>
        /// <param name="guess"></param>
        /// <returns>true if aready guessed, otherwise false</returns>
        private bool CheckIfAlreadyGuessed(char guess)
        {
            bool alreadyGuessed = false;

            //check incorrect guesses
            for(int i = 0; i < mStrbuilderIncorrectGuesses.Length; i++)
            {
                if(mStrbuilderIncorrectGuesses[i].ToString().ToUpper() == guess.ToString().ToUpper())
                {
                    alreadyGuessed = true;
                }
            }

            //check revealed guesses
            for(int i = 0; i < mRevealedGuesses.Length; i++)
            {
                if(mRevealedGuesses[i].ToString().ToUpper() == guess.ToString().ToUpper())
                {
                    alreadyGuessed = true;
                    break;
                }
            }

            return alreadyGuessed;
        }

        private static void PrintGameInstructions()
        {
            Console.WriteLine("g: guess a specific letter");
            Console.WriteLine("s: try to solve the word");
            Console.WriteLine("q: quit game");
        }

        private void PrintGameState()
        {
            Console.WriteLine("\nRemaining guesses: " + mRemainingGuesses);
            Console.WriteLine("Incorrect guesses: " + mStrbuilderIncorrectGuesses);
            Console.WriteLine("Current secret word: " + string.Join(" ", mRevealedGuesses));
        }
    }
}
