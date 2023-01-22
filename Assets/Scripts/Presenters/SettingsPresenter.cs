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

            settingsView.themesToggles[0].onValueChanged.AddListener(delegate
            {
                settingsService.SetSelectedColorIndex0();
            });

            settingsView.themesToggles[1].onValueChanged.AddListener(delegate
            {
                settingsService.SetSelectedColorIndex1();
            });

            settingsView.themesToggles[2].onValueChanged.AddListener(delegate
            {
                settingsService.SetSelectedColorIndex2();
            });

            settingsView.themesToggles[3].onValueChanged.AddListener(delegate
            {
                settingsService.SetSelectedColorIndex3();
            });

            settingsView.audioLevelSlider.onValueChanged.AddListener(delegate
            {
                settingsService.SetAudioLevelValues(settingsView);
            });

            settingsView.languageOptions.onValueChanged.AddListener(delegate
            {
                settingsService.SaveLanguageOption(settingsView);
            });
        }
    }
}