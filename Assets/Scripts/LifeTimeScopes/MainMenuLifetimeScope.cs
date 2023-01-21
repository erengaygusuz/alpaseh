using FTRGames.Alpaseh.Presenters;
using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
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
}

