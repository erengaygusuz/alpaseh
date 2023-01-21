using FTRGames.Alpaseh.Views;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Services
{
    public class HighScoresService
    {
        private readonly LocalizationService localizationService;
        private readonly ScoreService scoreService;

        public HighScoresService(LocalizationService localizationService, ScoreService scoreService)
        {
            
            this.localizationService = localizationService;
            this.scoreService = scoreService;
        }

        public void GetAllScoreList(HighScoresView highScoresView, Func<GameObject> scoreListFactory)
        {
            for (int i = 0; i < scoreService.GetScoreList().Count; i++)
            {
                AddAScoreToScoreListContent(highScoresView, scoreListFactory, i, scoreService.GetScoreList()[i].Username, scoreService.GetScoreList()[i].Score);
            }
        }

        private void AddAScoreToScoreListContent(HighScoresView highScoresView, Func<GameObject> scoreListFactory, int index, string username, int score)
        {
            highScoresView.ScoreListRow.SetActive(true);

            GameObject tempScoreObject = scoreListFactory.Invoke();

            tempScoreObject.transform.GetChild(0).GetComponent<Text>().text = (index + 1).ToString();
            tempScoreObject.transform.GetChild(1).GetComponent<Text>().text = username;
            tempScoreObject.transform.GetChild(2).GetComponent<Text>().text = score.ToString();

            tempScoreObject.transform.SetParent(highScoresView.ScoreListContent.transform);

            tempScoreObject.transform.localScale = new Vector3(1, 1, 1);
            tempScoreObject.transform.localPosition = new Vector3(tempScoreObject.transform.localPosition.x, tempScoreObject.transform.localPosition.y, 0);

            highScoresView.ScoreListRow.SetActive(false);
        }

        public void DeleteAllScoresFromScoreListContent(HighScoresView highScoresView)
        {
            for (int i = 0; i < highScoresView.ScoreListContent.transform.childCount; i++)
            {
                GameObject.Destroy(highScoresView.ScoreListContent.transform.GetChild(i).gameObject);
            }
        }

        public void DeleteAllScoresBtnClick(HighScoresView highScoresView)
        {
            highScoresView.infoPanel.SetActive(true);

            if (scoreService.GetScoreList().Count > 0)
            {
                highScoresView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
                highScoresView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
                highScoresView.infoPanel.transform.GetChild(2).gameObject.SetActive(false);
            }

            else
            {
                highScoresView.infoPanel.transform.GetChild(0).gameObject.SetActive(false);
                highScoresView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);
                highScoresView.infoPanel.transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        public void InfoPanelYesBtnClick(HighScoresView highScoresView)
        {
            highScoresView.infoPanel.transform.GetChild(0).gameObject.SetActive(false);
            highScoresView.infoPanel.transform.GetChild(1).gameObject.SetActive(true);

            scoreService.DeleteAllScoresFromTheList();

            DeleteAllScoresFromScoreListContent(highScoresView);
        }

        public void InfoPanelNoBtnClick(HighScoresView highScoresView)
        {
            highScoresView.infoPanel.SetActive(false);
        }

        public void InfoPanelOkBtnClick(HighScoresView highScoresView)
        {
            highScoresView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
            highScoresView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);

            highScoresView.infoPanel.SetActive(false);
        }

        public void GoToMainMenuBtnClick()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void AssignTranslatedValues(HighScoresView highScoresView)
        {
            highScoresView.scoreLabelsUsernameText.text = localizationService.GetLocalizationData().HighScores.ScoreLabelsUsernameText;
            highScoresView.scoreLabelsScoreText.text = localizationService.GetLocalizationData().HighScores.ScoreLabelsScoreText;
            highScoresView.messageBox1InfoText.text = localizationService.GetLocalizationData().HighScores.MessageBox1InfoText;
            highScoresView.messageBox1YesButtonText.text = localizationService.GetLocalizationData().HighScores.MessageBox1YesButtonText;
            highScoresView.messageBox1NoButtonText.text = localizationService.GetLocalizationData().HighScores.MessageBox1NoButtonText;
            highScoresView.messageBox2InfoText.text = localizationService.GetLocalizationData().HighScores.MessageBox2InfoText;
            highScoresView.messageBox2OkButtonText.text = localizationService.GetLocalizationData().HighScores.MessageBox2OkButtonText;
            highScoresView.messageBox3InfoText.text = localizationService.GetLocalizationData().HighScores.MessageBox3InfoText;
            highScoresView.messageBox3OkButtonText.text = localizationService.GetLocalizationData().HighScores.MessageBox3OkButtonText;
            highScoresView.mainMenuButtonText.text = localizationService.GetLocalizationData().HighScores.MainMenuButtonText;
            highScoresView.deleteAllButtonText.text = localizationService.GetLocalizationData().HighScores.DeleteAllButtonText;
        }
    }
}

