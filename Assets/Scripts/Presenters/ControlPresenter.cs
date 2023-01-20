using VContainer.Unity;

namespace FTRGames.Alpaseh.Core
{
    public class ControlPresenter : IStartable
    {
        private readonly ControlService controlService;
        private readonly LocalizationService localizationService;
        private readonly UIColorService uiColorService;

        public ControlPresenter(ControlService controlService, LocalizationService localizationService, UIColorService uiColorService)
        {
            this.controlService = controlService;
            this.localizationService = localizationService;
            this.uiColorService = uiColorService;
        }

        void IStartable.Start()
        {
            localizationService.Initialization();
            uiColorService.GetAllSchemes();
            controlService.CheckKeys();
            controlService.AudioLevelKeyInit();
        }
    }
}
