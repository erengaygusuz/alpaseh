using System.Collections.Generic;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Services
{
    public sealed class SettingsService
    {
        private static readonly Color32 ActiveTabColor = new Color32(219, 219, 219, 255);
        private static readonly Color32 InactiveTabColor = new Color32(188, 188, 188, 255);

        private readonly LocalizationService localizationService;
        private readonly ResourceDataService resourceDataService;
        private readonly AudioService audioService;
        private readonly UIColorService uiColorService;
        private readonly SceneNavigationService sceneNavigationService;

        public SettingsService(
            LocalizationService localizationService,
            ResourceDataService resourceDataService,
            AudioService audioService,
            UIColorService uiColorService,
            SceneNavigationService sceneNavigationService)
        {
            this.localizationService = localizationService;
            this.resourceDataService = resourceDataService;
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
            settingsView.usernameValue.text = PlayerPrefs.GetString(PlayerPrefsKeys.Username);
        }

        public void GetLanguageValues(SettingsView settingsView)
        {
            settingsView.languageOptions.value = resourceDataService.SelectedLanguageIndex;
        }

        public void FillLanguageDropdown(SettingsView settingsView)
        {
            var options = new List<Dropdown.OptionData>();

            for (int i = 0; i < localizationService.GetLanguageCount; i++)
            {
                options.Add(new Dropdown.OptionData(
                    localizationService.GetLocalizationData().Language[i].Name,
                    resourceDataService.GetLanguageFlag(i)));
            }

            settingsView.languageOptions.ClearOptions();
            settingsView.languageOptions.AddOptions(options);
            GetLanguageValues(settingsView);
        }

        public void PersonalTabClick(SettingsView settingsView)
        {
            SetTabState(settingsView, isPersonalTabActive: true);
        }

        public void GeneralTabClick(SettingsView settingsView)
        {
            SetTabState(settingsView, isPersonalTabActive: false);
            ActivateSelectedThemeToggle(settingsView);
        }

        public void SaveLanguageOption(SettingsView settingsView)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.SelectedLanguageIndex, settingsView.languageOptions.value);
            PlayerPrefs.Save();

            RefreshLanguageOptionLabels(settingsView);
            localizationService.languageChangedEvent.Invoke();
        }

        public void GoToMainMenuBtnClick(SettingsView settingsView)
        {
            SaveUsernameValue(settingsView);
            sceneNavigationService.Load(SceneNames.MainMenu);
        }

        public void SetAudioLevelValues(SettingsView settingsView)
        {
            audioService.SetVolumeAndSave(settingsView.audioLevelSlider.value);
            SetAudioLevelLabelValue(settingsView);
        }

        public void SetSelectedColorIndex(int index)
        {
            uiColorService.SelectColorScheme(index);
        }

        private static void SetTabState(SettingsView settingsView, bool isPersonalTabActive)
        {
            settingsView.personalTab.GetComponent<Image>().color = isPersonalTabActive ? ActiveTabColor : InactiveTabColor;
            settingsView.generalTab.GetComponent<Image>().color = isPersonalTabActive ? InactiveTabColor : ActiveTabColor;
            settingsView.personalTabContent.SetActive(isPersonalTabActive);
            settingsView.generalTabContent.SetActive(!isPersonalTabActive);
        }

        private void RefreshLanguageOptionLabels(SettingsView settingsView)
        {
            for (int i = 0; i < localizationService.GetLanguageCount; i++)
            {
                settingsView.languageOptions.options[i].text = localizationService.GetLocalizationData().Language[i].Name;
            }

            settingsView.languageOptions.captionText.text =
                settingsView.languageOptions.options[settingsView.languageOptions.value].text;
        }

        private static void SaveUsernameValue(SettingsView settingsView)
        {
            PlayerPrefs.SetString(PlayerPrefsKeys.Username, settingsView.usernameValue.text);
            PlayerPrefs.Save();
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

        private void ActivateSelectedThemeToggle(SettingsView settingsView)
        {
            if (settingsView.themesToggles == null || settingsView.themesToggles.Length == 0)
            {
                return;
            }

            int activeIndex = Mathf.Clamp(
                PlayerPrefs.GetInt(PlayerPrefsKeys.SelectedColorSchemeIndex, 0),
                0,
                settingsView.themesToggles.Length - 1);

            settingsView.themesToggles[activeIndex].isOn = true;
        }
    }
}
