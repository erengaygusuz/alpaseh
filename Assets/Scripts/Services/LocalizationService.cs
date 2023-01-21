using UnityEngine;
using System.Collections.Generic;
using FTRGames.Alpaseh.Models;
using Newtonsoft.Json.Linq;

namespace FTRGames.Alpaseh.Services
{
    public class LocalizationService
    {
        private TextAsset[] languageFiles;
        private List<Localization> LocalizationDatas { get; set; }
        public bool IsLanguageChanged { get; set; }
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
            JObject rss = JObject.Parse(languageFiles[selectedLanguageIndex].text);

            var introLocal = rss["Intro"].ToObject<Intro>();
            var mainMenuLocal = rss["MainMenu"].ToObject<MainMenu>();
            var howToPlayLocal = rss["HowToPlay"].ToObject<HowToPlay>();
            var settingsLocal = rss["Settings"].ToObject<Settings>();
            var highScoresLocal = rss["HighScores"].ToObject<HighScores>();
            var creditsLocal = rss["Credits"].ToObject<Credits>();
            var gameLocal = rss["Game"].ToObject<Game>();
            var languageLocal = ((JArray)rss["Language"]).ToObject<List<Language>>().ToArray();

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