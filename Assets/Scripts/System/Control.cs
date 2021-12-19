using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    private void Start()
    {
        CheckKeys();
    }

    private void CheckKeys()
    {
        if (PlayerPrefs.HasKey("Alpaseh-Username") && PlayerPrefs.HasKey("Alpaseh-Language"))
        {
            SceneManager.LoadScene("MainMenu");
        }

        else
        {
            SceneManager.LoadScene("Intro");
        }
    }
}
