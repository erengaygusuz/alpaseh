using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FTRGames.Alpaseh.Services
{
    public class MainMenuService
    {
        private readonly AudioView audioView;
        private readonly AudioService audioService;

        public MainMenuService(AudioService audioService, AudioView audioView)
        {
            this.audioService = audioService;
            this.audioView = audioView;
        }

        public void Initialization(MainMenuView mainMenuView)
        {
            PlayBackgroundAudio();
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
    }
}

