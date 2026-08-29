using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public class GameService
    {
        private readonly AudioService audioService;
        private readonly TweenService tweenService;
        private readonly ScoreService scoreService;
        private readonly SceneNavigationService sceneNavigationService;
        private readonly GameSessionService gameSessionService;
        private readonly GameTimerService gameTimerService;
        private readonly QuestionService questionService;
        private readonly AnswerService answerService;

        public UnityEvent GameOver { get; set; }
        public UnityEvent GameCompleted { get; set; }

        public bool IsGotoMainMenuBtnClick;

        public GameService(
            AudioService audioService,
            TweenService tweenService,
            ScoreService scoreService,
            SceneNavigationService sceneNavigationService,
            GameSessionService gameSessionService,
            GameTimerService gameTimerService,
            QuestionService questionService,
            AnswerService answerService)
        {
            this.audioService = audioService;
            this.tweenService = tweenService;
            this.scoreService = scoreService;
            this.sceneNavigationService = sceneNavigationService;
            this.gameSessionService = gameSessionService;
            this.gameTimerService = gameTimerService;
            this.questionService = questionService;
            this.answerService = answerService;
        }

        public void Initialization(AudioView audioView, LevelService levelService, GameView gameView)
        {
            GameEventsInit();
            gameSessionService.Initialize();
            gameTimerService.Initialize();
            InitGameUI(gameView, levelService);

            gameView.questionText.text = questionService.GetActiveQuestion(levelService);
            PlayAmbienceSound(audioView);
        }

        #region Sound Functions

        private void PlayAmbienceSound(AudioView audioView)
        {
            audioService.StopAudio(audioView.loopAudioSource);
            audioService.PlayGameSceneAudio();
        }

        #endregion

        private void InitGameUI(GameView gameView, LevelService levelService)
        {
            gameView.enteredNumberWordText.text = "";

            gameView.totalTimeText.text = Mathf.Round(gameSessionService.TotalTime).ToString();
            gameView.totalLifeText.text = gameSessionService.TotalLife.ToString();
            gameView.totalScoreText.text = gameSessionService.TotalScore.ToString();
            gameView.activeLevelText.text = (levelService.ActiveLevelIndex + 1).ToString();
        }

        private void GameEventsInit()
        {
            if (GameOver == null)
            {
                GameOver = new UnityEvent();
            }

            if (GameCompleted == null)
            {
                GameCompleted = new UnityEvent();
            }
        }

        #region Tick Event Functions

        public void GameCheck(GameView gameView)
        {
            gameTimerService.Tick(Time.deltaTime);

            gameView.totalTimeText.text = Mathf.Round(gameSessionService.TotalTime).ToString();

            if (gameSessionService.ShouldGameOver && !gameSessionService.IsGameOver)
            {
                gameSessionService.MarkGameOver();
                GameOver.Invoke();
            }
        }

        #endregion

        #region Event Binding Functions

        public void ControlBtnClick(GameView gameView, LevelService levelService)
        {
            bool isCorrectAnswer = answerService.CheckAnswer(
                levelService,
                gameView.enteredNumberWordText.text,
                gameView.questionText.text);

            gameSessionService.Pause();
            audioService.StopTimeTickAudio();

            if (isCorrectAnswer)
            {
                tweenService.PlayCorrectAnswerAnim(gameView.enteredNumberWordText, gameView.checkButton);
            }
            else
            {
                tweenService.PlayWrongAnswerAnim(gameView.enteredNumberWordText, gameView.checkButton);
            }

            if (questionService.IsLastQuestion(levelService))
            {
                GameCompleted.Invoke();
                gameSessionService.MarkCompleted();
            }
        }

        public void PrepareScreenForNextQuestion(GameView gameView, LevelService levelService)
        {
            if (questionService.IsLastQuestion(levelService))
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
            gameSessionService.Resume();
        }

        public void PlayAgainBtnClick()
        {
            sceneNavigationService.Load(SceneNames.Game);
        }

        public void ExitGameBtnClick()
        {
            Application.Quit();
        }

        public void GoToMainMenuBtnClick(GameView gameView)
        {
            IsGotoMainMenuBtnClick = true;
            ShowInfoPanelUI(gameView);
        }

        public void InfoPanelYesBtnClick(GameView gameView)
        {
            scoreService.IsNewScoreAdded = true;

            gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(false);
            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void InfoPanelNoBtnClick(GameView gameView)
        {
            gameView.infoPanel.SetActive(false);

            if (IsGotoMainMenuBtnClick)
            {
                Time.timeScale = 1;
                IsGotoMainMenuBtnClick = false;

                sceneNavigationService.Load(SceneNames.MainMenu);
            }
        }

        public void InfoPanelOkBtnClick(GameView gameView)
        {
            gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
            gameView.infoPanel.SetActive(false);

            if (IsGotoMainMenuBtnClick)
            {
                Time.timeScale = 1;
                IsGotoMainMenuBtnClick = false;

                sceneNavigationService.Load(SceneNames.MainMenu);
            }
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
            if (scoreService.CompareNewScoreWithScoresInTheList(gameSessionService.TotalScore))
            {
                Time.timeScale = 0;

                gameView.infoPanel.SetActive(true);
                gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
                gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (IsGotoMainMenuBtnClick)
            {
                Time.timeScale = 1;
                IsGotoMainMenuBtnClick = false;

                sceneNavigationService.Load(SceneNames.MainMenu);
            }
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

        #endregion
    }
}
