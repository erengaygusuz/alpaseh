using FTRGames.Alpaseh.Services;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class GamePresenter : IStartable, ITickable
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