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
        private readonly ScoreService scoreService;
        private readonly LevelService levelService;
        private readonly WordNumberConverterService wordNumberConverterService;
        private readonly WordParserService wordParserService;
        private readonly TweenService tweenService;
        private readonly AudioService audioService;

        public GamePresenter(
            GameService gameService,
            GameInputService gameInputService,
            ScoreService scoreService,
            GameView gameView,
            AudioView audioView,
            LevelService levelService,
            WordParserService wordParserService,
            WordNumberConverterService wordNumberConverterService,
            TweenService tweenService,
            AudioService audioService)
        {
            this.gameService = gameService;
            this.gameInputService = gameInputService;
            this.scoreService = scoreService;
            this.gameView = gameView;
            this.audioView = audioView;
            this.levelService = levelService;
            this.wordNumberConverterService = wordNumberConverterService;
            this.wordParserService = wordParserService;
            this.audioService = audioService;
            this.tweenService = tweenService;
        }

        void IStartable.Start()
        {
            scoreService.Initialization();
            wordParserService.Initialization();
            wordNumberConverterService.Initialization();
            levelService.Initialization();
            tweenService.Initialization();

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

            gameView.checkButton.onClick.AddListener(() => gameService.ControlBtnClick(gameView, levelService, wordNumberConverterService));
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
            gameInputService.SubmitPressed.AddListener(() => gameService.ControlBtnClick(gameView, levelService, wordNumberConverterService));

            levelService.EarnScore.AddListener(() => gameService.EarnScoreTextEffect(gameView, levelService));
            levelService.EarnTime.AddListener(() => gameService.EarnTimeTextEffect(gameView, levelService));
            levelService.LooseLife.AddListener(() => gameService.LooseLifeTextEffect(gameView, levelService));
            levelService.LooseTime.AddListener(() => gameService.LooseTimeTextEffect(gameView, levelService));
            levelService.EarnLife.AddListener(() => gameService.EarnLifeTextEffect(gameView, levelService));

            gameService.GameOver.AddListener(() => gameService.ShowGameOverPanel(gameView));
            gameService.GameOver.AddListener(() => gameService.StopGameLoopAudio(audioView));
            gameService.GameOver.AddListener(() => audioService.StopTimeTickAudio());
            gameService.GameOver.AddListener(() => gameService.PlayGameOverAudio());

            gameService.GameCompleted.AddListener(() => gameService.ShowGameCompletedPanel(gameView));
            gameService.GameCompleted.AddListener(() => gameService.StopGameLoopAudio(audioView));
            gameService.GameCompleted.AddListener(() => audioService.StopTimeTickAudio());
            gameService.GameCompleted.AddListener(() => gameService.PlayGameCompletedAudio());

            tweenService.playCorrectAnswerAnimEvent.AddListener(() => audioService.PlayCorrectAnswerAudio());
            tweenService.playCorrectAnswerAnimEvent.AddListener(() => gameService.PrepareScreenForNextQuestion(gameView, levelService));

            tweenService.playWrongAnswerAnimEvent.AddListener(() => audioService.PlayWrongAnswerAudio());
            tweenService.playWrongAnswerAnimEvent.AddListener(() => gameService.PrepareScreenForNextQuestion(gameView, levelService));
            tweenService.tweenTextEvent.AddListener(() => gameService.ContinueTheGame());
        }
    }
}
