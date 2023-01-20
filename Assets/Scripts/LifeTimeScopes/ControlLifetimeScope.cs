using FTRGames.Alpaseh.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ControlLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<ControlPresenter>();

        builder.Register<AudioService>(Lifetime.Scoped);
        builder.Register<ControlService>(Lifetime.Scoped);
    }
}
