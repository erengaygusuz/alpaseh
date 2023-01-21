using FTRGames.Alpaseh.Presenters;
using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FTRGames.Alpaseh.LifeTimeScopes
{
    public class HighScoresLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private HighScoresView highScoresView;

        [SerializeField]
        private GameObject scoreListRow;

        [SerializeField]
        private GameObject scoreListContent;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<HighScoresPresenter>();

            builder.Register<HighScoresService>(Lifetime.Scoped);

            builder.RegisterFactory<GameObject>(container =>
            {
                GameObject InstantiateScoreListRow()
                {
                    return container.Instantiate(scoreListRow, scoreListContent.transform);
                }

                return InstantiateScoreListRow;
            },
            Lifetime.Scoped);

            builder.RegisterComponent(highScoresView);
        }
    }
}

