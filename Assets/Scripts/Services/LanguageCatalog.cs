using System;
using System.Collections.Generic;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    [CreateAssetMenu(fileName = "LanguageCatalog", menuName = "Alpaseh/Data/Language Catalog")]
    public sealed class LanguageCatalog : ScriptableObject
    {
        [SerializeField]
        private LanguageCatalogEntry[] languages = Array.Empty<LanguageCatalogEntry>();

        public int Count => languages?.Length ?? 0;

        public IReadOnlyList<LanguageCatalogEntry> Languages => languages;

        public LanguageCatalogEntry GetLanguage(int languageIndex)
        {
            if (languages == null || languages.Length == 0)
            {
                throw new InvalidOperationException("Language catalog does not contain any language entries.");
            }

            int normalizedIndex = Mathf.Clamp(languageIndex, 0, languages.Length - 1);
            LanguageCatalogEntry language = languages[normalizedIndex];

            if (language == null)
            {
                throw new InvalidOperationException($"Language catalog entry at index {normalizedIndex} is missing.");
            }

            return language;
        }
    }

    [Serializable]
    public sealed class LanguageCatalogEntry
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private TextAsset localizationFile;

        [SerializeField]
        private TextAsset wordListFile;

        [SerializeField]
        private Sprite flag;

        [SerializeField]
        private string allowedLetters;

        public string Id => id;
        public TextAsset LocalizationFile => localizationFile;
        public TextAsset WordListFile => wordListFile;
        public Sprite Flag => flag;
        public string AllowedLetters => allowedLetters;
    }
}
