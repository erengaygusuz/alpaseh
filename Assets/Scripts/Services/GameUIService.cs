using FTRGames.Alpaseh.Views;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public sealed class GameUIService
    {
        private readonly GameSessionService gameSessionService;
        private readonly QuestionService questionService;

        public GameUIService(
            GameSessionService gameSessionService,
            QuestionService questionService)
        {
            this.gameSessionService = gameSessionService;
            this.questionService = questionService;
        }

        public void Initialize(GameView gameView, LevelService levelService)
        {
            gameView.enteredNumberWordText.text = "";
            RefreshHud(gameView, levelService);
            SetActiveQuestion(gameView, levelService);
        }

        public void RefreshTime(GameView gameView)
        {
            gameView.totalTimeText.text = Mathf.Round(gameSessionService.TotalTime).ToString();
        }

        public void RefreshHud(GameView gameView, LevelService levelService)
        {
            RefreshTime(gameView);
            gameView.totalLifeText.text = gameSessionService.TotalLife.ToString();
            gameView.totalScoreText.text = gameSessionService.TotalScore.ToString();
            gameView.activeLevelText.text = (levelService.ActiveLevelIndex + 1).ToString();
        }

        public void PrepareForNextQuestion(GameView gameView, LevelService levelService)
        {
            RefreshHud(gameView, levelService);

            gameView.enteredNumberWordText.gameObject.SetActive(true);
            gameView.enteredNumberWordText.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameView.enteredNumberWordText.color = new Color32(0, 0, 0, 255);

            SetActiveQuestion(gameView, levelService);
            gameView.enteredNumberWordText.text = "";
        }

        public void ShowScoreInfoPrompt(GameView gameView)
        {
            gameView.infoPanel.SetActive(true);
            gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
        }

        public void ShowScoreSaveConfirmation(GameView gameView)
        {
            gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(false);
            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void HideInfoPanel(GameView gameView)
        {
            gameView.infoPanel.SetActive(false);
        }

        public void ResetAndHideInfoPanel(GameView gameView)
        {
            gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
            gameView.infoPanel.SetActive(false);
        }

        public void ShowGameOverPanel(GameView gameView)
        {
            gameView.gameOverPanel.SetActive(true);
        }

        public void ShowGameCompletedPanel(GameView gameView)
        {
            gameView.gameOverPanel.SetActive(true);
        }

        private void SetActiveQuestion(GameView gameView, LevelService levelService)
        {
            gameView.questionText.text = questionService.GetActiveQuestion(levelService);
        }
    }
}
