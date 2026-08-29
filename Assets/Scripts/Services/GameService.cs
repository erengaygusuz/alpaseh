using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public class GameService
    {
        private readonly AudioService audioService;
        private readonly TweenService tweenService;
        private readonly GameSessionService gameSessionService;
        private readonly GameTimerService gameTimerService;
        private readonly QuestionService questionService;
        private readonly AnswerService answerService;
        private readonly GameFlowService gameFlowService;

        public UnityEvent GameOver => gameFlowService.GameOver;
        public UnityEvent GameCompleted => gameFlowService.GameCompleted;

        public GameService(
            AudioService audioService,
            TweenService tweenService,
            GameSessionService gameSessionService,
            GameTimerService gameTimerService,
            QuestionService questionService,
            AnswerService answerService,
            GameFlowService gameFlowService)
        {
            this.audioService = audioService;
            this.tweenService = tweenService;
            this.gameSessionService = gameSessionService;
            this.gameTimerService = gameTimerService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.gameFlowService = gameFlowService;
        }

        public void Initialization(AudioView audioView, LevelService levelService, GameView gameView)
        {
            gameSessionService.Initialize();
            gameTimerService.Initialize();
            InitGameUI(gameView, levelService);

            gameView.questionText.text = questionService.GetActiveQuestion(levelService);
            PlayAmbienceSound(audioView);
        }

        private void PlayAmbienceSound(AudioView audioView)
        {
            audioService.StopAudio(audioView.loopAudioSource);
            audioService.PlayGameSceneAudio();
        }

        private void InitGameUI(GameView gameView, LevelService levelService)
        {
            gameView.enteredNumberWordText.text = "";

            gameView.totalTimeText.text = Mathf.Round(gameSessionService.TotalTime).ToString();
            gameView.totalLifeText.text = gameSessionService.TotalLife.ToString();
            gameView.totalScoreText.text = gameSessionService.TotalScore.ToString();
            gameView.activeLevelText.text = (levelService.ActiveLevelIndex + 1).ToString();
        }

        public void GameCheck(GameView gameView)
        {
            gameTimerService.Tick(Time.deltaTime);
            gameView.totalTimeText.text = Mathf.Round(gameSessionService.TotalTime).ToString();
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

            if (isCorrectAnswer)
            {
                tweenService.PlayCorrectAnswerAnim(gameView.enteredNumberWordText, gameView.checkButton);
            }
            else
            {
                tweenService.PlayWrongAnswerAnim(gameView.enteredNumberWordText, gameView.checkButton);
            }

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

            gameView.totalTimeText.text = Mathf.Round(gameSessionService.TotalTime).ToString();
            gameView.totalLifeText.text = gameSessionService.TotalLife.ToString();
            gameView.totalScoreText.text = gameSessionService.TotalScore.ToString();
            gameView.activeLevelText.text = (levelService.ActiveLevelIndex + 1).ToString();

            gameView.enteredNumberWordText.gameObject.SetActive(true);
            gameView.enteredNumberWordText.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameView.enteredNumberWordText.color = new Color32(0, 0, 0, 255);

            gameView.questionText.text = questionService.GetActiveQuestion(levelService);
            gameView.enteredNumberWordText.text = "";
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

            gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(false);
            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void InfoPanelNoBtnClick(GameView gameView)
        {
            gameView.infoPanel.SetActive(false);
            gameFlowService.TryGoToMainMenu();
        }

        public void InfoPanelOkBtnClick(GameView gameView)
        {
            gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
            gameView.infoPanel.SetActive(false);

            gameFlowService.TryGoToMainMenu();
        }

        public void EarnScoreTextEffect(GameView gameView, LevelService levelService)
        {
            gameView.scoreIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.scoreIncDecObj,
                levelService.Levels[levelService.ActiveLevelIndex].EarnedScoreAmount.ToString(),
                Color.green,
                true,
                gameView.checkButton);
        }

        public void EarnTimeTextEffect(GameView gameView, LevelService levelService)
        {
            gameView.timeIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.timeIncDecObj,
                levelService.Levels[levelService.ActiveLevelIndex].EarnedTimeAmount.ToString(),
                Color.green,
                true,
                gameView.checkButton);
        }

        public void EarnLifeTextEffect(GameView gameView, LevelService levelService)
        {
            gameView.lifeIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.lifeIncDecObj,
                levelService.Levels[levelService.ActiveLevelIndex].LifeIncreaseAmount.ToString(),
                Color.green,
                true,
                gameView.checkButton);
        }

        public void LooseTimeTextEffect(GameView gameView, LevelService levelService)
        {
            gameView.timeIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.timeIncDecObj,
                levelService.Levels[levelService.ActiveLevelIndex].LoseTimeAmount.ToString(),
                Color.red,
                false,
                gameView.checkButton);
        }

        public void LooseLifeTextEffect(GameView gameView, LevelService levelService)
        {
            gameView.lifeIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.lifeIncDecObj,
                levelService.Levels[levelService.ActiveLevelIndex].LoseLifeAmount.ToString(),
                Color.red,
                false,
                gameView.checkButton);
        }

        public void ShowInfoPanelUI(GameView gameView)
        {
            if (gameFlowService.ShouldShowScoreInfoPanel())
            {
                Time.timeScale = 0.0f;

                gameView.infoPanel.SetActive(true);
                gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
                gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
                return;
            }

            gameFlowService.TryGoToMainMenu();
        }

        public void ShowGameOverPanel(GameView gameView)
        {
            gameView.gameOverPanel.SetActive(true);
        }

        public void StopGameLoopAudio(AudioView audioView)
        {
            audioService.StopAudio(audioView.loopAudioSource);
        }

        public void PlayGameOverAudio()
        {
            audioService.PlayGameOverAudio();
        }

        public void ShowGameCompletedPanel(GameView gameView)
        {
            gameView.gameOverPanel.SetActive(true);
        }

        public void PlayGameCompletedAudio()
        {
            audioService.PlayGameCompletedAudio();
        }
    }
}
