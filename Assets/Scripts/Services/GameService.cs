using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public class GameService
    {
        private readonly AudioService audioService;
        private readonly GameSessionService gameSessionService;
        private readonly GameTimerService gameTimerService;
        private readonly AnswerService answerService;
        private readonly GameFlowService gameFlowService;
        private readonly GameUIService gameUIService;
        private readonly GameFeedbackService gameFeedbackService;

        public UnityEvent GameOver => gameFlowService.GameOver;
        public UnityEvent GameCompleted => gameFlowService.GameCompleted;

        public GameService(
            AudioService audioService,
            GameSessionService gameSessionService,
            GameTimerService gameTimerService,
            AnswerService answerService,
            GameFlowService gameFlowService,
            GameUIService gameUIService,
            GameFeedbackService gameFeedbackService)
        {
            this.audioService = audioService;
            this.gameSessionService = gameSessionService;
            this.gameTimerService = gameTimerService;
            this.answerService = answerService;
            this.gameFlowService = gameFlowService;
            this.gameUIService = gameUIService;
            this.gameFeedbackService = gameFeedbackService;
        }

        public void Initialization(AudioView audioView, LevelService levelService, GameView gameView)
        {
            gameSessionService.Initialize();
            gameTimerService.Initialize();
            gameUIService.Initialize(gameView, levelService);
            PlayAmbienceSound(audioView);
        }

        public void GameCheck(GameView gameView)
        {
            gameTimerService.Tick(Time.deltaTime);
            gameUIService.RefreshTime(gameView);
            gameFlowService.CheckGameOver();
        }

        public void ControlBtnClick(GameView gameView, LevelService levelService)
        {
            bool isCorrectAnswer = answerService.CheckAnswer(
                levelService,
                gameView.enteredNumberWordText.text,
                gameView.questionText.text);

            gameFlowService.PauseForAnswer();
            audioService.StopTimeTickAudio();
            gameFeedbackService.PlayAnswerFeedback(gameView, isCorrectAnswer);
            gameFlowService.CompleteIfLastQuestion(levelService);
        }

        public void PrepareScreenForNextQuestion(GameView gameView, LevelService levelService)
        {
            if (!gameFlowService.CanPrepareNextQuestion(levelService))
            {
                return;
            }

            levelService.CalculateTimeScoreLifeAmount(gameSessionService);
            levelService.CalculateActiveLevelAndQuestionIndex(gameSessionService);
            gameUIService.PrepareForNextQuestion(gameView, levelService);
        }

        public void ContinueTheGame()
        {
            gameFlowService.ResumeGame();
        }

        public void PlayAgainBtnClick()
        {
            gameFlowService.PlayAgain();
        }

        public void ExitGameBtnClick()
        {
            gameFlowService.ExitGame();
        }

        public void GoToMainMenuBtnClick(GameView gameView)
        {
            gameFlowService.RequestMainMenu();
            ShowInfoPanelUI(gameView);
        }

        public void InfoPanelYesBtnClick(GameView gameView)
        {
            gameFlowService.AcceptScoreSave();
            gameUIService.ShowScoreSaveConfirmation(gameView);
        }

        public void InfoPanelNoBtnClick(GameView gameView)
        {
            gameUIService.HideInfoPanel(gameView);
            gameFlowService.TryGoToMainMenu();
        }

        public void InfoPanelOkBtnClick(GameView gameView)
        {
            gameUIService.ResetAndHideInfoPanel(gameView);
            gameFlowService.TryGoToMainMenu();
        }

        public void StopGameLoopAudio(AudioView audioView)
        {
            audioService.StopAudio(audioView.loopAudioSource);
        }

        public void PlayGameOverAudio()
        {
            audioService.PlayGameOverAudio();
        }

        public void PlayGameCompletedAudio()
        {
            audioService.PlayGameCompletedAudio();
        }

        private void PlayAmbienceSound(AudioView audioView)
        {
            audioService.StopAudio(audioView.loopAudioSource);
            audioService.PlayGameSceneAudio();
        }

        private void ShowInfoPanelUI(GameView gameView)
        {
            if (gameFlowService.ShouldShowScoreInfoPanel())
            {
                Time.timeScale = 0.0f;
                gameUIService.ShowScoreInfoPrompt(gameView);
                return;
            }

            gameFlowService.TryGoToMainMenu();
        }
    }
}
