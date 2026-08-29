using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class GamePresenter : IStartable, ITickable
    {
        private readonly GameView gameView;
        private readonly AudioView audioView;
        private readonly GameService gameService;
        private readonly GameStartupService gameStartupService;
        private readonly ScoreService scoreService;

        public GamePresenter(
            GameService gameService,
            GameStartupService gameStartupService,
            ScoreService scoreService,
            GameView gameView,
            AudioView audioView)
        {
            this.gameService = gameService;
            this.gameStartupService = gameStartupService;
            this.scoreService = scoreService;
            this.gameView = gameView;
            this.audioView = audioView;
        }

        void IStartable.Start()
        {
            gameStartupService.Start(gameView, audioView);
        }

        public void Tick()
        {
            scoreService.UpdateScoreValues();
            gameService.GameCheck(gameView);
        }
    }
}
