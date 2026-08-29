namespace FTRGames.Alpaseh.Services
{
    public sealed class AnswerService
    {
        private readonly WordNumberConverterService wordNumberConverterService;

        public AnswerService(WordNumberConverterService wordNumberConverterService)
        {
            this.wordNumberConverterService = wordNumberConverterService;
        }

        public void Initialization()
        {
            wordNumberConverterService.Initialization();
        }

        public bool CheckAnswer(LevelService levelService, string enteredNumberWord, string question)
        {
            var activeLevel = levelService.GetActiveLevel();
            string activeQuestionNumberWord = wordNumberConverterService.GetNumbersFromWord(question);

            return activeLevel.CheckEnteredNumberWord(enteredNumberWord, activeQuestionNumberWord);
        }
    }
}
