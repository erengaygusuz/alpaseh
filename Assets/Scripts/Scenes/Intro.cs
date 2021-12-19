using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private Text usernameLabel;
    [SerializeField]
    private Text usernameInputFieldPlaceholder;
    [SerializeField]
    private Text languageLabel;
    [SerializeField]
    private Text nextButtonText;
    [SerializeField]
    private Text messageBoxInfoText;
    [SerializeField]
    private Text messageBoxOkButtonText;
    [SerializeField]
    private Dropdown languageOptions;
    [SerializeField]
    private InputField username;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button messageBoxOkButton;
    [SerializeField]
    private GameObject warningPanel;

    private void Start()
    {
        Initializaiton();
    }

    private void Update()
    {
        if (LocalizationManager.Instance.IsLanguageChanged == true)
        {
            AssignTranslatedValues();

            LocalizationManager.Instance.IsLanguageChanged = false;
        }
    }

    private void Initializaiton()
    {
        UIEventBinding();
        GetLanguageValues();
        FillLanguageDropdown();
    }

    private void UIEventBinding()
    {
        nextButton.onClick.AddListener(NextBtnClick);
        messageBoxOkButton.onClick.AddListener(WarningPanelOKBtnClick);
        languageOptions.onValueChanged.AddListener(delegate
        {
            SaveLanguageOption();
        });
    }

    private void GetLanguageValues()
    {
        if (PlayerPrefs.HasKey("Alpaseh-SelectedLanguageIndex") == true)
        {
            languageOptions.value = PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex");
        }

        else
        {
            languageOptions.value = 0;
        }
    }

    private void FillLanguageDropdown()
    {
        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();

        for (int i = 0; i < LocalizationManager.Instance.GetLanguageCount; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(LocalizationManager.Instance.GetLocalizationData().Language[i].Name, Resources.Load<Sprite>("Flags/" + LocalizationManager.Instance.GetLanguageFlagFileNames()[i]));
            list.Add(option);
        }

        languageOptions.AddOptions(list);

        GetLanguageValues();
    }

    private void NextBtnClick()
    {
        SaveSelectedOptions();
    }

    private void SaveSelectedOptions()
    {
        if (username.text == "")
        {
            warningPanel.SetActive(true);
        }

        else
        {
            PlayerPrefs.SetString("Alpaseh-Username", username.text);
            PlayerPrefs.SetString("Alpaseh-Language", languageOptions.captionText.text);
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void WarningPanelOKBtnClick()
    {
        warningPanel.SetActive(false);
    }

    private void AssignTranslatedValues()
    {
        usernameLabel.text = LocalizationManager.Instance.GetLocalizationData().Intro.UsernameLabel;
        usernameInputFieldPlaceholder.text = LocalizationManager.Instance.GetLocalizationData().Intro.UsernameInputFieldPlaceholder;
        languageLabel.text = LocalizationManager.Instance.GetLocalizationData().Intro.LanguageLabel;
        nextButtonText.text = LocalizationManager.Instance.GetLocalizationData().Intro.NextButtonText;
        messageBoxInfoText.text = LocalizationManager.Instance.GetLocalizationData().Intro.MessageBoxInfoText;
        messageBoxOkButtonText.text = LocalizationManager.Instance.GetLocalizationData().Intro.MessageBoxOkButtonText;
    }

    public void SaveLanguageOption()
    {
        PlayerPrefs.SetInt("Alpaseh-SelectedLanguageIndex", languageOptions.value);
        PlayerPrefs.Save();

        for (int i = 0; i < LocalizationManager.Instance.GetLanguageCount; i++)
        {
            languageOptions.options[i].text = LocalizationManager.Instance.GetLocalizationData().Language[i].Name;
        }

        languageOptions.captionText.text = languageOptions.options[languageOptions.value].text;

        LocalizationManager.Instance.IsLanguageChanged = true;
    }
}
