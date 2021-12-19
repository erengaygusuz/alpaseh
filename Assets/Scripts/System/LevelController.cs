using System.Collections.Generic;
using UnityEngine.Events;

public class LevelController : Singleton<LevelController>
{
    public Level[] Levels { get; set; }

    public int ActiveLevelIndex { get; set; }

    public UnityEvent EarnScore { get; set; }
    public UnityEvent LooseLife { get; set; }
    public UnityEvent LooseTime { get; set; }
    public UnityEvent EarnTime { get; set; }

    public void Initialization()
    {
        InitLevels();
        InitActiveLevel();
        InitEvents();
    }

    private void InitLevels()
    {
        Levels = new Level[6];

        Levels[0] = new Level(0.25f, 10, 0.25f, 3, 5);
        Levels[0].WordList = WordParser.Instance.WordDatas.LevelWordList[0];

        Levels[1] = new Level(0.50f, 20, 0.25f, 4, 5);
        Levels[1].WordList = WordParser.Instance.WordDatas.LevelWordList[1];

        Levels[2] = new Level(0.75f, 30, 0.25f, 5, 5);
        Levels[2].WordList = WordParser.Instance.WordDatas.LevelWordList[2];

        Levels[3] = new Level(1.00f, 40, 0.25f, 6, 5);
        Levels[3].WordList = WordParser.Instance.WordDatas.LevelWordList[3];

        Levels[4] = new Level(1.25f, 50, 0.25f, 7, 5);
        Levels[4].WordList = WordParser.Instance.WordDatas.LevelWordList[4];

        Levels[5] = new Level(1.50f, 60, 0.25f, 8, 5);
        Levels[5].WordList = WordParser.Instance.WordDatas.LevelWordList[5];
    }

    private void InitEvents()
    {
        EarnScore = new UnityEvent();
        LooseLife = new UnityEvent();
        LooseTime = new UnityEvent();
        EarnTime = new UnityEvent();
    }

    private void InitActiveLevel()
    {
        ActiveLevelIndex = 0;
    }

    public Level GetActiveLevel()
    {
        return Levels[ActiveLevelIndex];
    }
}

public class Level
{
    private float lifeIncreaseAmount;
    private int earnedScoreAmount;
    private float loseLifeAmount;
    private float earnedTimeAmount;
    private float loseTimeAmount;
    private bool correctAnswer;
    public int activeQuestionIndex;

    public List<string> WordList { get; set; }

    public Level(float lifeIncreaseAmount, int earnedScoreAmount, float loseLifeAmount, int earnedTimeAmount, int loseTimeAmount)
    {
        this.lifeIncreaseAmount = lifeIncreaseAmount;
        this.earnedScoreAmount = earnedScoreAmount;
        this.loseLifeAmount = loseLifeAmount;
        this.earnedTimeAmount = earnedTimeAmount;
        this.loseTimeAmount = loseTimeAmount;
        
    }

    public float GetLoseLifeAmount
    {
        get { return loseLifeAmount; }
    }

    public float GetEarnedTimeAmount
    {
        get { return earnedTimeAmount; }
    }

    public float GetLoseTimeAmount
    {
        get { return loseTimeAmount; }
    }

    public float GetEarnedScoreAmount
    {
        get { return earnedScoreAmount; }
    }

    public bool CheckEnteredNumberWord(string enteredNumberWord, string activeQuestionNumberWord)
    {
        correctAnswer = true;

        if (enteredNumberWord.Length == 0)
        {
            correctAnswer = false;
        }

        else
        {
            for (int i = 0; i < enteredNumberWord.Length; i++)
            {
                if (enteredNumberWord[i] != activeQuestionNumberWord[i])
                {
                    if (!((activeQuestionNumberWord[i] == '6' && enteredNumberWord[i] == '9') || (activeQuestionNumberWord[i] == '9' && enteredNumberWord[i] == '6')))
                    {
                        correctAnswer = false;

                        break;
                    }
                }
            }
        }

        return correctAnswer;
    }

    public void CalculateTimeScoreLifeAmount(ref float totalTime, ref int totalScore, ref float totalLife)
    {
        if (correctAnswer)
        {
            totalScore += earnedScoreAmount;
            totalTime += earnedTimeAmount;

            LevelController.Instance.EarnTime.Invoke();
            LevelController.Instance.EarnScore.Invoke();
        }

        else
        {
            totalLife -= loseLifeAmount;
            totalTime -= loseTimeAmount;

            LevelController.Instance.LooseLife.Invoke();
            LevelController.Instance.LooseTime.Invoke();
        }

        activeQuestionIndex++;
    }

    public void CalculateActiveLevelAndQuestionIndex()
    {
        if (activeQuestionIndex == LevelController.Instance.GetActiveLevel().WordList.Count)
        {
            LevelController.Instance.ActiveLevelIndex++;

            activeQuestionIndex = 0;
        }
    }

    public void IncreaseLife(ref float totalLife)
    {
        totalLife += lifeIncreaseAmount;
    }
}
