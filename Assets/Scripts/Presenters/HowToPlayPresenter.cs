using FTRGames.Alpaseh.Services;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
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
