using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private AudioView audioView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LocalizationService>(Lifetime.Singleton);
            builder.Register<AudioService>(Lifetime.Singleton);
            builder.Register<UIColorService>(Lifetime.Singleton);
            builder.Register<ScoreService>(Lifetime.Singleton);

            builder.RegisterComponentInNewPrefab(audioView, Lifetime.Singleton).DontDestroyOnLoad();
        }
    }
}


