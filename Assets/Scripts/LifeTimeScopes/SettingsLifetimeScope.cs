using FTRGames.Alpaseh.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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
