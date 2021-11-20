using System;
using System.Threading;
namespace mastermind
{
    class mastermindbase
    {
        static int[] settings = { 0, 10, 4, 6 };//inputType, maxAttempts, positions, optionAmount
        static void Main()
        {
            Console.Clear();    
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Welcome to....");
            Console.SetCursorPosition(0, 4);
            Thread.Sleep(250);
            Console.WriteLine("                  ███╗░░░███╗░█████╗░░██████╗████████╗███████╗██████╗░███╗░░░███╗██╗███╗░░██╗██████╗░");
            Thread.Sleep(250);
            Console.WriteLine("                  ████╗░████║██╔══██╗██╔════╝╚══██╔══╝██╔════╝██╔══██╗████╗░████║██║████╗░██║██╔══██╗");
            Thread.Sleep(250);
            Console.WriteLine("                  ██╔████╔██║███████║╚█████╗░░░░██║░░░█████╗░░██████╔╝██╔████╔██║██║██╔██╗██║██║░░██║");
            Thread.Sleep(250);
            Console.WriteLine("                  ██║╚██╔╝██║██╔══██║░╚═══██╗░░░██║░░░██╔══╝░░██╔══██╗██║╚██╔╝██║██║██║╚████║██║░░██║");
            Thread.Sleep(250);
            Console.WriteLine("                  ██║░╚═╝░██║██║░░██║██████╔╝░░░██║░░░███████╗██║░░██║██║░╚═╝░██║██║██║░╚███║██████╔╝");
            Thread.Sleep(250);
            Console.WriteLine("                  ╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═════╝░░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═╝░░░░░╚═╝╚═╝╚═╝░░╚══╝╚═════╝░");
            Thread.Sleep(250);
            Console.SetCursorPosition(0, 12);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Instructions");
            Console.WriteLine(" You have received an encrypted message, but it's encrypted... So, in order to decipher it yo need to crack a 4 letter code before anything");
            Console.WriteLine(" Be careful though, as you have a limited number of attempts before the message gets destroyed");
            Console.WriteLine("");
            Console.WriteLine(" How it works");
            Console.WriteLine(" Introduce 4 letters from A to F");
            Console.WriteLine(" If you introduce the right code, congrats! You did it");
            Console.WriteLine(" If not, no worries you will get a hint to help you, which will indicate");
            Console.WriteLine(" #: Correct letter(s), in the right position");
            Console.WriteLine(" *: Correct letter(s), but not where it should be");
            Console.WriteLine(" ·: No luck, try with different letter(s)");
            Console.WriteLine(" That's All, Have Fun!!!");
            Console.SetCursorPosition(0, Console.CursorTop + 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[1]:Start Game");
            Console.WriteLine("[2]:Settings");
            Console.WriteLine("[3]:Close Game");

            do
            {
                var input = Console.ReadKey();
                switch (input.KeyChar)
                {
                    case '1':
                        Game();
                        break;
                    case '2':
                        Settings();
                        break;
                    case '3':
                        Environment.Exit(0);
                        break;
                }
            } while (true);
        }
        static bool keepcurrent()
        {
            Console.WriteLine("Invalid input, keep current? (Y/n)");

            do
            {
                var check = Console.ReadKey();
                switch (check.Key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                }
            } while (true);

        }
        /*static void SettingsLoad()
        {
            Console.Clear();
            Console.WriteLine("[1] New Settings");
            Console.WriteLine("[2] Load Settings");
            do
            {
                var check = Console.ReadKey();
                switch (check.KeyChar)
                {
                    case '1':
                        Settings();
                        break;
                    case '2':
                        break;

                }
            } while (true);
        }*/
        //Unimplemented code due issues to saving/loading settings file
        static void Settings()
        { 
            Console.Clear();
            int[,] settingsBounds = { { 0, 1 }, { 5, 50 }, { 2, 10 }, { 3, 9 } }; //InputType(min,max), MaxAttemps(min,max), Positions(min,max), Optionamont (min,max)
            string[] settingsPrompt = 
            {
                "Introduce input type(0:Letters, 1:Numbers, Current: " + settings[0] + ")",
                "Introduce max attempts per game (Min: 5,Max: 50 and Current: " + settings[1] + ")",
                "Introduce amount of letter/numbers to guess (Min 2:, Max: 10 and Current: " + settings[2] + ")",
                "Introduce amount of options avaiable (EX 3 = A,B,C/1,2,3 | 6=A,B,C,D,E,F/1,2,3,4,5,6) (Min: 3, Max: 9, Current: " + settings[3] + ")"
            };
            for (int i = 0; i < 4; i++) 
            {
            bool validInput = false;
                do
                {
                    Console.WriteLine(settingsPrompt[i]);
                    bool validNumber = Int32.TryParse(Console.ReadLine(), out int input);
                    if (!validNumber || input < settingsBounds[i,0] || input > settingsBounds[i,1])
                    {
                        bool keep = keepcurrent();
                        if (keep)
                            validInput = true;
                    }
                    else
                    {
                        settings[i] = input;
                        validInput = true;
                    }
                }
                while (!validInput);
                validInput = false;
            }
            Console.WriteLine("Settings Saved, Returning to main menu...");
            Main();
        }
        static void Game()
        {
            Console.Clear();
            Random rng = new Random();
            int inputType = settings[0];
            int maxAttempts = settings[1], positions = settings[2], optionAmount = settings[3];
            string secret = "";
            string secretDisplay = "";
            for (int i = 0; i < positions; i++)
            {
                char letterGen = Convert.ToChar(rng.Next(1,optionAmount+1)+64);
                secret += letterGen;
                switch (inputType)
                {
                    case 1:
                        secretDisplay += (char)(letterGen - 16);
                        break;
                    default:
                        secretDisplay += letterGen;
                        break;
                }
                
            }
            Console.WriteLine(secretDisplay);
            int misplaced, correctposition;
            String[] guesshistory = new string[maxAttempts];
            for (int i = 0; i < maxAttempts; i++)
            {
                correctposition = 0;
                misplaced = 0;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Make a guess: " +(maxAttempts - i)+ " attempts left");
                char[] secretcheck = new char[positions],
                      guesscheck = new char[positions];
                string guess = (""+Console.ReadLine());
                while (guess.Length < positions)
                    guess += " ";
                while (guess.Length > positions)
                    guess = guess.Remove(guess.Length-1);
                for (int j = 0; j < positions; j++)
                {
                    char letter = guess[j];
                    for (int k = 0; k < optionAmount; k++) 
                    {
                        
                        switch (inputType)
                        {
                            case 0:
                                if (letter == (char)('a' + k))
                                    letter -= ' ';
                                break;
                            case 1:
                                if (letter == (char)('1' + k))
                                letter += (char)16;
                                break;
                        }
                        if (letter == (char) ('A' + k))
                        {
                            guesscheck[j] = letter;
                            if (guesscheck[j] == secret[j])
                            {
                                correctposition++;
                                secretcheck[j] =(char) 0;
                                guesscheck[j] =(char) 0;
                            }
                            else
                                secretcheck[j] = secret[j];
                        }
                    }
                }
                for (int j = 0; j < positions; j++)
                {
                    if (secretcheck[j] != 0)
                        for (int k = 0; k < positions; k++)
                            if (secretcheck[j] == guesscheck[k] && guesscheck[k] !=0)
                            {
                                misplaced++;
                                secretcheck[j] =(char)0;
                                guesscheck[k] =(char)0;
                            }
                }
                Console.Clear();
                Console.SetCursorPosition(0, 3);
                guesshistory[i]=guess;
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
                if (correctposition==positions)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write("                                ");
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(guess +" ");
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
                    Main();
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int w = 0; w < 4; w++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine("No more attempts");
            Thread.Sleep(1000);
            for (int w = 0; w < 4; w++) 
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
            Main();
        }
    }
}