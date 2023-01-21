using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class MainMenuPresenter : IStartable
    {
        private readonly MainMenuView mainMenuView;
        private readonly MainMenuService mainMenuService;

        public MainMenuPresenter(MainMenuView mainMenuView, MainMenuService mainMenuService)
        {
            this.mainMenuService = mainMenuService;
            this.mainMenuView = mainMenuView;
        }

        void IStartable.Start()
        {
            mainMenuService.Initialization(mainMenuView);

            EventBinding();
        }

        private void EventBinding()
        {
            mainMenuView.startGameButton.onClick.AddListener(() => mainMenuService.StartGameBtnClick());
            mainMenuView.howToPlayButton.onClick.AddListener(() => mainMenuService.HowToPlayBtnClick());
            mainMenuView.settingsButton.onClick.AddListener(() => mainMenuService.SettingsBtnClick());
            mainMenuView.highScoresButton.onClick.AddListener(() => mainMenuService.HighScoresBtnClick());
            mainMenuView.creditsButton.onClick.AddListener(() => mainMenuService.CreditsBtnClick());
            mainMenuView.exitButton.onClick.AddListener(() => mainMenuService.ExitBtnClick());
        }
    }
}
