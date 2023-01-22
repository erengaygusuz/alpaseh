using FTRGames.Alpaseh.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FTRGames.Alpaseh.Services
{
    public class LanguageAssigner : MonoBehaviour
    {
        private List<string> LanguageObjects
        {
            get
            {
                return new List<string>()
                {
                    "Intro",
                    "MainMenu",
                    "HowToPlay",
                    "Settings",
                    "HighScores",
                    "Credits",
                    "Game",
                    "Language"
                };
            }
        }

        public LanguageObject SelectedLanguageObject;

        public string languageKey;

        private LocalizationService localizationService;

        [Inject]
        public void Construct(LocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        private void Start()
        {
            localizationService.languageChangedEvent.AddListener(AssignLanguageValue);

            AssignLanguageValue();
        }

        private void AssignLanguageValue()
        {
            GetComponent<Text>().text = localizationService.GetLocalizationData(SelectedLanguageObject, languageKey);
        }
    }
}
