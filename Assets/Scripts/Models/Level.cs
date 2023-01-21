using System.Collections.Generic;
using VContainer;

namespace FTRGames.Alpaseh.Models
{
    public class Level
    {
        private float lifeIncreaseAmount;
        public int earnedScoreAmount;
        public float loseLifeAmount;
        public float earnedTimeAmount;
        public float loseTimeAmount;
        public bool correctAnswer;
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

        public void IncreaseLife(ref float totalLife)
        {
            totalLife += lifeIncreaseAmount;
        }
    }
}
