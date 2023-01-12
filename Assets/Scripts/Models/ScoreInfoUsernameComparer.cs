using System.Collections.Generic;

namespace FTRGames.Alpaseh.Model
{
    public class ScoreInfoUsernameComparer : IComparer<ScoreInfo>
    {
        public int Compare(ScoreInfo x, ScoreInfo y)
        {
            return x.Username.CompareTo(y.Username);
        }
    }
}
