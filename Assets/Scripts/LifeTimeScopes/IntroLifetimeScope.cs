using FTRGames.Alpaseh.Presenters;
using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
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
}

