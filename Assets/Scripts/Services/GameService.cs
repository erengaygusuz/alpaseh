using FTRGames.Alpaseh.Views;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Services
{
    public class GameService 
    {
        private readonly AudioService audioService;
        private readonly TweenService tweenService;
        private readonly ScoreService scoreService;

        private float totalTime;
        private float totalLife;
        private int totalScore;

        public UnityEvent GameOver { get; set; }

        private bool isGameOver;

        private bool isWaitedForTimeTick;

        public bool IsGotoMainMenuBtnClick;

        public GameService(AudioService audioService, TweenService tweenService, ScoreService scoreService)
        {
            this.audioService = audioService;
            this.tweenService = tweenService;
            this.scoreService = scoreService;
        }

        public void Initialization(AudioView audioView, LevelService levelService, GameView gameView)
        {
            GameOverEventInit();
            InitLifeAndTime();
            InitGameUI(gameView, levelService);

            GetActiveQuestionText(levelService, gameView);

            PlayAmbienceSound(audioView);
            PlayTimeTickSound();
        }

        #region Sound Functions

        private void PlayTimeTickSound()
        {
            isWaitedForTimeTick = true;
        }

        private void PlayAmbienceSound(AudioView audioView)
        {
            audioService.StopAudio(audioView.loopAudioSource);
            audioService.PlayGameSceneAudio();
        }

        #endregion

        private void InitGameUI(GameView gameView, LevelService levelService)
        {
            gameView.enteredNumberWordText.text = "";

            gameView.totalTimeText.text = Mathf.Round(totalTime).ToString();
            gameView.totalLifeText.text = totalLife.ToString("F2");
            gameView.totalScoreText.text = totalScore.ToString();
            gameView.activeLevelText.text = (levelService.ActiveLevelIndex + 1).ToString();
        }

        private void InitLifeAndTime()
        {
            totalLife = 6;
            totalTime = 1200;
        }

        private void GameOverEventInit()
        {
            if (GameOver == null)
            {
                GameOver = new UnityEvent();
            }
        }

        private void GetActiveQuestionText(LevelService levelService, GameView gameView)
        {
            int activeQuestionIndex = levelService.Levels[levelService.ActiveLevelIndex].ActiveQuestionIndex;

            string question = levelService.Levels[levelService.ActiveLevelIndex].WordList[activeQuestionIndex].ToString();

            gameView.questionText.text = TurkishCharacterToEnglish(question);
        }

        private string TurkishCharacterToEnglish(string text)
        {
            char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

            for (int i = 0; i < turkishChars.Length; i++)
            {
                text = text.Replace(turkishChars[i], englishChars[i]);
            }

            return text;
        }

        private void ClearEnteredNumberWordText(GameView gameView)
        {
            gameView.enteredNumberWordText.text = "";
        }

        #region Tick Event Functions

        public void GameCheck(GameView gameView)
        {
            if (!isGameOver)
            {
                totalTime -= Time.deltaTime;

                if (isWaitedForTimeTick)
                {
                    MonoBehaviour mono = GameObject.FindObjectOfType<MonoBehaviour>();
                    mono.StartCoroutine(PlayTimeTickEverySecond());
                }
            }

            gameView.totalTimeText.text = Mathf.Round(totalTime).ToString();

            if ((totalLife <= 0 || totalTime <= 0) && !isGameOver)
            {
                totalLife = 0;
                totalTime = 0;
                isGameOver = true;

                GameOver.Invoke();
            }
        }

        private IEnumerator PlayTimeTickEverySecond()
        {
            isWaitedForTimeTick = false;
            yield return new WaitForSeconds(1.0f);
            audioService.PlayTimeTickAudio();
            isWaitedForTimeTick = true;
        }

        #endregion

        #region Event Binding Functions

        public void Number0BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "0";
        }

        public void Number1BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "1";
        }

        public void Number2BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "2";
        }

        public void Number3BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "3";
        }

        public void Number4BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "4";
        }

        public void Number5BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "5";
        }

        public void Number6BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "6";
        }

        public void Number7BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "7";
        }

        public void Number8BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "8";
        }

        public void Number9BtnClick(GameView gameView)
        {
            gameView.enteredNumberWordText.text += "9";
        }

        public void ControlBtnClick(GameView gameView, LevelService levelService, WordNumberConverterService wordNumberConverterService)
        {
            bool isCorrectAnswer = levelService.Levels[levelService.ActiveLevelIndex].CheckEnteredNumberWord(gameView.enteredNumberWordText.text,
                wordNumberConverterService.GetNumbersFromWord(gameView.questionText.text));

            levelService.CalculateTimeScoreLifeAmount(ref totalTime, ref totalScore, ref totalLife);
            levelService.CalculateActiveLevelAndQuestionIndex(ref totalLife);

            gameView.totalTimeText.text = Mathf.Round(totalTime).ToString();
            gameView.totalLifeText.text = totalLife.ToString("F2");
            gameView.totalScoreText.text = totalScore.ToString();
            gameView.activeLevelText.text = (levelService.ActiveLevelIndex + 1).ToString();

            GetActiveQuestionText(levelService, gameView);
            ClearEnteredNumberWordText(gameView);

            if (isCorrectAnswer)
            {
                audioService.PlayCorrectAnswerAudio();
            }

            else
            {
                audioService.PlayWrongAnswerAudio();
            }
        }

        public void DeleteBtnClick(GameView gameView)
        {
            if (gameView.enteredNumberWordText.text != "")
            {
                gameView.enteredNumberWordText.text = gameView.enteredNumberWordText.text.Remove(gameView.enteredNumberWordText.text.Length - 1);
            }
        }

        public void PlayAgainBtnClick()
        {
            SceneManager.LoadScene("Game");
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

                SceneManager.LoadScene("MainMenu");
            }
        }

        public void InfoPanelOkBtnClick(GameView gameView)
        {
            gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);

            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
            gameView.infoPanel.SetActive(false);

            if (IsGotoMainMenuBtnClick)
            {
                Time.timeScale = 1;
                IsGotoMainMenuBtnClick = false;

                SceneManager.LoadScene("MainMenu");
            }
        }

        public void EarnScoreTextEffect(GameView gameView, LevelService levelService)
        {
            gameView.scoreIncDecObj.SetActive(true);
            tweenService.TweenText(gameView.scoreIncDecObj, levelService.Levels[levelService.ActiveLevelIndex].EarnedScoreAmount.ToString(), Color.green, true);
        }

        public void EarnTimeTextEffect(GameView gameView, LevelService levelService)
        {
            gameView.timeIncDecObj.SetActive(true);
            tweenService.TweenText(gameView.timeIncDecObj, levelService.Levels[levelService.ActiveLevelIndex].EarnedTimeAmount.ToString(), Color.green, true);
        }

        public void LooseTimeTextEffect(GameView gameView, LevelService levelService)
        {
            gameView.timeIncDecObj.SetActive(true);
            tweenService.TweenText(gameView.timeIncDecObj, levelService.Levels[levelService.ActiveLevelIndex].LoseTimeAmount.ToString(), Color.red, false);
        }

        public void LooseLifeTextEffect(GameView gameView, LevelService levelService)
        {
            gameView.lifeIncDecObj.SetActive(true);
            tweenService.TweenText(gameView.lifeIncDecObj, levelService.Levels[levelService.ActiveLevelIndex].LoseLifeAmount.ToString(), Color.red, false);
        }

        public void ShowInfoPanelUI(GameView gameView)
        {
            if (scoreService.CompareNewScoreWithScoresInTheList(totalScore))
            {
                Time.timeScale = 0;

                gameView.infoPanel.SetActive(true);
                gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
                gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
            }

            else
            {
                if (IsGotoMainMenuBtnClick)
                {
                    Time.timeScale = 1;
                    IsGotoMainMenuBtnClick = false;

                    SceneManager.LoadScene("MainMenu");
                }
            }
        }

        public void ShowGameOverPanel(GameView gameView)
        {
            gameView.gameOverPanel.SetActive(true);
            gameView.gameOverPanel.transform.GetChild(1).GetComponent<Text>().text = "Game Over";
        }

        public void StopGameLoopAudio(AudioView audioView)
        {
            audioService.StopAudio(audioView.loopAudioSource);
        }

        public void PlayGameOverAudio()
        {
            audioService.PlayGameOverAudio();
        }

        #endregion
    }
}

