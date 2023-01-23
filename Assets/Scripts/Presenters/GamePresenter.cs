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
        private readonly ScoreService scoreService;
        private readonly LevelService levelService;
        private readonly WordNumberConverterService wordNumberConverterService;
        private readonly WordParserService wordParserService;
        private readonly TweenService tweenService;
        private readonly AudioService audioService;

        public GamePresenter(GameService gameService, ScoreService scoreService, GameView gameView, AudioView audioView, LevelService levelService,
            WordParserService wordParserService, WordNumberConverterService wordNumberConverterService, TweenService tweenService, AudioService audioService)
        {
            this.gameService = gameService;
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
        }

        public void Tick()
        {
            scoreService.UpdateScoreValues();
            gameService.GameCheck(gameView, levelService);
        }

        private void EventBinding()
        {
            gameView.numberButtons[0].onClick.AddListener(() => gameService.Number0BtnClick(gameView));
            gameView.numberButtons[1].onClick.AddListener(() => gameService.Number1BtnClick(gameView));
            gameView.numberButtons[2].onClick.AddListener(() => gameService.Number2BtnClick(gameView));
            gameView.numberButtons[3].onClick.AddListener(() => gameService.Number3BtnClick(gameView));
            gameView.numberButtons[4].onClick.AddListener(() => gameService.Number4BtnClick(gameView));
            gameView.numberButtons[5].onClick.AddListener(() => gameService.Number5BtnClick(gameView));
            gameView.numberButtons[6].onClick.AddListener(() => gameService.Number6BtnClick(gameView));
            gameView.numberButtons[7].onClick.AddListener(() => gameService.Number7BtnClick(gameView));
            gameView.numberButtons[8].onClick.AddListener(() => gameService.Number8BtnClick(gameView));
            gameView.numberButtons[9].onClick.AddListener(() => gameService.Number9BtnClick(gameView));

            gameView.checkButton.onClick.AddListener(() => gameService.ControlBtnClick(gameView, levelService, wordNumberConverterService));
            gameView.deleteButton.onClick.AddListener(() => gameService.DeleteBtnClick(gameView));
            gameView.mainMenuButton.onClick.AddListener(() => gameService.GoToMainMenuBtnClick(gameView));
            gameView.gameOverPanelPlayAgainButton.onClick.AddListener(() => gameService.PlayAgainBtnClick());
            gameView.gameOverPanelExitButton.onClick.AddListener(() => gameService.ExitGameBtnClick());
            gameView.gameOverPanelMainMenuButton.onClick.AddListener(() => gameService.GoToMainMenuBtnClick(gameView));
            gameView.infoPanelYesButton.onClick.AddListener(() => gameService.InfoPanelYesBtnClick(gameView));
            gameView.infoPanelNoButton.onClick.AddListener(() => gameService.InfoPanelNoBtnClick(gameView));
            gameView.infoPanelOkButton.onClick.AddListener(() => gameService.InfoPanelOkBtnClick(gameView));

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