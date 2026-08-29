using FTRGames.Alpaseh.Views;

namespace FTRGames.Alpaseh.Services
{
    public sealed class GameStartupService
    {
        private readonly GameService gameService;
        private readonly GameInputService gameInputService;
        private readonly GameFeedbackService gameFeedbackService;
        private readonly GameEventBindingService gameEventBindingService;
        private readonly AnswerService answerService;
        private readonly ScoreService scoreService;
        private readonly LevelService levelService;
        private readonly WordParserService wordParserService;

        public GameStartupService(
            GameService gameService,
            GameInputService gameInputService,
            GameFeedbackService gameFeedbackService,
            GameEventBindingService gameEventBindingService,
            AnswerService answerService,
            ScoreService scoreService,
            LevelService levelService,
            WordParserService wordParserService)
        {
            this.gameService = gameService;
            this.gameInputService = gameInputService;
            this.gameFeedbackService = gameFeedbackService;
            this.gameEventBindingService = gameEventBindingService;
            this.answerService = answerService;
            this.scoreService = scoreService;
            this.levelService = levelService;
            this.wordParserService = wordParserService;
        }

        public void Start(GameView gameView, AudioView audioView)
        {
            scoreService.Initialization();
            wordParserService.Initialization();
            answerService.Initialization();
            levelService.Initialization();
            gameFeedbackService.Initialization();

            gameService.Initialization(audioView, levelService, gameView);
            gameEventBindingService.Bind(gameView, audioView, levelService);
            gameInputService.Enable();
        }
    }
}
