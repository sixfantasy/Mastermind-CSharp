using System;

namespace mastermind
{
    public class Game
    {
        public int InputType, MaxAttempts, Positions, OptionAmount, InputValidation; //Settings

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

        public string InputConverter(string input)
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

        public bool Start(int[] settings)
        {
            Console.Clear();
            InputType = settings[0];
            MaxAttempts = settings[1];
            Positions = settings[2];
            OptionAmount = settings[3];
            InputValidation = settings[4];
            string secret = GenerateSecret();
            //Console.WriteLine(secretDisplay);
            string guess;
            for (int i = 0; i < MaxAttempts; i++)
            {
                Console.SetCursorPosition(0, 0);
                guess = DoAttempt(secret, i);
                if (CheckAttempt(secret, guess, i))
                    return true;
            }

            return false;
        }

        public string DoAttempt(string secret, int i)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Make a guess: " + (MaxAttempts - i) + " attempts left");
            Console.WriteLine("                                                     ");
            Console.SetCursorPosition(0, 1);
            if (InputType == 0)
                return InputConverter(WriteGuess(secret));
            return WriteGuess(secret);
        }

        public string WriteGuess(string secret)
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
                valid = ValidateInput(ref guess,ref valid);
            } while (!valid);
            return guess;
        }

        public bool ValidateInput(ref string guess, ref bool valid)
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
        public bool CheckAttempt(string secret, string guess, int index)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
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

        public string HintBuilder(int correct, int misplaced, string guess)
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