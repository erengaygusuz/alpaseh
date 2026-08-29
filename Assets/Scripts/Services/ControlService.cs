using UnityEngine;
using UnityEngine.SceneManagement;

namespace FTRGames.Alpaseh.Services
{
    public sealed class ControlService
    {
        public void LoadInitialScene()
        {
            var targetScene = PlayerPrefs.HasKey("Alpaseh-Username") &&
                              PlayerPrefs.HasKey("Alpaseh-Language")
                ? "MainMenu"
                : "Intro";

            SceneManager.LoadScene(targetScene);
        }
    }
}
