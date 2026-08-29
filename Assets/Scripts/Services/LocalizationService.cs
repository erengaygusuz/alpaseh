using System.Collections.Generic;
using FTRGames.Alpaseh.Enums;
using FTRGames.Alpaseh.Models.LocalizationData;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public class LocalizationService
    {
        private TextAsset[] languageFiles = new TextAsset[0];
        private List<Localization> LocalizationDatas { get; } = new List<Localization>();

        public UnityEvent languageChangedEvent = new UnityEvent();

        public int GetLanguageCount
        {
            get
            {
                return languageFiles.Length;
            }
        }

        public void Initialize()
        {
            LocalizationDatas.Clear();
            LoadAllLocalizationData();
        }

        private void LoadAllLocalizationData()
        {
            languageFiles = Resources.LoadAll<TextAsset>("Language/");

            for (var i = 0; i < languageFiles.Length; i++)
            {
                AddLocalizationData(i);
            }
        }

        private void AddLocalizationData(int selectedLanguageIndex)
        {
            var languageData = JObject.Parse(languageFiles[selectedLanguageIndex].text);

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
            return LocalizationDatas[PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex")];
        }

        public string GetLocalizationData(LanguageObject languageObject, string key)
        {
            var languageData = JObject.Parse(languageFiles[PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex")].text);

            return languageData[languageObject.ToString()][key].ToObject<string>();
        }

        public List<string> GetLanguageFlagFileNames()
        {
            var fileNames = new List<string>();

            for (var i = 0; i < languageFiles.Length; i++)
            {
                fileNames.Add(languageFiles[i].name);
            }

            return fileNames;
        }
    }
}
