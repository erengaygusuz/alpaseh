namespace FTRGames.Alpaseh.Services
{
    public sealed class QuestionService
    {
        public string GetActiveQuestion(LevelService levelService)
        {
            var activeLevel = levelService.GetActiveLevel();
            string question = activeLevel.WordList[activeLevel.ActiveQuestionIndex];

            return NormalizeQuestion(question);
        }

        public bool IsLastQuestion(LevelService levelService)
        {
            if (levelService.Levels == null || levelService.Levels.Length == 0)
            {
                return false;
            }

            int lastLevelIndex = levelService.Levels.Length - 1;

            if (levelService.ActiveLevelIndex != lastLevelIndex)
            {
                return false;
            }

            var lastLevel = levelService.Levels[lastLevelIndex];

            if (lastLevel.WordList == null || lastLevel.WordList.Count == 0)
            {
                return false;
            }

            return lastLevel.ActiveQuestionIndex == lastLevel.WordList.Count - 1;
        }

        private static string NormalizeQuestion(string text)
        {
            char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

            for (int i = 0; i < turkishChars.Length; i++)
            {
                text = text.Replace(turkishChars[i], englishChars[i]);
            }

            return text;
        }
    }
}
