using VContainer.Unity;

namespace FTRGames.Alpaseh.Scenes
{
    public class MainMenuPresenter : IStartable
    {
        private readonly MainMenuService mainMenuService;

        public MainMenuPresenter(MainMenuService mainMenuService)
        {
            this.mainMenuService = mainMenuService;
        }

        void IStartable.Start()
        {
            mainMenuService.Initialization();
        }
    }
}
