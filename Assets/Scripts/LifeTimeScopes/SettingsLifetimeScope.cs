using FTRGames.Alpaseh.Presenters;
using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
    public class SettingsLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private SettingsView settingsView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<SettingsPresenter>();

            builder.Register<SettingsService>(Lifetime.Scoped);

            builder.RegisterComponent(settingsView);
        }
    }
}

