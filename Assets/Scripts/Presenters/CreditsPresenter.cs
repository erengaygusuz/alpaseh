using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class CreditsPresenter : IStartable
    {
        private readonly CreditsService creditsService;
        private readonly CreditsView creditsView;

        public CreditsPresenter(CreditsView creditsView, CreditsService creditsService)
        {
            this.creditsService = creditsService;
            this.creditsView = creditsView;
        }

        void IStartable.Start()
        {
            creditsService.SetVersionValue(creditsView);

            EventBinding();
        }

        private void EventBinding()
        {
            creditsView.mainMenuButton.onClick.AddListener(() => creditsService.GoToMainMenuBtnClick());
        }
    }
}
