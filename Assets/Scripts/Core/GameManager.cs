using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("LanguageElements")]
        [SerializeField]
        private Text topBarTotalTimeText;
        [SerializeField]
        private Text topBarTotalLifeText;
        [SerializeField]
        private Text topBarActiveLevelText;
        [SerializeField]
        private Text topBarTotalScoreText;
        [SerializeField]
        private Text processButtonsCheckButtonText;
        [SerializeField]
        private Text processButtonsDeleteButtonText;
        [SerializeField]
        private Text processButtonsMainMenuButtonText;
        [SerializeField]
        private Text gameOverPanelInfoText;
        [SerializeField]
        private Text gameOverPanelMessageBox1InfoText;
        [SerializeField]
        private Text gameOverPanelMessageBox1YesBtnText;
        [SerializeField]
        private Text gameOverPanelMessageBox1NoBtnText;
        [SerializeField]
        private Text gameOverPanelMessageBox2InfoText;
        [SerializeField]
        private Text gameOverPanelMessageBox2OkBtnText;
        [SerializeField]
        private Text gameOverPanelPlayAgainButtonText;
        [SerializeField]
        private Text gameOverPanelMainMenuButtonText;
        [SerializeField]
        private Text gameOverPanelExitGameButtonText;

        [SerializeField]
        private Text questionText;
        [SerializeField]
        private Text enteredNumberWordText;
        [SerializeField]
        private Text totalTimeText;
        [SerializeField]
        private Text totalLifeText;
        [SerializeField]
        private Text totalScoreText;
        [SerializeField]
        private Text activeLevelText;

        private float totalTime;
        private float totalLife;
        private static int totalScore;

        [SerializeField]
        private GameObject lifeIncDecObj;
        [SerializeField]
        private GameObject scoreIncDecObj;
        [SerializeField]
        private GameObject timeIncDecObj;

        public static UnityEvent GameOver { get; set; }

        private bool isGameOver;

        private bool isWaitForTimeTick;

        public static bool IsGotoMainMenuBtnClick;

        private void Start()
        {
            WordParser.Initialization();
            WordNumberConverter.Initialization();
            LevelController.Initialization();
            Initialization();
        }

        private void Update()
        {
            if (!isGameOver)
            {
                totalTime -= Time.deltaTime;

                if (isWaitForTimeTick)
                {
                    StartCoroutine(PlayTimeTickEverySecond());
                }
            }

            totalTimeText.text = Mathf.Round(totalTime).ToString();

            if ((totalLife <= 0 || totalTime <= 0) && !isGameOver)
            {
                totalLife = 0;
                totalTime = 0;
                isGameOver = true;

                GameOver.Invoke();
            }
        }

        private void Initialization()
        {
            enteredNumberWordText.text = "";
            totalLife = 6;
            totalTime = 1200;
            totalTimeText.text = Mathf.Round(totalTime).ToString();
            totalLifeText.text = totalLife.ToString("F2");
            totalScoreText.text = totalScore.ToString();
            activeLevelText.text = (LevelController.ActiveLevelIndex + 1).ToString();

            GameOverEventBinding();
            GetActiveQuestionText();

            AudioManager.StopAudio(AudioManager.loopAudioSource);
            AudioManager.PlayGameSceneAudio();
            isWaitForTimeTick = true;

            AssignTranslatedValues();
            BindEvents();
        }

        private void BindEvents()
        {
            LevelController.EarnScore.AddListener(EarnScoreTextEffect);
            LevelController.EarnTime.AddListener(EarnTimeTextEffect);
            LevelController.LooseLife.AddListener(LooseLifeTextEffect);
            LevelController.LooseTime.AddListener(LooseTimeTextEffect);
        }

        private void GameOverEventBinding()
        {
            if (GameOver == null)
            {
                GameOver = new UnityEvent();
            }

            GameOver.AddListener(GameFinishController.ShowInfoPanelUI);
            GameOver.AddListener(GameFinishController.ShowGameOverPanel);
            GameOver.AddListener(GameFinishController.StopGameLoopAudio);
            GameOver.AddListener(GameFinishController.PlayGameOverAudio);
        }

        private void GetActiveQuestionText()
        {
            int activeQuestionIndex = LevelController.Levels[LevelController.ActiveLevelIndex].activeQuestionIndex;

            string question = LevelController.Levels[LevelController.ActiveLevelIndex].WordList[activeQuestionIndex].ToString();

            questionText.text = TurkishCharacterToEnglish(question);
        }

        public string TurkishCharacterToEnglish(string text)
        {
            char[] turkishChars = { 'ý', 'ð', 'Ý', 'Ð', 'ç', 'Ç', 'þ', 'Þ', 'ö', 'Ö', 'ü', 'Ü' };
            char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

            for (int i = 0; i < turkishChars.Length; i++)
            {
                text = text.Replace(turkishChars[i], englishChars[i]);
            }

            return text;
        }

        public void ClearEnteredNumberWordText()
        {
            enteredNumberWordText.text = "";
        }

        public void ControlBtnClick()
        {
            bool isCorrectAnswer = LevelController.Levels[LevelController.ActiveLevelIndex].CheckEnteredNumberWord(enteredNumberWordText.text, WordNumberConverter.GetNumbersFromWord(questionText.text));
            LevelController.Levels[LevelController.ActiveLevelIndex].CalculateTimeScoreLifeAmount(ref totalTime, ref totalScore, ref totalLife);
            LevelController.Levels[LevelController.ActiveLevelIndex].CalculateActiveLevelAndQuestionIndex();

            totalTimeText.text = Mathf.Round(totalTime).ToString();
            totalLifeText.text = totalLife.ToString("F2");
            totalScoreText.text = totalScore.ToString();
            activeLevelText.text = (LevelController.ActiveLevelIndex + 1).ToString();

            GetActiveQuestionText();
            ClearEnteredNumberWordText();

            if (isCorrectAnswer)
            {
                AudioManager.PlayCorrectAnswerAudio();
            }

            else
            {
                AudioManager.PlayWrongAnswerAudio();
            }
        }

        public void DeleteBtnClick()
        {
            if (enteredNumberWordText.text != "")
            {
                enteredNumberWordText.text = enteredNumberWordText.text.Remove(enteredNumberWordText.text.Length - 1);
            }
        }

        public void GoToMainMenuBtnClick()
        {
            IsGotoMainMenuBtnClick = true;

            GameFinishController.ShowInfoPanelUI();
        }

        public void NumberBtnClick(Text text)
        {
            enteredNumberWordText.text += text.text;
        }

        private IEnumerator PlayTimeTickEverySecond()
        {
            isWaitForTimeTick = false;
            yield return new WaitForSeconds(1.0f);
            AudioManager.PlayTimeTickAudio();
            isWaitForTimeTick = true;
        }

        private void AssignTranslatedValues()
        {
            topBarTotalTimeText.text = LocalizationManager.GetLocalizationData().Game.TopBarTotalTimeText;
            topBarTotalLifeText.text = LocalizationManager.GetLocalizationData().Game.TopBarTotalLifeText;
            topBarActiveLevelText.text = LocalizationManager.GetLocalizationData().Game.TopBarActiveLevelText;
            topBarTotalScoreText.text = LocalizationManager.GetLocalizationData().Game.TopBarTotalScoreText;
            processButtonsCheckButtonText.text = LocalizationManager.GetLocalizationData().Game.ProcessButtonsCheckButtonText;
            processButtonsDeleteButtonText.text = LocalizationManager.GetLocalizationData().Game.ProcessButtonsDeleteButtonText;
            processButtonsMainMenuButtonText.text = LocalizationManager.GetLocalizationData().Game.ProcessButtonsMainMenuButtonText;
            gameOverPanelInfoText.text = LocalizationManager.GetLocalizationData().Game.GameOverPanelInfoText;
            gameOverPanelMessageBox1InfoText.text = LocalizationManager.GetLocalizationData().Game.GameOverPanelMessageBox1InfoText;
            gameOverPanelMessageBox1YesBtnText.text = LocalizationManager.GetLocalizationData().Game.GameOverPanelMessageBox1YesBtnText;
            gameOverPanelMessageBox1NoBtnText.text = LocalizationManager.GetLocalizationData().Game.GameOverPanelMessageBox1NoBtnText;
            gameOverPanelMessageBox2InfoText.text = LocalizationManager.GetLocalizationData().Game.GameOverPanelMessageBox2InfoText;
            gameOverPanelMessageBox2OkBtnText.text = LocalizationManager.GetLocalizationData().Game.GameOverPanelMessageBox2OkBtnText;
            gameOverPanelPlayAgainButtonText.text = LocalizationManager.GetLocalizationData().Game.GameOverPanelPlayAgainButtonText;
            gameOverPanelMainMenuButtonText.text = LocalizationManager.GetLocalizationData().Game.GameOverPanelMainMenuButtonText;
            gameOverPanelExitGameButtonText.text = LocalizationManager.GetLocalizationData().Game.GameOverPanelExitGameButtonText;
        }

        private void EarnScoreTextEffect()
        {
            scoreIncDecObj.SetActive(true);
            TweenManager.TweenText(scoreIncDecObj, LevelController.Levels[LevelController.ActiveLevelIndex].GetEarnedScoreAmount.ToString(), Color.green, true);
        }

        private void EarnTimeTextEffect()
        {
            timeIncDecObj.SetActive(true);
            TweenManager.TweenText(timeIncDecObj, LevelController.Levels[LevelController.ActiveLevelIndex].GetEarnedTimeAmount.ToString(), Color.green, true);
        }

        private void LooseTimeTextEffect()
        {
            timeIncDecObj.SetActive(true);
            TweenManager.TweenText(timeIncDecObj, LevelController.Levels[LevelController.ActiveLevelIndex].GetLoseTimeAmount.ToString(), Color.red, false);
        }

        private void LooseLifeTextEffect()
        {
            lifeIncDecObj.SetActive(true);
            TweenManager.TweenText(lifeIncDecObj, LevelController.Levels[LevelController.ActiveLevelIndex].GetLoseLifeAmount.ToString(), Color.red, false);
        }

        public static int TotalScore
        {
            get { return totalScore; }
        }
    }
}