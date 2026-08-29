using System;
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
        private readonly LevelCatalog levelCatalog;

        public LevelService(WordParserService wordParserService, LevelCatalog levelCatalog)
        {
            this.wordParserService = wordParserService;
            this.levelCatalog = levelCatalog;
        }

        public void Initialization()
        {
            InitLevels();
            InitActiveLevel();
            InitEvents();
        }

        private void InitLevels()
        {
            int wordLevelCount = wordParserService.WordDatas.LevelWordList.Count;

            if (levelCatalog.Count != wordLevelCount)
            {
                throw new InvalidOperationException(
                    $"Level catalog count ({levelCatalog.Count}) does not match word level count ({wordLevelCount}).");
            }

            Levels = new Level[levelCatalog.Count];

            for (int i = 0; i < levelCatalog.Count; i++)
            {
                LevelConfig config = levelCatalog.GetLevel(i);

                Levels[i] = new Level(
                    config.LifeIncreaseAmount,
                    config.EarnedScoreAmount,
                    config.LoseLifeAmount,
                    config.EarnedTimeAmount,
                    config.LoseTimeAmount);

                Levels[i].WordList = wordParserService.WordDatas.LevelWordList[i];
            }
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

        public void CalculateTimeScoreLifeAmount(GameSessionService gameSessionService)
        {
            if (Levels[ActiveLevelIndex].CorrectAnswer)
            {
                gameSessionService.AddScore(Levels[ActiveLevelIndex].EarnedScoreAmount);
                gameSessionService.AddTime(Levels[ActiveLevelIndex].EarnedTimeAmount);

                EarnTime.Invoke();
                EarnScore.Invoke();
            }
            else
            {
                gameSessionService.LoseLife(Levels[ActiveLevelIndex].LoseLifeAmount);
                gameSessionService.LoseTime(Levels[ActiveLevelIndex].LoseTimeAmount);

                LooseLife.Invoke();
                LooseTime.Invoke();
            }

            Levels[ActiveLevelIndex].ActiveQuestionIndex++;
        }

        public void IncreaseLife(GameSessionService gameSessionService)
        {
            gameSessionService.AddLife(Levels[ActiveLevelIndex].LifeIncreaseAmount);
        }

        public void CalculateActiveLevelAndQuestionIndex(GameSessionService gameSessionService)
        {
            if (Levels[ActiveLevelIndex].ActiveQuestionIndex == GetActiveLevel().WordList.Count)
            {
                int levelCount = Levels.Length;
                var lastLevel = Levels[levelCount - 1];

                int lastLevelWordListLastItemIndex = lastLevel.WordList.Count - 1;

                if (ActiveLevelIndex == levelCount - 1 && Levels[ActiveLevelIndex].ActiveQuestionIndex != lastLevelWordListLastItemIndex)
                {
                    return;
                }

                IncreaseLife(gameSessionService);

                EarnLife.Invoke();

                ActiveLevelIndex++;

                Levels[ActiveLevelIndex].ActiveQuestionIndex = 0;
            }
        }
    }
}
