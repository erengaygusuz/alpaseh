using System;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    [CreateAssetMenu(fileName = "LevelCatalog", menuName = "Alpaseh/Data/Level Catalog")]
    public sealed class LevelCatalog : ScriptableObject
    {
        [SerializeField]
        private LevelConfig[] levels = Array.Empty<LevelConfig>();

        public int Count => levels?.Length ?? 0;

        public LevelConfig GetLevel(int levelIndex)
        {
            if (levels == null || levels.Length == 0)
            {
                throw new InvalidOperationException("Level catalog does not contain any level entries.");
            }

            if (levelIndex < 0 || levelIndex >= levels.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(levelIndex));
            }

            LevelConfig level = levels[levelIndex];

            if (level == null)
            {
                throw new InvalidOperationException($"Level catalog entry at index {levelIndex} is missing.");
            }

            return level;
        }
    }

    [Serializable]
    public sealed class LevelConfig
    {
        [SerializeField]
        private float lifeIncreaseAmount;

        [SerializeField]
        private int earnedScoreAmount;

        [SerializeField]
        private float loseLifeAmount;

        [SerializeField]
        private float earnedTimeAmount;

        [SerializeField]
        private float loseTimeAmount;

        public float LifeIncreaseAmount => lifeIncreaseAmount;
        public int EarnedScoreAmount => earnedScoreAmount;
        public float LoseLifeAmount => loseLifeAmount;
        public float EarnedTimeAmount => earnedTimeAmount;
        public float LoseTimeAmount => loseTimeAmount;
    }
}
