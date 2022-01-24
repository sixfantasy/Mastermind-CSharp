using System;
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

        public void Start(int[] settings)
        {
            Console.Clear();
            Random rng = new Random();
            inputType = settings[0];
            maxAttempts = settings[1];
            positions = settings[2];
            optionAmount = settings[3];
            string secret = GenerateSecret();
            //Console.WriteLine(secretDisplay);
            int misplaced = 0;
            int correctposition = 0;
            String[] guesshistory = new string[maxAttempts];
            string guess;
            for (int i = 0; i < maxAttempts; i++)
            {
                guess = DoAttempt(secret, i);
                CheckAttempt(secret, guess, ref correctposition, ref misplaced);
            }
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

        public void CheckAttempt(string secret, string guess,ref int checkCorrect,ref int checkMisplaced)
        {
            char[] secretcheck = new char[secret.Length];
            char[] guesscheck = new char[guess.Length];
            for (int i = 0; i < secretcheck.Length; i++)
            {
                secretcheck[i] = secret[i];
                guesscheck[i] = guess[i];
            }
            checkCorrect = CheckCorrect(secretcheck, guesscheck);
            checkMisplaced = CheckMisplaced(secretcheck, guesscheck);
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
    }
    
}

  /*      Console.Clear();
            Console.SetCursorPosition(0, 3);
            guesshistory[i] = guess;
            string hint = "";
            int cx = correctposition;
            int mx = misplaced;
            for (int j = 0; j < positions; j++)
            {
                if (cx > 0)
                {
                    hint += "#";
                    cx--;
                }
                else if (mx > 0)
                {
                    hint += "*";
                    mx--;
                }
                else
                {
                    hint += "·";
                }
            }

            guesshistory[i] += " " + hint;
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (int j = 0; j <= i; j++)
                Console.WriteLine(guesshistory[j]);
            if (correctposition == positions)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write("                                ");
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(guess + " ");
                for (int w = 0; w < positions; w++)
                {
                    Console.Write("#");
                    Thread.Sleep(1000);
                }

                for (int w = 0; w < 50; w++)
                {
                    Console.Write("#");
                    Thread.Sleep(50);
                }

                Console.WriteLine();
                for (int w = 0; w < 25; w++)
                {
                    Console.WriteLine("               ");
                    Thread.Sleep(100);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine("YOU WON!!!!!!!!");
                    Thread.Sleep(100);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                }

                Console.WriteLine();
                for (int w = 0; w < 55 + positions; w++)
                {
                    Console.Write("#");
                    Thread.Sleep(50);
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        for (int w = 0; w< 4; w++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }

        Console.WriteLine("No more attempts");
        Thread.Sleep(1000);
        for (int w = 0; w< 4; w++)
        {
            Console.WriteLine("GAME OVER");
            Thread.Sleep(250);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("         ");
            Thread.Sleep(250);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        Console.WriteLine("The answer was: " + secretDisplay);
        Console.WriteLine("Press any key to finish");
        Console.ReadKey();
    }
}

}*/