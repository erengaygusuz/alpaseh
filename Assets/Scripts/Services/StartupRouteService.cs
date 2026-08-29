using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public sealed class StartupRouteService
    {
        private const string UsernameKey = "Alpaseh-Username";
        private const string LanguageKey = "Alpaseh-Language";

        public bool ShouldShowIntro()
        {
            return !PlayerPrefs.HasKey(UsernameKey) || !PlayerPrefs.HasKey(LanguageKey);
        }
    }
}
