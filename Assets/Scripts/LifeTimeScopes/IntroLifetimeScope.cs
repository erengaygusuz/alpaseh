using FTRGames.Alpaseh.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class IntroLifetimeScope : LifetimeScope
{
    [SerializeField]
    private IntroView introView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<IntroPresenter>(Lifetime.Scoped);

        builder.Register<IntroService>(Lifetime.Scoped);

        builder.RegisterComponent(introView);
    }
}
