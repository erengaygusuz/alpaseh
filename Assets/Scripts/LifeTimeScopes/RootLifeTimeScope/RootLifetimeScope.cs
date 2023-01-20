using FTRGames.Alpaseh.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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
