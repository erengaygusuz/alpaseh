using VContainer.Unity;

namespace FTRGames.Alpaseh.Scenes
{
    public class HowToPlayPresenter : IStartable
    {
        private readonly HowToPlayService howToPlayService;

        public HowToPlayPresenter(HowToPlayService howToPlayService)
        {
            this.howToPlayService = howToPlayService;
        }

        void IStartable.Start()
        {
            howToPlayService.Initialization();
        }
    }
}
