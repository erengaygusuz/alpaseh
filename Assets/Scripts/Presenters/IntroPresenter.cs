using FTRGames.Alpaseh.Services;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class IntroPresenter : IStartable, ITickable
    {
        private readonly IntroService introService;

        public IntroPresenter(IntroService introService)
        {
            this.introService = introService;
        }

        void IStartable.Start()
        {
            introService.Initialization();
        }

        public void Tick()
        {
            introService.CheckLanguageState();
        }
    }
}