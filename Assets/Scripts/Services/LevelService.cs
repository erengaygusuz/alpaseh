using FTRGames.Alpaseh.Models;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public class LevelService
    {
        public Level[] Levels { get; set; }

        public int ActiveLevelIndex { get; set; }

        public UnityEvent EarnScore { get; set; }
        public UnityEvent LooseLife { get; set; }
        public UnityEvent LooseTime { get; set; }
        public UnityEvent EarnTime { get; set; }

        private readonly WordParserService wordParserService;

        public LevelService (WordParserService wordParserService)
        {
            this.wordParserService = wordParserService;
        }

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
            Levels[0].WordList = wordParserService.WordDatas.LevelWordList[0];

            Levels[1] = new Level(0.50f, 20, 0.25f, 4, 5);
            Levels[1].WordList = wordParserService.WordDatas.LevelWordList[1];

            Levels[2] = new Level(0.75f, 30, 0.25f, 5, 5);
            Levels[2].WordList = wordParserService.WordDatas.LevelWordList[2];

            Levels[3] = new Level(1.00f, 40, 0.25f, 6, 5);
            Levels[3].WordList = wordParserService.WordDatas.LevelWordList[3];

            Levels[4] = new Level(1.25f, 50, 0.25f, 7, 5);
            Levels[4].WordList = wordParserService.WordDatas.LevelWordList[4];

            Levels[5] = new Level(1.50f, 60, 0.25f, 8, 5);
            Levels[5].WordList = wordParserService.WordDatas.LevelWordList[5];
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

        public void CalculateTimeScoreLifeAmount(ref float totalTime, ref int totalScore, ref float totalLife)
        {
            if (Levels[ActiveLevelIndex].correctAnswer)
            {
                totalScore += Levels[ActiveLevelIndex].earnedScoreAmount;
                totalTime += Levels[ActiveLevelIndex].earnedTimeAmount;

                EarnTime.Invoke();
                EarnScore.Invoke();
            }

            else
            {
                totalLife -= Levels[ActiveLevelIndex].loseLifeAmount;
                totalTime -= Levels[ActiveLevelIndex].loseTimeAmount;

                LooseLife.Invoke();
                LooseTime.Invoke();
            }

            Levels[ActiveLevelIndex].activeQuestionIndex++;
        }

        public void CalculateActiveLevelAndQuestionIndex()
        {
            if (Levels[ActiveLevelIndex].activeQuestionIndex == GetActiveLevel().WordList.Count)
            {
                ActiveLevelIndex++;

                Levels[ActiveLevelIndex].activeQuestionIndex = 0;
            }
        }
    }
}
