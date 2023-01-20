using FTRGames.Alpaseh.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroService
{
    private readonly IntroView introView;
    private readonly LocalizationService localizationService;

    public IntroService(IntroView introView, LocalizationService localizationService)
    {
        this.introView = introView;
        this.localizationService = localizationService;
    }

    public void Initialization()
    {
        UIEventBinding();
        GetLanguageValues();
        FillLanguageDropdown();
    }

    private void UIEventBinding()
    {
        introView.nextButton.onClick.AddListener(NextBtnClick);

        introView.messageBoxOkButton.onClick.AddListener(WarningPanelOKBtnClick);

        introView.languageOptions.onValueChanged.AddListener(delegate
        {
            SaveLanguageOption();
        });
    }

    private void GetLanguageValues()
    {
        if (PlayerPrefs.HasKey("Alpaseh-SelectedLanguageIndex") == true)
        {
            introView.languageOptions.value = PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex");
        }

        else
        {
            introView.languageOptions.value = 0;
        }
    }

    private void FillLanguageDropdown()
    {
        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();

        for (int i = 0; i < localizationService.GetLanguageCount; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(localizationService.GetLocalizationData().Language[i].Name, Resources.Load<Sprite>("Flags/" + localizationService.GetLanguageFlagFileNames()[i]));
            list.Add(option);
        }

        introView.languageOptions.AddOptions(list);

        GetLanguageValues();
    }

    public void CheckLanguageState()
    {
        if (localizationService.IsLanguageChanged == true)
        {
            AssignTranslatedValues();

            localizationService.IsLanguageChanged = false;
        }
    }

    private void AssignTranslatedValues()
    {
        introView.usernameLabel.text = localizationService.GetLocalizationData().Intro.UsernameLabel;
        introView.usernameInputFieldPlaceholder.text = localizationService.GetLocalizationData().Intro.UsernameInputFieldPlaceholder;
        introView.languageLabel.text = localizationService.GetLocalizationData().Intro.LanguageLabel;
        introView.nextButtonText.text = localizationService.GetLocalizationData().Intro.NextButtonText;
        introView.messageBoxInfoText.text = localizationService.GetLocalizationData().Intro.MessageBoxInfoText;
        introView.messageBoxOkButtonText.text = localizationService.GetLocalizationData().Intro.MessageBoxOkButtonText;
    }

    private void NextBtnClick()
    {
        if (introView.username.text == "")
        {
            introView.warningPanel.SetActive(true);
        }

        else
        {
            PlayerPrefs.SetString("Alpaseh-Username", introView.username.text);
            PlayerPrefs.SetString("Alpaseh-Language", introView.languageOptions.captionText.text);
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void WarningPanelOKBtnClick()
    {
        introView.warningPanel.SetActive(false);
    }

    private void SaveLanguageOption()
    {
        PlayerPrefs.SetInt("Alpaseh-SelectedLanguageIndex", introView.languageOptions.value);
        PlayerPrefs.Save();

        for (int i = 0; i < localizationService.GetLanguageCount; i++)
        {
            introView.languageOptions.options[i].text = localizationService.GetLocalizationData().Language[i].Name;
        }

        introView.languageOptions.captionText.text = introView.languageOptions.options[introView.languageOptions.value].text;

        localizationService.IsLanguageChanged = true;
    }
}
