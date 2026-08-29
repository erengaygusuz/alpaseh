using System;

namespace FTRGames.Alpaseh.Services
{
    public sealed class GameSessionService
    {
        private readonly GameConfig gameConfig;

        public GameSessionService(GameConfig gameConfig)
        {
            this.gameConfig = gameConfig ?? throw new ArgumentNullException(nameof(gameConfig));
        }

        public float TotalTime { get; private set; }
        public float TotalLife { get; private set; }
        public int TotalScore { get; private set; }

        public bool IsGameOver { get; private set; }
        public bool IsGamePaused { get; private set; }
        public bool IsGameCompleted { get; private set; }

        public bool CanTick => !IsGameOver && !IsGamePaused;
        public bool ShouldGameOver => TotalLife <= 0.0f || TotalTime <= 0.0f;

        public void Initialize()
        {
            TotalTime = gameConfig.InitialTime;
            TotalLife = gameConfig.InitialLife;
            TotalScore = 0;

            IsGameOver = false;
            IsGamePaused = false;
            IsGameCompleted = false;
        }

        public void Tick(float deltaTime)
        {
            TotalTime -= deltaTime;
        }

        public void AddTime(float amount)
        {
            TotalTime += amount;
        }

        public void LoseTime(float amount)
        {
            TotalTime -= amount;
        }

        public void AddLife(float amount)
        {
            TotalLife += amount;
        }

        public void LoseLife(float amount)
        {
            TotalLife -= amount;
        }

        public void AddScore(int amount)
        {
            TotalScore += amount;
        }

        public void Pause()
        {
            IsGamePaused = true;
        }

        public void Resume()
        {
            IsGamePaused = false;
        }

        public void MarkCompleted()
        {
            IsGameCompleted = true;
        }

        public void MarkGameOver()
        {
            TotalLife = 0.0f;
            TotalTime = 0.0f;
            IsGameOver = true;
        }
    }
}
