using System;

namespace FTRGames.Alpaseh.Models
{
    [Serializable]
    public class Settings
    {
        public string GeneralTabText { get; set; }
        public string PersonalTabText { get; set; }
        public string GeneralContentAudioLabel { get; set; }
        public string GeneralContentThemesLabel { get; set; }
        public string PersonalContentUsernameLabel { get; set; }
        public string PersonalContentUsernameInputFieldPlaceholder { get; set; }
        public string PersonalContentLanguageLabel { get; set; }
        public string MainMenuButtonText { get; set; }
    }
}
