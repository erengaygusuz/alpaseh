
using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Services
{
    public class CreditsService
    {
        public void Initialization(CreditsView creditsView, LocalizationService localizationService)
        {
            UIReferenceInit(creditsView);
            AssignTranslatedValues(creditsView, localizationService);
        }

        private void UIReferenceInit(CreditsView creditsView)
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject contents = canvas.transform.GetChild(1).gameObject;
            creditsView.companyText = contents.transform.GetChild(0).GetComponent<Text>();
            creditsView.developerText = contents.transform.GetChild(1).GetComponent<Text>();
            creditsView.versionText = contents.transform.GetChild(4).GetComponent<Text>();
            creditsView.mainMenuButton = canvas.transform.GetChild(2).GetComponent<Button>();
            creditsView.mainMenuButtonText = creditsView.mainMenuButton.transform.GetChild(0).GetComponent<Text>();
        }

        private void AssignTranslatedValues(CreditsView creditsView, LocalizationService localizationService)
        {
            creditsView.companyText.text = localizationService.GetLocalizationData().Credits.CompanyText;
            creditsView.developerText.text = localizationService.GetLocalizationData().Credits.DeveloperText;
            creditsView.versionText.text = localizationService.GetLocalizationData().Credits.VersionText + Application.version;
            creditsView.mainMenuButtonText.text = localizationService.GetLocalizationData().Credits.MainMenuButtonText;
        }

        public void GoToMainMenuBtnClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
