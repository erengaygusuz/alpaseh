using FTRGames.Alpaseh.Views;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Services
{
    public class SettingsService
    {
        private readonly AudioView audioView;
        private readonly LocalizationService localizationService;

        public SettingsService(LocalizationService localizationService, AudioView audioView)
        {
            this.localizationService = localizationService;
            this.audioView = audioView;
        }

        public void Initialization(SettingsView settingsView)
        {
            GetUserNameValue(settingsView);
            FillLanguageDropdown(settingsView);
            GetLanguageValues(settingsView);
            AssignTranslatedValues(settingsView);
            GetAudioLevelValues(settingsView);
        }

        public void GetUserNameValue(SettingsView settingsView)
        {
            settingsView.usernameValue.text = PlayerPrefs.GetString("Alpaseh-Username");
        }

        public void GetLanguageValues(SettingsView settingsView)
        {
            if (PlayerPrefs.HasKey("Alpaseh-SelectedLanguageIndex") == true)
            {
                settingsView.languageOptions.value = PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex");
            }

            else
            {
                settingsView.languageOptions.value = 0;
            }
        }

        public void FillLanguageDropdown(SettingsView settingsView)
        {
            List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();

            for (int i = 0; i < localizationService.GetLanguageCount; i++)
            {
                Dropdown.OptionData option = new Dropdown.OptionData(localizationService.GetLocalizationData().Language[i].Name, Resources.Load<Sprite>("Flags/" + localizationService.GetLanguageFlagFileNames()[i]));
                list.Add(option);
            }

            settingsView.languageOptions.AddOptions(list);

            GetLanguageValues(settingsView);
        }

        public void PersonalTabClick(SettingsView settingsView)
        {
            settingsView.personalTab.GetComponent<Image>().color = new Color32(219, 219, 219, 255);
            settingsView.generalTab.GetComponent<Image>().color = new Color32(188, 188, 188, 255);
            settingsView.personalTabContent.SetActive(true);
            settingsView.generalTabContent.SetActive(false);
        }

        public void GeneralTabClick(SettingsView settingsView)
        {
            settingsView.personalTab.GetComponent<Image>().color = new Color32(188, 188, 188, 255);
            settingsView.generalTab.GetComponent<Image>().color = new Color32(219, 219, 219, 255);
            settingsView.personalTabContent.SetActive(false);
            settingsView.generalTabContent.SetActive(true);

            ActivateSelectedThemeToggle(settingsView);
        }

        public void AssignTranslatedValues(SettingsView settingsView)
        {
            settingsView.generalTabText.text = localizationService.GetLocalizationData().Settings.GeneralTabText;
            settingsView.personalTabText.text = localizationService.GetLocalizationData().Settings.PersonalTabText;
            settingsView.generalContentAudioLabel.text = localizationService.GetLocalizationData().Settings.GeneralContentAudioLabel;
            settingsView.generalContentThemesLabel.text = localizationService.GetLocalizationData().Settings.GeneralContentThemesLabel;
            settingsView.personalContentUsernameLabel.text = localizationService.GetLocalizationData().Settings.PersonalContentUsernameLabel;
            settingsView.personalContentUsernameInputFieldPlaceholder.text = localizationService.GetLocalizationData().Settings.PersonalContentUsernameInputFieldPlaceholder;
            settingsView.personalContentLanguageLabel.text = localizationService.GetLocalizationData().Settings.PersonalContentLanguageLabel;
            settingsView.mainMenuButtonText.text = localizationService.GetLocalizationData().Settings.MainMenuButtonText;
        }

        public void SaveLanguageOption(SettingsView settingsView)
        {
            PlayerPrefs.SetInt("Alpaseh-SelectedLanguageIndex", settingsView.languageOptions.value);
            PlayerPrefs.Save();

            for (int i = 0; i < localizationService.GetLanguageCount; i++)
            {
                settingsView.languageOptions.options[i].text = localizationService.GetLocalizationData().Language[i].Name;
            }

            settingsView.languageOptions.captionText.text = settingsView.languageOptions.options[settingsView.languageOptions.value].text;

            localizationService.IsLanguageChanged = true;
        }

        public void GoToMainMenuBtnClick(SettingsView settingsView)
        {
            SaveUsernameValue(settingsView);

            SceneManager.LoadScene("MainMenu");
        }

        private void SaveUsernameValue(SettingsView settingsView)
        {
            PlayerPrefs.SetString("Alpaseh-Username", settingsView.usernameValue.text);
        }

        public void SetAudioLevelValues(SettingsView settingsView)
        {
            audioView.loopAudioSource.volume = settingsView.audioLevelSlider.value;
            audioView.answerAudioSource.volume = settingsView.audioLevelSlider.value;
            audioView.timeTickAudioSource.volume = settingsView.audioLevelSlider.value;

            SetAudioLevelLabelValue(settingsView);

            SaveAudioLevelValue(settingsView);
        }

        private void SetAudioLevelLabelValue(SettingsView settingsView)
        {
            settingsView.audioLevelLabelValue.text = Mathf.RoundToInt(audioView.loopAudioSource.volume * 100).ToString();
        }

        private void SaveAudioLevelValue(SettingsView settingsView)
        {
            PlayerPrefs.SetFloat("Alpaseh-AudioLevelSliderValue", settingsView.audioLevelSlider.value);
            PlayerPrefs.Save();
        }

        private void GetAudioLevelValues(SettingsView settingsView)
        {
            audioView.loopAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
            audioView.answerAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
            audioView.timeTickAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");

            SetAudioLevelLabelValue(settingsView);

            settingsView.audioLevelSlider.value = audioView.loopAudioSource.volume;
        }

        public void SetSelectedColorIndex0()
        {
            int activeToggleIndex = 0;

            PlayerPrefs.SetInt("Alpaseh-SelectedColorSchemeIndex", activeToggleIndex);

            PlayerPrefs.Save();

            for (int i = 0; i < GameObject.FindObjectsOfType<ColorAssigner>().Length; i++)
            {
                GameObject.FindObjectsOfType<ColorAssigner>()[i].IsColorSchemeChanged = true;
            }
        }

        public void SetSelectedColorIndex1()
        {
            int activeToggleIndex = 1;

            PlayerPrefs.SetInt("Alpaseh-SelectedColorSchemeIndex", activeToggleIndex);

            PlayerPrefs.Save();

            for (int i = 0; i < GameObject.FindObjectsOfType<ColorAssigner>().Length; i++)
            {
                GameObject.FindObjectsOfType<ColorAssigner>()[i].IsColorSchemeChanged = true;
            }
        }

        public void SetSelectedColorIndex2()
        {
            int activeToggleIndex = 2;

            PlayerPrefs.SetInt("Alpaseh-SelectedColorSchemeIndex", activeToggleIndex);

            PlayerPrefs.Save();

            for (int i = 0; i < GameObject.FindObjectsOfType<ColorAssigner>().Length; i++)
            {
                GameObject.FindObjectsOfType<ColorAssigner>()[i].IsColorSchemeChanged = true;
            }
        }

        public void SetSelectedColorIndex3()
        {
            int activeToggleIndex = 3;

            PlayerPrefs.SetInt("Alpaseh-SelectedColorSchemeIndex", activeToggleIndex);

            PlayerPrefs.Save();

            for (int i = 0; i < GameObject.FindObjectsOfType<ColorAssigner>().Length; i++)
            {
                GameObject.FindObjectsOfType<ColorAssigner>()[i].IsColorSchemeChanged = true;
            }
        }

        private void ActivateSelectedThemeToggle(SettingsView settingsView)
        {
            settingsView.themesToggles[PlayerPrefs.GetInt("Alpaseh-SelectedColorSchemeIndex", 0)].isOn = true;
        }

        public void LanguageCheck(SettingsView settingsView)
        {
            if (localizationService.IsLanguageChanged == true)
            {
                AssignTranslatedValues(settingsView);

                localizationService.IsLanguageChanged = false;
            }
        }
    }
}

