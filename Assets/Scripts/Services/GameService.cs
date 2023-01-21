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
        private readonly GameView gameView;
        private readonly AudioView audioView;
        private readonly AudioService audioService;
        private readonly WordParserService wordParserService;
        private readonly LocalizationService localizationService;
        private readonly LevelService levelService;
        private readonly WordNumberConverterService wordNumberConverterService;
        private readonly TweenService tweenService;
        private readonly ScoreService scoreService;

        private float totalTime;
        private float totalLife;
        private int totalScore;

        public UnityEvent GameOver { get; set; }

        private bool isGameOver;

        private bool isWaitForTimeTick;

        public bool IsGotoMainMenuBtnClick;

        public GameService(AudioService audioService, WordParserService wordParserService, LocalizationService localizationService, LevelService levelService, 
            WordNumberConverterService wordNumberConverterService, GameView gameView, AudioView audioView, TweenService tweenService, ScoreService scoreService)
        {
            this.audioService = audioService;
            this.wordParserService = wordParserService;
            this.localizationService = localizationService;
            this.levelService = levelService;
            this.wordNumberConverterService = wordNumberConverterService;
            this.gameView = gameView;
            this.audioView = audioView;
            this.tweenService = tweenService;
            this.scoreService = scoreService;
        }

        public void Initialization()
        {
            UIEventBinding();
            wordParserService.Initialization();
            wordNumberConverterService.Initialization();
            levelService.Initialization();

            gameView.enteredNumberWordText.text = "";
            totalLife = 6;
            totalTime = 1200;
            gameView.totalTimeText.text = Mathf.Round(totalTime).ToString();
            gameView.totalLifeText.text = totalLife.ToString("F2");
            gameView.totalScoreText.text = totalScore.ToString();
            gameView.activeLevelText.text = (levelService.ActiveLevelIndex + 1).ToString();

            GameOverEventBinding();
            GetActiveQuestionText();

            audioService.StopAudio(audioView.loopAudioSource);
            audioService.PlayGameSceneAudio();
            isWaitForTimeTick = true;

            AssignTranslatedValues();
            BindEvents();
        }

        private void UIEventBinding()
        {
            gameView.numberButtons[0].onClick.AddListener(Number0BtnClick);
            gameView.numberButtons[1].onClick.AddListener(Number1BtnClick);
            gameView.numberButtons[2].onClick.AddListener(Number2BtnClick);
            gameView.numberButtons[3].onClick.AddListener(Number3BtnClick);
            gameView.numberButtons[4].onClick.AddListener(Number4BtnClick);
            gameView.numberButtons[5].onClick.AddListener(Number5BtnClick);
            gameView.numberButtons[6].onClick.AddListener(Number6BtnClick);
            gameView.numberButtons[7].onClick.AddListener(Number7BtnClick);
            gameView.numberButtons[8].onClick.AddListener(Number8BtnClick);
            gameView.numberButtons[9].onClick.AddListener(Number9BtnClick);

            gameView.checkButton.onClick.AddListener(ControlBtnClick);
            gameView.deleteButton.onClick.AddListener(DeleteBtnClick);
            gameView.mainMenuButton.onClick.AddListener(GoToMainMenuBtnClick);
            gameView.gameOverPanelPlayAgainButton.onClick.AddListener(PlayAgainBtnClick);
            gameView.gameOverPanelExitButton.onClick.AddListener(ExitGameBtnClick);
            gameView.gameOverPanelMainMenuButton.onClick.AddListener(GoToMainMenuBtnClick);
            gameView.infoPanelYesButton.onClick.AddListener(InfoPanelYesBtnClick);
            gameView.infoPanelNoButton.onClick.AddListener(InfoPanelNoBtnClick);
            gameView.infoPanelOkButton.onClick.AddListener(InfoPanelOkBtnClick);
        }

        private void BindEvents()
        {
            levelService.EarnScore.AddListener(EarnScoreTextEffect);
            levelService.EarnTime.AddListener(EarnTimeTextEffect);
            levelService.LooseLife.AddListener(LooseLifeTextEffect);
            levelService.LooseTime.AddListener(LooseTimeTextEffect);
        }

        private void GameOverEventBinding()
        {
            if (GameOver == null)
            {
                GameOver = new UnityEvent();
            }

            GameOver.AddListener(ShowInfoPanelUI);
            GameOver.AddListener(ShowGameOverPanel);
            GameOver.AddListener(StopGameLoopAudio);
            GameOver.AddListener(PlayGameOverAudio);
        }

        private void GetActiveQuestionText()
        {
            int activeQuestionIndex = levelService.Levels[levelService.ActiveLevelIndex].activeQuestionIndex;

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

        private void ClearEnteredNumberWordText()
        {
            gameView.enteredNumberWordText.text = "";
        }

        private void ControlBtnClick()
        {
            bool isCorrectAnswer = levelService.Levels[levelService.ActiveLevelIndex].CheckEnteredNumberWord(gameView.enteredNumberWordText.text, 
                wordNumberConverterService.GetNumbersFromWord(gameView.questionText.text));
        
            levelService.CalculateTimeScoreLifeAmount(ref totalTime, ref totalScore, ref totalLife);
            levelService.CalculateActiveLevelAndQuestionIndex();

            gameView.totalTimeText.text = Mathf.Round(totalTime).ToString();
            gameView.totalLifeText.text = totalLife.ToString("F2");
            gameView.totalScoreText.text = totalScore.ToString();
            gameView.activeLevelText.text = (levelService.ActiveLevelIndex + 1).ToString();

            GetActiveQuestionText();
            ClearEnteredNumberWordText();

            if (isCorrectAnswer)
            {
                audioService.PlayCorrectAnswerAudio();
            }

            else
            {
                audioService.PlayWrongAnswerAudio();
            }
        }

        private void DeleteBtnClick()
        {
            if (gameView.enteredNumberWordText.text != "")
            {
                gameView.enteredNumberWordText.text = gameView.enteredNumberWordText.text.Remove(gameView.enteredNumberWordText.text.Length - 1);
            }
        }

        private void GoToMainMenuBtnClick()
        {
            IsGotoMainMenuBtnClick = true;

            ShowInfoPanelUI();
        }

        private void Number0BtnClick()
        {
            gameView.enteredNumberWordText.text += "0";
        }

        private void Number1BtnClick()
        {
            gameView.enteredNumberWordText.text += "1";
        }

        private void Number2BtnClick()
        {
            gameView.enteredNumberWordText.text += "2";
        }

        private void Number3BtnClick()
        {
            gameView.enteredNumberWordText.text += "3";
        }

        private void Number4BtnClick()
        {
            gameView.enteredNumberWordText.text += "4";
        }

        private void Number5BtnClick()
        {
            gameView.enteredNumberWordText.text += "5";
        }

        private void Number6BtnClick()
        {
            gameView.enteredNumberWordText.text += "6";
        }

        private void Number7BtnClick()
        {
            gameView.enteredNumberWordText.text += "7";
        }

        private void Number8BtnClick()
        {
            gameView.enteredNumberWordText.text += "8";
        }

        private void Number9BtnClick()
        {
            gameView.enteredNumberWordText.text += "9";
        }

        private IEnumerator PlayTimeTickEverySecond()
        {
            isWaitForTimeTick = false;
            yield return new WaitForSeconds(1.0f);
            audioService.PlayTimeTickAudio();
            isWaitForTimeTick = true;
        }

        private void AssignTranslatedValues()
        {
            gameView.topBarTotalTimeText.text = localizationService.GetLocalizationData().Game.TopBarTotalTimeText;
            gameView.topBarTotalLifeText.text = localizationService.GetLocalizationData().Game.TopBarTotalLifeText;
            gameView.topBarActiveLevelText.text = localizationService.GetLocalizationData().Game.TopBarActiveLevelText;
            gameView.topBarTotalScoreText.text = localizationService.GetLocalizationData().Game.TopBarTotalScoreText;
            gameView.processButtonsCheckButtonText.text = localizationService.GetLocalizationData().Game.ProcessButtonsCheckButtonText;
            gameView.processButtonsDeleteButtonText.text = localizationService.GetLocalizationData().Game.ProcessButtonsDeleteButtonText;
            gameView.processButtonsMainMenuButtonText.text = localizationService.GetLocalizationData().Game.ProcessButtonsMainMenuButtonText;
            gameView.gameOverPanelInfoText.text = localizationService.GetLocalizationData().Game.GameOverPanelInfoText;
            gameView.gameOverPanelMessageBox1InfoText.text = localizationService.GetLocalizationData().Game.GameOverPanelMessageBox1InfoText;
            gameView.gameOverPanelMessageBox1YesBtnText.text = localizationService.GetLocalizationData().Game.GameOverPanelMessageBox1YesBtnText;
            gameView.gameOverPanelMessageBox1NoBtnText.text = localizationService.GetLocalizationData().Game.GameOverPanelMessageBox1NoBtnText;
            gameView.gameOverPanelMessageBox2InfoText.text = localizationService.GetLocalizationData().Game.GameOverPanelMessageBox2InfoText;
            gameView.gameOverPanelMessageBox2OkBtnText.text = localizationService.GetLocalizationData().Game.GameOverPanelMessageBox2OkBtnText;
            gameView.gameOverPanelPlayAgainButtonText.text = localizationService.GetLocalizationData().Game.GameOverPanelPlayAgainButtonText;
            gameView.gameOverPanelMainMenuButtonText.text = localizationService.GetLocalizationData().Game.GameOverPanelMainMenuButtonText;
            gameView.gameOverPanelExitGameButtonText.text = localizationService.GetLocalizationData().Game.GameOverPanelExitGameButtonText;
        }

        private void EarnScoreTextEffect()
        {
            gameView.scoreIncDecObj.SetActive(true);
            tweenService.TweenText(gameView.scoreIncDecObj, levelService.Levels[levelService.ActiveLevelIndex].GetEarnedScoreAmount.ToString(), Color.green, true);
        }

        private void EarnTimeTextEffect()
        {
            gameView.timeIncDecObj.SetActive(true);
            tweenService.TweenText(gameView.timeIncDecObj, levelService.Levels[levelService.ActiveLevelIndex].GetEarnedTimeAmount.ToString(), Color.green, true);
        }

        private void LooseTimeTextEffect()
        {
            gameView.timeIncDecObj.SetActive(true);
            tweenService.TweenText(gameView.timeIncDecObj, levelService.Levels[levelService.ActiveLevelIndex].GetLoseTimeAmount.ToString(), Color.red, false);
        }

        private void LooseLifeTextEffect()
        {
            gameView.lifeIncDecObj.SetActive(true);
            tweenService.TweenText(gameView.lifeIncDecObj, levelService.Levels[levelService.ActiveLevelIndex].GetLoseLifeAmount.ToString(), Color.red, false);
        }

        public void GameCheck()
        {
            if (!isGameOver)
            {
                totalTime -= Time.deltaTime;

                if (isWaitForTimeTick)
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

        private void ShowGameOverPanel()
        {
            gameView.gameOverPanel.SetActive(true);
            gameView.gameOverPanel.transform.GetChild(1).GetComponent<Text>().text = "Game Over";
        }

        private void ShowInfoPanelUI()
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

        private void InfoPanelYesBtnClick()
        {
            scoreService.IsNewScoreAdded = true;

            gameView.infoPanel.transform.GetChild(0).gameObject.SetActive(false);
            gameView.infoPanel.transform.GetChild(1).gameObject.SetActive(true);
        }

        private void InfoPanelNoBtnClick()
        {
            gameView.infoPanel.SetActive(false);

            if (IsGotoMainMenuBtnClick)
            {
                Time.timeScale = 1;
                IsGotoMainMenuBtnClick = false;

                SceneManager.LoadScene("MainMenu");
            }
        }

        private void InfoPanelOkBtnClick()
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

        private void StopGameLoopAudio()
        {
            audioService.StopAudio(audioView.loopAudioSource);
        }

        private void PlayGameOverAudio()
        {
            audioService.PlayGameOverAudio();
        }

        private void PlayAgainBtnClick()
        {
            SceneManager.LoadScene("Game");
        }

        private void ExitGameBtnClick()
        {
            Application.Quit();
        }
    }
}

