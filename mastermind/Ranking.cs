using System;
using System.IO;

namespace mastermind

{
    /// <summary>
    /// Class for the rankings
    /// </summary>
    public class Ranking
    {
        /// <summary>
        /// Path for the ranking file to work with
        /// </summary>
        private static string path;
        /// <summary>
        /// Reads the scores from the chosen file
        /// </summary>
        /// <param name="difficulty">Difficulty of the chosen ranking</param>
        /// <returns>Matrix with all the scores in the file</returns>
        public static string[,] ScoreReader(string difficulty)
        {
            path = "highscores" + difficulty + ".txt";
            if (!File.Exists(path))
                File.CreateText(path);
            using (StreamReader sr = File.OpenText(path))
            {
                string[,] highscores = new string[10, 2];
                for (int i = 0; i < 10; i++)
                {
                    string score = sr.ReadLine();
                    if (score == null)
                        return highscores;
                    string[] scoresplit = score.Split();
                    highscores[i, 0] = scoresplit[0];
                    highscores[i, 1] = scoresplit[1];
                }

                return highscores;
            }
        }
/// <summary>
/// Displays the scores in the chosen file
/// </summary>
        public static void HighscoreDisplayer()
        {
            Console.Clear();
            using (StreamReader sr = File.OpenText(path))
            {
                for (int i = 0; i < 10; i++)
                {
                   Console.WriteLine(sr.ReadLine()); 
                }
            }
        }
/// <summary>
/// Checks if the user has archieved a new highscore
/// </summary>
/// <param name="highscores">Matrix with the stored scores</param>
/// <param name="attemptsDone">Attempts done by the user hwo achieved the new score</param>
/// <param name="place">Place in the ranking for the new score player</param>
/// <returns>Binary result will be true if a new score has been achieved</returns>
        public static bool NewScoreChecker(string[,] highscores, int attemptsDone, out int place)
        {
            for (int i = 0; i < 10; i++)
            {
                if (highscores[i, 1] == null || Convert.ToInt32(highscores[i, 1]) > attemptsDone)
                {
                    place = i;
                    return true;
                }
            }

            place = -1;
            return false;
        }
/// <summary>
/// Writes the new score in the chosen file
/// </summary>
/// <param name="highscores">Matrix with the stored scores</param>
/// <param name="newScore">Score achieved by the player entering the ranking</param>
/// <param name="place">Place in the ranking for the new score player</param>
        public static void ScoreWriter(string[,] highscores, string newScore, int place)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            using (StreamWriter sw = new StreamWriter(path))
            {
                bool iFlag = false;
                Console.WriteLine("NEW HIGHSCORE!!! Introduce your name to save it");
                string pName = Console.ReadLine();
                
                for (int i = 0; i < 9; i++)
                {
                    if (i == place && !iFlag)
                    {
                        sw.WriteLine(pName + " " + newScore);
                        iFlag = true;
                        i--;
                    }
                    else if (highscores[i, 0] != null)
                        sw.WriteLine(highscores[i, 0] + " " + highscores[i, 1]);
                }
                if (iFlag == false)
                    sw.WriteLine(pName + " " + newScore);
            }
        }
    }
}