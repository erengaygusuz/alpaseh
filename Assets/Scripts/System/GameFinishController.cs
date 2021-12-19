using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFinishController : Singleton<GameFinishController>
{
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject infoPanel;

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetChild(1).GetComponent<Text>().text = "Game Over";
    }

    public void ShowInfoPanelUI()
    {
        if (ScoreManager.Instance.CompareNewScoreWithScoresInTheList(GameManager.Instance.TotalScore))
        {
            Time.timeScale = 0;

            infoPanel.SetActive(true);
            infoPanel.transform.GetChild(0).gameObject.SetActive(true);
            infoPanel.transform.GetChild(1).gameObject.SetActive(false);
        }

        else
        {
            if (GameManager.Instance.IsGotoMainMenuBtnClick)
            {
                Time.timeScale = 1;
                GameManager.Instance.IsGotoMainMenuBtnClick = false;

                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void InfoPanelYesBtnClick()
    {
        ScoreManager.Instance.IsNewScoreAdded = true;

        infoPanel.transform.GetChild(0).gameObject.SetActive(false);
        infoPanel.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void InfoPanelNoBtnClick()
    {
        infoPanel.SetActive(false);

        if (GameManager.Instance.IsGotoMainMenuBtnClick)
        {
            Time.timeScale = 1;
            GameManager.Instance.IsGotoMainMenuBtnClick = false;

            SceneManager.LoadScene("MainMenu");
        }
    }

    public void InfoPanelOkBtnClick()
    {
        infoPanel.transform.GetChild(0).gameObject.SetActive(true);
        infoPanel.transform.GetChild(1).gameObject.SetActive(false);

        infoPanel.SetActive(false);

        if (GameManager.Instance.IsGotoMainMenuBtnClick)
        {
            Time.timeScale = 1;
            GameManager.Instance.IsGotoMainMenuBtnClick = false;

            SceneManager.LoadScene("MainMenu");
        }
    }

    public void StopGameLoopAudio()
    {
        AudioManager.Instance.StopAudio(AudioManager.Instance.loopAudioSource);
    }

    public void PlayGameOverAudio()
    {
        AudioManager.Instance.PlayGameOverAudio();
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
