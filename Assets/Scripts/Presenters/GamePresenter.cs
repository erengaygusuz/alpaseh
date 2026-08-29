using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class GamePresenter : IStartable, ITickable
    {
        private readonly GameView gameView;
        private readonly AudioView audioView;
        private readonly GameService gameService;
        private readonly GameInputService gameInputService;
        private readonly GameFeedbackService gameFeedbackService;
        private readonly GameEventBindingService gameEventBindingService;
        private readonly AnswerService answerService;
        private readonly ScoreService scoreService;
        private readonly LevelService levelService;
        private readonly WordParserService wordParserService;

        public GamePresenter(
            GameService gameService,
            GameInputService gameInputService,
            GameFeedbackService gameFeedbackService,
            GameEventBindingService gameEventBindingService,
            AnswerService answerService,
            ScoreService scoreService,
            GameView gameView,
            AudioView audioView,
            LevelService levelService,
            WordParserService wordParserService)
        {
            this.gameService = gameService;
            this.gameInputService = gameInputService;
            this.gameFeedbackService = gameFeedbackService;
            this.gameEventBindingService = gameEventBindingService;
            this.answerService = answerService;
            this.scoreService = scoreService;
            this.gameView = gameView;
            this.audioView = audioView;
            this.levelService = levelService;
            this.wordParserService = wordParserService;
        }

        void IStartable.Start()
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

        public void Tick()
        {
            scoreService.UpdateScoreValues();
            gameService.GameCheck(gameView);
        }
    }
}
