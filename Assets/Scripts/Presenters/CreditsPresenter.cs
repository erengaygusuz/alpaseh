using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class CreditsPresenter : IStartable
    {
        private readonly CreditsService creditsService;
        private readonly LocalizationService localizationService;
        private readonly CreditsView creditsView;

        public CreditsPresenter(LocalizationService localizationService, CreditsView creditsView, CreditsService creditsService)
        {
            this.creditsService = creditsService;
            this.localizationService = localizationService;
            this.creditsView = creditsView;
        }

        void IStartable.Start()
        {
            creditsService.UIReferenceInit(creditsView);
            creditsService.AssignTranslatedValues(creditsView, localizationService);

            EventBinding();
        }

        private void EventBinding()
        {
            creditsView.mainMenuButton.onClick.AddListener(() => creditsService.GoToMainMenuBtnClick());
        }
    }
}
