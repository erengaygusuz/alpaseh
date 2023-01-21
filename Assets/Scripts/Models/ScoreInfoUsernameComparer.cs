using System.Collections.Generic;

namespace FTRGames.Alpaseh.Models
{
    public class ScoreInfoUsernameComparer : IComparer<ScoreInfo>
    {
        public int Compare(ScoreInfo x, ScoreInfo y)
        {
            return x.Username.CompareTo(y.Username);
        }
    }
}
