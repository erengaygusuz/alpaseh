using System;

namespace FTRGames.Alpaseh.Models.LocalizationData
{
    [Serializable]
    public class Localization
    {
        public Intro Intro { get; set; }
        public MainMenu MainMenu { get; set; }
        public HowToPlay HowToPlay { get; set; }
        public Settings Settings { get; set; }
        public HighScores HighScores { get; set; }
        public Credits Credits { get; set; }
        public Game Game { get; set; }
        public Language[] Language { get; set; }
    }
}
