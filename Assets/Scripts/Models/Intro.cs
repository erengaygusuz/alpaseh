using System;

namespace FTRGames.Alpaseh.Model
{
    [Serializable]
    public class Intro
    {
        public string UsernameLabel { get; set; }
        public string UsernameInputFieldPlaceholder { get; set; }
        public string LanguageLabel { get; set; }
        public string NextButtonText { get; set; }
        public string MessageBoxInfoText { get; set; }
        public string MessageBoxOkButtonText { get; set; }
    }
}
