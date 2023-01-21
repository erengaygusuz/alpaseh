using System.Collections.Generic;

namespace FTRGames.Alpaseh.Models
{
    public class Level
    {
        private float lifeIncreaseAmount;
        private int earnedScoreAmount;
        private float loseLifeAmount;
        private float earnedTimeAmount;
        private float loseTimeAmount;
        private bool correctAnswer;
        private int activeQuestionIndex;

        public List<string> WordList { get; set; }

        public Level(float lifeIncreaseAmount, int earnedScoreAmount, float loseLifeAmount, int earnedTimeAmount, int loseTimeAmount)
        {
            this.lifeIncreaseAmount = lifeIncreaseAmount;
            this.earnedScoreAmount = earnedScoreAmount;
            this.loseLifeAmount = loseLifeAmount;
            this.earnedTimeAmount = earnedTimeAmount;
            this.loseTimeAmount = loseTimeAmount;
        }

        public float LifeIncreaseAmount
        {
            get { return lifeIncreaseAmount; }
        }

        public int EarnedScoreAmount
        {
            get { return earnedScoreAmount; }
        }

        public float LoseLifeAmount
        {
            get { return loseLifeAmount; }
        }

        public float EarnedTimeAmount
        {
            get { return earnedTimeAmount; }
        }

        public float LoseTimeAmount
        {
            get { return loseTimeAmount; }
        }

        public bool CorrectAnswer
        {
            get { return correctAnswer; }
        }

        public int ActiveQuestionIndex
        {
            get { return activeQuestionIndex; }
            set { activeQuestionIndex = value; }
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

    }
}
