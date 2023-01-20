using FTRGames.Alpaseh.Core;
using FTRGames.Alpaseh.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class HowToPlayLifetimeScope : LifetimeScope
{
    [SerializeField]
    private HowToPlayView howToPlayView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<HowToPlayPresenter>();

        builder.Register<HowToPlayService>(Lifetime.Scoped);

        builder.RegisterComponent(howToPlayView);
    }
}
