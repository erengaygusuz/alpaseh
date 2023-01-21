using FTRGames.Alpaseh.Presenters;
using FTRGames.Alpaseh.Services;
using VContainer;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
    public class ControlLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ControlPresenter>();

            builder.Register<AudioService>(Lifetime.Scoped);
            builder.Register<ControlService>(Lifetime.Scoped);
        }
    }
}

