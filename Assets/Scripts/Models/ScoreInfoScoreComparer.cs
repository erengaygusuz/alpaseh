using System.Collections.Generic;

namespace FTRGames.Alpaseh.Models
{
    public class ScoreInfoScoreComparer : IComparer<ScoreInfo>
    {
        public int Compare(ScoreInfo x, ScoreInfo y)
        {
            return x.Score.CompareTo(y.Score);
        }
    }
}
