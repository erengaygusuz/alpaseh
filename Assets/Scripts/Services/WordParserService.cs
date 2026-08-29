using FTRGames.Alpaseh.Models;

namespace FTRGames.Alpaseh.Services
{
    public class WordParserService
    {
        private readonly ResourceDataService resourceDataService;

        private char[] IdentifiedLetters { get; set; }
        public WordData WordDatas { get; set; }

        public WordParserService(ResourceDataService resourceDataService)
        {
            this.resourceDataService = resourceDataService;
        }

        public void Initialization()
        {
            int selectedLanguageIndex = resourceDataService.SelectedLanguageIndex;

            LetterIdentification(resourceDataService.GetLanguageId(selectedLanguageIndex));
            ProcessData(selectedLanguageIndex);
        }

        private void LetterIdentification(string languageId)
        {
            if (languageId == ResourceDataService.TurkishLanguageId)
            {
                IdentifiedLetters = new char[]
                {
                    'o', 'O',
                    'e', 'E',
                    'z', 'Z',
                    'ı', 'I',
                    's', 'S',
                    'g', 'G',
                    'b', 'B',
                    'l', 'L',
                    'h', 'H'
                };

                return;
            }

            IdentifiedLetters = new char[]
            {
                'o', 'O',
                'e', 'E',
                'z', 'Z',
                's', 'S',
                'g', 'G',
                'b', 'B',
                'l', 'L',
                'h', 'H'
            };
        }

        private void ProcessData(int selectedLanguageIndex)
        {
            WordDatas = new WordData(
                resourceDataService.GetWordListFile(selectedLanguageIndex).text,
                IdentifiedLetters);
        }
    }
}
