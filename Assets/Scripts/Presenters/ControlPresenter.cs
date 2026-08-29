using FTRGames.Alpaseh.Services;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public sealed class ControlPresenter : IStartable
    {
        private readonly ControlService controlService;

        public ControlPresenter(ControlService controlService)
        {
            this.controlService = controlService;
        }

        public void Start()
        {
            controlService.LoadInitialScene();
        }
    }
}
