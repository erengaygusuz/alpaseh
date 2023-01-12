using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Core
{
    public class GameFinishController : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameOverPanel;
        [SerializeField]
        private GameObject infoPanel;

        public static void ShowGameOverPanel()
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.transform.GetChild(1).GetComponent<Text>().text = "Game Over";
        }

        public static void ShowInfoPanelUI()
        {
            if (ScoreManager.CompareNewScoreWithScoresInTheList(GameManager.TotalScore))
            {
                Time.timeScale = 0;

                infoPanel.SetActive(true);
                infoPanel.transform.GetChild(0).gameObject.SetActive(true);
                infoPanel.transform.GetChild(1).gameObject.SetActive(false);
            }

            else
            {
                if (GameManager.IsGotoMainMenuBtnClick)
                {
                    Time.timeScale = 1;
                    GameManager.IsGotoMainMenuBtnClick = false;

                    SceneManager.LoadScene("MainMenu");
                }
            }
        }

        public void InfoPanelYesBtnClick()
        {
            ScoreManager.IsNewScoreAdded = true;

            infoPanel.transform.GetChild(0).gameObject.SetActive(false);
            infoPanel.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void InfoPanelNoBtnClick()
        {
            infoPanel.SetActive(false);

            if (GameManager.IsGotoMainMenuBtnClick)
            {
                Time.timeScale = 1;
                GameManager.IsGotoMainMenuBtnClick = false;

                SceneManager.LoadScene("MainMenu");
            }
        }

        public void InfoPanelOkBtnClick()
        {
            infoPanel.transform.GetChild(0).gameObject.SetActive(true);
            infoPanel.transform.GetChild(1).gameObject.SetActive(false);

            infoPanel.SetActive(false);

            if (GameManager.IsGotoMainMenuBtnClick)
            {
                Time.timeScale = 1;
                GameManager.IsGotoMainMenuBtnClick = false;

                SceneManager.LoadScene("MainMenu");
            }
        }

        public static void StopGameLoopAudio()
        {
            AudioManager.StopAudio(AudioManager.loopAudioSource);
        }

        public static void PlayGameOverAudio()
        {
            AudioManager.PlayGameOverAudio();
        }

        public void GoToMainMenuBtnClick()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void PlayAgainBtnClick()
        {
            SceneManager.LoadScene("Game");
        }

        public void ExitGameBtnClick()
        {
            Application.Quit();
        }
    }
}
