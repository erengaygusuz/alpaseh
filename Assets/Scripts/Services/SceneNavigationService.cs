using System;
using UnityEngine.SceneManagement;

namespace FTRGames.Alpaseh.Services
{
    public sealed class SceneNavigationService
    {
        public void Load(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                throw new ArgumentException("Scene name cannot be null or whitespace.", nameof(sceneName));
            }

            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
