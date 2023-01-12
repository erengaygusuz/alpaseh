using System.Collections.Generic;

namespace FTRGames.Alpaseh.Core
{
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

                LevelController.EarnTime.Invoke();
                LevelController.EarnScore.Invoke();
            }

            else
            {
                totalLife -= loseLifeAmount;
                totalTime -= loseTimeAmount;

                LevelController.LooseLife.Invoke();
                LevelController.LooseTime.Invoke();
            }

            activeQuestionIndex++;
        }

        public void CalculateActiveLevelAndQuestionIndex()
        {
            if (activeQuestionIndex == LevelController.GetActiveLevel().WordList.Count)
            {
                LevelController.ActiveLevelIndex++;

                activeQuestionIndex = 0;
            }
        }

        public void IncreaseLife(ref float totalLife)
        {
            totalLife += lifeIncreaseAmount;
        }
    }
}
