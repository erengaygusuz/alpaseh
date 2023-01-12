using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using FTRGames.Alpaseh.Model;

namespace FTRGames.Alpaseh.Core
{
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField]
        private TextAsset[] languageFiles;
        private static List<Localization> LocalizationDatas { get; set; }
        public static bool IsLanguageChanged { get; set; }

        public void Awake()
        {
            DontDestroyOnLoad(this);

            LocalizationDataInit();

            GetAllLocalizationDatas();
        }

        private void LocalizationDataInit()
        {
            LocalizationDatas = new List<Localization>();
        }

        private void GetAllLocalizationDatas()
        {
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

        public static Localization GetLocalizationData()
        {
            return LocalizationDatas[PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex")];
        }

        public static int GetLanguageCount
        {
            get
            {
                return languageFiles.Length;
            }
        }

        public static List<string> GetLanguageFlagFileNames()
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