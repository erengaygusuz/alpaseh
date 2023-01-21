using UnityEngine;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Views
{
    public class SettingsView : MonoBehaviour
    {
        public Text generalTabText;
        public Text personalTabText;
        public Text generalContentAudioLabel;
        public Text generalContentThemesLabel;
        public Text personalContentUsernameLabel;
        public Text personalContentUsernameInputFieldPlaceholder;
        public Text personalContentLanguageLabel;
        public Text mainMenuButtonText;
        public GameObject personalTabContent;
        public GameObject generalTabContent;
        public GameObject personalTab;
        public GameObject generalTab;
        public Dropdown languageOptions;
        public InputField usernameValue;
        public Text audioLevelLabelValue;
        public Slider audioLevelSlider;
        public Toggle[] themesToggles;
        public ToggleGroup themesToggleGroup;
        public Button mainMenuButton;
    }
}