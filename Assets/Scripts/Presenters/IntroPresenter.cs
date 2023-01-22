using FTRGames.Alpaseh.Services;
using FTRGames.Alpaseh.Views;
using VContainer.Unity;

namespace FTRGames.Alpaseh.Presenters
{
    public class IntroPresenter : IStartable
    {
        private readonly IntroView introView;
        private readonly IntroService introService;

        public IntroPresenter(IntroView introView, IntroService introService)
        {
            this.introView = introView;
            this.introService = introService;
        }

        void IStartable.Start()
        {
            introService.Initialization(introView);

            EventBinding();
        }

        private void EventBinding()
        {
            introView.nextButton.onClick.AddListener(() => introService.NextBtnClick(introView));

            introView.messageBoxOkButton.onClick.AddListener(() => introService.WarningPanelOKBtnClick(introView));

            introView.languageOptions.onValueChanged.AddListener(delegate
            {
                introService.SaveLanguageOption(introView);
            });
        }
    }
}