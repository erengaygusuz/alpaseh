using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using FTRGames.Alpaseh.Model;

namespace FTRGames.Alpaseh.Core
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
            Localization tempLocal = JsonConvert.DeserializeObject<Localization>(languageFiles[selectedLanguageIndex].text);

            LocalizationDatas.Add(tempLocal);
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