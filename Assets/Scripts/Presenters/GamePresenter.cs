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
        private readonly GameUIService gameUIService;
        private readonly GameFeedbackService gameFeedbackService;
        private readonly AnswerService answerService;
        private readonly ScoreService scoreService;
        private readonly LevelService levelService;
        private readonly WordParserService wordParserService;
        private readonly AudioService audioService;

        public GamePresenter(
            GameService gameService,
            GameInputService gameInputService,
            GameUIService gameUIService,
            GameFeedbackService gameFeedbackService,
            AnswerService answerService,
            ScoreService scoreService,
            GameView gameView,
            AudioView audioView,
            LevelService levelService,
            WordParserService wordParserService,
            AudioService audioService)
        {
            this.gameService = gameService;
            this.gameInputService = gameInputService;
            this.gameUIService = gameUIService;
            this.gameFeedbackService = gameFeedbackService;
            this.answerService = answerService;
            this.scoreService = scoreService;
            this.gameView = gameView;
            this.audioView = audioView;
            this.levelService = levelService;
            this.wordParserService = wordParserService;
            this.audioService = audioService;
        }

        void IStartable.Start()
        {
            scoreService.Initialization();
            wordParserService.Initialization();
            answerService.Initialization();
            levelService.Initialization();
            gameFeedbackService.Initialization();

            gameService.Initialization(audioView, levelService, gameView);

            EventBinding();
            gameInputService.Enable();
        }

        public void Tick()
        {
            scoreService.UpdateScoreValues();
            gameService.GameCheck(gameView);
        }

        private void EventBinding()
        {
            gameView.numberButtons[0].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 0));
            gameView.numberButtons[1].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 1));
            gameView.numberButtons[2].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 2));
            gameView.numberButtons[3].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 3));
            gameView.numberButtons[4].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 4));
            gameView.numberButtons[5].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 5));
            gameView.numberButtons[6].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 6));
            gameView.numberButtons[7].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 7));
            gameView.numberButtons[8].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 8));
            gameView.numberButtons[9].onClick.AddListener(() => gameInputService.EnterNumber(gameView, 9));

            gameView.checkButton.onClick.AddListener(() => gameService.ControlBtnClick(gameView, levelService));
            gameView.deleteButton.onClick.AddListener(() => gameInputService.Delete(gameView));
            gameView.mainMenuButton.onClick.AddListener(() => gameService.GoToMainMenuBtnClick(gameView));
            gameView.gameOverPanelPlayAgainButton.onClick.AddListener(() => gameService.PlayAgainBtnClick());
            gameView.gameOverPanelExitButton.onClick.AddListener(() => gameService.ExitGameBtnClick());
            gameView.gameOverPanelMainMenuButton.onClick.AddListener(() => gameService.GoToMainMenuBtnClick(gameView));
            gameView.infoPanelYesButton.onClick.AddListener(() => gameService.InfoPanelYesBtnClick(gameView));
            gameView.infoPanelNoButton.onClick.AddListener(() => gameService.InfoPanelNoBtnClick(gameView));
            gameView.infoPanelOkButton.onClick.AddListener(() => gameService.InfoPanelOkBtnClick(gameView));

            gameInputService.NumberPressed.AddListener(number => gameInputService.EnterNumber(gameView, number));
            gameInputService.DeletePressed.AddListener(() => gameInputService.Delete(gameView));
            gameInputService.SubmitPressed.AddListener(() => gameService.ControlBtnClick(gameView, levelService));

            levelService.EarnScore.AddListener(() => gameFeedbackService.ShowEarnScore(gameView, levelService));
            levelService.EarnTime.AddListener(() => gameFeedbackService.ShowEarnTime(gameView, levelService));
            levelService.LooseLife.AddListener(() => gameFeedbackService.ShowLoseLife(gameView, levelService));
            levelService.LooseTime.AddListener(() => gameFeedbackService.ShowLoseTime(gameView, levelService));
            levelService.EarnLife.AddListener(() => gameFeedbackService.ShowEarnLife(gameView, levelService));

            gameService.GameOver.AddListener(() => gameUIService.ShowGameOverPanel(gameView));
            gameService.GameOver.AddListener(() => gameService.StopGameLoopAudio(audioView));
            gameService.GameOver.AddListener(() => audioService.StopTimeTickAudio());
            gameService.GameOver.AddListener(() => gameService.PlayGameOverAudio());

            gameService.GameCompleted.AddListener(() => gameUIService.ShowGameCompletedPanel(gameView));
            gameService.GameCompleted.AddListener(() => gameService.StopGameLoopAudio(audioView));
            gameService.GameCompleted.AddListener(() => audioService.StopTimeTickAudio());
            gameService.GameCompleted.AddListener(() => gameService.PlayGameCompletedAudio());

            gameFeedbackService.CorrectAnswerCompleted.AddListener(() => audioService.PlayCorrectAnswerAudio());
            gameFeedbackService.CorrectAnswerCompleted.AddListener(() => gameService.PrepareScreenForNextQuestion(gameView, levelService));

            gameFeedbackService.WrongAnswerCompleted.AddListener(() => audioService.PlayWrongAnswerAudio());
            gameFeedbackService.WrongAnswerCompleted.AddListener(() => gameService.PrepareScreenForNextQuestion(gameView, levelService));
            gameFeedbackService.FeedbackCompleted.AddListener(() => gameService.ContinueTheGame());
        }
    }
}
