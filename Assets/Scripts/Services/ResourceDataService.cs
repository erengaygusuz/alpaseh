using System;
using System.Collections.Generic;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public sealed class ResourceDataService
    {
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

        public string GetAllowedLetters(int languageIndex)
        {
            LanguageCatalogEntry language = GetLanguage(languageIndex);

            if (string.IsNullOrWhiteSpace(language.AllowedLetters))
            {
                throw new InvalidOperationException(
                    $"Language '{language.Id}' is missing its allowed letters in the language catalog.");
            }

            return language.AllowedLetters;
        }

        public string GetQuestionNormalizationSourceCharacters(int languageIndex)
        {
            return GetLanguage(languageIndex).QuestionNormalizationSourceCharacters ?? string.Empty;
        }

        public string GetQuestionNormalizationTargetCharacters(int languageIndex)
        {
            LanguageCatalogEntry language = GetLanguage(languageIndex);
            string sourceCharacters = language.QuestionNormalizationSourceCharacters ?? string.Empty;
            string targetCharacters = language.QuestionNormalizationTargetCharacters ?? string.Empty;

            ValidateCharacterMappingLength(
                language.Id,
                "question normalization",
                sourceCharacters,
                targetCharacters);

            return targetCharacters;
        }

        public string GetWordNumberSourceCharacters(int languageIndex)
        {
            LanguageCatalogEntry language = GetLanguage(languageIndex);

            if (string.IsNullOrWhiteSpace(language.WordNumberSourceCharacters))
            {
                throw new InvalidOperationException(
                    $"Language '{language.Id}' is missing its word number source characters in the language catalog.");
            }

            return language.WordNumberSourceCharacters;
        }

        public string GetWordNumberTargetCharacters(int languageIndex)
        {
            LanguageCatalogEntry language = GetLanguage(languageIndex);
            string sourceCharacters = language.WordNumberSourceCharacters ?? string.Empty;
            string targetCharacters = language.WordNumberTargetCharacters ?? string.Empty;

            ValidateCharacterMappingLength(
                language.Id,
                "word number",
                sourceCharacters,
                targetCharacters);

            return targetCharacters;
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

        private static void ValidateCharacterMappingLength(
            string languageId,
            string mappingName,
            string sourceCharacters,
            string targetCharacters)
        {
            if (sourceCharacters.Length == targetCharacters.Length)
            {
                return;
            }

            throw new InvalidOperationException(
                $"Language '{languageId}' {mappingName} source and target character counts do not match.");
        }
    }
}
