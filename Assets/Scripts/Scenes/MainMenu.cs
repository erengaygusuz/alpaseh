using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text startGameButtonText;
    [SerializeField]
    private Text howToPlayButtonText;
    [SerializeField]
    private Text settingsButtonText;
    [SerializeField]
    private Text highScoresButtonText;
    [SerializeField]
    private Text creditsButtonText;
    [SerializeField]
    private Text exitButtonText;
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Button howToPlayButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button highScoresButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private Button exitButton;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        UIEventBinding();
        PlayBackgroundAudio();
        AssignTranslatedValues();
    }

    private void PlayBackgroundAudio()
    {
        if (AudioManager.Instance.loopAudioSource.clip != null)
        {
            if (AudioManager.Instance.loopAudioSource.clip.name != "main-menu")
            {
                AudioManager.Instance.StopAudio(AudioManager.Instance.loopAudioSource);
            }
        }

        AudioManager.Instance.PlayMainMenuAudio();
    }

    private void UIEventBinding()
    {
        startGameButton.onClick.AddListener(StartGameBtnClick);
        howToPlayButton.onClick.AddListener(HowToPlayBtnClick);
        settingsButton.onClick.AddListener(SettingsBtnClick);
        highScoresButton.onClick.AddListener(HighScoresBtnClick);
        creditsButton.onClick.AddListener(CreditsBtnClick);
        exitButton.onClick.AddListener(ExitBtnClick);
    }

    private void StartGameBtnClick()
    {
        SceneManager.LoadScene("Game");
    }

    private void HowToPlayBtnClick()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    private void SettingsBtnClick()
    {
        SceneManager.LoadScene("Settings");
    }

    private void HighScoresBtnClick()
    {
        SceneManager.LoadScene("HighScores");
    }

    private void CreditsBtnClick()
    {
        SceneManager.LoadScene("Credits");
    }

    private void ExitBtnClick()
    {
        Application.Quit();
    }

    private void AssignTranslatedValues()
    {
        startGameButtonText.text = LocalizationManager.Instance.GetLocalizationData().MainMenu.StartGameButtonText;
        howToPlayButtonText.text = LocalizationManager.Instance.GetLocalizationData().MainMenu.HowToPlayButtonText;
        settingsButtonText.text = LocalizationManager.Instance.GetLocalizationData().MainMenu.SettingsButtonText;
        highScoresButtonText.text = LocalizationManager.Instance.GetLocalizationData().MainMenu.HighScoresButtonText;
        creditsButtonText.text = LocalizationManager.Instance.GetLocalizationData().MainMenu.CreditsButtonText;
        exitButtonText.text = LocalizationManager.Instance.GetLocalizationData().MainMenu.ExitButtonText;
    }
}
