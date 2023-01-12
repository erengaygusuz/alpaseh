using FTRGames.Alpaseh.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Scenes
{
    public class HowToPlay : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] infos;
        [SerializeField]
        private GameObject leftArrow;
        [SerializeField]
        private GameObject rightArrow;

        private int activeInfoIndex;

        [SerializeField]
        private Text infoPanelInfo1Text;
        [SerializeField]
        private Text infoPanelInfo2Text1;
        [SerializeField]
        private Text infoPanelInfo2Text2;
        [SerializeField]
        private Text infoPanelInfo3Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber0Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber1Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber2Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber3Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber4Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber5Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber6Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber7Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber8Text;
        [SerializeField]
        private Text infoPanelInfo3ButtonSetNumber9Text;
        [SerializeField]
        private Text infoPanelInfo4Text;
        [SerializeField]
        private Text infoPanelInfo4ButtonSetNumber1Text;
        [SerializeField]
        private Text infoPanelInfo4ButtonSetNumber3Text;
        [SerializeField]
        private Text infoPanelInfo4ButtonSetNumber7Text;
        [SerializeField]
        private Text infoPanelInfo4ButtonSetNumber8Text;
        [SerializeField]
        private Text infoPanelInfo5Text1;
        [SerializeField]
        private Text infoPanelInfo5Text2;
        [SerializeField]
        private Text infoPanelInfo5Text3;
        [SerializeField]
        private Text mainMenuButtonText;

        private void Start()
        {
            Initialization();
        }

        private void Initialization()
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
            activeInfoIndex = 0;
            ActivateInfo(activeInfoIndex);

            AssignTranslatedValues();
        }

        private void ActivateInfo(int index)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                if (i == index)
                {
                    continue;
                }

                infos[i].SetActive(false);
            }

            infos[index].SetActive(true);
        }

        public void LeftArrowBtnClick()
        {
            activeInfoIndex--;

            if (!rightArrow.activeInHierarchy)
            {
                rightArrow.SetActive(true);
            }

            if (activeInfoIndex == 0)
            {
                leftArrow.SetActive(false);
            }

            ActivateInfo(activeInfoIndex);
        }

        public void RightArrowBtnClick()
        {
            activeInfoIndex++;

            if (!leftArrow.activeInHierarchy)
            {
                leftArrow.SetActive(true);
            }

            if (activeInfoIndex == infos.Length - 1)
            {
                rightArrow.SetActive(false);
            }

            ActivateInfo(activeInfoIndex);
        }

        public void GoToMainMenuBtnClick()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void AssignTranslatedValues()
        {
            infoPanelInfo1Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo1Text;
            infoPanelInfo2Text1.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo2Text1;
            infoPanelInfo2Text2.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo2Text2;
            infoPanelInfo3Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3Text;
            infoPanelInfo3ButtonSetNumber0Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber0Text;
            infoPanelInfo3ButtonSetNumber1Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber1Text;
            infoPanelInfo3ButtonSetNumber2Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber2Text;
            infoPanelInfo3ButtonSetNumber3Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber3Text;
            infoPanelInfo3ButtonSetNumber4Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber4Text;
            infoPanelInfo3ButtonSetNumber5Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber5Text;
            infoPanelInfo3ButtonSetNumber6Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber6Text;
            infoPanelInfo3ButtonSetNumber7Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber7Text;
            infoPanelInfo3ButtonSetNumber8Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber8Text;
            infoPanelInfo3ButtonSetNumber9Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo3ButtonSetNumber9Text;
            infoPanelInfo4Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo4Text;
            infoPanelInfo4ButtonSetNumber1Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo4ButtonSetNumber1Text;
            infoPanelInfo4ButtonSetNumber3Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo4ButtonSetNumber3Text;
            infoPanelInfo4ButtonSetNumber7Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo4ButtonSetNumber7Text;
            infoPanelInfo4ButtonSetNumber8Text.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo4ButtonSetNumber8Text;
            infoPanelInfo5Text1.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo5Text1;
            infoPanelInfo5Text2.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo5Text2;
            infoPanelInfo5Text3.text = LocalizationManager.GetLocalizationData().HowToPlay.InfoPanelInfo5Text3;
            mainMenuButtonText.text = LocalizationManager.GetLocalizationData().HowToPlay.MainMenuButtonText;
        }
    }
}
