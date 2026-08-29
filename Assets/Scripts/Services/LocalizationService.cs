using System.Collections.Generic;
using FTRGames.Alpaseh.Enums;
using FTRGames.Alpaseh.Models.LocalizationData;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public sealed class LocalizationService
    {
        private readonly ResourceDataService resourceDataService;
        private readonly List<JObject> languageDataObjects = new List<JObject>();
        private readonly List<Localization> localizationDatas = new List<Localization>();

        public UnityEvent languageChangedEvent = new UnityEvent();

        public LocalizationService(ResourceDataService resourceDataService)
        {
            this.resourceDataService = resourceDataService;
        }

        public int GetLanguageCount => resourceDataService.LanguageCount;

        public void Initialize()
        {
            localizationDatas.Clear();
            languageDataObjects.Clear();
            LoadAllLocalizationData();
        }

        public Localization GetLocalizationData()
        {
            return localizationDatas[resourceDataService.SelectedLanguageIndex];
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

        private void LoadAllLocalizationData()
        {
            for (int i = 0; i < resourceDataService.LanguageCount; i++)
            {
                JObject languageData = JObject.Parse(resourceDataService.GetLocalizationFile(i).text);
                languageDataObjects.Add(languageData);
                localizationDatas.Add(CreateLocalizationData(languageData));
            }
        }

        private static Localization CreateLocalizationData(JObject languageData)
        {
            return new Localization
            {
                Intro = languageData["Intro"].ToObject<Intro>(),
                MainMenu = languageData["MainMenu"].ToObject<MainMenu>(),
                HowToPlay = languageData["HowToPlay"].ToObject<HowToPlay>(),
                Settings = languageData["Settings"].ToObject<Settings>(),
                HighScores = languageData["HighScores"].ToObject<HighScores>(),
                Credits = languageData["Credits"].ToObject<Credits>(),
                Game = languageData["Game"].ToObject<Game>(),
                Language = ((JArray)languageData["Language"]).ToObject<List<Language>>().ToArray()
            };
        }
    }
}
