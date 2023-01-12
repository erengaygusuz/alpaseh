using System;

namespace FTRGames.Alpaseh.Model
{
    [Serializable]
    public class HighScores
    {
        public string ScoreLabelsUsernameText { get; set; }
        public string ScoreLabelsScoreText { get; set; }
        public string MessageBox1InfoText { get; set; }
        public string MessageBox1YesButtonText { get; set; }
        public string MessageBox1NoButtonText { get; set; }
        public string MessageBox2InfoText { get; set; }
        public string MessageBox2OkButtonText { get; set; }
        public string MessageBox3InfoText { get; set; }
        public string MessageBox3OkButtonText { get; set; }
        public string MainMenuButtonText { get; set; }
        public string DeleteAllButtonText { get; set; }
    }
}