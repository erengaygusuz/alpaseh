using UnityEngine.Events;

namespace FTRGames.Alpaseh.Core
{
    public class LevelController
    {
        public static Level[] Levels { get; set; }

        public static int ActiveLevelIndex { get; set; }

        public static UnityEvent EarnScore { get; set; }
        public static UnityEvent LooseLife { get; set; }
        public static UnityEvent LooseTime { get; set; }
        public static UnityEvent EarnTime { get; set; }

        public static void Initialization()
        {
            InitLevels();
            InitActiveLevel();
            InitEvents();
        }

        private static void InitLevels()
        {
            Levels = new Level[6];

            Levels[0] = new Level(0.25f, 10, 0.25f, 3, 5);
            Levels[0].WordList = WordParser.WordDatas.LevelWordList[0];

            Levels[1] = new Level(0.50f, 20, 0.25f, 4, 5);
            Levels[1].WordList = WordParser.WordDatas.LevelWordList[1];

            Levels[2] = new Level(0.75f, 30, 0.25f, 5, 5);
            Levels[2].WordList = WordParser.WordDatas.LevelWordList[2];

            Levels[3] = new Level(1.00f, 40, 0.25f, 6, 5);
            Levels[3].WordList = WordParser.WordDatas.LevelWordList[3];

            Levels[4] = new Level(1.25f, 50, 0.25f, 7, 5);
            Levels[4].WordList = WordParser.WordDatas.LevelWordList[4];

            Levels[5] = new Level(1.50f, 60, 0.25f, 8, 5);
            Levels[5].WordList = WordParser.WordDatas.LevelWordList[5];
        }

        private static void InitEvents()
        {
            EarnScore = new UnityEvent();
            LooseLife = new UnityEvent();
            LooseTime = new UnityEvent();
            EarnTime = new UnityEvent();
        }

        private static void InitActiveLevel()
        {
            ActiveLevelIndex = 0;
        }

        public static Level GetActiveLevel()
        {
            return Levels[ActiveLevelIndex];
        }
    }

   
}
