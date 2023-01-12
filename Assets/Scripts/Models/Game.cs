using System;

namespace FTRGames.Alpaseh.Model
{

    [Serializable]
    public class Game
    {
        public string TopBarTotalTimeText { get; set; }
        public string TopBarTotalLifeText { get; set; }
        public string TopBarActiveLevelText { get; set; }
        public string TopBarTotalScoreText { get; set; }
        public string ProcessButtonsCheckButtonText { get; set; }
        public string ProcessButtonsDeleteButtonText { get; set; }
        public string ProcessButtonsMainMenuButtonText { get; set; }
        public string GameOverPanelInfoText { get; set; }
        public string GameOverPanelMessageBox1InfoText { get; set; }
        public string GameOverPanelMessageBox1YesBtnText { get; set; }
        public string GameOverPanelMessageBox1NoBtnText { get; set; }
        public string GameOverPanelMessageBox2InfoText { get; set; }
        public string GameOverPanelMessageBox2OkBtnText { get; set; }
        public string GameOverPanelPlayAgainButtonText { get; set; }
        public string GameOverPanelMainMenuButtonText { get; set; }
        public string GameOverPanelExitGameButtonText { get; set; }
    }
}
