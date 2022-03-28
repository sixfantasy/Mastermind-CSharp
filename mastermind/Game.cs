using System;
using static mastermind.Settings;

namespace mastermind
{/// <summary>
 /// Class for the mastermind game itself
 /// </summary>
    public class Game
    {
        private int InputType, MaxAttempts, Positions, OptionAmount, InputValidation; //Settings
/// <summary>
/// Generates a new secret randomly
/// </summary>
/// <returns>SecretGenerated</returns>
        public string GenerateSecret()
        {
            Random rng = new Random();
            string secret = "";
            char letterGen;
            for (int i = 0; i < Positions; i++)
                switch (InputType)
                {
                    case 0:
                        letterGen = Convert.ToChar(rng.Next(0, OptionAmount) + 'A');
                        secret += letterGen;
                        break;
                    case 1:
                        letterGen = Convert.ToChar(rng.Next(1, OptionAmount + 1) + '0');
                        secret += letterGen;
                        break;
                }

            return secret;
        }
/// <summary>
/// Converts user input to uppercase letters)
/// </summary>
/// <param name="input">User input</param>
/// <returns></returns>
        private string InputConverter(string input)
        {
            char[] holder = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= 'a' && input[i] < 'a' + OptionAmount)
                    holder[i] += (char) (input[i] - 'a' + 'A');
                else
                    holder[i] = input[i];

            }

            string final = new string(holder);
            return final;
        }
/// <summary>
/// Starts a mastremind game
/// </summary>
/// <param name="i">Attempts done at the current time before finish</param>
/// <returns>True for wins, false for loses</returns>
        public bool Start(out int i)
        {
            Console.Clear();
            InputType = CurrentSettings[0];
            MaxAttempts = CurrentSettings[1];
            Positions = CurrentSettings[2];
            OptionAmount = CurrentSettings[3];
            InputValidation = CurrentSettings[4];
            string secret = GenerateSecret();
            //Console.WriteLine(secretDisplay);
            return Play(secret, out i);
        }
/// <summary>
/// Plays the game
/// </summary>
/// <param name="secret">Secret code generated randomly</param>
/// <param name="i">Attempts done at the current time before finish</param>
/// <returns>True for wins, false for loses</returns>
        private bool Play(string secret, out int i){
            string guess;
            for (i = 0; i < MaxAttempts; i++)
            {
                Console.SetCursorPosition(0, 0);
                guess = DoAttempt(secret, i);
                if (CheckAttempt(secret, guess, i))
                    return true;
            }
            Console.WriteLine(secret);
            return false;
        }
/// <summary>
/// Player does one attempt to guess the code
/// </summary>
/// <param name="secret">Secret code generated randomly</param>
/// <param name="i">Attempts done at the current time before finish</param>
/// <returns>Guess code after proper validation</returns>
        private string DoAttempt(string secret, int i)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Make a guess: " + (MaxAttempts - i) + " attempts left");
            Console.WriteLine("                                                     ");
            Console.SetCursorPosition(0, 1);
            if (InputType == 0)
                return InputConverter(WriteGuess(secret));
            return WriteGuess(secret);
        }
/// <summary>
/// Takes the player input for the guess
/// </summary>
/// <param name="secret">Secret code generated randomly</param>
/// <returns>Guess code after validation</returns>
        private string WriteGuess(string secret)
        {
            bool valid = false;
            string guess;
            do
            {
                guess = ("" + Console.ReadLine());
                while (guess.Length < secret.Length)
                    guess += " ";
                while (guess.Length > secret.Length)
                    guess = guess.Remove(guess.Length - 1);
                if (InputType == 0) 
                    guess = InputConverter(guess);
                valid = ValidateInput(ref guess);
            } while (!valid);
            return guess;
        }
/// <summary>
/// Validates the input given by the player
/// </summary>
/// <param name="guess">Guess code to validate</param>
/// <returns>Binary result that indicates if the input is valid or no</returns>
        private bool ValidateInput(ref string guess)
        {
            char basex = ' ';
            switch (InputType)
            {
                case 0:
                    basex = 'A';
                    break;
                case 1:
                    basex = '0';
                    break;
            }
            {
                char[] randomized = new char[guess.Length];
                for (int i = 0; i < guess.Length; i++)
                {
                    randomized[i] = guess[i];
                    switch (InputValidation)
                    {
                        case 0:

                            return true;
                        case 1:
                            if (randomized[i] < basex || randomized[i] > basex + OptionAmount)
                            {
                                Console.WriteLine("Invalid character(s), try again");
                                return false;
                            }
                            break;
                        case 2:
                            Random rng = new Random();
                            if (randomized[i] < basex || randomized[i] > basex + OptionAmount)
                                randomized[i] = Convert.ToChar(rng.Next(0, OptionAmount) + basex);
                            break;
                    }
                }
                guess = new string(randomized);
            }
            return true;
        }
/// <summary>
/// Checks an attempt done by the player
/// </summary>
/// <param name="secret">Secret code</param>
/// <param name="guess">Guess code sent by the player</param>
/// <param name="index">Value to indicate where to write next hintr</param>
/// <returns>Binary result, true if secret matches guess (Game is won)</returns>
        private bool CheckAttempt(string secret, string guess, int index)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            char[] secretcheck = new char[secret.Length];
            char[] guesscheck = new char[guess.Length];
            for (int i = 0; i < secretcheck.Length; i++)
            {
                secretcheck[i] = secret[i];
                guesscheck[i] = guess[i];
            }

            int correct = CheckCorrect(secretcheck, guesscheck);
            int misplaced = CheckMisplaced(secretcheck, guesscheck);
            Console.SetCursorPosition(0, 4 + index);
            Console.WriteLine(HintBuilder(correct, misplaced, guess));
            if (correct == secret.Length)
                return true;
            return false;
        }
/// <summary>
/// Check coorrect letters in right position
/// </summary>
/// <param name="secretcheck">Temporary secret code used exclusively for checking purposes</param>
/// <param name="guesscheck">Temporary guess code used exclusively for checking purposes</param>
/// <returns>Amount of correct positions</returns>
        private int CheckCorrect(char[] secretcheck, char[] guesscheck)
        {
            int result = 0;
            for (int i = 0; i < secretcheck.Length; i++)
                if (secretcheck[i] == guesscheck[i])
                {
                    secretcheck[i] = ' ';
                    guesscheck[i] = ' ';
                    result++;
                }

            return result;
        }
/// <summary>
/// Checks letters that are correct but in a wrong position
/// </summary>
/// <param name="secretcheck">Temporary secret code used exclusively for checking purposes</param>
/// <param name="guesscheck">Temporary guess code used exclusively for checking purposes</param>
/// <returns>Amount of misplaced letters</returns>
        private int CheckMisplaced(char[] secretcheck, char[] guesscheck)
        {
            int result = 0;
            for (int i = 0; i < secretcheck.Length; i++)
            {
                for (int j = 0; j < secretcheck.Length; j++)
                    if (secretcheck[i] == guesscheck[j] && secretcheck[i] != ' ' && guesscheck[j] != ' ')
                    {
                        secretcheck[i] = ' ';
                        guesscheck[j] = ' ';
                        result++;
                    }
            }

            return result;
        }
/// <summary>
/// Builds a hint for the user based on the guess
/// </summary>
/// <param name="correct">Amount of correct letters in right position</param>
/// <param name="misplaced">Amount of correct letters in wrong position</param>
/// <param name="guess">Guess code sent by the player</param>
/// <returns>Hint ready to be written</returns>
        private string HintBuilder(int correct, int misplaced, string guess)
        {
            string hint = "";
            for (int j = 0; j < Positions; j++)
            {
                if (correct > 0)
                {
                    hint += "#";
                    correct--;
                }
                else if (misplaced > 0)
                {
                    hint += "*";
                    misplaced--;
                }
                else
                {
                    hint += "·";
                }
            }

            return guess + " " + hint;
        }
    }
}