using FTRGames.Alpaseh.Views;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public class MainMenuService
    {
        private readonly AudioView audioView;
        private readonly AudioService audioService;
        private readonly SceneNavigationService sceneNavigationService;

        public MainMenuService(
            AudioService audioService,
            AudioView audioView,
            SceneNavigationService sceneNavigationService)
        {
            this.audioService = audioService;
            this.audioView = audioView;
            this.sceneNavigationService = sceneNavigationService;
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
            sceneNavigationService.Load(SceneNames.Game);
        }

        public void HowToPlayBtnClick()
        {
            sceneNavigationService.Load(SceneNames.HowToPlay);
        }

        public void SettingsBtnClick()
        {
            sceneNavigationService.Load(SceneNames.Settings);
        }

        public void HighScoresBtnClick()
        {
            sceneNavigationService.Load(SceneNames.HighScores);
        }

        public void CreditsBtnClick()
        {
            sceneNavigationService.Load(SceneNames.Credits);
        }

        public void ExitBtnClick()
        {
            Application.Quit();
        }
    }
}
