using System;
using FTRGames.Alpaseh.Models;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public sealed class LevelService
    {
        private readonly WordParserService wordParserService;
        private readonly LevelCatalog levelCatalog;

        private Level[] levels = Array.Empty<Level>();

        public int LevelCount => levels.Length;
        public int ActiveLevelIndex { get; private set; }
        public bool IsLastLevel => LevelCount > 0 && ActiveLevelIndex == LevelCount - 1;

        public UnityEvent EarnScore { get; } = new UnityEvent();
        public UnityEvent LoseLife { get; } = new UnityEvent();
        public UnityEvent LoseTime { get; } = new UnityEvent();
        public UnityEvent EarnTime { get; } = new UnityEvent();
        public UnityEvent EarnLife { get; } = new UnityEvent();

        public LevelService(WordParserService wordParserService, LevelCatalog levelCatalog)
        {
            this.wordParserService = wordParserService;
            this.levelCatalog = levelCatalog;
        }

        public void Initialization()
        {
            InitLevels();
            ActiveLevelIndex = 0;
        }

        public Level GetActiveLevel()
        {
            if (LevelCount == 0)
            {
                throw new InvalidOperationException("Level service has not been initialized.");
            }

            return levels[ActiveLevelIndex];
        }

        public void CalculateTimeScoreLifeAmount(GameSessionService gameSessionService)
        {
            Level activeLevel = GetActiveLevel();

            if (activeLevel.CorrectAnswer)
            {
                gameSessionService.AddScore(activeLevel.EarnedScoreAmount);
                gameSessionService.AddTime(activeLevel.EarnedTimeAmount);

                EarnTime.Invoke();
                EarnScore.Invoke();
            }
            else
            {
                gameSessionService.LoseLife(activeLevel.LoseLifeAmount);
                gameSessionService.LoseTime(activeLevel.LoseTimeAmount);

                LoseLife.Invoke();
                LoseTime.Invoke();
            }

            activeLevel.AdvanceQuestion();
        }

        public void IncreaseLife(GameSessionService gameSessionService)
        {
            gameSessionService.AddLife(GetActiveLevel().LifeIncreaseAmount);
        }

        public void CalculateActiveLevelAndQuestionIndex(GameSessionService gameSessionService)
        {
            Level activeLevel = GetActiveLevel();

            if (activeLevel.ActiveQuestionIndex != activeLevel.WordList.Count)
            {
                return;
            }

            Level lastLevel = levels[LevelCount - 1];
            int lastLevelWordListLastItemIndex = lastLevel.WordList.Count - 1;

            if (IsLastLevel && activeLevel.ActiveQuestionIndex != lastLevelWordListLastItemIndex)
            {
                return;
            }

            IncreaseLife(gameSessionService);
            EarnLife.Invoke();

            ActiveLevelIndex++;
            GetActiveLevel().ResetQuestionProgress();
        }

        private void InitLevels()
        {
            int wordLevelCount = wordParserService.WordDatas.LevelWordList.Count;

            if (levelCatalog.Count != wordLevelCount)
            {
                throw new InvalidOperationException(
                    $"Level catalog count ({levelCatalog.Count}) does not match word level count ({wordLevelCount}).");
            }

            levels = new Level[levelCatalog.Count];

            for (int i = 0; i < levelCatalog.Count; i++)
            {
                LevelConfig config = levelCatalog.GetLevel(i);

                levels[i] = new Level(
                    config.LifeIncreaseAmount,
                    config.EarnedScoreAmount,
                    config.LoseLifeAmount,
                    config.EarnedTimeAmount,
                    config.LoseTimeAmount,
                    wordParserService.WordDatas.LevelWordList[i]);
            }
        }
    }
}
