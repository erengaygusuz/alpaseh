using FTRGames.Alpaseh.Views;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Services
{
    public class IntroService
    {
        private readonly LocalizationService localizationService;
        private readonly ResourceDataService resourceDataService;
        private readonly SceneNavigationService sceneNavigationService;

        public IntroService(
            LocalizationService localizationService,
            ResourceDataService resourceDataService,
            SceneNavigationService sceneNavigationService)
        {
            this.localizationService = localizationService;
            this.resourceDataService = resourceDataService;
            this.sceneNavigationService = sceneNavigationService;
        }

        public void Initialization(IntroView introView)
        {
            FillLanguageDropdown(introView);
        }

        private void GetLanguageValues(IntroView introView)
        {
            introView.languageOptions.value = resourceDataService.SelectedLanguageIndex;
        }

        private void FillLanguageDropdown(IntroView introView)
        {
            var options = new List<Dropdown.OptionData>();

            for (int i = 0; i < localizationService.GetLanguageCount; i++)
            {
                var option = new Dropdown.OptionData(
                    localizationService.GetLocalizationData().Language[i].Name,
                    resourceDataService.GetLanguageFlag(i));

                options.Add(option);
            }

            introView.languageOptions.ClearOptions();
            introView.languageOptions.AddOptions(options);

            GetLanguageValues(introView);
        }

        public void NextBtnClick(IntroView introView)
        {
            if (introView.username.text == "")
            {
                introView.warningPanel.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.Username, introView.username.text);
                PlayerPrefs.SetString(PlayerPrefsKeys.Language, introView.languageOptions.captionText.text);
                sceneNavigationService.Load(SceneNames.MainMenu);
            }
        }

        public void WarningPanelOKBtnClick(IntroView introView)
        {
            introView.warningPanel.SetActive(false);
        }

        public void SaveLanguageOption(IntroView introView)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.SelectedLanguageIndex, introView.languageOptions.value);
            PlayerPrefs.Save();

            for (int i = 0; i < localizationService.GetLanguageCount; i++)
            {
                introView.languageOptions.options[i].text = localizationService.GetLocalizationData().Language[i].Name;
            }

            introView.languageOptions.captionText.text = introView.languageOptions.options[introView.languageOptions.value].text;

            localizationService.languageChangedEvent.Invoke();
        }
    }
}
