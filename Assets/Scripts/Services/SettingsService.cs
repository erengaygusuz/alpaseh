using FTRGames.Alpaseh.Views;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Services
{
    public class SettingsService
    {
        private const string UsernameKey = "Alpaseh-Username";
        private const string SelectedLanguageIndexKey = "Alpaseh-SelectedLanguageIndex";
        private const string SelectedColorSchemeIndexKey = "Alpaseh-SelectedColorSchemeIndex";

        private readonly LocalizationService localizationService;
        private readonly AudioService audioService;
        private readonly UIColorService uiColorService;
        private readonly SceneNavigationService sceneNavigationService;

        public SettingsService(
            LocalizationService localizationService,
            AudioService audioService,
            UIColorService uiColorService,
            SceneNavigationService sceneNavigationService)
        {
            this.localizationService = localizationService;
            this.audioService = audioService;
            this.uiColorService = uiColorService;
            this.sceneNavigationService = sceneNavigationService;
        }

        public void Initialization(SettingsView settingsView)
        {
            GetUserNameValue(settingsView);
            FillLanguageDropdown(settingsView);
            GetLanguageValues(settingsView);
            GetAudioLevelValues(settingsView);
        }

        public void GetUserNameValue(SettingsView settingsView)
        {
            settingsView.usernameValue.text = PlayerPrefs.GetString(UsernameKey);
        }

        public void GetLanguageValues(SettingsView settingsView)
        {
            settingsView.languageOptions.value = PlayerPrefs.GetInt(SelectedLanguageIndexKey, 0);
        }

        public void FillLanguageDropdown(SettingsView settingsView)
        {
            var options = new List<Dropdown.OptionData>();

            for (int i = 0; i < localizationService.GetLanguageCount; i++)
            {
                var option = new Dropdown.OptionData(
                    localizationService.GetLocalizationData().Language[i].Name,
                    Resources.Load<Sprite>("Flags/" + localizationService.GetLanguageFlagFileNames()[i]));

                options.Add(option);
            }

            settingsView.languageOptions.ClearOptions();
            settingsView.languageOptions.AddOptions(options);

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

        public void SaveLanguageOption(SettingsView settingsView)
        {
            PlayerPrefs.SetInt(SelectedLanguageIndexKey, settingsView.languageOptions.value);
            PlayerPrefs.Save();

            for (int i = 0; i < localizationService.GetLanguageCount; i++)
            {
                settingsView.languageOptions.options[i].text = localizationService.GetLocalizationData().Language[i].Name;
            }

            settingsView.languageOptions.captionText.text = settingsView.languageOptions.options[settingsView.languageOptions.value].text;

            localizationService.languageChangedEvent.Invoke();
        }

        public void GoToMainMenuBtnClick(SettingsView settingsView)
        {
            SaveUsernameValue(settingsView);
            sceneNavigationService.Load(SceneNames.MainMenu);
        }

        private void SaveUsernameValue(SettingsView settingsView)
        {
            PlayerPrefs.SetString(UsernameKey, settingsView.usernameValue.text);
            PlayerPrefs.Save();
        }

        public void SetAudioLevelValues(SettingsView settingsView)
        {
            audioService.SetVolumeAndSave(settingsView.audioLevelSlider.value);
            SetAudioLevelLabelValue(settingsView);
        }

        private void SetAudioLevelLabelValue(SettingsView settingsView)
        {
            settingsView.audioLevelLabelValue.text = Mathf.RoundToInt(audioService.Volume * 100).ToString();
        }

        private void GetAudioLevelValues(SettingsView settingsView)
        {
            audioService.SetVolume(audioService.SavedVolume);
            settingsView.audioLevelSlider.value = audioService.Volume;
            SetAudioLevelLabelValue(settingsView);
        }

        public void SetSelectedColorIndex(int index)
        {
            uiColorService.SelectColorScheme(index);
        }

        private void ActivateSelectedThemeToggle(SettingsView settingsView)
        {
            if (settingsView.themesToggles == null || settingsView.themesToggles.Length == 0)
            {
                return;
            }

            var activeIndex = Mathf.Clamp(
                PlayerPrefs.GetInt(SelectedColorSchemeIndexKey, 0),
                0,
                settingsView.themesToggles.Length - 1);

            settingsView.themesToggles[activeIndex].isOn = true;
        }
    }
}
