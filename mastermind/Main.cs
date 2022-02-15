using System;
using System.Threading;

namespace mastermind
{
    class Mastermindbase
    {
        public int[] settings = {0, 10, 4, 6}; //inputType, maxAttempts, positions, optionAmount

        static void Main()
        {
            Console.ReadKey();
            Mastermindbase mastermind = new Mastermindbase();
            mastermind.Start();
        }

        public void Start()
        {
            bool end;
            do
            {
                ShowMenu();
                end = ChooseOption();
            } while (!end);
        }


        public void ShowMenu()
        {
            Console.Clear();
            Sequences.Intro();
            Console.SetCursorPosition(0, 12);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Instructions");
            Console.WriteLine(
                " You have received an encrypted message, but it's encrypted... So, in order to decipher it yo need to crack a 4 letter code before anything\n" +
                " Be careful though, as you have a limited number of attempts before the message gets destroyed\n\n" +

                " How it works\n" +
                " Introduce 4 letters from A to F\n" +
                " If you introduce the right code, congrats! You did it\n" +
                " If not, no worries you will get a hint to help you, which will indicate\n" +
                " #: Correct letter(s), in the right position\n" +
                " *: Correct letter(s), but not where it should be\n" +
                " ·: No luck, try with different letter(s)\n" +
                " That's All, Have Fun!!!");
            Console.SetCursorPosition(0, Console.CursorTop + 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[1]:Start Game");
            Console.WriteLine("[2]:Settings");
            Console.WriteLine("[x]:Close Game");
        }

        public bool ChooseOption()
        {
            do
            {
                var input = Console.ReadKey();
                switch (input.KeyChar)
                {
                    case '1':
                        Game game = new Game();
                        bool result = game.Start(settings);
                        if (result)
                            Sequences.Win();
                        else
                            Sequences.Lose();
                        return false;
                    case '2':
                        Settings();
                        return false;
                    case 'x':
                        return true;
                }
            } while (true);
        }

        static bool KeepCurrent()
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
        public void Settings()
        {
            Console.Clear();
            int[,] settingsBounds =
            {
                {0, 1}, {5, 50}, {2, 10}, {3, 9}
            }; //InputType(min,max), MaxAttemps(min,max), Positions(min,max), Optionamont (min,max)
            string[] settingsPrompt =
            {
                "Introduce input type(0:Letters, 1:Numbers, Current: " + settings[0] + ")",
                "Introduce max attempts per game (Min: 5,Max: 50 and Current: " + settings[1] + ")",
                "Introduce amount of letter/numbers to guess (Min 2:, Max: 10 and Current: " + settings[2] + ")",
                "Introduce amount of options avaiable (EX 3 = A,B,C/1,2,3 | 6=A,B,C,D,E,F/1,2,3,4,5,6) (Min: 3, Max: 9, Current: " +
                settings[3] + ")"
            };
            for (int i = 0; i < 4; i++)
            {
                bool validInput = false;
                do
                {
                    Console.WriteLine(settingsPrompt[i]);
                    bool validNumber = Int32.TryParse(Console.ReadLine(), out int input);
                    if (!validNumber || input < settingsBounds[i, 0] || input > settingsBounds[i, 1])
                    {
                        bool keep = KeepCurrent();
                        if (keep)
                            validInput = true;
                    }
                    else
                    {
                        settings[i] = input;
                        validInput = true;
                    }
                } while (!validInput);
            }

            Console.WriteLine("Settings Saved");
            Thread.Sleep(500);
        }
    }
}