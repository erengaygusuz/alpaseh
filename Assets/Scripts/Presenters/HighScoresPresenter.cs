using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using System;
using UnityEngine;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class HighScoresPresenter : IStartable, ITickable
    {
        private readonly HighScoresView highScoresView;
        private readonly HighScoresService highScoresService;
        private readonly ScoreService scoreService;

        private readonly Func<GameObject> scoreListFactory;

        public HighScoresPresenter(HighScoresView highScoresView, HighScoresService highScoresService, ScoreService scoreService, Func<GameObject> scoreListFactory)
        {
            this.highScoresView = highScoresView;
            this.scoreService = scoreService;
            this.highScoresService = highScoresService;
            this.scoreListFactory = scoreListFactory;
        }

        void IStartable.Start()
        {
            scoreService.Initialization();

            highScoresService.GetAllScoreList(highScoresView, scoreListFactory);

            EventBinding();
        }

        public void Tick()
        {
            scoreService.UpdateScoreValues();
        }

        private void EventBinding()
        {
            highScoresView.mainMenuButton.onClick.AddListener(() => highScoresService.GoToMainMenuBtnClick());
            highScoresView.deleteAllRecordsButton.onClick.AddListener(() => highScoresService.DeleteAllScoresBtnClick(highScoresView));
            highScoresView.infoPanelYesButton.onClick.AddListener(() => highScoresService.InfoPanelYesBtnClick(highScoresView));
            highScoresView.infoPanelNoButton.onClick.AddListener(() => highScoresService.InfoPanelNoBtnClick(highScoresView));
            highScoresView.infoPanelOk1Button.onClick.AddListener(() => highScoresService.InfoPanelOkBtnClick(highScoresView));
            highScoresView.infoPanelOk2Button.onClick.AddListener(() => highScoresService.InfoPanelOkBtnClick(highScoresView));
        }
    }
}