using FTRGames.Alpaseh.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsService
{
    private readonly SettingsView settingsView;
    private readonly AudioView audioView;
    private readonly LocalizationService localizationService;

    public SettingsService(SettingsView settingsView, LocalizationService localizationService, AudioView audioView)
    {
        this.settingsView = settingsView;
        this.localizationService = localizationService;
        this.audioView = audioView;
    }

    public void Initialization()
    {
        GetUserNameValue();
        FillLanguageDropdown();
        GetLanguageValues();
        AssignTranslatedValues();
        GetAudioLevelValues();
        UIEventBinding();
    }

    private void UIEventBinding()
    {
        settingsView.generalTab.GetComponent<Button>().onClick.AddListener(GeneralTabClick);
        settingsView.personalTab.GetComponent<Button>().onClick.AddListener(PersonalTabClick);
        settingsView.mainMenuButton.onClick.AddListener(GoToMainMenuBtnClick);

        settingsView.themesToggles[0].onValueChanged.AddListener(delegate
        {
            SetSelectedColorIndex0();
        });

        settingsView.themesToggles[1].onValueChanged.AddListener(delegate
        {
            SetSelectedColorIndex1();
        });

        settingsView.themesToggles[2].onValueChanged.AddListener(delegate
        {
            SetSelectedColorIndex2();
        });

        settingsView.themesToggles[3].onValueChanged.AddListener(delegate
        {
            SetSelectedColorIndex3();
        });

        settingsView.audioLevelSlider.onValueChanged.AddListener(delegate
        {
            SetAudioLevelValues();
        });

        settingsView.languageOptions.onValueChanged.AddListener(delegate
        {
            SaveLanguageOption();
        });
    }

    private void GetUserNameValue()
    {
        settingsView.usernameValue.text = PlayerPrefs.GetString("Alpaseh-Username");
    }

    private void GetLanguageValues()
    {
        if (PlayerPrefs.HasKey("Alpaseh-SelectedLanguageIndex") == true)
        {
            settingsView.languageOptions.value = PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex");
        }

        else
        {
            settingsView.languageOptions.value = 0;
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

        settingsView.languageOptions.AddOptions(list);

        GetLanguageValues();
    }

    private void PersonalTabClick()
    {
        settingsView.personalTab.GetComponent<Image>().color = new Color32(219, 219, 219, 255);
        settingsView.generalTab.GetComponent<Image>().color = new Color32(188, 188, 188, 255);
        settingsView.personalTabContent.SetActive(true);
        settingsView.generalTabContent.SetActive(false);
    }

    private void GeneralTabClick()
    {
        settingsView.personalTab.GetComponent<Image>().color = new Color32(188, 188, 188, 255);
        settingsView.generalTab.GetComponent<Image>().color = new Color32(219, 219, 219, 255);
        settingsView.personalTabContent.SetActive(false);
        settingsView.generalTabContent.SetActive(true);

        ActivateSelectedThemeToggle();
    }

    private void AssignTranslatedValues()
    {
        settingsView.generalTabText.text = localizationService.GetLocalizationData().Settings.GeneralTabText;
        settingsView.personalTabText.text = localizationService.GetLocalizationData().Settings.PersonalTabText;
        settingsView.generalContentAudioLabel.text = localizationService.GetLocalizationData().Settings.GeneralContentAudioLabel;
        settingsView.generalContentThemesLabel.text = localizationService.GetLocalizationData().Settings.GeneralContentThemesLabel;
        settingsView.personalContentUsernameLabel.text = localizationService.GetLocalizationData().Settings.PersonalContentUsernameLabel;
        settingsView.personalContentUsernameInputFieldPlaceholder.text = localizationService.GetLocalizationData().Settings.PersonalContentUsernameInputFieldPlaceholder;
        settingsView.personalContentLanguageLabel.text = localizationService.GetLocalizationData().Settings.PersonalContentLanguageLabel;
        settingsView.mainMenuButtonText.text = localizationService.GetLocalizationData().Settings.MainMenuButtonText;
    }

    private void SaveLanguageOption()
    {
        PlayerPrefs.SetInt("Alpaseh-SelectedLanguageIndex", settingsView.languageOptions.value);
        PlayerPrefs.Save();

        for (int i = 0; i < localizationService.GetLanguageCount; i++)
        {
            settingsView.languageOptions.options[i].text = localizationService.GetLocalizationData().Language[i].Name;
        }

        settingsView.languageOptions.captionText.text = settingsView.languageOptions.options[settingsView.languageOptions.value].text;

        localizationService.IsLanguageChanged = true;
    }

    private void GoToMainMenuBtnClick()
    {
        SaveUsernameValue();

        SceneManager.LoadScene("MainMenu");
    }

    private void SaveUsernameValue()
    {
        PlayerPrefs.SetString("Alpaseh-Username", settingsView.usernameValue.text);
    }

    private void SetAudioLevelValues()
    {
        audioView.loopAudioSource.volume = settingsView.audioLevelSlider.value;
        audioView.answerAudioSource.volume = settingsView.audioLevelSlider.value;
        audioView.timeTickAudioSource.volume = settingsView.audioLevelSlider.value;

        SetAudioLevelLabelValue();

        SaveAudioLevelValue();
    }

    private void SetAudioLevelLabelValue()
    {
        settingsView.audioLevelLabelValue.text = Mathf.RoundToInt(audioView.loopAudioSource.volume * 100).ToString();
    }

    private void SaveAudioLevelValue()
    {
        PlayerPrefs.SetFloat("Alpaseh-AudioLevelSliderValue", settingsView.audioLevelSlider.value);
        PlayerPrefs.Save();
    }

    private void GetAudioLevelValues()
    {
        audioView.loopAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
        audioView.answerAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
        audioView.timeTickAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");

        SetAudioLevelLabelValue();

        settingsView.audioLevelSlider.value = audioView.loopAudioSource.volume;
    }

    private void SetSelectedColorIndex0()
    {
        int activeToggleIndex = 0;

        PlayerPrefs.SetInt("Alpaseh-SelectedColorSchemeIndex", activeToggleIndex);

        PlayerPrefs.Save();

        for (int i = 0; i < GameObject.FindObjectsOfType<ColorAssigner>().Length; i++)
        {
            GameObject.FindObjectsOfType<ColorAssigner>()[i].IsColorSchemeChanged = true;
        }
    }

    private void SetSelectedColorIndex1()
    {
        int activeToggleIndex = 1;

        PlayerPrefs.SetInt("Alpaseh-SelectedColorSchemeIndex", activeToggleIndex);

        PlayerPrefs.Save();

        for (int i = 0; i < GameObject.FindObjectsOfType<ColorAssigner>().Length; i++)
        {
            GameObject.FindObjectsOfType<ColorAssigner>()[i].IsColorSchemeChanged = true;
        }
    }

    private void SetSelectedColorIndex2()
    {
        int activeToggleIndex = 2;

        PlayerPrefs.SetInt("Alpaseh-SelectedColorSchemeIndex", activeToggleIndex);

        PlayerPrefs.Save();

        for (int i = 0; i < GameObject.FindObjectsOfType<ColorAssigner>().Length; i++)
        {
            GameObject.FindObjectsOfType<ColorAssigner>()[i].IsColorSchemeChanged = true;
        }
    }

    private void SetSelectedColorIndex3()
    {
        int activeToggleIndex = 3;

        PlayerPrefs.SetInt("Alpaseh-SelectedColorSchemeIndex", activeToggleIndex);

        PlayerPrefs.Save();

        for (int i = 0; i < GameObject.FindObjectsOfType<ColorAssigner>().Length; i++)
        {
            GameObject.FindObjectsOfType<ColorAssigner>()[i].IsColorSchemeChanged = true;
        }
    }

    private void ActivateSelectedThemeToggle()
    {
        settingsView.themesToggles[PlayerPrefs.GetInt("Alpaseh-SelectedColorSchemeIndex", 0)].isOn = true;
    }

    public void LanguageCheck()
    {
        if (localizationService.IsLanguageChanged == true)
        {
            AssignTranslatedValues();

            localizationService.IsLanguageChanged = false;
        }
    }
}
