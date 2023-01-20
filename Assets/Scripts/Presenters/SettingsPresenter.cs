using VContainer.Unity;

namespace FTRGames.Alpaseh.Scenes
{
    public class SettingsPresenter : IStartable, ITickable
    {
        private readonly SettingsService settingsService;

        public SettingsPresenter(SettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        void IStartable.Start()
        {
            settingsService.Initialization();
        }

        public void Tick()
        {
            settingsService.LanguageCheck();
        }
    }
}