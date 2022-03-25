using System;
using System.IO;
using static mastermind.Ranking;
using static mastermind.Settings;

namespace mastermind
{
    /// <summary>
    /// Main class for mastermind menus, for starting a game and settings
    /// </summary>
    class Mastermindbase
    {

        /// <summary>
        /// Starts Mastermind
        /// </summary>
        static void Main()
        {
            Start();
        }

        /// <summary>
        /// Manages mastermind main menu
        /// </summary>
        public static void Start()
        {
            bool end;
            do
            {
                Console.ReadLine();
                Sequences.Intro();
                ShowMenu();
                end = ChooseOption();
            } while (!end);
        }

        /// <summary>
        /// Shows MasterMind main menu;
        /// </summary>
        public static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Instructions");
            Console.WriteLine(
                " You have received a message, but it's encrypted... So, in order to decipher it you need to crack a 4 letter code before anything\n" +
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
            Console.WriteLine("[1]:Start Game(Free Game)");
            Console.WriteLine("[2]:Start Game(Ranked Game)");
            Console.WriteLine("[3]:Settings");
            Console.WriteLine("[x]:Close Game");
        }

        /// <summary>
        /// Manages Mastermaind menu options
        /// </summary>
        /// <returns>Binary result to indicate when program should close</returns>
        public static bool ChooseOption()
        {
            do
            {
                bool result;
                var input = Console.ReadKey();
                switch (input.KeyChar)
                {
                    case '1':
                        Game game = new Game();
                        result = game.Start(out int i);
                        if (result)
                            Sequences.Win();
                        else
                            Sequences.Lose();
                        return false;
                    case '2':
                        RMenu();
                        return false;
                    case '3':
                        SettingsLoad();
                        return false;
                    case 'x':
                        return true;
                }
            } while (true);
        }

        /// <summary>
        /// Menu for ranked mastermind
        /// </summary>
        public static void RMenu()
        {
            Directory.SetCurrentDirectory("../..");
            Console.WriteLine("[1]:Easy (Letters:ABCD Amount:4 Attempts:10)");
            Console.WriteLine("[2]:Medium(Letters:ABCDEF Amount:4 Attempts:10)");
            Console.WriteLine("[3]:Hard(Letters:ABCDEFGH Amount:6 Attempts:12)");
            Console.WriteLine("[x]:Return to main menu");
            var input = Console.ReadKey();
            string difficulty = "";
            bool startFlag = false;
            do
            {
                switch (input.KeyChar)
                {
                    case '1':
                        CurrentSettings = new[] {0, 10, 4, 4, 2};
                        difficulty = "Easy";
                        startFlag = true;
                        break;
                    case '2':
                        CurrentSettings = new[] {0, 10, 4, 6, 2};
                        difficulty = "Medium";
                        startFlag = true;
                        break;
                    case '3':
                        CurrentSettings = new[] {0, 12, 6, 8, 2};
                        difficulty = "Hard";
                        startFlag = true;
                        break;
                    case 'x':
                        return;
                }
            } while (!startFlag);

            RPlay(difficulty);
        }
        /// <summary>
        /// Plays a ranked game
        /// </summary>
        /// <param name="difficulty">Difficulty for the ranked game</param>
        public static void RPlay(string difficulty)
        {
            Game rGame = new Game();
            bool win = rGame.Start(out int attemptsDone);
            if (win)
            {
                Sequences.Win();
                highscoreMenu(difficulty, attemptsDone);
            }
            else
            {
                HighscoreDisplayer();
                Sequences.Lose();
            }

            highscoreMenu(difficulty, attemptsDone);
        }

        /// <summary>
        /// Menu to display and chanche hoighsocres after a ranked game
        /// </summary>
        /// <param name="difficulty">Difficulty of the ranked game played</param>
        /// <param name="attemptsDone">Attempts used on the game itself</param>
        public static void highscoreMenu(string difficulty, int attemptsDone)
        {
            string[,] highscore = ScoreReader(difficulty);
            HighscoreDisplayer();
            bool NewScoreFlag = NewScoreChecker(highscore, attemptsDone, out int place);
            if (NewScoreFlag)
                ScoreWriter(highscore, attemptsDone.ToString(), place);
        }
    }
}