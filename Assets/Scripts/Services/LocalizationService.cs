using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using FTRGames.Alpaseh.Models.LocalizationData;
using UnityEngine.Events;
using FTRGames.Alpaseh.Enums;

namespace FTRGames.Alpaseh.Services
{
    public class LocalizationService
    {
        private TextAsset[] languageFiles;
        private List<Localization> LocalizationDatas { get; set; }

        public UnityEvent languageChangedEvent;

        public int GetLanguageCount
        {
            get
            {
                return languageFiles.Length;
            }
        }

        public void Initialization()
        {
            LocalizationDataInit();
            GetAllLocalizationDatas();
        }

        private void LocalizationDataInit()
        {
            LocalizationDatas = new List<Localization>();
            languageChangedEvent = new UnityEvent();
        }

        private void GetAllLocalizationDatas()
        {
            languageFiles = Resources.LoadAll<TextAsset>("Language/");

            for (int i = 0; i < languageFiles.Length; i++)
            {
                SetLocalizationData(i);
            }
        }

        private void SetLocalizationData(int selectedLanguageIndex)
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

            LocalizationDatas.Add(new Localization { 
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
            List<string> tempList = new List<string>();

            for (int i = 0; i < languageFiles.Length; i++)
            {
                tempList.Add(languageFiles[i].name);
            }

            return tempList;
        }
    }
}