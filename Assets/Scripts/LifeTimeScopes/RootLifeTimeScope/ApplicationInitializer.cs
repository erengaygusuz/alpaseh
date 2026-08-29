using FTRGames.Alpaseh.Services;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
    public sealed class ApplicationInitializer : IInitializable
    {
        private readonly LocalizationService localizationService;
        private readonly AudioService audioService;

        public ApplicationInitializer(
            LocalizationService localizationService,
            AudioService audioService)
        {
            this.localizationService = localizationService;
            this.audioService = audioService;
        }

        public void Initialize()
        {
            localizationService.Initialize();
            audioService.Initialize();
        }
    }
}
