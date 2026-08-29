using System;
using System.Collections.Generic;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public sealed class ResourceDataService
    {
        public const string EnglishLanguageId = "english";
        public const string TurkishLanguageId = "turkish";

        private readonly LanguageCatalog languageCatalog;

        public ResourceDataService(LanguageCatalog languageCatalog)
        {
            this.languageCatalog = languageCatalog ??
                throw new ArgumentNullException(nameof(languageCatalog));
        }

        public int LanguageCount => languageCatalog.Count;

        public int SelectedLanguageIndex => NormalizeLanguageIndex(
            PlayerPrefs.GetInt(PlayerPrefsKeys.SelectedLanguageIndex, 0));

        public IReadOnlyList<string> GetLanguageIds()
        {
            var languageIds = new string[LanguageCount];

            for (int i = 0; i < LanguageCount; i++)
            {
                languageIds[i] = languageCatalog.GetLanguage(i).Id;
            }

            return languageIds;
        }

        public string GetLanguageId(int languageIndex)
        {
            return GetLanguage(languageIndex).Id;
        }

        public TextAsset GetLocalizationFile(int languageIndex)
        {
            return GetRequiredAsset(
                GetLanguage(languageIndex).LocalizationFile,
                "localization file",
                languageIndex);
        }

        public TextAsset GetWordListFile(int languageIndex)
        {
            return GetRequiredAsset(
                GetLanguage(languageIndex).WordListFile,
                "word list file",
                languageIndex);
        }

        public Sprite GetLanguageFlag(int languageIndex)
        {
            return GetRequiredAsset(
                GetLanguage(languageIndex).Flag,
                "flag",
                languageIndex);
        }

        private LanguageCatalogEntry GetLanguage(int languageIndex)
        {
            return languageCatalog.GetLanguage(NormalizeLanguageIndex(languageIndex));
        }

        private int NormalizeLanguageIndex(int languageIndex)
        {
            if (LanguageCount <= 0)
            {
                throw new InvalidOperationException("Language catalog is empty.");
            }

            return Mathf.Clamp(languageIndex, 0, LanguageCount - 1);
        }

        private T GetRequiredAsset<T>(T asset, string assetType, int languageIndex)
            where T : UnityEngine.Object
        {
            if (asset == null)
            {
                string languageId = GetLanguage(languageIndex).Id;
                throw new InvalidOperationException(
                    $"Language '{languageId}' is missing its {assetType} reference in the language catalog.");
            }

            return asset;
        }
    }
}
