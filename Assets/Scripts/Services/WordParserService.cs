using FTRGames.Alpaseh.Models;

namespace FTRGames.Alpaseh.Services
{
    public class WordParserService
    {
        private readonly ResourceDataService resourceDataService;

        public WordData WordDatas { get; private set; }

        public WordParserService(ResourceDataService resourceDataService)
        {
            this.resourceDataService = resourceDataService;
        }

        public void Initialization()
        {
            int selectedLanguageIndex = resourceDataService.SelectedLanguageIndex;

            WordDatas = new WordData(
                resourceDataService.GetWordListFile(selectedLanguageIndex).text,
                resourceDataService.GetAllowedLetters(selectedLanguageIndex));
        }
    }
}
