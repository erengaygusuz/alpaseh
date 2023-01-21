using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FTRGames.Alpaseh.Services
{
    public class MainMenuService
    {
        private readonly AudioView audioView;
        private readonly AudioService audioService;
        private readonly LocalizationService localizationService;

        public MainMenuService(AudioService audioService, LocalizationService localizationService, AudioView audioView)
        {
            this.audioService = audioService;
            this.localizationService = localizationService;
            this.audioView = audioView;
        }

        public void Initialization(MainMenuView mainMenuView)
        {
            PlayBackgroundAudio();
            AssignTranslatedValues(mainMenuView);
        }

        private void PlayBackgroundAudio()
        {
            if (audioView.loopAudioSource.clip != null)
            {
                if (audioView.loopAudioSource.clip.name != "main-menu")
                {
                    audioService.StopAudio(audioView.loopAudioSource);
                }
            }

            audioService.PlayMainMenuAudio();
        }

        public void StartGameBtnClick()
        {
            SceneManager.LoadScene("Game");
        }

        public void HowToPlayBtnClick()
        {
            SceneManager.LoadScene("HowToPlay");
        }

        public void SettingsBtnClick()
        {
            SceneManager.LoadScene("Settings");
        }

        public void HighScoresBtnClick()
        {
            SceneManager.LoadScene("HighScores");
        }

        public void CreditsBtnClick()
        {
            SceneManager.LoadScene("Credits");
        }

        public void ExitBtnClick()
        {
            Application.Quit();
        }

        private void AssignTranslatedValues(MainMenuView mainMenuView)
        {
            mainMenuView.startGameButtonText.text = localizationService.GetLocalizationData().MainMenu.StartGameButtonText;
            mainMenuView.howToPlayButtonText.text = localizationService.GetLocalizationData().MainMenu.HowToPlayButtonText;
            mainMenuView.settingsButtonText.text = localizationService.GetLocalizationData().MainMenu.SettingsButtonText;
            mainMenuView.highScoresButtonText.text = localizationService.GetLocalizationData().MainMenu.HighScoresButtonText;
            mainMenuView.creditsButtonText.text = localizationService.GetLocalizationData().MainMenu.CreditsButtonText;
            mainMenuView.exitButtonText.text = localizationService.GetLocalizationData().MainMenu.ExitButtonText;
        }
    }
}

