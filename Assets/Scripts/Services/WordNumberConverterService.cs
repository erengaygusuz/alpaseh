using System.Collections.Generic;

namespace FTRGames.Alpaseh.Services
{
    public sealed class WordNumberConverterService
    {
        private readonly ResourceDataService resourceDataService;
        private readonly Dictionary<char, char> wordNumberPairs = new Dictionary<char, char>();

        public WordNumberConverterService(ResourceDataService resourceDataService)
        {
            this.resourceDataService = resourceDataService;
        }

        public void Initialization()
        {
            CreateWordNumberPairs(resourceDataService.SelectedLanguageIndex);
        }

        public string GetNumbersFromWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return string.Empty;
            }

            var numberCharacters = new List<char>();

            for (int i = 0; i < word.Length; i++)
            {
                if (wordNumberPairs.TryGetValue(word[i], out char numberCharacter))
                {
                    numberCharacters.Add(numberCharacter);
                }
            }

            numberCharacters.Reverse();
            return new string(numberCharacters.ToArray());
        }

        private void CreateWordNumberPairs(int languageIndex)
        {
            wordNumberPairs.Clear();

            string sourceCharacters = resourceDataService.GetWordNumberSourceCharacters(languageIndex);
            string targetCharacters = resourceDataService.GetWordNumberTargetCharacters(languageIndex);

            for (int i = 0; i < sourceCharacters.Length; i++)
            {
                wordNumberPairs[sourceCharacters[i]] = targetCharacters[i];
            }
        }
    }
}
