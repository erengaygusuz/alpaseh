using FTRGames.Alpaseh.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Scenes
{
    public class HighScores : MonoBehaviour
    {
        [Header("LanguageElements")]
        [SerializeField]
        private Text scoreLabelsUsernameText;
        [SerializeField]
        private Text scoreLabelsScoreText;
        [SerializeField]
        private Text messageBox1InfoText;
        [SerializeField]
        private Text messageBox1YesButtonText;
        [SerializeField]
        private Text messageBox1NoButtonText;
        [SerializeField]
        private Text messageBox2InfoText;
        [SerializeField]
        private Text messageBox2OkButtonText;
        [SerializeField]
        private Text messageBox3InfoText;
        [SerializeField]
        private Text messageBox3OkButtonText;
        [SerializeField]
        private Text mainMenuButtonText;
        [SerializeField]
        private Text deleteAllButtonText;

        [SerializeField]
        private GameObject ScoreListContent;

        [SerializeField]
        private GameObject ScoreListRow;

        [SerializeField]
        private GameObject infoPanel;

        private void Start()
        {
            Initializaiton();
        }

        private void Initializaiton()
        {
            AssignTranslatedValues();
            GetAllScoreList();
        }

        private void GetAllScoreList()
        {
            for (int i = 0; i < ScoreManager.GetScoreList().Count; i++)
            {
                AddAScoreToScoreListContent(i, ScoreManager.GetScoreList()[i].Username, ScoreManager.GetScoreList()[i].Score);
            }
        }

        private void AddAScoreToScoreListContent(int index, string username, int score)
        {
            ScoreListRow.SetActive(true);

            GameObject tempScoreObject = Instantiate(ScoreListRow.transform).gameObject;

            tempScoreObject.transform.GetChild(0).GetComponent<Text>().text = (index + 1).ToString();
            tempScoreObject.transform.GetChild(1).GetComponent<Text>().text = username;
            tempScoreObject.transform.GetChild(2).GetComponent<Text>().text = score.ToString();

            tempScoreObject.transform.SetParent(ScoreListContent.transform);

            tempScoreObject.transform.localScale = new Vector3(1, 1, 1);
            tempScoreObject.transform.localPosition = new Vector3(tempScoreObject.transform.localPosition.x, tempScoreObject.transform.localPosition.y, 0);

            ScoreListRow.SetActive(false);
        }

        private void DeleteAllScoresFromScoreListContent()
        {
            for (int i = 0; i < ScoreListContent.transform.childCount; i++)
            {
                Destroy(ScoreListContent.transform.GetChild(i).gameObject);
            }
        }

        public void DeleteAllScoresBtnClick()
        {
            infoPanel.SetActive(true);

            if (ScoreManager.GetScoreList().Count > 0)
            {
                infoPanel.transform.GetChild(0).gameObject.SetActive(true);
                infoPanel.transform.GetChild(1).gameObject.SetActive(false);
                infoPanel.transform.GetChild(2).gameObject.SetActive(false);
            }

            else
            {
                infoPanel.transform.GetChild(0).gameObject.SetActive(false);
                infoPanel.transform.GetChild(1).gameObject.SetActive(false);
                infoPanel.transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        public void InfoPanelYesBtnClick()
        {
            infoPanel.transform.GetChild(0).gameObject.SetActive(false);
            infoPanel.transform.GetChild(1).gameObject.SetActive(true);

            ScoreManager.DeleteAllScoresFromTheList();

            DeleteAllScoresFromScoreListContent();
        }

        public void InfoPanelNoBtnClick()
        {
            infoPanel.SetActive(false);
        }

        public void InfoPanelOkBtnClick()
        {
            infoPanel.transform.GetChild(0).gameObject.SetActive(true);
            infoPanel.transform.GetChild(1).gameObject.SetActive(false);

            infoPanel.SetActive(false);
        }

        public void GoToMainMenuBtnClick()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void AssignTranslatedValues()
        {
            scoreLabelsUsernameText.text = LocalizationManager.GetLocalizationData().HighScores.ScoreLabelsUsernameText;
            scoreLabelsScoreText.text = LocalizationManager.GetLocalizationData().HighScores.ScoreLabelsScoreText;
            messageBox1InfoText.text = LocalizationManager.GetLocalizationData().HighScores.MessageBox1InfoText;
            messageBox1YesButtonText.text = LocalizationManager.GetLocalizationData().HighScores.MessageBox1YesButtonText;
            messageBox1NoButtonText.text = LocalizationManager.GetLocalizationData().HighScores.MessageBox1NoButtonText;
            messageBox2InfoText.text = LocalizationManager.GetLocalizationData().HighScores.MessageBox2InfoText;
            messageBox2OkButtonText.text = LocalizationManager.GetLocalizationData().HighScores.MessageBox2OkButtonText;
            messageBox3InfoText.text = LocalizationManager.GetLocalizationData().HighScores.MessageBox3InfoText;
            messageBox3OkButtonText.text = LocalizationManager.GetLocalizationData().HighScores.MessageBox3OkButtonText;
            mainMenuButtonText.text = LocalizationManager.GetLocalizationData().HighScores.MainMenuButtonText;
            deleteAllButtonText.text = LocalizationManager.GetLocalizationData().HighScores.DeleteAllButtonText;
        }
    }
}