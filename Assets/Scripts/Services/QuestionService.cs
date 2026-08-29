namespace FTRGames.Alpaseh.Services
{
    public sealed class QuestionService
    {
        private readonly ResourceDataService resourceDataService;

        public QuestionService(ResourceDataService resourceDataService)
        {
            this.resourceDataService = resourceDataService;
        }

        public string GetActiveQuestion(LevelService levelService)
        {
            var activeLevel = levelService.GetActiveLevel();
            string question = activeLevel.WordList[activeLevel.ActiveQuestionIndex];

            return NormalizeQuestion(question);
        }

        public bool IsLastQuestion(LevelService levelService)
        {
            if (levelService.LevelCount == 0 || !levelService.IsLastLevel)
            {
                return false;
            }

            var lastLevel = levelService.GetActiveLevel();

            if (lastLevel.WordList == null || lastLevel.WordList.Count == 0)
            {
                return false;
            }

            return lastLevel.ActiveQuestionIndex == lastLevel.WordList.Count - 1;
        }

        private string NormalizeQuestion(string text)
        {
            int languageIndex = resourceDataService.SelectedLanguageIndex;
            string sourceCharacters = resourceDataService.GetQuestionNormalizationSourceCharacters(languageIndex);
            string targetCharacters = resourceDataService.GetQuestionNormalizationTargetCharacters(languageIndex);

            for (int i = 0; i < sourceCharacters.Length; i++)
            {
                text = text.Replace(sourceCharacters[i], targetCharacters[i]);
            }

            return text;
        }
    }
}
