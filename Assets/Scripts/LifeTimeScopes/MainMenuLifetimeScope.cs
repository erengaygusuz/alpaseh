using FTRGames.Alpaseh.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuLifetimeScope : LifetimeScope
{
    [SerializeField]
    private MainMenuView mainMenuView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<MainMenuPresenter>(Lifetime.Scoped);

        builder.Register<MainMenuService>(Lifetime.Scoped);

        builder.RegisterComponent(mainMenuView);
    }
}
