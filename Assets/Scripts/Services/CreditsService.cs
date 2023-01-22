
using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FTRGames.Alpaseh.Services
{
    public class CreditsService
    {
        public void SetVersionValue(CreditsView creditsView)
        {
            creditsView.versionValue.text = Application.version;
        }

        public void GoToMainMenuBtnClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
