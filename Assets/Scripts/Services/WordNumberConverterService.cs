using System.Collections.Generic;

namespace FTRGames.Alpaseh.Services
{
    public sealed class WordNumberConverterService
    {
        private readonly ResourceDataService resourceDataService;
        private CharacterMapping wordNumberMapping;

        public WordNumberConverterService(ResourceDataService resourceDataService)
        {
            this.resourceDataService = resourceDataService;
        }

        public void Initialization()
        {
            wordNumberMapping = resourceDataService.GetWordNumberMapping(
                resourceDataService.SelectedLanguageIndex);
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
                if (wordNumberMapping.TryGetValue(word[i], out char numberCharacter))
                {
                    numberCharacters.Add(numberCharacter);
                }
            }

            numberCharacters.Reverse();
            return new string(numberCharacters.ToArray());
        }
    }
}
