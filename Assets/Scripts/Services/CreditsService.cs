using FTRGames.Alpaseh.Views;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public class CreditsService
    {
        private readonly SceneNavigationService sceneNavigationService;

        public CreditsService(SceneNavigationService sceneNavigationService)
        {
            this.sceneNavigationService = sceneNavigationService;
        }

        public void SetVersionValue(CreditsView creditsView)
        {
            creditsView.versionValue.text = Application.version;
        }

        public void GoToMainMenuBtnClick()
        {
            sceneNavigationService.Load(SceneNames.MainMenu);
        }
    }
}
