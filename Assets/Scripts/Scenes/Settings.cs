using FTRGames.Alpaseh.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Scenes
{
    public class Settings : MonoBehaviour
    {
        [Header("LanguageElements")]
        [SerializeField]
        private Text generalTabText;
        [SerializeField]
        private Text personalTabText;
        [SerializeField]
        private Text generalContentAudioLabel;
        [SerializeField]
        private Text generalContentThemesLabel;
        [SerializeField]
        private Text personalContentUsernameLabel;
        [SerializeField]
        private Text personalContentUsernameInputFieldPlaceholder;
        [SerializeField]
        private Text personalContentLanguageLabel;
        [SerializeField]
        private Text mainMenuButtonText;

        [SerializeField]
        private GameObject personalTabContent;
        [SerializeField]
        private GameObject generalTabContent;
        [SerializeField]
        private GameObject personalTab;
        [SerializeField]
        private GameObject generalTab;

        [SerializeField]
        private Dropdown languageOptions;
        [SerializeField]
        private InputField usernameValue;

        [SerializeField]
        private Text audioLevelLabelValue;
        [SerializeField]
        private Slider audioLevelSlider;

        [SerializeField]
        private Toggle[] themesToggles;

        [SerializeField]
        private ToggleGroup themesToggleGroup;

        private void Start()
        {
            Initializaiton();
        }

        private void Update()
        {
            if (LocalizationManager.IsLanguageChanged == true)
            {
                AssignTranslatedValues();

                LocalizationManager.IsLanguageChanged = false;
            }
        }

        private void Initializaiton()
        {
            GetUserNameValue();
            FillLanguageDropdown();
            GetLanguageValues();
            AssignTranslatedValues();
            GetAudioLevelValues();
        }

        private void GetUserNameValue()
        {
            usernameValue.text = PlayerPrefs.GetString("Alpaseh-Username");
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

            for (int i = 0; i < LocalizationManager.GetLanguageCount; i++)
            {
                Dropdown.OptionData option = new Dropdown.OptionData(LocalizationManager.GetLocalizationData().Language[i].Name, Resources.Load<Sprite>("Flags/" + LocalizationManager.GetLanguageFlagFileNames()[i]));
                list.Add(option);
            }

            languageOptions.AddOptions(list);

            GetLanguageValues();
        }

        public void PersonalTabClick()
        {
            personalTab.GetComponent<Image>().color = new Color32(219, 219, 219, 255);
            generalTab.GetComponent<Image>().color = new Color32(188, 188, 188, 255);
            personalTabContent.SetActive(true);
            generalTabContent.SetActive(false);
        }

        public void GeneralTabClick()
        {
            personalTab.GetComponent<Image>().color = new Color32(188, 188, 188, 255);
            generalTab.GetComponent<Image>().color = new Color32(219, 219, 219, 255);
            personalTabContent.SetActive(false);
            generalTabContent.SetActive(true);

            ActivateSelectedThemeToggle();
        }

        private void AssignTranslatedValues()
        {
            generalTabText.text = LocalizationManager.GetLocalizationData().Settings.GeneralTabText;
            personalTabText.text = LocalizationManager.GetLocalizationData().Settings.PersonalTabText;
            generalContentAudioLabel.text = LocalizationManager.GetLocalizationData().Settings.GeneralContentAudioLabel;
            generalContentThemesLabel.text = LocalizationManager.GetLocalizationData().Settings.GeneralContentThemesLabel;
            personalContentUsernameLabel.text = LocalizationManager.GetLocalizationData().Settings.PersonalContentUsernameLabel;
            personalContentUsernameInputFieldPlaceholder.text = LocalizationManager.GetLocalizationData().Settings.PersonalContentUsernameInputFieldPlaceholder;
            personalContentLanguageLabel.text = LocalizationManager.GetLocalizationData().Settings.PersonalContentLanguageLabel;
            mainMenuButtonText.text = LocalizationManager.GetLocalizationData().Settings.MainMenuButtonText;
        }

        public void SaveLanguageOption()
        {
            PlayerPrefs.SetInt("Alpaseh-SelectedLanguageIndex", languageOptions.value);
            PlayerPrefs.Save();

            for (int i = 0; i < LocalizationManager.GetLanguageCount; i++)
            {
                languageOptions.options[i].text = LocalizationManager.GetLocalizationData().Language[i].Name;
            }

            languageOptions.captionText.text = languageOptions.options[languageOptions.value].text;

            LocalizationManager.IsLanguageChanged = true;
        }

        public void GoToMainMenuBtnClick()
        {
            SaveUsernameValue();

            SceneManager.LoadScene("MainMenu");
        }

        private void SaveUsernameValue()
        {
            PlayerPrefs.SetString("Alpaseh-Username", usernameValue.text);
        }

        public void SetAudioLevelValues()
        {
            AudioManager.loopAudioSource.volume = audioLevelSlider.value;
            AudioManager.answerAudioSource.volume = audioLevelSlider.value;
            AudioManager.timeTickAudioSource.volume = audioLevelSlider.value;

            SetAudioLevelLabelValue();

            SaveAudioLevelValue();
        }

        private void SetAudioLevelLabelValue()
        {
            audioLevelLabelValue.text = Mathf.RoundToInt(AudioManager.loopAudioSource.volume * 100).ToString();
        }

        private void SaveAudioLevelValue()
        {
            PlayerPrefs.SetFloat("Alpaseh-AudioLevelSliderValue", audioLevelSlider.value);
            PlayerPrefs.Save();
        }

        private void GetAudioLevelValues()
        {
            AudioManager.loopAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
            AudioManager.answerAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
            AudioManager.timeTickAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");

            SetAudioLevelLabelValue();

            audioLevelSlider.value = AudioManager.loopAudioSource.volume;
        }

        public void SetSelectedColorIndex(Toggle toggle)
        {
            if (toggle.isOn)
            {
                int activeToggleIndex = toggle.gameObject.transform.GetSiblingIndex();

                PlayerPrefs.SetInt("Alpaseh-SelectedColorSchemeIndex", activeToggleIndex);

                PlayerPrefs.Save();

                for (int i = 0; i < FindObjectsOfType<ColorAssigner>().Length; i++)
                {
                    FindObjectsOfType<ColorAssigner>()[i].IsColorSchemeChanged = true;
                }
            }
        }

        private void ActivateSelectedThemeToggle()
        {
            themesToggles[PlayerPrefs.GetInt("Alpaseh-SelectedColorSchemeIndex", 0)].isOn = true;
        }
    }
}