using System.Collections.Generic;
using FTRGames.Alpaseh.Enums;
using FTRGames.Alpaseh.Models.LocalizationData;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public class LocalizationService
    {
        private readonly ResourceDataService resourceDataService;
        private readonly List<JObject> languageDataObjects = new List<JObject>();
        private List<Localization> LocalizationDatas { get; } = new List<Localization>();

        public UnityEvent languageChangedEvent = new UnityEvent();

        public LocalizationService(ResourceDataService resourceDataService)
        {
            this.resourceDataService = resourceDataService;
        }

        public int GetLanguageCount => resourceDataService.LanguageCount;

        public void Initialize()
        {
            LocalizationDatas.Clear();
            languageDataObjects.Clear();
            LoadAllLocalizationData();
        }

        private void LoadAllLocalizationData()
        {
            for (int i = 0; i < resourceDataService.LanguageCount; i++)
            {
                JObject languageData = JObject.Parse(resourceDataService.GetLocalizationFile(i).text);
                languageDataObjects.Add(languageData);
                AddLocalizationData(languageData);
            }
        }

        private void AddLocalizationData(JObject languageData)
        {
            var introLocal = languageData["Intro"].ToObject<Intro>();
            var mainMenuLocal = languageData["MainMenu"].ToObject<MainMenu>();
            var howToPlayLocal = languageData["HowToPlay"].ToObject<HowToPlay>();
            var settingsLocal = languageData["Settings"].ToObject<Settings>();
            var highScoresLocal = languageData["HighScores"].ToObject<HighScores>();
            var creditsLocal = languageData["Credits"].ToObject<Credits>();
            var gameLocal = languageData["Game"].ToObject<Game>();
            var languageLocal = ((JArray)languageData["Language"]).ToObject<List<Language>>().ToArray();

            LocalizationDatas.Add(new Localization
            {
                Intro = introLocal,
                MainMenu = mainMenuLocal,
                HowToPlay = howToPlayLocal,
                Settings = settingsLocal,
                HighScores = highScoresLocal,
                Credits = creditsLocal,
                Game = gameLocal,
                Language = languageLocal
            });
        }

        public Localization GetLocalizationData()
        {
            return LocalizationDatas[resourceDataService.SelectedLanguageIndex];
        }

        public string GetLocalizationData(LanguageObject languageObject, string key)
        {
            JObject languageData = languageDataObjects[resourceDataService.SelectedLanguageIndex];
            return languageData[languageObject.ToString()][key].ToObject<string>();
        }

        public List<string> GetLanguageFlagFileNames()
        {
            return new List<string>(resourceDataService.GetLanguageIds());
        }
    }
}
