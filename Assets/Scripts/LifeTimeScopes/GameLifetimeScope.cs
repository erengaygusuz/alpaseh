using FTRGames.Alpaseh.Presenters;
using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameView gameView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GamePresenter>();

            builder.Register<GameService>(Lifetime.Scoped);
            builder.Register<WordParserService>(Lifetime.Scoped);
            builder.Register<TweenService>(Lifetime.Scoped);
            builder.Register<WordNumberConverterService>(Lifetime.Scoped);
            builder.Register<LevelService>(Lifetime.Scoped);

            builder.RegisterComponent(gameView);
        }
    }
}

