using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text topBarTotalTimeText;
    public Text topBarTotalLifeText;
    public Text topBarActiveLevelText;
    public Text topBarTotalScoreText;
    public Text processButtonsCheckButtonText;
    public Text processButtonsDeleteButtonText;
    public Text processButtonsMainMenuButtonText;
    public Text gameOverPanelInfoText;
    public Text gameOverPanelMessageBox1InfoText;
    public Text gameOverPanelMessageBox1YesBtnText;
    public Text gameOverPanelMessageBox1NoBtnText;
    public Text gameOverPanelMessageBox2InfoText;
    public Text gameOverPanelMessageBox2OkBtnText;
    public Text gameOverPanelPlayAgainButtonText;
    public Text gameOverPanelMainMenuButtonText;
    public Text gameOverPanelExitGameButtonText;
    public Text questionText;
    public Text enteredNumberWordText;
    public Text totalTimeText;
    public Text totalLifeText;
    public Text totalScoreText;
    public Text activeLevelText;
    public GameObject lifeIncDecObj;
    public GameObject scoreIncDecObj;
    public GameObject timeIncDecObj;
    public GameObject gameOverPanel;
    public GameObject infoPanel;
    public Button[] numberButtons;
    public Button checkButton;
    public Button deleteButton;
    public Button mainMenuButton;
    public Button gameOverPanelPlayAgainButton;
    public Button gameOverPanelMainMenuButton;
    public Button gameOverPanelExitButton;
    public Button infoPanelYesButton;
    public Button infoPanelNoButton;
    public Button infoPanelOkButton;
}
