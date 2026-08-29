using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine.UI;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class SettingsPresenter : IStartable
    {
        private readonly SettingsView settingsView;
        private readonly SettingsService settingsService;

        public SettingsPresenter(SettingsView settingsView, SettingsService settingsService)
        {
            this.settingsView = settingsView;
            this.settingsService = settingsService;
        }

        void IStartable.Start()
        {
            settingsService.Initialization(settingsView);
            EventBinding(settingsView);
        }

        private void EventBinding(SettingsView settingsView)
        {
            settingsView.generalTab.GetComponent<Button>().onClick.AddListener(() => settingsService.GeneralTabClick(settingsView));
            settingsView.personalTab.GetComponent<Button>().onClick.AddListener(() => settingsService.PersonalTabClick(settingsView));
            settingsView.mainMenuButton.onClick.AddListener(() => settingsService.GoToMainMenuBtnClick(settingsView));

            for (int i = 0; i < settingsView.themesToggles.Length; i++)
            {
                var themeIndex = i;

                settingsView.themesToggles[i].onValueChanged.AddListener(isOn =>
                {
                    if (isOn)
                    {
                        settingsService.SetSelectedColorIndex(themeIndex);
                    }
                });
            }

            settingsView.audioLevelSlider.onValueChanged.AddListener(_ =>
            {
                settingsService.SetAudioLevelValues(settingsView);
            });

            settingsView.languageOptions.onValueChanged.AddListener(_ =>
            {
                settingsService.SaveLanguageOption(settingsView);
            });
        }
    }
}
