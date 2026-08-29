namespace FTRGames.Alpaseh.Services
{
    public sealed class GameTimerService
    {
        private const float TimeTickInterval = 1.0f;

        private readonly GameSessionService gameSessionService;
        private readonly AudioService audioService;

        private float timeTickElapsed;

        public GameTimerService(GameSessionService gameSessionService, AudioService audioService)
        {
            this.gameSessionService = gameSessionService;
            this.audioService = audioService;
        }

        public void Initialize()
        {
            timeTickElapsed = 0.0f;
        }

        public void Tick(float deltaTime)
        {
            if (!gameSessionService.CanTick)
            {
                return;
            }

            gameSessionService.Tick(deltaTime);
            timeTickElapsed += deltaTime;

            if (timeTickElapsed < TimeTickInterval)
            {
                return;
            }

            timeTickElapsed -= TimeTickInterval;

            if (!gameSessionService.IsGameCompleted)
            {
                audioService.PlayTimeTickAudio();
            }
        }
    }
}
