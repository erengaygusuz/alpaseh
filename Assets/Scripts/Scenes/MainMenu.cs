using FTRGames.Alpaseh.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Scenes
{
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
            if (AudioManager.loopAudioSource.clip != null)
            {
                if (AudioManager.loopAudioSource.clip.name != "main-menu")
                {
                    AudioManager.StopAudio(AudioManager.loopAudioSource);
                }
            }

            AudioManager.PlayMainMenuAudio();
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
            startGameButtonText.text = LocalizationManager.GetLocalizationData().MainMenu.StartGameButtonText;
            howToPlayButtonText.text = LocalizationManager.GetLocalizationData().MainMenu.HowToPlayButtonText;
            settingsButtonText.text = LocalizationManager.GetLocalizationData().MainMenu.SettingsButtonText;
            highScoresButtonText.text = LocalizationManager.GetLocalizationData().MainMenu.HighScoresButtonText;
            creditsButtonText.text = LocalizationManager.GetLocalizationData().MainMenu.CreditsButtonText;
            exitButtonText.text = LocalizationManager.GetLocalizationData().MainMenu.ExitButtonText;
        }
    }
}
