using FTRGames.Alpaseh.Services;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class CreditsPresenter : IStartable
    {
        private CreditsService creditsService;

        public CreditsPresenter(CreditsService creditsService)
        {
            this.creditsService = creditsService;
        }

        void IStartable.Start()
        {
            creditsService.UIReferenceInit();
            creditsService.UIEventBinding();
            creditsService.AssignTranslatedValues();
        }
    }
}
