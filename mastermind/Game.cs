using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Threading;

namespace mastermind
{
    public class Game
    {
        public int inputType, maxAttempts, positions, optionAmount; //Settings
        
        public string GenerateSecret()
        {
            Random rng = new Random();
            string secret = "";
            char letterGen;
            for (int i = 0; i < positions; i++)
                switch (inputType)
            { 
                case 0:
                    letterGen = Convert.ToChar(rng.Next(1, optionAmount + 1) + 'A');
                    secret += letterGen;
                    break;
                case 1:
                    letterGen = Convert.ToChar(rng.Next(1, optionAmount + 1) + '0');
                    secret += letterGen;
                    break;
            }
            return secret;
        }
        public string InputConverter(string input)
        {
            string final = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] > 'a' || input[i] < 'a'+ optionAmount)
                    final += ((char) (input[i] - 'a' + 'A'));
                else
                    final += input[i];
            }
            return (string)final;
        }

        public bool Start(int[] settings)
        {
            Console.Clear();
            Random rng = new Random();
            inputType = settings[0];
            maxAttempts = settings[1];
            positions = settings[2];
            optionAmount = settings[3];
            string secret = GenerateSecret();
            //Console.WriteLine(secretDisplay);
            String[] guesshistory = new string[maxAttempts];
            string guess;
            for (int i = 0; i < maxAttempts; i++)
            {
                guess = DoAttempt(secret, i);
                if (CheckAttempt(secret, guess, guesshistory, i));
                    return true;
                
            }
            return false;
        }
        public string DoAttempt(string secret, int i)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Make a guess: " + (maxAttempts - i) + " attempts left");
            if (inputType == 0) 
                return InputConverter(WriteGuess(secret));
            return WriteGuess(secret);
        }
        public string WriteGuess(string secret)
        {
            string guess = ("" + Console.ReadLine());
            while (guess.Length < secret.Length)
                guess += " ";
            while (guess.Length > secret.Length)
                guess = guess.Remove(guess.Length - 1);
            //ValidateInput()
            return guess;
        }

        public bool CheckAttempt(string secret, string guess, string[]guesshistory, int index)
        {
            char[] secretcheck = new char[secret.Length];
            char[] guesscheck = new char[guess.Length];
            for (int i = 0; i < secretcheck.Length; i++)
            {
                secretcheck[i] = secret[i];
                guesscheck[i] = guess[i];
            }

            int correct = CheckCorrect(secretcheck, guesscheck);
            int misplaced = CheckMisplaced(secretcheck, guesscheck);
            guesshistory[index] = HintBuilder(correct, misplaced, guess);
            if (correct == secret.Length)
                return true;
            return false;
        }

        public int CheckCorrect(char[] secretcheck, char[] guesscheck)
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
        public int CheckMisplaced(char[] secretcheck, char[] guesscheck)
        {
            int result = 0;
            for (int i = 0; i < secretcheck.Length; i++) 
            for (int j = 0; i < secretcheck.Length; i++)
                if (secretcheck[i] == guesscheck[j] && secretcheck[i]!=' '&& guesscheck[j]!=' ')
                {
                    secretcheck[i] = ' ';
                    guesscheck[i] = ' ';
                    result++;
                }
            return result;
        }

        public string HintBuilder(int correct, int misplaced, string guess)
        {
            string hint = "";
            for (int j = 0; j < positions; j++)
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

