using FTRGames.Alpaseh.Views;
using UnityEngine.SceneManagement;

namespace FTRGames.Alpaseh.Services
{
    public class HowToPlayService
    {
        private readonly LocalizationService localizationService;

        private int activeInfoIndex;

        public HowToPlayService(LocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        public void Initialization(HowToPlayView howToPlayView)
        {
            InitActiveInfoIndex();
            ActivateNavigationButtons(howToPlayView);
            ActivateInfo(howToPlayView, activeInfoIndex);
            AssignTranslatedValues(howToPlayView);
        }

        private void InitActiveInfoIndex()
        {
            activeInfoIndex = 0;
        }

        private void ActivateNavigationButtons(HowToPlayView howToPlayView)
        {
            howToPlayView.leftArrow.SetActive(false);
            howToPlayView.rightArrow.SetActive(true);
        }

        private void ActivateInfo(HowToPlayView howToPlayView, int index)
        {
            for (int i = 0; i < howToPlayView.infos.Length; i++)
            {
                if (i == index)
                {
                    continue;
                }

                howToPlayView.infos[i].SetActive(false);
            }

            howToPlayView.infos[index].SetActive(true);
        }

        public void LeftArrowBtnClick(HowToPlayView howToPlayView)
        {
            activeInfoIndex--;

            if (!howToPlayView.rightArrow.activeInHierarchy)
            {
                howToPlayView.rightArrow.SetActive(true);
            }

            if (activeInfoIndex == 0)
            {
                howToPlayView.leftArrow.SetActive(false);
            }

            ActivateInfo(howToPlayView, activeInfoIndex);
        }

        public void RightArrowBtnClick(HowToPlayView howToPlayView)
        {
            activeInfoIndex++;

            if (!howToPlayView.leftArrow.activeInHierarchy)
            {
                howToPlayView.leftArrow.SetActive(true);
            }

            if (activeInfoIndex == howToPlayView.infos.Length - 1)
            {
                howToPlayView.rightArrow.SetActive(false);
            }

            ActivateInfo(howToPlayView, activeInfoIndex);
        }

        public void GoToMainMenuBtnClick(HowToPlayView howToPlayView)
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void AssignTranslatedValues(HowToPlayView howToPlayView)
        {
            howToPlayView.infoPanelInfo1Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo1Text;
            howToPlayView.infoPanelInfo2Text1.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo2Text1;
            howToPlayView.infoPanelInfo2Text2.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo2Text2;
            howToPlayView.infoPanelInfo3Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber0Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber0Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber1Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber1Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber2Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber2Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber3Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber3Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber4Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber4Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber5Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber5Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber6Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber6Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber7Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber7Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber8Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber8Text;
            howToPlayView.infoPanelInfo3ButtonSetNumber9Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber9Text;
            howToPlayView.infoPanelInfo4Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo4Text;
            howToPlayView.infoPanelInfo4ButtonSetNumber1Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo4ButtonSetNumber1Text;
            howToPlayView.infoPanelInfo4ButtonSetNumber3Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo4ButtonSetNumber3Text;
            howToPlayView.infoPanelInfo4ButtonSetNumber7Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo4ButtonSetNumber7Text;
            howToPlayView.infoPanelInfo4ButtonSetNumber8Text.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo4ButtonSetNumber8Text;
            howToPlayView.infoPanelInfo5Text1.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo5Text1;
            howToPlayView.infoPanelInfo5Text2.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo5Text2;
            howToPlayView.infoPanelInfo5Text3.text = localizationService.GetLocalizationData().HowToPlay.InfoPanelInfo5Text3;
            howToPlayView.mainMenuButtonText.text = localizationService.GetLocalizationData().HowToPlay.MainMenuButtonText;
        }
    }
}


