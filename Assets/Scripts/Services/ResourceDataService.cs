using System;
using System.Collections.Generic;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public sealed class ResourceDataService
    {
        public const string EnglishLanguageId = "english";
        public const string TurkishLanguageId = "turkish";

        private static readonly string[] LanguageIds =
        {
            EnglishLanguageId,
            TurkishLanguageId
        };

        public int LanguageCount => LanguageIds.Length;

        public int SelectedLanguageIndex => NormalizeLanguageIndex(
            PlayerPrefs.GetInt(PlayerPrefsKeys.SelectedLanguageIndex, 0));

        public IReadOnlyList<string> GetLanguageIds()
        {
            return LanguageIds;
        }

        public string GetLanguageId(int languageIndex)
        {
            return LanguageIds[NormalizeLanguageIndex(languageIndex)];
        }

        public TextAsset GetLocalizationFile(int languageIndex)
        {
            return LoadRequired<TextAsset>($"Language/{GetLanguageId(languageIndex)}");
        }

        public TextAsset GetWordListFile(int languageIndex)
        {
            return LoadRequired<TextAsset>($"WordList/word-{GetLanguageId(languageIndex)}");
        }

        public Sprite GetLanguageFlag(int languageIndex)
        {
            return LoadRequired<Sprite>($"Flags/{GetLanguageId(languageIndex)}");
        }

        private static int NormalizeLanguageIndex(int languageIndex)
        {
            return Mathf.Clamp(languageIndex, 0, LanguageIds.Length - 1);
        }

        private static T LoadRequired<T>(string resourcePath) where T : UnityEngine.Object
        {
            T asset = Resources.Load<T>(resourcePath);

            if (asset == null)
            {
                throw new InvalidOperationException($"Required resource could not be loaded: {resourcePath}");
            }

            return asset;
        }
    }
}
