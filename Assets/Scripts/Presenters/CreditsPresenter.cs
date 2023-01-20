using VContainer.Unity;

namespace FTRGames.Alpaseh.Scenes
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
