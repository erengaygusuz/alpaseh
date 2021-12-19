using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    private Text companyText;
    private Text developerText;
    private Text versionText;
    private Text mainMenuButtonText;
    private Button mainMenuButton;

    private void Start()
    {
        UIReferenceInit();
        UIEventBinding();
        AssignTranslatedValues();
    }

    private void UIReferenceInit()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject contents = canvas.transform.GetChild(1).gameObject;
        companyText = contents.transform.GetChild(0).GetComponent<Text>();
        developerText = contents.transform.GetChild(1).GetComponent<Text>();
        versionText = contents.transform.GetChild(4).GetComponent<Text>();
        mainMenuButton = canvas.transform.GetChild(2).GetComponent<Button>();
        mainMenuButtonText = mainMenuButton.transform.GetChild(0).GetComponent<Text>();
    }

    private void UIEventBinding()
    {
        mainMenuButton.onClick.AddListener(GoToMainMenuBtnClick);
    }

    public void GoToMainMenuBtnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void AssignTranslatedValues()
    {
        companyText.text = LocalizationManager.Instance.GetLocalizationData().Credits.CompanyText;
        developerText.text = LocalizationManager.Instance.GetLocalizationData().Credits.DeveloperText;
        versionText.text = LocalizationManager.Instance.GetLocalizationData().Credits.VersionText + Application.version;
        mainMenuButtonText.text = LocalizationManager.Instance.GetLocalizationData().Credits.MainMenuButtonText;
    }
}
