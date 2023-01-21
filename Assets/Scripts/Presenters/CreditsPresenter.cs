using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class CreditsPresenter : IStartable
    {
        private readonly LocalizationService localizationService;
        private readonly CreditsView creditsView;

        public CreditsPresenter(LocalizationService localizationService, CreditsView creditsView)
        {
            this.localizationService = localizationService;
            this.creditsView = creditsView;
        }

        void IStartable.Start()
        {
            UIReferenceInit();
            UIEventBinding();
            AssignTranslatedValues();
        }

        private void UIReferenceInit()
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject contents = canvas.transform.GetChild(1).gameObject;
            creditsView.companyText = contents.transform.GetChild(0).GetComponent<Text>();
            creditsView.developerText = contents.transform.GetChild(1).GetComponent<Text>();
            creditsView.versionText = contents.transform.GetChild(4).GetComponent<Text>();
            creditsView.mainMenuButton = canvas.transform.GetChild(2).GetComponent<Button>();
            creditsView.mainMenuButtonText = creditsView.mainMenuButton.transform.GetChild(0).GetComponent<Text>();
        }

        private void UIEventBinding()
        {
            creditsView.mainMenuButton.onClick.AddListener(GoToMainMenuBtnClick);
        }

        private void GoToMainMenuBtnClick()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void AssignTranslatedValues()
        {
            creditsView.companyText.text = localizationService.GetLocalizationData().Credits.CompanyText;
            creditsView.developerText.text = localizationService.GetLocalizationData().Credits.DeveloperText;
            creditsView.versionText.text = localizationService.GetLocalizationData().Credits.VersionText + Application.version;
            creditsView.mainMenuButtonText.text = localizationService.GetLocalizationData().Credits.MainMenuButtonText;
        }
    }
}
