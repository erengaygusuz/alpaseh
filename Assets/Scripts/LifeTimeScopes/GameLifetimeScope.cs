using FTRGames.Alpaseh.Core;
using FTRGames.Alpaseh.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private GameView gameView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GamePresenter>();

        builder.Register<GameService>(Lifetime.Scoped);
        builder.Register<WordParser>(Lifetime.Scoped);
        builder.Register<TweenService>(Lifetime.Scoped);
        builder.Register<WordNumberConverter>(Lifetime.Scoped);
        builder.Register<LevelService>(Lifetime.Scoped);

        builder.RegisterComponent(gameView);
    }
}
