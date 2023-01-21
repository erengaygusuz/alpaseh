using FTRGames.Alpaseh.Services;
using System;
using UnityEngine;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class HighScoresPresenter : IStartable, ITickable
    {
        private readonly HighScoresService highScoresService;
        private readonly ScoreService scoreService;

        private readonly Func<GameObject> scoreListFactory;

        public HighScoresPresenter(HighScoresService highScoresService, ScoreService scoreService, Func<GameObject> scoreListFactory)
        {
            this.scoreService = scoreService;
            this.highScoresService = highScoresService;
            this.scoreListFactory = scoreListFactory;
        }

        void IStartable.Start()
        {
            scoreService.Initialization();
            highScoresService.Initialization(scoreListFactory);
        }

        public void Tick()
        {
            scoreService.UpdateScoreValues();
        }
    }
}