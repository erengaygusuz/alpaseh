using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
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
    private int totalScore;

    [SerializeField]
    private GameObject lifeIncDecObj;
    [SerializeField]
    private GameObject scoreIncDecObj;
    [SerializeField]
    private GameObject timeIncDecObj;

    public UnityEvent GameOver { get; set; }

    private bool isGameOver;

    private bool isWaitForTimeTick;

    public bool IsGotoMainMenuBtnClick;

    private void Start()
    {
        WordParser.Instance.Initialization();
        WordNumberConverter.Instance.Initialization();
        LevelController.Instance.Initialization();
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
        activeLevelText.text = (LevelController.Instance.ActiveLevelIndex + 1).ToString();

        GameOverEventBinding();
        GetActiveQuestionText();

        AudioManager.Instance.StopAudio(AudioManager.Instance.loopAudioSource);
        AudioManager.Instance.PlayGameSceneAudio();
        isWaitForTimeTick = true;

        AssignTranslatedValues();
        BindEvents();
    }

    private void BindEvents()
    {
        LevelController.Instance.EarnScore.AddListener(EarnScoreTextEffect);
        LevelController.Instance.EarnTime.AddListener(EarnTimeTextEffect);
        LevelController.Instance.LooseLife.AddListener(LooseLifeTextEffect);
        LevelController.Instance.LooseTime.AddListener(LooseTimeTextEffect);
    }

    private void GameOverEventBinding()
    {
        if (GameOver == null)
        {
            GameOver = new UnityEvent();
        }

        GameOver.AddListener(GameFinishController.Instance.ShowInfoPanelUI);
        GameOver.AddListener(GameFinishController.Instance.ShowGameOverPanel);
        GameOver.AddListener(GameFinishController.Instance.StopGameLoopAudio);
        GameOver.AddListener(GameFinishController.Instance.PlayGameOverAudio);
    }

    private void GetActiveQuestionText()
    {
        int activeQuestionIndex = LevelController.Instance.Levels[LevelController.Instance.ActiveLevelIndex].activeQuestionIndex;

        string question = LevelController.Instance.Levels[LevelController.Instance.ActiveLevelIndex].WordList[activeQuestionIndex].ToString();

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
        bool isCorrectAnswer = LevelController.Instance.Levels[LevelController.Instance.ActiveLevelIndex].CheckEnteredNumberWord(enteredNumberWordText.text, WordNumberConverter.Instance.GetNumbersFromWord(questionText.text));
        LevelController.Instance.Levels[LevelController.Instance.ActiveLevelIndex].CalculateTimeScoreLifeAmount(ref totalTime, ref totalScore, ref totalLife);
        LevelController.Instance.Levels[LevelController.Instance.ActiveLevelIndex].CalculateActiveLevelAndQuestionIndex();

        totalTimeText.text = Mathf.Round(totalTime).ToString();
        totalLifeText.text = totalLife.ToString("F2");
        totalScoreText.text = totalScore.ToString();
        activeLevelText.text = (LevelController.Instance.ActiveLevelIndex + 1).ToString();

        GetActiveQuestionText();
        ClearEnteredNumberWordText();

        if (isCorrectAnswer)
        {
            AudioManager.Instance.PlayCorrectAnswerAudio();
        }

        else
        {
            AudioManager.Instance.PlayWrongAnswerAudio();
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

        GameFinishController.Instance.ShowInfoPanelUI();
    }

    public void NumberBtnClick(Text text)
    {
        enteredNumberWordText.text += text.text;
    }

    private IEnumerator PlayTimeTickEverySecond()
    {
        isWaitForTimeTick = false;
        yield return new WaitForSeconds(1.0f);
        AudioManager.Instance.PlayTimeTickAudio();
        isWaitForTimeTick = true;
    }

    private void AssignTranslatedValues()
    {
        topBarTotalTimeText.text = LocalizationManager.Instance.GetLocalizationData().Game.TopBarTotalTimeText;
        topBarTotalLifeText.text = LocalizationManager.Instance.GetLocalizationData().Game.TopBarTotalLifeText;
        topBarActiveLevelText.text = LocalizationManager.Instance.GetLocalizationData().Game.TopBarActiveLevelText;
        topBarTotalScoreText.text = LocalizationManager.Instance.GetLocalizationData().Game.TopBarTotalScoreText;
        processButtonsCheckButtonText.text = LocalizationManager.Instance.GetLocalizationData().Game.ProcessButtonsCheckButtonText;
        processButtonsDeleteButtonText.text = LocalizationManager.Instance.GetLocalizationData().Game.ProcessButtonsDeleteButtonText;
        processButtonsMainMenuButtonText.text = LocalizationManager.Instance.GetLocalizationData().Game.ProcessButtonsMainMenuButtonText;
        gameOverPanelInfoText.text = LocalizationManager.Instance.GetLocalizationData().Game.GameOverPanelInfoText;
        gameOverPanelMessageBox1InfoText.text = LocalizationManager.Instance.GetLocalizationData().Game.GameOverPanelMessageBox1InfoText;
        gameOverPanelMessageBox1YesBtnText.text = LocalizationManager.Instance.GetLocalizationData().Game.GameOverPanelMessageBox1YesBtnText;
        gameOverPanelMessageBox1NoBtnText.text = LocalizationManager.Instance.GetLocalizationData().Game.GameOverPanelMessageBox1NoBtnText;
        gameOverPanelMessageBox2InfoText.text = LocalizationManager.Instance.GetLocalizationData().Game.GameOverPanelMessageBox2InfoText;
        gameOverPanelMessageBox2OkBtnText.text = LocalizationManager.Instance.GetLocalizationData().Game.GameOverPanelMessageBox2OkBtnText;
        gameOverPanelPlayAgainButtonText.text = LocalizationManager.Instance.GetLocalizationData().Game.GameOverPanelPlayAgainButtonText;
        gameOverPanelMainMenuButtonText.text = LocalizationManager.Instance.GetLocalizationData().Game.GameOverPanelMainMenuButtonText;
        gameOverPanelExitGameButtonText.text = LocalizationManager.Instance.GetLocalizationData().Game.GameOverPanelExitGameButtonText;
    }

    private void EarnScoreTextEffect()
    {
        scoreIncDecObj.SetActive(true);
        TweenManager.Instance.TweenText(scoreIncDecObj, LevelController.Instance.Levels[LevelController.Instance.ActiveLevelIndex].GetEarnedScoreAmount.ToString(), Color.green, true);
    }

    private void EarnTimeTextEffect()
    {
        timeIncDecObj.SetActive(true);
        TweenManager.Instance.TweenText(timeIncDecObj, LevelController.Instance.Levels[LevelController.Instance.ActiveLevelIndex].GetEarnedTimeAmount.ToString(), Color.green, true);
    }

    private void LooseTimeTextEffect()
    {
        timeIncDecObj.SetActive(true);
        TweenManager.Instance.TweenText(timeIncDecObj, LevelController.Instance.Levels[LevelController.Instance.ActiveLevelIndex].GetLoseTimeAmount.ToString(), Color.red, false);
    }

    private void LooseLifeTextEffect()
    {
        lifeIncDecObj.SetActive(true);
        TweenManager.Instance.TweenText(lifeIncDecObj, LevelController.Instance.Levels[LevelController.Instance.ActiveLevelIndex].GetLoseLifeAmount.ToString(), Color.red, false);
    }

    public int TotalScore
    {
        get { return totalScore; }
    }
}
