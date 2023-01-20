using VContainer.Unity;

namespace FTRGames.Alpaseh.Scenes
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