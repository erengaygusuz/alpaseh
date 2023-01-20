using FTRGames.Alpaseh.Core;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoresService
{
    private readonly HighScoresView highScoresView;
    private readonly LocalizationService localizationService;
    private readonly ScoreService scoreService;

    public HighScoresService(HighScoresView highScoresView, LocalizationService localizationService, ScoreService scoreService)
    {
        this.highScoresView = highScoresView;
        this.localizationService = localizationService;
        this.scoreService = scoreService;
    }

    public void Initialization(Func<GameObject> scoreListFactory)
    {
        UIEventBinding();
        AssignTranslatedValues();
        GetAllScoreList(scoreListFactory);
    }

    private void UIEventBinding()
    {
        highScoresView.mainMenuButton.onClick.AddListener(GoToMainMenuBtnClick);
        highScoresView.deleteAllRecordsButton.onClick.AddListener(DeleteAllScoresBtnClick);
        highScoresView.infoPanelYesButton.onClick.AddListener(InfoPanelYesBtnClick);
        highScoresView.infoPanelNoButton.onClick.AddListener(InfoPanelNoBtnClick);
        highScoresView.infoPanelOk1Button.onClick.AddListener(InfoPanelOkBtnClick);
        highScoresView.infoPanelOk2Button.onClick.AddListener(InfoPanelOkBtnClick);
    }

    private void GetAllScoreList(Func<GameObject> scoreListFactory)
    {
        for (int i = 0; i < scoreService.GetScoreList().Count; i++)
        {
            AddAScoreToScoreListContent(scoreListFactory, i, scoreService.GetScoreList()[i].Username, scoreService.GetScoreList()[i].Score);
        }
    }

    private void AddAScoreToScoreListContent(Func<GameObject> scoreListFactory, int index, string username, int score)
    {
        highScoresView.ScoreListRow.SetActive(true);

        GameObject tempScoreObject = scoreListFactory.Invoke(); //GameObject.Instantiate(highScoresView.ScoreListRow.transform).gameObject;

        tempScoreObject.transform.GetChild(0).GetComponent<Text>().text = (index + 1).ToString();
        tempScoreObject.transform.GetChild(1).GetComponent<Text>().text = username;
        tempScoreObject.transform.GetChild(2).GetComponent<Text>().text = score.ToString();

        tempScoreObject.transform.SetParent(highScoresView.ScoreListContent.transform);

        tempScoreObject.transform.localScale = new Vector3(1, 1, 1);
        tempScoreObject.transform.localPosition = new Vector3(tempScoreObject.transform.localPosition.x, tempScoreObject.transform.localPosition.y, 0);

        highScoresView.ScoreListRow.SetActive(false);
    }

    private void DeleteAllScoresFromScoreListContent()
    {
        for (int i = 0; i < highScoresView.ScoreListContent.transform.childCount; i++)
        {
            GameObject.Destroy(highScoresView.ScoreListContent.transform.GetChild(i).gameObject);
        }
    }

    private void DeleteAllScoresBtnClick()
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

    private void InfoPanelYesBtnClick()
    {
        highScoresView.infoPanel.transform.GetChild(0).gameObject.SetActive(false);
        highScoresView.infoPanel.transform.GetChild(1).gameObject.SetActive(true);

        scoreService.DeleteAllScoresFromTheList();

        DeleteAllScoresFromScoreListContent();
    }

    private void InfoPanelNoBtnClick()
    {
        highScoresView.infoPanel.SetActive(false);
    }

    private void InfoPanelOkBtnClick()
    {
        highScoresView.infoPanel.transform.GetChild(0).gameObject.SetActive(true);
        highScoresView.infoPanel.transform.GetChild(1).gameObject.SetActive(false);

        highScoresView.infoPanel.SetActive(false);
    }

    private void GoToMainMenuBtnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void AssignTranslatedValues()
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
