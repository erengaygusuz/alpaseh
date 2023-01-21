using FTRGames.Alpaseh.Presenters;
using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
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
}

