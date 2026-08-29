using FTRGames.Alpaseh.Views;

namespace FTRGames.Alpaseh.Services
{
    public class HowToPlayService
    {
        private readonly SceneNavigationService sceneNavigationService;
        private int activeInfoIndex;

        public HowToPlayService(SceneNavigationService sceneNavigationService)
        {
            this.sceneNavigationService = sceneNavigationService;
        }

        public void Initialization(HowToPlayView howToPlayView)
        {
            InitActiveInfoIndex();
            ActivateNavigationButtons(howToPlayView);
            ActivateInfo(howToPlayView, activeInfoIndex);
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

        public void GoToMainMenuBtnClick()
        {
            sceneNavigationService.Load(SceneNames.MainMenu);
        }
    }
}
