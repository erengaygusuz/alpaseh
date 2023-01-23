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
        public UnityEvent EarnLife { get; set; }

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

            Levels[0] = new Level(1f, 10, 1.0f, 3, 5);
            Levels[0].WordList = wordParserService.WordDatas.LevelWordList[0];

            Levels[1] = new Level(2f, 20, 1.0f, 4, 5);
            Levels[1].WordList = wordParserService.WordDatas.LevelWordList[1];

            Levels[2] = new Level(3f, 30, 1.0f, 5, 5);
            Levels[2].WordList = wordParserService.WordDatas.LevelWordList[2];

            Levels[3] = new Level(4f, 40, 1.0f, 6, 5);
            Levels[3].WordList = wordParserService.WordDatas.LevelWordList[3];

            Levels[4] = new Level(5f, 50, 1.0f, 7, 5);
            Levels[4].WordList = wordParserService.WordDatas.LevelWordList[4];
        }

        private void InitEvents()
        {
            EarnScore = new UnityEvent();
            LooseLife = new UnityEvent();
            LooseTime = new UnityEvent();
            EarnTime = new UnityEvent();
            EarnLife = new UnityEvent();
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
            if (Levels[ActiveLevelIndex].CorrectAnswer)
            {
                totalScore += Levels[ActiveLevelIndex].EarnedScoreAmount;
                totalTime += Levels[ActiveLevelIndex].EarnedTimeAmount;

                EarnTime.Invoke();
                EarnScore.Invoke();
            }

            else
            {
                totalLife -= Levels[ActiveLevelIndex].LoseLifeAmount;
                totalTime -= Levels[ActiveLevelIndex].LoseTimeAmount;

                LooseLife.Invoke();
                LooseTime.Invoke();
            }

            Levels[ActiveLevelIndex].ActiveQuestionIndex++;
        }

        public void IncreaseLife(ref float totalLife)
        {
            totalLife += Levels[ActiveLevelIndex].LifeIncreaseAmount;
        }

        public void CalculateActiveLevelAndQuestionIndex(ref float totalLife)
        {
            if (Levels[ActiveLevelIndex].ActiveQuestionIndex == GetActiveLevel().WordList.Count)
            {
                IncreaseLife(ref totalLife);

                EarnLife.Invoke();

                ActiveLevelIndex++;

                Levels[ActiveLevelIndex].ActiveQuestionIndex = 0;
            }
        }
    }
}
