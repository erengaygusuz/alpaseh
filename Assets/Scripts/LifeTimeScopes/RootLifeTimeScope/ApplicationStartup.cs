using FTRGames.Alpaseh.Services;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
    public sealed class ApplicationStartup : IStartable
    {
        private readonly StartupRouteService startupRouteService;
        private readonly SceneNavigationService sceneNavigationService;

        public ApplicationStartup(
            StartupRouteService startupRouteService,
            SceneNavigationService sceneNavigationService)
        {
            this.startupRouteService = startupRouteService;
            this.sceneNavigationService = sceneNavigationService;
        }

        public void Start()
        {
            var activeScene = SceneManager.GetActiveScene();

            if (activeScene.name == SceneNames.MainMenu && startupRouteService.ShouldShowIntro())
            {
                sceneNavigationService.Load(SceneNames.Intro);
            }
        }
    }
}
