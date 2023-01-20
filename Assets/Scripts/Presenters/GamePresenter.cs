using VContainer.Unity;

namespace FTRGames.Alpaseh.Core
{
    public class GamePresenter : ITickable, IStartable
    {
        private readonly GameService gameService;
        private readonly ScoreService scoreService;

        public GamePresenter(GameService gameService, ScoreService scoreService)
        {
            this.gameService = gameService;
            this.scoreService = scoreService;
        }

        void IStartable.Start()
        {
            scoreService.Initialization();
            gameService.Initialization();
        }

        public void Tick()
        {
            scoreService.UpdateScoreValues();
            gameService.GameCheck();
        }
    }
}