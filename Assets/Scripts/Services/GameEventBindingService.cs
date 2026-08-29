using FTRGames.Alpaseh.Views;

namespace FTRGames.Alpaseh.Services
{
    public sealed class GameEventBindingService
    {
        private const int NumberButtonCount = 10;

        private readonly GameService gameService;
        private readonly GameInputService gameInputService;
        private readonly GameUIService gameUIService;
        private readonly GameFeedbackService gameFeedbackService;
        private readonly AudioService audioService;

        public GameEventBindingService(
            GameService gameService,
            GameInputService gameInputService,
            GameUIService gameUIService,
            GameFeedbackService gameFeedbackService,
            AudioService audioService)
        {
            this.gameService = gameService;
            this.gameInputService = gameInputService;
            this.gameUIService = gameUIService;
            this.gameFeedbackService = gameFeedbackService;
            this.audioService = audioService;
        }

        public void Bind(GameView gameView, AudioView audioView, LevelService levelService)
        {
            BindNumberButtons(gameView);
            BindGameButtons(gameView, levelService);
            BindKeyboardInput(gameView, levelService);
            BindLevelFeedback(gameView, levelService);
            BindGameFlow(gameView, audioView);
            BindFeedbackCompletion(gameView, levelService);
        }

        private void BindNumberButtons(GameView gameView)
        {
            for (int buttonIndex = 0; buttonIndex < NumberButtonCount; buttonIndex++)
            {
                int capturedButtonIndex = buttonIndex;
                gameView.numberButtons[capturedButtonIndex].onClick.AddListener(
                    () => gameInputService.EnterNumber(gameView, capturedButtonIndex));
            }
        }

        private void BindGameButtons(GameView gameView, LevelService levelService)
        {
            gameView.checkButton.onClick.AddListener(() => gameService.ControlBtnClick(gameView, levelService));
            gameView.deleteButton.onClick.AddListener(() => gameInputService.Delete(gameView));
            gameView.mainMenuButton.onClick.AddListener(() => gameService.GoToMainMenuBtnClick(gameView));
            gameView.gameOverPanelPlayAgainButton.onClick.AddListener(() => gameService.PlayAgainBtnClick());
            gameView.gameOverPanelExitButton.onClick.AddListener(() => gameService.ExitGameBtnClick());
            gameView.gameOverPanelMainMenuButton.onClick.AddListener(() => gameService.GoToMainMenuBtnClick(gameView));
            gameView.infoPanelYesButton.onClick.AddListener(() => gameService.InfoPanelYesBtnClick(gameView));
            gameView.infoPanelNoButton.onClick.AddListener(() => gameService.InfoPanelNoBtnClick(gameView));
            gameView.infoPanelOkButton.onClick.AddListener(() => gameService.InfoPanelOkBtnClick(gameView));
        }

        private void BindKeyboardInput(GameView gameView, LevelService levelService)
        {
            gameInputService.NumberPressed.AddListener(number => gameInputService.EnterNumber(gameView, number));
            gameInputService.DeletePressed.AddListener(() => gameInputService.Delete(gameView));
            gameInputService.SubmitPressed.AddListener(() => gameService.ControlBtnClick(gameView, levelService));
        }

        private void BindLevelFeedback(GameView gameView, LevelService levelService)
        {
            levelService.EarnScore.AddListener(() => gameFeedbackService.ShowEarnScore(gameView, levelService));
            levelService.EarnTime.AddListener(() => gameFeedbackService.ShowEarnTime(gameView, levelService));
            levelService.LoseLife.AddListener(() => gameFeedbackService.ShowLoseLife(gameView, levelService));
            levelService.LoseTime.AddListener(() => gameFeedbackService.ShowLoseTime(gameView, levelService));
            levelService.EarnLife.AddListener(() => gameFeedbackService.ShowEarnLife(gameView, levelService));
        }

        private void BindGameFlow(GameView gameView, AudioView audioView)
        {
            gameService.GameOver.AddListener(() => gameUIService.ShowGameOverPanel(gameView));
            gameService.GameOver.AddListener(() => audioService.StopAudio(audioView.loopAudioSource));
            gameService.GameOver.AddListener(() => audioService.StopTimeTickAudio());
            gameService.GameOver.AddListener(() => audioService.PlayGameOverAudio());

            gameService.GameCompleted.AddListener(() => gameUIService.ShowGameCompletedPanel(gameView));
            gameService.GameCompleted.AddListener(() => audioService.StopAudio(audioView.loopAudioSource));
            gameService.GameCompleted.AddListener(() => audioService.StopTimeTickAudio());
            gameService.GameCompleted.AddListener(() => audioService.PlayGameCompletedAudio());
        }

        private void BindFeedbackCompletion(GameView gameView, LevelService levelService)
        {
            gameFeedbackService.CorrectAnswerCompleted.AddListener(() => audioService.PlayCorrectAnswerAudio());
            gameFeedbackService.CorrectAnswerCompleted.AddListener(() => gameService.PrepareScreenForNextQuestion(gameView, levelService));

            gameFeedbackService.WrongAnswerCompleted.AddListener(() => audioService.PlayWrongAnswerAudio());
            gameFeedbackService.WrongAnswerCompleted.AddListener(() => gameService.PrepareScreenForNextQuestion(gameView, levelService));
            gameFeedbackService.FeedbackCompleted.AddListener(() => gameService.ContinueTheGame());
        }
    }
}
