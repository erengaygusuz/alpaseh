using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public sealed class StartupRouteService
    {
        public bool ShouldShowIntro()
        {
            return !PlayerPrefs.HasKey(PlayerPrefsKeys.Username) || !PlayerPrefs.HasKey(PlayerPrefsKeys.Language);
        }
    }
}
