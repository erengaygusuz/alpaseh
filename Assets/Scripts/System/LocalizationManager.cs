using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class LocalizationManager : Singleton<LocalizationManager>
{
    [SerializeField]
    private TextAsset[] languageFiles;
    private List<Localization> LocalizationDatas { get; set; }
    public bool IsLanguageChanged { get; set; }

    public void Awake()
    {
        DontDestroyOnLoad(this);

        LocalizationDataInit();

        GetAllLocalizationDatas();
    }

    private void LocalizationDataInit()
    {
        LocalizationDatas = new List<Localization>();
    }

    private void GetAllLocalizationDatas()
    {
        for (int i = 0; i < languageFiles.Length; i++)
        {
            SetLocalizationData(i);
        }
    }

    private void SetLocalizationData(int selectedLanguageIndex)
    {
        Localization tempLocal = JsonConvert.DeserializeObject<Localization>(languageFiles[selectedLanguageIndex].text);

        LocalizationDatas.Add(tempLocal);
    }

    public Localization GetLocalizationData()
    {
        return LocalizationDatas[PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex")];
    }

    public int GetLanguageCount
    {
        get 
        {
            return languageFiles.Length;
        }
    }

    public List<string> GetLanguageFlagFileNames()
    {
        List<string> tempList = new List<string>();

        for (int i = 0; i < languageFiles.Length; i++)
        {
            tempList.Add(languageFiles[i].name);
        }

        return tempList;
    }

    [Serializable]
    public struct Localization
    {
        public Intro Intro { get; set; }
        public MainMenu MainMenu { get; set; }
        public HowToPlay HowToPlay { get; set; }
        public Settings Settings { get; set; }
        public HighScores HighScores { get; set; }
        public Credits Credits { get; set; }
        public Game Game { get; set; }
        public Language[] Language { get; set; }
    }

    [Serializable]
    public struct Intro
    {
        public string UsernameLabel { get; set; }
        public string UsernameInputFieldPlaceholder { get; set; }
        public string LanguageLabel { get; set; }
        public string NextButtonText { get; set; }
        public string MessageBoxInfoText { get; set; }
        public string MessageBoxOkButtonText { get; set; }
    }

    [Serializable]
    public struct MainMenu
    {
        public string StartGameButtonText { get; set; }
        public string HowToPlayButtonText { get; set; }
        public string SettingsButtonText { get; set; }
        public string HighScoresButtonText { get; set; }
        public string CreditsButtonText { get; set; }
        public string ExitButtonText { get; set; }
    }

    [Serializable]
    public struct HowToPlay
    {
        public string InfoPanelInfo1Text { get; set; }
        public string InfoPanelInfo2Text1 { get; set; }
        public string InfoPanelInfo2Text2 { get; set; }
        public string InfoPanelInfo3Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber0Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber1Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber2Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber3Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber4Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber5Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber6Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber7Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber8Text { get; set; }
        public string InfoPanelInfo3ButtonSetNumber9Text { get; set; }
        public string InfoPanelInfo4Text { get; set; }
        public string InfoPanelInfo4ButtonSetNumber1Text { get; set; }
        public string InfoPanelInfo4ButtonSetNumber3Text { get; set; }
        public string InfoPanelInfo4ButtonSetNumber7Text { get; set; }
        public string InfoPanelInfo4ButtonSetNumber8Text { get; set; }
        public string InfoPanelInfo5Text1 { get; set; }
        public string InfoPanelInfo5Text2 { get; set; }
        public string InfoPanelInfo5Text3 { get; set; }
        public string MainMenuButtonText { get; set; }
    }

    [Serializable]
    public struct Settings
    {
        public string GeneralTabText { get; set; }
        public string PersonalTabText { get; set; }
        public string GeneralContentAudioLabel { get; set; }
        public string GeneralContentThemesLabel { get; set; }
        public string PersonalContentUsernameLabel { get; set; }
        public string PersonalContentUsernameInputFieldPlaceholder { get; set; }
        public string PersonalContentLanguageLabel { get; set; }
        public string MainMenuButtonText { get; set; }
    }

    [Serializable]
    public struct HighScores
    {
        public string ScoreLabelsUsernameText { get; set; }
        public string ScoreLabelsScoreText { get; set; }
        public string MessageBox1InfoText { get; set; }
        public string MessageBox1YesButtonText { get; set; }
        public string MessageBox1NoButtonText { get; set; }
        public string MessageBox2InfoText { get; set; }
        public string MessageBox2OkButtonText { get; set; }
        public string MessageBox3InfoText { get; set; }
        public string MessageBox3OkButtonText { get; set; }
        public string MainMenuButtonText { get; set; }
        public string DeleteAllButtonText { get; set; }
    }

    [Serializable]
    public struct Credits
    {
        public string CompanyText { get; set; }
        public string DeveloperText { get; set; }
        public string VersionText { get; set; }
        public string MainMenuButtonText { get; set; }
    }

    [Serializable]
    public struct Game
    {
        public string TopBarTotalTimeText { get; set; }
        public string TopBarTotalLifeText { get; set; }
        public string TopBarActiveLevelText { get; set; }
        public string TopBarTotalScoreText { get; set; }
        public string ProcessButtonsCheckButtonText { get; set; }
        public string ProcessButtonsDeleteButtonText { get; set; }
        public string ProcessButtonsMainMenuButtonText { get; set; }
        public string GameOverPanelInfoText { get; set; }
        public string GameOverPanelMessageBox1InfoText { get; set; }
        public string GameOverPanelMessageBox1YesBtnText { get; set; }
        public string GameOverPanelMessageBox1NoBtnText { get; set; }
        public string GameOverPanelMessageBox2InfoText { get; set; }
        public string GameOverPanelMessageBox2OkBtnText { get; set; }
        public string GameOverPanelPlayAgainButtonText { get; set; }
        public string GameOverPanelMainMenuButtonText { get; set; }
        public string GameOverPanelExitGameButtonText { get; set; }
    }

    [Serializable]
    public struct Language
    {
        public string Name { get; set; }
    }
}