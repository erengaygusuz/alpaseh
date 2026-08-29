using System;
using System.Collections.Generic;

namespace FTRGames.Alpaseh.Models
{
    public sealed class Level
    {
        public Level(
            float lifeIncreaseAmount,
            int earnedScoreAmount,
            float loseLifeAmount,
            int earnedTimeAmount,
            int loseTimeAmount,
            IReadOnlyList<string> wordList)
        {
            WordList = wordList ?? throw new ArgumentNullException(nameof(wordList));
            LifeIncreaseAmount = lifeIncreaseAmount;
            EarnedScoreAmount = earnedScoreAmount;
            LoseLifeAmount = loseLifeAmount;
            EarnedTimeAmount = earnedTimeAmount;
            LoseTimeAmount = loseTimeAmount;
        }

        public IReadOnlyList<string> WordList { get; }
        public float LifeIncreaseAmount { get; }
        public int EarnedScoreAmount { get; }
        public float LoseLifeAmount { get; }
        public int EarnedTimeAmount { get; }
        public int LoseTimeAmount { get; }
        public bool CorrectAnswer { get; private set; }
        public int ActiveQuestionIndex { get; private set; }

        public bool CheckEnteredNumberWord(string enteredNumberWord, string activeQuestionNumberWord)
        {
            CorrectAnswer = !string.IsNullOrEmpty(enteredNumberWord) &&
                string.Equals(enteredNumberWord, activeQuestionNumberWord, StringComparison.Ordinal);

            return CorrectAnswer;
        }

        public void AdvanceQuestion()
        {
            ActiveQuestionIndex++;
        }

        public void ResetQuestionProgress()
        {
            ActiveQuestionIndex = 0;
        }
    }
}
