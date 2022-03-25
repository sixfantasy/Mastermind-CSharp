using static mastermind.Ranking;

namespace mastermind
{
    public class RankingTest
    {
        public static void TestReader()
        {
            string[,] highscore = ScoreReader("Sample");
            ScoreReader("Sample");
            int attemptsDone = 7;
            bool NewScoreFlag = NewScoreChecker(highscore, attemptsDone, out int place);
            if (NewScoreFlag)
                ScoreWriter(highscore, attemptsDone.ToString(), place);

        }
    }
}