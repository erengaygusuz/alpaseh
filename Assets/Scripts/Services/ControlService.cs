using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlService
{
    private readonly AudioView audioView;

    public ControlService(AudioView audioView)
    {
        this.audioView = audioView;
    }

    public void CheckKeys()
    {
        PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("Alpaseh-Username") && PlayerPrefs.HasKey("Alpaseh-Language"))
        {
            SceneManager.LoadScene("MainMenu");
        }

        else
        {
            SceneManager.LoadScene("Intro");
        }
    }

    public void AudioLevelKeyInit()
    {
        if (!PlayerPrefs.HasKey("Alpaseh-AudioLevelSliderValue"))
        {
            PlayerPrefs.SetFloat("Alpaseh-AudioLevelSliderValue", 1.0f);
            PlayerPrefs.Save();
        }

        else
        {
            audioView.loopAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
            audioView.answerAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
            audioView.timeTickAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
        }
    }
}
