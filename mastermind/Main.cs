using System;
using System.IO;

namespace mastermind
{
    class Mastermindbase
    {
        public int[] Settings = {0, 10, 4, 6, 2}; //inputType, maxAttempts, positions, optionAmount, Validation

        static void Main()
        {
            Mastermindbase mastermind = new Mastermindbase();
            mastermind.Start();
        }

        public void Start()
        {
             
            bool end;
            do
            {
                Sequences.Intro();
                ShowMenu();
                end = ChooseOption();
            } while (!end);
        }


        public void ShowMenu()
        {
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
                        bool result = game.Start(Settings);
                        if (result)
                            Sequences.Win();
                        else
                            Sequences.Lose();
                        return false;
                    case '2':
                        SettingsLoad();
                        return false;
                    case 'x':
                        return true;
                }
            } while (true);
        }

        public bool KeepCurrent()
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

        public void SettingsLoad()
        {
           
            do
            { 
                Console.Clear();
                Console.WriteLine("[1] New Settings");
                Console.WriteLine("[2] Load Settings");
                Console.WriteLine("[3] Save Settings");
                Console.WriteLine("[x] Return to menu");
                ConsoleKeyInfo check = Console.ReadKey();
                switch (check.KeyChar)
                {
                    case '1':
                        Settingsx();
                        break;
                    case '2':
                        SettingsLoader();
                        break;
                    case '3':
                        SettingsSaver();
                        break;
                    case 'x':
                        return;
                }
            } while (true);
        }

        public void SettingsSaver()
        {
            TextWriter saver = new StreamWriter("../../../Settings.txt");
            saver.WriteLine("{0} {1} {2} {3} {4}",Settings[0],Settings[1],Settings[2],Settings[3],Settings[4]);
            saver.Close();
            Console.WriteLine("Saved! Press any key to continue");
            Console.ReadKey();
        }

        public void SettingsLoader()
        {
            TextReader loader = new StreamReader("../../../Settings.txt");
            string[] loadData = loader.ReadLine().Split(' ');
            for (int i = 0; i < Settings.LongLength; i++)
            {
               Settings[i] = Convert.ToInt32(loadData[i]);
            }
            loader.Close();
            Console.WriteLine("Loaded! Press any key to continue");
            Console.ReadKey();
        }

        public void Settingsx()
        {
            Console.Clear();
            int[,] settingsBounds =
            {
                {0, 1}, {5, 50}, {2, 10}, {3, 9}, {0,2}
            }; //InputType(min,max), MaxAttemps(min,max), Positions(min,max), Optionamont (min,max)
            string[] settingsPrompt =
            {
                "Introduce input type(0:Letters, 1:Numbers, Current: " + Settings[0] + ")",
                "Introduce max attempts per game (Min: 5,Max: 50 and Current: " + Settings[1] + ")",
                "Introduce amount of letter/numbers to guess (Min 2:, Max: 10 and Current: " + Settings[2] + ")",
                "Introduce amount of options avaiable (EX 3 = A,B,C/1,2,3 | 6=A,B,C,D,E,F/1,2,3,4,5,6) (Min: 3, Max: 9, Current: " + Settings[3] + ")",
                "Introduce validation method:\n" +
                " 0 No validation      (accepts any character, empty will be replaced by stars)\n" +
                " 1 Errors Happen      (if characters are invalid, will prompt you to put correct values within bounds)\n" +
                " 2 Random             (if values are invalid, will replace invalid values b6y totally random ones)\n" +
                "Current: " + Settings[4]
            };
            for (int i = 0; i < 5; i++)
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
                        Settings[i] = input;
                        validInput = true;
                    }
                } while (!validInput);
            }

            Console.WriteLine("Settings Saved, press any key to continue");
            Console.ReadKey();
        }
    }
}