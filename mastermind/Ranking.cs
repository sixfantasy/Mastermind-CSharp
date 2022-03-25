using System;
using System.IO;

namespace mastermind

{
    public class Ranking
    {
        private static string path;
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