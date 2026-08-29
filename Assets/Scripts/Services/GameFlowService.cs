using UnityEngine;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public sealed class GameFlowService
    {
        private readonly GameSessionService gameSessionService;
        private readonly QuestionService questionService;
        private readonly ScoreService scoreService;
        private readonly SceneNavigationService sceneNavigationService;

        private bool isMainMenuRequested;

        public UnityEvent GameOver { get; } = new UnityEvent();
        public UnityEvent GameCompleted { get; } = new UnityEvent();

        public GameFlowService(
            GameSessionService gameSessionService,
            QuestionService questionService,
            ScoreService scoreService,
            SceneNavigationService sceneNavigationService)
        {
            this.gameSessionService = gameSessionService;
            this.questionService = questionService;
            this.scoreService = scoreService;
            this.sceneNavigationService = sceneNavigationService;
        }

        public void CheckGameOver()
        {
            if (!gameSessionService.ShouldGameOver || gameSessionService.IsGameOver)
            {
                return;
            }

            gameSessionService.MarkGameOver();
            GameOver.Invoke();
        }

        public void PauseForAnswer()
        {
            gameSessionService.Pause();
        }

        public void ResumeGame()
        {
            gameSessionService.Resume();
        }

        public bool CompleteIfLastQuestion(LevelService levelService)
        {
            if (!questionService.IsLastQuestion(levelService))
            {
                return false;
            }

            GameCompleted.Invoke();
            gameSessionService.MarkCompleted();
            return true;
        }

        public bool CanPrepareNextQuestion(LevelService levelService)
        {
            return !questionService.IsLastQuestion(levelService);
        }

        public void PlayAgain()
        {
            sceneNavigationService.Load(SceneNames.Game);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void RequestMainMenu()
        {
            isMainMenuRequested = true;
        }

        public bool ShouldShowScoreInfoPanel()
        {
            return scoreService.CompareNewScoreWithScoresInTheList(gameSessionService.TotalScore);
        }

        public void AcceptScoreSave()
        {
            scoreService.IsNewScoreAdded = true;
        }

        public bool TryGoToMainMenu()
        {
            if (!isMainMenuRequested)
            {
                return false;
            }

            Time.timeScale = 1.0f;
            isMainMenuRequested = false;
            sceneNavigationService.Load(SceneNames.MainMenu);
            return true;
        }
    }
}
