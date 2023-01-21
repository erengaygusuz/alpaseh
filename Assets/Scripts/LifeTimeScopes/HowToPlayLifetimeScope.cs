using FTRGames.Alpaseh.Presenters;
using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
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
}

