using FTRGames.Alpaseh.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CreditsLifetimeScope : LifetimeScope
{
    [SerializeField]
    private CreditsView creditsView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<CreditsPresenter>();

        builder.Register<CreditsService>(Lifetime.Scoped);

        builder.RegisterComponent(creditsView);
    }
}
