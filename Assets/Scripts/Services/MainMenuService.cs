using FTRGames.Alpaseh.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuService
{
    private readonly MainMenuView mainMenuView;
    private readonly AudioView audioView;
    private readonly AudioService audioService;
    private readonly LocalizationService localizationService;

    public MainMenuService(MainMenuView mainMenuView, AudioService audioService, LocalizationService localizationService, AudioView audioView)
    {
        this.mainMenuView = mainMenuView;
        this.audioService = audioService;
        this.localizationService = localizationService;
        this.audioView = audioView;
    }

    public void Initialization()
    {
        UIEventBinding();
        PlayBackgroundAudio();
        AssignTranslatedValues();
    }

    private void PlayBackgroundAudio()
    {
        if (audioView.loopAudioSource.clip != null)
        {
            if (audioView.loopAudioSource.clip.name != "main-menu")
            {
                audioService.StopAudio(audioView.loopAudioSource);
            }
        }

        audioService.PlayMainMenuAudio();
    }

    private void UIEventBinding()
    {
        mainMenuView.startGameButton.onClick.AddListener(StartGameBtnClick);
        mainMenuView.howToPlayButton.onClick.AddListener(HowToPlayBtnClick);
        mainMenuView.settingsButton.onClick.AddListener(SettingsBtnClick);
        mainMenuView.highScoresButton.onClick.AddListener(HighScoresBtnClick);
        mainMenuView.creditsButton.onClick.AddListener(CreditsBtnClick);
        mainMenuView.exitButton.onClick.AddListener(ExitBtnClick);
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
        mainMenuView.startGameButtonText.text = localizationService.GetLocalizationData().MainMenu.StartGameButtonText;
        mainMenuView.howToPlayButtonText.text = localizationService.GetLocalizationData().MainMenu.HowToPlayButtonText;
        mainMenuView.settingsButtonText.text = localizationService.GetLocalizationData().MainMenu.SettingsButtonText;
        mainMenuView.highScoresButtonText.text = localizationService.GetLocalizationData().MainMenu.HighScoresButtonText;
        mainMenuView.creditsButtonText.text = localizationService.GetLocalizationData().MainMenu.CreditsButtonText;
        mainMenuView.exitButtonText.text = localizationService.GetLocalizationData().MainMenu.ExitButtonText;
    }
}
