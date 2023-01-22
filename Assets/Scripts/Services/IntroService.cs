using FTRGames.Alpaseh.Views;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Services
{
    public class IntroService
    {
        private readonly LocalizationService localizationService;

        public IntroService(LocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        public void Initialization(IntroView introView)
        {
            FillLanguageDropdown(introView);
        }

        private void GetLanguageValues(IntroView introView)
        {
            if (PlayerPrefs.HasKey("Alpaseh-SelectedLanguageIndex") == true)
            {
                introView.languageOptions.value = PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex");
            }

            else
            {
                introView.languageOptions.value = 0;
            }
        }

        private void FillLanguageDropdown(IntroView introView)
        {
            List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();

            for (int i = 0; i < localizationService.GetLanguageCount; i++)
            {
                Dropdown.OptionData option = new Dropdown.OptionData(localizationService.GetLocalizationData().Language[i].Name, Resources.Load<Sprite>("Flags/" + localizationService.GetLanguageFlagFileNames()[i]));
                list.Add(option);
            }

            introView.languageOptions.AddOptions(list);

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
                PlayerPrefs.SetString("Alpaseh-Username", introView.username.text);
                PlayerPrefs.SetString("Alpaseh-Language", introView.languageOptions.captionText.text);
                SceneManager.LoadScene("MainMenu");
            }
        }

        public void WarningPanelOKBtnClick(IntroView introView)
        {
            introView.warningPanel.SetActive(false);
        }

        public void SaveLanguageOption(IntroView introView)
        {
            PlayerPrefs.SetInt("Alpaseh-SelectedLanguageIndex", introView.languageOptions.value);
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

