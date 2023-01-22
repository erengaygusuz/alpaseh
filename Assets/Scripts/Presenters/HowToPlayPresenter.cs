using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class HowToPlayPresenter : IStartable
    {
        private readonly HowToPlayView howToPlayView;
        private readonly HowToPlayService howToPlayService;

        public HowToPlayPresenter(HowToPlayView howToPlayView, HowToPlayService howToPlayService)
        {
            this.howToPlayView = howToPlayView;
            this.howToPlayService = howToPlayService;
        }

        void IStartable.Start()
        {
            howToPlayService.Initialization(howToPlayView);

            EventBinding();
        }

        private void EventBinding()
        {
            howToPlayView.leftArrowButton.onClick.AddListener(() => howToPlayService.LeftArrowBtnClick(howToPlayView));
            howToPlayView.rightArrowButton.onClick.AddListener(() => howToPlayService.RightArrowBtnClick(howToPlayView));
            howToPlayView.mainMenuButton.onClick.AddListener(() => howToPlayService.GoToMainMenuBtnClick());
        }
    }
}
