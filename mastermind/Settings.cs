using System;
using System.IO;

namespace mastermind
{
    /// <summary>
    /// Class to manage setttings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Stores the current settings to use when starting the game
        /// </summary>
        ///  /// <remarks>{inputType, maxAttempts, positions, optionAmount, Validation}</remarks>
        public static int[] CurrentSettings = {0, 10, 4, 6, 2, 0}; 
        /// <summary>
        /// If the input is invalid, choose if want keep the current values
        /// </summary>
        /// <returns>True: keep, False: rewrite setting</returns>
        public static bool KeepCurrent()
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
/// <summary>
/// Menu to manage settings
/// </summary>
        public static void SettingsLoad()
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
/// <summary>
/// Stores settings in a text file
/// </summary>
        public static void SettingsSaver()
        {
            TextWriter saver = new StreamWriter("../../../Settings.txt");
            saver.WriteLine("{0} {1} {2} {3} {4}", CurrentSettings[0], CurrentSettings[1], CurrentSettings[2], CurrentSettings[3], CurrentSettings[4]);
            saver.Close();
            Console.WriteLine("Saved! Press any key to continue");
            Console.ReadKey();
        }
/// <summary>
/// Loads settings from a text file
/// </summary>
        public static void SettingsLoader()
        {
            TextReader loader = new StreamReader("../../../Settings.txt");
            string[] loadData = loader.ReadLine().Split(' ');
            for (int i = 0; i < CurrentSettings.Length; i++)
            {
                CurrentSettings[i] = Convert.ToInt32(loadData[i]);
            }

            loader.Close();
            Console.WriteLine("Loaded! Press any key to continue");
            Console.ReadKey();
        }
/// <summary>
/// Configure new settings
/// </summary>
        public static void Settingsx()
        {
            Console.Clear();
            int[,] settingsBounds =
            {
                {0, 1}, {5, 50}, {2, 10}, {3, 9}, {0, 2}
            }; //InputType(min,max), MaxAttemps(min,max), Positions(min,max), Optionamont (min,max)
            string[] settingsPrompt =
            {
                "Introduce input type(0:Letters, 1:Numbers, Current: " + CurrentSettings[0] + ")",
                "Introduce max attempts per game (Min: 5,Max: 50 and Current: " + CurrentSettings[1] + ")",
                "Introduce amount of letter/numbers to guess (Min 2:, Max: 10 and Current: " + CurrentSettings[2] + ")",
                "Introduce amount of options avaiable (EX 3 = A,B,C/1,2,3 | 6=A,B,C,D,E,F/1,2,3,4,5,6) (Min: 3, Max: 9, Current: " +
                CurrentSettings[3] + ")",
                "Introduce validation method:\n" +
                " 0 No validation      (accepts any character, empty will be replaced by stars)\n" +
                " 1 Errors Happen      (if characters are invalid, will prompt you to put correct values within bounds)\n" +
                " 2 Random             (if values are invalid, will replace invalid values b6y totally random ones)\n" +
                "Current: " + CurrentSettings[4]
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
                        CurrentSettings[i] = input;
                        validInput = true;
                    }
                } while (!validInput);
            }
            Console.WriteLine("Settings Saved, press any key to continue");
            Console.ReadKey();
        }
    }
}